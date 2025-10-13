using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TNGLuong.Models;
using TNGLuong.ModelsView;

namespace TNGLuong
{
    public partial class BangTinhLuongCN : System.Web.UI.Page
    {
        string strPreviousRowID = string.Empty;
        int intSubTotalIndex = 1;
        string strPreviousRowID_NK = string.Empty;
        int intSubTotalIndex_NK = 1;
        TNG_CTLDbContact db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl linhapns = (HtmlGenericControl)this.Master.FindControl("nhapNS");
            HtmlGenericControl lithoigiancho = (HtmlGenericControl)this.Master.FindControl("thoigiancho");
            HtmlGenericControl liduyetNS = (HtmlGenericControl)this.Master.FindControl("duyetNS");
            HtmlGenericControl liluongns = (HtmlGenericControl)this.Master.FindControl("luongns");
            HtmlGenericControl libluong = (HtmlGenericControl)this.Master.FindControl("bluong");
            linhapns.Attributes.Add("class", "");
            lithoigiancho.Attributes.Add("class", "");
            liduyetNS.Attributes.Add("class", "");
            liluongns.Attributes.Add("class", "active");
            libluong.Attributes.Add("class", "");
            db = new TNG_CTLDbContact();
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            if (Session["username"] != null)
            {
                lblMaNS.Text = Session["username"].ToString();
                lblDonVi.Text = Session["TenDonVi"].ToString().Split('-')[0];
                lblHoten.Text = Session["fullname"].ToString();
                lblBoPhan.Text = Session["TenPhongBan"].ToString();
                if (!IsPostBack)
                {
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    if (!string.IsNullOrEmpty(txtDate.Text))
                    {
                        loadDataLuongCN();
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void loadDataLuongCN()
        {
            try
            {
                int phongid = 0;
                int mansid = 0;
                if (Session["PhongBanID"] != null && Session["PhongBanID"].ToString() != "")
                {
                    phongid = Convert.ToInt32(Session["PhongBanID"].ToString());
                }
                if (Session["userid"] != null)
                    mansid = Convert.ToInt32(Session["userid"].ToString());
                DateTime dte = DateTime.Parse(txtDate.Text);
                object[] sqlPr =
               {
                    new SqlParameter("@PhongBanID", phongid),
                    new SqlParameter("@MaNS_ID", mansid),
                    new SqlParameter("@Ngay", dte.Date)
                };
                //lương trong ngày
                string sqlQuery = "[dbo].[pr_LCB_LuongNgayCongNhan_WEB_GETData_By_Ngay] @PhongBanID,@MaNS_ID,@Ngay";
                List<LuongCongNhan> lst = new List<LuongCongNhan>();
                lst = db.Database.SqlQuery<LuongCongNhan>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    LuongCongNhan lgcn = lst[0];
                    if (lgcn.PheDuyet == true)
                    {
                        lblTrangThai.Text = "Đã duyệt";
                        lblTrangThai.ForeColor = System.Drawing.Color.Blue;
                    }
                    else
                    {
                        lblTrangThai.Text = "Chưa duyệt";
                        lblTrangThai.ForeColor = System.Drawing.Color.Red;
                    }
                    lblThoiGianLamViec.Text = string.Format("{0:#,0}", lgcn.GioLamViec);
                    TimeSpan times = TimeSpan.FromMinutes(double.Parse(lgcn.GioChoViec.ToString()));
                    lblSoGioNgungViec.Text = string.Format("{0:D2}:{1:D2}", times.Hours, times.Minutes);
                    lblLuongCapBac.Text = string.Format("{0:#,0}", lgcn.LuongCB);
                    lblLuongSP.Text = string.Format("{0:#,0}", lgcn.NangSuat);
                    lblLuongNhayKhau.Text = string.Format("{0:#,0}", lgcn.NhayKhau);
                    lblLuongVuotGK.Text = string.Format("{0:#,0}", lgcn.VuotNangSuat);
                    lblLuongThemGio.Text = string.Format("{0:#,0}", lgcn.LuongThemGio);
                    LuongNgungViec.Text = string.Format("{0:#,0}", lgcn.LuongChoViec);
                    lblTongLuongNgay.Text = string.Format("{0:#,0}", lgcn.TongTienLuong);
                }
                else
                {
                    lblThoiGianLamViec.Text = "0";
                    lblSoGioNgungViec.Text = "0";
                    lblLuongCapBac.Text = "0";
                    lblLuongSP.Text = "0";
                    lblLuongNhayKhau.Text = "0";
                    lblLuongVuotGK.Text = "0";
                    lblLuongThemGio.Text = "0";
                    LuongNgungViec.Text = "0";
                    lblTongLuongNgay.Text = "0";
                }

                //lương lũy kế tháng
                object[] sqlPrthang =
               {
                    new SqlParameter("@PhongBanID", phongid),
                    new SqlParameter("@MaNS_ID", mansid),
                    new SqlParameter("@Ngay", dte.Date)
                };
                string sqlQuerythang = "[dbo].[pr_LCB_LuongNgayCongNhan_WEB_GETData_By_Thang] @PhongBanID,@MaNS_ID,@Ngay";
                List<LuongCongNhan> lstthang = new List<LuongCongNhan>();
                lstthang = db.Database.SqlQuery<LuongCongNhan>(sqlQuerythang, sqlPrthang).ToList();
                if (lstthang != null && lstthang.Count > 0)
                {
                    LuongCongNhan lgcn = lstthang[0];
                    lblLuongSPLK.Text = string.Format("{0:#,0}", lgcn.NangSuat);
                    lblLuongNhayKhauLK.Text = string.Format("{0:#,0}", lgcn.NhayKhau);
                    LuongGiaoKhoanLK.Text = string.Format("{0:#,0}", lgcn.VuotNangSuat);
                    lblLuongThemGioLK.Text = string.Format("{0:#,0}", lgcn.LuongThemGio);
                    lblLuongNgungViecLK.Text = string.Format("{0:#,0}", lgcn.LuongChoViec);
                    lblTongLk.Text = string.Format("{0:#,0}", lgcn.TongTienLuong);
                }
                else
                {
                    lblLuongSPLK.Text = "0";
                    lblLuongNhayKhauLK.Text = "0";
                    LuongGiaoKhoanLK.Text = "0";
                    lblLuongThemGioLK.Text = "0";
                    lblLuongNgungViecLK.Text = "0";
                    lblTongLk.Text = "0";
                }
            }
            catch (Exception ex) { }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDate.Text))
            {
                loadDataLuongCN();
            }
        }

