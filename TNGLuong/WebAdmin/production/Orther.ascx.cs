using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Models;

namespace TNGLuong.WebAdmin.production
{
    public partial class Orther : System.Web.UI.UserControl
    {
        TNGLuongDbContact db = null;
        private MemoryCache cache = MemoryCache.Default;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Chức năng khác";
            db = new TNGLuongDbContact();
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            if (Session["username"] != null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings.Count > 0)
                {
                    string sKhoaWeb = System.Configuration.ConfigurationManager.AppSettings["CloseWeb"].ToString();
                    if (!string.IsNullOrEmpty(sKhoaWeb) && sKhoaWeb.Equals("true"))
                        btnShow_Hide_BL.Text = "Mở Website";
                    else
                        btnShow_Hide_BL.Text = "Đóng Website";
                }
                if (!IsPostBack)
                    lblMessenger.Text = "";
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            lblMessenger.Text = "";
            divMesssenger.Style["display"] = "none";
        }

        protected void btnMempryCache_Click(object sender, EventArgs e)
        {
            cache.Remove("Users");
            List<View_Web_ThongTinNS> lst = new List<View_Web_ThongTinNS>();
            lst = db.View_Web_ThongTinNS.ToList();
            if (lst != null && lst.Count > 0)
            {
                cache.Set("Users", lst, DateTimeOffset.UtcNow.AddHours(10));
                divMesssenger.Style["display"] = "block";
                lblMessenger.Text = "Đã cập nhật lại thông tin người dùng.";
            }
        }

        protected void btnDBNangSuat_Click(object sender, EventArgs e)
        {
            string sqlQuery = "EXEC [TNG_Data].[dbo].[pr_LCB_KeHoach_NhanVien_SyncFromCTL]";
            db.Database.CommandTimeout = 3600;
            int sus = db.Database.ExecuteSqlCommand(sqlQuery);
            if(sus != 0)
            {
                db.Database.CommandTimeout = 30;
                divMesssenger.Style["display"] = "block";
                lblMessenger.Text = "Đã cập nhật lại thông tin giao khoán.";
            }
        }

        protected void btnDBCapBTP_Click(object sender, EventArgs e)
        {
            string sqlQuery = "EXEC [TNG_Data].[dbo].[LCB_SoLuong_CapBTP_SynData]";
            db.Database.CommandTimeout = 1800;
            int sus = db.Database.ExecuteSqlCommand(sqlQuery);
            if (sus != 0)
            {
                db.Database.CommandTimeout = 30;
                divMesssenger.Style["display"] = "block";
                lblMessenger.Text = "Đã cập nhật lại thông tin số cấp BTP.";
            }
        }

        protected void btnShow_Hide_BL_Click(object sender, EventArgs e)
        {
            if (btnShow_Hide_BL.Text == "Mở Website")
            {
                UpdateAppSettings("CloseWeb","false");
                btnShow_Hide_BL.Text = "Đóng Website";
            }
            else
            {
                UpdateAppSettings("CloseWeb", "true");
                btnShow_Hide_BL.Text = "Mở Website";
            }
        }

        private void UpdateAppSettings(string key, string value)
        {
            System.Configuration.Configuration configFile = null;
            if (System.Web.HttpContext.Current != null)
            {
                configFile =
                    System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            }
            else
            {
                configFile =
                    ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
    }
}