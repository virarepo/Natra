using Natra.Models;
using Natra.Pages;
using Natra.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Natra
{
    public partial class MainPage : ContentPage
    {
        //private ObservableCollection<Product> _searchedProducts;
        private List<Product> _searchedProducts;
        private List<Product> mockupList;

        private bool _isBusy=false;
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
            mockupList = new List<Product>();
            addMockupData();

            #region Searchbar
            searchBar.SearchCommand = new Command(
                () => {
                    
                    findProducts();
                });

            

            searchBar.TextChanged += (s,o) => {
                IsBusy = true;
                Device.StartTimer(TimeSpan.FromSeconds(3), findProducts);
                //findProducts();
            };
            #endregion


            //productListView.ItemSelected += (s, e) => {
            //    Product p = (Product)(((ListView)s).SelectedItem);
            //    openAddSiparisPage(p);
            //    //openAddToBasketDialog(p);
            //    ((ListView)s).SelectedItem = null;
            //};

            productListView.ItemTapped += (s, e) => {
                Product p = (Product)(((ListView)s).SelectedItem);
                openAddSiparisPage(p);
                //openAddToBasketDialog(p);
                ((ListView)s).SelectedItem = null;
            };



            productListView.ItemsSource = mockupList.FindAll(o => o.Name.StartsWith("e"));

            var hc = new HttpConnector.RestService();
            hc.getAllStoks();

        }

        private void openAddSiparisPage(Product p)
        {
            App.AppInstance.MainPage.Navigation.PushAsync(new AddSiparisPage());
        }

        private async void openAddToBasketDialog(Product product)
        {
            if (product == null) return;
            var page = new AddToBucketPopupPage();
            page.HeightRequest = 200;
            page.WidthRequest = 200;
            await Navigation.PushPopupAsync(page);
            page.HeightRequest = 200;
            page.WidthRequest = 200;
            //await Navigation.PushAsync(page);
        }

        private  bool findProducts()
        {

            //_searchedProducts = mockupList.FindAll(o => o.Name.StartsWith(searchBar.Text));
            if (searchBar.Text.Length > 0) {
                productListView.ItemsSource = mockupList.FindAll(o => o.Name.StartsWith(searchBar.Text));
                IsBusy = false;
                return false;
            }
            else { productListView.ItemsSource = null; IsBusy = false; }
            return false;
        }

        private void addMockupData()
        {
            mockupList.Add(new Product() { Name = "elma", StockAmount = 4, AmountType="kg", Seller = new Seller() { Name = "Hüseyin" } });
            mockupList.Add(new Product() { Name = "armut", StockAmount = 4, AmountType = "kasa", Seller = new Seller() { Name = "Hüseyin" } });
            mockupList.Add(new Product() { Name = "peynir", StockAmount = 4, AmountType = "kg", Seller = new Seller() { Name = "Ahmet" } });
            mockupList.Add(new Product() { Name = "peynir", StockAmount = 4, AmountType = "kg", Seller = new Seller() { Name = "Mehmet" } });
        }

    }
}
