using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TNGLuong.Models;

namespace TNGLuong
{
    public partial class ThoiGianCho : System.Web.UI.Page
    {
        TNG_QLSXDbContact db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new TNG_QLSXDbContact();
            
            HtmlGenericControl linhapns = (HtmlGenericControl)this.Master.FindControl("nhapNS");
            HtmlGenericControl lithoigiancho = (HtmlGenericControl)this.Master.FindControl("thoigiancho");
            HtmlGenericControl liduyetNS = (HtmlGenericControl)this.Master.FindControl("duyetNS");
            HtmlGenericControl liluongns = (HtmlGenericControl)this.Master.FindControl("luongns");
            HtmlGenericControl libluong = (HtmlGenericControl)this.Master.FindControl("bluong");
            linhapns.Attributes.Add("class", "");
            lithoigiancho.Attributes.Add("class", "active");
            liduyetNS.Attributes.Add("class", "");
            liluongns.Attributes.Add("class", "");
            libluong.Attributes.Add("class", "");
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    Session["namebtn"] = null;
                    timer1.Enabled = false;
                    tdlbl.Visible = false;
                    tdbtn.Visible = false;
                }
                Control header = this.Master.FindControl("siteheader");
                if (btn1.Visible == false)
                {
                    header.Visible = false;
                }
                else
                {
                    header.Visible = true;
                }
            }
            else
                Response.Redirect("Login.aspx");
        }

        protected void btnBTP_Click(object sender, EventArgs e)
        {
            Session["namebtn"] = btnBTP.Text;
            lblTieuDe.Text = btnBTP.Text.ToUpper();
            checkControl();
        }

        protected void checkControl()
        {
            Session["startdate"] = DateTime.Now.ToString();
            Session["tick"] = 0;
            timer1.Enabled = true;
            btn1.Visible = false;
            btn2.Visible = false;
            tdlbl.Visible = true;
            tdbtn.Visible = true;            
        }

        protected void timer1_Tick(object sender, EventArgs e)
        {
            string hours = "00", minutes = "00", secondss = "00";
            int tick = Convert.ToInt32(Session["tick"].ToString()) + timer1.Interval;
            Session["tick"] = tick;
            decimal seconds = tick / 1000;
            int hour = Convert.ToInt32(Math.Floor(seconds / 3600));
            int minute = Convert.ToInt32(Math.Floor((seconds / 60) % 60));
            int second = Convert.ToInt32(seconds % 60);
            if (hour < 10)
                hours = "0" + hour.ToString();
            else
                hours = hour.ToString();
            if (minute < 10)
                minutes = "0" + minute;
            else
                minutes = minute.ToString();
            if (second < 10)
                secondss = "0" + second;
            else
                secondss = second.ToString();
            lblDongHo.Text = minutes + ":" + secondss;
        }

        protected void btnCoDien_Click(object sender, EventArgs e)
        {
            Session["namebtn"] = "Chờ " + btnCoDien.Text.ToLower();
            lblTieuDe.Text = "CHỜ " + btnCoDien.Text.ToUpper();
            checkControl();
        }

        protected void btnPhuLieu_Click(object sender, EventArgs e)
        {
            Session["namebtn"] = "Chờ " + btnPhuLieu.Text.ToLower();
            lblTieuDe.Text = "CHỜ " + btnPhuLieu.Text.ToUpper();
            checkControl();
        }

        //protected void btnKhauTrc_Click(object sender, EventArgs e)
        //{
        //    Session["namebtn"] = "Chờ " + btnKhauTrc.Text.ToLower();
        //    lblTieuDe.Text = "CHỜ " + btnKhauTrc.Text.ToUpper();
        //    checkControl();
        //}

        protected void btnHuongDan_Click(object sender, EventArgs e)
        {
            Session["namebtn"] = "Chờ " + btnHuongDan.Text.ToLower();
            lblTieuDe.Text = "CHỜ " + btnHuongDan.Text.ToUpper();
            checkControl();
        }

        protected void btnChatLuong_Click(object sender, EventArgs e)
        {
            Session["namebtn"] = "Chờ " + btnChatLuong.Text.ToLower();
            lblTieuDe.Text = "CHỜ " + btnChatLuong.Text.ToUpper();
            checkControl();
        }

        protected void btnKhac_Click(object sender, EventArgs e)
        {
            Session["namebtn"] = btnKhac.Text;
            lblTieuDe.Text = btnKhac.Text.ToUpper();
            checkControl();
        }

        protected void btnKetThuc_Click(object sender, EventArgs e)
        {
            Control header = this.Master.FindControl("siteheader");
            if (header != null)
            {
                header.Visible = true;
                timer1.Enabled = false;
                btn1.Visible = true;
                btn2.Visible = true;
                tdlbl.Visible = false;
                tdbtn.Visible = false;
                lblDongHo.Text = "00:00";
                if (Session["username"] != null && Session["namebtn"] != null && Session["startdate"] != null)
                {
                    CBC_BaoNgungViec bnv = new CBC_BaoNgungViec();
                    bnv.LyDoNgungViec = Session["namebtn"].ToString();
                    bnv.MaNS = Session["username"].ToString();
                    bnv.ThoiGian_BatDau = DateTime.Parse(Session["startdate"].ToString());
                    bnv.ThoiGian_KetThuc = DateTime.Now;
                    db.CBC_BaoNgungViec.Add(bnv);
                    db.SaveChanges();
                }
            }
        }
    }
}