using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net; //该框架可用来对其他网站发送请求
using System.Text;

namespace TestDesign.AppData
{
    public static class Request
    {


        //Get请求
        public static string GetRequest(string url, string pams)
        {
            url += pams;
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "GET";
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.1;Win64;x64) AppleWebKit/537.36 (KHTML,like Gecko)Chrome/67.0.3396.79 Safari/537.36";
               
                //请求页面服务器出现异常，返回一个特定的字符串
                try
                {
                    WebResponse wr = req.GetResponse();
                    using (StreamReader reader = new StreamReader(wr.GetResponseStream(), System.Text.Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
                catch
                {
                    //请求失败返回一个特定的字符串
                    return "ResponseError";
                }
            }
            catch
            {
                //url无效
                return "RequestError";
            }                                    

        }


        //Post请求
        public static string PostRequest(string url, string pams)
        {
            byte[] bs = Encoding.ASCII.GetBytes(pams);
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.1;Win64;x64) AppleWebKit/537.36 (KHTML,like Gecko)Chrome/67.0.3396.79 Safari/537.36";
                req.ContentLength = bs.Length;
                Stream reqStream = req.GetRequestStream();
                reqStream.Write(bs, 0, bs.Length);
                try
                {
                    HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
                catch
                {
                    //请求失败返回一个特定的字符串
                    return "ResponseError";
                }
              

            }
            catch
            {
                //url无效
                return "RequestError";
            }
           
        }

       









    }
}
