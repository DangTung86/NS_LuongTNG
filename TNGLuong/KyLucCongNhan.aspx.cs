using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Models;

namespace TNGLuong
{
    public partial class KyLucCongNhan : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new TNG_CTLDbContact();
            if (Session["username"] != null) 
            {
                if (!IsPostBack)
                {
                    btnTuanNay.Attributes["class"] = "active";
                    tabTuanNay.Attributes["class"] = "tab-pane fade in active";
                    btnTuanTrc.Attributes["class"] = "";
                    tabTuanTruoc.Attributes["class"] = "tab-pane fade";
                    btnThangNay.Attributes["class"] = "";
                    tabThangNay.Attributes["class"] = "tab-pane fade";
                    btnThangTrc.Attributes["class"] = "";
                    tabThangTruoc.Attributes["class"] = "tab-pane fade";
                    Load_ddlDonVi();
                    Load_gvTuanNay(); 
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Load_ddlDonVi()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("IDTimKiem", typeof(int)));
            dt.Columns.Add(new DataColumn("TimKiem", typeof(string)));
            DataRow dr = dt.NewRow();
            dr["IDTimKiem"] = 1;
            dr["TimKiem"] = "Bộ phận";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["IDTimKiem"] = 2;
            dr["TimKiem"] = "Chi nhánh";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["IDTimKiem"] = 3;
            dr["TimKiem"] = "Toàn công ty";
            dt.Rows.Add(dr);

            cmbDonVi.DataSource = dt;
            cmbDonVi.DataTextField = "TimKiem";
            cmbDonVi.DataValueField = "IDTimKiem";
            cmbDonVi.SelectedValue = "1";
            cmbDonVi.DataBind();

            cmbDonViTuanTrc.DataSource = dt;
            cmbDonViTuanTrc.DataTextField = "TimKiem";
            cmbDonViTuanTrc.DataValueField = "IDTimKiem";
            cmbDonViTuanTrc.SelectedValue = "1";
            cmbDonViTuanTrc.DataBind();

            cmbDonViThang.DataSource = dt;
            cmbDonViThang.DataTextField = "TimKiem";
            cmbDonViThang.DataValueField = "IDTimKiem";
            cmbDonViThang.SelectedValue = "1";
            cmbDonViThang.DataBind();

            cmbDonViThangTrc.DataSource = dt;
            cmbDonViThangTrc.DataTextField = "TimKiem";
            cmbDonViThangTrc.DataValueField = "IDTimKiem";
            cmbDonViThangTrc.SelectedValue = "1";
            cmbDonViThangTrc.DataBind();
        }

