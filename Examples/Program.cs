using ImgProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {

            int i = 5 % 2;

            string imgPath = "E:\\GitHub\\ImgProcessing\\samples\\zeebraJpegLarge.tif";
            using (ImgProcessor ip = new ImgProcessor(imgPath))
            {
                //var img = ip.Chunks[0, 0];

                ip[19,29].Save(@"E:\q.tiff");
            }
        }
    }
}
