using System;
using System.Windows.Input;
using VendorApp.Extension;
using VendorApp.Infrastructure;
using VendorApp.Interface;

namespace VendorApp.ViewModel
{
	public class BaseViewModel<TModel> : BaseObservableObject
    {
        protected readonly INavigationService _navigationService;

        public BaseViewModel(INavigationService navigationService,
                            TModel model = default)
        {
            _navigationService = navigationService;

            Model = model;
        }

        public BaseViewModel(TModel model) : this(
            ServiceHelper.GetService<INavigationService>(),
            model)
        {
        }

        public TModel Model { get; protected set; }

        string _Title;
        public string Title
        {
            get => _Title;
            set => SetProperty(ref _Title, value);
        }

        bool _IsBusy;
        public bool IsBusy
        {
            get => _IsBusy;
            set => SetProperty(ref _IsBusy, value);
        }
        protected internal Task UpdateModelAsync(TModel model)
        {
            Model = model;

            return ModelUpdatedVirtualAsync(model);
        }

        protected virtual Task ModelUpdatedVirtualAsync(TModel model)
        {
            return Task.CompletedTask;
        }

        protected virtual Task DisappearingCommandAsyncVirtual() => Task.CompletedTask;

        protected virtual Task BackCommandAsyncVirtual()
        {
            return _navigationService.NavigateBackAsync();
        }

    }

    public class BaseViewModel : BaseViewModel<object>
    {
        public BaseViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        public BaseViewModel() : this(
            ServiceHelper.GetService<INavigationService>())
        {
        }
    }
}

