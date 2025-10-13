using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TNGLuong
{
    public partial class CanhBao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btncloseMain.ServerClick += new EventHandler(btncloseMain_Click);
            if (!ultils.isMobileBrowser())
            {
                Session["CanhBaoLD"] = "CanhBao";
                Response.Redirect("Login.aspx");
            }
        }

        protected void btncloseMain_Click(object sender, EventArgs e)
        {
            Session["CanhBaoLD"] = "CanhBao";
            Response.Redirect("Login.aspx");
        }
    }
}