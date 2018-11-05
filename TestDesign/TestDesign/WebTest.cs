using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TestDesign.AppData;

namespace TestDesign
{
    //该类中方法只是用来处理首页面（进入单页面）
    public partial class WebTest : Form
    {
        public WebTest()
        {
            InitializeComponent();
            //页面添加计时器
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(timer1EventProcessor);//添加事件

        }

        //开始运行测试
        private void button1_Click(object sender, EventArgs e)
        {
            //判断客户要发送get请求还是post请求
            bool GetChe = this.radioButton1.Checked;
            bool PostChe = this.radioButton2.Checked;


            //用户输入的URL
            string url = this.textBox1.Text.Trim();

            //用户输入的参数
            string pams = this.textBox5.Text.Trim();

            //用户输入的线程数
            int n = 0;
            try
            {
                n = int.Parse(this.textBox2.Text.Trim());
            }
            catch
            {
                n = 1;
            }


            //用户输入的循环次数
            int crl = 0;
            try
            {
                crl = int.Parse(this.textBox3.Text.Trim());
            }
            catch
            {
                crl = 1;
            }

            //用户选择请求的的url，是否请求所有的url
            bool Allurl = this.radioButton4.Checked;
            bool NotAllurl = this.radioButton3.Checked;

            string result = " ";

            //定义一个列表用来记录所有的URL
            List<string> urls = new List<string>();
            //  urls.Add(url);

            //定义一个列表用来记录错误请求的url和错误原因
            List<string> Errorurls = new List<string>();

            //定义一个列表用来记录外链的url
            List<string> Outurls = new List<string>();

            //获取baseurl
            string baseurl = PageHandle.GetBaseUrl(url);

            //开始执行页面请求
            if (GetChe)
            {
                // result = "Get请求，请求页面成功!";
                //get请求走的逻辑
                for (int i = 1; i <= n; i++)
                {

                    for (int j = 1; j <= crl; j++)
                    {
                        /*
                        Thread th = new Thread(new ThreadStart(() => Request.GetRequest(url, pams)));
                        th.Start();
                        //this.textBox4.Text += result;
                        th.Abort();
                        */
                        string htmls = Request.GetRequest(url, pams);
                        //提取HTML中a标签的href的值
                        List<string> href = PageHandle.GetHref(htmls);
                        //将得到的href去重 更改为http开头
                        href = PageHandle.ChangeHttp(href, baseurl);
                        href = PageHandle.RemoveRepeat(href);

                        //当输入url错误时，记录错误的url和原因
                        string str = PageHandle.GetHttpError(url, htmls);
                        Errorurls = PageHandle.StrJoinList(Errorurls, str);


                        PageTestMain p = new PageTestMain(baseurl, urls, href, Errorurls, Outurls);

                        //判断用户是否请求网站中所有的url
                        if (Allurl)
                        {
                            p.TestMain();
                        }
                        else
                        {
                            p.TestBaseMain();
                        }

                        //输出urls
                        urls = p.urls;
                        foreach (string m in urls)
                        {
                            result += "  ";
                            result += m;
                        }

                        result += "  ************************";

                        //输出Outurls
                        Outurls = p.Outurls;
                        Outurls = PageHandle.RemoveRepeat(Outurls);
                        foreach (string m in Outurls)
                        {
                            result += "  ";
                            result += m;
                        }

                        result += "   ========================";

                        //输出Errorurls
                        Errorurls = p.Errorurls;
                        //Errorurls = PageHandle.RemoveRepeat(Errorurls); 错误的url根本不需要去重
                        foreach (string m in Errorurls)
                        {
                            result += "  ";
                            result += m;
                        }





                    }

                }


            }
            else
            {
                // result = "Post请求，请求页面成功！";
                //post请求走的逻辑
                for (int i = 1; i <= n; i++)
                {
                    for (int j = 1; j <= crl; j++)
                    {

                        Thread th = new Thread(new ThreadStart(() => Request.PostRequest(url, pams)));
                        th.Start();
                        // this.textBox4.Text += result;
                        th.Abort();
                    }

                }


            }

            //执行结果显示
            this.textBox4.Text = result;

        }


        public void timer1EventProcessor(object source, EventArgs e)
        {
            this.label7.Text = DateTime.Now.ToString();
        }


    }
}
