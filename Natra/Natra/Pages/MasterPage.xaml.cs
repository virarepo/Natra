using Natra.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Natra.Pages
{
    public partial class MasterPage : ContentPage
    {
        public MasterPage()
        {
            InitializeComponent();

            deleteAllSip.Clicked +=  (s,o) => { DBHelper.deleteAllSiparises(); };
        }
    }
}
