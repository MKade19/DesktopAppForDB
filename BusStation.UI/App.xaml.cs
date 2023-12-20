using BusStation.UI.Services;
using BusStation.UI.Services.Abstract;
using BusStation.UI.Util;
using BusStation.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace BusStation.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);

            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<IBusProducerDataService, BusProducerDataService>();
            services.AddTransient<IBusModelDataService, BusModelDataService>();
            services.AddTransient<IBusDataService, BusDataService>();
            services.AddTransient<IBusRouteDataService, BusRouteDataService>();
            services.AddTransient<IPositionDataService, PositionDataService>();
            services.AddTransient<IWorkerDataService, WorkerDataService>();
            services.AddTransient<IMedicalInspectionDataService, MedicalInspectionDataService>();
            services.AddTransient<ITechnicalInspectionDataService, TechnicalInspectionDataService>();
            services.AddTransient<IRepairmentDataService, RepairmentDataService>();
            services.AddTransient<IVoyageDataService, VoyageDataService>();
            services.AddTransient<IAuthDataService, AuthDataService>();
            services.AddSingleton<UserStateViewModel>();
            services.AddSingleton<MainWindow>();
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBoxStore.Error(e.Exception.Message);
            e.Handled = true;
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
