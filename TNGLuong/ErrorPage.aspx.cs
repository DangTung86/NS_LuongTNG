using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TNGLuong
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Configuration.ConfigurationManager.AppSettings.Count > 0)
            {
                string sKhoaWeb = System.Configuration.ConfigurationManager.AppSettings["CloseWeb"].ToString();
                if (!string.IsNullOrEmpty(sKhoaWeb) && sKhoaWeb.Equals("true"))
                    lblErr.Text = "WEBSITE NGỪNG HOẠT ĐỘNG!".ToUpper();
                else
                    lblErr.Text = "Trang web đang được bảo trì, vui lòng quay lại sau.".ToUpper();
            }
            else
                lblErr.Text = "Trang web đang được bảo trì, vui lòng quay lại sau.".ToUpper();
        }
    }
}