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
using Square.Picasso;
using Android.Support.V7.Widget;
using Android.Support.V7.CardView;
using Android.Support.V7.App;

namespace Flowers_shop
{
    [Activity(Label = "News_Activity")]
    public class Separate_News_Activity : AppCompatActivity
    {
        ImageView imgview;
        TextView title;
        TextView text;
        News news;
        CardView cardview;
        FrameLayout flayout;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.cardview);
            imgview = FindViewById<ImageView>(Resource.Id.cardview_image);
            title = FindViewById<TextView>(Resource.Id.cardview_title);
            text = FindViewById<TextView>(Resource.Id.cardview_description);
            cardview = FindViewById<CardView>(Resource.Id.card);
            flayout = FindViewById<FrameLayout>(Resource.Id.framelayout);

            news = JsonConvert.DeserializeObject<News>(Intent.GetStringExtra("News"));
            Picasso.With(this).Load("http://www.segodnya.ua"+news.newsImgURL).Into(imgview);
            title.Text = news.newsTitle;
            title.TextSize = 25;
            text.Text = news.newsContent;
            text.TextSize = 16;
            flayout.LayoutParameters = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            cardview.LayoutParameters = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,ViewGroup.LayoutParams.MatchParent);  
        }
    }
}