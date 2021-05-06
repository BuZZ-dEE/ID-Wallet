﻿using Android.Graphics.Drawables;
using Android.Support.V4.Content.Res;
using Android.Widget;
using IDWallet.Packages.FormsPinView;
using FormsPinView.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AColor = Android.Graphics.Color;
using AView = Android.Views.View;

[assembly: ExportRenderer(typeof(PinItemView), typeof(PinItemViewRenderer))]
namespace FormsPinView.Droid
{
    public class PinItemViewRenderer : ViewRenderer<PinItemView, AView>
    {
        private RippleButton _button;

        public PinItemViewRenderer(Android.Content.Context context) : base(context)
        {
        }

        public static void Init()
        {
            _ = typeof(PinItemViewRenderer);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _button = null;
            }
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<PinItemView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {

            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    int sideSize = (int)Context.ToPixels(Element.Size);

                    _button = new RippleButton(Context, GetRippleColor());
                    _button.SetWidth(sideSize);
                    _button.SetHeight(sideSize);
                    _button.SetTextColor(GetColor());
                    _button.Background = GetBackgroundDrawable();
                    _button.Text = Element.Text;
                    _button.SetTextSize(Android.Util.ComplexUnitType.Pt, 10);
                    _button.Gravity = Android.Views.GravityFlags.Center;
                    _button.OnClick += (sender, args) =>
                    {
                        Element?.Command?.Execute(Element?.CommandParameter);
                    };

                    FrameLayout frame = new FrameLayout(Context);
                    FrameLayout.LayoutParams @params = new FrameLayout.LayoutParams(sideSize, sideSize);
                    @params.Gravity = Android.Views.GravityFlags.Center;
                    frame.AddView(_button, @params);

                    SetNativeControl(frame);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_button != null)
            {
                if (e.PropertyName == PinItemView.ColorProperty.PropertyName)
                {
                    _button.SetTextColor(GetColor());
                    return;
                }
                else if (e.PropertyName == PinItemView.BorderColorProperty.PropertyName)
                {
                    _button.Background = GetBackgroundDrawable();
                    return;
                }
                else if (e.PropertyName == PinItemView.RippleColorProperty.PropertyName)
                {
                    _button.SetRippleColor(GetRippleColor());
                    return;
                }
            }
            base.OnElementPropertyChanged(sender, e);
        }

        private Drawable GetBackgroundDrawable()
        {
            GradientDrawable drawable = (GradientDrawable)ResourcesCompat.GetDrawable(
                res: Resources,
                id: IDWallet.Droid.Resource.Drawable.bkg_roundedview,
                theme: null);
            return drawable;
        }

        private AColor GetColor()
        {
            return Element?.Color.ToAndroid() ?? AColor.Gray;
        }

        private AColor GetRippleColor()
        {
            return Element?.RippleColor.ToAndroid() ?? AColor.Gray;
        }
    }
}