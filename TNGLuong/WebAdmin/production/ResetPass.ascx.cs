using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Models;
using TNGLuong.ModelsView;

namespace TNGLuong.WebAdmin.production
{
    public partial class ResetPass : System.Web.UI.UserControl
    {
        TNGLuongDbContact db = null;
        TNG_CTLDbContact dbCTL = null;
        Info ifo = null;
        private MemoryCache cache = MemoryCache.Default;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Reset Pass";
            db = new TNGLuongDbContact();
            dbCTL = new TNG_CTLDbContact();
            ifo = new Info();
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    lblMessenger.Text = "";
                    Load_ddlDonVi();
                }
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

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string mans = txtmans.Value.ToString();
                string manstk = Session["username"].ToString();
                if (Session["username"].ToString().Equals("admin"))
                {
                    ResetPassUS(mans, manstk);
                }
                else
                {
                    object[] sqlPr =
                    {
                        new SqlParameter("@MaNS", mans),
                        new SqlParameter("@MaNS_Reset", manstk)
                    };
                    string sqlQuery = "[dbo].[pr_Web_check_CungDonVi] @MaNS,@MaNS_Reset";
                    List<string> lst = new List<string>();
                    lst = db.Database.SqlQuery<string>(sqlQuery, sqlPr).ToList();
                    if (lst != null && lst.Count > 0)
                    {
                        ResetPassUS(mans, manstk);
                    }
                    else
                    {
                        divMesssenger.Style["display"] = "block";
                        lblMessenger.Text = "Mã nhân sự muốn reset mật khẩu không thuộc đơn vị của bạn!";
                        return;
                    }
                }
            }
            catch { }
        }

        protected void ResetPassUS(string mans, string manstk)
        {
            DM_TaiKhoan us = new DM_TaiKhoan();
            us = db.DM_TaiKhoan.Where(x => x.MaNS.ToUpper() == mans.ToUpper()).FirstOrDefault();
            if (us != null)
            {
                us.PassWord = ifo.encryptString(us.SoCMT);
                us.UpdatePass = DateTime.Now;
                int id = db.SaveChanges();
                if (id != 0)
                {
                    LCB_WEB_ResetPass rsp = new LCB_WEB_ResetPass();
                    rsp.MaNS = mans.ToUpper();
                    rsp.MaNS_Reset = manstk.ToUpper();
                    rsp.NgayReset = DateTime.Now;
                    dbCTL.LCB_WEB_ResetPass.Add(rsp);
                    dbCTL.SaveChanges();

                    cache.Remove("Users");
                    List<View_Web_ThongTinNS> lst = new List<View_Web_ThongTinNS>();
                    lst = db.View_Web_ThongTinNS.ToList();
                    if (lst != null && lst.Count > 0)
                    {
                        cache.Set("Users", lst, DateTimeOffset.UtcNow.AddHours(10));
                        divMesssenger.Style["display"] = "block";
                        lblMessenger.Text = "Đã reset mật khẩu về mặc định.";
                        return;
                    }
                }
                else
                {
                    divMesssenger.Style["display"] = "block";
                    lblMessenger.Text = "Lỗi đổi mật khẩu vui lòng kiểm tra lại.";
                    return;
                }
            }
            else
            {
                divMesssenger.Style["display"] = "block";
                lblMessenger.Text = "Mã nhân sự này không tồn tại, vui lòng kiểm tra lại!";
                return;
            }
        }

        protected void Load_ddlDonVi()
        {
            try
            {
                List<View_ChiNhanh> lst = new List<View_ChiNhanh>();
                lst = dbCTL.View_ChiNhanh.ToList();
                if (lst != null && lst.Count > 0)
                {
                    ddlDonVi.DataSource = lst;
                    ddlDonVi.DataTextField = "TenDonVi";
                    ddlDonVi.DataValueField = "DonViID";
                    ddlDonVi.DataBind();

                    string idDonVi = "0";
                    if (!Session["username"].ToString().Equals("admin"))
                    {
                        if (!Session["DonViID"].ToString().Equals("25") && !Session["DonViID"].ToString().Equals("137") && !Session["DonViID"].ToString().Equals("136"))
                        {
                            object[] sqlPr =
                            {
                                new SqlParameter("@DonViID", Session["DonViID"].ToString())
                            };
                            string sqlQuery = "[dbo].[pr_Web_GetDonViID_CN] @DonViID";
                            List<string> lstStr = new List<string>();
                            lstStr = db.Database.SqlQuery<string>(sqlQuery, sqlPr).ToList();
                            ddlDonVi.SelectedValue = lstStr[0].ToString();
                            ddlDonVi.Enabled = false;
                            idDonVi = lstStr[0].ToString();
                        }
                        else
                        {
                            ddlDonVi.SelectedValue = lst[0].DonViID.ToString();
                            idDonVi = lst[0].DonViID.ToString();
                            ddlDonVi.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlDonVi.SelectedValue = lst[0].DonViID.ToString();
                        idDonVi = lst[0].DonViID.ToString();
                        ddlDonVi.Enabled = true;
                    }

                    Load_ddlToMay(idDonVi);
                }
            }
            catch (Exception ex) { }
        }

        protected void Load_ddlToMay(string idDonVi)
        {
            try
            {
                List<View_ToMay> lst = new List<View_ToMay>();
                lst = dbCTL.View_ToMay.Where(x => x.DonViID.Equals(idDonVi)).OrderBy(x => x.TenPhongban).ToList();
                if (lst != null && lst.Count > 0)
                {
                    ddlToMay.DataSource = lst;
                    ddlToMay.DataTextField = "TenPhongban";
                    ddlToMay.DataValueField = "PhongBanID";
                    ddlToMay.DataBind();
                    ddlToMay.SelectedValue = lst[0].PhongBanID.ToString();
                }
            }
            catch (Exception ex) { }
        }

        protected void ddlDonVi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string idDonVi = ddlDonVi.SelectedValue.ToString();
                Load_ddlToMay(idDonVi);
            }
            catch (Exception ex) { }
        }

        protected void btnResetToMay_Click(object sender, EventArgs e)
        {
            try
            {
                string manstk = Session["username"].ToString();
                int sus = 0;
                int phongbanid = 0;
                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    phongbanid = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                object[] sqlPr =
                {
                    new SqlParameter("@iPhongBanID", phongbanid)
                };
                // Processing.  
                string sqlQuery = "[dbo].[pr_Web_DM_TaiKhoan_SelectOne_MaNS_SoCMT_byPhongBanID] @iPhongBanID";
                List<clsResetToMay> lst = new List<clsResetToMay>();
                lst = db.Database.SqlQuery<clsResetToMay>(sqlQuery, sqlPr).ToList();
                foreach (clsResetToMay item in lst)
                {
                    string mans = item.MaNS.Trim();
                    DM_TaiKhoan us = new DM_TaiKhoan();
                    us = db.DM_TaiKhoan.Where(x => x.MaNS.ToUpper() == mans.ToUpper()).FirstOrDefault();
                    if (us != null)
                    {
                        us.PassWord = ifo.encryptString(us.SoCMT.Trim());
                        us.UpdatePass = DateTime.Now;
                        db.SaveChanges();
                    }
                }

                cache.Remove("Users");
                List<View_Web_ThongTinNS> lstNS = new List<View_Web_ThongTinNS>();
                lstNS = db.View_Web_ThongTinNS.ToList();
                if (lstNS != null && lstNS.Count > 0)
                {
                    cache.Set("Users", lstNS, DateTimeOffset.UtcNow.AddHours(10));
                    divMesssenger.Style["display"] = "block";
                    lblMessenger.Text = "Đã reset mật khẩu của các mã nhân sự trong tổ về mặc định.";
                    return;
                }
            }
            catch { }
        }
    }
}