        #region "Load Data gv"
        protected void Load_gvTuanNay()
        {
            int donviid = 0;
            int phongid = 0;
            if (cmbDonVi.SelectedValue != null && cmbDonVi.SelectedValue.ToString() != "")
            {
                string sTimKiem = cmbDonVi.SelectedValue.ToString();
                if (sTimKiem == "1")
                    phongid = Convert.ToInt32(Session["PhongBanID"].ToString()); 
                else if(sTimKiem == "2")
                    donviid = Convert.ToInt32(Session["DonViID_Cha"].ToString());
                else if (sTimKiem == "3")
                {
                    donviid = 0;
                    phongid = 0;
                }
            }
            string susername = "";
            if (Session["username"] != null)
                susername = Session["username"].ToString();
            object[] sqlPr =
            {
                new SqlParameter("@sMaNS", susername),
                new SqlParameter("@iDonViID", donviid),
                new SqlParameter("@iPhongBanID", phongid)
            };
            string sqlQuery = "[dbo].[pr_Web_LCB_LuongNgayCongNhan_LayDanhSachKyLuc_Luong_Tuan] @sMaNS,@iDonViID,@iPhongBanID";
            List<clsKyLucCaNhan> lst = new List<clsKyLucCaNhan>();
            lst = db.Database.SqlQuery<clsKyLucCaNhan>(sqlQuery, sqlPr).ToList();
            if (lst != null && lst.Count > 0)
            {
                gvTuanNay.DataSource = lst;
                gvTuanNay.DataBind();
                clsKyLucCaNhan cls = new clsKyLucCaNhan();
                cls = lst.Where(x => x.TrangThai == 1).SingleOrDefault();
                if (cls != null)
                    lblXepHangTN.Text = "Xếp hạng: " + cls.SoTT + "/" + cls.TongNS;
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("SoTT", typeof(Int64)));
                dt.Columns.Add(new DataColumn("HoTenTop", typeof(string)));
                dt.Columns.Add(new DataColumn("TongTienLuong", typeof(decimal)));
                dt.Columns.Add(new DataColumn("TrangThai", typeof(int)));
                DataRow row = dt.NewRow();
                row["SoTT"] = 0;
                row["HoTenTop"] = "";
                row["TongTienLuong"] = 0;
                row["TrangThai"] = 0;
                dt.Rows.Add(row);
                gvTuanNay.DataSource = dt;
                gvTuanNay.DataBind();
                gvTuanNay.Rows[0].Cells.Clear();
                gvTuanNay.Rows[0].Cells.Add(new TableCell());
                gvTuanNay.Rows[0].Cells[0].ColumnSpan = 4;
                gvTuanNay.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gvTuanNay.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void Load_gvTuanTrc()
        {
            int donviid = 0;
            int phongid = 0;
            if (cmbDonViTuanTrc.SelectedValue != null && cmbDonViTuanTrc.SelectedValue.ToString() != "")
            {
                string sTimKiem = cmbDonViTuanTrc.SelectedValue.ToString();
                if (sTimKiem == "1")
                    phongid = Convert.ToInt32(Session["PhongBanID"].ToString());
                else if (sTimKiem == "2")
                    donviid = Convert.ToInt32(Session["DonViID_Cha"].ToString());
                else if (sTimKiem == "3")
                {
                    donviid = 0;
                    phongid = 0;
                }
            }
            string susername = "";
            if (Session["username"] != null)
                susername = Session["username"].ToString();
            object[] sqlPr =
            {
                new SqlParameter("@sMaNS", susername),
                new SqlParameter("@iDonViID", donviid),
                new SqlParameter("@iPhongBanID", phongid)
            };
            string sqlQuery = "[dbo].[pr_Web_LCB_LuongNgayCongNhan_LayDanhSachKyLuc_Luong_TuanTruoc] @sMaNS,@iDonViID,@iPhongBanID";
            List<clsKyLucCaNhan> lst = new List<clsKyLucCaNhan>();
            lst = db.Database.SqlQuery<clsKyLucCaNhan>(sqlQuery, sqlPr).ToList();
            if (lst != null && lst.Count > 0)
            {
                gvTuanTrc.DataSource = lst;
                gvTuanTrc.DataBind();
                clsKyLucCaNhan cls = new clsKyLucCaNhan();
                cls = lst.Where(x => x.TrangThai == 1).SingleOrDefault();
                if (cls != null)
                    lblXepHangTC.Text = "Xếp hạng: " + cls.SoTT + "/" + cls.TongNS;
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("SoTT", typeof(Int64)));
                dt.Columns.Add(new DataColumn("HoTenTop", typeof(string)));
                dt.Columns.Add(new DataColumn("TongTienLuong", typeof(decimal)));
                dt.Columns.Add(new DataColumn("TrangThai", typeof(int)));
                DataRow row = dt.NewRow();
                row["SoTT"] = 0;
                row["HoTenTop"] = "";
                row["TongTienLuong"] = 0;
                row["TrangThai"] = 0;
                dt.Rows.Add(row);
                gvTuanTrc.DataSource = dt;
                gvTuanTrc.DataBind();
                gvTuanTrc.Rows[0].Cells.Clear();
                gvTuanTrc.Rows[0].Cells.Add(new TableCell());
                gvTuanTrc.Rows[0].Cells[0].ColumnSpan = 4;
                gvTuanTrc.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gvTuanTrc.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void Load_gvThang()
        {
            int donviid = 0;
            int phongid = 0;
            if (cmbDonViThang.SelectedValue != null && cmbDonViThang.SelectedValue.ToString() != "")
            {
                string sTimKiem = cmbDonViThang.SelectedValue.ToString();
                if (sTimKiem == "1")
                    phongid = Convert.ToInt32(Session["PhongBanID"].ToString());
                else if (sTimKiem == "2")
                    donviid = Convert.ToInt32(Session["DonViID_Cha"].ToString());
                else if (sTimKiem == "3")
                {
                    donviid = 0;
                    phongid = 0;
                }
            }
            string susername = "";
            if (Session["username"] != null)
                susername = Session["username"].ToString();
            object[] sqlPr =
            {
                new SqlParameter("@sMaNS", susername),
                new SqlParameter("@iDonViID", donviid),
                new SqlParameter("@iPhongBanID", phongid)
            };
            string sqlQuery = "[dbo].[pr_Web_LCB_LuongNgayCongNhan_LayDanhSachKyLuc_Luong_Thang] @sMaNS,@iDonViID,@iPhongBanID";
            List<clsKyLucCaNhan> lst = new List<clsKyLucCaNhan>();
            lst = db.Database.SqlQuery<clsKyLucCaNhan>(sqlQuery, sqlPr).ToList();
            if (lst != null && lst.Count > 0)
            {
                gvThang.DataSource = lst;
                gvThang.DataBind();
                clsKyLucCaNhan cls = new clsKyLucCaNhan();
                cls = lst.Where(x => x.TrangThai == 1).SingleOrDefault();
                if (cls != null)
                    lblXepHangThgN.Text = "Xếp hạng: " + cls.SoTT + "/" + cls.TongNS;
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("SoTT", typeof(Int64)));
                dt.Columns.Add(new DataColumn("HoTenTop", typeof(string)));
                dt.Columns.Add(new DataColumn("TongTienLuong", typeof(decimal)));
                dt.Columns.Add(new DataColumn("TrangThai", typeof(int)));
                DataRow row = dt.NewRow();
                row["SoTT"] = 0;
                row["HoTenTop"] = "";
                row["TongTienLuong"] = 0;
                row["TrangThai"] = 0;
                dt.Rows.Add(row);
                gvThang.DataSource = dt;
                gvThang.DataBind();
                gvThang.Rows[0].Cells.Clear();
                gvThang.Rows[0].Cells.Add(new TableCell());
                gvThang.Rows[0].Cells[0].ColumnSpan = 4;
                gvThang.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gvThang.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void Load_gvThangTrc()
        {
            int donviid = 0;
            int phongid = 0;
            if (cmbDonViThangTrc.SelectedValue != null && cmbDonViThangTrc.SelectedValue.ToString() != "")
            {
                string sTimKiem = cmbDonViThangTrc.SelectedValue.ToString();
                if (sTimKiem == "1")
                    phongid = Convert.ToInt32(Session["PhongBanID"].ToString());
                else if (sTimKiem == "2")
                    donviid = Convert.ToInt32(Session["DonViID_Cha"].ToString());
                else if (sTimKiem == "3")
                {
                    donviid = 0;
                    phongid = 0;
                }
            }
            string susername = "";
            if (Session["username"] != null)
                susername = Session["username"].ToString();
            object[] sqlPr =
            {
                new SqlParameter("@sMaNS", susername),
                new SqlParameter("@iDonViID", donviid),
                new SqlParameter("@iPhongBanID", phongid)
            };
            string sqlQuery = "[dbo].[pr_Web_LCB_LuongNgayCongNhan_LayDanhSachKyLuc_Luong_ThangTruoc] @sMaNS,@iDonViID,@iPhongBanID";
            List<clsKyLucCaNhan> lst = new List<clsKyLucCaNhan>();
            lst = db.Database.SqlQuery<clsKyLucCaNhan>(sqlQuery, sqlPr).ToList();
            if (lst != null && lst.Count > 0)
            {
                gvThangTrc.DataSource = lst;
                gvThangTrc.DataBind();
                clsKyLucCaNhan cls = new clsKyLucCaNhan();
                cls = lst.Where(x => x.TrangThai == 1).SingleOrDefault();
                if (cls != null)
                    lblXepHangThgC.Text = "Xếp hạng: " + cls.SoTT + "/" + cls.TongNS;
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("SoTT", typeof(Int64)));
                dt.Columns.Add(new DataColumn("HoTenTop", typeof(string)));
                dt.Columns.Add(new DataColumn("TongTienLuong", typeof(decimal)));
                dt.Columns.Add(new DataColumn("TrangThai", typeof(int)));
                DataRow row = dt.NewRow();
                row["SoTT"] = 0;
                row["HoTenTop"] = "";
                row["TongTienLuong"] = 0;
                row["TrangThai"] = 0;
                dt.Rows.Add(row);
                gvThangTrc.DataSource = dt;
                gvThangTrc.DataBind();
                gvThangTrc.Rows[0].Cells.Clear();
                gvThangTrc.Rows[0].Cells.Add(new TableCell());
                gvThangTrc.Rows[0].Cells[0].ColumnSpan = 4;
                gvThangTrc.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gvThangTrc.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }
        #endregion

        #region "cmb_SelectedIndexChanged"
        protected void cmbDonVi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_gvTuanNay();
        }     

        protected void cmbDonViTuanTrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_gvTuanTrc();
        }

        protected void cmbDonViThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_gvThang();
        }

