using Natra.Helpers;
using Natra.HttpConnector;
using Natra.Models;
using Natra.UICommunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Natra.Pages
{
    public partial class SepetPage : ContentPage
    {

        List<Siparis> siparises;

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                sepetPageActivityIndicator.IsVisible = value;
                sepetPageActivityIndicator.IsRunning = value;
                sepetPageForm.IsVisible = !value;
                _isBusy = value;
                //OnPropertyChanged();
            }
        }

        public SepetPage()
        {
            InitializeComponent();

            siparises = new List<Siparis>();

            Title = Globals.SepetPageTitle;

            getSiparises();

            sepetPageOnayButton.Clicked += async (s, e) =>
            {
                //var rs =;
                IsBusy = true;
                bool result = await new RestService().sepetOnay(siparises);
                IsBusy = false;
                await showResultPopup(result);

                await App.AppInstance.MainPage.Navigation.PopAsync();
                //bool a= { () => await rs.sepetOnay(siparises) };
            };
            

        }

        private async Task<bool> showResultPopup(bool result)
        {
            return await new PopupManager().showInfoPopup(this,Globals.siparisinizOnaylandi);
        }

        private void setLabels()
        {
            double genTop = 0;
            double kdvTop = 0;
            double brutTop=0;
            foreach (var siparis in siparises)
            {
                genTop += siparis.GenelToplam;
                kdvTop += siparis.KDVToplam;
                brutTop += siparis.BrutTutar;
            }
            sepetPageBrutToplamLabel.Text = string.Format("{0} : {1} TL",Globals.BrutToplam,brutTop);
            sepetPageGenelToplamLabel.Text = string.Format("{0} : {1} TL",Globals.GenelToplam,genTop);
            sepetPageKDVToplamLabel.Text = string.Format("{0} : {1} TL",Globals.KDVToplami,kdvTop);
        }

        public async void OnSiparisDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var sip = mi.CommandParameter as Siparis;
            bool action =await DisplayAlert(Globals.siparisSilinsinMi, sip.HesapKodu, "Evet", "Hayır");
            if (action)
            {
                DBHelper.deleteSiparis(sip);
                getSiparises();
            }
        }

        private void getSiparises()
        {
            siparises = DBHelper.getAllSiparises();

            //sepetListView.ItemsSource = DBHelper.getAllSiparises();
            sepetListView.ItemsSource = siparises;

            setLabels();
        }

        
    }
}
