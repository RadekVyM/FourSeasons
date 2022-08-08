#if WINDOWS
using Microsoft.UI.Xaml.Media.Imaging;
using WImage = Microsoft.UI.Xaml.Controls.Image;

namespace FourSeasons.Maui.Behaviors
{
    public partial class CenteredImageBehavior : PlatformBehavior<Image, WImage> // Note the use of UIImageView which is an iOS-specific API
    {
        private Image bindable;
        private WImage platformView;
        private View parent;

        protected override void OnAttachedTo(Image bindable, WImage platformView)
        {
            if (bindable.Parent is View parent)
            {
                parent.SizeChanged += ParentSizeChanged;

                this.parent = parent;
                this.platformView = platformView;
                this.bindable = bindable;

                bindable.VerticalOptions = LayoutOptions.Center;
                bindable.HorizontalOptions = LayoutOptions.Center;
            }
        }

        private void ParentSizeChanged(object sender, EventArgs e)
        {
            double width = this.platformView.Width;
            double height = this.platformView.Height;
            var parentSize = new Size(this.parent.Width, this.parent.Height);
            var parentRatio = parentSize.Width / parentSize.Height;

            if (platformView.Source is BitmapImage bitmap)
            {
                var imageSize = new Size(bitmap.PixelWidth, bitmap.PixelHeight);
                var imageRatio = imageSize.Width / imageSize.Height;
                if (parentRatio <= imageRatio)
                {
                    width = imageSize.Width / (imageSize.Height / parentSize.Height);
                    height = parentSize.Height;
                }
                else
                {
                    width = parentSize.Width;
                    height = imageSize.Height / (imageSize.Width / parentSize.Width);
                }
            }

            bindable.WidthRequest = width;
            bindable.HeightRequest = height;
        }

        protected override void OnDetachedFrom(Image bindable, WImage platformView)
        {
            if (bindable.Parent is View parent)
            {
                parent.SizeChanged -= ParentSizeChanged;
            }
        }
    }
}
#endif