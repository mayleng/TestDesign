using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace urlTestWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.Sleep(10);
            int n = 0;
            for (int i=0; i <= 100; i++)
            {
                n += i;
            }
            Response.Write(n);
        }
    }
}