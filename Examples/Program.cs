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
            string exeLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeDir = System.IO.Path.GetDirectoryName(exeLocation);
            string imgPath = exeDir + @"\..\..\..\samples\\zeebraJpegLarge.tif";

            using (ImgProcessor ip = new ImgProcessor(imgPath))
            {
                ImgProcessing.Callbacks.GrayscaleOperation gop = new ImgProcessing.Callbacks.GrayscaleOperation();

                var sv = new ImgProcessing.Callbacks.SplitToFileOperation(@"E:\1");

                ip.ProcessByOperations(new IOperation[] { sv });
            }
        }
    }
}
