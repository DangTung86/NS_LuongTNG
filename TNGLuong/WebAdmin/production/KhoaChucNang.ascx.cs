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
    public partial class KhoaChucNang : System.Web.UI.UserControl
    {
        TNG_CTLDbContact db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Cập nhật danh sách tổ trưởng";
            db = new TNG_CTLDbContact();
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    lblMessenger.Text = "";
                    Load_ddlDonVi();
                    Load_chkListChucNang();
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
                }
            }
            catch (Exception ex) { }
        }

        protected void Load_chkListChucNang()
        {
            List<LCB_WEB_ChucNang> lstChucNang = new List<LCB_WEB_ChucNang>();
            lstChucNang = db.LCB_WEB_ChucNang.Where(x => x.ID_NhomChuNang == 2).OrderBy(x => x.TenChucNang).ToList();
            if (lstChucNang != null && lstChucNang.Count > 0)
            {
                chkList_ChucNang.DataSource = lstChucNang;
                chkList_ChucNang.DataTextField = "TenChucNang";
                chkList_ChucNang.DataValueField = "ID_ChucNang";
                chkList_ChucNang.DataBind();
            }
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
                    new SqlParameter("@iDonViID", donviid)
                };
                string sqlQuery = "[dbo].[pr_LCB_WEB_KhoaChucNang_SelectMain_wDonViID] @iDonViID";
                List<LCB_WEB_KhoaChucNang_Append> lst = new List<LCB_WEB_KhoaChucNang_Append>();
                lst = db.Database.SqlQuery<LCB_WEB_KhoaChucNang_Append>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    gvKhoaChucNang.DataSource = lst;
                    gvKhoaChucNang.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("ID_KhoaChucNang", typeof(int)));
                    dt.Columns.Add(new DataColumn("TenDonVi", typeof(string)));
                    dt.Columns.Add(new DataColumn("TenChucNang", typeof(string)));

                    DataRow row = dt.NewRow();
                    row["ID_KhoaChucNang"] = 0;
                    row["TenDonVi"] = "";
                    row["TenChucNang"] = "";
                    dt.Rows.Add(row);
                    gvKhoaChucNang.DataSource = dt;
                    gvKhoaChucNang.DataBind();
                    gvKhoaChucNang.Rows[0].Cells.Clear();
                    gvKhoaChucNang.Rows[0].Cells.Add(new TableCell());
                    gvKhoaChucNang.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count+1;
                    gvKhoaChucNang.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gvKhoaChucNang.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("ID_KhoaChucNang", typeof(int)));
                dt.Columns.Add(new DataColumn("TenDonVi", typeof(string)));
                dt.Columns.Add(new DataColumn("TenChucNang", typeof(string)));

                DataRow row = dt.NewRow();
                row["ID_KhoaChucNang"] = 0;
                row["TenDonVi"] = "";
                row["TenChucNang"] = "";
                dt.Rows.Add(row);
                gvKhoaChucNang.DataSource = dt;
                gvKhoaChucNang.DataBind();
                gvKhoaChucNang.Rows[0].Cells.Clear();
                gvKhoaChucNang.Rows[0].Cells.Add(new TableCell());
                gvKhoaChucNang.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count+1;
                gvKhoaChucNang.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gvKhoaChucNang.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void ddlDonVi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string idDonVi = ddlDonVi.SelectedValue.ToString();
                LoadDataGrid();
            }
            catch (Exception ex) { }
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDonVi.SelectedValue == null || ddlDonVi.SelectedValue.ToString() == "")
                {
                    divMesssenger.Style["display"] = "block";
                    lblMessenger.Text = "Chưa chọn đơn vị!";
                    return;
                }
                else if (chkList_ChucNang.Items.Count <= 0) 
                {
                    divMesssenger.Style["display"] = "block";
                    lblMessenger.Text = "Chưa chọn chức năng cần khóa!";
                    return;
                }
                else
                {
                    LCB_WEB_KhoaChucNang clsKhoaCN = new LCB_WEB_KhoaChucNang();
                    for (int i = 0; i < chkList_ChucNang.Items.Count; i++)
                    {
                        if (chkList_ChucNang.Items[i].Selected == true)
                        {
                            clsKhoaCN = new LCB_WEB_KhoaChucNang();
                            clsKhoaCN.DonViID = int.Parse(ddlDonVi.SelectedValue.ToString());
                            clsKhoaCN.ID_ChucNang = int.Parse(chkList_ChucNang.Items[i].Value.ToString());
                            db.LCB_WEB_KhoaChucNang.Add(clsKhoaCN);
                            db.SaveChanges();
                        }
                    }
                    LoadDataGrid();
                }
            }
            catch (Exception ex) { }
        }

        protected void gvKhoaChucNang_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                LCB_WEB_KhoaChucNang cls = new LCB_WEB_KhoaChucNang();
                int ID_KhoaChucNang = Convert.ToInt32(gvKhoaChucNang.DataKeys[e.RowIndex].Value.ToString());
                cls = db.LCB_WEB_KhoaChucNang.Where(x => x.ID_KhoaChucNang == ID_KhoaChucNang).SingleOrDefault();
                if (cls != null)
                    db.LCB_WEB_KhoaChucNang.Remove(cls);
                int sus = db.SaveChanges();
                if (sus != 0)
                {
                    divMesssenger.Style["display"] = "block";
                    lblMessenger.Text = "Đã xóa chức năng!";
                }
                LoadDataGrid();
            }
            catch (Exception ex)
            {

            }
        }
    }
}