using System;
using System.Threading;

namespace Venturous.TestWeb
{
    public partial class UpdPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            lblText.Text = "I was clicked";
        }
    }
}