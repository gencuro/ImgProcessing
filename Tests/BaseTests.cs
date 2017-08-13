using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ImgProcessing;

namespace Tests
{
    [TestFixture]
    public class BaseTests
    {
        private readonly string m_testImgPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\..\..\..\samples\\testImg.jpg";

        [Test]
        public void TestEmptyString()
        {
            Assert.Throws(typeof(ArgumentNullException), () => { new ImgProcessor(string.Empty); });
        }

        [Test]
        public void TestNotExistingPath()
        {
            Assert.Throws(typeof(ArgumentException), () => { new ImgProcessor(@"C:\notExist.img"); });
        }

        [Test]
        public void TestProcessingSizeException()
        {
            Assert.Throws(typeof(ArgumentException), () => { new ImgProcessor(m_testImgPath, new System.Drawing.Size(12000, 100)); });
            Assert.Throws(typeof(ArgumentException), () => { new ImgProcessor(m_testImgPath, new System.Drawing.Size(100, 5000)); });

            Assert.Throws(typeof(ArgumentException), () => { new ImgProcessor(m_testImgPath, new System.Drawing.Size(100, 50), new System.Drawing.Size(200, 50)); });
            Assert.Throws(typeof(ArgumentException), () => { new ImgProcessor(m_testImgPath, new System.Drawing.Size(100, 50), new System.Drawing.Size(30, 80)); });
        }

        [Test]
        public void TestImageSize()
        {
            var testImageDimension = new System.Drawing.Size(5472, 3648);

            using (var imgProc = new ImgProcessor(m_testImgPath))
            {
                Assert.AreEqual(testImageDimension, imgProc.Size);
            }
        }

        [Test]
        [Explicit("Depends on save path.")]
        public void TestSplittingOperation()
        {
            var processingSize = new System.Drawing.Size(1000, 500);
            string directoryToSave = @"E:\1";

            using (var imgProc = new ImgProcessor(m_testImgPath, processingSize, new System.Drawing.Size(200, 100)))
            {
                int countOnHeight = imgProc.Size.Height / processingSize.Height;
                countOnHeight += (imgProc.Size.Height % processingSize.Height != 0) ? 1 : 0;

                int countOnWidth = imgProc.Size.Width / processingSize.Width;
                countOnWidth += (imgProc.Size.Width % processingSize.Width != 0) ? 1 : 0;

                int filesCount = countOnHeight * countOnWidth;

                imgProc.ProcessByOperations(new IOperation[] { new ImgProcessing.Callbacks.SplitToFileOperation(directoryToSave) });

                var files = System.IO.Directory.GetFiles(directoryToSave);

                Assert.AreEqual(filesCount, files.Length);
            }
        }
    }
}
