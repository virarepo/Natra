using Natra.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Natra.Pages
{
    public partial class MDPage : MasterDetailPage
    {

        protected override void OnAppearing() //page on the screen
        {
            base.OnAppearing();
            sepetim.Text = string.Format("Sepetim({0})", DBHelper.getSiparisCount());

            if (!DBHelper.checkLoggedIn())
            {
                //openLoginPage();
                return;
            }

            //if (DBHelper.checkFirstOpenningApp()) openTutorialPage();
        }

        ToolbarItem sepetim = null;
        public MDPage()
        {

            
            //InitializeComponent();
            var mp = new MasterPage();
            mp.Title = "MasterPage Title";
            Master = mp;
            //Master.Title = "master";
            Detail = new MainPage();
            Title = "Natra";

            


            sepetim = new ToolbarItem("", null, () =>
             {
                 openSepetPage();
                //App.Current.Properties[new Random().Next(100000).ToString()] = new Random().Next(100000);


                 //Example Toolbar item and dialog box
                 //var page = new ContentPage();
                 //var result = await page.DisplayAlert("howToGo", "Message", "Accept", "Cancel");
                 //this.ShouldShowToolbarButton();
             });

            ToolbarItems.Add(sepetim);

            ToolbarItems.Add(new ToolbarItem("okuytt", null, () =>
            {
               // App.Current.Properties[new Random().Next(100000).ToString()] = new Random().Next(100000);
                foreach (var key in App.Current.Properties.Keys)
                {
                    System.Diagnostics.Debug.WriteLine(key);
                }

            }));

            ToolbarItems.Add(new ToolbarItem("silll", null, () =>
            {
                // App.Current.Properties[new Random().Next(100000).ToString()] = new Random().Next(100000);

                //App.Current.Properties.Remove()
                foreach (var key in App.Current.Properties.Keys)
                {
                    System.Diagnostics.Debug.WriteLine(key);
                }

            }));

            //ToolbarItems.Add(new ToolbarItem("Sepetim", "icon.png", async () =>
            //{
            //    //Example Toolbar item and dialog box
            //    //var page = new ContentPage();
            //    //var result = await page.DisplayAlert("howToGo", "Message", "Accept", "Cancel");
            //    //this.ShouldShowToolbarButton();
            //}));
        }

        private void openTutorialPage()
        {
            App.AppInstance.MainPage.Navigation.PushAsync(new TutorialPage());
        }

        private void openLoginPage()
        {
            App.AppInstance.MainPage.Navigation.PushAsync(new LoginPage());
        }

        private void openSepetPage()
        {
            App.AppInstance.MainPage.Navigation.PushAsync(new SepetPage());
        }

    }
}
