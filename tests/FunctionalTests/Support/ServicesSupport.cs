using FunctionalTests.Extensions;
using Reqnroll;
using Reqnroll.BoDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalTests.Support
{
    [Binding]
    internal class ServicesSupport
    {
        private readonly IObjectContainer _container;

        public ServicesSupport(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void InitializeContainer()
        {
            _container
                .AddConfig()
                .AddRunningTrackerApi();
        }
    }
}
