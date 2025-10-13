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

namespace TNGLuong
{
    public partial class _Default : Page
    {
        TNGLuongDbContact db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new TNGLuongDbContact();
            if (Session["username"] != null)
            {
                Response.Redirect("/NangSuatCongNhan");
                //if (!IsPostBack)
                //{
                //    txtStart.Text = DateTime.Now.AddMonths(-1).Month + "-"+ DateTime.Now.AddMonths(-1).Year;
                //    txtStart_TextChanged(sender, e);
                //}
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void txtStart_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtStart.Text) && Session["userid"] != null && Session["userid"].ToString() != "")
                {
                    string[] lst = txtStart.Text.Split('-');
                    int thang = int.Parse(lst[0].ToString());
                    int nam = int.Parse(lst[1].ToString());
                    //View_BangLuongTheoThang bltheothang = new View_BangLuongTheoThang();
                    //bltheothang = db.View_BangLuongTheoThang.SingleOrDefault(x => x.MaNS_ID == int.Parse(Session["userid"].ToString()) && x.Thang == thang && x.Nam == nam);
                    DanhSachLuongNhanVien blu = new DanhSachLuongNhanVien();                    
                    string ssss = Session["userid"].ToString();
                    var UID = int.Parse(Session["userid"].ToString());
                    if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Equals(("true").ToUpper()))
                    {
                        View_BangLuongTheoThang_DANHGIA bl = new View_BangLuongTheoThang_DANHGIA();
                        bl = db.View_BangLuongTheoThang_DANHGIA.OrderByDescending(x => x.MaBangLuong_ID).FirstOrDefault(x => x.MaNS_ID == UID && x.Thang == thang && x.Nam == nam);
                        if (bl != null)
                            blu = DG_GetListBangLuongUser(bl.MaBangLuong_ID, int.Parse(Session["userid"].ToString()), thang, nam);
                        else
                            blu = null;
                    }
                    else
                    {
                        View_BangLuongTheoThang bl = new View_BangLuongTheoThang();
                        bl = db.View_BangLuongTheoThang.OrderByDescending(x => x.MaBangLuong_ID).FirstOrDefault(x => x.MaNS_ID == UID && x.Thang == thang && x.Nam == nam);
                        if (bl != null)
                            blu = GetListBangLuongUser(bl.MaBangLuong_ID, int.Parse(Session["userid"].ToString()), thang, nam);
                        else
                            blu = null;
                    }
                    if (blu != null)
                    {
                        lblSoCong.Text = string.Format("{0:#,0.#}", blu.SoCong);
                        lblSoTG.Text = string.Format("{0:#,0}", blu.SoCong_ThoiGian);
                        lblLuongCB.Text = string.Format("{0:#,0}", blu.TL_CapBac);
                        lblXepLoai.Text = blu.XepLoai == null ? "" : blu.XepLoai.ToString();
                        decimal luongbaoh = blu.MucLuongCB + blu.PC_Luong + blu.PC_PCCC + blu.PC_ATVSV;
                        lblLuongBH.Text = string.Format("{0:#,0}", blu.Luong_DongBaoHiem);
                        lblLuongCoBan.Text = string.Format("{0:#,0}", blu.MucLuongCB);
                        lblLuongPhuCap.Text = string.Format("{0:#,0}", blu.PC_ChucVu);
                        lblMucPCCC.Text = string.Format("{0:#,0}", blu.PC_PCCC);
                        lblMucATVSV.Text = string.Format("{0:#,0}", blu.PC_ATVSV);
                        decimal lgthuctra = blu.TL_LuongSP_8h + blu.TL_ThoiGian + blu.TL_Luong_ThemGio + blu.PC_Luong + blu.PC_PhuNu + blu.TL_VNS_HQCV + blu.TL_KiemNhiem + blu.Thuong_XepHang;
                        lblLuongThucTra.Text = string.Format("{0:#,0}", blu.TongThuNhap);
                        lblLuong8h.Text = string.Format("{0:#,0}", blu.TL_LuongSP_8h);
                        lblLuongThoiGian.Text = string.Format("{0:#,0}", blu.TL_ThoiGian);
                        lblLuongThemGio.Text = string.Format("{0:#,0}", blu.TL_Luong_ThemGio);
                        lblPCLuong.Text = string.Format("{0:#,0}", blu.PC_Luong);
                        lblPhuCapNu.Text = string.Format("{0:#,0}", blu.PC_PhuNu);
                        lblTLVNS.Text = string.Format("{0:#,0}", blu.TL_VNS_HQCV);
                        lblTLKiemNhiem.Text = string.Format("{0:#,0}", blu.TL_KiemNhiem);
                        lblTTXephang.Text = string.Format("{0:#,0}", blu.Thuong_XepHang);
                        decimal plcty = blu.PC_ConNho + blu.PC_Khac;
                        lblPhucLoiCty.Text = string.Format("{0:#,0}", plcty);
                        lblConNho.Text = string.Format("{0:#,0}", blu.PC_ConNho);
                        lblKhac.Text = string.Format("{0:#,0}", blu.PC_Khac);
                        decimal phainop = blu.KT_BaoHiem + blu.KT_ThueTNCN + blu.KT_DangPhi + blu.KT_CongDoan + blu.KT_DoanPhi + blu.KT_Khac;
                        lblTongPhaiNop.Text = string.Format("{0:#,0}", phainop);
                        lblBHXH.Text = string.Format("{0:#,0}", blu.KT_BaoHiem);
                        lblThueTNCaNhan.Text = string.Format("{0:#,0}", blu.KT_ThueTNCN);
                        lblDangPhi.Text = string.Format("{0:#,0}", blu.KT_DangPhi);
                        lblCongDoanPhi.Text = string.Format("{0:#,0}", blu.KT_CongDoan);
                        lblDoanPhi.Text = string.Format("{0:#,0}", blu.KT_DoanPhi);
                        lblPhaiNopKhac.Text = string.Format("{0:#,0}", blu.KT_Khac);
                        lblSoTienDuocNhan.Text = string.Format("{0:#,0}", blu.SoTien_ConNhan);
                        decimal thuongnam = blu.TN_ABC + blu.TN_LDTT + blu.TN_CSTD + blu.TN_LuongThang13;
                        lblThuongNam.Text = string.Format("{0:#,0}", thuongnam);
                        lblThuongXH.Text = string.Format("{0:#,0}", blu.TN_ABC);
                        lblThuongLDTienTien.Text = string.Format("{0:#,0}", blu.TN_LDTT);
                        lblThuongChiSiThiDua.Text = string.Format("{0:#,0}", blu.TN_CSTD);
                        lblThuongThang13.Text = string.Format("{0:#,0}", blu.TN_LuongThang13);
                        if (blu.TenDonVi.Contains("Chi nhánh Thời Trang TNG") || blu.isTNGF==true)
                        {
                            decimal khac = blu.PC_Khac - blu.PC_ATVSV - blu.PC_PCCC;
                            if (khac > 0)
                                lblKhac.Text = string.Format("{0:#,0}", khac);
                            else
                                lblKhac.Text = "0";
                            lblTTKhac.Text = string.Format("{0:#,0}", blu.T_Khac);
                            if (blu.TenBoPhan.Equals("Tổ là hơi"))
                            {
                                lblConNho.Text = string.Format("{0:#,0}", blu.PC_PhuNu);
                            }
                            else if (blu.TenBoPhan.Contains("Tổ may"))
                            {
                                lblConNho.Text = string.Format("{0:#,0}", blu.PC_PhuNu);
                            }
                            else if (blu.TenBoPhan.Equals("Nhóm Cửa hàng"))
                            {
                                lblLuong8h.Text = string.Format("{0:#,0}", blu.TL_CapBac);
                                lblLuongCB.Text = string.Format("{0:#,0}", blu.TL_LuongSP_8h); 
                            }
                            else if (blu.TenBoPhan.Equals("Phòng Kỹ thuật Công nghệ") || blu.TenBoPhan.Equals("Phòng Cơ điện")
                                  || blu.TenBoPhan.Equals("P. Chất lượng") || blu.TenBoPhan.Equals("Quản lý xưởng")
                                  || blu.TenBoPhan.Equals("Quản lý tổ"))
                            {
                                lblLuong8h.Text = string.Format("{0:#,0}", blu.TL_CapBac);
                                lblLuongCB.Text = string.Format("{0:#,0}", blu.TL_LuongSP_8h);
                            }
                        }
                    }
                    else
                    {
                        lblSoCong.Text = "";
                        lblSoTG.Text = "";
                        lblLuongCB.Text = "";
                        lblXepLoai.Text = "";
                        lblLuongBH.Text = "";
                        lblLuongCoBan.Text = "";
                        lblLuongPhuCap.Text = "";
                        lblMucPCCC.Text = "";
                        lblMucATVSV.Text = "";
                        lblLuongThucTra.Text = "";
                        lblLuong8h.Text = "";
                        lblLuongThoiGian.Text = "";
                        lblLuongThemGio.Text = "";
                        lblPCLuong.Text = "";
                        lblPhuCapNu.Text = "";
                        lblTLVNS.Text = "";
                        lblTLKiemNhiem.Text = "";
                        lblTTXephang.Text = "";
                        lblPhucLoiCty.Text = "";
                        lblConNho.Text = "";
                        lblKhac.Text = "";                        
                        lblTongPhaiNop.Text = "";
                        lblBHXH.Text = "";
                        lblThueTNCaNhan.Text = "";
                        lblDangPhi.Text = "";
                        lblCongDoanPhi.Text = "";
                        lblDoanPhi.Text = "";
                        lblPhaiNopKhac.Text = "";
                        lblSoTienDuocNhan.Text = "";
                        lblThuongNam.Text = "";
                        lblThuongXH.Text = "";
                        lblThuongLDTienTien.Text = "";
                        lblThuongChiSiThiDua.Text = "";
                        lblThuongThang13.Text = "";
                    }
                }
            }
            catch(Exception ex) { }
        }
        
        protected DanhSachLuongNhanVien GetListBangLuongUser(int mabangluong,int mansid, int thang, int nam)
        {
            // Initialization.  
            DanhSachLuongNhanVien lst = new DanhSachLuongNhanVien();

            try
            {
                // Settings.  
                object[] sqlPr =
                {
                    new SqlParameter("@MaBangLuong_ID", mabangluong),
                    new SqlParameter("@MaNS_ID", mansid),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam)
                };
                // Processing.  
                string sqlQuery = "[dbo].[BangLuongGET_ChiTiet] @MaBangLuong_ID,@MaNS_ID,@Thang,@Nam";

                lst = db.Database.SqlQuery<DanhSachLuongNhanVien>(sqlQuery, sqlPr).FirstOrDefault();
            }
            catch(Exception ex)
            {
                lst = null;
            }

            // Info.  
            return lst;
        }

        protected DanhSachLuongNhanVien DG_GetListBangLuongUser(int mabangluong, int mansid, int thang, int nam)
        {
            // Initialization.  
            DanhSachLuongNhanVien lst = new DanhSachLuongNhanVien();

            try
            {
                // Settings.  
                object[] sqlPr =
                {
                    new SqlParameter("@MaBangLuong_ID", mabangluong),
                    new SqlParameter("@MaNS_ID", mansid),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam)
                };
                // Processing.  
                string sqlQuery = "[dbo].[prDG_BangLuongGET_ChiTiet] @MaBangLuong_ID,@MaNS_ID,@Thang,@Nam";

                lst = db.Database.SqlQuery<DanhSachLuongNhanVien>(sqlQuery, sqlPr).FirstOrDefault();
            }
            catch (Exception ex)
            {
                lst = null;
            }

            // Info.  
            return lst;
        }
    }
}