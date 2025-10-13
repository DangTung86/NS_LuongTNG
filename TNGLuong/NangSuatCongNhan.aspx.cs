using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TNGLuong.Models;
using TNGLuong.ModelsView;

namespace TNGLuong
{
    public partial class NangSuatCongNhan : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        string strPreviousRowID = string.Empty;
        int intSubTotalIndex = 1;
        int lanDanhGia = 0;
        int ID_NhanSu = 0;
        int phongbanid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new TNG_CTLDbContact();
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            btncloseHD.ServerClick += new EventHandler(btncloseHD_Click);
            btnSreach.ServerClick += new EventHandler(btnSearch_Click);

            if (Session["username"] != null)
            {
                ID_NhanSu = Int32.Parse(Session["userid"].ToString());
                if (!IsPostBack)
                {
                    if(Session["HetHopDong"] != null && Session["HetHopDong"].ToString() == "1")
                    {
                        btnNhapNhayKhau.Enabled = false;
                        btnSaveToMay.Enabled = false;
                        btnSaveNhayKhau.Enabled = false;
                        lblTrangThai.Text = "Hết hạn hợp đồng!";
                        lblTrangThai.ForeColor = Color.Red;
                    }
                    else
                    {
                        btnNhapNhayKhau.Enabled = true;
                        btnSaveToMay.Enabled = true;
                        btnSaveNhayKhau.Enabled = true;
                        lblTrangThai.Text = "";
                        lblTrangThai.ForeColor = Color.Black;
                    }
                    Session["dtNK"] = null;
                    Session["DiLam"] = null;
                    Session["IsKhoaMaHang"] = null;
                    Session["TableMaHang"] = null;
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Equals(("true").ToUpper()))
                        txtDate.Enabled = false;
                    else
                        txtDate.Enabled = true;


                    if (!string.IsNullOrEmpty(txtDate.Text))
                    {
                        Session["DiLam"] = KiemTraNgayDiLam(DateTime.Now);
                        loadDataMaHang();
                        if (ddlMaHang.SelectedValue != null && ddlMaHang.SelectedValue != "")
                        {
                            DateTime dte = Convert.ToDateTime(txtDate.Text);
                            if (Session["TableMaHang"] != null)
                            {
                                DataTable dt = Session["TableMaHang"] as DataTable;
                                DataRow dr = dt.Select("MaHang_ID = '" + ddlMaHang.SelectedValue.ToString() + "'").FirstOrDefault();
                                if (dr != null)
                                {
                                    Session["IsKhoaMaHang"] = bool.Parse(dr["IsKhoaMaHang"].ToString());
                                    Session["IsKCS"] = bool.Parse(dr["IsKCS"].ToString());
                                    Session["IsCNPT"] = bool.Parse(dr["IsCNPT"].ToString());
                                    lblSoLuong_CapBTP.Text = dr["SoLuong_CapBTP"].ToString();
                                    lblHieuSuat.Text = dr["HieuSuat"].ToString();
                                }
                                else
                                {
                                    Session["IsKhoaMaHang"] = null;
                                    Session["IsKCS"] = null;
                                    Session["IsCNPT"] = null;
                                    lblSoLuong_CapBTP.Text = "";
                                    lblHieuSuat.Text = "";
                                }
                            }
                        }
                        loadDataGridToMay();
                        loadGridNhayKhau();
                        checkNgayNhapNS();
                    }

                    loadDataLanDanhGia();
                    lanDanhGia = Convert.ToInt32(ddlLanDanhGia.SelectedValue.ToString());

                    phongbanid = Int32.Parse(Session["PhongBanID"].ToString());
                    getDatasetLoaiMay(); // load các cột loại máy
                    LoadData_XepHang(phongbanid, lanDanhGia);
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
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


        protected void LoadData_XepHang(int PhongBanID, int LanDanhGia)
            {
            gridKyNangCongNhan.Rows[0].Cells[0].Text = "Xếp hạng";
            var dt = Select_NhanSu_wPhongBanID_LanDanhGia_ByID_NhanSu(ID_NhanSu, LanDanhGia);
            if (dt == null)
            {
                gridKyNangCongNhan.Rows[0].Visible = false;
                return;

            }
            var lstLoaiMay = new List<string> { };

            for (int j = 1; j < gridKyNangCongNhan.Columns.Count; j++)
            {
                string tenLoaiMay = HttpUtility.HtmlDecode(gridKyNangCongNhan.HeaderRow.Cells[j].Text);
                var danhGia_LoaiMay = dt.Where(dg => dg.TenLoaiMay.Trim() == tenLoaiMay.Trim() && dg.ID_NhanSu == ID_NhanSu).FirstOrDefault();
                if (danhGia_LoaiMay == null || danhGia_LoaiMay.XepHang == null)
                {
                    gridKyNangCongNhan.Columns[j].Visible = false;
                    continue;

                }
                else gridKyNangCongNhan.Columns[j].Visible = true;
                string kyHieuXepHang = danhGia_LoaiMay.XepHang;
                if (kyHieuXepHang == null) continue;
                gridKyNangCongNhan.Rows[0].Cells[j].Text = kyHieuXepHang;
                string val = kyHieuXepHang.Trim().Substring(0, 1);

                loadColor(val, 0, j);
            }
        }


        protected List<MTKN_XepHang_NhanSu_New> Select_NhanSu_wPhongBanID_LanDanhGia_ByID_NhanSu(int ID_NhanSu, int lanDanhGia)
            {
                try
                {
                    object[] sqlPr =
                    {
                        new SqlParameter("@ID_NhanSu", ID_NhanSu),
                        new SqlParameter("@ID_LanDanhGia", lanDanhGia),
                        new SqlParameter("@iErrorCode", DBNull.Value)
                    };

                    string sqlQuery = "[dbo].[pr_MTKN_NhanSu_LoaiMay_XepHang_Select_wID_NhanSu] @ID_NhanSu, @ID_LanDanhGia, @iErrorCode";
                    DbContext dbc = new DbContext("name=TNG_QLKT");
                    List<MTKN_XepHang_NhanSu_New> lst2 = new List<MTKN_XepHang_NhanSu_New>();
                    lst2 = dbc.Database.SqlQuery<MTKN_XepHang_NhanSu_New>(sqlQuery, sqlPr).ToList();
                    dbc.Dispose();
                    return lst2;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }


        protected void loadColor(string val, int row, int col)
                {
                    switch (val)
                    {
                        case "A":
                            gridKyNangCongNhan.Rows[row].Cells[col].BackColor = Color.Blue;
                            break;
                        case "B":
                            gridKyNangCongNhan.Rows[row].Cells[col].BackColor = Color.Green;
                            break;
                        case "C":
                            gridKyNangCongNhan.Rows[row].Cells[col].BackColor = Color.Yellow;
                            break;
                        case "D":
                            gridKyNangCongNhan.Rows[row].Cells[col].BackColor = Color.Orange;
                            break;
                        case "E":
                            gridKyNangCongNhan.Rows[row].Cells[col].BackColor = Color.Red;
                            break;
                    }
                }


        protected void getDatasetLoaiMay()
        {

            int mans = Int32.Parse(Session["userid"].ToString());
            int phongbanid = Int32.Parse(Session["PhongBanID"].ToString());

            object[] sqlPro2 =
                {
                    new SqlParameter("@ID_PhongBan", phongbanid),
                    new SqlParameter("@ID_LanDanhGia", lanDanhGia),
                    new SqlParameter("@iErrorCode", DBNull.Value),
                };

            string sqlQuery = "[dbo].[pr_MTKN_NhanSu_LoaiMay_XepHang_Select_LoaiMay_New] @ID_PhongBan, @ID_LanDanhGia, @iErrorCode";
            DbContext dbc = new DbContext("name=TNG_QLKT");
            List<MTKN_NhanSu_LoaiMay_XepHang> lst2 = new List<MTKN_NhanSu_LoaiMay_XepHang>();
            lst2 = dbc.Database.SqlQuery<MTKN_NhanSu_LoaiMay_XepHang>(sqlQuery, sqlPro2).ToList();
            dbc.Dispose();


            foreach (var data in lst2)
            {
                TemplateField newColumn = new TemplateField();
                newColumn.HeaderText = data.TenLoaiMay;   // Set the header text
                newColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                gridKyNangCongNhan.Columns.Add(newColumn);
            }

            // Add an empty item to the data source
            List<MTKN_NhanSu_LoaiMay_XepHang> lst3 = new List<MTKN_NhanSu_LoaiMay_XepHang>() { new MTKN_NhanSu_LoaiMay_XepHang() };

            // Bind the data source to the GridView
            gridKyNangCongNhan.DataSource = lst3;
            gridKyNangCongNhan.DataBind();
           
        }


        protected void checkNgayNhapNS()
        {
            LCB_ThongSoHeThong tsht = new LCB_ThongSoHeThong();
            tsht = db.LCB_ThongSoHeThong.FirstOrDefault();
            if (tsht != null)
            {
                DateTime dte = Convert.ToDateTime(txtDate.Text);
                DateTime tungay = DateTime.Now.AddDays(-double.Parse(tsht.GiaTri_So.ToString()));
                if (DateTime.Compare(dte.Date, tungay.Date) >= 0 && DateTime.Compare(dte.Date, DateTime.Now.Date) <= 0)
                {
                    btnSaveToMay.Enabled = true;
                    btnSaveNhayKhau.Enabled = true;
                    btnNhapNhayKhau.Enabled = true;
                }
                else
                {
                    btnSaveToMay.Enabled = false;
                    btnSaveNhayKhau.Enabled = false;
                    btnNhapNhayKhau.Enabled = false;
                }
            }
        }

        protected void loadDataMaHang()
        {
            try
            {
                int idmans = 0;
                int phongid = 0;
                if (Session["userid"] != null)
                    idmans = Convert.ToInt32(Session["userid"].ToString());
                if (Session["PhongBanID"] != null)
                    phongid = Convert.ToInt32(Session["PhongBanID"].ToString());
                DateTime dte = Convert.ToDateTime(txtDate.Text);
                object[] sqlPr =
                {
                    new SqlParameter("@PhongBanID_NS", phongid),
                    new SqlParameter("@MaNS_ID", idmans),
                    new SqlParameter("@Ngay", DateTime.Parse(txtDate.Text).Date)
                };
                string sqlQuery = "[dbo].[pr_WEB_LCB_KeHoach_NhanVien_SelectMaHangByMaNSID_PhongID] @PhongBanID_NS,@MaNS_ID,@Ngay";
                List<ListMaHang> lst = new List<ListMaHang>();
                lst = db.Database.SqlQuery<ListMaHang>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {                    
                    if (Session["NhomCat_HoanThien"] != null && !Session["NhomCat_HoanThien"].ToString().ToLower().Contains("kiểm gấp"))
                    {
                        ListMaHang cls = new ListMaHang();
                        cls.MaHang = "Tất cả";
                        cls.MaHang_ID = 0;
                        cls.SoLuong_CapBTP = "";
                        cls.HieuSuat = "";
                        cls.IsKhoaMaHang = false;
                        cls.IsKCS = false;
                        cls.IsCNPT = false;
                        lst.Insert(0, cls);
                    }
                    if (Session["NhomCat_HoanThien"] != null && Session["NhomCat_HoanThien"].ToString().ToLower().Contains("kiểm gấp"))
                    {
                        ListMaHang cls = new ListMaHang();
                        cls.MaHang = "Tất cả";
                        cls.MaHang_ID = 0;
                        cls.SoLuong_CapBTP = "";
                        cls.HieuSuat = "";
                        cls.IsKhoaMaHang = false;
                        cls.IsKCS = true;
                        cls.IsCNPT = false;
                        lst.Insert(0, cls);
                    }
                    if (Session["NhomPhuTro"] != null && Session["NhomPhuTro"].ToString()== "5")
                    {
                        ListMaHang cls = new ListMaHang();
                        cls.MaHang = "Tất cả";
                        cls.MaHang_ID = 0;
                        cls.SoLuong_CapBTP = "";
                        cls.HieuSuat = "";
                        cls.IsKhoaMaHang = false;
                        cls.IsKCS = false;
                        cls.IsCNPT = true;
                        lst.Insert(0, cls);
                    }

                    Session["TableMaHang"] = ultils.CreateDataTable<ListMaHang>(lst);
                    Session["IsKhoaMaHang"] = lst[0].IsKhoaMaHang;
                    Session["IsKCS"] = lst[0].IsKCS;
                    Session["IsCNPT"] = lst[0].IsCNPT;

                    ddlMaHang.DataSource = lst;
                    ddlMaHang.DataBind();
                    //ddlMaHang.SelectedIndex = 0;
                }
            }
            catch (Exception ex) { }
        }

        protected void loadDataGridToMay()
        {
            try
              {
                string mahang = "";
                int m_iMaHangID = 0;
                int phongid = 0;
                int mansid = 0;
                bool m_bIsKCS = false;
                bool m_bIsCNPT = false;
                if (Session["userid"] != null)
                    mansid = Convert.ToInt32(Session["userid"].ToString());
                DateTime dte = Convert.ToDateTime(txtDate.Text);
                if (Session["PhongBanID"] != null)
                    phongid = Convert.ToInt32(Session["PhongBanID"].ToString());
                if (ddlMaHang.SelectedValue != null && ddlMaHang.SelectedValue.ToString() != "")
                {
                    mahang = ddlMaHang.SelectedItem.Text;
                    m_iMaHangID = Convert.ToInt32(ddlMaHang.SelectedValue.ToString());
                }
                string nameNSCache = "NS" + DateTime.Parse(txtDate.Text).ToString("dd-MM-yyyy");
                string sss = Session["DonViID_Cha"].ToString();

                if (Session["IsKCS"] != null)
                    m_bIsKCS = bool.Parse(Session["IsKCS"].ToString());
                if (Session["IsCNPT"] != null)
                    m_bIsCNPT = bool.Parse(Session["IsCNPT"].ToString());
                object[] sqlPr =
                {
                    new SqlParameter("@PhongBanID", phongid),
                    new SqlParameter("@MaNS_ID", mansid),
                    new SqlParameter("@Ngay", DateTime.Parse(txtDate.Text).Date),
                    new SqlParameter("@MaHang", mahang),
                    new SqlParameter("@MaHangID", m_iMaHangID),
                    new SqlParameter("@IsKCS", m_bIsKCS),
                    new SqlParameter("@IsCNPT", m_bIsCNPT)
                };
                string sqlQuery = "";
                if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Equals(("true").ToUpper()))
                    sqlQuery = "[dbo].[prDG_Web_LCB_KeHoach_NhanVien_Select_wNhanSu_And_Ngay] @PhongBanID,@MaNS_ID,@Ngay,@MaHang,@MaHangID,@IsKCS,@IsCNPT";
                else
                    sqlQuery = "[dbo].[pr_Web_LCB_KeHoach_NhanVien_Select_wNhanSu_And_Ngay] @PhongBanID,@MaNS_ID,@Ngay,@MaHang,@MaHangID,@IsKCS,@IsCNPT";
                List<ListNangSuat> lst = new List<ListNangSuat>();
                lst = db.Database.SqlQuery<ListNangSuat>(sqlQuery, sqlPr).ToList();

                DataTable dt = ultils.CreateDataTable<ListNangSuat>(lst);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gridNangSuatToMay.DataSource = dt;
                    gridNangSuatToMay.DataBind();

                    if (Session["NhomCat_HoanThien"] != null)
                    {
                        if (!Session["NhomCat_HoanThien"].ToString().ToLower().Contains("kiểm gấp"))
                        {
                            if (Session["NhomCat_HoanThien"].ToString() == "1")
                            {
                                gridNangSuatToMay.HeaderRow.Cells[3].Text = "Tổng SL cắt";
                            }
                            else if (Session["NhomCat_HoanThien"].ToString() == "3")
                            {
                                gridNangSuatToMay.HeaderRow.Cells[3].Text = "Tổng SL ra chuyền";
                            }
                            lblTieuDe_CapBPT.Visible = false;
                            lblSoLuong_CapBTP.Visible = false;
                            gridNangSuatToMay.Columns[1].Visible = false;
                            gridNangSuatToMay.Columns[2].Visible = true;
                            gridNangSuatToMay.Columns[3].Visible = true;
                            
                        }
                        else if (Session["NhomCat_HoanThien"].ToString().ToLower().Contains("kiểm gấp"))
                        {
                            //lblTieuDe_CapBPT.Visible = true;
                            //lblSoLuong_CapBTP.Visible = true;
                            gridNangSuatToMay.Columns[1].Visible = true;
                            gridNangSuatToMay.Columns[2].Visible = true;
                            gridNangSuatToMay.Columns[3].Visible = false;
                        }
                    }
                    else
                    {
                        if(Session["NhomPhuTro"] != null && Session["NhomPhuTro"].ToString() == "5")
                        {
                            gridNangSuatToMay.Columns[1].Visible = true;
                            gridNangSuatToMay.Columns[2].Visible = true;
                            gridNangSuatToMay.Columns[3].Visible = false;
                            gridNangSuatToMay.Columns[9].Visible = true;
                        }
                        else {
                            lblTieuDe_CapBPT.Visible = true;
                            lblSoLuong_CapBTP.Visible = true;
                            gridNangSuatToMay.Columns[1].Visible = true;
                            gridNangSuatToMay.Columns[2].Visible = false;
                            gridNangSuatToMay.Columns[3].Visible = false;
                            gridNangSuatToMay.Columns[9].Visible = false;
                        }
                    }

                    Session["tyle"] = dt.Rows[0]["Tyle"].ToString();
                    if (Session["HetHopDong"] != null && Session["HetHopDong"].ToString() == "1") return;
                    if (Session["IsKhoaMaHang"] != null && bool.Parse(Session["IsKhoaMaHang"].ToString()) == true)
                    {
                        btnSaveToMay.Enabled = false;
                        btnSaveNhayKhau.Enabled = false;
                        btnNhapNhayKhau.Enabled = false;
                        if(dt.Rows[0]["DaPD"].ToString() == "PD")
                            lblTrangThai.Text = "Đã duyệt";
                        else
                            lblTrangThai.Text = "Chưa duyệt";
                    }
                    else if (dt.Rows[0]["DaPD"].ToString() == "PD")
                    {
                        lblTrangThai.Text = "Đã duyệt";
                        btnSaveToMay.Enabled = false;
                        btnSaveNhayKhau.Enabled = false;
                        btnNhapNhayKhau.Enabled = false;
                        lblTrangThai.ForeColor = System.Drawing.Color.Blue;
                    }
                    else
                    {
                        btnSaveToMay.Enabled = true;
                        btnSaveNhayKhau.Enabled = true;
                        btnNhapNhayKhau.Enabled = true;
                        lblTrangThai.Text = "Chưa duyệt";
                        lblTrangThai.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    btnSaveToMay.Enabled = true;
                    btnSaveNhayKhau.Enabled = true;
                    btnNhapNhayKhau.Enabled = true;
                    LoadGridNull();
                }
            }
            catch (Exception ex) {
                LoadGridNull();
            }
        }

        protected void LoadGridNull()
        {
            List<ListNangSuat> lst = new List<ListNangSuat>();
            DataTable dt = ultils.CreateDataTable<ListNangSuat>(lst);
            DataRow row = dt.NewRow();
            row["MaHang"] = "";
            row["PhongBanID"] = 0;
            row["NhomSize"] = 0;
            row["ID_CachMay"] = 0;
            row["ID_CongDoan"] = 0;
            row["MaNS_ID"] = 0;
            row["Ngay"] = DateTime.Now;
            row["PhongBanID_NS"] = 0;
            row["BacCapDo"] = "";
            row["BacKyNang"] = "";
            row["KeHoach_NhanVien"] = 0;
            row["ThucHien_NhanVien"] = 0;
            row["DaPD"] = "";
            row["LuyKe"] = 0;
            row["LuyKe_CongDoan"] = 0;
            row["HieuSuat"] = 0;
            row["TenMaHang"] = "";
            row["TenCongDoan"] = "";
            row["TGCN"] = 0;
            row["DonGia"] = 0;
            row["STT_Cum"] = 0;
            row["ID_Cum"] = 0;
            row["STT_CongDoan"] = 0;
            row["TonTai"] = 0;
            row["PheDuyet"] = true;
            row["TenPhongban"] = "";
            row["Tyle"] = 0;
            row["IsKhoaMaHang"] = false;
            row["IsBTP"] = true;
            dt.Rows.Add(row);
            gridNangSuatToMay.DataSource = dt;
            gridNangSuatToMay.DataBind();
            gridNangSuatToMay.Rows[0].Cells.Clear();
            gridNangSuatToMay.Rows[0].Cells.Add(new TableCell());
            gridNangSuatToMay.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
            gridNangSuatToMay.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
            gridNangSuatToMay.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            if (Session["HetHopDong"] != null && Session["HetHopDong"].ToString() == "1") return;
            lblTrangThai.Text = "Chưa duyệt";
            lblTrangThai.ForeColor = System.Drawing.Color.Red;
        }
       
        protected DataTable loadDataNhayKhau()
        {
            try
            {
                int idmans = 0;
                int phongid = 0;
                if (Session["userid"] != null)
                    idmans = Convert.ToInt32(Session["userid"].ToString());
                if (Session["PhongBanID"] != null)
                    phongid = Convert.ToInt32(Session["PhongBanID"].ToString());

                DateTime dte = Convert.ToDateTime(txtDate.Text);
                string nameNKCache = "NK" + DateTime.Parse(txtDate.Text).ToString("dd-MM-yyyy");
                object[] sqlPr =
                {
                    new SqlParameter("@PhongBanID_NS", phongid),
                    new SqlParameter("@MaNS_ID", idmans),
                    new SqlParameter("@Ngay", dte)
                };
                string sqlQuery = "";
                if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Equals(("true").ToUpper()))
                    sqlQuery = "[dbo].[prDG_WEB_LCB_NhayKhau_Select_NangSuatCongNhan] @PhongBanID_NS,@MaNS_ID,@Ngay";//pr_LCB_NhayKhauCongNhan_WEB_Select_NangSuatCongNhan
                else
                    sqlQuery = "[dbo].[pr_WEB_LCB_NhayKhau_Select_NangSuatCongNhan] @PhongBanID_NS,@MaNS_ID,@Ngay";
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

        protected void loadGridNhayKhau()
        {
            try
            {
                DataTable dt = this.loadDataNhayKhau();
                if (dt != null && dt.Rows.Count > 0)
                {
                    Session["dtNK"] = dt;
                    gridNangSuatNhayKhau.DataSource = dt;
                    gridNangSuatNhayKhau.DataBind();
                }
                else
                {
                    Load_ResetGV_NK();
                }
            }
            catch
            {
                Load_ResetGV_NK();
            }
        }

        protected void Load_ResetGV_NK()
        {
            List<ListNSNhayKhau> lst = new List<ListNSNhayKhau>();
            DataTable dt = ultils.CreateDataTableStr<ListNSNhayKhau>(lst);
            dt.Rows.Add(dt.NewRow());
            gridNangSuatNhayKhau.DataSource = dt;
            gridNangSuatNhayKhau.DataBind();
            gridNangSuatNhayKhau.Rows[0].Cells.Clear();
            gridNangSuatNhayKhau.Rows[0].Cells.Add(new TableCell());
            gridNangSuatNhayKhau.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
            gridNangSuatNhayKhau.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
            gridNangSuatNhayKhau.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDate.Text) && DateTime.Compare(DateTime.Parse(txtDate.Text), DateTime.Parse("2020-07-31")) > 0 && DateTime.Compare(DateTime.Parse(txtDate.Text),DateTime.Today) <= 0)
            {
                Session["DiLam"] = KiemTraNgayDiLam(DateTime.Parse(txtDate.Text));
                ddlMaHang.Items.Clear();
                loadDataMaHang();
                loadDataGridToMay();
                loadGridNhayKhau();
                checkNgayNhapNS();
            }            
        }

