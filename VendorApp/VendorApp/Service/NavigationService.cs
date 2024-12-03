using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorApp.Interface;
using VendorApp.Constant;

namespace VendorApp.Service
{
    public class NavigationService : INavigationService
    {
        public Task NavigateBackAsync()
        {
            return Shell.Current.GoToAsync("..");
        }

        public Task NavigateToAsync(string route)
        {
            return Shell.Current.GoToAsync(route);
        }
        public Task NavigateToAsync(string route, object parameter)
        {
            var navigationParam = new Dictionary<string, object>()
            {
                { ApplicationConstant.NavigationParameterKey, parameter}
            };
            return Shell.Current.GoToAsync(route, navigationParam);
        }

        public Task NavigateToRootAsync()
        {
            return Shell.Current.CurrentPage.Navigation.PopToRootAsync();
        }
    }
}

