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
using Android.Support.V4.App;
using Android.Support.V4.View;

namespace Sino.Droid.SlidePager
{
    public class SlideViewAdapter : PagerAdapter
    {
        private Context _context;

        public SlideViewAdapter(Context context)
        {
            _context = context;
        }

        private List<View> picList = new List<View>();

        public void AddAll(List<int> ids)
        {
            foreach (int id in ids)
            {
                picList.Add(CreateView(id));
            }
        }

        protected View CreateView(int id)
        {
            ImageView iv = new ImageView(_context);
            iv.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent);
            iv.SetScaleType(ImageView.ScaleType.FitXy);
            iv.SetImageResource(id);
            return iv;
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            View v = picList[position];
            container.AddView(v);
            return v;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object objectValue)
        {
            container.RemoveView((View)objectValue);
        }

        public override int Count
        {
            get
            {
                return picList.Count;
            }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object objectValue)
        {
            return view == objectValue;
        }
    }
}