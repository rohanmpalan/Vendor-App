using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VendorApp.Interface;
using VendorApp.Model;

namespace VendorApp.ViewModel
{
	public class MainPageViewModel : BaseViewModel
    {

		private List<Vendor> _vendorList;
        public List<Vendor> VendorList
        {
            get => _vendorList;
            set => SetProperty(ref _vendorList, value);
        }

		private ObservableCollection<Vendor> _filteredvendorList;
        public ObservableCollection<Vendor> FiliteredVendorList
        {
            get => _filteredvendorList;
            set => SetProperty(ref _filteredvendorList, value);
        }

		private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set {
				 SetProperty(ref _searchText, value);
                  SearchCommandHandler();
			    }
        }
        
		private readonly IVendorService _vendorService;

		//public ICommand SearchVendorCommand;
		public MainPageViewModel(INavigationService navigationService, IVendorService vendorService) : base(navigationService)
        {
			_vendorService= vendorService;
			Initialize();
		}


		public ICommand SelectionChangedCommand => new Command<Vendor>( async (Vendor vendor) =>
				{
				    await _navigationService.NavigateToAsync(nameof(VendorDetailsPageViewModel),vendor);
				});

        private void SearchCommandHandler()
        {
           FiliteredVendorList = new ObservableCollection<Vendor>(VendorList.Where(v => v.Name.Contains(SearchText)));
        }

        private void Initialize()
        {
            VendorList =  _vendorService.GetVendors();
			FiliteredVendorList = new ObservableCollection<Vendor>(VendorList);
			
        }
		
    }
}

