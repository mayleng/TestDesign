using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestDesign.AppData
{
    //主要用来处理页面中a标签中的连接中的请求（默认a标签的url都是路径
    //排除href为JS或者方法）
    class PageTestMain
    {
        public string baseurl;
        public List<string> urls;
        public List<string> href;
        public List<string> Errorurls;
        public List<string> Outurls;

        /// <summary>
        /// 构造函数用来传递参数
        /// </summary>
        /// <param name="baseurl">初始URL地址</param>
        /// <param name="urls">定义的的一个基准列表，存储所有的URL</param>
        /// <param name="href">单个页面的URL集合</param>
        public PageTestMain(string baseurl, List<string> urls, List<string> href, List<string> Errorurls,List<string> Outurls)
        {
            this.baseurl = baseurl;
            this.urls = urls;
            this.href = href;
            this.Errorurls = Errorurls;
            this.Outurls = Outurls;
        }

        //用来获取整个网站中所有的a标签中的连接，并请求
        public void TestMain()
        {
            //如果href是urls的子集，就结束
            if (PageHandle.ChildList(urls,href)) 
            {
               
                return;
            }
            else
            {
                //暂存urls中的内容，后期循环请求使用
                List<string> list1 = new List<string>();
                list1.AddRange(urls);
               
                //将得到的href去重 更改为http开头
                href = PageHandle.ChangeHttp(href, baseurl);
                href = PageHandle.RemoveRepeat(href);

                //将href中新的url添加到urls中
                urls = PageHandle.ListMemIn(urls, href);
                

                //请求新的url，得到新的href
                foreach (string m in href)
                {
                    //新的url则请求
                    if (!(list1.Contains(m)))
                    {
                        string pam = " ";
                        string htmls = Request.GetRequest(m, pam);
                        href = PageHandle.GetHref(htmls);
                        //将得到的href去重 更改为http开头
                        href = PageHandle.ChangeHttp(href, baseurl);
                        href = PageHandle.RemoveRepeat(href);

                        //当输入url错误时，记录错误的url和原因
                        string str = PageHandle.GetHttpError(m, htmls);
                        Errorurls = PageHandle.StrJoinList(Errorurls, str);
       
                        //新的href循环
                        TestMain();                     
                    }                 
                }             
            }
        }

        //只对网站域名内的url请求，其他外链不请求
        public void TestBaseMain()
        {
            //如果href是urls的子集，就结束
            if (PageHandle.ChildList(urls, href))
            {
                return;
            }
            else
            {
                //暂存urls中的内容，后期循环请求使用
                List<string> list1 = new List<string>();
                list1.AddRange(urls);

                //暂存其他外链的url
                List<string> list2 = new List<string>();


                //将得到的href去重 更改为http开头
                href = PageHandle.ChangeHttp(href, baseurl);
                href = PageHandle.RemoveRepeat(href);

                //将外链的url提取出来放到oututl中
                foreach (string m in href)
                {
                    if (!PageHandle.InBaseUrl(baseurl, m))
                    {
                        list2.Add(m);
                    }
                }
                Outurls = PageHandle.ListMemIn(Outurls, list2);

                //去除href中的外链url
                foreach (string m in list2)
                {
                    href.Remove(m);
                }

                //将href中新的url添加到urls中
                urls = PageHandle.ListMemIn(urls, href);


                //请求新的url，得到新的href
                foreach (string m in href)
                {
                    //新的url则请求
                    if (!(list1.Contains(m)))
                    {
                        string pam = " ";
                        string htmls = Request.GetRequest(m, pam);
                        href = PageHandle.GetHref(htmls);
                        //将得到的href去重 更改为http开头
                        href = PageHandle.ChangeHttp(href, baseurl);
                        href = PageHandle.RemoveRepeat(href);

                        //当输入url错误时，记录错误的url和原因
                        string str = PageHandle.GetHttpError(m, htmls);
                        Errorurls = PageHandle.StrJoinList(Errorurls, str);

                        //新的href循环
                        TestBaseMain();
                    }
                }
            }
        }



    }
}
