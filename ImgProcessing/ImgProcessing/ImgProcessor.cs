using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;


namespace ImgProcessing
{
    public class ImgProcessor : IDisposable
    {
        private FileStream m_fs;
        private Bitmap m_img;
        private Size m_partSize;
        private Size m_processingSize;
        private List<List<Rectangle>> m_parts = new List<List<Rectangle>>();


        public ImgProcessor(string imagePath) : this(imagePath, new Size(1000, 500))
        {
        }

        public ImgProcessor(string imagePath, Size imagePartSize) : this(imagePath, imagePartSize, new Size(300, 300))
        {
        }

        public ImgProcessor(string imagePath, Size imagePartSize, Size processingSize)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                throw new ArgumentNullException("imagePath", "Path to image should not be an empty string.");
            }

            LoadImage(imagePath);

            if ((imagePartSize.Width > m_img.Width) || (imagePartSize.Height > m_img.Height))
            {
                throw new ArgumentException("Part size could not be bigger then image size.", "imagePartSize");
            }

            m_partSize = imagePartSize;

            if((processingSize.Width > m_partSize.Width) || (processingSize.Height > m_partSize.Height))
            {
                throw new ArgumentException("Processing size could not be bigger then processing size.", "processingSize");
            }

            m_processingSize = processingSize;

            SplitImage();
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

        public Size ProcessingParts { get; set; }

        public Image this[int x, int y]
        {
            get
            {
                return m_img.Clone(m_parts[x][y], m_img.PixelFormat);
            }
            /*
            private set
            {
                //m_parts[x][y] = value;
                m_parts[x][y] = value.Size;
            }*/
        }

        private void LoadImage(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine("Image does not exists.");
                throw new ArgumentException("Image does not exists.", "imagePath");
            }

            m_fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            m_img = (Bitmap)Image.FromStream(m_fs);
        }

        private void SplitImage()
        {
            int xs = 0;
            //int ys = 0;

            int x = 0;
            int y = 0;

            while (x < m_img.Width)
            {
                m_parts.Add(new List<Rectangle>());
                
                while (y < m_img.Height)
                {
                    var height = (y + m_partSize.Height < m_img.Height) ? m_partSize.Height : (m_img.Height - y);
                    m_parts[xs].Add(new Rectangle(x, y, m_partSize.Width, height));

                    y += height;
                }

                y = 0;

                var width = (x + m_partSize.Width < m_img.Width) ? m_partSize.Width : (m_img.Width - x);
                x += width;

                xs += 1;
            }

            ProcessingParts = new Size(m_parts.Count, m_parts[0].Count);
            /*

            int x = m_img.Width / m_partSize.Width;

            ProcessingParts = new Size(6, 7);*/
        }
    }
}
