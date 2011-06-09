using System;

namespace Venturous.TestWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnServerError_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}