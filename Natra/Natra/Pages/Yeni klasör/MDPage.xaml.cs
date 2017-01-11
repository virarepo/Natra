using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Natra
{
    public partial class MDPage : MasterDetailPage
    {
        public MDPage()
        {
            //InitializeComponent();
            var dp = new MasterPage();
            dp.Title = "dddd";
            Master = dp;
            //Master.Title = "master";
            Detail = new MainPage();
            Title = "Natra";

            ToolbarItems.Add(new ToolbarItem("Sepetim(1)", null, async () =>
            {
                //Example Toolbar item and dialog box
                //var page = new ContentPage();
                //var result = await page.DisplayAlert("howToGo", "Message", "Accept", "Cancel");
                //this.ShouldShowToolbarButton();
            }));

            //ToolbarItems.Add(new ToolbarItem("Sepetim", "icon.png", async () =>
            //{
            //    //Example Toolbar item and dialog box
            //    //var page = new ContentPage();
            //    //var result = await page.DisplayAlert("howToGo", "Message", "Accept", "Cancel");
            //    //this.ShouldShowToolbarButton();
            //}));

            
        }
    }
}
