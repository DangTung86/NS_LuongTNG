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
    public partial class MTKN_XepHang_CongNhan : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        TNGLuongDbContact dblog = null;
        int iID_LanDanhGia, iID_NhanSu;
        protected void Page_Load(object sender, EventArgs e)

        {
            btnclose.ServerClick += new EventHandler(btnclose_Click);
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
        protected void loadDataGrid(GridView gridDanhSachNhanVien, int mansid)
        {
            try
            {
                if (Session["DataUser"] != null)
                {
                    DataTable dt = Session["DataUser"] as DataTable;
                    dt = dt.Select("MaNS_ID = " + mansid).CopyToDataTable();
                    dt.DefaultView.Sort = "NgayApDung ASC";
                    gridDanhSachNhanVien.DataSource = dt;
                    gridDanhSachNhanVien.DataBind();
                }
                else
                {
                    List<ListCongDoan_KeHoach_TenCongDoan> lst = new List<ListCongDoan_KeHoach_TenCongDoan>();
                    DataTable dt = ultils.CreateDataTable<ListCongDoan_KeHoach_TenCongDoan>(lst);
                    dt.Rows.Add(dt.NewRow());
                    gridDanhSachNhanVien.Rows[0].Cells.Clear();
                    gridDanhSachNhanVien.Rows[0].Cells.Add(new TableCell());
                    gridDanhSachNhanVien.Rows[0].Cells[0].ColumnSpan = 4;
                    gridDanhSachNhanVien.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridDanhSachNhanVien.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

                }
            }
            catch (Exception ex)
            {
                List<ListCongDoan_KeHoach_TenCongDoan> lst = new List<ListCongDoan_KeHoach_TenCongDoan>();
                DataTable dt = ultils.CreateDataTable<ListCongDoan_KeHoach_TenCongDoan>(lst);
                dt.Rows.Add(dt.NewRow());
                gridDanhSachNhanVien.DataSource = dt;
                gridDanhSachNhanVien.DataBind();
                gridDanhSachNhanVien.Rows[0].Cells.Clear();
                gridDanhSachNhanVien.Rows[0].Cells.Add(new TableCell());
                gridDanhSachNhanVien.Rows[0].Cells[0].ColumnSpan = 4;
                gridDanhSachNhanVien.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gridDanhSachNhanVien.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
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
                    new SqlParameter("@iPhongBanID", phongbanid),
                    new SqlParameter("@iID_LanDanhGia", lanDanhGia),
                    new SqlParameter("@iErrorCode", DBNull.Value),
                };
                string sqlQuery2 = "[dbo].[pr_MTKN_Select_ThongTinNhanSu_ToMay] @iPhongBanID,@iID_LanDanhGia,@iErrorCode";

                List<MTKN_DanhSachNhanSuTo> lst = new List<MTKN_DanhSachNhanSuTo>();
                lst = db.Database.SqlQuery<MTKN_DanhSachNhanSuTo>(sqlQuery2, sqlPro).ToList();

                if (lst != null && lst.Count > 0)
                {

                    Session["DataNhanSu"] = lst;
                    gridNhanVien.DataSource = lst;
                    gridNhanVien.DataBind();
                }
                else
                {
                    Session["DataUser"] = null;
                }
            }
            catch (Exception ex) { }
        }

        protected void getDatasetChiTietLoaiMay(int lanDanhGia, int idNhanSu)
        {
            try
            {

                object[] sqlPr =
            {
                    new SqlParameter("@iID_LanDanhGia", lanDanhGia),
                    new SqlParameter("@iMaNS_ID", idNhanSu),
                    new SqlParameter("@iErrorCode", DBNull.Value),
                };

                string sqlQuery = "[dbo].[pr_MTKN_XepHang_NhanSu] @iID_LanDanhGia,@iMaNS_ID,@iErrorCode";
                List<MTKN_XepHang_NhanSu> lst2 = new List<MTKN_XepHang_NhanSu>();
                lst2 = db.Database.SqlQuery<MTKN_XepHang_NhanSu>(sqlQuery, sqlPr).ToList();

                if (lst2.Count > 0)
                {
                    //pnameCNDN.Visible = true;
                    txtLanDanhGia.Value = lanDanhGia.ToString();
                    txtMaNSID.Value = idNhanSu.ToString();
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

        protected void gridNhanVien_ItemCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int lanDanhGia = 0;
                if (ddlLanDanhGia.SelectedValue != null && ddlLanDanhGia.SelectedValue.ToString() != "")
                    lanDanhGia = Convert.ToInt32(ddlLanDanhGia.SelectedValue.ToString());
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                //iID_NhanSu = id;
                //iID_LanDanhGia = lanDanhGia;
                if (e.CommandName.Equals("ChiTietNhanSu"))
                {

                    getDatasetChiTietLoaiMay(lanDanhGia, id);
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

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
        }
        protected void btnCloseNhanSuCongDoan_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
        }

        protected void btnSaveDanhGia_Click(object sender, EventArgs e)
        {

            try
            {
                var s = txtMaNSID.Value;
                var ss = txtLanDanhGia.Value;
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

                            int lblID = Convert.ToInt32(((Label)row.Cells[0].FindControl("lblID")).Text);
                            int lblID_LoaiMay = Convert.ToInt32(((Label)row.Cells[0].FindControl("lblID_LoaiMay")).Text);

                            if (ddlXepHang.SelectedValue.ToString() != "")
                            {
                                if (ddlXepHang.SelectedValue != null && ddlXepHang.SelectedValue.ToString() != "" && ddlXepHang.SelectedValue.ToString() != "0")
                                {
                                    dr = dtXepHang.NewRow();
                                    dr["ID"] = lblID;
                                    dr["MaNS_ID"] = txtMaNSID.Value;
                                    dr["ID_LoaiMay"] = lblID_LoaiMay;
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
        protected void txtSearchTenMay_Click(object sender, EventArgs e)
        {
            List<ListCongDoan_KeHoach_CongNhan> lsts = new List<ListCongDoan_KeHoach_CongNhan>();
            if (Session["DataCongDoan"] != null)
            {
                lsts = Session["DataCongDoan"] as List<ListCongDoan_KeHoach_CongNhan>;
            }


        }
    }
}