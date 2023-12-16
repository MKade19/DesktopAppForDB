using BusStation.UI.ViewModels;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using BusStation.UI.Services.Abstract;
using BusStation.UI.Services;

namespace BusStation.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel()
            };

            base.OnStartup(e);
        }

        public App()
        {
            Application.Current.DispatcherUnhandledException +=Current_DispatcherUnhandledException;


            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // show error here
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<IBusProducerDataService, BusProducerDataService>();
        }
    }
}
