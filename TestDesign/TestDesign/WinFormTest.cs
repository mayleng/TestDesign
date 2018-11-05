using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestDesign
{
    public partial class WinFormTest : Form
    {
        public WinFormTest()
        {
            InitializeComponent();
        }

        //执行winform相关的操作
        private void button1_Click(object sender, EventArgs e)
        {
            //窗口类名
            string WinformClass = this.textBox1.Text;

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

            //用户输入字符串
            string strs = this.textBox2.Text;

            //测试结果
            string result = " result";
            this.textBox4.Text = result;






        }
    }
}
