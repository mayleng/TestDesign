using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; //C# 的正则类

namespace TestDesign.AppData
{
    public static class PageHandle
    {

        //将字符串写入HTML文件
        public static void WriStrHtm(string contents)
        {
            //获取程序的根目录
            string baseUrl = System.AppDomain.CurrentDomain.BaseDirectory;
            //baseUrl=C:\Users\bonree\Desktop\webapi\netframework\TestDesign\TestDesign\bin\Debug

            //判断文件是否存在
            StreamWriter sr;
            if (File.Exists(baseUrl + "/../../content.html"))
            {
                File.Delete(baseUrl + "/../../content.html");
            }

            sr = File.CreateText(baseUrl + "/../../content.html");
            sr.WriteLine(contents);
            sr.Close();


        }


        //提取HTML字符串中的a标签中href值,将href值添加到一个string的集合中
        public static List<string> GetHref(string html)
        {
            Regex reg = new Regex(@"(?is)<a[^>]*?href=(['""\s]?)(?<href>[^'""\s]*)\1[^>]*?>");
            MatchCollection match = reg.Matches(html);
            List<string> hrefs = new List<string>();
            foreach (Match m in match)
            {
                string a = m.Groups["href"].Value;
                hrefs.Add(a);
            }
            return hrefs;
        }

        //请求失败时，查看失败的原因
        public static string GetHttpError(string url,string htmls)
        {
            if(htmls== "RequestError")
            {
                return "请求：" + url + "  RequestError";
            }
            if(htmls== "ResponseError")
            {
                return "请求：" + url + "  ResponseError";
            }
            else
            {
                return " "; //此逻辑是为了去掉为HTML页面时的正常请求
            }
        }

        //判断获得的href是否以http://或者https://开头
        public static bool HrefStartWithHttp(string url)
        {
            //将字符串转成小写
            url = url.ToLower();

            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //当URL不以http或者HTTPS开头时，判断是否以/ 开头
        public static bool HrefStartWithoutHttp(string url)
        {
            if (url.StartsWith("/"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //当URL不以http或者https开头时，拼接URL
        public static string JoinUrl(string baseurl, string url)
        {
            if (HrefStartWithoutHttp(url))
            {
                url = baseurl + url;

            }
            else
            {
                url = baseurl + "/" + url;
            }

            return url;
        }

        //获取网站的baseURL  baseurl格式为：http://localhost 不要/
        public static string GetBaseUrl(string url)
        {
            string baseurl = " ";
            url = url.Trim();
            //定义一个集合存储/的下标
            List<int> n = new List<int>();
            char[] myChar = url.ToCharArray();
            string s = "/";
            for (int i = 0; i < myChar.Length; i++)
            {
                if (myChar[i].ToString() == s)
                {
                    n.Add(i);
                }
            }

            if (n.Count <= 2)
            {
                baseurl = url;
            }
            else
            {
                //只需要获取第三小的那个数，就能得到第三个/的位置
                int n1 = n[0];
                for (int i = 0; i <= 2; i++)
                {

                    n1 = n.Min();
                    n.Remove(n1);


                }
                baseurl = url.Substring(0, n1);

            }

            return baseurl;

        }


        //判断列表中的成员是否是http://格式  不是的转换成http://格式
        public static List<string> ChangeHttp(List<string> list, string baseurl)
        {
            string ss;
            List<string> list1 = new List<string>();
            foreach (string m in list)
            {
                if (PageHandle.HrefStartWithHttp(m))
                {
                    list1.Add(m);
                }
                else
                {
                    ss = PageHandle.JoinUrl(baseurl, m);
                    list1.Add(ss);

                }
            }
            return list1;

        }

        //此方法用来用来去除列表中重复的元素
        public static List<string> RemoveRepeat(List<string> list)
        {
            List<string> list2 = new List<string>();
            foreach (string m in list)
            {
                if (!list2.Contains(m))
                {
                    list2.Add(m);
                }
            }
            return list2;


        }

        //判断列表2中的成员是否在列表1中，如果不存在则添加到列表1中
        public static List<string> ListMemIn(List<string> list1, List<string> list2)
        {
         
            //判断列表2中元素是否存在列表1中
            foreach (string m in list2)
            {
                if (!list1.Contains(m))
                {
                    list1.Add(m);
                }
            }
            return list1;
        }

        //判断小集合是大集合的子集list2<list1
        public static bool ChildList(List<string> list1, List<string> list2)
        {
            //子集和的元素少于大集合元素
            if (list2.Count <= list1.Count)
            {
                //并且子集和里的每一个元素都属于大集合
                foreach (string m in list2)
                {
                    if (list1.Contains(m))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }

                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //将某一个字符串加入一个列表
        public static List<string> StrJoinList(List<string> list,string ss)
        {
            if (!list.Contains(ss))
            {
                list.Add(ss);
            }
            return list;
        }

        //判断请求的url是否在域名类     传入的url以http://开头
        public static bool InBaseUrl(string baseurl,string url)
        {
            //将字符串转成小写
            baseurl = baseurl.ToLower();
            url = url.ToLower();
            if (url.StartsWith(baseurl))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
