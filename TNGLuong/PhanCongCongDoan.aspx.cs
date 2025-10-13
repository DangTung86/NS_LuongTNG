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
    public partial class PhanCongCongDoan : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        TNGLuongDbContact dblog = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            CloseMessage.ServerClick += new EventHandler(btnCloseMessage_Click);

            btnSearchTenCN.ServerClick += new EventHandler(btnSearchTenCN_Click);
            btnSearchTenCD.ServerClick += new EventHandler(btnSearchTenCD_Click);
            closeNhanSuCongDoan.ServerClick += new EventHandler(closeNhanSuCongDoan_Click);

            db = new TNG_CTLDbContact();
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    Session["DTMaHang"] = null;
                    Session["ListSelectedData"] = null;
                    Session["NhomSize"] = null;
                    Session["ID_CachMay"] = null;
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

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
                    loadDataMaHang();

                    bool bQuyen = getQuyenCapNhatCongDoan();
                    var s = Session["NhomCongViec_ID"].ToString();
                    if (bQuyen == true)
                    {
                        getDataset();
                    }
                    else
                    {
                        lblMessenger.Text = "Không thể phân công đoạn cho mã hàng này!";
                        messageShow.Style["display"] = "block";
                        divThongBao.Style["display"] = "block";
                    }
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
        protected void loadDataMaHang()
        {
            try
            {
                int donviid = 0;
                int phongid = 0;
                if (Session["DonViID_CN"] != null)
                    donviid = Convert.ToInt32(Session["DonViID_CN"].ToString());
                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    phongid = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                object[] sqlPr =
                {
                    new SqlParameter("@iPhongBanID", phongid),
                    new SqlParameter("@iThang", DateTime.Now.Month),
                    new SqlParameter("@iNam", DateTime.Now.Year),
                    new SqlParameter("@iErrorCode", DBNull.Value),

                };
                string sqlQuery = "[dbo].[pr_WEB_LCB_KeHoach_NhanVien_CongDoan_Select_DanhSachMaHang] @iPhongBanID,@iThang,@iNam,@iErrorCode";
                List<MaHangPhanCongDoan> lst = new List<MaHangPhanCongDoan>();
                lst = db.Database.SqlQuery<MaHangPhanCongDoan>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    DataTable dt = ultils.CreateDataTable<MaHangPhanCongDoan>(lst);
                    Session["DTMaHang"] = dt;
                    ddlMaHang.DataSource = lst;
                    ddlMaHang.DataBind();

                    if (ddlMaHang.SelectedValue != null && ddlMaHang.SelectedValue.ToString() != "" && ddlMaHang.SelectedValue.ToString() != "0")
                    {
                        long id = long.Parse(ddlMaHang.SelectedValue.ToString());
                        DataRow row = dt.Select("STT=" + id).Single();
                        if (row != null)
                        {
                            Session["MaHang"] = row["MaHang"].ToString();
                            Session["NhomSize"] = row["NhomSize"].ToString();
                            Session["ID_CachMay"] = row["ID_CachMay"].ToString();
                        }
                    }
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

                    //float total = float.Parse(dt.Rows[0]["TongTyLe"].ToString());
                    decimal total = getTongTyLeGiaoKhoan(mansid);
                    gridDanhSachNhanVien.FooterRow.ForeColor = System.Drawing.Color.Red;
                    gridDanhSachNhanVien.FooterRow.Cells[1].Text = "Tổng mã hàng";
                    gridDanhSachNhanVien.FooterRow.Cells[1].Font.Bold = true;
                    gridDanhSachNhanVien.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    gridDanhSachNhanVien.FooterRow.Cells[2].Text = string.Format("{0:#,0.##}%", total);
                    gridDanhSachNhanVien.FooterRow.Cells[2].Font.Bold = true;
                    gridDanhSachNhanVien.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
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

        protected decimal getTongTyLeGiaoKhoan(int sMaNS_ID)
        {
            try
            {
                string sMaHang = "";
                int phongid = 0;
                byte idcachmay = 0;
                byte nhomsize = 0;
                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    phongid = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                if (Session["NhomSize"] != null)
                    nhomsize = byte.Parse(Session["NhomSize"].ToString());
                if (Session["ID_CachMay"] != null)
                    idcachmay = byte.Parse(Session["ID_CachMay"].ToString());
                if (Session["MaHang"] != null)
                    sMaHang = Session["MaHang"].ToString();
                object[] sqlPr =
                {
                    new SqlParameter("@iPhongBanID", phongid),
                    new SqlParameter("@iThang", DateTime.Now.Month),
                    new SqlParameter("@iNam", DateTime.Now.Year),
                    new SqlParameter("@iMaNS_ID", sMaNS_ID),
                    new SqlParameter("@sMaHang", sMaHang),
                    new SqlParameter("@byNhomSize", nhomsize),
                    new SqlParameter("@byID_CachMay", idcachmay),
                    new SqlParameter("@iErrorCode", DBNull.Value)

                };
                string sqlQuery = "[dbo].[pr_WEB_LCB_KeHoach_NhanVien_CongDoan_Select_TongTyLe_GiaoKhoan] @iPhongBanID,@iThang,@iNam,@iMaNS_ID,@sMaHang,@byNhomSize,@byID_CachMay,@iErrorCode";
                List<decimal> lst = new List<decimal>();
                lst = db.Database.SqlQuery<decimal>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                    return decimal.Parse(lst[0].ToString());
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        protected bool getQuyenCapNhatCongDoan()
        {
            bool chk = false;
            try
            {
                var today = DateTime.Today;
                int phongbanid = 0;
                int donviid = 0;
                int tomay = 0;
                int idcachmay = 0;
                int nhomsize = 0;
                string mahang = "";
                if (Session["NhomCongViec_ID"].ToString() != "2")
                {
                    return true;
                }
                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    tomay = Convert.ToInt32(ddlToMay.SelectedValue.ToString());

                if (Session["ChucVu"] != null)
                    phongbanid = Convert.ToInt32(Session["ChucVu"].ToString());
                if (Session["NhomSize"] != null)
                    nhomsize = byte.Parse(Session["NhomSize"].ToString());
                if (Session["ID_CachMay"] != null)
                    idcachmay = byte.Parse(Session["ID_CachMay"].ToString());
                if (Session["MaHang"] != null)
                    mahang = Session["MaHang"].ToString();
                if (Session["DonViID_CN"] != null)
                    donviid = Convert.ToInt32(Session["DonViID_CN"].ToString());

                object[] sqlPr =
                        {
                        new SqlParameter("@sMaHang", mahang),
                        new SqlParameter("@iPhongBanID", phongbanid),
                        new SqlParameter("@byNhomSize", nhomsize),
                        new SqlParameter("@byID_CachMay", idcachmay),
                        new SqlParameter("@iThang", today.Month),
                        new SqlParameter("@iNam", today.Year),
                        new SqlParameter("@iErrorCode", DBNull.Value),
                    };

                string sqlQuery = "[dbo].[pr_LCB_KeHoach_NhanVien_CongDoan_CheckCapNhatSoDoChuyen]  @iThang,@iNam, @sMaHang, @iPhongBanID,@byNhomSize,@byID_CachMay,@iErrorCode";
                List<bool> lst2 = new List<bool>();
                chk = db.Database.SqlQuery<bool>(sqlQuery, sqlPr).SingleOrDefault();
            }
            catch (Exception EX)
            {
                chk = false;
            }
            return chk;
        }
        protected void getDataset()
        {
            try
            {
                var today = DateTime.Today;
                int phongbanid = 0;
                int donviid = 0;
                int tomay = 0;
                int idcachmay = 0;
                int nhomsize = 0;
                string mahang = "";

                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    tomay = Convert.ToInt32(ddlToMay.SelectedValue.ToString());

                if (Session["ChucVu"] != null)
                    phongbanid = Convert.ToInt32(Session["ChucVu"].ToString());
                if (Session["NhomSize"] != null)
                    nhomsize = byte.Parse(Session["NhomSize"].ToString());
                if (Session["ID_CachMay"] != null)
                    idcachmay = byte.Parse(Session["ID_CachMay"].ToString());
                if (Session["MaHang"] != null)
                    mahang = Session["MaHang"].ToString();
                if (Session["DonViID_CN"] != null)
                    donviid = Convert.ToInt32(Session["DonViID_CN"].ToString());

                object[] sqlPro =
                {
                    new SqlParameter("@iPhongBanID", phongbanid),
                    new SqlParameter("@iThang", today.Month),
                    new SqlParameter("@iNam", today.Year),
                    new SqlParameter("@iErrorCode", DBNull.Value),
                };
                string sqlQuery2 = "[dbo].[pr_LCB_KeHoach_NhanVien_Select_DanhSachNhanSu_GiaoKeHoach_wThangNam_And_PhongBan] @iPhongBanID,@iThang,@iNam,@iErrorCode";

                List<DanhSachNhanSuGiaoKeHoach> lst = new List<DanhSachNhanSuGiaoKeHoach>();
                lst = db.Database.SqlQuery<DanhSachNhanSuGiaoKeHoach>(sqlQuery2, sqlPro).ToList();

                if (lst != null && lst.Count > 0)
                {

                    Session["DataNhanSu"] = lst;

                    object[] sqlPr =
                    {
                        new SqlParameter("@sMaHang", mahang),
                        new SqlParameter("@iPhongBanID", phongbanid),
                        new SqlParameter("@byNhomSize", nhomsize),
                        new SqlParameter("@byID_CachMay", idcachmay),
                        new SqlParameter("@iThang", today.Month),
                        new SqlParameter("@iNam", today.Year),
                        new SqlParameter("@iErrorCode", DBNull.Value),
                    };

                    string sqlQuery = "[dbo].[pr_LCB_KeHoach_NhanVien_CongDoan_Select_wThangNam_MaHang_And_PhongBan] @sMaHang, @iPhongBanID,@byNhomSize,@byID_CachMay, @iThang,@iNam,@iErrorCode";
                    List<ListCongDoan_KeHoach> lst2 = new List<ListCongDoan_KeHoach>();
                    lst2 = db.Database.SqlQuery<ListCongDoan_KeHoach>(sqlQuery, sqlPr).ToList();

                    object[] sqlPr1 =
                    {
                        new SqlParameter("@sMaHang", mahang),
                        new SqlParameter("@iPhongBanID", phongbanid),
                        new SqlParameter("@iDonViID", donviid),
                        new SqlParameter("@byNhomSize", nhomsize),
                        new SqlParameter("@byID_CachMay", idcachmay),
                        new SqlParameter("@iThang", today.Month),
                        new SqlParameter("@iNam", today.Year),
                        new SqlParameter("@iErrorCode", DBNull.Value),
                    };
                    string sqlQuery1 = "[dbo].[pr_LCB_KeHoach_NhanVien_Select_ThietKeChuyen_wMaHang_And_PhongBan] @sMaHang, @iPhongBanID,@iDonViID,@byNhomSize,@byID_CachMay, @iThang,@iNam,@iErrorCode";
                    List<ListCongDoanCN> lst1 = new List<ListCongDoanCN>();

                    lst1 = db.Database.SqlQuery<ListCongDoanCN>(sqlQuery1, sqlPr1).ToList();


                    List<ListCongDoan_KeHoach_TenCongDoan> listCongDoan = new List<ListCongDoan_KeHoach_TenCongDoan>();
                    foreach (var item in lst2)
                    {
                        if (item != null)
                        {
                            var s = lst1.Where(m => m.ID_CongDoan == item.ID_CongDoan).FirstOrDefault();
                            ListCongDoan_KeHoach_TenCongDoan congDoan = new ListCongDoan_KeHoach_TenCongDoan();
                            if (s != null)
                            {
                                congDoan.ID_CongDoan = item.ID_CongDoan;
                                congDoan.MaNS_ID = item.MaNS_ID;
                                congDoan.NgayApDung = item.Ngay_ApDung.ToString("yyyy-MM-dd");
                                congDoan.TenCongDoan = s.TenCongDoan;
                                congDoan.TenCum = s.TenCum;
                                congDoan.TyLe_Giao = float.Parse(item.TyLe_Giao.ToString());
                                congDoan.TongTyLe = float.Parse(item.TongTyLe.ToString());
                                listCongDoan.Add(congDoan);
                            }

                        }

                    }

                    DataTable dt = ultils.CreateDataTable<ListCongDoan_KeHoach_TenCongDoan>(listCongDoan);
                    Session["DataUser"] = dt;

                }
                else
                {
                    Session["DataUser"] = null;
                }
                if (lst != null && lst.Count > 0)
                {
                    //pnameCNDN.Visible = true;
                    lvUser.DataSource = lst;
                    lvUser.DataBind();

                }
                else
                {
                    Session["DataListCN"] = null;
                }

                //else
                //pnameCNDN.Visible = false;
            }
            catch (Exception ex) { }
        }
        protected void getDatasetChiTiet(int mansid)
        {
            try
            {
                var today = DateTime.Today;
                int phongbanid = 0;
                int donviid = 0;
                int tomay = 0;
                int idcachmay = 0;
                int nhomsize = 0;
                string mahang = "";

                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    tomay = Convert.ToInt32(ddlToMay.SelectedValue.ToString());

                if (Session["ChucVu"] != null)
                    phongbanid = Convert.ToInt32(Session["ChucVu"].ToString());
                if (Session["NhomSize"] != null)
                    nhomsize = byte.Parse(Session["NhomSize"].ToString());
                if (Session["ID_CachMay"] != null)
                    idcachmay = byte.Parse(Session["ID_CachMay"].ToString());
                if (Session["MaHang"] != null)
                    mahang = Session["MaHang"].ToString();
                if (Session["DonViID_CN"] != null)
                    donviid = Convert.ToInt32(Session["DonViID_CN"].ToString());

                object[] sqlPro =
                {
                    new SqlParameter("@iPhongBanID", phongbanid),
                    new SqlParameter("@iThang", today.Month),
                    new SqlParameter("@iNam", today.Year),
                    new SqlParameter("@iErrorCode", DBNull.Value),
                };
                string sqlQuery2 = "[dbo].[pr_LCB_KeHoach_NhanVien_Select_DanhSachNhanSu_GiaoKeHoach_wThangNam_And_PhongBan] @iPhongBanID,@iThang,@iNam,@iErrorCode";

                List<DanhSachNhanSuGiaoKeHoach> lst = new List<DanhSachNhanSuGiaoKeHoach>();
                lst = db.Database.SqlQuery<DanhSachNhanSuGiaoKeHoach>(sqlQuery2, sqlPro).ToList();


                if (lst != null && lst.Count > 0)
                {
                    object[] sqlPr =
                {
                    new SqlParameter("@sMaHang", mahang),
                    new SqlParameter("@iPhongBanID", phongbanid),
                    new SqlParameter("@byNhomSize", nhomsize),
                    new SqlParameter("@byID_CachMay", idcachmay),
                    new SqlParameter("@iThang", today.Month),
                    new SqlParameter("@iNam", today.Year),
                    new SqlParameter("@iErrorCode", DBNull.Value),
                };

                    string sqlQuery = "[dbo].[pr_LCB_KeHoach_NhanVien_CongDoan_Select_wThangNam_MaHang_And_PhongBan] @sMaHang, @iPhongBanID,@byNhomSize,@byID_CachMay, @iThang,@iNam,@iErrorCode";
                    List<ListCongDoan_KeHoach> lst2 = new List<ListCongDoan_KeHoach>();
                    lst2 = db.Database.SqlQuery<ListCongDoan_KeHoach>(sqlQuery, sqlPr).ToList();

                    object[] sqlPr1 =
           {
                    new SqlParameter("@sMaHang", mahang),
                    new SqlParameter("@iPhongBanID", phongbanid),
                    new SqlParameter("@iDonViID", donviid),
                    new SqlParameter("@byNhomSize", nhomsize),
                    new SqlParameter("@byID_CachMay", idcachmay),
                    new SqlParameter("@iThang", today.Month),
                    new SqlParameter("@iNam", today.Year),
                    new SqlParameter("@iErrorCode", DBNull.Value),
                };
                    string sqlQuery1 = "[dbo].[pr_LCB_KeHoach_NhanVien_Select_ThietKeChuyen_wMaHang_And_PhongBan] @sMaHang, @iPhongBanID,@iDonViID,@byNhomSize,@byID_CachMay, @iThang,@iNam,@iErrorCode";
                    List<ListCongDoanCN> lst1 = new List<ListCongDoanCN>();

                    lst1 = db.Database.SqlQuery<ListCongDoanCN>(sqlQuery1, sqlPr1).ToList();


                    List<ListCongDoan_KeHoach_CongNhan> listCongDoanCN = new List<ListCongDoan_KeHoach_CongNhan>();
                    foreach (var item in lst1)
                    {
                        if (item != null)
                        {
                            var s = lst2.Where(m => m.ID_CongDoan == item.ID_CongDoan && m.MaNS_ID == mansid).FirstOrDefault();
                            var tong = lst2.Where(n => n.ID_CongDoan == item.ID_CongDoan).Sum(m => m.TyLe_Giao);
                            ListCongDoan_KeHoach_CongNhan congDoanCN = new ListCongDoan_KeHoach_CongNhan();
                            if (s != null)
                            {
                                congDoanCN.MaNS_ID = mansid;
                                congDoanCN.ID_CongDoan = item.ID_CongDoan;
                                congDoanCN.NgayApDung = s.Ngay_ApDung.ToString("yyyy-MM-dd");
                                congDoanCN.TenCongDoan = item.TenCongDoan;
                                congDoanCN.TenCum = item.TenCum;
                                congDoanCN.TGCN = item.TGCN;
                                congDoanCN.TongKH = tong.ToString("0.##");
                                congDoanCN.TyLe_Giao = s.TyLe_Giao.ToString("0.##");
                                congDoanCN.HeSoK = item.HeSoK.ToString("0.##");
                                listCongDoanCN.Add(congDoanCN);
                            }
                            else
                            {

                                congDoanCN.MaNS_ID = mansid;
                                congDoanCN.ID_CongDoan = item.ID_CongDoan;
                                congDoanCN.NgayApDung = null;
                                congDoanCN.TenCongDoan = item.TenCongDoan;
                                congDoanCN.TenCum = item.TenCum;
                                congDoanCN.TGCN = item.TGCN;
                                congDoanCN.TongKH = tong.ToString("0.##");
                                congDoanCN.TyLe_Giao = null;
                                congDoanCN.HeSoK = item.HeSoK.ToString("0.##");
                                listCongDoanCN.Add(congDoanCN);
                            }
                        }

                    }
                    if (listCongDoanCN != null && listCongDoanCN.Count > 0)
                    {
                        Session["DataCongDoan"] = listCongDoanCN;
                        gridPhanCongCongDoan.DataSource = listCongDoanCN;
                        gridPhanCongCongDoan.DataBind();
                    }
                }




            }
            catch (Exception ex) { }
        }
        protected void getDatasetChiTietNhanSuCongDoan(int idcongdoan)
        {
            try
            {
                var today = DateTime.Today;
                int phongbanid = 0;
                int donviid = 0;
                int idcachmay = 0;
                int nhomsize = 0;
                string mahang = "";

                if (Session["ChucVu"] != null)
                    phongbanid = Convert.ToInt32(Session["ChucVu"].ToString());

                if (Session["NhomSize"] != null)
                    nhomsize = byte.Parse(Session["NhomSize"].ToString());

                if (Session["ID_CachMay"] != null)
                    idcachmay = byte.Parse(Session["ID_CachMay"].ToString());

                if (Session["MaHang"] != null)
                    mahang = Session["MaHang"].ToString();

                if (Session["DonViID_CN"] != null)
                    donviid = Convert.ToInt32(Session["DonViID_CN"].ToString());

                object[] sqlPr =
            {
                    new SqlParameter("@sID_CongDoan", idcongdoan),
                    new SqlParameter("@iPhongBanID", phongbanid),
                    new SqlParameter("@byNhomSize", nhomsize),
                    new SqlParameter("@byID_CachMay", idcachmay),
                    new SqlParameter("@iThang", today.Month),
                    new SqlParameter("@iNam", today.Year),
                    new SqlParameter("@iErrorCode", DBNull.Value),
                };

                string sqlQuery = "[dbo].[pr_LCB_KeHoach_NhanVien_CongDoan_Select_NhanSu_CongDoan_NgayThang] @sID_CongDoan, @iPhongBanID,@byNhomSize,@byID_CachMay, @iThang,@iNam,@iErrorCode";
                List<ListCongDoan_KeHoach> lst2 = new List<ListCongDoan_KeHoach>();
                lst2 = db.Database.SqlQuery<ListCongDoan_KeHoach>(sqlQuery, sqlPr).ToList();

                var listNSCD = new List<ListCongDoan_KeHoach_NhanSu>();
                if (lst2 != null && lst2.Count > 0)
                {
                    object[] sqlPro =
                    {
                    new SqlParameter("@iPhongBanID", phongbanid),
                    new SqlParameter("@iThang", today.Month),
                    new SqlParameter("@iNam", today.Year),
                    new SqlParameter("@iErrorCode", DBNull.Value),
                };
                    string sqlQuery2 = "[dbo].[pr_LCB_KeHoach_NhanVien_Select_DanhSachNhanSu_GiaoKeHoach_wThangNam_And_PhongBan] @iPhongBanID,@iThang,@iNam,@iErrorCode";

                    List<DanhSachNhanSuGiaoKeHoach> lst = new List<DanhSachNhanSuGiaoKeHoach>();

                    lst = db.Database.SqlQuery<DanhSachNhanSuGiaoKeHoach>(sqlQuery2, sqlPro).ToList();


                    foreach (var item in lst2)
                    {
                        if (item != null)
                        {
                            ListCongDoan_KeHoach_NhanSu cdns = new ListCongDoan_KeHoach_NhanSu();
                            var ns = lst.Where(m => m.MaNS_ID == item.MaNS_ID).FirstOrDefault();
                            cdns.MaNS_ID = item.MaNS_ID;
                            cdns.ID_CongDoan = item.ID_CongDoan;
                            cdns.Ngay_ApDung = item.Ngay_ApDung;
                            cdns.HoTen = ns.HoTen;
                            cdns.TyLe_Giao = item.TyLe_Giao.ToString("0.##");
                            listNSCD.Add(cdns);
                        }

                    }


                }
                if (listNSCD != null && listNSCD.Count > 0)
                {
                    gridChiTietNhanSuCongDoan.DataSource = listNSCD;
                    gridChiTietNhanSuCongDoan.DataBind();
                }
            }
            catch (Exception ex) { }
        }
        protected void lvUser_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.WebControls.GridView grid = e.Item.FindControl("gridDanhSachNhanSu") as System.Web.UI.WebControls.GridView;
                Label label = e.Item.FindControl("lblMaNS_ID") as Label;
                Label lblHoTen = e.Item.FindControl("lblHoTen") as Label;
                Label lblMaNS = e.Item.FindControl("lblMaNS") as Label;

                if (label != null && !string.IsNullOrEmpty(label.Text))
                {
                    int mansid = Convert.ToInt32(label.Text);
                    lblHoTen.ForeColor = System.Drawing.Color.Red;
                    lblMaNS.ForeColor = System.Drawing.Color.Red;
                    loadDataGrid(grid, mansid);
                }
            }
            catch (Exception ex) { }
        }

        protected void lvUser_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                dblog = new TNGLuongDbContact();
                View_Web_ThongTinNS nhansu = dblog.View_Web_ThongTinNS.Where(x => x.MaNS_ID == id).SingleOrDefault();
                if (nhansu != null)
                {
                    lblChiTiet.Text = "Chi tiết công đoạn cá nhân: " + nhansu.HoTen + " (" + nhansu.MaNS + ")";
                    lblPhongBan_ID.Text = nhansu.PhongBanID.ToString();
                    lblMaNS_IDCN.Text = nhansu.MaNS_ID.ToString();


                }

                if (e.CommandName.Equals("ChiTiet"))
                {
                    getDatasetChiTiet(id);
                    txtNgayApDung.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    addthismodalContact.Style["display"] = "block";
                }
            }
            catch { }
        }
        protected void gridPhanCongCongDoan_ItemCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());

                if (e.CommandName.Equals("ChiTietNhanSu"))
                {
                    getDatasetChiTietNhanSuCongDoan(id);
                    modalNhanSuCongDoan.Style["display"] = "block";
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
        protected void closeNhanSuCongDoan_Click(object sender, EventArgs e)
        {
            modalNhanSuCongDoan.Style["display"] = "none";
        }
        protected void btnHuy_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
        }
        protected void btnCloseNhanSuCongDoan_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
        }
        protected bool checkValueAm_CongDoan()
        {
            bool sus = true;
            foreach (GridViewRow row in gridPhanCongCongDoan.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txt = (TextBox)row.Cells[0].FindControl("txtKeHoach");
                    Label tong = (Label)row.Cells[0].FindControl("lblTongKH");

                    if (txt.Text != "")
                    {

                        if (double.Parse(txt.Text.Trim()) < 0 || double.Parse(txt.Text.Trim()) > 1.3)
                        {
                            sus = false;
                            break;
                        }
                    }
                }
            }
            return sus;
        }
        protected void btnSavePhanCong_Click(object sender, EventArgs e)
        {
            try
            {
                List<ListKeHoachCapNhatTuDong> listKHNV = new List<ListKeHoachCapNhatTuDong>();
                DataTable dtCongDoan = new DataTable();
                dtCongDoan.Columns.Add(new DataColumn("Thang", typeof(int)));
                dtCongDoan.Columns.Add(new DataColumn("Nam", typeof(int)));
                dtCongDoan.Columns.Add(new DataColumn("MaHang", typeof(string)));
                dtCongDoan.Columns.Add(new DataColumn("PhongBanID", typeof(int)));
                dtCongDoan.Columns.Add(new DataColumn("NhomSize", typeof(byte)));
                dtCongDoan.Columns.Add(new DataColumn("ID_CachMay", typeof(byte)));
                dtCongDoan.Columns.Add(new DataColumn("ID_CongDoan", typeof(int)));
                dtCongDoan.Columns.Add(new DataColumn("MaNS_ID", typeof(int)));
                dtCongDoan.Columns.Add(new DataColumn("PhongBanID_NS", typeof(int)));
                dtCongDoan.Columns.Add(new DataColumn("TyLe_Giao", typeof(decimal)));
                dtCongDoan.Columns.Add(new DataColumn("Ngay_ApDung", typeof(DateTime)));
                if (checkValueAm_CongDoan() == false)
                {
                    lblMessenger.Text = "Số lượng thực hiện không thể nhỏ hơn 0 hoặc lớn hơn 1.3!";
                    messageShow.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                    return;
                }

                else
                {
                    int id = 0;
                    if (gridPhanCongCongDoan.Rows.Count > 0)
                    {
                        DateTime dte = DateTime.Parse(txtNgayApDung.Text);
                        foreach (GridViewRow row in gridPhanCongCongDoan.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                byte lblNhomSize = 0;
                                byte lblID_CachMay = 0;
                                string mahang = "";
                                string lblID_CongDoan = ((Label)row.Cells[0].FindControl("lblID_CongDoan")).Text;
                                string lblMaNS = ((Label)row.Cells[0].FindControl("lblMaNS_ID")).Text;
                                int thang = dte.Month;
                                int nam = dte.Year;
                                string keHoach = ((TextBox)row.Cells[0].FindControl("txtKeHoach")).Text.Trim();
                                string phongbanid_ns = lblPhongBan_ID.Text;
                                int phongid = 0;

                                if (Session["ChucVu"] != null)
                                    phongid = Convert.ToInt32(Session["ChucVu"].ToString());
                                if (Session["NhomSize"] != null)
                                    lblNhomSize = byte.Parse(Session["NhomSize"].ToString());
                                if (Session["ID_CachMay"] != null)
                                    lblID_CachMay = byte.Parse(Session["ID_CachMay"].ToString());
                                if (Session["MaHang"] != null)
                                    mahang = Session["MaHang"].ToString();


                                if (!string.IsNullOrEmpty(lblID_CongDoan) && !string.IsNullOrEmpty(keHoach))
                                {
                                    ListKeHoachCapNhatTuDong khnv = new ListKeHoachCapNhatTuDong();
                                    decimal kh = decimal.Parse(keHoach);
                                    int mans = int.Parse(lblMaNS);
                                    int phongbanidns = int.Parse(phongbanid_ns);
                                    int idCongDoan = int.Parse(lblID_CongDoan);

                                    DataRow dr = dtCongDoan.NewRow();
                                    dr["Thang"] = thang;
                                    dr["Nam"] = nam;
                                    dr["MaHang"] = ddlMaHang.SelectedItem.Text.ToString().Trim();
                                    dr["PhongBanID"] = phongid;
                                    dr["NhomSize"] = lblNhomSize;
                                    dr["ID_CachMay"] = lblID_CachMay;
                                    dr["ID_CongDoan"] = idCongDoan;
                                    dr["MaNS_ID"] = mans;
                                    dr["PhongBanID_NS"] = phongbanidns;
                                    dr["Ngay_ApDung"] = dte.Date;
                                    dr["TyLe_Giao"] = kh;
                                    dtCongDoan.Rows.Add(dr);

                                    khnv.ID_CachMay = lblID_CachMay;
                                    khnv.MaHang = ddlMaHang.SelectedItem.Text.ToString().Trim();
                                    khnv.Nam = nam;
                                    khnv.Thang = thang;
                                    khnv.PhongBanID = phongbanidns;
                                    khnv.NhomSize = lblNhomSize;
                                    khnv.Ngay_ApDung = dte.Date;
                                    listKHNV.Add(khnv);
                                }
                            }
                        }
                        if (dtCongDoan != null && dtCongDoan.Rows.Count > 0)
                        {
                            var parameter = new SqlParameter("@dtKeHoach", SqlDbType.Structured);
                            parameter.Value = dtCongDoan;
                            parameter.TypeName = "dbo.udt_LCB_KeHoach_NhanVien_CongDoan";
                            string sqlQuery = "[dbo].[pr_WEB_LCB_KeHoach_NhanVien_CongDoan_Insert_UDT] @dtKeHoach";
                            id = id + db.Database.ExecuteSqlCommand(sqlQuery, parameter);
                        }
                        if (id != 0)
                        {
                            foreach (var item in listKHNV)
                            {
                                if (item != null)
                                {
                                    object[] sqlPr =
                                         {
                                                new SqlParameter("@sMaHang", item.MaHang),
                                                new SqlParameter("@iPhongBanID", item.PhongBanID),
                                                new SqlParameter("@byNhomSize", item.NhomSize),
                                                new SqlParameter("@byID_CachMay", item.ID_CachMay),
                                                new SqlParameter("@iThang", item.Thang),
                                                new SqlParameter("@iNam", item.Nam),
                                                new SqlParameter("@daNgay_ApDung", item.Ngay_ApDung),
                                                new SqlParameter("@iErrorCode", DBNull.Value),
                                            };
                                    string sqlQuery = "[dbo].[pr_LCB_KeHoach_NhanVien_TuDongTinh_KeHoach]@sMaHang, @iPhongBanID,@byNhomSize,@byID_CachMay, @iThang,@iNam,@daNgay_ApDung,@iErrorCode";
                                    id = id + db.Database.ExecuteSqlCommand(sqlQuery, sqlPr);

                                }
                            }
                            int phongbanidns = 0;
                            if (Session["ChucVu"] != null)
                                phongbanidns = Convert.ToInt32(Session["ChucVu"].ToString());
                            object[] sqlPrAuto =
                                         {
                                                new SqlParameter("@Thang", dte.Month),
                                                new SqlParameter("@Nam", dte.Year),
                                                new SqlParameter("@PhongBanID", phongbanidns),
                                            };
                            string sqlQueryAuto = "EXEC [TNG_Data].[dbo].[pr_LCB_KeHoach_NhanVien_SyncFromCTL_wThangNam_And_PhongBanID]@Thang,@Nam,@PhongBanID";
                            db.Database.CommandTimeout = 3600;
                            int sus = db.Database.ExecuteSqlCommand(sqlQueryAuto, sqlPrAuto);

                            addthismodalContact.Style["display"] = "none";
                            lblMessenger.Text = "Đã cập nhật công đoạn công nhân.";
                            messageShow.Style["display"] = "block";
                            divThongBao.Style["display"] = "block";
                            getDataset();
                        }
                        else
                        {
                            lblMessenger.Text = "Cập nhật không thành công";
                            messageShow.Style["display"] = "block";
                            divThongBao.Style["display"] = "block";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlMaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMaHang.SelectedValue != null && ddlMaHang.SelectedValue.ToString() != "" && ddlMaHang.SelectedValue.ToString() != "0")
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

                bool bQuyen = getQuyenCapNhatCongDoan();
                if (bQuyen)
                {
                    getDataset();
                }
                else
                {
                    lblMessenger.Text = "Không thể phân công đoạn cho mã hàng này!";
                    messageShow.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                }
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
                    lvUser.DataSource = ns;
                    lvUser.DataBind();
                }
                else
                {
                    lvUser.DataSource = lsts;
                    lvUser.DataBind();
                }
            }
        }
        protected void btnSearchTenCD_Click(object sender, EventArgs e)
        {
            List<ListCongDoan_KeHoach_CongNhan> lsts = new List<ListCongDoan_KeHoach_CongNhan>();
            if (Session["DataCongDoan"] != null)
            {
                lsts = Session["DataCongDoan"] as List<ListCongDoan_KeHoach_CongNhan>;
            }

            if (lsts != null && lsts.Count > 0)
            {

                if (!(string.IsNullOrEmpty(txtSearchTenCD.Value)))
                {
                    var ns = lsts.Where(m => m.TenCongDoan.ToLower().Contains(txtSearchTenCD.Value.ToLower())).ToList();
                    gridPhanCongCongDoan.DataSource = ns;
                    gridPhanCongCongDoan.DataBind();
                }
                else
                {
                    gridPhanCongCongDoan.DataSource = lsts;
                    gridPhanCongCongDoan.DataBind();
                }
            }
        }
    }
}