using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            tBoxCityName.Text = "Stuttgart";
        }

        private async void btShowWeatherInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var information = await ShowWeatherInformation();
                tBoxCountry.Text = information.Country;
                tBoxWindSpeed.Text = information.WindSpeed.ToString();
                tBoxPressure.Text = tBoxCityName.Text;
                tBoxTemperature.Text = tBoxCityName.Text;
                tBoxHumidity.Text = tBoxCityName.Text;
                tBoxCod.Text = information.Cod.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void btShowOpenWeatherMap_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tBoxCityName.Text != string.Empty)
                {
                    RequestHeaderAccept(client);

                    var stringTask = client.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?q={tBoxCityName.Text}&APPID=c49d41380cdd216db704d02be9332fbd");
                    string msg = await stringTask;
                    tblockWeatherInfo.Text = msg;
                }
                else
                {
                    MessageBox.Show("Please insert a city name");
                }
            }
            catch(HttpRequestException httpEx)
            {
                MessageBox.Show(httpEx.ToString());
                tblockWeatherInfo.Text = "HttpRequestException, no connection to the internet";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                tblockWeatherInfo.Text = $"Exception: {ex}";
            }
        }

        private async Task<WeatherInformation> ShowWeatherInformation()
        {
            WeatherInformation result = new WeatherInformation();

            try
            {
                if (tBoxCityName.Text != string.Empty)
                {
                    RequestHeaderAccept(client);

                    var streamSource = await client.GetStreamAsync($"https://api.openweathermap.org/data/2.5/weather?q={tBoxCityName.Text}&APPID=c49d41380cdd216db704d02be9332fbd");
                    result = await System.Text.Json.JsonSerializer.DeserializeAsync<WeatherInformation>(streamSource);
                }
                else
                {
                    MessageBox.Show("Please insert a city name");
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show(httpEx.ToString());
                tblockWeatherInfo.Text = "HttpRequestException, no connection to the internet";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                tblockWeatherInfo.Text = $"Exception: {ex}";
            }

            return result;
        }

        private void RequestHeaderAccept(HttpClient cl)
        {
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
