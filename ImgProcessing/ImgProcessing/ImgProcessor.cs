using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;


namespace ImgProcessing
{
    public class ImgProcessor : IDataBinder, IDisposable
    {
        private FileStream m_fs;
        private Bitmap m_img;
        private Size m_processingSize;
        private Size m_bufferSize;

#region Constructor
        public ImgProcessor(string path)
            : this(path, new Size(1000, 500))
        {
        }

        public ImgProcessor(string path, Size processingSize)
            : this(path, processingSize, new Size(300, 300))
        {
        }

        public ImgProcessor(string path, Size processingSize, Size bufferSize)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("imagePath", "Path to image should not be an empty string.");
            }

            LoadImage(path);

            if ((processingSize.Width > m_img.Width) || (processingSize.Height > m_img.Height))
            {
                throw new ArgumentException("Part size could not be bigger then image size.", "imagePartSize");
            }

            m_processingSize = processingSize;

            if ((bufferSize.Width > processingSize.Width) || (bufferSize.Height > processingSize.Height))
            {
                throw new ArgumentException("Processing size could not be bigger then processing size.", "processingSize");
            }

            this.m_bufferSize = bufferSize;
        }
#endregion

        public Size Size
        {
            get { return m_img.Size; }
        }

        public void ProcessByOperations(IEnumerable<IOperation> operations)
        {
            foreach (var operation in operations)
            {
                foreach (var part in SplitImage(m_img.Size, m_processingSize))
                {
                    operation.Execute(part);
                }
            }
        }

        public void ProcessByParts(IEnumerable<IOperation> operations)
        {
            foreach (var part in SplitImage(m_img.Size, m_processingSize))
            {
                foreach (var operation in operations)
                {
                    operation.Execute(part);
                }
            }
        }

        public void ProcesOperationWithData(Rectangle rect, Action<byte[]> operation)
        {
            foreach(var buffer in Helper.SplitToRectangle(rect.Size, m_bufferSize))
            {
                var data = m_img.LockBits(buffer, System.Drawing.Imaging.ImageLockMode.ReadWrite, m_img.PixelFormat);
                IntPtr ptr = data.Scan0;

                int bytes = Math.Abs(data.Stride) * buffer.Size.Height;
                byte[] rgbValues = new byte[bytes];

                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                operation(rgbValues);

                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

                m_img.UnlockBits(data);
            }
        }

        public Image BindCoordinateWithImage(Rectangle rect)
        {
            return m_img.Clone(rect, m_img.PixelFormat);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_img != null)
                {
                    m_img.Dispose();
                    m_img = null;
                }

                if (m_fs != null)
                {
                    m_fs.Close();
                    m_fs.Dispose();
                    m_fs = null;
                }
            }

            // free native resources if there are any.  
        }

        private void LoadImage(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Image does not exists.");
                throw new ArgumentException("Image does not exists.", "path");
            }

            m_fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            m_img = (Bitmap)Image.FromStream(m_fs);
        }

        private IEnumerable<ProcessingPart> SplitImage(Size img, Size processing)
        {
            int i = 0;
            foreach (var rect in Helper.SplitToRectangle(img, processing))
            {
                yield return new ProcessingPart(i, rect, this);
                i++;
            }
        }
    }
}
