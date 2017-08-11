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

        public ProcessingPart(int index, Rectangle bounds, IDataBinder binder)
        {
            if(binder == null)
            {
                throw new ArgumentNullException("binder");
            }

            Index = index;
            Bounds = bounds;
            Size = new Size(bounds.Width, bounds.Height);
            m_binder = binder;
        }

        public int Index { get; private set; }
        public Rectangle Bounds { get; private set; }
        public Size Size { get; private set; }

        public void Save(string path)
        {
            m_binder.BindCoordinateWithImage(Bounds).Save(path);
        }

        public BitmapData GetBitmapData()
        {
            return m_binder.BindLockBits(Bounds);
        }

        public void UnlockBits(BitmapData data)
        {
            m_binder.BindUnlockBits(data);
        }
    }
}
