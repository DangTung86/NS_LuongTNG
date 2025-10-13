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
    public partial class MTKN_MucTieu_PhongBan : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        TNGLuongDbContact dblog = null;
        int iID_LanDanhGia, iID_NhanSu;
        protected void Page_Load(object sender, EventArgs e)

        {
            //btnclose.ServerClick += new EventHandler(btnclose_Click);
            CloseMessage.ServerClick += new EventHandler(btnCloseMessage_Click);

            btnSearchTenCN.ServerClick += new EventHandler(btnSearchTenCN_Click);
            //btnSearchTenCD.ServerClick += new EventHandler(btnSearchTenCD_Click);
            //closeNhanSuCongDoan.ServerClick += new EventHandler(closeNhanSuCongDoan_Click);

            db = new TNG_CTLDbContact();
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    Session["DTLanDanhGia"] = null;


                    int phongid_ns = 0;
                    if (Session["ChucVu"] != null)
                        phongid_ns = Convert.ToInt32(Session["ChucVu"].ToString());
                    if (Session["TenDonVi"] != null)
                        txtTenDonVi.Text = Session["TenDonVi"].ToString();
                    View_ToMay tm = new View_ToMay();
                    tm = db.View_ToMay.Where(x => x.PhongBanID == phongid_ns).SingleOrDefault();
                    if (tm != null)
                        Session["DonViID_CN"] = tm.DonViID;



                    loadDataToMay();
                    loadDataLanDanhGia();


                    getDataset();


                }
            }
            else
                Response.Redirect("Login.aspx");


        }
        protected void loadDataToMay()
        {
            try
            {
                string donviid = "";
                if (Session["DonViID_CN"] != null)
                    donviid = Session["DonViID_CN"].ToString();
                List<View_ToMay> lst = new List<View_ToMay>();
                lst = db.View_ToMay.Where(x => x.DonViID == donviid).OrderBy(x => x.TenPhongban).ToList();
                if (lst != null && lst.Count > 0)
                {
                    Session["lstToMay"] = lst;
                    ddlToMay.DataSource = lst;
                    ddlToMay.DataBind();
                    if (Session["ChucVu"] != null)
                    {
                        ddlToMay.SelectedValue = Session["ChucVu"].ToString();
                    }
                }

            }
            catch (Exception ex) { }
        }
        protected void loadDataLanDanhGia()
        {
            try
            {
                object[] sqlPr =
                {
                    new SqlParameter("@iErrorCode", DBNull.Value),

                };
                string sqlQuery = "[dbo].[pr_MTKN_SelectAll_LanDanhGia] @iErrorCode";
                List<MTKN_DM_LanDanhGia> lst = new List<MTKN_DM_LanDanhGia>();
                lst = db.Database.SqlQuery<MTKN_DM_LanDanhGia>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    //DataTable dt = ultils.CreateDataTable<MTKN_DM_LanDanhGia>(lst);
                    //Session["DTLanDanhGia"] = dt;
                    ddlLanDanhGia.DataSource = lst;
                    ddlLanDanhGia.DataBind();

                    //if (ddlLanDanhGia.SelectedValue != null && ddlLanDanhGia.SelectedValue.ToString() != "" && ddlLanDanhGia.SelectedValue.ToString() != "0")
                    //{
                    //    long id = long.Parse(ddlLanDanhGia.SelectedValue.ToString());
                    //    DataRow row = dt.Select("STT=" + id).Single();

                    //}
                }
            }
            catch (Exception ex) { }
        }

        protected void getDataset()
        {
            try
            {
                int phongbanid = 0;

                int tomay = 0;
                int lanDanhGia = 0;

                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    tomay = Convert.ToInt32(ddlToMay.SelectedValue.ToString());

                if (Session["ChucVu"] != null)
                    phongbanid = Convert.ToInt32(Session["ChucVu"].ToString());

                if (ddlLanDanhGia.SelectedValue != null && ddlLanDanhGia.SelectedValue.ToString() != "")
                    lanDanhGia = Convert.ToInt32(ddlLanDanhGia.SelectedValue.ToString());

                object[] sqlPro =
                {
                    new SqlParameter("@PhongBanID", phongbanid),
                    new SqlParameter("@ID_LanDanhGia", lanDanhGia),
                    new SqlParameter("@iErrorCode", DBNull.Value),
                };
                string sqlQuery2 = "[dbo].[pr_MTKN_MucTieu_LoaiMay_PhongBan_Select_wDonVi_PhongBan] @PhongBanID,@ID_LanDanhGia,@iErrorCode";

                List<MTKN_MucTieu_XepHang> lst = new List<MTKN_MucTieu_XepHang>();
                lst = db.Database.SqlQuery<MTKN_MucTieu_XepHang>(sqlQuery2, sqlPro).ToList();

                if (lst != null && lst.Count > 0)
                {
                    txtSoNS.Text = lst.FirstOrDefault().SoNS.ToString();
                    //Session["DataNhanSu"] = lst;
                    gridLoaiMay.DataSource = lst;
                    gridLoaiMay.DataBind();
                }
                else
                {
                    //Session["DataUser"] = null;
                }
            }
            catch (Exception ex) { }
        }

        protected void getDatasetChiTietLoaiMay(int lanDanhGia, int idLoaiMay, int phongBanID)
        {
            try
            {

                object[] sqlPr =
            {
                    new SqlParameter("@PhongBanID", phongBanID),
                    new SqlParameter("@ID_LoaiMay", idLoaiMay),
                    new SqlParameter("@ID_LanDanhGia", lanDanhGia),
                    new SqlParameter("@iErrorCode", DBNull.Value),
                };

                string sqlQuery = "[dbo].[pr_MTKN_MucTieu_LoaiMay_PhongBan_DanhSachThucHien] @PhongBanID,@ID_LoaiMay,@ID_LanDanhGia,@iErrorCode";
                List<MTKN_MucTieu_LoaiMay> lst2 = new List<MTKN_MucTieu_LoaiMay>();
                lst2 = db.Database.SqlQuery<MTKN_MucTieu_LoaiMay>(sqlQuery, sqlPr).ToList();

                if (lst2.Count > 0)
                {
                    //pnameCNDN.Visible = true;
                    txtLanDanhGia.Value = lanDanhGia.ToString();
                    txtID_LoaiMay.Value = idLoaiMay.ToString();
                    txtPhongPhanID.Value = phongBanID.ToString();
                    gridDanhSachLoaiMay.DataSource = lst2.OrderByDescending(m => m.ID_XepHang);
                    gridDanhSachLoaiMay.DataBind();

                }
            }
            catch (Exception ex) { }
        }

        protected void gridDanhSachLoaiMay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlXepHang = (DropDownList)e.Row.FindControl("ddlXepHang");
                    DropDownList ddlMucTieu = (DropDownList)e.Row.FindControl("ddlMucTieu");

                    object[] sqlPr = { new SqlParameter("@iErrorCode", DBNull.Value) };
                    string sqlQuery = "[dbo].[pr_MTKN_Select_DM_XepHang] @iErrorCode";
                    List<MTKN_DM_XepHang> lst = new List<MTKN_DM_XepHang>();

                    lst = db.Database.SqlQuery<MTKN_DM_XepHang>(sqlQuery, sqlPr).ToList();

                    if (ddlXepHang != null)
                    {
                        ddlXepHang.DataSource = lst;
                        ddlXepHang.DataTextField = "XepHang";
                        ddlXepHang.DataValueField = "ID_XepHang";
                        ddlXepHang.DataBind();
                        ddlXepHang.Items.Insert(0, new ListItem("", "0"));
                    }
                    if (ddlMucTieu != null)
                    {
                        ddlMucTieu.DataSource = lst;
                        ddlMucTieu.DataTextField = "XepHang";
                        ddlMucTieu.DataValueField = "ID_XepHang";
                        ddlMucTieu.DataBind();
                        ddlMucTieu.Items.Insert(0, new ListItem("", "0"));
                    }
                    if (DataBinder.Eval(e.Row.DataItem, "ID_XepHang") != null && !string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ID_XepHang").ToString()))
                        ddlXepHang.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ID_XepHang").ToString();

                    if (DataBinder.Eval(e.Row.DataItem, "ID_XepHang_MucTieu") != null && !string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ID_XepHang_MucTieu").ToString()))

                        ddlMucTieu.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ID_XepHang_MucTieu").ToString();

                }
            }
            catch (Exception ex) { }
        }

        protected void gridLoaiMay_ItemCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int lanDanhGia = 0;
                int phongbanid = 0;
                if (ddlLanDanhGia.SelectedValue != null && ddlLanDanhGia.SelectedValue.ToString() != "")
                    lanDanhGia = Convert.ToInt32(ddlLanDanhGia.SelectedValue.ToString());
                int id = Convert.ToInt32(e.CommandArgument.ToString());

                if (Session["ChucVu"] != null)
                    phongbanid = Convert.ToInt32(Session["ChucVu"].ToString());
                //iID_NhanSu = id;
                //iID_LanDanhGia = lanDanhGia;
                if (e.CommandName.Equals("ChiTietNhanSu"))
                {

                    getDatasetChiTietLoaiMay(lanDanhGia, id, phongbanid);
                    addthismodalContact.Style["display"] = "block";
                }
            }
            catch { }
        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
        }
        protected void btnCloseMessage_Click(object sender, EventArgs e)
        {
            messageShow.Style["display"] = "none";
        }
        protected void ddlLanDanhGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLanDanhGia.SelectedValue != null && ddlLanDanhGia.SelectedValue.ToString() != "" && ddlLanDanhGia.SelectedValue.ToString() != "0")
            {
                iID_LanDanhGia = Convert.ToInt32(ddlLanDanhGia.SelectedValue.ToString());
                getDataset();
            }
        }
        protected void btnSearchTenCN_Click(object sender, EventArgs e)
        {

            List<DanhSachNhanSuGiaoKeHoach> lsts = new List<DanhSachNhanSuGiaoKeHoach>();
            if (Session["DataNhanSu"] != null)
            {
                lsts = Session["DataNhanSu"] as List<DanhSachNhanSuGiaoKeHoach>;
            }

            if (lsts != null && lsts.Count > 0)
            {

                if (!(string.IsNullOrEmpty(txtsearchTenCN.Value)))
                {
                    var ns = lsts.Where(m => m.HoTen.ToLower().Contains(txtsearchTenCN.Value.ToLower()) || m.MaNS.ToLower().Contains(txtsearchTenCN.Value.ToLower())).ToList();
                    //lvUser.DataSource = ns;
                    //lvUser.DataBind();
                }
                else
                {
                    //lvUser.DataSource = lsts;
                    //lvUser.DataBind();
                }
            }
        }
        protected void btnSaveMucTieu_Click(object sender, EventArgs e)
        {
            try
            {
                int soNS = Convert.ToInt32(txtSoNS.Text);

                int phongbanid = 0;
                int lanDanhGia = 0;
                string maNS = "";

                if (Session["ChucVu"] != null)
                    phongbanid = Convert.ToInt32(Session["ChucVu"].ToString());

                if (ddlLanDanhGia.SelectedValue != null && ddlLanDanhGia.SelectedValue.ToString() != "")
                    lanDanhGia = Convert.ToInt32(ddlLanDanhGia.SelectedValue.ToString());
                if (Session["username"] != null)
                    maNS = Session["username"].ToString();


                DataTable dtMucTieu = new DataTable("dtMucTieu_PhongBan");
                dtMucTieu.Columns.Add(new DataColumn("ID_MucTieu", typeof(int)));
                dtMucTieu.Columns.Add(new DataColumn("PhongBanID", typeof(int)));
                dtMucTieu.Columns.Add(new DataColumn("ID_LoaiMay", typeof(int)));
                dtMucTieu.Columns.Add(new DataColumn("ID_LanDanhGia", typeof(int)));
                dtMucTieu.Columns.Add(new DataColumn("TyLeDat", typeof(int)));
                dtMucTieu.Columns.Add(new DataColumn("TenDangNhap_Lap", typeof(string)));
                int id = 0;

                if (gridLoaiMay.Rows.Count > 0)
                {
                    DataRow dr = null;

                    foreach (GridViewRow row in gridLoaiMay.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            int lblID_MucTieu = Convert.ToInt32(((Label)row.Cells[0].FindControl("lblID_MucTieu")).Text);
                            int lblID_LoaiMay = Convert.ToInt32(((Label)row.Cells[0].FindControl("lblID_LoaiMay")).Text);
                            int soNguoi = Convert.ToInt32(((TextBox)row.Cells[0].FindControl("txtSoNguoi")).Text);

                            if (soNguoi > soNS)
                            {

                                lblMessenger.Text = "Không nhập vượt quá số nhân sự của tổ!";
                                messageShow.Style["display"] = "block";
                                divThongBao.Style["display"] = "block";
                                return;
                            }
                            dr = dtMucTieu.NewRow();
                            dr["ID_MucTieu"] = lblID_MucTieu;
                            dr["PhongBanID"] = phongbanid;
                            dr["ID_LoaiMay"] = lblID_LoaiMay;
                            dr["ID_LanDanhGia"] = lanDanhGia;
                            dr["TyLeDat"] = (Convert.ToDecimal(soNguoi) / soNS) * 100;
                            dr["TenDangNhap_Lap"] = maNS;
                            dtMucTieu.Rows.Add(dr);
                        }
                    }
                }
                if (dtMucTieu != null && dtMucTieu.Rows.Count > 0)
                {
                    db = new TNG_CTLDbContact();
                    var parameter = new SqlParameter("@dtMucTieu_PhongBan", SqlDbType.Structured);
                    parameter.Value = dtMucTieu;
                    parameter.TypeName = "dbo.udt_MTKM_MucTieu_PhongBan";
                    string sqlQuery = "[dbo].[pr_MTKN_MucTieu_PhongBan_Update_UDT] @dtMucTieu_PhongBan";
                    id = id + db.Database.ExecuteSqlCommand(sqlQuery, parameter);
                }
                if (id != 0)
                {
                    lblMessenger.Text = "Đã cập nhật";
                    messageShow.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                    getDataset();
                }
                else
                {

                    lblMessenger.Text = "Đã cập nhật";
                    messageShow.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                    getDataset();


                }

            }
            catch (Exception ex)
            {
                lblMessenger.Text = "Lỗi cập nhật.";
                messageShow.Style["display"] = "block";
                divThongBao.Style["display"] = "block";
            }
        }

        protected void txtSearchTenMay_Click(object sender, EventArgs e)
        {
            List<ListCongDoan_KeHoach_CongNhan> lsts = new List<ListCongDoan_KeHoach_CongNhan>();
            if (Session["DataCongDoan"] != null)
            {
                lsts = Session["DataCongDoan"] as List<ListCongDoan_KeHoach_CongNhan>;
            }


        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
        }
        protected void btnSaveDanhGia_Click(object sender, EventArgs e)
        {

            try
            {

                DataTable dtXepHang = new DataTable("dtXepHang_NhanSu");
                dtXepHang.Columns.Add(new DataColumn("ID", typeof(int)));
                dtXepHang.Columns.Add(new DataColumn("MaNS_ID", typeof(int)));
                dtXepHang.Columns.Add(new DataColumn("ID_LoaiMay", typeof(int)));
                dtXepHang.Columns.Add(new DataColumn("ID_XepHang", typeof(int)));
                dtXepHang.Columns.Add(new DataColumn("ID_LanDanhGia", typeof(int)));
                dtXepHang.Columns.Add(new DataColumn("ID_XepHang_MucTieu", typeof(int)));
                int id = 0;
                if (gridDanhSachLoaiMay.Rows.Count > 0)
                {
                    DataRow dr = null;
                    foreach (GridViewRow row in gridDanhSachLoaiMay.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {


                            DropDownList ddlXepHang = (DropDownList)row.Cells[0].FindControl("ddlXepHang");
                            DropDownList ddlMucTieu = (DropDownList)row.Cells[0].FindControl("ddlMucTieu");

                            int lblID = ((Label)row.Cells[0].FindControl("lblID")).Text == "" ? 0 : Convert.ToInt32(((Label)row.Cells[0].FindControl("lblID")).Text);
                            int lblMaNS_ID = Convert.ToInt32(((Label)row.Cells[0].FindControl("lblMaNS_ID")).Text);
                            //int lblID_LoaiMay = Convert.ToInt32(((Label)row.Cells[0].FindControl("lblID_LoaiMay")).Text);

                            if (ddlXepHang.SelectedValue.ToString() != "")
                            {
                                if (ddlXepHang.SelectedValue != null && ddlXepHang.SelectedValue.ToString() != "")
                                {
                                    dr = dtXepHang.NewRow();
                                    dr["ID"] = lblID;
                                    dr["MaNS_ID"] = lblMaNS_ID;
                                    dr["ID_LoaiMay"] = txtID_LoaiMay.Value;
                                    dr["ID_XepHang"] = ddlXepHang.SelectedValue.ToString();
                                    dr["ID_LanDanhGia"] = txtLanDanhGia.Value;
                                    dr["ID_XepHang_MucTieu"] = ddlMucTieu.SelectedValue.ToString();
                                    dtXepHang.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    if (dtXepHang != null && dtXepHang.Rows.Count > 0)
                    {
                        db = new TNG_CTLDbContact();
                        var parameter = new SqlParameter("@dtXepHang_NhanSu", SqlDbType.Structured);
                        parameter.Value = dtXepHang;
                        parameter.TypeName = "dbo.udt_MTKN_XepHang_NhanSu";
                        string sqlQuery = "[dbo].[pr_MTKN_XepHang_CongNhan_Update_UDT] @dtXepHang_NhanSu";
                        id = id + db.Database.ExecuteSqlCommand(sqlQuery, parameter);
                    }
                    if (id != 0)
                    {
                        lblMessenger.Text = "Đã cập nhật";
                        addthismodalContact.Style["display"] = "block";
                        divThongBao.Style["display"] = "block";
                        getDataset();
                    }
                    else
                    {

                        lblMessenger.Text = "Đã cập nhật";
                        addthismodalContact.Style["display"] = "block";
                        divThongBao.Style["display"] = "block";
                        getDataset();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessenger.Text = "Lỗi cập nhật.";
                addthismodalContact.Style["display"] = "block";
                divThongBao.Style["display"] = "block";
            }



        }
    }
}