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
    public partial class DuyetNS : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl linhapns = (HtmlGenericControl)this.Master.FindControl("nhapNS");
            HtmlGenericControl lithoigiancho = (HtmlGenericControl)this.Master.FindControl("thoigiancho");
            HtmlGenericControl liduyetNS = (HtmlGenericControl)this.Master.FindControl("duyetNS");
            HtmlGenericControl liluongns = (HtmlGenericControl)this.Master.FindControl("luongns");
            HtmlGenericControl libluong = (HtmlGenericControl)this.Master.FindControl("bluong");
            linhapns.Attributes.Add("class", "");
            lithoigiancho.Attributes.Add("class", "");
            liduyetNS.Attributes.Add("class", "active");
            liluongns.Attributes.Add("class", "");
            libluong.Attributes.Add("class", "");
            db = new TNG_CTLDbContact();
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    Session["PD_PhongBanID"] = Session["ChucVu"].ToString();
                    if (!string.IsNullOrEmpty(txtDate.Text))
                    {
                        ItemsGet();
                        loadDataGridLuongCN();
                        loadDataGrid();
                    }
                }
            }
            else
                Response.Redirect("Login.aspx");
        }

        protected void ItemsGet()
        {
            try
            {
                int phongbanid = 0;
                if (Session["ChucVu"] != null)
                    phongbanid = Convert.ToInt32(Session["ChucVu"].ToString());
                object[] sqlPr =
                {
                    new SqlParameter("@PhongBanID", phongbanid),
                    new SqlParameter("@Ngay", DateTime.Parse(txtDate.Text).Date)
                };
                string sqlQuery = "[dbo].[pr_LCB_KeHoach_NhanVien_SelectList_HoTen_MaHang_By_PhongID_Ngay] @PhongBanID,@Ngay";
                List<ListUser> lst = new List<ListUser>();
                lst = db.Database.SqlQuery<ListUser>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    ddlUser.DataSource = lst;
                    ddlUser.DataBind();
                    if(ddlUser.SelectedValue != null && ddlUser.SelectedValue.ToString() != "")
                    {
                        string trangthai = ddlUser.SelectedItem.Text.Split('-')[1];
                        if (!string.IsNullOrEmpty(trangthai) && trangthai.Contains("Chưa duyệt"))
                        {
                            ddlUser.Attributes.Add("style", "color:red");
                            ddlUser.SelectedItem.Attributes.Add("style", "color:red");
                        }
                        else
                        {
                            ddlUser.Attributes.Add("style", "color:black");
                            ddlUser.SelectedItem.Attributes.Add("style", "color:black");
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            ItemsGet();
            loadDataGridLuongCN();
            loadDataGrid();
        }

        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            string trangthai = ddlUser.SelectedItem.Text.Split('-')[1];
            if (!string.IsNullOrEmpty(trangthai) && trangthai.Contains("Chưa duyệt"))
            {
                ddlUser.Attributes.Add("style", "color:red");
                ddlUser.SelectedItem.Attributes.Add("style", "color:red");
            }
            else
            {
                ddlUser.Attributes.Add("style", "color:black");
                ddlUser.SelectedItem.Attributes.Add("style", "color:black");
            }
            checkColorItem_DropDownList();
            loadDataGridLuongCN();
            loadDataGrid();
        }

        protected void loadDataGrid()
        {
            try
            {
                int mansid = 0;
                if (ddlUser.SelectedValue != null && ddlUser.SelectedValue.ToString() != "")
                    mansid = Convert.ToInt32(ddlUser.SelectedValue.ToString());
                int phongid = 0;
                if (Session["PD_PhongBanID"] != null)
                    phongid = Convert.ToInt32(Session["PD_PhongBanID"].ToString());
                DateTime ngay = DateTime.Parse(txtDate.Text);
                object[] sqlPr =
               {
                    new SqlParameter("@MaNS_ID", mansid),
                    new SqlParameter("@Ngay", ngay.Date)
                };
                string sqlQuery = "[dbo].[pr_LCB_NangSuatCongNhan_Duyet_WEB_SelectPheDuyet] @MaNS_ID,@Ngay";
                List<ListNangSuatCongNhan> lst = new List<ListNangSuatCongNhan>();
                lst = db.Database.SqlQuery<ListNangSuatCongNhan>(sqlQuery, sqlPr).ToList();
                DataTable dt = ultils.CreateDataTable<ListNangSuatCongNhan>(lst);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //gridNangSuatUser.DataSource = dt;
                    //gridNangSuatUser.DataBind();
                    //LCB_NangSuatCongNhan_PheDuyet pd = new LCB_NangSuatCongNhan_PheDuyet();
                    //pd = db.LCB_NangSuatCongNhan_PheDuyet.Where(x => x.MaNS_ID == mansid && x.PhongBanID == phongid && x.Ngay.Year == ngay.Year && x.Ngay.Month == ngay.Month && x.Ngay.Day == ngay.Day).FirstOrDefault();
                    //if(pd != null)
                    //{
                    //    txtLyDo.Text = pd.LyDo;
                    //}
                }
                else
                {
                    txtLyDo.Text = "Tổ trưởng chưa phê duyệt.";
                    dt.Rows.Add(dt.NewRow());
                    gridNangSuatUser.DataSource = dt;
                    gridNangSuatUser.DataBind();
                    gridNangSuatUser.Rows[0].Cells.Clear();
                    gridNangSuatUser.Rows[0].Cells.Add(new TableCell());
                    gridNangSuatUser.Rows[0].Cells[0].ColumnSpan = 14;
                    gridNangSuatUser.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridNangSuatUser.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex) { }
        }

        protected void loadDataGridLuongCN()
        {
            try
            {
                int mansid = 0;
                if (ddlUser.SelectedValue != null && ddlUser.SelectedValue.ToString() != "")
                    mansid = Convert.ToInt32(ddlUser.SelectedValue.ToString());
                int phongid = 0;
                if (Session["PD_PhongBanID"] != null)
                    phongid = Convert.ToInt32(Session["PD_PhongBanID"].ToString());
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

                    lblluongCB.Text = "Lương CB: " + string.Format("{0:#,0}", decimal.Parse(dt.Rows[0]["LuongCB"].ToString())) + "(VNĐ)";

                    decimal total = 0, totallgsp = 0, totalvuot = 0, totalthemh = 0;
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
                    gridLuongNS.FooterRow.BackColor = System.Drawing.Color.Beige;

                    gridLuongNS.FooterRow.Cells[3].Text = string.Format("{0:#,0}", totalvuot);
                    gridLuongNS.FooterRow.Cells[3].Font.Bold = true;
                    gridLuongNS.FooterRow.BackColor = System.Drawing.Color.Beige;

                    gridLuongNS.FooterRow.Cells[4].Text = string.Format("{0:#,0}", totalthemh);
                    gridLuongNS.FooterRow.Cells[4].Font.Bold = true;
                    gridLuongNS.FooterRow.BackColor = System.Drawing.Color.Beige;

                    gridLuongNS.FooterRow.Cells[5].Text = string.Format("{0:#,0}", total);
                    gridLuongNS.FooterRow.Cells[5].Font.Bold = true;
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
                    gridLuongNS.Rows[0].Cells[0].ColumnSpan = 10;
                    gridLuongNS.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridLuongNS.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex) { }
        }

        protected void gridNangSuatUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "MaHang").ToString();
                Label lbl = (Label)e.Row.FindControl("lblPheDuyet");
                if (lbl != null && !string.IsNullOrEmpty(lbl.Text))
                {
                    if (lbl.Text == "PD")
                    {
                        lbl.ForeColor = System.Drawing.Color.Blue;
                    }
                    else if (lbl.Text == "CD")
                    {
                        lbl.ForeColor = System.Drawing.Color.Red;
                        //btnSave.Enabled = true;
                    }
                    else
                    {
                        lbl.ForeColor = System.Drawing.Color.Black;
                        //btnSave.Enabled = true;
                    }
                }
            }
        }

        protected void btnPD_Click(object sender, EventArgs e)
        {
            try
            {
                //int mansid = 0;
                //if (ddlUser.SelectedValue != null && ddlUser.SelectedValue.ToString() != "")
                //    mansid = Convert.ToInt32(ddlUser.SelectedValue.ToString());
                //int phongid = 0;
                //if (Session["PD_PhongBanID"] != null)
                //    phongid = Convert.ToInt32(Session["PD_PhongBanID"].ToString());
                //DateTime ngay = DateTime.Parse(txtDate.Text);
                //LCB_NangSuatCongNhan_PheDuyet dp = new LCB_NangSuatCongNhan_PheDuyet();
                //dp = db.LCB_NangSuatCongNhan_PheDuyet.Where(x => x.MaNS_ID == mansid && x.PhongBanID == phongid && x.Ngay.Year == ngay.Year && x.Ngay.Month == ngay.Month && x.Ngay.Day == ngay.Day).FirstOrDefault();
                //if (dp != null)
                //{
                //    dp.IsPheDuyet = true;
                //    dp.TenDangNhap_PheDuyet = Session["username"].ToString();
                //    dp.Ngay_PheDuyet = DateTime.Now;
                //    db.SaveChanges();                    
                //    lblMessenger.Text = "Đã phê duyệt.";
                //    addthismodalContact.Style["display"] = "block";
                //    divThongBao.Style["display"] = "block";
                //}
                //else
                //{
                //    LCB_NangSuatCongNhan_PheDuyet nscn_dp = new LCB_NangSuatCongNhan_PheDuyet();
                //    nscn_dp.MaNS_ID = mansid;
                //    nscn_dp.PhongBanID = phongid;
                //    nscn_dp.Ngay = ngay;
                //    nscn_dp.IsPheDuyet = true;
                //    nscn_dp.TenDangNhap_PheDuyet = Session["username"].ToString();
                //    nscn_dp.Ngay_PheDuyet = DateTime.Now;
                //    nscn_dp.LyDo = "Tổ trưởng phê duyệt.";
                //    db.LCB_NangSuatCongNhan_PheDuyet.Add(nscn_dp);
                //    db.SaveChanges();                    
                //    lblMessenger.Text = "Đã phê duyệt.";
                //    addthismodalContact.Style["display"] = "block";
                //    divThongBao.Style["display"] = "block";
                //}
                //if (!string.IsNullOrEmpty(txtDate.Text))
                //{
                //    ItemsGet();
                //    loadDataGridLuongCN();
                //    loadDataGrid();
                //}
            }
            catch { }
        }

        protected void btnHuyPD_Click(object sender, EventArgs e)
        {
            try
            {
                int mansid = 0;
                if (ddlUser.SelectedValue != null && ddlUser.SelectedValue.ToString() != "")
                    mansid = Convert.ToInt32(ddlUser.SelectedValue.ToString());
                int phongid = 0;
                if (Session["PD_PhongBanID"] != null)
                    phongid = Convert.ToInt32(Session["PD_PhongBanID"].ToString());
                DateTime ngay = DateTime.Parse(txtDate.Text);
                //LCB_NangSuatCongNhan_PheDuyet dp = new LCB_NangSuatCongNhan_PheDuyet();
                //dp = db.LCB_NangSuatCongNhan_PheDuyet.Where(x => x.MaNS_ID == mansid && x.PhongBanID == phongid && x.Ngay.Year == ngay.Year && x.Ngay.Month == ngay.Month && x.Ngay.Day == ngay.Day).FirstOrDefault();
                //if (dp != null)
                //{
                //    dp.IsPheDuyet = false;
                //    dp.TenDangNhap_PheDuyet = Session["username"].ToString();
                //    dp.Ngay_PheDuyet = DateTime.Now;
                //    db.SaveChanges();
                    
                //    lblMessenger.Text = "Hủy phê duyệt.";
                //    addthismodalContact.Style["display"] = "block";
                //    divThongBao.Style["display"] = "block";
                //}
                //else
                //{
                    //LCB_NangSuatCongNhan_PheDuyet nscn_dp = new LCB_NangSuatCongNhan_PheDuyet();
                    //nscn_dp.MaNS_ID = mansid;
                    //nscn_dp.PhongBanID = phongid;
                    //nscn_dp.Ngay = ngay;
                    //nscn_dp.IsPheDuyet = false;
                    //nscn_dp.TenDangNhap_PheDuyet = Session["username"].ToString();
                    //nscn_dp.Ngay_PheDuyet = DateTime.Now;
                    //if (!string.IsNullOrEmpty(txtLyDo.Text))
                    //    nscn_dp.LyDo = txtLyDo.Text;
                    //else
                    //    nscn_dp.LyDo = "Tổ trưởng chưa phê duyệt.";
                    //db.LCB_NangSuatCongNhan_PheDuyet.Add(nscn_dp);
                    //db.SaveChanges();
                    
                    //lblMessenger.Text = "Hủy phê duyệt.";
                    //addthismodalContact.Style["display"] = "block";
                    //divThongBao.Style["display"] = "block";
                //}

                //if (!string.IsNullOrEmpty(txtDate.Text))
                //{
                //    ItemsGet();
                //    loadDataGridLuongCN();
                //    loadDataGrid();
                //}
            }
            catch { }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
            divThongBao.Style["display"] = "none";
        }

        protected void gridNangSuatUser_RowCreated(object sender, GridViewRowEventArgs e)
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

        protected void ddlUser_DataBound(object sender, EventArgs e)
        {
            checkColorItem_DropDownList();
        }

        protected void checkColorItem_DropDownList()
        {
            foreach (ListItem myItem in ddlUser.Items)
            {
                string trangthai = myItem.Text.Split('-')[1];
                if (!string.IsNullOrEmpty(trangthai) && trangthai.Contains("Chưa duyệt"))
                {
                    myItem.Attributes.Add("style", "color:red");
                }
                else
                {
                    myItem.Attributes.Add("style", "color:black");
                }
            }
        }
    }
}