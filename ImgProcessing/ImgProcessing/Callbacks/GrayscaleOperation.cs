using System;
using System.Drawing;

namespace ImgProcessing.Callbacks
{
    public class GrayscaleOperation : IOperation
    {
        public void Execute(ProcessingPart part)
        {
            var bits = part.GetBitmapData();

            IntPtr ptr = bits.Scan0;

            int bytes = Math.Abs(bits.Stride) * part.Size.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int i = 0; i < rgbValues.Length; i += 3)
            {
                byte gray = (byte)(rgbValues[i] * .21 + rgbValues[i + 1] * .71 + rgbValues[i + 2] * .071);
                rgbValues[i] = rgbValues[i + 1] = rgbValues[i + 2] = gray;
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            part.UnlockBits(bits);
        }
    }
}
