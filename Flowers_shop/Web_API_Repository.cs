using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Flowers_shop
{
    class Web_API_Repository
    {
        const string url = "http://5.58.21.200:8080/";
        const string json = "https://api.myjson.com/bins/h78b3";
        public List<News> DownloadNews()
        {
            WebClient conn = new WebClient();
            var data = conn.DownloadString(json);
            JArray jdata = JArray.Parse(data);
            List<News> news = JsonConvert.DeserializeObject<List<News>>(data);
            return news;
        }
    }
}