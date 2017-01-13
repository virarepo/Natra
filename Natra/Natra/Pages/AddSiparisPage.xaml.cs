using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Natra.Helpers;

using Xamarin.Forms;
using Natra.Models;

namespace Natra.Pages
{
    public partial class AddSiparisPage : ContentPage
    {
        public Stok stok { get; set; }

        Siparis_d newSiparis;

        //public Double Toplam { get; set; }

        public AddSiparisPage(Stok stok_)
        {

            this.stok = stok_;

            newSiparis = new Siparis_d();

            InitializeComponent();

            BindingContext = this;

            Title = stok.StokKodu;

            onayButton.IsEnabled = false;

            OlcuBirimiPicker.Items.Add(stok.OlcuBirimi1);  //ölçü birimleri picker a ekleniyor
            OlcuBirimiPicker.Items.Add(stok.OlcuBirimi2);  //ölçü birimleri picker a ekleniyor

            OlcuBirimiPicker.SelectedIndex = 0;

            OlcuBirimiPicker.Title = Globals.selectOlcuBirimi;  //popup title

            #region ClickHandlers

            OlcuBirimiPicker.PropertyChanged += (s, e) =>
            {
                CalculateToplam();
            };

            miktarEntry.TextChanged += (s, e) =>
            {
                CalculateToplam();
            };

            onayButton.Clicked += (s, e) =>
            {
                newSiparis.OlcuBirimi = OlcuBirimiPicker.Items[OlcuBirimiPicker.SelectedIndex];
                //newSiparis.SiparisNotlari = aciklamaEditor.Text;
                newSiparis.stok = stok;
                newSiparis.HesapKodu = App.AppInstance.currentUser.username;
               
                //newSiparis.GenelToplam = Toplam;
                if (!DBHelper.addSiparisToSepet(newSiparis)) Logger.errLog("addSiparisToSepet addSiparisPage", null);
                App.AppInstance.MainPage.Navigation.PopAsync();  // go back to previous screen
            };

            #endregion
        }

        void CalculateToplam()
        {
            if (miktarEntry.Text == null) return;
            int carpan = 1;
            if (OlcuBirimiPicker.SelectedIndex == 1) carpan = stok.Carpan;  // kasa - kg
            int miktar = 0;

            try
            {
                miktar = Int32.Parse(miktarEntry.Text);
                if (miktar * carpan > stok.bakiye)   // stok kontrol
                {
                    if (miktarEntry.Text != null) urunFiyatLabel.Text = Globals.StokYok;
                    onayButton.IsEnabled = false;
                    kdvLabel.Text = "";
                    toplamLabel.Text = "";
                }
                else
                {
                    newSiparis.Miktar = miktar;

                    newSiparis.BirimFiyat = carpan * stok.SatisFiyati1;

                    newSiparis.BrutTutar = (miktar * newSiparis.BirimFiyat);  // urunler toplami

                    urunFiyatLabel.Text = string.Format("{0} : {1} TL", Globals.UrunToplami, newSiparis.BrutTutar.ToString());

                    var KDVToplam = newSiparis.BrutTutar * stok.KDV / 100;

                    var GenelToplam = newSiparis.BrutTutar + KDVToplam;   // kdv dahil

                    toplamLabel.Text = string.Format("Toplam Tutar: {0} TL", GenelToplam);

                    kdvLabel.Text = string.Format("KDV Toplamı : {0} TL", KDVToplam); 

                    onayButton.IsEnabled = true;
                }
            }
            catch (Exception e)  // virgül-nokta girilmişse
            {
                onayButton.IsEnabled = false;
                if (miktarEntry.Text != null) urunFiyatLabel.Text = Globals.MiktarIsWrongMsg;
                kdvLabel.Text = "";
                toplamLabel.Text = "";
            }
        }

    }
}
