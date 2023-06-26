using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
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
        /// Выбранная культура.
        /// </summary>
        private string _selectedCulture;

        /// <summary>
        /// Коллекция культур.
        /// </summary>
        private Dictionary<string, string> _cultures;

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
            SelectedCulture = _defaultCultureName;
        }

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
        /// Обновляет культуру.
        /// </summary>
        private void UpdateCulture()
        {
            var newCultureInfo = new CultureInfo(SelectedCulture);
            CultureInfo.DefaultThreadCurrentCulture = newCultureInfo;
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
        }
    }
}