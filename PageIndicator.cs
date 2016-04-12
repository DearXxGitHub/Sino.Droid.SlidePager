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
using Android.Support.V4.View;
using Android.Util;
using Android.Content.Res;
using Android.Graphics;
using Java.Lang.Reflect;

namespace Sino.Droid.SlidePager
{
    public class PageIndicator : LinearLayout, ViewPager.IOnPageChangeListener
    {
        public static int DEFAULT_INDICATOR_SPACING = 5;

        private int mActivePosition = 1;
        private int mIndicatorSpacing;
        private bool mIndicatorTypeChanged = false;

        private IndicatorType mIndicatorType = IndicatorType.Circle;
        private ViewPager mViewPager;

        public PageIndicator(Context context)
            : this(context, null) { }

        public PageIndicator(Context context, IAttributeSet attrs)
            : this(context, attrs, 0) { }

        public PageIndicator(Context context, IAttributeSet attrs, int def)
            : base(context, attrs, def)
        {
            TypedArray a = context.Theme.ObtainStyledAttributes(attrs, Resource.Styleable.PageIndicator, 0, 0);
            try
            {
                mIndicatorSpacing = a.GetDimensionPixelSize(
                    Resource.Styleable.PageIndicator_indicator_spacing,
                    Dp2Px(context, DEFAULT_INDICATOR_SPACING));
                int indicatorTypeValue = a.GetInt(
                    Resource.Styleable.PageIndicator_indicator_type,
                    (int)mIndicatorType);
                mIndicatorType = (IndicatorType)indicatorTypeValue;
            }
            finally
            {
                a.Recycle();
            }

            Init();
        }

        protected PageIndicator(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer) { }

        private void Init()
        {
            Orientation = Android.Widget.Orientation.Horizontal;
            if (!(LayoutParameters is FrameLayout.LayoutParams))
            {
                FrameLayout.LayoutParams param = new FrameLayout.LayoutParams(
                    ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent);
                param.Gravity = GravityFlags.Bottom | GravityFlags.Start;
                LayoutParameters = param;
            }
        }

        public void SetViewPager(ViewPager pager)
        {
            mViewPager = pager;
            pager.SetOnPageChangeListener(this);
            SetIndicatorType(mIndicatorType);
        }

        public void SetIndicatorType(IndicatorType indicatorType)
        {
            mIndicatorType = indicatorType;
            mIndicatorTypeChanged = true;
            if (mViewPager != null)
            {
                AddIndicator(mViewPager.Adapter.Count);
            }
        }

        private void RemoveIndicator()
        {
            RemoveAllViews();
        }

        private void AddIndicator(int count)
        {
            RemoveIndicator();
            if (count <= 0) return;
            if (mIndicatorType == IndicatorType.Circle)
            {
                for (int i = 0; i < count; i++)
                {
                    ImageView img = new ImageView(Context);
                    LayoutParams param = new LayoutParams(
                        LayoutParams.WrapContent, LayoutParams.WrapContent);
                    param.LeftMargin = mIndicatorSpacing;
                    param.RightMargin = mIndicatorSpacing;
                    img.SetImageResource(Resource.Drawable.circle_indicator_stroke);
                    AddView(img, param);
                }
            }
            else if (mIndicatorType == IndicatorType.Fraction)
            {
                TextView textView = new TextView(Context);
                textView.SetTextColor(Color.White);
                int padding = Dp2Px(Context, 10);
                textView.SetPadding(padding, padding >> 1, padding, padding >> 1);
                textView.SetBackgroundResource(Resource.Drawable.fraction_indicator_bg);
                textView.Tag = count;
                LayoutParams param = new LayoutParams(
                    LayoutParams.WrapContent, LayoutParams.WrapContent);
                AddView(textView, param);
            }
            UpdateIndicator(mViewPager.CurrentItem);
        }

        private void UpdateIndicator(int p)
        {
            if (mIndicatorTypeChanged || mActivePosition != p)
            {
                mIndicatorTypeChanged = false;
                if (mIndicatorType == IndicatorType.Circle)
                {
                    if (mActivePosition == -1)
                    {
                        ((ImageView)GetChildAt(p)).SetImageResource(Resource.Drawable.circle_indicator_solid);
                        mActivePosition = p;
                        return;
                    }
                    ((ImageView)GetChildAt(mActivePosition))
                        .SetImageResource(Resource.Drawable.circle_indicator_stroke);
                    ((ImageView)GetChildAt(p))
                        .SetImageResource(Resource.Drawable.circle_indicator_solid);
                }
                else if (mIndicatorType == IndicatorType.Fraction)
                {
                    TextView textView = (TextView)GetChildAt(0);
                    textView.Text = String.Format("{0}/{1}", p + 1, (int)textView.Tag);
                }
                mActivePosition = p;
            }
        }

        private ViewPager.IOnPageChangeListener GetOnPageChangeListener(ViewPager pager)
        {
            try
            {
                Field f = pager.Class.GetDeclaredField("mOnPageChangeListener");
                f.Accessible = true;
                return (ViewPager.IOnPageChangeListener)f.Get(pager);
            }
            catch (NoSuchPropertyException e)
            {
                e.PrintStackTrace();
            }
            catch (Java.Lang.IllegalAccessException e)
            {
                e.PrintStackTrace();
            }
            return null;
        }

        private int Dp2Px(Context context, int dpValue)
        {
            return (int)context.Resources.DisplayMetrics.Density * dpValue;
        }

        public void OnPageScrollStateChanged(int state)
        {

        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            
        }

        public void OnPageSelected(int position)
        {
            UpdateIndicator(position);
        }
    }
}