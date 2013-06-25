using System.Windows;
using CineworldPortable;
using Microsoft.Phone.Controls;

namespace CineworldPortablePlayground
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const string ApiKey = "qFcrX7cf";
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var client = new CineworldClient(ApiKey);

            var cinemas = await client.GetCinemasAsync();
        }
    }
}