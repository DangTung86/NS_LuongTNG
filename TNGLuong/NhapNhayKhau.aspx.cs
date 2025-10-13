using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Models;
using TNGLuong.ModelsView;

namespace TNGLuong
{
    public partial class NhapNhayKhau : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new TNG_CTLDbContact();
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            btnSreach.ServerClick += new EventHandler(btnSearch_Click);
            btnGetSelectData.ServerClick += new EventHandler(btnGetSelectData_Click);
            if (Session["username"] != null)
            {
                if(!IsPostBack)
                {
                    Session["DTMaHang"] = null;
                    Session["ListSelectedData"] = null;
                    Session["NhomSize"] = null;
                    Session["ID_CachMay"] = null;
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Equals(("true").ToUpper()))
                        txtDate.Enabled = false;
                    else
                        txtDate.Enabled = true;
                    if (!string.IsNullOrEmpty(txtDate.Text))
                    {
                        loadDataToMay();
                        loadDataMaHang();
                        DataTable dt = this.loadDataGridNhayKhau();
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            gridNangSuatNhayKhau.DataSource = dt;
                            gridNangSuatNhayKhau.DataBind();
                        }
                        else
                        {
                            List<ListNSNhayKhau> lst = new List<ListNSNhayKhau>();
                            dt = ultils.CreateDataTable<ListNSNhayKhau>(lst);
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
                }
            }
            else
                Response.Redirect("Login.aspx");
        }

        protected DataTable loadDataGridNhayKhau()
        {
            try
            {
                int idmans = 0;
                int phongid = 0;
                int donviid = 0;
                byte nhomsize = 0;
                byte idcachmay = 0;
                string mahang = "";
                int Phongbanid_ns = 0;
                if (Session["userid"] != null)
                    idmans = Convert.ToInt32(Session["userid"].ToString());
                if (Session["DonViID"] != null)
                    donviid = Convert.ToInt32(Session["DonViID"].ToString());
                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    phongid = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                DateTime dte = Convert.ToDateTime(txtDate.Text);
                if (Session["NhomSize"] != null)
                    nhomsize = byte.Parse(Session["NhomSize"].ToString());
                if (Session["ID_CachMay"] != null)
                    idcachmay = byte.Parse(Session["ID_CachMay"].ToString());
                if (Session["MaHang"] != null)
                    mahang = Session["MaHang"].ToString();
                if (Session["PhongBanID"] != null)
                    Phongbanid_ns = Convert.ToInt32(Session["PhongBanID"].ToString());
                object[] sqlPr =
                {
                    new SqlParameter("@MaHang", mahang),
                    new SqlParameter("@PhongBanID", phongid),
                    new SqlParameter("@DonViID", donviid),
                    new SqlParameter("@NhomSize", nhomsize),
                    new SqlParameter("@ID_CachMay", idcachmay),
                    new SqlParameter("@MaNS_ID", idmans),
                    new SqlParameter("@Ngay", dte),
                    new SqlParameter("@iPhongBanID_NS", Phongbanid_ns)
                };
                string sqlQuery = "";
                if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Equals(("true").ToUpper()))
                    sqlQuery = "[dbo].[prDG_WEB_Select_ChonCongDoanNhayKhau] @MaHang,@PhongBanID,@DonViID,@NhomSize,@ID_CachMay,@MaNS_ID,@Ngay,@iPhongBanID_NS";
                else
                    sqlQuery = "[dbo].[pr_WEB_Select_ChonCongDoanNhayKhau] @MaHang,@PhongBanID,@DonViID,@NhomSize,@ID_CachMay,@MaNS_ID,@Ngay,@iPhongBanID_NS";
                List<ListNSNhayKhau> lst = new List<ListNSNhayKhau>();
                lst = db.Database.SqlQuery<ListNSNhayKhau>(sqlQuery, sqlPr).ToList();
                DataTable dt = ultils.CreateDataTable<ListNSNhayKhau>(lst);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) { return null; }
        }

        protected void btnSaveNhayKhau_Click(object sender, EventArgs e)
        {
            lblMessenger.Text = "Bạn có muốn lưu dữ liệu nhảy khâu từ mã hàng <b><font color=\"red\"> \"" + ddlMaHang.SelectedItem.Text.ToUpper() + "\"</font></b> của <b><font color=\"red\">\"" + ddlToMay.SelectedItem.Text.ToUpper() + "\"</font></b> ko?";
            addthismodalContact.Style["display"] = "block";
        }

        protected void gridNangSuatNhayKhau_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txt = (TextBox)e.Row.FindControl("txtCongNhanNhayKhau");
                txt.Attributes.Add("type", "number");
                if (!string.IsNullOrEmpty(txt.Text) && txt.Text == "0")
                    txt.Attributes.Add("onclick", "this.value = '';");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = this.loadDataGridNhayKhau();
            if (dt != null && dt.Rows.Count > 0)
            {
                StringBuilder filter = new StringBuilder();
                if (!(string.IsNullOrEmpty(txtsearch.Value)))
                {
                    filter.Append("TenCongDoan Like '%" + txtsearch.Value + "%'");
                }

                DataView dv = dt.DefaultView;
                dv.RowFilter = filter.ToString();

                gridNangSuatNhayKhau.DataSource = dv;
                gridNangSuatNhayKhau.DataBind();
            }
            else
            {
                List<ListNSNhayKhau> lst = new List<ListNSNhayKhau>();
                dt = ultils.CreateDataTable<ListNSNhayKhau>(lst);
                dt.Rows.Add(dt.NewRow());
                gridNangSuatNhayKhau.DataSource = dt;
                gridNangSuatNhayKhau.DataBind();
                gridNangSuatNhayKhau.Rows[0].Cells.Clear();
                gridNangSuatNhayKhau.Rows[0].Cells.Add(new TableCell());
                gridNangSuatNhayKhau.Rows[0].Cells[0].ColumnSpan = 14;
                gridNangSuatNhayKhau.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gridNangSuatNhayKhau.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void loadDataToMay()
        {
            try
            {
                int phong113 = 0;
                if (Session["PhongBanID"] != null)
                    phong113 = Convert.ToInt32(Session["PhongBanID"].ToString());
                object[] sqlpra =
                {
                    new SqlParameter("@PhongBanID", phong113)
                };
                string sqlQuery113 = "[dbo].[pr_WEB_Select_Phong113_wPhongBanID] @PhongBanID";
                string tenphongban = db.Database.SqlQuery<string>(sqlQuery113, sqlpra).FirstOrDefault();
                int idmans = 0;
                int donviid = 0;
                if (Session["userid"] != null)
                    idmans = Convert.ToInt32(Session["userid"].ToString());
                if (Session["DonViID"] != null)
                    donviid = Convert.ToInt32(Session["DonViID"].ToString());
                DateTime dte = Convert.ToDateTime(txtDate.Text);
                object[] sqlPr =
                {
                    new SqlParameter("@DonViID", donviid),
                    new SqlParameter("@MaNS_ID", idmans),
                    new SqlParameter("@Ngay", dte)
                };
                string sqlQuery = "";
                if(!string.IsNullOrEmpty(tenphongban) && tenphongban.Contains("113"))
                    sqlQuery = "[dbo].[pr_WEB_LCB_NhayKhau_DanhSachPhongBan_Select_113_wMaNS_ID_AND_Ngay] @DonViID,@MaNS_ID,@Ngay";
                else
                    sqlQuery = "[dbo].[pr_WEB_LCB_NhayKhau_DanhSachPhongBan_Select_wMaNS_ID_AND_Ngay] @DonViID,@MaNS_ID,@Ngay";
                List<View_ToMay> lst = new List<View_ToMay>();
                lst = db.Database.SqlQuery<View_ToMay>(sqlQuery, sqlPr).ToList();
                if (Session["PhongBanID"] != null)
                {
                    int phongids = Convert.ToInt32(Session["PhongBanID"].ToString());
                    View_ToMay tm = new View_ToMay();
                    tm = db.View_ToMay.Where(x => x.PhongBanID == phongids).SingleOrDefault();
                    if (tm != null)
                        lst.Add(tm);
                }
                if (lst != null && lst.Count > 0)
                {
                    List<View_ToMay> lstCat_HT = new List<View_ToMay>();
                    if (Session["NhomCat_HoanThien"] != null && Session["NhomCat_HoanThien"].ToString() == "1")
                    {
                        lstCat_HT = lst.Where(x => x.TenPhongban.ToLower().Contains("cắt")).ToList();
                        ddlToMay.DataSource = lstCat_HT;
                        ddlToMay.DataBind();
                        if (Session["PhongBanID"] != null)
                        {
                            ddlToMay.SelectedValue = Session["PhongBanID"].ToString();
                        }
                    }
                    else if (Session["NhomCat_HoanThien"] != null && Session["NhomCat_HoanThien"].ToString() == "3")
                    {
                        lstCat_HT = lst.Where(x => x.TenPhongban.ToLower().Contains("hoàn thiện")).ToList();
                        ddlToMay.DataSource = lstCat_HT;
                        ddlToMay.DataBind();
                        if (Session["PhongBanID"] != null)
                        {
                            ddlToMay.SelectedValue = Session["PhongBanID"].ToString();
                        }
                    }
                    else
                    {
                        ddlToMay.DataSource = lst;
                        ddlToMay.DataBind();
                        if (Session["PhongBanID"] != null)
                        {
                            ddlToMay.SelectedValue = Session["PhongBanID"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void ddlToMay_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMaHang.Items.Clear();
            loadDataMaHang();
            DataTable dt = this.loadDataGridNhayKhau();
            if (dt != null && dt.Rows.Count > 0)
            {
                gridNangSuatNhayKhau.DataSource = dt;
                gridNangSuatNhayKhau.DataBind();
            }
            else
            {
                List<ListNSNhayKhau> lst = new List<ListNSNhayKhau>();
                dt = ultils.CreateDataTable<ListNSNhayKhau>(lst);
                dt.Rows.Add(dt.NewRow());
                gridNangSuatNhayKhau.DataSource = dt;
                gridNangSuatNhayKhau.DataBind();
                gridNangSuatNhayKhau.Rows[0].Cells.Clear();
                gridNangSuatNhayKhau.Rows[0].Cells.Add(new TableCell());
                gridNangSuatNhayKhau.Rows[0].Cells[0].ColumnSpan = 14;
                gridNangSuatNhayKhau.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gridNangSuatNhayKhau.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void loadDataMaHang()
        {
            try
            {
                int donviid = 0;
                int phongid = 0;
                if (Session["DonViID"] != null)
                    donviid = Convert.ToInt32(Session["DonViID"].ToString());
                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    phongid = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                DateTime dte = Convert.ToDateTime(txtDate.Text);
                object[] sqlPr =
                {
                    new SqlParameter("@PhongBanID", phongid),
                    new SqlParameter("@DonViID", donviid),
                    new SqlParameter("@Ngay", dte.Date)
                };
                string sqlQuery = "[dbo].[pr_WEB_LCB_MaHang_NhayKhau_SelectByPhongID_DonVi] @PhongBanID,@DonViID,@Ngay";
                List<MaHangNhayKhau> lst = new List<MaHangNhayKhau>();
                lst = db.Database.SqlQuery<MaHangNhayKhau>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    DataTable dt = ultils.CreateDataTable<MaHangNhayKhau>(lst);
                    Session["DTMaHang"] = dt;
                    ddlMaHang.DataSource = lst;
                    ddlMaHang.DataBind();

                    if (ddlMaHang.SelectedValue != null && ddlMaHang.SelectedValue.ToString() != "" && ddlMaHang.SelectedValue.ToString() != "0")
                    {
                        long id = long.Parse(ddlMaHang.SelectedValue.ToString());
                        DataRow row = dt.Select("STT=" + id).Single();
                        if (row != null)
                        {
                            Session["NhomSize"] = row["NhomSize"].ToString();
                            Session["ID_CachMay"] = row["ID_CachMay"].ToString();
                            Session["MaHang"] = row["MaHang"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void ddlMaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlMaHang.SelectedValue != null && ddlMaHang.SelectedValue.ToString() != "" && ddlMaHang.SelectedValue.ToString() != "0")
            {
                long id = long.Parse(ddlMaHang.SelectedValue.ToString());
                DataTable dtmh = (DataTable)Session["DTMaHang"];
                DataRow row = dtmh.Select("STT=" + id).Single();
                if (row != null)
                {
                    Session["NhomSize"] = row["NhomSize"].ToString();
                    Session["ID_CachMay"] = row["ID_CachMay"].ToString();
                    Session["MaHang"] = row["MaHang"].ToString();
                }
            
                DataTable dt = this.loadDataGridNhayKhau();
                if (dt != null && dt.Rows.Count > 0)
                {
                    gridNangSuatNhayKhau.DataSource = dt;
                    gridNangSuatNhayKhau.DataBind();
                }
                else
                {
                    List<ListNSNhayKhau> lst = new List<ListNSNhayKhau>();
                    dt = ultils.CreateDataTable<ListNSNhayKhau>(lst);
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
        }

        protected void callstore()
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
                    new SqlParameter("@iPhongBanID", phongid),
                    new SqlParameter("@iMaNS_ID", mansid),
                    new SqlParameter("@daNgay", dte.Date)
                };
                string sqlQuery = "exec [dbo].[pr_LCB_LuongNgayCongNhan_TinhLuong] @iPhongBanID,@iMaNS_ID,@daNgay";
                db.Database.ExecuteSqlCommand(sqlQuery, sqlPr);
            }
            catch (Exception ex) { }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            loadDataToMay();
            loadDataMaHang();
            DataTable dt = this.loadDataGridNhayKhau();
            if (dt != null && dt.Rows.Count > 0)
            {
                gridNangSuatNhayKhau.DataSource = dt;
                gridNangSuatNhayKhau.DataBind();
            }
            else
            {
                List<ListNSNhayKhau> lst = new List<ListNSNhayKhau>();
                dt = ultils.CreateDataTable<ListNSNhayKhau>(lst);
                dt.Rows.Add(dt.NewRow());
                gridNangSuatNhayKhau.DataSource = dt;
                gridNangSuatNhayKhau.DataBind();
                gridNangSuatNhayKhau.Rows[0].Cells.Clear();
                gridNangSuatNhayKhau.Rows[0].Cells.Add(new TableCell());
                gridNangSuatNhayKhau.Rows[0].Cells[0].ColumnSpan = 14;
                gridNangSuatNhayKhau.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gridNangSuatNhayKhau.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void chkRow_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int phongid = 0;
                int mansid = 0;
                string mahang = "";
                byte nhomsize = 0;
                byte idcachmay = 0;
                if (Session["userid"] != null)
                    mansid = Convert.ToInt32(Session["userid"].ToString());
                DateTime dte = DateTime.Parse(txtDate.Text);
                if (ddlMaHang.SelectedValue != null && ddlMaHang.SelectedValue.ToString() != "" && ddlMaHang.SelectedValue.ToString() != "0")
                    mahang = ddlMaHang.SelectedItem.Text;
                if (Session["NhomSize"] != null)
                    nhomsize = byte.Parse(Session["NhomSize"].ToString());
                if (Session["ID_CachMay"] != null)
                    idcachmay = byte.Parse(Session["ID_CachMay"].ToString());
                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    phongid = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                CheckBox chk = sender as CheckBox;
                if (chk.Checked)
                {
                    GridViewRow row = (GridViewRow)chk.NamingContainer;
                    string lblID_CongDoanNhayKhau = ((Label)row.Cells[0].FindControl("lblID_CongDoanNhayKhau")).Text;                    
                    string nangsuat = ((TextBox)row.Cells[0].FindControl("txtCongNhanNhayKhau")).Text;
                    string lblTenCongDoanNhayKhau = ((Label)row.Cells[0].FindControl("lblTenCongDoanNhayKhau")).Text;
                    string lblDaPDNhayKhau = ((Label)row.Cells[0].FindControl("lblDaPDNhayKhau")).Text;
                    string lblLuyKeCD = ((Label)row.Cells[0].FindControl("lblLuyKeCD")).Text;
                    string lblSoLuong_CapBTP = ((Label)row.Cells[0].FindControl("lblSoLuong_CapBTP")).Text;
                    List<ListNSNhayKhau> lst = new List<ListNSNhayKhau>();
                    if (Session["ListSelectedData"] != null)
                        lst = (List<ListNSNhayKhau>)Session["ListSelectedData"];
                    ListNSNhayKhau nsnk = new ListNSNhayKhau();
                    nsnk.MaHang = mahang;
                    nsnk.TenCongDoan = lblTenCongDoanNhayKhau;
                    nsnk.PheDuyet = string.Empty;
                    nsnk.DonGia = decimal.Parse(lblDaPDNhayKhau);
                    nsnk.CongNhan = int.Parse(nangsuat);
                    nsnk.PhongBanID = phongid;
                    nsnk.NhomSize = nhomsize;
                    nsnk.ID_CachMay = idcachmay;
                    nsnk.ID_CongDoan = int.Parse(lblID_CongDoanNhayKhau);
                    nsnk.MaNS_ID = mansid;
                    nsnk.Ngay = dte;
                    nsnk.LuyKe = int.Parse(lblLuyKeCD);
                    nsnk.SoLuong_CapBTP = int.Parse(lblSoLuong_CapBTP);
                    lst.Add(nsnk);
                    if (lst != null && lst.Count > 0)
                        Session["ListSelectedData"] = lst;
                }
            }
            catch(Exception ex) { }
        }

        protected void btnGetSelectData_Click(object sender, EventArgs e)
        {
            if(Session["ListSelectedData"] != null)
            {
                List<ListNSNhayKhau> lst = (List<ListNSNhayKhau>)Session["ListSelectedData"];
                DataTable dt = ultils.CreateDataTable<ListNSNhayKhau>(lst);
                if(dt != null && dt.Rows.Count > 0)
                {
                    gridNangSuatNhayKhau.DataSource = dt;
                    gridNangSuatNhayKhau.DataBind();
                }
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
            divBTN.Visible = true;
        }

        protected bool checkValueNangSuat()
        {
            decimal thanhtienNS = 0;
            decimal thanhtienNK = 0;
            decimal tong = 0;
            foreach (GridViewRow row in gridNangSuatNhayKhau.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string itemNum = ((TextBox)row.Cells[0].FindControl("txtCongNhanNhayKhau")).Text;
                    string dongia = ((Label)row.Cells[0].FindControl("lblDaPDNhayKhau")).Text;
                    if (!string.IsNullOrEmpty(itemNum) && !string.IsNullOrEmpty(dongia) && decimal.Parse(itemNum.Trim()) > 0)
                    {
                        decimal tien = decimal.Parse(itemNum.Trim()) * decimal.Parse(dongia.Trim());
                        thanhtienNK = thanhtienNK + tien;
                    }
                }
            }
            tong = thanhtienNS + thanhtienNK;
            if (thanhtienNS > 1000000)
                return false;
            else if (thanhtienNK > 1000000)
                return false;
            else if (tong > 1000000)
                return false;
            else
                return true;
        }

        protected bool checkLuyKeNhayKhau()
        {
            bool sus = true;
            foreach (GridViewRow row in gridNangSuatNhayKhau.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string lblLKBTP = ((Label)row.Cells[0].FindControl("lblLuyKeCD")).Text;
                    string lblSoLuong_CapBTP = ((Label)row.Cells[0].FindControl("lblSoLuong_CapBTP")).Text;
                    string lblIsBTP = ((Label)row.Cells[0].FindControl("lblIsBTP")).Text;
                    TextBox txt = (TextBox)row.Cells[0].FindControl("txtCongNhanNhayKhau");
                    if (!string.IsNullOrEmpty(lblIsBTP) && lblIsBTP.Trim() == "1")
                    {
                        if (!string.IsNullOrEmpty(txt.Text) && !string.IsNullOrEmpty(lblLKBTP) && !string.IsNullOrEmpty(lblSoLuong_CapBTP) && Convert.ToDouble(txt.Text.Trim()) > 0)
                        {
                            double luyke = Convert.ToDouble(lblLKBTP.Trim());
                            double thuchien = Convert.ToDouble(txt.Text.Trim());
                            double total = luyke + thuchien;
                            double slbtp = Convert.ToDouble(lblSoLuong_CapBTP.Trim());
                            if (slbtp < total)
                            {
                                txt.Text = "0";
                                sus = false;
                                break;
                            }
                        }
                    }
                }
            }
            return sus;
        }

        protected bool checkValueAm_NhayKhau()
        {
            bool sus = true;
            foreach (GridViewRow row in gridNangSuatNhayKhau.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txt = (TextBox)row.Cells[0].FindControl("txtCongNhanNhayKhau");
                    if (!string.IsNullOrEmpty(txt.Text) && double.Parse(txt.Text.Trim()) < 0)
                    {
                        txt.Text = "0";
                        sus = false;
                        break;
                    }
                }
            }
            return sus;
        }

        protected int checkValue_NhayKhau_Sau3Ngay()
        {
            int sus = -1;
            foreach (GridViewRow row in gridNangSuatNhayKhau.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl = (Label)row.Cells[0].FindControl("lblID_CongDoanNhayKhau");
                    TextBox txt = (TextBox)row.Cells[0].FindControl("txtCongNhanNhayKhau");
                    if (!string.IsNullOrEmpty(txt.Text) && double.Parse(txt.Text.Trim()) != 0)
                    {
                        string mSoNgay = getSoNgayLonHon3(int.Parse(lbl.Text));
                        if (!string.IsNullOrEmpty(mSoNgay))
                        {
                            sus = row.RowIndex; 
                            break;
                        }
                    }
                }
            }
            return sus;
        }

        protected string getSoNgayLonHon3(int mID_CongDoan)
        {
            try
            {
                int Phongbanid_ns = 0;
                int phongid = 0;
                int mansid = 0;
                string mahang = "";
                byte nhomsize = 0;
                byte idcachmay = 0;
                if (Session["PhongBanID"] != null)
                    Phongbanid_ns = Convert.ToInt32(Session["PhongBanID"].ToString());
                if (Session["userid"] != null)
                    mansid = Convert.ToInt32(Session["userid"].ToString());
                DateTime dte = DateTime.Parse(txtDate.Text);
                if (Session["MaHang"] != null)
                    mahang = Session["MaHang"].ToString();
                if (Session["NhomSize"] != null)
                    nhomsize = byte.Parse(Session["NhomSize"].ToString());
                if (Session["ID_CachMay"] != null)
                    idcachmay = byte.Parse(Session["ID_CachMay"].ToString());
                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    phongid = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                object[] sqlPr =
                {
                    new SqlParameter("@Ngay", dte),
                    new SqlParameter("@MaNS_ID", mansid),
                    new SqlParameter("@PhongBanID_NS", Phongbanid_ns),                    
                    new SqlParameter("@ID_CongDoan", mID_CongDoan),
                    new SqlParameter("@MaHang", mahang),
                    new SqlParameter("@PhongBanID", phongid),
                    new SqlParameter("@NhomSize", nhomsize),
                    new SqlParameter("@ID_CachMay", idcachmay)
                };
                // Processing.  
                string sqlQuery = "[dbo].[pr_Web_LCB_NhayKhau_getSoNgayDaNhap_CongDoan] @Ngay,@MaNS_ID,@PhongBanID_NS,@ID_CongDoan,@MaHang,@PhongBanID,@NhomSize,@ID_CachMay";

                string checktngf = db.Database.SqlQuery<string>(sqlQuery, sqlPr).SingleOrDefault();
                return checktngf;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtNhayKhau = new DataTable("dtNhayKhau");
                dtNhayKhau.Columns.Add(new DataColumn("MaNS_ID", typeof(int)));
                dtNhayKhau.Columns.Add(new DataColumn("PhongBanID_NS", typeof(int)));
                dtNhayKhau.Columns.Add(new DataColumn("Ngay", typeof(DateTime)));
                dtNhayKhau.Columns.Add(new DataColumn("ID_CongDoan", typeof(int)));
                dtNhayKhau.Columns.Add(new DataColumn("MaHang", typeof(string)));
                dtNhayKhau.Columns.Add(new DataColumn("PhongBanID", typeof(int)));
                dtNhayKhau.Columns.Add(new DataColumn("NhomSize", typeof(byte)));
                dtNhayKhau.Columns.Add(new DataColumn("ID_CachMay", typeof(byte)));
                dtNhayKhau.Columns.Add(new DataColumn("SoLuong_NhayKhau", typeof(int)));

                if (checkValueAm_NhayKhau() == false)
                {
                    lblMessenger.Text = "Số lượng thực hiện không thể nhỏ hơn 0!";
                    divBTN.Visible = false;
                    addthismodalContact.Style["display"] = "block";
                    return;
                }
                else if (checkLuyKeNhayKhau() == false && Session["NhomCat_HoanThien"] == null)
                {
                    lblMessenger.Text = "Số thực hiện lớn hơn số cấp Bán thành phẩm lên chuyền!";
                    divBTN.Visible = false;
                    addthismodalContact.Style["display"] = "block";
                    return;
                }
                else if (checkLuyKeNhayKhau() == false && Session["NhomCat_HoanThien"] != null)
                {                    
                    if (Session["NhomCat_HoanThien"].ToString() == "1")
                    {
                        lblMessenger.Text = "Số thực hiện lớn hơn tổng số lượng cắt!";
                        divBTN.Visible = false;
                        addthismodalContact.Style["display"] = "block";
                        return;
                    }
                    else if (Session["NhomCat_HoanThien"].ToString() == "3")
                    {
                        lblMessenger.Text = "Số thực hiện lớn hơn tổng số lượng ra chuyền!";
                        divBTN.Visible = false;
                        addthismodalContact.Style["display"] = "block";
                        return;
                    }
                }
                else if(checkValueNangSuat() == false)
                {
                    lblMessenger.Text = "Số lượng thực hiện không đúng, vui lòng kiểm tra lại.";
                    divBTN.Visible = false;
                    addthismodalContact.Style["display"] = "block";

                    return;
                }
                else
                {
                    int index = checkValue_NhayKhau_Sau3Ngay();
                    if (index >= 0)
                    {
                        GridViewRow row = gridNangSuatNhayKhau.Rows[index];
                        string lbl = ((Label)row.Cells[0].FindControl("lblTenCongDoanNhayKhau")).Text;
                        lblMessenger.Text = "công đoạn " + lbl.ToUpper() + " đã nhập nhẩy khâu quá 3 ngày, bạn không được nhập nữa!";
                        divBTN.Visible = false;
                        addthismodalContact.Style["display"] = "block";

                        return;
                    }
                    else
                    {
                        int id = 0;
                        if (gridNangSuatNhayKhau.Rows.Count > 0)
                        {
                            int Phongbanid_ns = 0;
                            int phongid = 0;
                            int mansid = 0;
                            string mahang = "";
                            byte nhomsize = 0;
                            byte idcachmay = 0;
                            if (Session["PhongBanID"] != null)
                                Phongbanid_ns = Convert.ToInt32(Session["PhongBanID"].ToString());
                            if (Session["userid"] != null)
                                mansid = Convert.ToInt32(Session["userid"].ToString());
                            DateTime dte = DateTime.Parse(txtDate.Text);
                            if (Session["MaHang"] != null)
                                mahang = Session["MaHang"].ToString();
                            if (Session["NhomSize"] != null)
                                nhomsize = byte.Parse(Session["NhomSize"].ToString());
                            if (Session["ID_CachMay"] != null)
                                idcachmay = byte.Parse(Session["ID_CachMay"].ToString());
                            if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                                phongid = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                            foreach (GridViewRow row in gridNangSuatNhayKhau.Rows)
                            {
                                if (row.RowType == DataControlRowType.DataRow)
                                {
                                    string lblID_CongDoanNhayKhau = ((Label)row.Cells[0].FindControl("lblID_CongDoanNhayKhau")).Text;
                                    string nangsuat = ((TextBox)row.Cells[0].FindControl("txtCongNhanNhayKhau")).Text;
                                    if (!string.IsNullOrEmpty(lblID_CongDoanNhayKhau) && !string.IsNullOrEmpty(nangsuat) && Convert.ToInt32(nangsuat) > 0)
                                    {
                                        DataRow dr = dtNhayKhau.NewRow();
                                        int idcongdoan = int.Parse(lblID_CongDoanNhayKhau);
                                        int ns = int.Parse(nangsuat);
                                        if (ns > 0 && idcongdoan > 0)
                                        {
                                            dr["MaHang"] = mahang.Trim();
                                            dr["PhongBanID_NS"] = Phongbanid_ns;
                                            dr["PhongBanID"] = phongid;
                                            dr["NhomSize"] = nhomsize;
                                            dr["ID_CachMay"] = idcachmay;
                                            dr["ID_CongDoan"] = idcongdoan;
                                            dr["MaNS_ID"] = mansid;
                                            dr["Ngay"] = dte.Date;
                                            dr["SoLuong_NhayKhau"] = ns;
                                            dtNhayKhau.Rows.Add(dr);
                                        }
                                    }
                                }
                            }
                            if (dtNhayKhau != null && dtNhayKhau.Rows.Count > 0)
                            {
                                var parameter = new SqlParameter("@dtSoLuong", SqlDbType.Structured);
                                parameter.Value = dtNhayKhau;
                                parameter.TypeName = "dbo.udt_web_LCB_NhayKhau_Update_SoLuong";
                                string sqlQuery = "[dbo].[pr_Web_LCB_NhayKhau_Update_SoLuong_TrangNhayKhau_UDT] @dtSoLuong";
                                id = id + db.Database.ExecuteSqlCommand(sqlQuery, parameter);
                            }
                            if (id != 0)
                            {
                                callstore();
                                Response.Redirect("NangSuatCongNhan.aspx");
                            }
                        }
                    }
                }
                divBTN.Visible = true;
            }
            catch (Exception ex) { }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            Response.Redirect("NangSuatCongNhan");
        }
    }
}