        protected void loadDataGridNangSuat()
        {
            try
            {
                int mansid = 0;
                if (Session["userid"] != null)
                    mansid = Convert.ToInt32(Session["userid"].ToString());
                DateTime dte = DateTime.Parse(txtDate.Text);
                object[] sqlPr =
               {
                    new SqlParameter("@MaNS_ID", mansid),
                    new SqlParameter("@Ngay", dte.Date)
                };
                string sqlQuery = "[dbo].[pr_LCB_KeHoach_NhanVien_WEB_GetDataByMaNSID_Ngay] @MaNS_ID,@Ngay";
                List<NangSuatNgayCongNhan> lst = new List<NangSuatNgayCongNhan>();
                lst = db.Database.SqlQuery<NangSuatNgayCongNhan>(sqlQuery, sqlPr).ToList();
                //DataTable dt = ultils.CreateDataTable<NangSuatCongNhan>(lst);
                if (lst != null && lst.Count > 0)
                {
                    gridNangSuat.DataSource = lst;
                    gridNangSuat.DataBind();

                    decimal totalTT = 0;
                    decimal totalTL = 0;
                    gridNangSuat.FooterRow.Cells[0].Text = "Tổng: ";
                    gridNangSuat.FooterRow.Cells[0].Font.Bold = true;
                    gridNangSuat.FooterRow.Cells[0].ColumnSpan = 6;
                    gridNangSuat.FooterRow.Cells[1].Visible = false;
                    gridNangSuat.FooterRow.Cells[2].Visible = false;
                    gridNangSuat.FooterRow.Cells[3].Visible = false;
                    gridNangSuat.FooterRow.Cells[4].Visible = false;
                    gridNangSuat.FooterRow.Cells[5].Visible = false;
                    for (int i=0;i < lst.Count;i++)
                    {
                        NangSuatNgayCongNhan nscn = lst[i];
                        totalTT += nscn.ThanhTien;
                        totalTL += nscn.TienLuong;
                    }
                    gridNangSuat.FooterRow.Cells[6].Text = string.Format("{0:#,0}", totalTT);
                    gridNangSuat.FooterRow.Cells[6].Font.Bold = true;
                    gridNangSuat.FooterRow.Cells[6].Style["text-align"] = "right";

                    gridNangSuat.FooterRow.Cells[8].Text = string.Format("{0:#,0}", totalTL);
                    gridNangSuat.FooterRow.Cells[8].Font.Bold = true;
                    gridNangSuat.FooterRow.Cells[8].Style["text-align"] = "right";

                    gridNangSuat.FooterRow.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    DataTable dt = ultils.CreateDataTable<NangSuatNgayCongNhan>(lst);
                    dt.Rows.Add(dt.NewRow());
                    gridNangSuat.DataSource = dt;
                    gridNangSuat.DataBind();
                    gridNangSuat.Rows[0].Cells.Clear();
                    gridNangSuat.Rows[0].Cells.Add(new TableCell());
                    gridNangSuat.Rows[0].Cells[0].ColumnSpan = 9;
                    gridNangSuat.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridNangSuat.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    gridNangSuat.FooterRow.Cells[0].Text = "Tổng: ";
                    gridNangSuat.FooterRow.Cells[0].Font.Bold = true;
                    gridNangSuat.FooterRow.Cells[0].ColumnSpan = 6;
                    gridNangSuat.FooterRow.Cells[1].Visible = false;
                    gridNangSuat.FooterRow.Cells[2].Visible = false;
                    gridNangSuat.FooterRow.Cells[3].Visible = false;
                    gridNangSuat.FooterRow.Cells[4].Visible = false;
                    gridNangSuat.FooterRow.Cells[5].Visible = false;
                    decimal total = 0;
                    gridNangSuat.FooterRow.Cells[6].Text = string.Format("{0:#,0}", total);
                    gridNangSuat.FooterRow.Cells[6].Font.Bold = true;
                    gridNangSuat.FooterRow.Cells[6].Style["text-align"] = "right";

                    gridNangSuat.FooterRow.Cells[8].Text = string.Format("{0:#,0}", total);
                    gridNangSuat.FooterRow.Cells[8].Font.Bold = true;
                    gridNangSuat.FooterRow.Cells[8].Style["text-align"] = "right";
                    gridNangSuat.FooterRow.BackColor = System.Drawing.Color.Beige;
                }
            }
            catch (Exception ex) { }
        }

