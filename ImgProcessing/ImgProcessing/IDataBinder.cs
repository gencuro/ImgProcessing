using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgProcessing
{
    public interface IDataBinder
    {
        Image BindCoordinateWithImage(Rectangle rect);

    }
}
