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
    public partial class BangLuongThang : System.Web.UI.Page
    {
        TNGLuongDbContact db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new TNGLuongDbContact();
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    txtStart.Text = DateTime.Now.AddMonths(-1).Month + "-" + DateTime.Now.AddMonths(-1).Year;
                    txtStart_TextChanged(sender, e);
                }
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

                    clsBangLuongThang blu = new clsBangLuongThang();
                    blu = GetBangLuongThang(int.Parse(Session["userid"].ToString()), thang, nam);
                    //if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Equals(("true").ToUpper()))
                    //{
                    //    return;
                    //}
                    //else
                    //{
                    //    //blu = GetBangLuongThang(int.Parse(Session["userid"].ToString()), thang, nam);
                    //}
                    if (blu != null)
                    {
                        //Tổng thu nhập: II + III
                        lblSoCong.Text = string.Format("{0:#,0.#}", blu.SoCong);
                        lblThuNhap.Text = string.Format("{0:#,0}", 0);
                        //I. Tiền lương căn cứ đóng Bảo hiểm: 1 + 2 + 3+ 4
                        lblLuongBH.Text = string.Format("{0:#,0}", blu.Luong_DongBaoHiem);
                        lblLuongCoBan.Text = string.Format("{0:#,0}", blu.MucLuongCB);
                        lblLuongPhuCap.Text = string.Format("{0:#,0}", blu.PC_ChucVu);
                        lblMucPCCC.Text = string.Format("{0:#,0}", blu.PC_PCCC);
                        lblMucATVSV.Text = string.Format("{0:#,0}", blu.PC_ATVSV);
                        //II. Tiền lương thực trả cho người lao động: 5 + 6 + 7 + 8 + 9 + 10 + 11 + 12 + 13
                        decimal dcTienluongThuc = blu.TL_LuongSP_8h + blu.TL_ThoiGian + blu.TL_Luong_ThemGio + blu.PC_Luong +
                                                  blu.PC_PhuNu + blu.TL_VNS_HQCV + blu.TL_KiemNhiem + blu.Thuong_XepHang;
                        lblLuongThucTra.Text = string.Format("{0:#,0}", blu.TongThuNhap);
                        //2.1 Tiền Lương
                        lblLuong8h.Text = string.Format("{0:#,0}", blu.TL_LuongSP_8h);
                        lblLuongThoiGian.Text = string.Format("{0:#,0}", blu.TL_ThoiGian);
                        lblLuongThemGio.Text = string.Format("{0:#,0}", blu.TL_Luong_ThemGio);
                        lblLgThemGioThuong.Text = string.Format("{0:#,0}", blu.TL_ThemGioNgayThuong);
                        lblLgThemGioNgayNghiLe.Text = string.Format("{0:#,0}", blu.TL_ThemGioNgayNghi);
                        //2.2 Phụ cấp lương
                        lblPCLuong.Text = string.Format("{0:#,0}", blu.PC_Luong);
                        lblPhuCapNu.Text = string.Format("{0:#,0}", blu.PC_PhuNu);
                        //2.3 Phụ cấp khác
                        lblTLVNS.Text = string.Format("{0:#,0}", blu.TL_VNS_HQCV);
                        lblTLKiemNhiem.Text = string.Format("{0:#,0}", blu.TL_KiemNhiem);
                        lblTTXepHang_Text.Text = blu.XepLoai;
                        lblTTXephang.Text = string.Format("{0:#,0}", blu.Thuong_XepHang);
                        lblTTBoXungThem.Text = string.Format("{0:#,0}", 0);
                        //III. Phúc lợi công ty: 14 + 15
                        decimal dcPhucLoiCty = blu.PC_ConNho + blu.PC_Khac;
                        lblPhucLoiCty.Text = string.Format("{0:#,0}", dcPhucLoiCty);
                        lblConNho.Text = string.Format("{0:#,0}", blu.PC_ConNho);
                        lblKhac.Text = string.Format("{0:#,0}", blu.PC_Khac);
                        lblKhac_AnCa.Text = string.Format("{0:#,0}", blu.PC_AnCa);
                        lblKhac_XangXe.Text = string.Format("{0:#,0}", blu.PC_XangXe);
                        //decimal dcHoTroYeu = blu.PC_Khac - (blu.PC_AnCa + blu.PC_XangXe);
                        lblKhac_HoTroYeu.Text = string.Format("{0:#,0}", blu.PC_HoTroTayNgheYeu);
                        lblTienChuyenCan.Text = string.Format("{0:#,0}", blu.PC_ChuyenCan);
                        //B. Tổng số tiền người lao động phải nộp: 16 + 17 + 18 + 19 + 20 + 21
                        decimal dcTienPhaiNop = blu.KT_BaoHiem + blu.KT_ThueTNCN + blu.KT_DangPhi + blu.KT_CongDoan + blu.KT_DoanPhi + blu.KT_Khac;
                        lblTongPhaiNop.Text = string.Format("{0:#,0}", dcTienPhaiNop);
                        lblBHXH.Text = string.Format("{0:#,0}", blu.KT_BaoHiem);
                        lblThueTNCaNhan.Text = string.Format("{0:#,0}", blu.KT_ThueTNCN);
                        lblDangPhi.Text = string.Format("{0:#,0}", blu.KT_DangPhi);
                        lblCongDoanPhi.Text = string.Format("{0:#,0}", blu.KT_CongDoan);
                        lblDoanPhi.Text = string.Format("{0:#,0}", blu.KT_DoanPhi);
                        lblPhaiNopKhac.Text = string.Format("{0:#,0}", blu.KT_Khac);
                        //C. Số tiền người lao động được nhận
                        lblSoTienDuocNhan.Text = string.Format("{0:#,0}", blu.SoTien_ConNhan);

                        //Tổng thu nhập: II + III
                        //decimal dcTongThuNhap = dcTienluongThuc + dcPhucLoiCty;
                        lblThuNhap.Text = string.Format("{0:#,0}", blu.TongThuNhap);
                        if (blu.TenDonVi.ToString().Contains("Chi nhánh Thời Trang TNG"))
                        {
                            decimal khac = blu.PC_Khac - blu.PC_ATVSV - blu.PC_PCCC;
                            if (khac > 0)
                                lblKhac.Text = string.Format("{0:#,0}", khac);
                            else
                                lblKhac.Text = "0";
                            //lblTTKhac.Text = string.Format("{0:#,0}", blu.T_Khac);
                            if (blu.TenBoPhan.Equals("Tổ là hơi"))
                            {
                                lblConNho.Text = string.Format("{0:#,0}", blu.PC_PhuNu);
                            }
                            else if (blu.TenBoPhan.ToString().Contains("Tổ may"))
                            {
                                lblConNho.Text = string.Format("{0:#,0}", blu.PC_PhuNu);
                            }
                            else if (blu.TenBoPhan.Equals("Nhóm Cửa hàng"))
                            {
                                lblLuong8h.Text = string.Format("{0:#,0}", blu.TL_CapBac);
                            }
                            else if (blu.TenBoPhan.Equals("Phòng Kỹ thuật Công nghệ") || blu.TenBoPhan.Equals("Phòng Cơ điện")
                                  || blu.TenBoPhan.Equals("P. Chất lượng") || blu.TenBoPhan.Equals("Quản lý xưởng")
                                  || blu.TenBoPhan.Equals("Quản lý tổ"))
                            {
                                lblLuong8h.Text = string.Format("{0:#,0}", blu.TL_CapBac);
                            }
                        }
                    }
                    else
                    {
                        lblSoCong.Text = "";
                        lblThuNhap.Text = "";
                        lblLgThemGioThuong.Text = "";
                        lblLgThemGioNgayNghiLe.Text = "";
                        lblTTBoXungThem.Text = "";
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
                        lblKhac_AnCa.Text = "";
                        lblKhac_XangXe.Text = "";
                        lblKhac_HoTroYeu.Text = "";
                        lblTongPhaiNop.Text = "";
                        lblBHXH.Text = "";
                        lblThueTNCaNhan.Text = "";
                        lblDangPhi.Text = "";
                        lblCongDoanPhi.Text = "";
                        lblDoanPhi.Text = "";
                        lblPhaiNopKhac.Text = "";
                        lblSoTienDuocNhan.Text = "";
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected clsBangLuongThang GetBangLuongThang(int mansid, int thang, int nam)
        {
            // Initialization.  
            clsBangLuongThang cls = new clsBangLuongThang();

            try
            {
                // Settings.  
                object[] sqlPr =
                {
                    new SqlParameter("@MaNS_ID", mansid),
                    new SqlParameter("@iThang", thang),
                    new SqlParameter("@Nam", nam)
                };
                // Processing.  
                string sqlQuery = "[TNG_ThongBao].[dbo].[pr_SelectBangLuong_ChiTiet_ThangNam] @MaNS_ID,@iThang,@Nam";

                cls = db.Database.SqlQuery<clsBangLuongThang>(sqlQuery, sqlPr).FirstOrDefault();
            }
            catch (Exception ex)
            {
                cls = null;
            }

            // Info.  
            return cls;
        }
    }
}