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
            using(var img = m_binder.BindCoordinateWithImage(Bounds))
            {
                // img?.Save(path);  -- for VS2015, but unfortunately I have only VS2013.
                if(img != null)
                {
                    img.Save(path);
                }
            }
        }

        public void ExecuteOperation(Action<byte[]> operation)
        {
            m_binder.ProcesOperationWithData(Bounds, operation);
        }
    }
}
