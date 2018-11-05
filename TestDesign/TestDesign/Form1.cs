using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestDesign.AppData;


namespace TestDesign
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        //进入网站测试界面
        private void button1_Click_1(object sender, EventArgs e)
        {
            WebTest w1 = new WebTest();
            w1.ShowDialog();
            // this.Close();
        }

        //进入winform测试界面
        private void button2_Click(object sender, EventArgs e)
        {
            WinFormTest f1 = new WinFormTest();
            f1.ShowDialog();
        }
    }
}
