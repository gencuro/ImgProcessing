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

            //using (ImgProcessor ip = new ImgProcessor(imgPath))
            using (ImgProcessor ip = new ImgProcessor(imgPath, new System.Drawing.Size(7000, 2000)))
            {
                // TODO : REMOVE
                ip.ProcessByParts(new IOperation[] { new ImgProcessing.Callbacks.GrayscaleOperation() });



                ProcessByOperationsExample(ip);
                ProcessByPartsExample(ip);
            }
        }

        private static void ProcessByOperationsExample(ImgProcessor imgProc)
        {
            imgProc.ProcessByOperations(new IOperation[] { new ImgProcessing.Callbacks.GrayscaleOperation(), new ImgProcessing.Callbacks.SplitToFileOperation(@"E:\1") });
        }

        private static void ProcessByPartsExample(ImgProcessor imgProc)
        {
            imgProc.ProcessByParts(new IOperation[] { new ImgProcessing.Callbacks.GrayscaleOperation(), new ImgProcessing.Callbacks.SplitToFileOperation(@"E:\1") });
        }
    }
}