        protected void loadDataGridNhayKhau()
        {
            try
            {
                int mansid = 0;
                if (Session["userid"] != null)
                    mansid = Convert.ToInt32(Session["userid"].ToString());
                DateTime dte = DateTime.Parse(txtDate.Text);
                object[] sqlPr =
               {
                    new SqlParameter("@MaNS_ID", mansid),
                    new SqlParameter("@Ngay", dte.Date)
                };
                string sqlQuery = "[dbo].[pr_LCB_NhayKhau_WEB_GETDataByMaNS_Ngay] @MaNS_ID,@Ngay";
                List<NangSuatNgayCongNhan> lst = new List<NangSuatNgayCongNhan>();
                lst = db.Database.SqlQuery<NangSuatNgayCongNhan>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    gridNhayKhau.DataSource = lst;
                    gridNhayKhau.DataBind();

                    decimal total = 0;
                    gridNhayKhau.FooterRow.Cells[0].Text = "Tổng: ";
                    gridNhayKhau.FooterRow.Cells[0].Font.Bold = true;
                    gridNhayKhau.FooterRow.Cells[0].ColumnSpan = 4;
                    gridNhayKhau.FooterRow.Cells[1].Visible = false;
                    gridNhayKhau.FooterRow.Cells[2].Visible = false;
                    gridNhayKhau.FooterRow.Cells[3].Visible = false;
                    for (int i = 0;i < lst.Count;i++)
                    {
                        NangSuatNgayCongNhan nscn = lst[i];
                        total += nscn.ThanhTien;
                    }
                    gridNhayKhau.FooterRow.Cells[4].Text = string.Format("{0:#,0}", total);
                    gridNhayKhau.FooterRow.Cells[4].Font.Bold = true;
                    gridNhayKhau.FooterRow.Cells[4].Style["text-align"] = "right";
                    gridNhayKhau.FooterRow.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    DataTable dt = ultils.CreateDataTable<NangSuatNgayCongNhan>(lst);
                    dt.Rows.Add(dt.NewRow());
                    gridNhayKhau.DataSource = dt;
                    gridNhayKhau.DataBind();
                    gridNhayKhau.Rows[0].Cells.Clear();
                    gridNhayKhau.Rows[0].Cells.Add(new TableCell());
                    gridNhayKhau.Rows[0].Cells[0].ColumnSpan = 5;
                    gridNhayKhau.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridNhayKhau.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    gridNhayKhau.FooterRow.Cells[0].Text = "Tổng: ";
                    gridNhayKhau.FooterRow.Cells[0].Font.Bold = true;
                    gridNhayKhau.FooterRow.Cells[0].ColumnSpan = 4;
                    gridNhayKhau.FooterRow.Cells[1].Visible = false;
                    gridNhayKhau.FooterRow.Cells[2].Visible = false;
                    gridNhayKhau.FooterRow.Cells[3].Visible = false;
                    decimal total = 0;
                    gridNhayKhau.FooterRow.Cells[4].Text = string.Format("{0:#,0}", total);
                    gridNhayKhau.FooterRow.Cells[4].Font.Bold = true;
                    gridNhayKhau.FooterRow.Cells[4].Style["text-align"] = "right";
                    gridNhayKhau.FooterRow.BackColor = System.Drawing.Color.Beige; //text - align: right;
                }
            }
            catch (Exception ex) { }
        }

