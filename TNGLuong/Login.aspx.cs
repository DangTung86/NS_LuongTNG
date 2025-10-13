using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Models;
using TNGLuong.ModelsView;

namespace TNGLuong
{
    public partial class Login : System.Web.UI.Page
    {
        TNGLuongDbContact db = null;
        TNG_CTLDbContact dbCtl = null;
        Info ifo = null;
        private MemoryCache cache = MemoryCache.Default;

        protected void Page_Load(object sender, EventArgs e)
        {
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            btncloseMain.ServerClick += new EventHandler(btncloseMain_Click);
            db = new TNGLuongDbContact();
            dbCtl = new TNG_CTLDbContact();
            ifo = new Info();
            if (!IsPostBack)
            {   
                if(!ultils.isMobileBrowser())
                {
                    Session["CanhBaoLD"] = "CanhBao";
                }
                if (Session["CanhBaoLD"] != null && Session["CanhBaoLD"].ToString() != "")
                {
                    List<clsPhatThanh> lstPT = Get_LinkPhatThanh();
                    if (lstPT != null && lstPT.Count > 0)
                    {
                        pTieuDePT.InnerText = "Bản tin phát thanh TNG.";
                        string sLinkPhatThanh = "http://14.170.154.13:8086/" + lstPT[0].ID_PhatThanh.ToString() + "/" + lstPT[0].TenFile;
                        audio_PT.Src = sLinkPhatThanh;
                    }
                    Session["fullname"] = null;
                    Session["username"] = null;
                    Session["userid"] = null;
                    Session["PhongBanID"] = null;
                    Session["ToMay"] = null;
                    Session["ChucVu"] = null;
                    Session["TNGF"] = null;
                    Session["DonViID"] = null;
                    Session["DonViID_Cha"] = null;
                    Session["TenDonVi"] = null;
                    Session["TenPhongban"] = null;
                    Session["DoiTuongID"] = null;
                    Session["IsResetPass"] = null;
                    Session["NhomCat_HoanThien"] = null;
                    Session["Admin"] = null;
                    Session["time1"] = null;
                    Session["time2"] = null;
                    Session["time3"] = null;
                    Session["time4"] = null;
                    Session["Avatar"] = null;
                    Session["KhoaBL"] = false;
                    Session["NhomCongViec_ID"] = null;
                    Session["HetHopDong"] = null;
                    Session["CanhBaoLD"] = null;

                    if (cache.Get("Users") == null)
                    {
                        List<View_Web_ThongTinNS> lst = new List<View_Web_ThongTinNS>();
                        lst = db.View_Web_ThongTinNS.ToList();
                        if (lst != null && lst.Count > 0)
                            cache.Set("Users", lst, DateTimeOffset.UtcNow.AddHours(10));
                    }
                    if (Request.Cookies["userid"] != null)
                        txtmans.Value = Request.Cookies["userid"].Value;
                    if (Request.Cookies["pwd"] != null)
                        txtpass.Attributes.Add("value", Request.Cookies["pwd"].Value);
                    if (Request.Cookies["userid"] != null && Request.Cookies["pwd"] != null)
                        cbRemenber.Checked = true;
                }
                else
                    Response.Redirect("CanhBao.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            List<LCB_WEB_TimeOpen> lstTO = new List<LCB_WEB_TimeOpen>();
            lstTO = dbCtl.LCB_WEB_TimeOpen.Where(x => x.IsActive == true).OrderBy(x => x.STT).ToList();
            if (lstTO != null && lstTO.Count > 0)
            {
                foreach (LCB_WEB_TimeOpen o in lstTO)
                {
                    Session["time1"] = TimeSpan.Parse(o.TimeStart.ToString("HH:mm"));
                    Session["time2"] = TimeSpan.Parse(o.TimeEnd.ToString("HH:mm"));
                }
            }

            if (!string.IsNullOrEmpty(txtmans.Value) && txtmans.Value.ToLower().Equals("admin"))
            {
                if (!string.IsNullOrEmpty(txtpass.Text) && txtpass.Text.ToLower().Equals("admin!@#"))
                {
                    Session["username"] = "admin";
                    Session["fullname"] = "Admin";
                    Response.Redirect("Admin");
                }
            }
            else
            {
                #region "Tạm dóng"
                //TimeSpan time1 = new TimeSpan();
                //TimeSpan time2 = new TimeSpan();
                //TimeSpan time3 = new TimeSpan();
                //TimeSpan time4 = new TimeSpan();
                //TimeSpan time = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
                //if (Session["time1"] != null)
                //    time1 = TimeSpan.Parse(Session["time1"].ToString());
                //if (Session["time2"] != null)
                //    time2 = TimeSpan.Parse(Session["time2"].ToString());
                //if (Session["time3"] != null)
                //    time3 = TimeSpan.Parse(Session["time3"].ToString());
                //if (Session["time4"] != null)
                //    time4 = TimeSpan.Parse(Session["time4"].ToString());

                //if (TimeSpan.Compare(time, time1) >= 0 && TimeSpan.Compare(time, time2) < 0)
                //{
                //    MemoryCaheLogin();
                //}
                //else if (TimeSpan.Compare(time, time3) >= 0)
                //{
                //    MemoryCaheLogin();
                //}
                //else if (TimeSpan.Compare(time, time4) < 0)
                //{
                //    MemoryCaheLogin();
                //}
                //else
                //{
                //    MemoryCaheLogin();
                //    //NotMemoryCaheLogin();
                //}
                #endregion
                MemoryCaheLogin();
            }
        }

        protected void MemoryCaheLogin()
        {
            if (!string.IsNullOrEmpty(txtmans.Value) && !string.IsNullOrEmpty(txtpass.Text))
            {
                View_Web_ThongTinNS infous = new View_Web_ThongTinNS();
                try
                {
                    List<View_Web_ThongTinNS> lst = new List<View_Web_ThongTinNS>();
                    if (cache.Get("Users") != null)
                    {
                        lst = (List<View_Web_ThongTinNS>)cache.Get("Users");
                        if (lst != null && lst.Count > 0)
                        {
                            string m_sMaNS = txtmans.Value.ToLower();
                            View_Web_ThongTinNS us = new View_Web_ThongTinNS();
                            us = lst.Where(x => x.MaNS.ToLower().Equals(m_sMaNS)).SingleOrDefault();
                            if (us != null)
                                infous = us;
                            else
                            {
                                us = getThongTinNS(m_sMaNS.ToUpper());
                                if (us != null)
                                    infous = us;
                            }
                        }
                    }
                }
                catch { infous = null; }
                if (infous != null && infous.MaNS_ID != 0)
                {
                    if (infous.PassWord.ToLower().Equals(ifo.encryptString(txtpass.Text.ToString()).ToLower()))
                    {
                        Session["fullname"] = infous.HoTen;
                        Session["username"] = infous.MaNS;
                        Session["userid"] = infous.MaNS_ID;
                        Session["PhongBanID"] = infous.PhongBanID;
                        if (infous.ToMay != null && infous.ToMay > 0)
                            Session["ToMay"] = infous.ToMay;
                        else if (infous.ToMay == null && infous.DoiTuongID == 1)
                            Session["ToMay"] = infous.PhongBanID;
                        Session["ChucVu"] = infous.ToTruong;
                        //Session["TNGF"] = TNGFTrue(infous.MaNS);
                        Session["DonViID"] = infous.DonViID;
                        Session["DonViID_Cha"] = infous.DonViIDCha;
                        bool bKBL = IsKhoaBangLuong(infous.DonViIDCha.Value);
                        Session["KhoaBL"] = IsKhoaBangLuong(infous.DonViIDCha.Value).ToString().ToUpper();
                        Session["TenDonVi"] = infous.TenDonVi;
                        Session["TenPhongban"] = infous.TenPhongban;
                        Session["DoiTuongID"] = infous.DoiTuongID;
                        if (infous.ID_NhomCongViec != null)
                        {
                            Session["NhomCongViec_ID"] = infous.ID_NhomCongViec;
                        }
                        else
                        {
                            Session["NhomCongViec_ID"] = 0;
                        }
                        Session["HetHopDong"] = infous.IsHetHanHD;

                        Session["Avatar"] = infous.AvatarUrl;
                        Session["IsResetPass"] = CheckAccResetPass(infous.MaNS);
                        if (infous.ID_NhomCongViec == 1 || infous.ID_NhomCongViec == 3 || infous.TenPhongban.ToLower().Contains("kiểm gấp"))
                        {
                            if (infous.ID_NhomCongViec == null)
                                Session["NhomCat_HoanThien"] = infous.TenPhongban;
                            else
                                Session["NhomCat_HoanThien"] = infous.ID_NhomCongViec;
                        }
                        if (infous.ID_NhomCongViec == 5)
                            Session["NhomPhuTro"] = 5;

                        if (cbRemenber.Checked == true)
                        {
                            Response.Cookies["userid"].Value = txtmans.Value.Trim();
                            Response.Cookies["pwd"].Value = txtpass.Text.ToString();
                            Response.Cookies["userid"].Expires = DateTime.Now.AddDays(15);
                            Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(15);
                        }
                        else
                        {
                            Response.Cookies["userid"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);
                        }

                        if (Session["ChucVu"] != null)
                        {
                            lbtnNSNgay.Visible = true;
                            //lbtnDKComCa.Visible = true;
                            lbtnChoViec.Visible = false;
                            lbtnDSCN.Visible = true;

                            lbtnTongHop.Visible = false;
                            lbtnPhanToNK.Visible = true;
                            lbtnPhanCumTrg.Visible = true;
                            lblPhanCongDoan.Visible = true;

                            lbtnblNgay.Visible = false;
                            if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Equals(("true").ToUpper()))
                            {
                                lbltonghopluong.Visible = false;
                                lblCongDiLam.Visible = false;
                            }
                            else
                            {
                                lbltonghopluong.Visible = true;
                                lblCongDiLam.Visible = true;
                            }
                            //end
                        }
                        else /*if (Session["ToMay"] != null)*/
                        {
                            lbtnNSNgay.Visible = true;
                            //lbtnDKComCa.Visible = true;
                            lbtnChoViec.Visible = true;
                            lbtnDSCN.Visible = false;

                            lbtnTongHop.Visible = true;
                            lbtnPhanToNK.Visible = false;
                            lbltonghopluong.Visible = false;
                            lbtnPhanCumTrg.Visible = false;
                            lblCongDiLam.Visible = false;
                            lblPhanCongDoan.Visible = false;

                            lbtnblNgay.Visible = true;
                            lbtnblThang.Visible = true;
                        }

                        //Khảo sát 28/06/2021
                        if (Session["DonViID"] != null && (Session["DonViID"].ToString() == "73" || Session["DonViID"].ToString() == "74"))
                            a_KhaoSat.Visible = true;
                        else
                            a_KhaoSat.Visible = false;

                        if (Session["IsResetPass"] != null)
                        {
                            if (Session["IsResetPass"].ToString() == "1")
                                lblQuanTriWeb.Visible = true;
                            else if (Session["IsResetPass"].ToString() == "0")
                                lblQuanTriWeb.Visible = false;
                            else
                                lblQuanTriWeb.Visible = false;
                        }
                        else
                            lblQuanTriWeb.Visible = false;
                        //if (infous.ID_NhomCongViec != 1 && infous.ID_NhomCongViec != 3 && !infous.TenPhongban.ToLower().Contains("kiểm gấp") && infous.ID_NhomCongViec != 5)
                        //{
                        //    Response.Redirect("/KyLucCongNhan");
                        //}
                        //else
                            addthismodalContact.Style["display"] = "block"; 

                        TimeSpan time1 = new TimeSpan();
                        TimeSpan time2 = new TimeSpan();
                        TimeSpan time = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
                        if (Session["time1"] != null)
                            time1 = TimeSpan.Parse(Session["time1"].ToString());
                        if (Session["time2"] != null)
                            time2 = TimeSpan.Parse(Session["time2"].ToString());
                        TimeSpan timeHT = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
                        if (TimeSpan.Compare(timeHT, time1) >= 0 && TimeSpan.Compare(timeHT, time2) <= 0 && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
                        {
                            lbtnNSNgay.Visible = false;
                            //lbtnDKComCa.Visible = false;
                        }
                        else
                        {
                            lbtnNSNgay.Visible = true;
                            //lbtnDKComCa.Visible = true;
                        }

                    }
                    else
                    {
                        lblErr.Text = "Nhập sai mật khẩu, vui lòng kiểm tra lại.";
                    }
                }
                else
                {
                    //lblErr.Text = "Mã nhân sự không tồn tại, vui lòng kiểm tra lại.";
                    InfoUserName nsClass = new InfoUserName();
                    SqlParameter pr = new SqlParameter("@MaNS", txtmans.Value.ToString());
                    nsClass = db.Database.SqlQuery<InfoUserName>("ThongTinNS_Goc_GetInfoByMaNS @MaNS", pr).SingleOrDefault();
                    if (nsClass != null)
                    {
                        string smmt = nsClass.PassWord.Replace(" ", String.Empty);
                        if (smmt.Equals(txtpass.Text.ToString()))
                        {
                            DM_TaiKhoan us = new DM_TaiKhoan();
                            us.MaNS_ID = Convert.ToInt32(nsClass.MaNS_ID.ToString());
                            us.MaNS = nsClass.MaNS;
                            if (nsClass.HoTen.Split(' ').Length == 3)
                            {
                                us.HoDem = nsClass.HoTen.Split(' ')[0].ToString() + " " + nsClass.HoTen.Split(' ')[1].ToString();
                                us.Ten = nsClass.HoTen.Split(' ')[2].ToString();
                            }
                            else if (nsClass.HoTen.Split(' ').Length == 4)
                            {
                                us.HoDem = nsClass.HoTen.Split(' ')[0].ToString() + " " + nsClass.HoTen.Split(' ')[1].ToString() + " " + nsClass.HoTen.Split(' ')[2].ToString();
                                us.Ten = nsClass.HoTen.Split(' ')[3].ToString();
                            }
                            else
                            {
                                us.HoDem = nsClass.HoTen.Split(' ')[0].ToString();
                                us.Ten = nsClass.HoTen.Split(' ')[1].ToString();
                            }
                            us.NgaySinh = DateTime.Now;
                            us.SoCMT = smmt;
                            us.GioiTinh = true;
                            us.PassWord = ifo.encryptString(txtpass.Text.ToString());
                            us.Avatar = null;
                            us.IsActive = true;
                            db.DM_TaiKhoan.Add(us);
                            int id = db.SaveChanges();
                            if (id > 0)
                            {
                                Session["fullname"] = nsClass.HoTen;
                                Session["username"] = nsClass.MaNS;
                                Session["userid"] = nsClass.MaNS_ID;
                                Session["PhongBanID"] = nsClass.PhongbanID;

                                if (nsClass.ToMay != null && nsClass.ToMay > 0)
                                    Session["ToMay"] = nsClass.ToMay;
                                else if (nsClass.ToMay == null && nsClass.DoiTuongID == 1)
                                    Session["ToMay"] = nsClass.PhongbanID;
                                Session["ChucVu"] = nsClass.ToTruong;

                                Session["DonViID"] = nsClass.DonViID;
                                Session["KhoaBL"] = IsKhoaBangLuong(nsClass.DonViID.Value);
                                Session["TenDonVi"] = nsClass.TenDonVi;
                                Session["TenPhongban"] = nsClass.TenPhongban;
                                Session["DoiTuongID"] = nsClass.DoiTuongID;
                                Session["IsResetPass"] = CheckAccResetPass(nsClass.MaNS);
                                //if (nsClass.ID_NhomCongViec == 1 || nsClass.ID_NhomCongViec == 3)
                                //    Session["NhomCat_HoanThien"] = nsClass.ID_NhomCongViec;
                                if (nsClass.ID_NhomCongViec != null)
                                {
                                    Session["NhomCongViec_ID"] = nsClass.ID_NhomCongViec;
                                }
                                else
                                {
                                    Session["NhomCongViec_ID"] = 0;
                                }
                                Session["HetHopDong"] = nsClass.IsHetHanHD;

                                if (nsClass.ID_NhomCongViec == 1 || nsClass.ID_NhomCongViec == 3 || nsClass.TenPhongban.ToLower().Contains("kiểm gấp"))
                                {
                                    if (nsClass.ID_NhomCongViec == null)
                                        Session["NhomCat_HoanThien"] = nsClass.TenPhongban;
                                    else
                                        Session["NhomCat_HoanThien"] = nsClass.ID_NhomCongViec;
                                }
                                if (infous.ID_NhomCongViec == 5)
                                    Session["NhomPhuTro"] = 5;
                                if (cbRemenber.Checked == true)
                                {
                                    Response.Cookies["userid"].Value = txtmans.Value.Trim();
                                    Response.Cookies["pwd"].Value = txtpass.Text.ToString();
                                    Response.Cookies["userid"].Expires = DateTime.Now.AddDays(15);
                                    Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(15);
                                }
                                else
                                {
                                    Response.Cookies["userid"].Expires = DateTime.Now.AddDays(-1);
                                    Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);
                                }

                                if (Session["ChucVu"] != null)
                                {
                                    lbtnNSNgay.Visible = true;
                                    //lbtnDKComCa.Visible = true;
                                    lbtnChoViec.Visible = false;
                                    lbtnDSCN.Visible = true;
                                    lbtnTongHop.Visible = false;
                                    lbtnPhanToNK.Visible = true;
                                    lbtnPhanCumTrg.Visible = true;
                                    lblPhanCongDoan.Visible = true;

                                    lbtnblNgay.Visible = false;
                                    if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Equals(("true").ToUpper()))
                                    {
                                        lbltonghopluong.Visible = false;
                                        lblCongDiLam.Visible = false;
                                    }
                                    else
                                    {
                                        lbltonghopluong.Visible = true;
                                        lblCongDiLam.Visible = true;
                                    }
                                }
                                else /*if (Session["ToMay"] != null)*/
                                {
                                    lbtnNSNgay.Visible = true;
                                    //lbtnDKComCa.Visible = true;
                                    lbtnChoViec.Visible = true;
                                    lbtnDSCN.Visible = false;
                                    lbtnTongHop.Visible = true;
                                    lbtnPhanToNK.Visible = false;
                                    lbtnPhanCumTrg.Visible = false;
                                    lbltonghopluong.Visible = false;
                                    lblCongDiLam.Visible = false;
                                    lblPhanCongDoan.Visible = false;

                                    lbtnblNgay.Visible = true;
                                    lbtnblThang.Visible = true;
                                }
                                if (Session["IsResetPass"] != null)
                                {
                                    if (Session["IsResetPass"].ToString() == "1")
                                        lblQuanTriWeb.Visible = true;
                                    else if (Session["IsResetPass"].ToString() == "0")
                                        lblQuanTriWeb.Visible = false;
                                    else
                                        lblQuanTriWeb.Visible = false;
                                }
                                else
                                    lblQuanTriWeb.Visible = false;
                                //if (nsClass.ID_NhomCongViec != 1 && nsClass.ID_NhomCongViec != 3 && !nsClass.TenPhongban.ToLower().Contains("kiểm gấp") && nsClass.ID_NhomCongViec != 5)
                                //{
                                    addthismodalContact.Style["display"] = "block";
                                //}
                                //else
                                //    Response.Redirect("/KyLucCongNhan");

                                TimeSpan time1 = new TimeSpan();
                                TimeSpan time2 = new TimeSpan();
                                TimeSpan time = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
                                if (Session["time1"] != null)
                                    time1 = TimeSpan.Parse(Session["time1"].ToString());
                                if (Session["time2"] != null)
                                    time2 = TimeSpan.Parse(Session["time2"].ToString());
                                TimeSpan timeHT = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
                                if (TimeSpan.Compare(timeHT, time1) >= 0 && TimeSpan.Compare(timeHT, time2) <= 0 && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
                                {
                                    lbtnNSNgay.Visible = false;
                                    //lbtnDKComCa.Visible = false;
                                }
                                else
                                {
                                    lbtnNSNgay.Visible = true;
                                    //lbtnDKComCa.Visible = true;
                                }
                            }
                            else
                            {
                                lblErr.Text = "Nhập sai mật khẩu, vui lòng kiểm tra lại.";
                            }
                        }
                        else
                        {
                            lblErr.Text = "Nhập sai số CMND, vui lòng kiểm tra lại.";
                        }
                    }
                    else
                    {
                        lblErr.Text = "Mã nhân sự không tồn tại hoặc đã bị khóa, vui lòng kiểm tra lại.";
                    }
                }
            }
            if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Equals(("true").ToUpper()))
            {
                lbtnNSNgay.Visible = true;
                //lbtnDKComCa.Visible = true;
                lbtnChoViec.Visible = false;
                lbtnDSCN.Visible = false;
                lbtnTongHop.Visible = false;
                lbtnPhanToNK.Visible = false;
                lbtnPhanCumTrg.Visible = false;
                lbltonghopluong.Visible = false;
                lblCongDiLam.Visible = false;
                lblPhanCongDoan.Visible = false;

                lbtnblNgay.Visible = false;
                lbtnblThang.Visible = true;
                lbtnTongHop.Visible = false;
                lbtnDKComCa.Visible = false;
                lblKhaiBaoYTe.Visible = false;
            }

            int iDonViID = 0;
            if (Session["DonViID"] != null)
                iDonViID = int.Parse(Session["DonViID"].ToString());
            TNG_CTLDbContact dbCTL = new TNG_CTLDbContact();
            List<LCB_WEB_KhoaChucNang> lstChucNang = new List<LCB_WEB_KhoaChucNang>();
            lstChucNang = dbCTL.LCB_WEB_KhoaChucNang.Where(x => x.DonViID == iDonViID).OrderBy(x => x.ID_ChucNang).ToList();
            if (lstChucNang != null && lstChucNang.Count > 0)
            {
                foreach (LCB_WEB_KhoaChucNang item in lstChucNang)
                {
                    LCB_WEB_ChucNang cls = new LCB_WEB_ChucNang();
                    cls = dbCTL.LCB_WEB_ChucNang.Where(x => x.ID_ChucNang == item.ID_ChucNang).SingleOrDefault();
                    if (cls == null) continue;
                    object obContrrol = string.IsNullOrEmpty(cls.TenControl) ? null : (this.FindControl(cls.TenControl) as HyperLink);
                    object obTenControl_Login = string.IsNullOrEmpty(cls.TenControl_Login) ? null : (this.FindControl(cls.TenControl_Login) as HyperLink);
                    object obTenControl_Mobile = string.IsNullOrEmpty(cls.TenControl_Mobile) ? null : (this.FindControl(cls.TenControl_Mobile) as HyperLink);
                    if(obContrrol != null)
                        (this.FindControl(cls.TenControl) as HyperLink).Visible = false;
                    if (obTenControl_Login != null)
                        (this.FindControl(cls.TenControl_Login) as HyperLink).Visible = false;
                    if (obTenControl_Mobile != null)
                        (this.FindControl(cls.TenControl_Mobile) as HyperLink).Visible = false;
                    //if (cls.TenChucNang == "Nhập năng suất")
                    //    lbtnNSNgay.Visible = false;
                    //if (cls.TenChucNang == "Đăng ký cơm ca")
                    //    lbtnDKComCa.Visible = false;
                    //if (cls.TenChucNang == "Tổng hợp CĐ")
                    //    lbtnTongHop.Visible = false;
                    //if (cls.TenChucNang == "Nhập thời gian chờ")
                    //    lbtnChoViec.Visible = false;
                    //if (cls.TenChucNang == "Phân cụm trưởng")
                    //    lbtnPhanCumTrg.Visible = false;
                    //if (cls.TenChucNang == "Phân tổ nhảy khâu")
                    //    lbtnPhanToNK.Visible = false;
                    //if (cls.TenChucNang == "Duyệt năng suất")
                    //    lbtnDSCN.Visible = false;
                    //if (cls.TenChucNang == "Bảng lương ngày")
                    //    lbtnblNgay.Visible = false;
                    //if (cls.TenChucNang == "Tổng hợp lương")
                    //    lbltonghopluong.Visible = false;
                    //if (cls.TenChucNang == "Bảng lương tháng")
                    //    lbtnblThang.Visible = false;
                    //if (cls.TenChucNang == "Kiểm tra công đi làm")
                    //    lblCongDiLam.Visible = false;
                    //if (cls.TenChucNang == "Phân công công đoạn")
                    //    lblPhanCongDoan.Visible = false;
                    //if (cls.TenChucNang == "Đánh giá kỹ năng")
                    //    lblKhaiBaoYTe.Visible = false;
                    //if (cls.TenChucNang == "Quản trị web")
                    //    lblQuanTriWeb.Visible = false;
                }
            }
        }

