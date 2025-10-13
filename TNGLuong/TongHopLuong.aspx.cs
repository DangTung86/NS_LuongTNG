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
    public partial class TongHopLuong : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        TNGLuongDbContact dblog = null;
        DatabaseManager dm = null;
        StoredParameterCollection spc = null;
        string strPreviousRowID = string.Empty;
        int intSubTotalIndex = 1;
        string strPreviousRowIDLg = string.Empty;
        int intSubTotalIndexLg = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            db = new TNG_CTLDbContact();
            dm = new DatabaseManager(db);
            spc = new StoredParameterCollection();
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    if (!string.IsNullOrEmpty(txtDate.Text))
                    {
                        getDataGrid();
                    }
                }
            }
            else
                Response.Redirect("Login.aspx");
        }

        private void getDataGrid()
        {
            try
            {
                int donviid = 0;
                int phongid_ns = 0;                
                if (Session["ChucVu"] != null)
                    phongid_ns = Convert.ToInt32(Session["ChucVu"].ToString());
                View_ToMay cls = db.View_ToMay.Where(x => x.PhongBanID == phongid_ns).SingleOrDefault();
                if (cls != null)
                    donviid = Convert.ToInt32(cls.DonViID);
                DateTime dte = Convert.ToDateTime(txtDate.Text);
                object[] sqlPr =
                {
                    new SqlParameter("@iDonViID", donviid),
                    new SqlParameter("@iPhongBanID", phongid_ns),
                    new SqlParameter("@iThang", dte.Month),
                    new SqlParameter("@iNam", dte.Year),
                    new SqlParameter("@Ngay", dte.Date)
                };
                string sqlQuery = "[dbo].[pr_WEB_TongHopLuongThang_Select_by_DonViID_and_PhongBan_and_Date] @iDonViID,@iPhongBanID,@iThang,@iNam,@Ngay";
                List<clsTongHopLuongThang> lst = new List<clsTongHopLuongThang>();
                lst = db.Database.SqlQuery<clsTongHopLuongThang>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    grdTongHopLuong.DataSource = lst;
                    grdTongHopLuong.DataBind();

                    lblTLThang.Text = string.Format("{0:#,0.##}", lst[0].TTLThang);
                    lblSLBLThang.Text = string.Format("{0:#,0.##}", lst[0].SLBLThang);
                    lblTLNgay.Text = string.Format("{0:#,0.##}", lst[0].TTLNgay);
                    lblSLBLNgay.Text = string.Format("{0:#,0.##}", lst[0].SLBLNgay);

                    double tyleThg = (double.Parse(lst[0].SLBLThang.ToString()) / double.Parse(lst.Count.ToString())) *100;
                    lblTLBLThang.Text = string.Format("{0:#,0.##}%", tyleThg);
                    double tyleNg = (double.Parse(lst[0].SLBLNgay.ToString()) / double.Parse(lst.Count.ToString())) * 100;
                    lblTLBLNgay.Text = string.Format("{0:#,0.##}%", tyleNg);

                    if (lst[0].PheDuyet != null && lst[0].PheDuyet.Equals("Chưa phê duyệt"))
                    {
                        lblTrangThai.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblTrangThai.ForeColor = System.Drawing.Color.Black;
                    }
                    lblTrangThai.Text = lst[0].PheDuyet;
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("STT", typeof(long)));
                    dt.Columns.Add(new DataColumn("MaNS_ID", typeof(int)));
                    dt.Columns.Add(new DataColumn("PhongBanID_NS", typeof(int)));
                    dt.Columns.Add(new DataColumn("HoTen", typeof(string)));
                    dt.Columns.Add(new DataColumn("MaNS", typeof(string)));
                    dt.Columns.Add(new DataColumn("TLThang", typeof(int)));
                    dt.Columns.Add(new DataColumn("BLThang", typeof(int)));
                    dt.Columns.Add(new DataColumn("TLNgay", typeof(int)));
                    dt.Columns.Add(new DataColumn("BLNgay", typeof(int)));
                    DataRow dr = dt.NewRow();
                    dr["STT"] = 0;
                    dr["MaNS_ID"] = 0;
                    dr["PhongBanID_NS"] = 0;
                    dr["HoTen"] = "s";
                    dr["MaNS"] = "s";
                    dr["TLThang"] = 0;
                    dr["BLThang"] = 0;
                    dr["TLNgay"] = 0;
                    dr["BLNgay"] = 0;
                    dt.Rows.Add(dr);

                    grdTongHopLuong.DataSource = dt;
                    grdTongHopLuong.DataBind();
                    grdTongHopLuong.Rows[0].Cells.Clear();
                    grdTongHopLuong.Rows[0].Cells.Add(new TableCell());
                    grdTongHopLuong.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                    grdTongHopLuong.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    grdTongHopLuong.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex) {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("STT", typeof(long)));
                dt.Columns.Add(new DataColumn("MaNS_ID", typeof(int)));
                dt.Columns.Add(new DataColumn("PhongBanID_NS", typeof(int)));
                dt.Columns.Add(new DataColumn("HoTen", typeof(string)));
                dt.Columns.Add(new DataColumn("MaNS", typeof(string)));
                dt.Columns.Add(new DataColumn("TLThang", typeof(int)));
                dt.Columns.Add(new DataColumn("BLThang", typeof(int)));
                dt.Columns.Add(new DataColumn("TLNgay", typeof(int)));
                dt.Columns.Add(new DataColumn("BLNgay", typeof(int)));
                DataRow dr = dt.NewRow();
                dr["STT"] = 0;
                dr["MaNS_ID"] = 0;
                dr["PhongBanID_NS"] = 0;
                dr["HoTen"] = "s";
                dr["MaNS"] = "s";
                dr["TLThang"] = 0;
                dr["BLThang"] = 0;
                dr["TLNgay"] = 0;
                dr["BLNgay"] = 0;
                dt.Rows.Add(dr);

                grdTongHopLuong.DataSource = dt;
                grdTongHopLuong.DataBind();
                grdTongHopLuong.Rows[0].Cells.Clear();
                grdTongHopLuong.Rows[0].Cells.Add(new TableCell());
                grdTongHopLuong.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                grdTongHopLuong.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                grdTongHopLuong.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        private void getDataGridChiTiet(int mansid)
        {
            try
            {
                int donviid = 0;
                int phongid_ns = 0;
                if (Session["ChucVu"] != null)
                    phongid_ns = Convert.ToInt32(Session["ChucVu"].ToString());
                View_ToMay cls = db.View_ToMay.Where(x => x.PhongBanID == phongid_ns).SingleOrDefault();
                if (cls != null)
                    donviid = Convert.ToInt32(cls.DonViID);
                DateTime dte = Convert.ToDateTime(txtDate.Text);
                string sqlQuery = "pr_WEB_TongHopLuongSelect_ChiTiet_All_2_Table";
                spc.Add("@iMaNS_ID", SqlDbType.Int, ParameterDirection.Input, mansid);
                spc.Add("@iDonViID", SqlDbType.Int, ParameterDirection.Input, donviid);
                spc.Add("@iPhongBanID", SqlDbType.Int, ParameterDirection.Input, phongid_ns);
                spc.Add("@iThang", SqlDbType.Int, ParameterDirection.Input, dte.Month);
                spc.Add("@iNam", SqlDbType.Int, ParameterDirection.Input, dte.Year);
                spc.Add("@daNgay", SqlDbType.Date, ParameterDirection.Input, dte.Date);

                DataSet ds = new DataSet();
                ds = dm.ExecuteStoredProcedure(sqlQuery, spc);
                //grid lương chi tiết cá nhân
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    grdChiTietTongHop.DataSource = ds.Tables[0];
                    grdChiTietTongHop.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Ngay", typeof(string)));
                    dt.Columns.Add(new DataColumn("LuongSP", typeof(decimal)));
                    dt.Columns.Add(new DataColumn("NhayKhau", typeof(decimal)));
                    dt.Columns.Add(new DataColumn("VuotNangSuat", typeof(decimal)));
                    dt.Columns.Add(new DataColumn("LuongThemGio", typeof(decimal)));
                    dt.Columns.Add(new DataColumn("BuLuong", typeof(decimal)));
                    dt.Columns.Add(new DataColumn("TongTienLuong", typeof(decimal)));
                    DataRow dr = dt.NewRow();
                    dr["Ngay"] = "";
                    dr["LuongSP"] = 0;
                    dr["NhayKhau"] = 0;
                    dr["VuotNangSuat"] = 0;
                    dr["LuongThemGio"] = 0;
                    dr["BuLuong"] = 0;
                    dr["TongTienLuong"] = 0;
                    dt.Rows.Add(dr);

                    grdChiTietTongHop.DataSource = dt;
                    grdChiTietTongHop.DataBind();
                    grdChiTietTongHop.Rows[0].Cells.Clear();
                    grdChiTietTongHop.Rows[0].Cells.Add(new TableCell());
                    grdChiTietTongHop.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                    grdChiTietTongHop.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    grdChiTietTongHop.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }

                //grid Nang Suat chi tiết cá nhân
                if (ds != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    grdChiTietNangSuat.DataSource = ds.Tables[1];
                    grdChiTietNangSuat.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Style", typeof(string)));
                    dt.Columns.Add(new DataColumn("TenPhongban", typeof(string)));
                    dt.Columns.Add(new DataColumn("TenCongDoan", typeof(string)));
                    dt.Columns.Add(new DataColumn("TGCN", typeof(decimal)));
                    dt.Columns.Add(new DataColumn("LKKeHoach", typeof(int)));
                    dt.Columns.Add(new DataColumn("LKThucHien", typeof(int)));
                    dt.Columns.Add(new DataColumn("KeHoach", typeof(int)));
                    dt.Columns.Add(new DataColumn("ThucHien", typeof(int)));
                    DataRow dr = dt.NewRow();
                    dr["Style"] = "";
                    dr["TenPhongban"] = "";
                    dr["TenCongDoan"] = "";
                    dr["TGCN"] = 0;
                    dr["LKKeHoach"] = 0;
                    dr["LKThucHien"] = 0;
                    dr["KeHoach"] = 0;
                    dr["ThucHien"] = 0;
                    dt.Rows.Add(dr);

                    grdChiTietNangSuat.DataSource = dt;
                    grdChiTietNangSuat.DataBind();
                    grdChiTietNangSuat.Rows[0].Cells.Clear();
                    grdChiTietNangSuat.Rows[0].Cells.Add(new TableCell());
                    grdChiTietNangSuat.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                    grdChiTietNangSuat.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    grdChiTietNangSuat.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
        }

        protected void grdTongHopLuong_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                dblog = new TNGLuongDbContact();
                View_Web_ThongTinNS nhansu = dblog.View_Web_ThongTinNS.Where(x => x.MaNS_ID == id).SingleOrDefault();
                if(nhansu != null)
                {
                    lblChiTietBL.Text = "Chi tiết bảng lương tháng của cá nhân: "+ nhansu.HoTen + " ("+ nhansu.MaNS + ")";
                    lblChiTietNS.Text = "Chi tiết năng suất tháng của cá nhân: " + nhansu.HoTen + " (" + nhansu.MaNS + ")";
                }
                if (e.CommandName.Equals("ChiTiet"))
                {
                    getDataGridChiTiet(id);
                    addthismodalContact.Style["display"] = "block";
                }
            }
            catch { }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            getDataGrid();
        }

        protected void grdChiTietNangSuat_RowCreated(object sender, GridViewRowEventArgs e)
        {            
            bool IsSubTotalRowNeedToAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Style") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "Style").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Style") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Style") != null))
            {
                GridView grdViewOrders = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "Style").ToString();
                cell.ColumnSpan = 7;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }
            #endregion
            if (IsSubTotalRowNeedToAdd)
            {
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "Style") != null)
                {
                    GridView grdViewOrders = (GridView)sender;
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    TableCell cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "Style").ToString();
                    cell.ColumnSpan = 7;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
            }
        }

        protected void grdChiTietNangSuat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "Style").ToString();
            }
        }

        protected void grdChiTietTongHop_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowIDLg = DataBinder.Eval(e.Row.DataItem, "MaNS_ID").ToString();
            }
        }

        protected void grdChiTietTongHop_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            if ((strPreviousRowIDLg != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaNS_ID") != null))
                if (strPreviousRowIDLg != DataBinder.Eval(e.Row.DataItem, "MaNS_ID").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowIDLg != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaNS_ID") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                intSubTotalIndexLg = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowIDLg == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaNS_ID") != null))
            {
                GridView grdViewOrders = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Tổng";
                cell.CssClass = "GroupHeaderStyle";
                cell.Attributes.Add("style", "text-align: center;");
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "TLuongSP","{0:#,0.##}").ToString();
                cell.CssClass = "GroupHeaderStyle";
                cell.Attributes.Add("style", "text-align: right;");
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "TNhayKhau", "{0:#,0.##}").ToString();
                cell.CssClass = "GroupHeaderStyle";
                cell.Attributes.Add("style", "text-align: right;");
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "TVuotNangSuat", "{0:#,0.##}").ToString();
                cell.CssClass = "GroupHeaderStyle";
                cell.Attributes.Add("style", "text-align: right;");
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "TLuongThemGio", "{0:#,0.##}").ToString();
                cell.CssClass = "GroupHeaderStyle";
                cell.Attributes.Add("style", "text-align: right;");
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "TBuLuong", "{0:#,0.##}").ToString();
                cell.CssClass = "GroupHeaderStyle";
                cell.Attributes.Add("style", "text-align: right;");
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "TTongTienLuong", "{0:#,0.##}").ToString();
                cell.CssClass = "GroupHeaderStyle";
                cell.Attributes.Add("style", "text-align: right;");
                row.Cells.Add(cell);
                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndexLg, row);
                intSubTotalIndexLg++;
            }
            #endregion
            if (IsSubTotalRowNeedToAdd)
            {
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "MaNS_ID") != null)
                {
                    GridView grdViewOrders = (GridView)sender;
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    TableCell cell = new TableCell();
                    cell.Text = "Tổng";
                    cell.CssClass = "GroupHeaderStyle";
                    cell.Attributes.Add("style", "text-align: center;");
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "TLuongSP", "{0:#,0.##}").ToString();
                    cell.CssClass = "GroupHeaderStyle";
                    cell.Attributes.Add("style", "text-align: right;");
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "TNhayKhau", "{0:#,0.##}").ToString();
                    cell.CssClass = "GroupHeaderStyle";
                    cell.Attributes.Add("style", "text-align: right;");
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "TVuotNangSuat", "{0:#,0.##}").ToString();
                    cell.CssClass = "GroupHeaderStyle";
                    cell.Attributes.Add("style", "text-align: right;");
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "TLuongThemGio", "{0:#,0.##}").ToString();
                    cell.CssClass = "GroupHeaderStyle";
                    cell.Attributes.Add("style", "text-align: right;");
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "TBuLuong", "{0:#,0.##}").ToString();
                    cell.CssClass = "GroupHeaderStyle";
                    cell.Attributes.Add("style", "text-align: right;");
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "TTongTienLuong", "{0:#,0.##}").ToString();
                    cell.CssClass = "GroupHeaderStyle";
                    cell.Attributes.Add("style", "text-align: right;");
                    row.Cells.Add(cell);
                    grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndexLg, row);
                    intSubTotalIndexLg++;
                }
                #endregion
            }
        }
    }
}