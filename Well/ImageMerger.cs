using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Well
{
    public class ImageMerger
    {
        public static ImageSource merge(string folder)
        {
            string path1 = folder + "10Clubs.png";
            string path2 = folder + "11Diamonds.png";
            string path3 = folder + "12Hearts.png";
            string path4 = folder + "13Spades.png";
            Uri uri1 = new Uri("pack://application:,,," + path1);
            Uri uri2 = new Uri("pack://application:,,," + path2);
            Uri uri3 = new Uri("pack://application:,,," + path3);
            Uri uri4 = new Uri("pack://application:,,," + path4);
            BitmapFrame frame1 = BitmapDecoder.Create(uri1, BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First();
            BitmapFrame frame2 = BitmapDecoder.Create(uri2, BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First();
            BitmapFrame frame3 = BitmapDecoder.Create(uri3, BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First();
            BitmapFrame frame4 = BitmapDecoder.Create(uri4, BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First();

            // Gets the size of the images (I assume each image has the same size)
            int imageWidth = frame1.PixelWidth;
            int imageHeight = frame1.PixelHeight;

            // Draws the images into a DrawingVisual component
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawImage(frame1, new Rect(0, 0, imageWidth, imageHeight));
                drawingContext.DrawImage(frame2, new Rect(imageWidth, 0, imageWidth, imageHeight));
                drawingContext.DrawImage(frame3, new Rect(imageWidth * 2, 0, imageWidth, imageHeight));
                drawingContext.DrawImage(frame4, new Rect(imageWidth * 3, 0, imageWidth, imageHeight));
            }

            // Converts the Visual (DrawingVisual) into a BitmapSource
            RenderTargetBitmap bmp = new RenderTargetBitmap(imageWidth * 4, imageHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            return bmp;
        }
    }
}
