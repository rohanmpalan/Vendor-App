using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorApp.Interface
{
	public interface INavigationService
	{
        Task NavigateBackAsync();
        Task NavigateToAsync(string route);
        public Task NavigateToAsync(string route, object parameter);
        Task NavigateToRootAsync();
    }
}

