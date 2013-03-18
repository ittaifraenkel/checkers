using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace checkers
{
    class AddPics
    {
        public AddPics(Size size)
        {
            picSize = size;
        }
        private Size picSize;
        private Image xPic = Image.FromFile(@"Red.png");
        private Image oPic = Image.FromFile(@"Black.png");
        private Image qxPic = Image.FromFile(@"RedKing.png");
        private Image qoPic = Image.FromFile(@"BlackKing.png");
        public Image GetX()
        {
            return ResizeImage(xPic, picSize);
        }

        public Image GetO()
        {
            return ResizeImage(oPic, picSize);
        }

        public Image GetQX()
        {
            return ResizeImage(qxPic, picSize);
        }

        public Image GetQO()
        {
            return ResizeImage(qoPic, picSize);
        }

        public static Image ResizeImage(Image image, Size size,
        bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
    }
}