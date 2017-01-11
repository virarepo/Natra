using Natra.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Natra.Pages
{
    public partial class AddSiparisPage : ContentPage
    {


        public AddSiparisPage()
        {
            InitializeComponent();

            Title = Globals.addProductPageTitle;

            // OlcuBirimi1.On = true;




            OlcuBirimiPicker.Items.Add("kg");
            OlcuBirimiPicker.Items.Add("koli");

            OlcuBirimiPicker.SelectedIndex = 1;


            OlcuBirimiPicker.Title = Globals.selectOlcuBirimi;
            


            //OlcuBirimi1.PropertyChanged += (s, e) => {
            //    OlcuBirimi2.On = !((SwitchCell)s).On;
            //};

            //OlcuBirimi1.PropertyChanged += (s, e) => {
            //    OlcuBirimi1.On = !((SwitchCell)s).On;
            //};



        }
    }
}
