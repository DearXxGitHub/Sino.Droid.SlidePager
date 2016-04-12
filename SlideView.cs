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
using Android.Util;
using Android.Support.V4.View;

namespace Sino.Droid.SlidePager
{
    public class SlideView
    {
        private ViewPager _viewPager;
        private PageIndicator _pageIndicator;
        private SlideViewAdapter _adapter;
        private Activity _activity;

        public SlideView(Activity activity)
        {
            _adapter = new SlideViewAdapter(activity);
            _activity = activity;
        }

        public void SetImageResources(params int[] ids)
        {
            _adapter.AddAll(ids.ToList());
            _adapter.NotifyDataSetChanged();
        }

        public void AttachTo()
        {
            _activity.SetContentView(Resource.Layout.sino_droid_slidepager_slideview);
            _pageIndicator = _activity.FindViewById<PageIndicator>(Resource.Id.indicator);
            _viewPager = _activity.FindViewById<ViewPager>(Resource.Id.pager);
            _viewPager.Adapter = _adapter;
            _pageIndicator.SetViewPager(_viewPager);
        }

        public void SetIndicatorState(IndicatorType type)
        {
            _pageIndicator.SetIndicatorType(type);
        }
    }
}