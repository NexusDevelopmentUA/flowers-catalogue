using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using UniversalImageLoader.Core;
using Square.Picasso;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using System.Collections.Generic;
using SupportFragment = Android.Support.V4.App.Fragment;
namespace Flowers_shop
{
    [Activity(Label = "Shop_activity")]
    public class Shop_activity : Activity
    {
        GridView gridView;
        List<ImageView> images = new List<ImageView>();
        List<string> titles = new List<string>();
        string[] imgs = { "clematis_arabella.jpg" ,
                          "delphinium_king_arthur.jpg",
                          "hibiscus_osiau_blue.jpg",
                          "peaonia_garden_treasure.jpg",
                          "potentila_marian_robin.jpg",
        "clematis_arabella.jpg" ,
                          "delphinium_king_arthur.jpg",
                          "hibiscus_osiau_blue.jpg",
                          "peaonia_garden_treasure.jpg",
                          "potentila_marian_robin.jpg",
        "clematis_arabella.jpg" ,
                          "delphinium_king_arthur.jpg",
                          "hibiscus_osiau_blue.jpg",
                          "peaonia_garden_treasure.jpg",
                          "potentila_marian_robin.jpg",};

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Shop);
            var config = ImageLoaderConfiguration.CreateDefault(ApplicationContext);
            foreach (var str in imgs)
            {
                ImageView temp_image = new ImageView(this);
                ImageLoader loader = ImageLoader.Instance;
                titles.Add(str);
            }

            GridViewAdapter adapter = new GridViewAdapter(this, images, titles);
            gridView = FindViewById<GridView>(Resource.Id.grid_view_image_text);
            gridView.Adapter = adapter;

        }
    }
    public class GridViewAdapter : BaseAdapter
    {

        private Context context;
        public List<ImageView> images;
        public List<string> titles;

        public GridViewAdapter(Context context, List<ImageView> _images, List<string> _titles)
        {
            this.context = context;
            images = _images;
            titles = _titles;
        }

        public override int Count
        {
            get
            {
                return titles.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view;
            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            if (convertView == null)
            {
                view = new View(context);
                view = inflater.Inflate(Resource.Layout.gridview, null);
                TextView txtView = view.FindViewById<TextView>(Resource.Id.textView);
                ImageView imgView = view.FindViewById<ImageView>(Resource.Id.imageView);
                txtView.Text = titles[position].Replace(".jpg","");
                Picasso.With(context).Load("http://i.imgur.com/DvpvklR.png").Into(imgView);
            }
            else
            {
                view = (View)convertView;
            }
            view.Click += View_Click;
            return view;
        }

        private void View_Click(object sender, System.EventArgs e)
        {
            var transaction = FragmentManager.BeginTransaction();
        }

        private void ShowFragment(SupportFragment fragment)
        {
            if (fragment.IsVisible)
            {
                return;
            }

            var trans = FragmentManager.BeginTransaction();

            //trans.SetCustomAnimations(Resource.Animation.slide_in, Resource.Animation.slide_out, Resource.Animation.slide_in, Resource.Animation.slide_out);

            fragment.View.BringToFront();
            mCurrentFragment.View.BringToFront();

            trans.Hide(mCurrentFragment);
            trans.Show(fragment);

            trans.AddToBackStack(null);
            mStackFragments.Push(mCurrentFragment);
            trans.Commit();

            mCurrentFragment = fragment;
        }
    }
}