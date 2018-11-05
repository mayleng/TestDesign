using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace urlTestWeb
{
    public partial class Wpage2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.Sleep(10);
            string[] str = { "a", "b", "c", "d", "e" ,"f"};
            string n = "";
            foreach (string i in str)
            {
                n += i;
            }
            Response.Write(n);

        }
    }
}