        protected void NotMemoryCaheLogin()
        {
            if (!string.IsNullOrEmpty(txtmans.Value) && !string.IsNullOrEmpty(txtpass.Text))
            {
                InfoUserName nsClass = new InfoUserName();
                SqlParameter pr = new SqlParameter("@MaNS", txtmans.Value.ToString());
                nsClass = db.Database.SqlQuery<InfoUserName>("ThongTinNS_GetInfoByMaNS @MaNS", pr).SingleOrDefault();
                if (nsClass != null)
                {
                    if (nsClass.PassWord.ToLower().Equals(ifo.encryptString(txtpass.Text.ToString()).ToLower()))
                    {
                        Session["fullname"] = nsClass.HoTen + "-" + nsClass.MaNS;
                        Session["username"] = nsClass.MaNS;
                        Session["userid"] = nsClass.MaNS_ID;
                        Session["PhongBanID"] = nsClass.PhongbanID;
                        if (nsClass.ToMay != null && nsClass.ToMay > 0)
                            Session["ToMay"] = nsClass.ToMay;
                        else if (nsClass.ToMay == null && nsClass.DoiTuongID == 1)
                            Session["ToMay"] = nsClass.PhongbanID;
                        Session["ChucVu"] = nsClass.ToTruong;
                        Session["DonViID"] = nsClass.DonViID;
                        Session["KhoaBL"] = IsKhoaBangLuong(nsClass.DonViID.Value);
                        Session["TenDonVi"] = nsClass.TenDonVi;
                        Session["TenPhongban"] = nsClass.TenPhongban;
                        Session["DoiTuongID"] = nsClass.DoiTuongID;
                        Session["Avatar"] = nsClass.AvatarUrl;
                        Session["IsResetPass"] = CheckAccResetPass(nsClass.MaNS);
                        if (nsClass.ID_NhomCongViec == 1 || nsClass.ID_NhomCongViec == 3)
                            Session["NhomCat_HoanThien"] = nsClass.ID_NhomCongViec;
                        if (cbRemenber.Checked == true)
                        {
                            Response.Cookies["userid"].Value = txtmans.Value.Trim();
                            Response.Cookies["pwd"].Value = txtpass.Text.ToString();
                            Response.Cookies["userid"].Expires = DateTime.Now.AddDays(15);
                            Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(15);
                        }
                        else
                        {
                            Response.Cookies["userid"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);
                        }

                        if (Session["ChucVu"] != null)
                        {
                            lbtnNSNgay.Visible = true;
                            //lbtnDKComCa.Visible = true;
                            lbtnChoViec.Visible = false;
                            lbtnDSCN.Visible = true;
                            lbtnTongHop.Visible = false;
                            lbtnPhanToNK.Visible = true;
                            lbtnPhanCumTrg.Visible = true;
                            lblCongDiLam.Visible = true;
                            lblPhanCongDoan.Visible = true;
                            lbtnblNgay.Visible = false;

                            if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Contains("true"))
                            {
                                lbltonghopluong.Visible = false;
                                lbtnblThang.Visible = false;
                            }
                            else
                            {
                                lbltonghopluong.Visible = true;
                                lbtnblThang.Visible = true;
                            }
                        }
                        else /*if (Session["ToMay"] != null)*/
                        {
                            lbtnNSNgay.Visible = true;
                            //lbtnDKComCa.Visible = true;
                            lbtnChoViec.Visible = true;
                            lbtnDSCN.Visible = false;
                            lbtnTongHop.Visible = true;
                            lbtnPhanToNK.Visible = false;
                            lbltonghopluong.Visible = false;
                            lbtnPhanCumTrg.Visible = false;
                            lblCongDiLam.Visible = false;
                            lblPhanCongDoan.Visible = false;

                            if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Contains("true"))
                            {
                                lbtnblNgay.Visible = false;
                                lbtnblThang.Visible = false;
                            }
                            else
                            {
                                lbtnblNgay.Visible = true;
                                lbtnblThang.Visible = true;
                            }
                        }
                        //else
                        //{
                        //    lbtnNSNgay.Visible = false;
                        //    lbtnDKComCa.Visible = false;
                        //    lbtnChoViec.Visible = false;
                        //    lbtnDSCN.Visible = false;
                        //    lbtnblNgay.Visible = false;
                        //    lbtnTongHop.Visible = false;
                        //    lbtnPhanToNK.Visible = false;
                        //}
                        addthismodalContact.Style["display"] = "block";
                    }
                    else
                    {
                        lblErr.Text = "Nhập sai mật khẩu, vui lòng kiểm tra lại.";
                    }
                }
                else
                {
                    lblErr.Text = "Mã nhân sự không tồn tại hoặc đã bị khóa, vui lòng kiểm tra lại.";
                }
            }
        }

        protected View_Web_ThongTinNS getThongTinNS(string manhansu)
        {
            try
            {
                // Settings.  
                object[] sqlPr =
                {
                    new SqlParameter("@MaNS", manhansu)
                };
                // Processing.  
                string sqlQuery = "[dbo].[pr_Web_View_Web_ThongTinNS_wMaNS] @MaNS";

                View_Web_ThongTinNS cls = db.Database.SqlQuery<View_Web_ThongTinNS>(sqlQuery, sqlPr).SingleOrDefault();
                return cls;
            }
            catch (Exception ex)
            {
                View_Web_ThongTinNS cls = new View_Web_ThongTinNS();
                return cls;
            }
        }

        protected string TNGFTrue(string manhansu)
        {
            try
            {
                // Settings.  
                object[] sqlPr =
                {
                    new SqlParameter("@MaNS", manhansu)
                };
                // Processing.  
                string sqlQuery = "[dbo].[ThongTinNhanSu_ByMaNS] @MaNS";

                string checktngf = db.Database.SqlQuery<string>(sqlQuery, sqlPr).SingleOrDefault();
                return checktngf;
            }
            catch (Exception ex)
            {
                string checktngf = null;
                return checktngf;
            }
        }

        protected string CheckAccResetPass(string manhansu)
        {
            try
            {
                // Settings.  
                object[] sqlPr =
                {
                    new SqlParameter("@MaNS", manhansu)
                };
                // Processing.  
                string sqlQuery = "[dbo].[pr_Web_CheckAccount_IsResetPass] @MaNS";

                string checktngf = db.Database.SqlQuery<string>(sqlQuery, sqlPr).SingleOrDefault();
                return checktngf;
            }
            catch (Exception ex)
            {
                string checktngf = null;
                return checktngf;
            }
        }

        protected bool IsKhoaBangLuong(int m_iDonViID)
        {
            try
            {
                // Settings.  
                object[] sqlPr =
                {
                    new SqlParameter("@iDonViID", m_iDonViID)
                };
                // Processing.  
                string sqlQuery = "[dbo].[pr_LCB_WEB_KhoaBangLuong_Select_KhoaBlg_wDonViID] @iDonViID";

                bool checktngf = dbCtl.Database.SqlQuery<bool>(sqlQuery, sqlPr).SingleOrDefault();
                return checktngf;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void lbtnNSNgay_Click(object sender, EventArgs e)
        {
            //TimeSpan time1 = new TimeSpan();
            //TimeSpan time2 = new TimeSpan();
            //TimeSpan time3 = new TimeSpan();
            //TimeSpan time4 = new TimeSpan();
            //TimeSpan time = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
            //if(Session["time1"] !=null)
            //    time1 = TimeSpan.Parse(Session["time1"].ToString());
            //if (Session["time2"] != null)
            //    time2 = TimeSpan.Parse(Session["time2"].ToString());
            //if (Session["time3"] != null)
            //    time3 = TimeSpan.Parse(Session["time3"].ToString());
            //if (Session["time4"] != null)
            //    time4 = TimeSpan.Parse(Session["time4"].ToString());
            //if (TimeSpan.Compare(time, time1) >=0 && TimeSpan.Compare(time, time2) < 0)
            //{
            Response.Redirect("NangSuatCongNhan");
            addthismodalContact.Style["display"] = "none";
            //}
            //else if (TimeSpan.Compare(time, time3) >= 0)
            //{
            //    Response.Redirect("NangSuatCongNhan");
            //    addthismodalContact.Style["display"] = "none";
            //}
            //else if (TimeSpan.Compare(time, time4) < 0)
            //{
            //    Response.Redirect("NangSuatCongNhan");
            //    addthismodalContact.Style["display"] = "none";
            //}
            //else
            //{
            //    popupcontact.Style["display"] = "block";
            //    addthismodalContact.Style["display"] = "none";
            //}
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            popupcontact.Style["display"] = "none";
        }

        protected void btncloseMain_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
        }

        protected void txtAdmin_TextChanged(object sender, EventArgs e)
        {
            if (txtAdmin.Text.Equals("cntt@123345"))
            {
                Session["Admin"] = "cntt@123345";
                Response.Redirect("NangSuatCongNhan");
                popupcontact.Style["display"] = "none";
            }
        }

        protected void lbltonghopluong_Click(object sender, EventArgs e)
        {
            Response.Redirect("TongHopLuong");
        }

        protected List<clsPhatThanh> Get_LinkPhatThanh()
        {
            try
            {
                List<clsPhatThanh> lst = new List<clsPhatThanh>();
                string sqlQuery = "[dbo].[pr_Web_NSTL_PR_PhatThanh_GetDuLieuPhatThanh_GanNhat]";
                lst = db.Database.SqlQuery<clsPhatThanh>(sqlQuery).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public class clsPhatThanh
    {
        public int ID_PhatThanh { get; set; }
        public string TenFile { get; set; }
        public DateTime Ngay_Upload { get; set; }
    }
}