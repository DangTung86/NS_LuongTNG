using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Models;
using TNGLuong.ModelsView;

namespace TNGLuong.WebAdmin.production
{
    public partial class IsPayroll : System.Web.UI.UserControl
    {
        TNG_CTLDbContact db = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Khóa bảng lương đơn vị";
            db = new TNG_CTLDbContact();
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
                string sqlQuery = "pr_NSTL_DM_DonVi_LayDanhSachDonViCha";
                List<clsDM_DonVi> lst = new List<clsDM_DonVi>();
                lst = db.Database.SqlQuery<clsDM_DonVi>(sqlQuery).ToList();                
                if (lst != null && lst.Count > 0)
                {
                    ddlDonVi.DataSource = lst;
                    ddlDonVi.DataTextField = "TenDonVi";
                    ddlDonVi.DataValueField = "DonViID";
                    ddlDonVi.DataBind();
                    //if (!Session["username"].ToString().Equals("admin"))
                    //{
                    //    if (!Session["DonViID_Cha"].ToString().Equals("65") && !Session["DonViID_Cha"].ToString().Equals("138") && !Session["DonViID_Cha"].ToString().Equals("139"))
                    //    {
                    //        ddlDonVi.SelectedValue = Session["DonViID_Cha"].ToString();
                    //        ddlDonVi.Enabled = false;
                    //    }
                    //    else
                    //    {
                    //        ddlDonVi.Enabled = true;
                    //    }
                    //}                    
                    //else
                    //{
                    //    ddlDonVi.Enabled = true;
                    //}
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
                    new SqlParameter("@iErrorCode", 1)
                };
                string sqlQuery = "[dbo].[pr_LCB_WEB_KhoaBangLuong_Select_Data_Grid] @iErrorCode";
                List<LCB_WEB_KhoaBangLuong_Append> lst = new List<LCB_WEB_KhoaBangLuong_Append>();
                lst = db.Database.SqlQuery<LCB_WEB_KhoaBangLuong_Append>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    gvKhoaBLg.DataSource = lst;
                    gvKhoaBLg.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("DonViID", typeof(int)));
                    dt.Columns.Add(new DataColumn("TenDonVi", typeof(string)));
                    dt.Columns.Add(new DataColumn("TinhTrang", typeof(string)));
                    dt.Columns.Add(new DataColumn("HoTen", typeof(string)));
                    dt.Columns.Add(new DataColumn("Ngay_IsKhoa", typeof(string)));

                    DataRow row = dt.NewRow();
                    row["DonViID"] = 0;
                    row["TenDonVi"] = "";
                    row["TinhTrang"] = "";
                    row["HoTen"] = "";
                    row["Ngay_IsKhoa"] = "";
                    dt.Rows.Add(row);
                    gvKhoaBLg.DataSource = dt;
                    gvKhoaBLg.DataBind();
                    gvKhoaBLg.Rows[0].Cells.Clear();
                    gvKhoaBLg.Rows[0].Cells.Add(new TableCell());
                    gvKhoaBLg.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                    gvKhoaBLg.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gvKhoaBLg.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("DonViID", typeof(int)));
                dt.Columns.Add(new DataColumn("TenDonVi", typeof(string)));
                dt.Columns.Add(new DataColumn("TinhTrang", typeof(string)));
                dt.Columns.Add(new DataColumn("HoTen", typeof(string)));
                dt.Columns.Add(new DataColumn("Ngay_IsKhoa", typeof(string)));

                DataRow row = dt.NewRow();
                row["DonViID"] = 0;
                row["TenDonVi"] = "";
                row["TinhTrang"] = "";
                row["HoTen"] = "";
                row["Ngay_IsKhoa"] = "";
                dt.Rows.Add(row);
                gvKhoaBLg.DataSource = dt;
                gvKhoaBLg.DataBind();
                gvKhoaBLg.Rows[0].Cells.Clear();
                gvKhoaBLg.Rows[0].Cells.Add(new TableCell());
                gvKhoaBLg.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                gvKhoaBLg.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gvKhoaBLg.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                int sus = 0;
                int donviid = 0;
                if (ddlDonVi.SelectedValue != null && ddlDonVi.SelectedValue.ToString() != "")
                    donviid = int.Parse(ddlDonVi.SelectedValue.ToString());
                LCB_WEB_KhoaBangLuong cls = new LCB_WEB_KhoaBangLuong();
                cls = db.LCB_WEB_KhoaBangLuong.Where(x => x.DonViID == donviid).FirstOrDefault();
                if (cls != null)
                {
                    sus = 1;
                }
                else
                {
                    cls = new LCB_WEB_KhoaBangLuong();
                    cls.DonViID = donviid;
                    cls.KhoaBlg = true;
                    cls.NguoiKhoa = Session["username"].ToString();
                    cls.NgayKhoa = DateTime.Now;
                    cls.TonTai = true;
                    db.LCB_WEB_KhoaBangLuong.Add(cls);
                    sus = db.SaveChanges();
                }
                LoadDataGrid();
            }
            catch (Exception ex) { }
        }

        protected void gvKhoaBLg_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvKhoaBLg_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                LCB_WEB_KhoaBangLuong cls = new LCB_WEB_KhoaBangLuong();
                int m_iDonViID = Convert.ToInt32(gvKhoaBLg.DataKeys[e.RowIndex].Value.ToString());
                cls = db.LCB_WEB_KhoaBangLuong.Where(x => x.DonViID == m_iDonViID).SingleOrDefault();
                if (cls != null)
                {
                    db.LCB_WEB_KhoaBangLuong.Remove(cls);
                    db.SaveChanges();
                }
                LoadDataGrid();
            }
            catch (Exception ex)
            {

            }
        }
    }
}