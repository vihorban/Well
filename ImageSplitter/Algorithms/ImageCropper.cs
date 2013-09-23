using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.IO;

using ImageSplitter.Objects;
using System.Drawing.Imaging;

namespace ImageSplitter.Algorithms
{
    class ImageCropper
    {
        public static Image cropImage(Image image, Rectangle cropArea)
        {
            Bitmap bitmap = new Bitmap(image);
            Bitmap bmpCrop = bitmap.Clone(cropArea, bitmap.PixelFormat);
            return (Image)(bmpCrop);
        }

        public static void cropImage(string inputFileName, int numOfRows = 6, int numOfCols = 14, int numOfImportantRows = 6, int numOfImportantCols = 10)
        {
            List<Card> myDeck = DeckGenerator.generateMicrosoftDeck();
            Image image = Image.FromFile(inputFileName);
            double width = image.Width / (double)numOfCols;
            double height = image.Height / (double)numOfRows;
            string extension = ".png";
            for (int i = 0; i < numOfImportantRows; i++)
                for (int j = 0; j < numOfImportantCols; j++)
                {
                    double borderX = 7;
                    double borderY = 7;
                    int currentWidth = (int)(width - borderX);
                    int currentHeight = (int)(height - borderY);
                    Image newImage = new Bitmap(currentWidth, currentHeight);
                    Graphics graphics = Graphics.FromImage(newImage);
                    int cardPositionX = (int)(j * width + borderX/2) + 1;
                    int cardPositionY = (int)(i * height + borderY/2);
                    graphics.DrawImage(image, new Rectangle(0, 0, currentWidth, currentHeight), new Rectangle(cardPositionX, cardPositionY, currentWidth, currentHeight), GraphicsUnit.Pixel);
                    graphics.Dispose();
                    int currentNumberOfCard = i * numOfImportantCols + j;
                    if (i == 5)
                    {
                        if (j == numOfImportantCols - 1)
                            currentNumberOfCard = i * numOfImportantCols + 1;
                        else
                            currentNumberOfCard = i * numOfImportantCols;
                    }
                    Image resizedImage = new Bitmap(newImage, new Size(142,192));
                    resizedImage.Save(myDeck[currentNumberOfCard].path() + extension, ImageFormat.Png);
                }
        }
    }
}
