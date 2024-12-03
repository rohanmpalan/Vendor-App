using System.Windows.Input;
using VendorApp.Constant;
using VendorApp.Model;
using VendorApp.Interface;

namespace VendorApp.ViewModel;

public class VendorDetailsPageViewModel : BaseViewModel<Vendor>, IQueryAttributable
{


     private Vendor _vendor;
        public Vendor Vendor
        {
            get => _vendor;
            set => SetProperty(ref _vendor, value);
        }  

      public VendorDetailsPageViewModel(INavigationService navigationService,
            Vendor model = null) : base(navigationService, model)
        {
			
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Model = query[ApplicationConstant.NavigationParameterKey] as Vendor;
            Vendor = Model;
        }
}