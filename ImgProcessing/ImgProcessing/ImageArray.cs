using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgProcessing
{
    public class ImageArray<T>
    {
        private List<List<T>> m_items = new List<List<T>>();

        public void AppendRow(IList<T> elements)
        {

        }

        public T this[int x, int y]
        {
            get
            {
                return m_items[x][y];
            }
            set
            {
                m_items[x][y] = value;
            }
        }  
    }
}
