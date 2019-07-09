using DennisDemos.Demoes;
using DennisDemos.Demoes.WeChat;

namespace DennisDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            //AreaClass shape = new Shape(100);
            //System.Console.WriteLine(shape.Area);

            {
                string base64Encoded = "QzpcVXNlcnNcdi15YW55d3Vcc291cmNlXHJlcG9zXERvYmlcRG9iaVxEb2JpXHd3d3Jvb3RcaW1hZ2VzXElNR184NDIyLnBuZw==";
                string base64Decoded;
                byte[] data = System.Convert.FromBase64String(base64Encoded);
                base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
            }


            {
                string base64Decoded = @"C:\Users\v-yanywu\source\repos\Dobi\Dobi\Dobi\wwwroot\images\IMG_8422.png";
                string base64Encoded;
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(base64Decoded);
                base64Encoded = System.Convert.ToBase64String(data);
            }
        }
    }
}
