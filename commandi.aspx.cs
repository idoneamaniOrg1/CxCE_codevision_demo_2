using System;
using System.Diagnostics;
using system.Web.Security.AntiXss.Sanitize;

namespace CxCE_Demo
{
    public partial class commandi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string processtostart = AntiXss.Sanitize(systemname.Text);
            Process.Start(processtostart);
        }
    }
}
