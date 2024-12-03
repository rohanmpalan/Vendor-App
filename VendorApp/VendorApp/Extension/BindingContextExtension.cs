using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace VendorApp.Extension
{
    public class BindingContextExtension<T> : IMarkupExtension<T>
    {
        public T ProvideValue(IServiceProvider serviceProvider)
        {
            return ServiceHelper.GetService<T>();
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }
}

