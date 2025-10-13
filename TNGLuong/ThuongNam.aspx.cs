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
    public partial class ThuongNam : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new TNG_CTLDbContact();
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    if (Session["fullname"] != null)
                        lblHoTen.Text = Session["fullname"].ToString().Split('-')[0];
                    lblMaNS.Text = Session["username"].ToString();
                    load_ddlNam();
                    lblDonVi_ToMay.Text = Session["TenDonVi"].ToString() +" - "+ Session["TenPhongban"].ToString();
                    lblTieuDe.Text = "PHIẾU THANH TOÁN THƯỞNG NĂM  "+ Convert.ToInt32(ddlNam.SelectedValue.ToString());
                    loadThongTinPage();
                }
            }
            else
                Response.Redirect("Login.aspx");
        }

        protected void load_ddlNam()
        {
            DateTime dte = DateTime.Now;
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SoNam", typeof(int)));
            for(int i= dte.Year - 5;i<= dte.Year+1;i++)
            {
                DataRow dr = dt.NewRow();
                dr["SoNam"] = i;
                dt.Rows.Add(dr);
            }
            ddlNam.DataSource = dt;
            ddlNam.DataBind();
            ddlNam.SelectedValue = (dte.Year-1).ToString();
        }

        protected void loadThongTinPage()
        {            
            int m_iname = 0;
            int m_iMaNS = 0;
            if (ddlNam.SelectedValue != null && ddlNam.SelectedValue.ToString() != "")
                m_iname = Convert.ToInt32(ddlNam.SelectedValue.ToString());
            if (Session["userid"] != null)
                m_iMaNS = Convert.ToInt32(Session["userid"].ToString());
            object[] sqlPr =
            {
                new SqlParameter("@iNam", m_iname),
                new SqlParameter("@iMaNS_ID", m_iMaNS),
                new SqlParameter("@iErrorCode", 1)
            };
            string sqlQuery = "[dbo].[pr_ThuongNam_ChiTiet_Select_XemIn_PhieuThuong_Office] @iNam,@iMaNS_ID,@iErrorCode";
            List<clsThuongNam> lst = new List<clsThuongNam>();
            lst = db.Database.SqlQuery<clsThuongNam>(sqlQuery, sqlPr).ToList();
            if (lst != null && lst.Count > 0)
            {
                clsThuongNam cls = lst[0];
                lblTong_LuongSP.Text = string.Format("{0:#,0.##}",cls.Tong_LuongSP);
                lblLuong_Thang13.Text = string.Format("{0:#,0.##}", cls.Luong_Thang13); ;
                lblTong_ThuongABC.Text = string.Format("{0:#,0.##}", cls.Tong_ThuongABC); ;
                lblThuong_ABC.Text = string.Format("{0:#,0.##}", cls.Thuong_ABC); ;
                lblSoThang_LamViec.Text = string.Format("{0:#,0.##}", cls.SoThang_LamViec); ;
                lblThuong_TienTien.Text = string.Format("{0:#,0.##}", cls.Thuong_TienTien); ;
                lblSoThang_XetThuong.Text = string.Format("{0:#,0.##}", cls.SoThang_XetThuong); ;
                lblSoThang_LoaiA.Text = string.Format("{0:#,0.##}", cls.SoThang_LoaiA); ;
                lblThuong_ToiThieu.Text = string.Format("{0:#,0.##}", cls.Thuong_ToiThieu); ;
                lblSoThang_BuLuong.Text = string.Format("{0:#,0.##}", cls.SoThang_BuLuong); ;
                lblThuong_Khac.Text = string.Format("{0:#,0.##}", cls.Thuong_Khac); ;
                lblSoThang_KyLuat.Text = string.Format("{0:#,0.##}", cls.SoThang_KyLuat); ;
                lblTong_ThuongNam.Text = string.Format("{0:#,0.##}", cls.Tong_ThuongNam); ;
                lblTong_ThuNhap.Text = string.Format("{0:#,0.##}", cls.Tong_ThuNhap); ;
                lblThueTNCN.Text = string.Format("{0:#,0.##}", cls.ThueTNCN); ;
                lblThuNhapBQ.Text = string.Format("{0:#,0.##}", cls.ThuNhapBQ); ;
                lblThucNhan.Text = string.Format("{0:#,0.##}", cls.ThucNhan); ;
                lblTyLe_Thuong_TTN.Text = string.Format("{0:#,0.##}%", (cls.TyLe_Thuong_TTN*100)); ;
                lblTyLe_Thuong_TNBQ.Text = string.Format("{0:#,0.##}%", (cls.TyLe_Thuong_TNBQ * 100)); ;
                lblHeSoK1.Text = string.Format("{0:#,0.00}", cls.HeSoK1);
                lblHeSoK2.Text = string.Format("{0:#,0.00}", cls.HeSoK2);
                lblHeSoK3.Text = string.Format("{0:#,0.00}", cls.HeSoK3);
                lblHeSoK4.Text = string.Format("{0:#,0.00}", cls.HeSoK4);
            }
            else
            {
                lblTong_LuongSP.Text = "";
                lblLuong_Thang13.Text = "";
                lblTong_ThuongABC.Text = "";
                lblThuong_ABC.Text = "";
                lblSoThang_LamViec.Text = "";
                lblThuong_TienTien.Text = "";
                lblSoThang_XetThuong.Text = "";
                lblSoThang_LoaiA.Text = "";
                lblThuong_ToiThieu.Text = "";
                lblSoThang_BuLuong.Text = "";
                lblThuong_Khac.Text = "";
                lblSoThang_KyLuat.Text = "";
                lblTong_ThuongNam.Text = "";
                lblTong_ThuNhap.Text = "";
                lblThueTNCN.Text = "";
                lblThuNhapBQ.Text = "";
                lblThucNhan.Text = "";
                lblTyLe_Thuong_TTN.Text = "";
                lblTyLe_Thuong_TNBQ.Text = "";
            }
        }

        protected void ddlNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNam.SelectedValue == null || ddlNam.SelectedValue.ToString() == "") return;
            lblTieuDe.Text = "PHIẾU THANH TOÁN THƯỞNG NĂM  " + Convert.ToInt32(ddlNam.SelectedValue.ToString());
            loadThongTinPage();
        }
    }
}