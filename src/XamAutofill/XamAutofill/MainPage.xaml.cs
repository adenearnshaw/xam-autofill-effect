using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using XamAutofill.PlatformConfiguration.AndroidSpecific;

namespace XamAutofill
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            PhoneEntry.On<Android>().SetAutofillHint(AutofillHintType.Phone);
        }
    }
}
