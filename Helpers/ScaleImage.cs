using System.Drawing;
using System.Drawing.Drawing2D;

namespace Helpers
{
    public static class ScaleImage
    {
        public static Image Scale(Image source, int newWidth, int newHeight)
        {
            var clipRectangle=new Rectangle(0,0,source.Width,source.Height);
            Bitmap dest=new Bitmap(newWidth, newHeight);
            try
            {
                using (Graphics g = Graphics.FromImage(dest))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(source,
                        new Rectangle(0, 0, newWidth, newHeight),
                        clipRectangle, GraphicsUnit.Pixel);
                }//done with drawing on "g"
                return (Image)dest;//transfer IDisposable ownership
            }
            catch
            { //error before IDisposable ownership transfer
                if (dest != null) dest.Dispose();
                throw;
            }
        }
    }
}
