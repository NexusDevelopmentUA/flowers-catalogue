using Android.App;
using Android.OS;
using Android.Media;
using System.Net;
using AngleSharp.Dom;
using System.Collections.Generic;
using AngleSharp.Parser.Html;
using Android.Graphics;
using Java.Nio;
using AngleSharp.Dom.Html;
using Android.Widget;

namespace Flowers_shop
{
    [Activity(Label = "Flowers_shop", Icon = "@drawable/icon")]

    public class News_activity : Activity    
    {
        const string url = "http://www.segodnya.ua/tags/цветы.html";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);
            Get_News();
        }

        //need refact all of this into async
        public void Get_News()
        {
            List<string> divs = new List<string>();
            List<string> images_url = new List<string>();
            List<Image> images = new List<Image>();
            string html;
            WebClient client = new WebClient();
            using (client)
            {   
                html = client.DownloadString(url);
            }

            var parser = new HtmlParser();
            var document = parser.Parse(html);

            foreach (IElement element in document.QuerySelectorAll("div"))
            {
                divs.Add(element.GetElementsByClassName("news-block-wrapper").ToString());
            }
            TextView txt = FindViewById<TextView>(Resource.Id.txtview);
            txt.Text = divs[0];            
        }
        //♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣
        public void Get_images(IHtmlDocument document)
        {
            foreach (IElement element in document.QuerySelectorAll("img"))
            {
                //images_url.Add(element.GetAttribute("src"));
                var request = WebRequest.Create("http://www.segodnya.ua/" + element.GetAttribute("src"));
                var response = request.GetResponse();
                Bitmap bitmap = null;
                using (var response_stream = response.GetResponseStream())
                {
                    bitmap = BitmapFactory.DecodeStream(response_stream);
                }
                var byteArray = ByteBuffer.Allocate(bitmap.ByteCount);
                bitmap.CopyPixelsToBuffer(byteArray);
                byte[] bytes = byteArray.ToArray<byte>();
            }
        }
    }
}