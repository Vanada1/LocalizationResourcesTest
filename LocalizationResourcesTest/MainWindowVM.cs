using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using Core;
using LocalizationResourcesTest.Properties;
using Resources = LocalizationResourcesTest.Properties.VMResources.MainWindow;

namespace LocalizationResourcesTest
{
    /// <summary>
    /// VM главного окна.
    /// </summary>
    public class MainWindowVM : ObservableObject
    {
        /// <summary>
        /// Имя стандартной культуры.
        /// </summary>
        private readonly string _defaultCultureName;

        /// <summary>
        /// Путь до папки с настройками.
        /// </summary>
        private readonly string _directory =
            $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\TestSettings";

        /// <summary>
        /// Выбранная культура.
        /// </summary>
        private string _selectedCulture;

        /// <summary>
        /// Коллекция культур.
        /// </summary>
        private Dictionary<string, string> _cultures;

        /// <summary>
        /// Строковое значение.
        /// </summary>
        private string _value;

        /// <summary>
        /// Событие изменения культуры.
        /// </summary>
        public event EventHandler UpdatedCulture;

        /// <summary>
        /// Создание VM главного окна.
        /// </summary>
        public MainWindowVM()
        {
            _defaultCultureName = CultureInfo.CurrentCulture.Name;
            _cultures = new Dictionary<string, string>()
            {
                [_defaultCultureName] = Resources.DefaultCulture,
                ["ru"] = Resources.RuCulture,
                ["en"] = Resources.EnCulture,
                ["de"] = Resources.DeCulture,
                ["zh"] = Resources.ZhCulture
            };

            SelectedCulture = File.Exists(FilePath) ? File.ReadAllText(FilePath) : _defaultCultureName;

            // TODO: Использзуется абсолютный путь у DllPath. Поменять на свой
            var dll = Assembly.LoadFrom(DllPath);
            var type = dll.GetType("LoadingProjectTest.TestClass");
            var obj = (IInterface)Activator.CreateInstance(type);
            MyName = obj.Name;
        }

        public string FilePath => $"{_directory}\\Settings.txt";

        /// <summary>
        /// Коллекция культур.
        /// </summary>
        public Dictionary<string, string> Cultures
        {
            get => _cultures;
            private set => SetProperty(ref _cultures, value);
        }

        /// <summary>
        /// Выбранная культура.
        /// </summary>
        public string SelectedCulture
        {
            get => _selectedCulture;
            set
            {
                if (value == null)
                {
                    return;
                }

                if (SetProperty(ref _selectedCulture, value))
                {
                    UpdateCulture();
                }
            }
        }

        /// <summary>
        /// Строковое значение.
        /// </summary>
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        /// <summary>
        /// Мое имя.
        /// </summary>
        public string MyName { get; }

        // TODO: Тут использзуется абсолютный путь. Поменять на свой
        private string DllPath =>
            @"C:\Users\vovas\source\repos\LocalizationResourcesTest\LoadingProjectTest\bin\Debug\LoadingProjectTest.dll";

        /// <summary>
        /// Обновляет культуру.
        /// </summary>
        private void UpdateCulture()
        {
            var newCultureInfo = new CultureInfo(SelectedCulture);
            CultureInfo.DefaultThreadCurrentUICulture = newCultureInfo;
            Resources.Culture = newCultureInfo;

            var newCultureNames = new Dictionary<string, string>()
            {
                [_defaultCultureName] = Resources.DefaultCulture,
                ["ru"] = Resources.RuCulture,
                ["en"] = Resources.EnCulture,
                ["de"] = Resources.DeCulture,
                ["zh"] = Resources.ZhCulture
            };

            Cultures = newCultureNames;
            UpdatedCulture?.Invoke(this, EventArgs.Empty);

            if (!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }

            File.WriteAllText(FilePath, SelectedCulture);
        }
    }
}