        protected void btnChiTiet_Click(object sender, EventArgs e)
        {
            loadDataGridNangSuat();
            loadDataGridNhayKhau();
            addthismodalContact.Style["display"] = "block";
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
        }

        protected void gridNangSuat_RowCreated(object sender, GridViewRowEventArgs e)
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
                cell.Text = "Mã hàng: " + DataBinder.Eval(e.Row.DataItem, "MaHang").ToString();
                cell.ColumnSpan = 9;
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
                    cell.Text = "Mã hàng: " + DataBinder.Eval(e.Row.DataItem, "MaHang").ToString();
                    cell.ColumnSpan = 9;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
            }
        }

        protected void gridNhayKhau_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            if ((strPreviousRowID_NK != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaHang_ID") != null))
                if (strPreviousRowID_NK != DataBinder.Eval(e.Row.DataItem, "MaHang_ID").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID_NK != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaHang_ID") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                intSubTotalIndex_NK = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID_NK == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaHang_ID") != null))
            {
                GridView grdViewOrders = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Mã hàng: " + DataBinder.Eval(e.Row.DataItem, "MaHang").ToString();
                cell.ColumnSpan = 5;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex_NK, row);
                intSubTotalIndex_NK++;
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
                    cell.Text = "Mã hàng: " + DataBinder.Eval(e.Row.DataItem, "MaHang").ToString();
                    cell.ColumnSpan = 5;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex_NK, row);
                    intSubTotalIndex_NK++;
                }
                #endregion
            }
        }

        protected void gridNangSuat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "MaHang_ID").ToString();
            }
        }

        protected void gridNhayKhau_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID_NK = DataBinder.Eval(e.Row.DataItem, "MaHang_ID").ToString();
            }
        }
    }
}