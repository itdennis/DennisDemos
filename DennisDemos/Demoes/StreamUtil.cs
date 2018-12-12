using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DennisDemos.Demos
{
    public static class StreamUtil
    {
        const int BufferSize = 8192;
        public static void CopyTo(this Stream input, Stream output)
        {
            byte[] buffer = new byte[BufferSize];
            int read;
            while ((read = input.Read(buffer,0,buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }
        public static byte[] ReadFully(this Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                CopyTo(input, ms);
                return ms.ToArray();
            }
        }
    }
}
