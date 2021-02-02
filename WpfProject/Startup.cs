using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WpfProject.DAL;

namespace WpfProject
{
    public partial class Startup : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Application app = new Application();

            var context = DataContextAccesor.GetDataContext();

            context.Database.EnsureCreated();

            MainWindow w = new MainWindow();
            w.Show();
        }
    }
}
