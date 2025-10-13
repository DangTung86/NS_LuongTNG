using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TNGLuong
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] != null)
            {
                if (Request.QueryString["page"] != null)
                {
                    string ss = Request.QueryString["page"].ToString();
                    switch (Request.QueryString["page"])
                    {
                        case "PhanQuyenUser":
                            phMain.Controls.Add(LoadControl("WebAdmin/production/PhanQuyenUser.ascx"));
                            break;
                        case "Orther":
                            phMain.Controls.Add(LoadControl("WebAdmin/production/Orther.ascx"));
                            break;
                        case "InsertMaHang":
                            phMain.Controls.Add(LoadControl("WebAdmin/production/InsertMaHang.ascx"));
                            break;
                        case "KhoaBangLuog":
                            phMain.Controls.Add(LoadControl("WebAdmin/production/IsPayroll.ascx"));
                            break;
                        case "ToTruong":
                            phMain.Controls.Add(LoadControl("WebAdmin/production/ToTruong.ascx"));
                            break;
                        case "KhoaChucNang":
                            phMain.Controls.Add(LoadControl("WebAdmin/production/KhoaChucNang.ascx"));
                            break;
                        default:
                            phMain.Controls.Add(LoadControl("WebAdmin/production/ResetPass.ascx"));
                            break;
                    }
                }
                else
                    phMain.Controls.Add(LoadControl("WebAdmin/production/ResetPass.ascx"));
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings.Count > 0)
                {
                    string sKhoaWeb = System.Configuration.ConfigurationManager.AppSettings["CloseWeb"].ToString();
                    if (!string.IsNullOrEmpty(sKhoaWeb) && sKhoaWeb.Equals("true"))
                    {
                        Session["username"] = "hethong";
                        Response.Redirect("admin?page=Orther");
                    }
                    else
                        Response.Redirect("Login.aspx");
                }
                else
                    Response.Redirect("Login.aspx");
            }
        }
    }
}