using Natra.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Natra.UICommunications
{
    public class PopupManager
    {
        public async Task<bool> showInfoPopup(Page page,string message)
        {
            return await page.DisplayAlert(Globals.bilgi, message, null, "Tamam");
        }
    }
}
