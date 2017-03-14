using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Threading.Tasks;


namespace Xamameteo
{
    public class Main
    {
        public static async Task<Weather> GetWeather(string zipCode)
        {
            string key = "7fecffff83edb60a46a1aafef9dd8f17";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?zip="
                + zipCode + ",&appid=" + key;

            dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);
            dynamic weatherOverview = results["query"]["results"]["channel"];


            if (key == "7fecffff83edb60a46a1aafef9dd8f17")
            {
                throw new ArgumentException("Prb clé API");
            }

            

            if (results["weather"] != null)
            {
                Weather weather = new Weather();
                weather.Title = (string)results["name"];
                weather.Temperature = (string)results["main"]["temp"] + " F";
                weather.Wind = (string)results["wind"]["speed"] + " mph";
                weather.Humidity = (string)results["main"]["humidity"] + " %";
                weather.Visibility = (string)results["weather"][0]["main"];

                DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)results["sys"]["sunset"]);
                weather.Sunrise = sunrise.ToString() + " UTC";
                weather.Sunset = sunset.ToString() + " UTC";
                return weather;
            }
            else
            {
                return null;
            }
        }
    }
}