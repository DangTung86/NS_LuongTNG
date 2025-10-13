using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Models;

namespace TNGLuong
{
    public partial class KhaiBaoSucKhoe : System.Web.UI.Page
    {
        KhaiBaoYTeDbContact db = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            db = new KhaiBaoYTeDbContact();
            txtTuNgay.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtDenNgay.Text = DateTime.Now.ToString("yyyy-MM-dd");
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            if (ddlNhomBenh.Text == "")
            {
                loadDataNhomBenh();
            }
            if (ddlDiaDiem.Text == "")
            {
                loadDataDiaDiem();
            }
            
            
        }
        protected void loadDataNhomBenh()
        {
            try
            {

                List<SK_NhomBenh> lst = new List<SK_NhomBenh>();
                lst = db.SK_NhomBenh.Where(m => m.TrangThai == true).ToList();
                if (lst != null && lst.Count > 0)
                {
                    ddlNhomBenh.DataSource = lst;
                    ddlNhomBenh.DataBind();
                    if (lst != null && lst.Count > 0)
                    {
                        ddlNhomBenh.DataSource = lst;
                        ddlNhomBenh.DataTextField = "TenNhomBenh";
                        ddlNhomBenh.DataValueField = "Id";
                        ddlNhomBenh.DataBind();
                    }
                }
            }
            catch (Exception ex) { }
        }
        protected void loadDataDiaDiem()
        {
            try
            {

                List<SK_DiaDiemDieuTri> lst = new List<SK_DiaDiemDieuTri>();
                lst = db.SK_DiaDiemDieuTri.Where(m => m.TrangThai == true).ToList();
                if (lst != null && lst.Count > 0)
                {
                    ddlDiaDiem.DataSource = lst;
                    ddlDiaDiem.DataBind();
                    if (lst != null && lst.Count > 0)
                    {
                        ddlDiaDiem.DataSource = lst;
                        ddlDiaDiem.DataTextField = "TenDiaDiem";
                        ddlDiaDiem.DataValueField = "Id";
                        ddlDiaDiem.DataBind();
                    }
                }
            }
            catch (Exception ex) { }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int idNhomBenh = 0;
            int idDiaDiem = 0;
            int mansid = 0;
            string mans = "";
            string hoTen = "";
            int phongbanid = 0;
            int donviid = 0;
            bool ketQua = false;

            if(chkDaKhoi.Checked == true)
            {
                ketQua = true;
            } 
            if (ddlNhomBenh.SelectedValue != null && ddlNhomBenh.SelectedValue.ToString() != "")
                idNhomBenh = Convert.ToInt32(ddlNhomBenh.SelectedValue.ToString());

            if (ddlDiaDiem.SelectedValue != null && ddlDiaDiem.SelectedValue.ToString() != "")
                idDiaDiem = Convert.ToInt32(ddlDiaDiem.SelectedValue.ToString());

            if (Session["userid"] != null)
                mansid = Convert.ToInt32(Session["userid"].ToString());

            if (Session["username"] != null)
                mans = Session["username"].ToString();

            if (Session["fullname"] != null)
                hoTen = Session["fullname"].ToString();

            if (Session["PhongBanID"] != null)
                phongbanid = Convert.ToInt32(Session["PhongBanID"].ToString());
            if (Session["DonViID"] != null)
                donviid = Convert.ToInt32(Session["DonViID"].ToString());
            if (txtTuNgay.Text == null || txtDenNgay.Text == null || txtTenBenh == null || txtPhuongPhapDT == null)
            {
                lblMessenger.Text = "Vui lòng nhập đầy đủ thông tin!";
                addthismodalContact.Style["display"] = "block";
                divThongBao.Style["display"] = "block";
            }
            else
            {
                try
                {
                    SK_KhaiBaoDinhKy sk = new SK_KhaiBaoDinhKy();
                    sk.IdNhomBenh = idNhomBenh;
                    sk.IdNoiDieuTri = idDiaDiem;
                    sk.PhongBanID = phongbanid;
                    sk.DonViID = donviid;
                    sk.MaNS_ID = mansid;
                    sk.MaNS = mans;
                    sk.Nam = DateTime.Now.Year;
                    sk.NgayKhaiBao = DateTime.Now;
                    sk.NgayBatDau = DateTime.Parse(txtTuNgay.Text);
                    sk.NgayKetThuc = DateTime.Parse(txtDenNgay.Text);
                    sk.TenBenh = txtTenBenh.Text.ToString();
                    sk.PhuongPhapDieuTri = txtPhuongPhapDT.Text.ToString();
                    sk.KetQuaDieuTri = ketQua;
                    sk.HoTen = hoTen;

                    db.SK_KhaiBaoDinhKy.Add(sk);
                    db.SaveChanges();

                    lblMessenger.Text = "Cập nhật thành công !";
                    addthismodalContact.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                }
                catch (Exception ex)
                {
                    lblMessenger.Text = "Cập nhật không thành công!";
                    addthismodalContact.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                }
               
            }
           


        }
        protected void chkDangDieuTri_CheckedChanged(object sender, EventArgs e)
        {
            chkDaKhoi.Checked = false;
            
        }
        protected void chkDaKhoi_CheckedChanged(object sender, EventArgs e)
        {
            chkDangDieuTri.Checked = false; 
        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            
            addthismodalContact.Style["display"] = "none";
            divThongBao.Style["display"] = "none";
            Response.Redirect("KhaiBaoSucKhoe");
        }
    }
}