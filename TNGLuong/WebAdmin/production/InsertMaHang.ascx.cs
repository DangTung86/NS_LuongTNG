using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Models;

namespace TNGLuong.WebAdmin.production
{
    public partial class InsertMaHang : System.Web.UI.UserControl
    {
        TNG_CTLDbContact db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Cập nhật mã hàng bỏ BTP";
            db = new TNG_CTLDbContact();
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM");
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
                if(lst != null && lst.Count>0)
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
            catch(Exception ex) { }
        }

        protected void Load_ddlToMay(string idDonVi)
        {
            try
            {
                List<View_ToMay> lst = new List<View_ToMay>();
                lst = db.View_ToMay.Where(x=>x.DonViID.Equals(idDonVi)).OrderBy(x=>x.TenPhongban).ToList();
                if (lst != null && lst.Count > 0)
                {
                    ddlToMay.DataSource = lst;
                    ddlToMay.DataTextField = "TenPhongban";
                    ddlToMay.DataValueField = "PhongBanID";
                    ddlToMay.DataBind();
                    ddlToMay.SelectedValue = lst[0].PhongBanID.ToString();
                    Load_ddlMaHang();
                }
            }
            catch (Exception ex) { }
        }

        protected void Load_ddlMaHang()
        {
            try
            {
                int idToMay = 0;
                int iddonvi = 0;
                int.TryParse(ddlToMay.SelectedValue.ToString(), out idToMay);
                int.TryParse(ddlDonVi.SelectedValue.ToString(), out iddonvi);
                DateTime dte = Convert.ToDateTime(txtDate.Text);
                List<LCB_MaHang> lst = new List<LCB_MaHang>();
                lst = db.LCB_MaHang.Where(x => x.PhongBanID == idToMay && x.Nam == dte.Year && x.Thang == dte.Month && x.DonViID == iddonvi).ToList();
                if (lst != null && lst.Count > 0)
                {
                    ddlMaHang.DataSource = lst;
                    ddlMaHang.DataTextField = "MaHang";
                    ddlMaHang.DataValueField = "MaHang";
                    ddlMaHang.DataBind();

                    ddlMaHang.SelectedValue = lst[0].MaHang;
                }
            }
            catch (Exception ex) { }
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
                string sqlQuery = "[dbo].[pr_LCB_MaHang_KhongBat_LuyKeBTP_SelectAll] @DonViID";
                List<LCB_MaHang_KhongBat_LuyKeBTP_Append> lst = new List<LCB_MaHang_KhongBat_LuyKeBTP_Append>();
                lst = db.Database.SqlQuery<LCB_MaHang_KhongBat_LuyKeBTP_Append>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    gvMaHangNotBTP.DataSource = lst;
                    gvMaHangNotBTP.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Id_BTP", typeof(int)));
                    dt.Columns.Add(new DataColumn("MaHang", typeof(string)));
                    dt.Columns.Add(new DataColumn("PhongBanID", typeof(int)));
                    dt.Columns.Add(new DataColumn("TenPhongban", typeof(string)));
                    dt.Columns.Add(new DataColumn("TenDonVi", typeof(string)));
                    DataRow row = dt.NewRow();
                    row["Id_BTP"] = 0;
                    row["MaHang"] = "";
                    row["PhongBanID"] = 0;
                    row["TenPhongban"] = "";
                    row["TenDonVi"] = "";
                    dt.Rows.Add(row);
                    gvMaHangNotBTP.DataSource = dt;
                    gvMaHangNotBTP.DataBind();
                    gvMaHangNotBTP.Rows[0].Cells.Clear();
                    gvMaHangNotBTP.Rows[0].Cells.Add(new TableCell());
                    gvMaHangNotBTP.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                    gvMaHangNotBTP.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gvMaHangNotBTP.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex) {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Id_BTP", typeof(int)));
                dt.Columns.Add(new DataColumn("MaHang", typeof(string)));
                dt.Columns.Add(new DataColumn("PhongBanID", typeof(int)));
                dt.Columns.Add(new DataColumn("TenPhongban", typeof(string)));
                dt.Columns.Add(new DataColumn("TenDonVi", typeof(string)));
                DataRow row = dt.NewRow();
                row["Id_BTP"] = 0;
                row["MaHang"] = "";
                row["PhongBanID"] = 0;
                row["TenPhongban"] = "";
                row["TenDonVi"] = "";
                dt.Rows.Add(row);
                gvMaHangNotBTP.DataSource = dt;
                gvMaHangNotBTP.DataBind();
                gvMaHangNotBTP.Rows[0].Cells.Clear();
                gvMaHangNotBTP.Rows[0].Cells.Add(new TableCell());
                gvMaHangNotBTP.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                gvMaHangNotBTP.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gvMaHangNotBTP.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
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
            catch(Exception ex) { }
        }

        protected void ddlToMay_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Load_ddlMaHang();
                LoadDataGrid();
            }
            catch (Exception ex) { }
        }

        protected void gvMaHangNotBTP_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                LCB_MaHang_KhongBat_LuyKeBTP cls = new LCB_MaHang_KhongBat_LuyKeBTP();
                int id = Convert.ToInt32(gvMaHangNotBTP.DataKeys[e.RowIndex].Value.ToString());
                cls = db.LCB_MaHang_KhongBat_LuyKeBTP.Where(x => x.Id_BTP == id).SingleOrDefault();
                db.LCB_MaHang_KhongBat_LuyKeBTP.Remove(cls);
                int sus = db.SaveChanges();
                if(sus != 0)
                {
                    divMesssenger.Style["display"] = "block";
                    lblMessenger.Text = "Đã xóa mã hàng!";
                }
                LoadDataGrid();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string m_MaHang = ddlMaHang.SelectedItem.Text;
                int m_PhongBanID = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                LCB_MaHang_KhongBat_LuyKeBTP cls_chk = new LCB_MaHang_KhongBat_LuyKeBTP();
                cls_chk = db.LCB_MaHang_KhongBat_LuyKeBTP.Where(x => x.MaHang == m_MaHang && x.PhongBanID == m_PhongBanID).SingleOrDefault();
                if (cls_chk != null && cls_chk.Id_BTP > 0)
                {
                    divMesssenger.Style["display"] = "block";
                    lblMessenger.Text = "Đã tồn tại mã hàng thuộc bộ phận này!";
                }
                else
                {
                    LCB_MaHang_KhongBat_LuyKeBTP cls = new LCB_MaHang_KhongBat_LuyKeBTP();
                    cls.MaHang = ddlMaHang.SelectedItem.Text;
                    cls.PhongBanID = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                    cls.Ngay_Lap = DateTime.Now;
                    cls.Nguoi_Lap = Session["username"].ToString();
                    cls.Ngay_CapNhat = DateTime.Now;
                    cls.Nguoi_CapNhat = Session["username"].ToString();
                    cls.TonTai = true;
                    db.LCB_MaHang_KhongBat_LuyKeBTP.Add(cls);
                    int sus = db.SaveChanges();
                    if (sus != 0)
                    {
                        divMesssenger.Style["display"] = "block";
                        lblMessenger.Text = "Đã thêm mới mã hàng bỏ BTP!";
                    }
                    LoadDataGrid();
                }
            }
            catch(Exception ex) { }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            Load_ddlMaHang();
        }

        protected void gvMaHangNotBTP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //ImageButton imgbtn = (ImageButton)e.Row.FindControl("cmdDelete");
                //if (!Session["username"].ToString().Equals("admin"))
                //    imgbtn.Enabled = false;
                //else
                //    imgbtn.Enabled = true;
            }
        }
    }
}