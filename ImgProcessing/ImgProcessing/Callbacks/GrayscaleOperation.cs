using System;
using System.Drawing;

namespace ImgProcessing.Callbacks
{
    public class GrayscaleOperation : IOperation
    {
        private void Grayscale(byte[] data)
        {
            for (int i = 0; i < data.Length; i += 3)
            {
                byte gray = (byte)(data[i] * .21 + data[i + 1] * .71 + data[i + 2] * .071);
                data[i] = data[i + 1] = data[i + 2] = gray;
            }
        }

        public void Execute(ProcessingPart part)
        {
            part.ExecuteOperation(Grayscale);
        }
    }
}
