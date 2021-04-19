using System;
using System.Windows;

namespace WpfNestedGridApp
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string startupXaml = SingletonProgramConfiguration.Instance.GetConfigurationData()["startupxaml"];
            this.StartupUri = new Uri(startupXaml, UriKind.Relative);
        }

        protected override void OnActivated(EventArgs e)
        {
            //this.Resources["enumConverter"] = 
            // base.OnActivated(e);
        }
    }
}
