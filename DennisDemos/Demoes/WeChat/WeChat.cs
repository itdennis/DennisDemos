using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes.WeChat
{
    public class WeChat
    {
        private const string appid = "wx5549020498b877c2";
        private const string secret = "23e4036a44dd0f0d700cd659daf2c5b7";


        public class AccessToken
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public DateTime CreateDate { get; set; }


        }
        public void SendTemplete(string temid, string touser, object data)
        {
            var token = GetAccessToken();
            var postUrl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", token);
            var msgData = new
            {
                touser = touser,
                template_id = temid,
                data = data
            };
            var j = JsonConvert.SerializeObject(msgData);
            var res = GetDataByPost(postUrl, j);


        }
        private string GetAccessToken()
        {
            var token = new AccessToken();
            var accessToken = CacheHelper.GetCache("accessToken");
            if (accessToken == null)
            {
                //获取TOKEN
                token = GetToken();
                CacheHelper.SetCache("accessToken", token);

            }
            else
            {
                token = CacheHelper.GetCache("accessToken") as AccessToken;
                //判断时间
                if (DateTime.Now > token.CreateDate.AddSeconds(token.expires_in))
                {
                    //获取TOKEN
                    token = GetToken();
                    CacheHelper.SetCache("accessToken", token);
                }
            }


            var t = CacheHelper.GetCache("accessToken") as AccessToken;
            return t.access_token;
        }
        private AccessToken GetToken()
        {
            string apiurl = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appid}&secret={secret}";
            string res = GetDataByPost(apiurl);
            var v = JsonConvert.DeserializeObject<AccessToken>(res);
            v.CreateDate = DateTime.Now;
            return v;
        }
        private string GetDataByPost(string url, string postData = "")
        {
            string result = "";
            byte[] byteData = Encoding.GetEncoding("UTF-8").GetBytes(postData);
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Referer = url;
                request.Accept = "*/*";
                request.Timeout = 30 * 1000;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
                request.Method = "POST";
                request.ContentLength = byteData.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(byteData, 0, byteData.Length);
                stream.Flush();
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream backStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(backStream, Encoding.GetEncoding("UTF-8"));
                result = sr.ReadToEnd();
                sr.Close();
                backStream.Close();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
