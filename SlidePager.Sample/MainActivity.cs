using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Sino.Droid.SlidePager;
using System.Collections.Generic;

namespace SlidePager.Sample
{
    [Activity(Label = "SlidePager.Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SlideView sv = new SlideView(this);
            sv.SetImageResources(Resource.Drawable.welfirst, Resource.Drawable.welsecond, Resource.Drawable.welthird);
            sv.AttachTo();
        }
    }
}

