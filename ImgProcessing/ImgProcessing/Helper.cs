using System.Collections.Generic;
using System.Drawing;

namespace ImgProcessing
{
    public static class Helper
    {
        public static IEnumerable<Rectangle> SplitToRectangle(Size img, Size part)
        {
            int x = 0;

            while (x < img.Width)
            {
                int y = 0;

                while (y < img.Height)
                {
                    int height = (y + part.Height < img.Height) ? part.Height : (img.Height - y);
                    yield return new Rectangle(x, y, part.Width, height);
                    y += height;
                }

                y = 0;

                int width = (x + part.Width < img.Width) ? part.Width : (img.Width - x);
                x += width;
            }
        }
    }
}