        protected void ddlMaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dte = Convert.ToDateTime(txtDate.Text);
                if (Session["TableMaHang"] != null)
                {
                    DataTable dt = Session["TableMaHang"] as DataTable;
                    DataRow dr = dt.Select("MaHang_ID = '" + ddlMaHang.SelectedValue.ToString() + "'").FirstOrDefault();
                    if (dr != null)
                    {
                        Session["IsKhoaMaHang"] = bool.Parse(dr["IsKhoaMaHang"].ToString());
                        Session["IsKCS"] = bool.Parse(dr["IsKCS"].ToString());
                        Session["IsCNPT"] = bool.Parse(dr["IsCNPT"].ToString());
                        lblSoLuong_CapBTP.Text = dr["SoLuong_CapBTP"].ToString();
                        lblHieuSuat.Text = dr["HieuSuat"].ToString();                  
                    }
                    else
                    {
                        Session["IsKhoaMaHang"] = null;
                        Session["IsKCS"] = null;
                        Session["IsCNPT"] = null;
                        lblSoLuong_CapBTP.Text = "";
                        lblHieuSuat.Text = "";
                    }
                }

                loadDataGridToMay();
                if(Session["dtNK"] == null)
                {
                    Load_ResetGV_NK();
                }
            }
            catch { }
        }

        protected void gridNangSuatToMay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txt = (TextBox)e.Row.FindControl("txtThucHien_NhanVienToMay");
                CheckBox chk = (CheckBox)e.Row.FindControl("chkIsKhoaMaHang");
                if (chk != null && chk.Checked == true)
                {
                    txt.Enabled = false;
                } 
                else
                {
                    txt.Attributes.Add("type", "number");
                    if (string.IsNullOrEmpty(txt.Text) || txt.Text == "0")
                    {
                        txt.Attributes.Add("onclick", "this.value = '0';");
                    }
                    txt.Enabled = true;
                }       
            }
        }
        protected void gridNangSuatToMayTT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txt = (TextBox)e.Row.FindControl("txtThucHien_NhanVienToMayTT");
                if (Session["IsKhoaMaHang"] != null && bool.Parse(Session["IsKhoaMaHang"].ToString()) != true)
                {
                    txt.Enabled = true;
                    txt.Attributes.Add("type", "number");
                    if (!string.IsNullOrEmpty(txt.Text) && txt.Text == "0")
                    {
                        txt.Attributes.Add("onclick", "this.value = '';");
                    }
                }
                else
                {
                    txt.Enabled = false;
                }
            }
        }
        protected void gridNangSuatNhayKhau_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double hs = 1.2;
                Label hieusuat = (Label)e.Row.FindControl("lblHSNhayKhau");
                Label lblIsKhoaMaHang_NK = (Label)e.Row.FindControl("lblIsKhoaMaHang_NK");
                TextBox txt = (TextBox)e.Row.FindControl("txtCongNhanNhayKhau");
                txt.Attributes.Add("type", "number");
                if (!string.IsNullOrEmpty(txt.Text) && txt.Text == "0")
                    txt.Attributes.Add("onclick", "this.value = '';");
                if (hieusuat != null)
                    hieusuat.Text = string.Format("{0:0.#}",hs);
                if (lblIsKhoaMaHang_NK.Text == "1")
                    if(txt.Enabled == true)
                        txt.Enabled = false; 
            }
        }

        protected void btnSaveToMay_Click(object sender, EventArgs e)
        {
            if (Session["DiLam"] == null) return;
            if(Convert.ToBoolean(Session["DiLam"]) != true)
            {
                lblMessenger.Text = "Ngày báo năng suất là ngày nghỉ lễ, là ngày chủ nhật không đăng ký làm thêm hoặc là ngày không đi làm!";
                addthismodalContact.Style["display"] = "block";
                divThongBao.Style["display"] = "block";
                return;
            }
            try
            {
                decimal tyle = 0;
                if (Session["tyle"] != null)
                    tyle = decimal.Parse(Session["tyle"].ToString());
                DataTable dtToMay = new DataTable();
                dtToMay.Columns.Add(new DataColumn("MaHang", typeof(string)));
                dtToMay.Columns.Add(new DataColumn("MaHangID", typeof(int)));
                dtToMay.Columns.Add(new DataColumn("PhongBanID", typeof(int)));
                dtToMay.Columns.Add(new DataColumn("NhomSize", typeof(int)));
                dtToMay.Columns.Add(new DataColumn("ID_CachMay", typeof(int)));
                dtToMay.Columns.Add(new DataColumn("ID_CongDoan", typeof(int)));
                dtToMay.Columns.Add(new DataColumn("MaNS_ID", typeof(int)));
                dtToMay.Columns.Add(new DataColumn("Ngay", typeof(DateTime)));
                dtToMay.Columns.Add(new DataColumn("ThucHien_NhanVien", typeof(int)));
                if(checkValueAm_ToMay() == false)
                {
                    lblMessenger.Text = "Số lượng thực hiện không thể nhỏ hơn 0!";
                    addthismodalContact.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                    return;
                }
                else if (Session["NhomCat_HoanThien"] == null && checkLuyKeToMay() == false)
                {
                    lblMessenger.Text = "Số thực hiện lớn hơn số cấp Bán thành phẩm lên chuyền!";
                    addthismodalContact.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                    return;
                }
                else if(Session["NhomCat_HoanThien"] != null && checkLuyKeToCat_HoanThien() == false)
                {
                    if (Session["NhomCat_HoanThien"].ToString() == "1")
                    {
                        lblMessenger.Text = "Số thực hiện lớn hơn tổng số lượng cắt!";
                        addthismodalContact.Style["display"] = "block";
                        divThongBao.Style["display"] = "block";
                        return;
                    }
                    else if(Session["NhomCat_HoanThien"].ToString() == "3")
                    {
                        lblMessenger.Text = "Số thực hiện lớn hơn tổng số lượng ra chuyền!";
                        addthismodalContact.Style["display"] = "block";
                        divThongBao.Style["display"] = "block";
                        return;
                    }
                    else if (Session["NhomCat_HoanThien"].ToString().ToLower().Contains("kiểm gấp"))
                    {
                        lblMessenger.Text = "Số thực hiện lớn hơn tổng số lượng ra chuyền!";
                        addthismodalContact.Style["display"] = "block";
                        divThongBao.Style["display"] = "block";
                        return;
                    }
                }
                else if (checkValueNangSuat() == false)
                {
                    lblMessenger.Text = "Số lượng thực hiện không đúng, vui lòng kiểm tra lại.";
                    addthismodalContact.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                    return;
                }
                else
                { 
                    int id = 0;
                    bool m_bIsCNPT = false;
                    int m_iMaHangID = 0;
                    if (Session["IsCNPT"] != null)
                        m_bIsCNPT = bool.Parse(Session["IsCNPT"].ToString());
                    if (ddlMaHang.SelectedValue != null && ddlMaHang.SelectedValue.ToString() != "")
                        m_iMaHangID = Convert.ToInt32(ddlMaHang.SelectedValue.ToString());
                    if (gridNangSuatToMay.Rows.Count > 0)
                    {
                        DateTime dte = DateTime.Parse(txtDate.Text);
                        foreach (GridViewRow row in gridNangSuatToMay.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                string m_sMaHang = ((Label)row.Cells[0].FindControl("lblMaHang")).Text;
                                string lblMaHangID = ((Label)row.Cells[0].FindControl("lblMaHangID")).Text;
                                string lblNhomSize = ((Label)row.Cells[0].FindControl("lblNhomSizeToMay")).Text;                                
                                string lblID_CachMay = ((Label)row.Cells[0].FindControl("lblID_CachMayToMay")).Text;
                                string lblID_CongDoan = ((Label)row.Cells[0].FindControl("lblID_CongDoanToMay")).Text;
                                string nangsuat = ((TextBox)row.Cells[0].FindControl("txtThucHien_NhanVienToMay")).Text.Trim();
                                string phongid = ((Label)row.Cells[0].FindControl("lblPhongBanIDToMay")).Text; 
                                if (!string.IsNullOrEmpty(lblNhomSize) && !string.IsNullOrEmpty(lblID_CachMay) && !string.IsNullOrEmpty(lblID_CongDoan) && !string.IsNullOrEmpty(nangsuat))
                                {
                                    int siMaHangID = int.Parse(lblMaHangID);
                                    int nhomsize = int.Parse(lblNhomSize);
                                    int idcanhmay = int.Parse(lblID_CachMay);
                                    int idcongdoan = int.Parse(lblID_CongDoan);
                                    int mansid = Convert.ToInt32(Session["userid"].ToString());
                                    int ns = int.Parse(nangsuat);
                                    DataRow dr = dtToMay.NewRow();
                                    dr["MaHang"] = m_sMaHang;
                                    dr["MaHangID"] = siMaHangID;
                                    dr["PhongBanID"] = phongid;
                                    dr["NhomSize"] = nhomsize;
                                    dr["ID_CachMay"] = idcanhmay;
                                    dr["ID_CongDoan"] = idcongdoan;
                                    dr["MaNS_ID"] = mansid;
                                    dr["Ngay"] = dte.Date;
                                    dr["ThucHien_NhanVien"] = ns;
                                    dtToMay.Rows.Add(dr);
                                }
                            }
                        }
                        if(dtToMay != null && dtToMay.Rows.Count > 0)
                        {
                            bool m_bIsKCS = false;
                            bool bIsCNPT = false;
                            if (Session["IsKCS"] != null)
                                m_bIsKCS = bool.Parse(Session["IsKCS"].ToString());
                            if (Session["NhomPhuTro"] != null && Session["NhomPhuTro"].ToString() == "5")
                                bIsCNPT = true;
                            SqlParameter[] params_ = new SqlParameter[3];
                            params_[0] = new SqlParameter("@dtThucHien", SqlDbType.Structured);
                            params_[0].Value = dtToMay;
                            params_[0].TypeName = "dbo.udt_web_LCB_KeHoach_NhanVien_Update_ThucHien";
                            params_[1] = new SqlParameter("@IsKCS", m_bIsKCS);
                            params_[2] = new SqlParameter("@IsCNPT", bIsCNPT);

                            string sqlQuery = "[dbo].[pr_Web_LCB_KeHoach_NhanVien_Update_ThucHien_UDT] @dtThucHien,@IsKCS,@IsCNPT";
                            id = id + db.Database.ExecuteSqlCommand(sqlQuery, params_);
                        }
                        if (id != 0)
                        {
                            callstore();
                            loadDataGridToMay();
                            loadGridNhayKhau();
                            lblMessenger.Text = "Đã cập nhập năng suất.";
                            addthismodalContact.Style["display"] = "block";
                            divThongBao.Style["display"] = "block";
                        }
                        else
                        {
                            loadDataGridToMay();
                            loadGridNhayKhau();
                        }
                    }
                }
            }
            
            catch (Exception ex) { }
        }

        protected bool checkValueNangSuat()
        {
            decimal thanhtienNS = 0;
            decimal thanhtienNK = 0;
            decimal tong = 0;
            foreach (GridViewRow row in gridNangSuatToMay.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string itemNum = ((TextBox)row.Cells[5].FindControl("txtThucHien_NhanVienToMay")).Text;
                    string dongia = ((Label)row.Cells[2].FindControl("lblDonGiaToMay")).Text;
                    if (!string.IsNullOrEmpty(itemNum) && !string.IsNullOrEmpty(dongia) && decimal.Parse(itemNum.Trim()) > 0)
                    {
                        decimal tien = decimal.Parse(itemNum.Trim()) * decimal.Parse(dongia.Trim());
                        thanhtienNS = thanhtienNS + tien;
                    }
                }
            }
            foreach (GridViewRow row in gridNangSuatNhayKhau.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string itemNum = ((TextBox)row.Cells[3].FindControl("txtCongNhanNhayKhau")).Text;
                    string dongia = ((Label)row.Cells[2].FindControl("lblDonGiaNhayKhau")).Text;
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

        protected bool checkLuyKeToMay()
        {
            bool sus = true;
            double soluongBTP = 0;            
            foreach (GridViewRow row in gridNangSuatToMay.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txt = (TextBox)row.Cells[0].FindControl("txtThucHien_NhanVienToMay");
                    string lblTHGoc = ((Label)row.Cells[0].FindControl("lblThucHienGoc")).Text;
                    string sSoLuong_CapBTP = ((Label)row.Cells[0].FindControl("g_lblSoLuong_CapBTP")).Text;
                    double.TryParse(sSoLuong_CapBTP, out soluongBTP);
                    string luykecd = ((Label)row.Cells[0].FindControl("lblLuyKeCD")).Text;
                    CheckBox chk = (CheckBox)row.Cells[0].FindControl("chkIsBTP");
                    if (!string.IsNullOrEmpty(txt.Text) && !string.IsNullOrEmpty(lblTHGoc) && !string.IsNullOrEmpty(luykecd) && double.Parse(txt.Text.Trim()) > 0 && !lblTHGoc.Trim().Equals(txt.Text.Trim()) && chk != null && chk.Checked == true)
                    {
                        double totalSL = double.Parse(txt.Text.Trim()) + double.Parse(luykecd);
                        if (soluongBTP < totalSL)
                        {
                            txt.Text = "0";
                            sus = false;
                            break;
                        }
                    }
                }
            }
            return sus;
        }

        protected bool checkLuyKeToCat_HoanThien()
        {
            bool sus = true;
            foreach (GridViewRow row in gridNangSuatToMay.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txt = (TextBox)row.Cells[0].FindControl("txtThucHien_NhanVienToMay");
                    string lblTHGoc = ((Label)row.Cells[0].FindControl("lblThucHienGoc")).Text;
                    string luykecd = ((Label)row.Cells[0].FindControl("lblLuyKeCD")).Text;
                    string sSoLuong_CapBTP = ((Label)row.Cells[0].FindControl("g_lblSoLuong_CapBTP")).Text;
                    CheckBox chk = (CheckBox)row.Cells[0].FindControl("chkIsBTP");
                    if (!string.IsNullOrEmpty(txt.Text) && !string.IsNullOrEmpty(lblTHGoc) && !string.IsNullOrEmpty(luykecd) && double.Parse(txt.Text.Trim()) > 0 && !lblTHGoc.Trim().Equals(txt.Text.Trim()) && chk != null && chk.Checked == true)
                    {
                        double totalSL = double.Parse(txt.Text.Trim()) + double.Parse(luykecd);
                        double soluongBTP = double.Parse(sSoLuong_CapBTP);
                        if (soluongBTP < totalSL)
                        {
                            txt.Text = "0";
                            sus = false;
                            break;
                        }
                    }
                }
            }
            return sus;
        }

        protected bool checkLuyKeNhayKhau()
        {
            bool sus = true;
            foreach (GridViewRow row in gridNangSuatNhayKhau.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string lblLKBTP = ((Label)row.Cells[0].FindControl("lblLuyKeNK")).Text;
                    string lblSoLuong_CapBTP = ((Label)row.Cells[0].FindControl("lblSoLuong_CapBTP")).Text;
                    string lblIsBTP = ((Label)row.Cells[0].FindControl("lblIsBTP")).Text;
                    string lblTHGoc = ((Label)row.Cells[0].FindControl("lblThucHienGoc")).Text;
                    TextBox txt = (TextBox)row.Cells[0].FindControl("txtCongNhanNhayKhau");
                    if (!string.IsNullOrEmpty(lblIsBTP) && lblIsBTP.Trim() == "1")
                    {
                        if (!string.IsNullOrEmpty(txt.Text) && !string.IsNullOrEmpty(lblTHGoc) && !string.IsNullOrEmpty(lblLKBTP) && !string.IsNullOrEmpty(lblSoLuong_CapBTP) && !lblTHGoc.Trim().Equals(txt.Text.Trim()) && Convert.ToDouble(txt.Text.Trim()) > 0)
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

        protected bool checkValueAm_ToMay()
        {
            bool sus = true;
            foreach (GridViewRow row in gridNangSuatToMay.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txt = (TextBox)row.Cells[5].FindControl("txtThucHien_NhanVienToMay");
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

        protected void btnSaveNhayKhau_Click(object sender, EventArgs e)
        {
            if (Session["DiLam"] == null) return;
            if (Convert.ToBoolean(Session["DiLam"]) != true)
            {
                lblMessenger.Text = "Ngày báo năng suất là ngày nghỉ lễ, là ngày chủ nhật không đăng ký làm thêm hoặc là ngày không đi làm!";
                addthismodalContact.Style["display"] = "block";
                divThongBao.Style["display"] = "block";
                return;
            }
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
                    addthismodalContact.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                    return;
                }
                else if (checkLuyKeNhayKhau() == false)
                {
                    lblMessenger.Text = "Số thực hiện lớn hơn số cấp Bán thành phẩm lên chuyền!";
                    addthismodalContact.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                    return;
                }
                else if (checkValueNangSuat() == false)
                {
                    lblMessenger.Text = "Số lượng thực hiện không đúng, vui lòng kiểm tra lại.";
                    addthismodalContact.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                    return;
                }
                else
                {
                    int id = 0;
                    if (gridNangSuatNhayKhau.Rows.Count > 0)
                    {
                        int Phongbanid_ns = 0;
                        int mansid = 0;
                        if (Session["PhongBanID"] != null)
                            Phongbanid_ns = Convert.ToInt32(Session["PhongBanID"].ToString());
                        if (Session["userid"] != null)
                            mansid = Convert.ToInt32(Session["userid"].ToString());
                        DateTime dte = DateTime.Parse(txtDate.Text);

                        foreach (GridViewRow row in gridNangSuatNhayKhau.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                string lblID_CongDoanNhayKhau = ((Label)row.Cells[0].FindControl("lblID_CongDoanNhayKhau")).Text;
                                string lblPhongBanIDNhayKhau = ((Label)row.Cells[0].FindControl("lblPhongBanIDNhayKhau")).Text;
                                string lblMaHangNhayKhau = ((Label)row.Cells[0].FindControl("lblMaHangNhayKhau")).Text;
                                string lblNhomSizeNhayKhau = ((Label)row.Cells[0].FindControl("lblNhomSizeNhayKhau")).Text;
                                string lblID_CachMayNhayKhau = ((Label)row.Cells[0].FindControl("lblID_CachMayNhayKhau")).Text;
                                string nangsuat = ((TextBox)row.Cells[0].FindControl("txtCongNhanNhayKhau")).Text.Trim();
                                if (!string.IsNullOrEmpty(lblID_CongDoanNhayKhau) && !string.IsNullOrEmpty(nangsuat) && !string.IsNullOrEmpty(lblPhongBanIDNhayKhau) && !string.IsNullOrEmpty(lblMaHangNhayKhau) && !string.IsNullOrEmpty(lblNhomSizeNhayKhau) && !string.IsNullOrEmpty(lblID_CachMayNhayKhau))
                                {
                                    string mahang = lblMaHangNhayKhau.Split('/')[1].ToString();
                                    int idcongdoan = int.Parse(lblID_CongDoanNhayKhau);
                                    int Phongbanid = int.Parse(lblPhongBanIDNhayKhau);
                                    byte nhomsize = byte.Parse(lblNhomSizeNhayKhau);
                                    byte idcanhmay = byte.Parse(lblID_CachMayNhayKhau);
                                    int ns = int.Parse(nangsuat);
                                    DataRow dr = dtNhayKhau.NewRow();
                                    if (ns >= 0 && idcongdoan > 0)
                                    {
                                        dr["MaHang"] = mahang.Trim();
                                        dr["PhongBanID_NS"] = Phongbanid_ns;
                                        dr["PhongBanID"] = Phongbanid;
                                        dr["NhomSize"] = nhomsize;
                                        dr["ID_CachMay"] = idcanhmay;
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
                            string sqlQuery = "[dbo].[pr_Web_LCB_NhayKhau_Update_SoLuong_UDT] @dtSoLuong";
                            id = id + db.Database.ExecuteSqlCommand(sqlQuery, parameter);
                        }
                        if (id != 0)
                        {
                            callstore();
                            loadDataGridToMay();
                            loadGridNhayKhau();
                            lblMessenger.Text = "Đã cập nhật năng suất.";
                            addthismodalContact.Style["display"] = "block";
                            divThongBao.Style["display"] = "block";
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
            divThongBao.Style["display"] = "none";
            loadDataGridToMay();
            loadGridNhayKhau();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = this.loadDataNhayKhau();
            if (dt != null && dt.Rows.Count > 0)
            {
                StringBuilder filter = new StringBuilder();
                if (!(string.IsNullOrEmpty(txtsearch.Value)))
                {
                    filter.Append("MaHang Like '%" + txtsearch.Value + "%' OR TenCongDoan Like '%" + txtsearch.Value + "%'");
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
            catch(Exception ex) { }
        }

        protected void btnNhapNhayKhau_Click(object sender, EventArgs e)
        {
            if (Session["username"] != null)
            {
                if (Session["DiLam"] == null) return;
                if (Convert.ToBoolean(Session["DiLam"]) != true)
                {
                    lblMessenger.Text = "Ngày báo năng suất là ngày nghỉ lễ, là ngày chủ nhật không đăng ký làm thêm hoặc là ngày không đi làm!";
                    addthismodalContact.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                    return;
                }
                Response.Redirect("NhapNhayKhau.aspx");
            }
        }

        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            string nameNSCache = "NS" + DateTime.Parse(txtDate.Text).ToString("dd-MM-yyyy");
            string nameNKCache = "NK" + DateTime.Parse(txtDate.Text).ToString("dd-MM-yyyy");
            Cache[nameNSCache] = null;
            Cache[nameNKCache] = null;
            loadDataGridToMay();
            loadGridNhayKhau();
        }

        protected void btncloseHD_Click(object sender, EventArgs e)
        {
            loadDataGridToMay();
            loadGridNhayKhau();
            divHDThaoTac.Style["display"] = "none";
        }

        protected void loadGridHuongDanThaoTac(int idcongdoan)
        {
            try
            {
                string mahang = "";
                int iDonViID = 0;
                if (ddlMaHang.SelectedValue != null && ddlMaHang.SelectedValue.ToString() != "")
                    mahang = ddlMaHang.SelectedItem.Text;
                if(Session["DonViID_Cha"] != null && Session["DonViID_Cha"].ToString() != "")
                    iDonViID = int.Parse(Session["DonViID_Cha"].ToString());
                object[] sqlPr =
                {
                    new SqlParameter("@sMaHang", mahang),
                    new SqlParameter("@iDonViID", iDonViID),
                    new SqlParameter("@iID_CongDoan", idcongdoan),
                    new SqlParameter("@iErrorCode", DBNull.Value)
                };
                string sqlQuery = "[dbo].[pr_LCB_WEB_HuongDanThaoTacCongDoan_wID_CongDoan] @sMaHang,@iDonViID,@iID_CongDoan,@iErrorCode";
                List<HuongDanThaoTac> lst = new List<HuongDanThaoTac>();
                lst = db.Database.SqlQuery<HuongDanThaoTac>(sqlQuery, sqlPr).ToList();
                if(lst != null && lst.Count > 0)
                {
                    gridHuongDanThaoTac.DataSource = lst;
                    gridHuongDanThaoTac.DataBind();
                }
                else
                {
                    lst = new List<HuongDanThaoTac>();
                    DataTable dt = ultils.CreateDataTableStr<HuongDanThaoTac>(lst);
                    dt.Rows.Add(dt.NewRow());
                    gridHuongDanThaoTac.DataSource = dt;
                    gridHuongDanThaoTac.DataBind();
                    gridHuongDanThaoTac.Rows[0].Cells.Clear();
                    gridHuongDanThaoTac.Rows[0].Cells.Add(new TableCell());
                    gridHuongDanThaoTac.Rows[0].Cells[0].ColumnSpan = 4;
                    gridHuongDanThaoTac.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridHuongDanThaoTac.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch(Exception ex) {
                List<HuongDanThaoTac> lst = new List<HuongDanThaoTac>();
                DataTable dt = ultils.CreateDataTableStr<HuongDanThaoTac>(lst);
                dt.Rows.Add(dt.NewRow());
                gridHuongDanThaoTac.DataSource = dt;
                gridHuongDanThaoTac.DataBind();
                gridHuongDanThaoTac.Rows[0].Cells.Clear();
                gridHuongDanThaoTac.Rows[0].Cells.Add(new TableCell());
                gridHuongDanThaoTac.Rows[0].Cells[0].ColumnSpan = 4;
                gridHuongDanThaoTac.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gridHuongDanThaoTac.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void gridHuongDanThaoTac_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "ID_NhomThaoTac") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "ID_NhomThaoTac").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "ID_NhomThaoTac") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "ID_NhomThaoTac") != null))
            {
                GridView grdViewOrders = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "Ten_NhomThaoTac").ToString();
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
                if (DataBinder.Eval(e.Row.DataItem, "ID_NhomThaoTac") != null)
                {
                    GridView grdViewOrders = (GridView)sender;
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    TableCell cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "Ten_NhomThaoTac").ToString();
                    cell.ColumnSpan = 5;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
            }
        }

        protected void gridHuongDanThaoTac_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "ID_NhomThaoTac").ToString();
            }
        }

        protected void gridNangSuatToMay_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("huongdan"))
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                loadGridHuongDanThaoTac(id);
                divHDThaoTac.Style["display"] = "block";
                if (Session["dtNK"] == null)
                {
                    Load_ResetGV_NK();
                }
            }
        }
        //protected void gridNangSuatToMayTT_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName.Equals("huongdan"))
        //    {
        //        int id = Convert.ToInt32(e.CommandArgument.ToString());
        //        loadGridHuongDanThaoTac(id);
        //        divHDThaoTac.Style["display"] = "block";
        //    }
        //}

        protected void gridNangSuatNhayKhau_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("huongdanNhayKhau"))
            {
                if(e.CommandArgument != null && e.CommandArgument.ToString() != "")
                {
                    int id = Convert.ToInt32(e.CommandArgument.ToString());
                    loadGridHuongDanThaoTac(id);
                    divHDThaoTac.Style["display"] = "block";
                    if (Session["dtNK"] == null)
                    {
                        Load_ResetGV_NK();
                    }
                }                
            }
            else
            {
                DataTable dt = Session["dtNK"] as DataTable;
                if(dt == null)
                {
                    Load_ResetGV_NK();
                }
            }
        }

        protected void lbtnChiTiet_Click(object sender, EventArgs e)
        {
            LinkButton lnbtn = sender as LinkButton;            
            LinkButton lbtnAnChiTiet = ((LinkButton)this.gridNangSuatToMay.HeaderRow.FindControl("lbtnAnChiTiet"));
            lnbtn.Visible = false;
            lbtnAnChiTiet.Visible = true;
            this.gridNangSuatToMay.Columns[3].Visible = true;
            this.gridNangSuatToMay.Columns[5].Visible = true;
            this.gridNangSuatToMay.Columns[6].Visible = true;
            this.gridNangSuatToMay.Columns[7].ItemStyle.Width = 15;
            this.gridNangSuatToMay.Columns[8].ItemStyle.Width = 15;

            DataTable dt = Session["dtNK"] as DataTable;
            if (dt == null)
            {
                Load_ResetGV_NK();
            }
        }

        protected void lbtnAnChiTiet_Click(object sender, EventArgs e)
        {
            LinkButton lnbtn = sender as LinkButton;
            LinkButton lbtnChiTiet = ((LinkButton)this.gridNangSuatToMay.HeaderRow.FindControl("lbtnChiTiet"));
            lnbtn.Visible = false;
            lbtnChiTiet.Visible = true;
            this.gridNangSuatToMay.Columns[3].Visible = false;
            this.gridNangSuatToMay.Columns[5].Visible = false;
            this.gridNangSuatToMay.Columns[6].Visible = false;
            this.gridNangSuatToMay.Columns[7].ItemStyle.Width = 60;
            this.gridNangSuatToMay.Columns[8].ItemStyle.Width = 60;

            DataTable dt = Session["dtNK"] as DataTable;
            if (dt == null)
            {
                Load_ResetGV_NK();
            }
        }

        protected void lbtnNK_CT_Click(object sender, EventArgs e)
        {
            LinkButton lnbtn = sender as LinkButton;
            LinkButton lbtnAnChiTiet_NK = ((LinkButton)this.gridNangSuatNhayKhau.HeaderRow.FindControl("LinkNK_AnCT"));
            lnbtn.Visible = false;
            lbtnAnChiTiet_NK.Visible = true;
            this.gridNangSuatNhayKhau.Columns[2].Visible = true;
            this.gridNangSuatNhayKhau.Columns[3].Visible = true;
            this.gridNangSuatNhayKhau.Columns[4].ItemStyle.Width = 15;
        }

        protected void LinkNK_AnCT_Click(object sender, EventArgs e)
        {
            LinkButton lnbtn = sender as LinkButton;
            LinkButton lbtnChiTiet_NK = ((LinkButton)this.gridNangSuatNhayKhau.HeaderRow.FindControl("lbtnNK_CT"));
            lnbtn.Visible = false;
            lbtnChiTiet_NK.Visible = true;
            this.gridNangSuatNhayKhau.Columns[2].Visible = false;
            this.gridNangSuatNhayKhau.Columns[3].Visible = false;
            this.gridNangSuatNhayKhau.Columns[4].ItemStyle.Width = 40;
        }

        protected void ddlLanDanhGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLanDanhGia.SelectedValue != null && ddlLanDanhGia.SelectedValue.ToString() != "" && ddlLanDanhGia.SelectedValue.ToString() != "0")
            {

                try
                {
                    loadDataGridToMay();
                    Load_ResetGV_NK();
                    lanDanhGia = Convert.ToInt32(ddlLanDanhGia.SelectedValue.ToString());
                    phongbanid = Int32.Parse(Session["PhongBanID"].ToString());
                    LoadData_XepHang(phongbanid, lanDanhGia);

                }
                catch { }

            }
        }

        protected void gridKyNangCongNhan_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected bool KiemTraNgayDiLam(DateTime daNgay)
        {
            try
            {
                int iMaNS_ID = Convert.ToInt32(Session["userid"]);
                int iPhongBanID = Convert.ToInt32(Session["PhongBanID"]);
                // Settings.  
                object[] sqlPr =
                {
                    new SqlParameter("@iPhongBanID",iPhongBanID),
                    new SqlParameter("@daNgay", daNgay),
                    new SqlParameter("@sMaNS_ID", iMaNS_ID)
                };
                // Processing.  
                string sqlQuery = "[dbo].[pr_tblCTL_DangKyLamThem_KiemTraNgayLeChuNhat_wNgay_MaNS_ID] @iPhongBanID,@daNgay,@sMaNS_ID";

                return db.Database.SqlQuery<bool>(sqlQuery, sqlPr).SingleOrDefault();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}