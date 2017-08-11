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
        private IDataBinder m_binder;

        public ProcessingPart(int row, int column, int x, int y, int width, int height, IDataBinder binder)
        {
            if(binder == null)
            {
                throw new ArgumentNullException("binder");
            }

            Row = row;
            Column = column;
            Bounds = new Rectangle(x, y, width, height);
            Size = new Size(width, height);
            m_binder = binder;
        }

        public int Row { get; private set; }
        public int Column { get; private set; }
        public Rectangle Bounds { get; private set; }
        public Size Size { get; private set; }

        public void Save(string path)
        {
            m_binder.BindCoordinateWithImage(Bounds).Save(path);
        }
    }
}
