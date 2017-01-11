using Natra.Helpers;
using Natra.HttpConnector;
using Natra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Natra.Pages
{
    public partial class MainPage : ContentPage
    {
        //private ObservableCollection<Product> _searchedProducts;
        private List<Stok> _stoksFromRemoteDb;
        private List<Stok> mockupList;

        private bool listViewMustRefresh = true;

        protected override void OnAppearing() //page on the screen
        {
            base.OnAppearing();
            if (listViewMustRefresh)
            {
                productListView.BeginRefresh();
                listViewMustRefresh = false;
            }
            // sepetim.Text = string.Format("Sepetim({0})", DBHelper.getSiparisCount());
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                activityIndicator.IsVisible = value;
                activityIndicator.IsRunning = value;
                productListView.IsVisible = !value;
                _isBusy = value;
                //OnPropertyChanged();
            }
        }

        public MainPage()
        {
            BindingContext = this;
            InitializeComponent();

            mockupList = new List<Stok>(); // demo list
            addMockupData(); // demo list
            

            #region Searchbar

            searchBar.SearchCommand = new Command(
                () =>
                {
                    findProducts();
                });



            searchBar.TextChanged += (s, o) =>
            {
                //IsBusy = true;
                //Device.StartTimer(TimeSpan.FromSeconds(3), findProducts);
                findProducts();
            };
            #endregion


            //productListView.ItemSelected += (s, e) => {
            //    Product p = (Product)(((ListView)s).SelectedItem);
            //    openAddSiparisPage(p);
            //    //openAddToBasketDialog(p);
            //    ((ListView)s).SelectedItem = null;
            //};

            productListView.ItemTapped += (s, e) =>
            {
                Stok p = (Stok)(((ListView)s).SelectedItem);
                openAddSiparisPage(p);
                //openAddToBasketDialog(p);
                ((ListView)s).SelectedItem = null;
            };

            productListView.RefreshCommand = new Command(getAllProducts);

            productListView.IsPullToRefreshEnabled = true;

            //productListView.IsRefreshing = true;





            //productListView.ItemsSource = mockupList.FindAll(o => o.Name.StartsWith("e"));

            //getAllProducts();

        }

        private void openAddSiparisPage(Stok p)
        {
            App.AppInstance.MainPage.Navigation.PushAsync(new AddSiparisPage(p));
        }



        private async void openAddToBasketDialog(Product product)
        {
            //if (product == null) return;
            //var page = new AddToBucketPopupPage();
            //page.HeightRequest = 200;
            //page.WidthRequest = 200;
            //await Navigation.PushPopupAsync(page);
            //page.HeightRequest = 200;
            //page.WidthRequest = 200;
            //await Navigation.PushAsync(page);
        }

        private bool findProducts()
        {

            //_searchedProducts = mockupList.FindAll(o => o.Name.StartsWith(searchBar.Text));
            if (searchBar.Text.Length > 0)
            {
                productListView.ItemsSource = _stoksFromRemoteDb.FindAll(o => o.StokKodu.StartsWith(searchBar.Text));
                //IsBusy = false;
                return false;
            }
            else
            {
                productListView.ItemsSource = _stoksFromRemoteDb;
                //IsBusy = false;
            }
            return false;
        }

        private async void getAllProducts()
        {
            //IsBusy = true;


            _stoksFromRemoteDb = await new RestService().getAllStoks();

            productListView.ItemsSource = _stoksFromRemoteDb;

            //IsBusy = false;

            productListView.IsRefreshing = false;

            //await Task.Run(() =>
            //{
            //    var x = new RestService().getAllStoks();
            //    //productListView.ItemsSource = 
            //    var t=(List)
            //    IsBusy = false;
            //});


        }

        private void addMockupData()
        {
            var elma= new Stok() { StokKodu = "Elma", SatisFiyati1 = 10, bakiye = 10, OlcuBirimi1 = "kg", OlcuBirimi2 = "kasa", StokAciklamasi = "ack1", Carpan = 10, KDV = 10 };
            var cilek = new Stok() { StokKodu = "Çilek", SatisFiyati1 = 20, bakiye = 20, OlcuBirimi1 = "kg", OlcuBirimi2 = "kasa", StokAciklamasi = "ack2", Carpan = 20, KDV = 20 };
            var armut = new Stok() { StokKodu = "Armut", SatisFiyati1 = 30, bakiye = 30, OlcuBirimi1 = "kg", OlcuBirimi2 = "kasa", StokAciklamasi = "ack3", Carpan = 30, KDV = 30 };
            mockupList.Add(elma);
            mockupList.Add(cilek);
            mockupList.Add(armut);

            var sip1 = new Siparis() { stok = elma, BrutTutar = 100, Miktar = 10, KDVToplam = 10, GenelToplam = 110, OlcuBirimi = "kg", SiparisNotlari = "not1" };
            var sip2 = new Siparis() { stok = cilek, BrutTutar = 400, Miktar = 20, KDVToplam = 80, GenelToplam = 480, OlcuBirimi = "kg", SiparisNotlari = "not2" };

            DBHelper.addSiparisToSepet(sip1);
            DBHelper.addSiparisToSepet(sip2);

        }

    }
}
