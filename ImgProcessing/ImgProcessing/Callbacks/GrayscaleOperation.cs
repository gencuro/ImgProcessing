
using System.Drawing;
namespace ImgProcessing.Callbacks
{
    public class GrayscaleOperation : IOperation
    {
        public void Execute(ProcessingPart part)
        {
            for (int y = 0; y < part.Size.Height; y++)
            {
                for (int x = 0; x < part.Size.Width; x++)
                {
                    /*
                    Color color = part.InnerBitmap.GetPixel(x, y);

                    int avg = (color.R + color.G + color.B) / 3;
                    part.InnerBitmap.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
                     * */
                }
            }

            part.Save(@"E:\q.tiff");
        }
    }
}
