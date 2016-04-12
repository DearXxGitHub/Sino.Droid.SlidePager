using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace Sino.Droid.SlidePager
{
    public class SlideViewFragment : Fragment
    {
        private static string PIC_URL = "slidepagefragment_picurl";

        public static SlideViewFragment NewInstance(int id)
        {
            Bundle arguments = new Bundle();
            arguments.PutInt(PIC_URL, id);

            SlideViewFragment fragment = new SlideViewFragment();
            fragment.Arguments = arguments;

            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ImageView iv = new ImageView(inflater.Context);
            iv.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            iv.SetScaleType(ImageView.ScaleType.FitCenter);
            if (Arguments != null)
            {
                int id = Arguments.GetInt(PIC_URL);
                iv.SetImageResource(id);
            }

            container.AddView(iv);

            return container;
        }
    }
}