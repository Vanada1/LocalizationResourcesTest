using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using LoadingProjectTest.Properties.TestClassResources;

namespace LoadingProjectTest
{
    public class TestClass : IInterface
    {
        public string Name => LoadingProject.MyName;
    }
}
