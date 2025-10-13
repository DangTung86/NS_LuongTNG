using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Models;

namespace TNGLuong
{
    public partial class Admin1 : System.Web.UI.MasterPage
    {
        TNG_CTLDbContact db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new TNG_CTLDbContact();
            if (Session["username"] != null)
            {
                liPhanQuyenUser.Visible = false;
                liInsertMaHang.Visible = false;
                liToTruong.Visible = false;
                liKhoaBLg.Visible = false;
                liOrther.Visible = false;
                liResetPass.Visible = false;
                liKhoaChucNang.Visible = false;
                if (Session["username"].ToString().Equals("admin"))
                {
                    liPhanQuyenUser.Visible = true;
                    liInsertMaHang.Visible = true;
                    liToTruong.Visible = true;
                    liKhoaBLg.Visible = true;
                    liOrther.Visible = true;
                    liKhoaChucNang.Visible = true;
                    liResetPass.Visible = true;
                }
                else if (Session["username"].ToString().Equals("hethong"))
                {
                    liPhanQuyenUser.Visible = false;
                    liInsertMaHang.Visible = false;
                    liToTruong.Visible = false;
                    liKhoaBLg.Visible = false;
                    liOrther.Visible = true;
                    liKhoaChucNang.Visible = true;
                    liResetPass.Visible = false;
                }
                else
                {
                    LCB_WEB_Admin cls = new LCB_WEB_Admin();
                    string m_sMaNS = Session["username"].ToString();
                    cls = db.LCB_WEB_Admin.Where(x => x.MaNS == m_sMaNS).SingleOrDefault();
                    if (cls != null && !string.IsNullOrEmpty(cls.VaiTro) && cls.VaiTro != "0")
                    {
                        string[] role = cls.VaiTro.Split('|');
                        if (role.Length > 0)
                        {
                            for (int i = 0; i < role.Length; i++)
                            {
                                if (role[i].Equals("1"))
                                    liPhanQuyenUser.Visible = true;
                                if (role[i].Equals("2"))
                                    liResetPass.Visible = true;
                                if (role[i].Equals("3"))
                                    liToTruong.Visible = true;
                                if (role[i].Equals("4"))
                                    liInsertMaHang.Visible = true;
                                if (role[i].Equals("5"))
                                    liKhoaBLg.Visible = true;
                                if (role[i].Equals("6"))
                                    liOrther.Visible = true;
                                if (role[i].Equals("7"))
                                    liKhoaChucNang.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
                if (Session["Avatar"] != null)
                {
                    imgAvatar.Src = "http://appmobile.tng.vn:8082/office_files/" + Session["Avatar"].ToString();
                    imgAvatar_min.Src = "http://appmobile.tng.vn:8082/office_files/" + Session["Avatar"].ToString();
                }
                if(Session["fullname"] != null)
                {
                    lblFullName.Text = Session["fullname"].ToString();
                    lblFullName_min.Text = Session["fullname"].ToString();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}