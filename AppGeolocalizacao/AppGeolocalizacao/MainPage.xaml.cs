using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace AppGeolocalizacao
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void btnLocalizacao_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest()
                { DesiredAccuracy = GeolocationAccuracy.Best });
                if (location != null)
                {
                    lblLatitude.Text = "Latitude: " + location.Latitude.ToString();
                    lblLongitude.Text = "Longitude: " + location.Longitude.ToString();
                }
            }
            catch (FeatureNotEnabledException fnsEx)
            {
                await DisplayAlert("Falhou", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Falhou", pEx.Message, "OK");
            }
            catch(Exception er)
            {
                await DisplayAlert("Falhou", er.Message, "OK");
            }
        }

        public async Task MostrarMapa()
        {
            var location = await Geolocation.GetLocationAsync(new GeolocationRequest()
            {DesiredAccuracy = GeolocationAccuracy.Best});
            var locationinfo = new Location(location.Latitude,location.Longitude);
            var options = new MapLaunchOptions { Name = "Meu local"};
            await Map.OpenAsync(locationinfo, options);
        }

        private async void btnVerMapa_Clicked(object sender, EventArgs e)
        {
            await MostrarMapa();
        }
    }
}
