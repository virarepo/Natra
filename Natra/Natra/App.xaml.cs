using Natra.Models;
using Natra.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Natra
{
    public partial class App : Application
    {

        private static App _appInstance;

        public static App AppInstance  //to make app reachable from everywhere
        {
            get { return _appInstance; }
            private set { _appInstance = value; }
        }

        public User currentUser { get; private set; }

        public App()
        {
            AppInstance = this;

            InitializeComponent();

            arrangeUser();

            MainPage = new NavigationPage(new MDPage());
            //openLoginPage();
        }

        private void arrangeUser()
        {
            currentUser = new User() { username = "cari1", pwd="cari1pwd"};
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
