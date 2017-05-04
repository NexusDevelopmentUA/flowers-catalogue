using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using System;
using Android.Views;
using Android.Widget;
using Square.Picasso;
using Android.Content;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Android.Support.V7.App;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace Flowers_shop
{
    [Activity(Label = "Flowers_shop", Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light")]

    public class List_News_activity : ActionBarActivity
    {
        const string url = "http://www.segodnya.ua";
        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        RecyclerViewAdapter mAdapter;
        List<News> news;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Web_API_Repository download = new Web_API_Repository();
            news = download.DownloadNews();

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new RecyclerViewAdapter(news, this);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecyclerView.SetAdapter(mAdapter);

        }

        private void MAdapter_ItemClick(object sender, int e)
        {
            Toast.MakeText(this, "News #" + e, ToastLength.Short).Show();
            var separate_news = new Intent(this, typeof(Separate_News_Activity));
            List<string> transfer = new List<string>();
            News transfer_news = new News();
            transfer_news = news[e];
            separate_news.PutExtra("News", JsonConvert.SerializeObject(news[e]));
            StartActivity(separate_news);
        }

        //♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣♣

        class RecyclerViewAdapter : RecyclerView.Adapter
        {
            public List<News> news;
            public Context context;

            public override int ItemCount
            {
                get
                {
                    return news.Count;
                }
            }

            private void OnClick(int position)
            {
                ItemClick?.Invoke(this, position); //if (ItemClick != null)  ItemClick(this, position);
            }

            public RecyclerViewAdapter(List<News> _news, Context _context)
            {
                news = _news;
                context = _context;
            }

            public event EventHandler<int> ItemClick;
            //refact this into async
            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                RecyclerViewHolder viewholder = holder as RecyclerViewHolder;
                //filling recycler view with data
                Picasso.With(context).Load("http://www.segodnya.ua" + news[position].newsImgURL).Into(viewholder.image);
                viewholder.title.Text = news[position].newsTitle;
                viewholder.description.Text = news[position].newsSubTitle;
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.cardview, parent, false);
                RecyclerViewHolder viewholder = new RecyclerViewHolder(itemView, OnClick);
                return viewholder;
            }
        }

        public class RecyclerViewHolder : RecyclerView.ViewHolder
        {
            public ImageView image { get; private set; }
            public TextView title { get; private set; }
            public TextView description { get; private set; }
            public RecyclerViewHolder(View itemview, Action<int> listener)
                : base(itemview)
            {
                image = itemview.FindViewById<ImageView>(Resource.Id.cardview_image);
                title = itemview.FindViewById<TextView>(Resource.Id.cardview_title);
                description = itemview.FindViewById<TextView>(Resource.Id.cardview_description);

                itemview.Click += (sender, e) => listener(Position);
            }
        }
    }
}