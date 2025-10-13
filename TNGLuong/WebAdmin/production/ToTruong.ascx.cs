using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class ToTruong : System.Web.UI.UserControl
    {
        TNG_CTLDbContact db = null;
        TNGLuongDbContact dblg = null;
        private MemoryCache cache = MemoryCache.Default;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Cập nhật danh sách tổ trưởng";
            db = new TNG_CTLDbContact();
            dblg = new TNGLuongDbContact();
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    lblMessenger.Text = "";
                    Load_ddlDonVi();
                    LoadDataGrid();
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

        protected void Load_ddlDonVi()
        {
            try
            {
                List<View_ChiNhanh> lst = new List<View_ChiNhanh>();
                lst = db.View_ChiNhanh.ToList();
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
                lst = db.View_ToMay.Where(x => x.DonViID.Equals(idDonVi)).OrderBy(x => x.TenPhongban).ToList();
                if (lst != null && lst.Count > 0)
                {
                    ddlToMay.DataSource = lst;
                    ddlToMay.DataTextField = "TenPhongban";
                    ddlToMay.DataValueField = "PhongBanID";
                    ddlToMay.DataBind();
                    ddlToMay.SelectedValue = lst[0].PhongBanID.ToString();

                    string mans = getMaNS();
                    if (!string.IsNullOrEmpty(mans))
                        txtMaNS.Text = mans.ToUpper();
                    else
                        txtMaNS.Text = "";
                }
            }
            catch (Exception ex) { }
        }

        protected InfoUserName getclsThongTinNhanSu()
        {
            try
            {
                InfoUserName nsClass = new InfoUserName();
                SqlParameter pr = new SqlParameter("@MaNS", txtMaNS.Text.Trim().ToUpper());
                nsClass = dblg.Database.SqlQuery<InfoUserName>("ThongTinNS_Goc_GetInfoByMaNS @MaNS", pr).SingleOrDefault();
                return nsClass;
            }
            catch (Exception ex) { return null; }
        }

        protected string getMaNS()
        {
            try
            {
                int phongbanid = 0;
                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    phongbanid = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                SqlParameter pr = new SqlParameter("@PhongBanID", phongbanid);
                string mans = db.Database.SqlQuery<string>("pr_Web_LCB_ToTruong_CBC_SelectMaNS_ByPhongBanID @PhongBanID", pr).SingleOrDefault();
                return mans;
            }
            catch (Exception ex) { return string.Empty; }
        }

        protected void LoadDataGrid()
        {
            try
            {
                int donviid = 0;
                if (ddlDonVi.SelectedValue != null && ddlDonVi.SelectedValue.ToString() != "")
                    donviid = int.Parse(ddlDonVi.SelectedValue.ToString());
                object[] sqlPr =
                {
                    new SqlParameter("@DonViID", donviid)
                };
                string sqlQuery = "[dbo].[pr_LCB_ToTruong_CBC_SelectAll] @DonViID";
                List<LCB_ToTruong_CBC_Append> lst = new List<LCB_ToTruong_CBC_Append>();
                lst = db.Database.SqlQuery<LCB_ToTruong_CBC_Append>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    gvToTruong.DataSource = lst;
                    gvToTruong.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("PhongBanID", typeof(int)));
                    dt.Columns.Add(new DataColumn("TenDonVi", typeof(string)));
                    dt.Columns.Add(new DataColumn("TenPhongban", typeof(string)));
                    dt.Columns.Add(new DataColumn("MaNS", typeof(string)));
                    dt.Columns.Add(new DataColumn("HoTen", typeof(string)));
                    
                    DataRow row = dt.NewRow();
                    row["PhongBanID"] = 0;
                    row["TenDonVi"] = "";
                    row["TenPhongban"] = "";
                    row["MaNS"] = "";
                    row["HoTen"] = "";
                    dt.Rows.Add(row);
                    gvToTruong.DataSource = dt;
                    gvToTruong.DataBind();
                    gvToTruong.Rows[0].Cells.Clear();
                    gvToTruong.Rows[0].Cells.Add(new TableCell());
                    gvToTruong.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                    gvToTruong.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gvToTruong.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("PhongBanID", typeof(int)));
                dt.Columns.Add(new DataColumn("TenDonVi", typeof(string)));
                dt.Columns.Add(new DataColumn("TenPhongban", typeof(string)));
                dt.Columns.Add(new DataColumn("MaNS", typeof(string)));
                dt.Columns.Add(new DataColumn("HoTen", typeof(string)));

                DataRow row = dt.NewRow();
                row["PhongBanID"] = 0;
                row["TenDonVi"] = "";
                row["TenPhongban"] = "";
                row["MaNS"] = "";
                row["HoTen"] = "";
                dt.Rows.Add(row);
                gvToTruong.DataSource = dt;
                gvToTruong.DataBind();
                gvToTruong.Rows[0].Cells.Clear();
                gvToTruong.Rows[0].Cells.Add(new TableCell());
                gvToTruong.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                gvToTruong.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gvToTruong.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void ddlDonVi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string idDonVi = ddlDonVi.SelectedValue.ToString();
                Load_ddlToMay(idDonVi);
                LoadDataGrid();
            }
            catch (Exception ex) { }
        }

        protected void ddlToMay_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string mans = getMaNS();
                if (!string.IsNullOrEmpty(mans))
                    txtMaNS.Text = mans.ToUpper();
                else
                    txtMaNS.Text = "";
                LoadDataGrid();
            }
            catch (Exception ex) { }
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaNS.Text))
                {
                    divMesssenger.Style["display"] = "block";
                    lblMessenger.Text = "Mã nhân sự chưa được nhập.";
                }
                else
                {
                    int sus = 0;
                    int phongbanid = 0;
                    if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                        phongbanid = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                    InfoUserName nsClass = getclsThongTinNhanSu();
                    LCB_ToTruong_CBC cls = new LCB_ToTruong_CBC();
                    cls = db.LCB_ToTruong_CBC.Where(x => x.MaNS_ID == nsClass.MaNS_ID.Value).FirstOrDefault();
                    if (cls != null)
                    {
                        LCB_ToTruong_CBC clsPB = new LCB_ToTruong_CBC();
                        clsPB = db.LCB_ToTruong_CBC.Where(x => x.PhongBanID == cls.PhongBanID).FirstOrDefault();
                        if (clsPB != null)
                            db.LCB_ToTruong_CBC.Remove(clsPB);
                        sus = db.SaveChanges();
                        if (sus != 0)
                        {
                            cls = new LCB_ToTruong_CBC();
                            cls.PhongBanID = phongbanid;
                            cls.MaNS_ID = nsClass.MaNS_ID.Value;
                            cls.Hang = null;
                            cls.TyLe = null;
                            cls.MaNS_ID_QLPX = null;
                            cls.Nguoi_Tao = Session["username"].ToString();
                            cls.Ngay_Tao = DateTime.Now;
                            db.LCB_ToTruong_CBC.Add(cls);
                            sus = db.SaveChanges();
                        }
                    }
                    else
                    {
                        cls = new LCB_ToTruong_CBC();
                        cls.PhongBanID = phongbanid;
                        cls.MaNS_ID = nsClass.MaNS_ID.Value;
                        cls.Hang = null;
                        cls.TyLe = null;
                        cls.MaNS_ID_QLPX = null;
                        cls.Nguoi_Tao = Session["username"].ToString();
                        cls.Ngay_Tao = DateTime.Now;
                        db.LCB_ToTruong_CBC.Add(cls);
                        sus = db.SaveChanges();
                    }
                    if (sus != 0)
                    {
                        cache.Remove("Users");
                        List<View_Web_ThongTinNS> lst = new List<View_Web_ThongTinNS>();
                        lst = dblg.View_Web_ThongTinNS.ToList();
                        if (lst != null && lst.Count > 0)
                            cache.Set("Users", lst, DateTimeOffset.UtcNow.AddHours(10));
                        divMesssenger.Style["display"] = "block";
                        lblMessenger.Text = "Đã cập nhật lại tổ trưởng!";                        
                    }
                    LoadDataGrid();                    
                }
            }
            catch (Exception ex) { }
        }

        protected void gvToTruong_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvToTruong_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                LCB_ToTruong_CBC cls = new LCB_ToTruong_CBC();
                int idphongban = Convert.ToInt32(gvToTruong.DataKeys[e.RowIndex].Value.ToString());
                cls = db.LCB_ToTruong_CBC.Where(x => x.PhongBanID == idphongban).SingleOrDefault();
                if(cls != null)
                    db.LCB_ToTruong_CBC.Remove(cls);
                int sus = db.SaveChanges();
                if (sus != 0)
                {
                    divMesssenger.Style["display"] = "block";
                    lblMessenger.Text = "Đã xóa tổ trưởng của tổ "+ ddlToMay.SelectedItem.Text.ToUpper() +"!";
                }
                LoadDataGrid();
            }
            catch (Exception ex)
            {

            }
        }
    }
}