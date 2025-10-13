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

namespace TNGLuong
{
    public partial class TongHopCongDoan : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        string strPreviousRowID = string.Empty;
        int intSubTotalIndex = 1;
        string strPreviousRowID_nk = string.Empty;
        int intSubTotalIndex_nk = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new TNG_CTLDbContact();
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Equals(("true").ToUpper()))
                    {
                        Load_ddlThang();
                        if (ddlThang.SelectedValue != null && ddlThang.SelectedValue.ToString() != "")
                        {
                            loadDataGridToMay();
                            loadDataGridNhayKhau();
                        }
                    }
                    else
                    {
                        txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        if (!string.IsNullOrEmpty(txtDate.Text))
                        {
                            loadDataGridToMay();
                            loadDataGridNhayKhau();
                        }
                    }
                }
            }
        }

        private void Load_ddlThang()
        {
            List<clsThang> lst = new List<clsThang>();
            for (int i = 1; i <= 12; i++)
            {
                clsThang cls = new clsThang();
                cls.TenThang = "Tháng " + i.ToString();
                cls.ThangID = i;
                lst.Add(cls);
            }
            if (lst != null && lst.Count > 0)
            {
                ddlThang.DataSource = lst;
                ddlThang.DataBind();
                ddlThang.SelectedValue = DateTime.Now.Month + "";
            }
        }

        protected void loadDataGridToMay()
        {
            try
            {
                int phongid_ns = 0;
                int mansid = 0;
                if (Session["userid"] != null)
                    mansid = Convert.ToInt32(Session["userid"].ToString());
                if (Session["PhongBanID"] != null)
                    phongid_ns = Convert.ToInt32(Session["PhongBanID"].ToString());
                string sqlQuery = "";
                List<TongHopCD> lst = new List<TongHopCD>();

                if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Equals(("true").ToUpper()))
                {
                    int iThang = 0;
                    if (ddlThang.SelectedValue != null && ddlThang.SelectedValue.ToString() != "")
                    {
                        iThang = Convert.ToInt32(ddlThang.SelectedValue.ToString());
                    }
                    object[] sqlPr =
                    {
                        new SqlParameter("@PhongBanID_NS", phongid_ns),
                        new SqlParameter("@MaNS_ID", mansid),
                        new SqlParameter("@iThang", iThang)
                    };
                    sqlQuery = "[dbo].[prDG_Web_TongHopCongDoan_ToMay] @PhongBanID_NS,@MaNS_ID,@iThang";
                    lst = db.Database.SqlQuery<TongHopCD>(sqlQuery, sqlPr).ToList();
                }
                else
                {
                    DateTime dte = Convert.ToDateTime(txtDate.Text);
                    object[] sqlPr =
                    {
                        new SqlParameter("@PhongBanID_NS", phongid_ns),
                        new SqlParameter("@MaNS_ID", mansid),
                        new SqlParameter("@Ngay", DateTime.Parse(txtDate.Text).Date)
                    };
                    sqlQuery = "[dbo].[pr_Web_TongHopCongDoan_ToMay] @PhongBanID_NS,@MaNS_ID,@Ngay";
                    lst = db.Database.SqlQuery<TongHopCD>(sqlQuery, sqlPr).ToList();                    
                }                
                
                if (lst != null && lst.Count > 0)
                {

                    gridNangSuatToMay.DataSource = lst;
                    gridNangSuatToMay.DataBind();
                }
                else
                {
                    for (int i = 0; i < gridNangSuatToMay.Rows.Count; i++)
                    {
                        gridNangSuatToMay.Rows[i].Cells.Clear();
                    }
                    DataTable dt = ultils.CreateDataTable<TongHopCD>(lst);
                    dt.Rows.Add(dt.NewRow());
                    gridNangSuatToMay.DataSource = dt;
                    gridNangSuatToMay.DataBind();
                    gridNangSuatToMay.Rows[0].Cells.Clear();
                    gridNangSuatToMay.Rows[0].Cells.Add(new TableCell());
                    gridNangSuatToMay.Rows[0].Cells[0].ColumnSpan = 5;
                    gridNangSuatToMay.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridNangSuatToMay.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                for (int i = 0; i < gridNangSuatToMay.Rows.Count; i++)
                {
                    gridNangSuatToMay.Rows[i].Cells.Clear();
                }
                List<TongHopCD> lst = new List<TongHopCD>();
                DataTable dt = ultils.CreateDataTable<TongHopCD>(lst);
                dt.Rows.Add(dt.NewRow());
                gridNangSuatToMay.DataSource = dt;
                gridNangSuatToMay.DataBind();
                gridNangSuatToMay.Rows[0].Cells.Clear();
                gridNangSuatToMay.Rows[0].Cells.Add(new TableCell());
                gridNangSuatToMay.Rows[0].Cells[0].ColumnSpan = 5;
                gridNangSuatToMay.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gridNangSuatToMay.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void loadDataGridNhayKhau()
        {
            try
            {
                int phongid_ns = 0;
                int mansid = 0;
                if (Session["userid"] != null)
                    mansid = Convert.ToInt32(Session["userid"].ToString());                
                if (Session["PhongBanID"] != null)
                    phongid_ns = Convert.ToInt32(Session["PhongBanID"].ToString());
                string sqlQuery = "";
                List<TongHopCD> lst = new List<TongHopCD>();

                if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Equals(("true").ToUpper()))
                {
                    int iThang = 0;
                    if (ddlThang.SelectedValue != null && ddlThang.SelectedValue.ToString() != "")
                    {
                        iThang = Convert.ToInt32(ddlThang.SelectedValue.ToString());
                    }
                    object[] sqlPr =
                    {
                        new SqlParameter("@PhongBanID_NS", phongid_ns),
                        new SqlParameter("@MaNS_ID", mansid),
                        new SqlParameter("@iThang", iThang)
                    };
                    sqlQuery = "[dbo].[prDG_Web_TongHopCongDoan_ToMay] @PhongBanID_NS,@MaNS_ID,@iThang";
                    lst = db.Database.SqlQuery<TongHopCD>(sqlQuery, sqlPr).ToList();
                }
                else
                {
                    DateTime dte = Convert.ToDateTime(txtDate.Text);
                    object[] sqlPr =
                    {
                        new SqlParameter("@PhongBanID_NS", phongid_ns),
                        new SqlParameter("@MaNS_ID", mansid),
                        new SqlParameter("@Ngay", DateTime.Parse(txtDate.Text).Date),
                    };
                    sqlQuery = "[dbo].[pr_Web_TongHopCongDoan_NhayKhau] @PhongBanID_NS,@MaNS_ID,@Ngay";
                    lst = db.Database.SqlQuery<TongHopCD>(sqlQuery, sqlPr).ToList();                    
                }                
                
                if (lst != null && lst.Count > 0)
                {

                    gridNangSuatNhayKhau.DataSource = lst;
                    gridNangSuatNhayKhau.DataBind();
                }
                else
                {
                    for (int i = 0; i < gridNangSuatNhayKhau.Rows.Count; i++)
                    {
                        gridNangSuatNhayKhau.Rows[i].Cells.Clear();
                    }
                    DataTable dt = ultils.CreateDataTable<TongHopCD>(lst);
                    dt.Rows.Add(dt.NewRow());
                    gridNangSuatNhayKhau.DataSource = dt;
                    gridNangSuatNhayKhau.DataBind();
                    gridNangSuatNhayKhau.Rows[0].Cells.Clear();
                    gridNangSuatNhayKhau.Rows[0].Cells.Add(new TableCell());
                    gridNangSuatNhayKhau.Rows[0].Cells[0].ColumnSpan = 5;
                    gridNangSuatNhayKhau.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridNangSuatNhayKhau.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                for (int i = 0; i < gridNangSuatNhayKhau.Rows.Count; i++)
                {
                    gridNangSuatNhayKhau.Rows[i].Cells.Clear();
                }
                List<TongHopCD> lst = new List<TongHopCD>();
                DataTable dt = ultils.CreateDataTable<TongHopCD>(lst);
                dt.Rows.Add(dt.NewRow());
                gridNangSuatNhayKhau.DataSource = dt;
                gridNangSuatNhayKhau.DataBind();
                gridNangSuatNhayKhau.Rows[0].Cells.Clear();
                gridNangSuatNhayKhau.Rows[0].Cells.Add(new TableCell());
                gridNangSuatNhayKhau.Rows[0].Cells[0].ColumnSpan = 5;
                gridNangSuatNhayKhau.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gridNangSuatNhayKhau.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDate.Text) && DateTime.Compare(DateTime.Parse(txtDate.Text), DateTime.Parse("2020-07-31")) > 0 && DateTime.Compare(DateTime.Parse(txtDate.Text), DateTime.Today) <= 0)
            {
                loadDataGridToMay();
                loadDataGridNhayKhau();
            }
        }

        protected void gridNangSuatToMay_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaHang_ID") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "MaHang_ID").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaHang_ID") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaHang_ID") != null))
            {
                GridView grdViewOrders = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "MaHang").ToString();
                cell.ColumnSpan = 5;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }
            #endregion
            if (IsSubTotalRowNeedToAdd)
            {
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "MaHang_ID") != null)
                {
                    GridView grdViewOrders = (GridView)sender;
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    TableCell cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "MaHang").ToString();
                    cell.ColumnSpan = 5;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
            }
        }

        protected void gridNangSuatToMay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "MaHang_ID").ToString();
            }
        }

        protected void gridNangSuatNhayKhau_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            if ((strPreviousRowID_nk != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaHang_ID") != null))
                if (strPreviousRowID_nk != DataBinder.Eval(e.Row.DataItem, "MaHang_ID").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID_nk != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaHang_ID") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                intSubTotalIndex_nk = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID_nk == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaHang_ID") != null))
            {
                GridView grdViewOrders = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "MaHang").ToString();
                cell.ColumnSpan = 5;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex_nk, row);
                intSubTotalIndex_nk++;
            }
            #endregion
            if (IsSubTotalRowNeedToAdd)
            {
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "MaHang_ID") != null)
                {
                    GridView grdViewOrders = (GridView)sender;
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    TableCell cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "MaHang").ToString();
                    cell.ColumnSpan = 5;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex_nk, row);
                    intSubTotalIndex_nk++;
                }
                #endregion
            }
        }

        protected void gridNangSuatNhayKhau_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID_nk = DataBinder.Eval(e.Row.DataItem, "MaHang_ID").ToString();
            }
        }

        protected void ddlThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlThang.SelectedValue != null && ddlThang.SelectedValue.ToString() != "")
            {
                loadDataGridToMay();
                loadDataGridNhayKhau();
            }
        }
    }
}