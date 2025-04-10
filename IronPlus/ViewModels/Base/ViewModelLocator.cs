using System;
using TinyIoC;
using Microsoft.Maui;
using IronPlus.Interfaces;
using IronPlus.Services;
using System.Reflection;
using System.Globalization;

namespace IronPlus.ViewModels
{
    public static class ViewModelLocator
    {
        private static TinyIoCContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        public static bool UseMockService { get; set; }

        static ViewModelLocator()
        {
            _container = new TinyIoCContainer();

            // View models - by default, TinyIoC will register concrete classes as multi-instance.
            _container.Register<RpeChartCalculationViewModel>();
            _container.Register<UnitConversionViewModel>();
            _container.Register<WarmUpCalculationViewModel>();
            _container.Register<SettingsViewModel>();
            _container.Register<ShowSpecificWeightViewModel>();
            _container.Register<AboutViewModel>();
            _container.Register<ThemeSettingsViewModel>();
            _container.Register<HowToUseRpeChartViewModel>();
            _container.Register<HowToUseUnitConversionViewModel>();
            _container.Register<HowToUseWarmUpCalculationViewModel>();
            _container.Register<BarbellSettingsViewModel>();
            _container.Register<AddBarbellDetailsViewModel>();


            // Services - by default, TinyIoC will register interface registrations as singletons.
            _container.Register<IRpeCalculationService, RpeCalculationService>();
            _container.Register<IWarmUpCalculationService, WarmUpCalculationService>();
            _container.Register<IDatabaseService, DatabaseService>();
            _container.Register<IDialogService, DialogService>();
            _container.Register<ISettingsService, SettingsService>();
            _container.Register<IThemeService, ThemeService>();
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _container.Register<TInterface, T>().AsSingleton();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            if (viewName.Contains("Page"))
            {
                viewName = viewName.Replace("Page", "View");
            }
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
