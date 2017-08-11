using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgProcessing
{
    public class ProcessingPart
    {
        public ProcessingPart(int row, int column, int x, int y, int width, int height)
        {
            Row = row;
            Column = column;
            Bounds = new Rectangle(x, y, width, height);
            Size = new Size(width, height);
        }

        public int Row { get; private set; }
        public int Column { get; private set; }
        public Rectangle Bounds { get; private set; }
        public Size Size { get; private set; }
        public Bitmap InnerBitmap { get; set; }
        //public BitmapData InnerData { get; set; }

        public void Save(string path)
        {
            // Save to file.
        }
    }
}
