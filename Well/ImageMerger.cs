using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Well
{
    public class ImageMerger
    {
        public const string BasePath = "pack://application:,,,";

        public static ImageSource Merge(string folder)
        {
            string path1 = folder + "10Clubs.png";
            string path2 = folder + "11Diamonds.png";
            string path3 = folder + "12Hearts.png";
            string path4 = folder + "13Spades.png";
            var uri1 = new Uri(BasePath + path1);
            var uri2 = new Uri(BasePath + path2);
            var uri3 = new Uri(BasePath + path3);
            var uri4 = new Uri(BasePath + path4);
            BitmapFrame frame1 =
                BitmapDecoder.Create(uri1, BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First();
            BitmapFrame frame2 =
                BitmapDecoder.Create(uri2, BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First();
            BitmapFrame frame3 =
                BitmapDecoder.Create(uri3, BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First();
            BitmapFrame frame4 =
                BitmapDecoder.Create(uri4, BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First();

            // Gets the size of the images (I assume each image has the same size)
            int imageWidth = frame1.PixelWidth;
            int imageHeight = frame1.PixelHeight;

            // Draws the images into a DrawingVisual component
            var drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawImage(frame1, new Rect(0, 0, imageWidth, imageHeight));
                drawingContext.DrawImage(frame2, new Rect(imageWidth, 0, imageWidth, imageHeight));
                drawingContext.DrawImage(frame3, new Rect(imageWidth*2, 0, imageWidth, imageHeight));
                drawingContext.DrawImage(frame4, new Rect(imageWidth*3, 0, imageWidth, imageHeight));
            }

            // Converts the Visual (DrawingVisual) into a BitmapSource
            var bmp = new RenderTargetBitmap(imageWidth*4, imageHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            return bmp;
        }
    }
}