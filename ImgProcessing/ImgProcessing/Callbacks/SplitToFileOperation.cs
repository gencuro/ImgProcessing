using System;

namespace ImgProcessing.Callbacks
{
    public sealed class SplitToFileOperation : IOperation
    {
        private string m_path;

        public SplitToFileOperation(string path)
        {
            if(!System.IO.Directory.Exists(path))
            {
                throw new ArgumentException("Folder does not exists.", "path");
            }

            m_path = path;
        }

        public void Execute(ProcessingPart part)
        {
            string path = m_path + @"\" + part.Row.ToString() + part.Column.ToString() + @".jpeg";
            part.Save(path);
        }
    }
}
