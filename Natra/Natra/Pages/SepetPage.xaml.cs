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

        List<Siparis_d> siparises;
        Siparis_h siparis_h;

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

            Title = Globals.SepetPageTitle;

            siparis_h = new Siparis_h();

            getSiparises();

            sepetPageOnayButton.Clicked += async (s, e) =>
            {
                //var rs =;

                siparis_h.SiparisNotlari = aciklamaEditor.Text;

                IsBusy = true;
                bool result = await new RestService().sepetOnay(siparis_h);
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

        private void setLabelsAndCalculateSum()
        {
            double genTop = 0;
            double kdvTop = 0;
            double brutTop=0;
            foreach (var siparis in siparises)
            {
                var kdv = siparis.BrutTutar * siparis.stok.KDV / 100;
                genTop += siparis.BrutTutar + (kdv);
                kdvTop += kdv;
                brutTop += siparis.BrutTutar;
            }

            siparis_h.GenelToplam = genTop;
            siparis_h.BrutTutar = brutTop;
            siparis_h.KDVToplam = kdvTop;


            sepetPageBrutToplamLabel.Text = string.Format("{0} : {1} TL",Globals.BrutToplam,brutTop);
            sepetPageGenelToplamLabel.Text = string.Format("{0} : {1} TL",Globals.GenelToplam,genTop);
            sepetPageKDVToplamLabel.Text = string.Format("{0} : {1} TL",Globals.KDVToplami,kdvTop);
        }

        public async void OnSiparisDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var sip = mi.CommandParameter as Siparis_d;
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

            siparis_h.siparis_dList = siparises;

            //sepetListView.ItemsSource = DBHelper.getAllSiparises();
            sepetListView.ItemsSource = siparises;

            setLabelsAndCalculateSum();
        }

        
    }
}
