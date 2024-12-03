using System;
using VendorApp.ViewModel;
using VendorApp.Interface;
using VendorApp.Service;
using CommunityToolkit.Maui;
namespace VendorApp.Extension
{
    public static class AppBuilder
    {

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IVendorService, VendorService>();
            return builder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<VendorDetailsPage>();
            return builder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<BaseViewModel>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<VendorDetailsPageViewModel>();
            return builder;
        }

        public static MauiAppBuilder RegisterNavigation(this MauiAppBuilder builder)
        {
             builder.Services.AddTransientWithShellRoute<MainPage, MainPageViewModel>(nameof(MainPageViewModel));
             builder.Services.AddTransientWithShellRoute<VendorDetailsPage, VendorDetailsPageViewModel>(nameof(VendorDetailsPageViewModel));
            return builder;
        }

    }
}