        protected void cmbDonViThangTrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_gvThangTrc();
        }
        #endregion

        #region "GV ONRowDataBound"
        protected void gvTuanNay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblTrangThai");
                if (lbl != null && lbl.Text == "1")
                {
                    e.Row.Font.Bold = true;
                    e.Row.BackColor = Color.Red;
                }
            }
        }

        protected void gvTuanTrc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblTrangThaiTrc");
                if (lbl != null && lbl.Text == "1")
                {
                    e.Row.Font.Bold = true;
                    e.Row.BackColor = Color.Red;
                }
            }
        }

        protected void gvThang_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblTrangThaiThang");
                if (lbl != null && lbl.Text == "1")
                {
                    e.Row.Font.Bold = true;
                    e.Row.BackColor = Color.Red;
                }
            }
        }

        protected void gvThangTrc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblTrangThaiThangTrc");
                if (lbl != null && lbl.Text == "1")
                {
                    e.Row.Font.Bold = true;
                    e.Row.BackColor = Color.Red;
                }
            }
        }
        #endregion

        #region "LinkButton Onclick"
        protected void btnTuanNay_Click(object sender, EventArgs e)
        {
            Load_gvTuanNay();
            btnTuanNay.Attributes["class"] = "active";
            tabTuanNay.Attributes["class"] = "tab-pane fade in active";
            btnTuanTrc.Attributes["class"] = "";
            tabTuanTruoc.Attributes["class"] = "tab-pane fade";
            btnThangNay.Attributes["class"] = "";
            tabThangNay.Attributes["class"] = "tab-pane fade";
            btnThangTrc.Attributes["class"] = "";
            tabThangTruoc.Attributes["class"] = "tab-pane fade";
        }

        protected void btnTuanTrc_Click(object sender, EventArgs e)
        {
            Load_gvTuanTrc();
            btnTuanNay.Attributes["class"] = "";
            tabTuanNay.Attributes["class"] = "tab-pane fade";
            btnTuanTrc.Attributes["class"] = "active";
            tabTuanTruoc.Attributes["class"] = "tab-pane fade in active";
            btnThangNay.Attributes["class"] = "";
            tabThangNay.Attributes["class"] = "tab-pane fade";
            btnThangTrc.Attributes["class"] = "";
            tabThangTruoc.Attributes["class"] = "tab-pane fade";
        }

        protected void btnThangNay_Click(object sender, EventArgs e)
        {
            Load_gvThang();
            btnTuanNay.Attributes["class"] = "";
            tabTuanNay.Attributes["class"] = "tab-pane fade";
            btnTuanTrc.Attributes["class"] = "";
            tabTuanTruoc.Attributes["class"] = "tab-pane fade";
            btnThangNay.Attributes["class"] = "active";
            tabThangNay.Attributes["class"] = "tab-pane fade in active";
            btnThangTrc.Attributes["class"] = "";
            tabThangTruoc.Attributes["class"] = "tab-pane fade";
        }

        protected void btnThangTrc_Click(object sender, EventArgs e)
        {
            Load_gvThangTrc();
            btnTuanNay.Attributes["class"] = "";
            tabTuanNay.Attributes["class"] = "tab-pane fade";
            btnTuanTrc.Attributes["class"] = "";
            tabTuanTruoc.Attributes["class"] = "tab-pane fade";
            btnThangNay.Attributes["class"] = "";
            tabThangNay.Attributes["class"] = "tab-pane fade";
            btnThangTrc.Attributes["class"] = "active";
            tabThangTruoc.Attributes["class"] = "tab-pane fade in active";
        }
        #endregion
    }
}