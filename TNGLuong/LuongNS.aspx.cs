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
    public partial class LuongNS : System.Web.UI.Page
    {
        string strPreviousRowID = string.Empty;   
        int intSubTotalIndex = 1;
        TNG_CTLDbContact db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new TNG_CTLDbContact();
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            if(Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM");
                    if(!string.IsNullOrEmpty(txtDate.Text))
                    {
                        loadDataLuongCN();
                        if(gridLuongNS.Rows.Count > 0 && !string.IsNullOrEmpty(gridLuongNS.Rows[0].Cells[0].Text))
                        {
                            if(gridLuongNS.Rows[0].Cells[0].Text != "Chưa có dữ liệu ..!")
                            {
                                DateTime dte = new DateTime();
                                if (DateTime.TryParse(gridLuongNS.Rows[0].Cells[0].Text, out dte))
                                    loadDataDetail(dte);
                            }    
                            else
                            {
                                DateTime dte = DateTime.Parse(txtDate.Text);
                                loadDataDetail(dte);
                            }                        
                        }                        
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
                string sqlQuery = "[dbo].[pr_LCB_LuongNgayCongNhan_WEB_SelectByPhongID_MansID_Ngay] @PhongBanID,@MaNS_ID,@Ngay";
                List<LCB_LuongNgayCongNhan> lst = new List<LCB_LuongNgayCongNhan>();
                lst = db.Database.SqlQuery<LCB_LuongNgayCongNhan>(sqlQuery, sqlPr).ToList();
                DataTable dt = ultils.CreateDataTable<LCB_LuongNgayCongNhan>(lst);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gridLuongNS.DataSource = dt;
                    gridLuongNS.DataBind();

                    lblluongCB.Text = "Lương CB: "+ string.Format("{0:#,0}", decimal.Parse(dt.Rows[0]["LuongCB"].ToString())) + "(VNĐ)";

                    decimal total = 0,totallgsp = 0, totalvuot = 0,totalthemh = 0;
                    gridLuongNS.FooterRow.Cells[0].Text = "Tổng (VNĐ): ";
                    gridLuongNS.FooterRow.Cells[0].Font.Bold = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        total += Convert.ToDecimal(dr["TongTienLuong"]);
                        totallgsp += Convert.ToDecimal(dr["LuongSP"]);
                        totalvuot += Convert.ToDecimal(dr["VuotNangSuat"]);
                        totalthemh += Convert.ToDecimal(dr["LuongThemGio"]);
                    }
                    gridLuongNS.FooterRow.Cells[2].Text = string.Format("{0:#,0}", totallgsp);
                    gridLuongNS.FooterRow.Cells[2].Font.Bold = true;
                    gridLuongNS.FooterRow.Cells[2].Style["text-align"] = "right";
                    gridLuongNS.FooterRow.BackColor = System.Drawing.Color.Beige;

                    gridLuongNS.FooterRow.Cells[3].Text = string.Format("{0:#,0}", totalvuot);
                    gridLuongNS.FooterRow.Cells[3].Font.Bold = true;
                    gridLuongNS.FooterRow.Cells[3].Style["text-align"] = "right";
                    gridLuongNS.FooterRow.BackColor = System.Drawing.Color.Beige;

                    gridLuongNS.FooterRow.Cells[4].Text = string.Format("{0:#,0}", totalthemh);
                    gridLuongNS.FooterRow.Cells[4].Font.Bold = true;
                    gridLuongNS.FooterRow.Cells[4].Style["text-align"] = "right";
                    gridLuongNS.FooterRow.BackColor = System.Drawing.Color.Beige;

                    gridLuongNS.FooterRow.Cells[5].Text = string.Format("{0:#,0}", 0);
                    gridLuongNS.FooterRow.Cells[5].Font.Bold = true;
                    gridLuongNS.FooterRow.Cells[5].Style["text-align"] = "right";
                    gridLuongNS.FooterRow.BackColor = System.Drawing.Color.Beige;

                    gridLuongNS.FooterRow.Cells[6].Text = string.Format("{0:#,0}", total);
                    gridLuongNS.FooterRow.Cells[6].Font.Bold = true;
                    gridLuongNS.FooterRow.Cells[6].Style["text-align"] = "right";
                    gridLuongNS.FooterRow.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr["PhongBanID"] = 0;
                    dr["MaNS_ID"] = mansid;
                    dr["Ngay"] = DateTime.Now.ToString();
                    dr["GioLamViec"] = 0;
                    dr["LuongCB"] = 0;
                    dr["LuongSP"] = 0;
                    dr["VuotNangSuat"] = 0;
                    dr["LuongThemGio"] = 0;
                    dr["TongTienLuong"] = 0;
                    dt.Rows.Add(dt.NewRow());
                    gridLuongNS.DataSource = dt;
                    gridLuongNS.DataBind();
                    gridLuongNS.Rows[0].Cells.Clear();
                    gridLuongNS.Rows[0].Cells.Add(new TableCell());
                    gridLuongNS.Rows[0].Cells[0].ColumnSpan = 9;
                    gridLuongNS.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridLuongNS.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex) { }
        }

        protected void loadDataDetail(DateTime dte)
        {
            try
            {
                int mansid = 0;
                if (Session["userid"] != null)
                    mansid = Convert.ToInt32(Session["userid"].ToString());
                object[] sqlPr =
               {
                    new SqlParameter("@MaNS_ID", mansid),
                    new SqlParameter("@Ngay", dte.Date)
                };
                string sqlQuery = "[dbo].[pr_LCB_NangSuatCongNhan_Duyet_WEB_SelectPheDuyet] @MaNS_ID,@Ngay";
                List<ListNangSuatCongNhan> lst = new List<ListNangSuatCongNhan>();
                lst = db.Database.SqlQuery<ListNangSuatCongNhan>(sqlQuery, sqlPr).ToList();
                DataTable dt = ultils.CreateDataTable<ListNangSuatCongNhan>(lst);
                if (dt != null && dt.Rows.Count > 0)
                {
                    girdDetailNS.DataSource = dt;
                    girdDetailNS.DataBind();

                    decimal total = 0;
                    girdDetailNS.FooterRow.Cells[0].Text = "Tổng: ";
                    girdDetailNS.FooterRow.Cells[0].Font.Bold = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal tong = Convert.ToDecimal(dr["DonGia"]) * Convert.ToInt32(dr["CongNhan"]);
                        total += tong;
                    }
                    girdDetailNS.FooterRow.Cells[4].Text = string.Format("{0:#,0}", total);
                    girdDetailNS.FooterRow.Cells[4].Font.Bold = true;
                    girdDetailNS.FooterRow.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    dt.Rows.Add(dt.NewRow());
                    girdDetailNS.DataSource = dt;
                    girdDetailNS.DataBind();
                    girdDetailNS.Rows[0].Cells.Clear();
                    girdDetailNS.Rows[0].Cells.Add(new TableCell());
                    girdDetailNS.Rows[0].Cells[0].ColumnSpan = 12;
                    girdDetailNS.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    girdDetailNS.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex) { }
        }

        protected void gridLuongNS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DateTime dte = Convert.ToDateTime(e.CommandArgument.ToString());
            if(e.CommandName == "Detail" && dte != null)
            {
                loadDataDetail(dte);

                addthismodalContact.Style["display"] = "block";
                divThongBao.Style["display"] = "block";
            }
            if(e.CommandName == "Refresh" && dte != null)
            {
                callstore(dte);
                loadDataLuongCN();
            }
        }

        protected void callstore(DateTime dte)
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

                object[] sqlPr =
                {
                    new SqlParameter("@iPhongBanID", phongid),
                    new SqlParameter("@iMaNS_ID", mansid),
                    new SqlParameter("@daNgay", dte.Date)
                };
                string sqlQuery = "exec [dbo].[pr_LCB_LuongNgayCongNhan_TinhLuong] @iPhongBanID,@iMaNS_ID,@daNgay";
                db.Database.ExecuteSqlCommand(sqlQuery, sqlPr);
            }
            catch (Exception ex) { }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
            divThongBao.Style["display"] = "none";
        }

        protected void girdDetailNS_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaHang") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "MaHang").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaHang") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "MaHang") != null))
            {
                GridView grdViewOrders = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Mã hàng: " + DataBinder.Eval(e.Row.DataItem, "MaHang").ToString();
                cell.ColumnSpan = 6;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }
            #endregion
            if (IsSubTotalRowNeedToAdd)
            {
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "MaHang") != null)
                {
                    GridView grdViewOrders = (GridView)sender;
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    TableCell cell = new TableCell();
                    cell.Text = "Mã hàng: " + DataBinder.Eval(e.Row.DataItem, "MaHang").ToString();
                    cell.ColumnSpan = 6;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
            }
        }

        protected void girdDetailNS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "MaHang").ToString();
                    decimal dongia = decimal.Parse(DataBinder.Eval(e.Row.DataItem, "DonGia").ToString());
                    int ns = int.Parse(DataBinder.Eval(e.Row.DataItem, "CongNhan").ToString());
                    Label txt = (Label)e.Row.FindControl("lblTOng");
                    if (txt != null)
                    {
                        decimal tong = dongia * ns;
                        txt.Text = string.Format("{0:#,0}", tong);
                    }                   
                }
            }
            catch { }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDate.Text))
            {
                loadDataLuongCN();
                if (gridLuongNS.Rows.Count > 0 && !string.IsNullOrEmpty(gridLuongNS.Rows[0].Cells[0].Text) && gridLuongNS.Rows[0].Cells[0].Text != "Chưa có dữ liệu ..!")
                {
                    DateTime dte = new DateTime();
                    if(DateTime.TryParse(gridLuongNS.Rows[0].Cells[0].Text, out dte))
                        loadDataDetail(dte);
                }
            }
        }
    }
}