using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Models;
using TNGLuong.ModelsView;

namespace TNGLuong
{
    public partial class DoiMatKhau : System.Web.UI.Page
    {
        TNGLuongDbContact db = null;
        Info ifo = null;
        private MemoryCache cache = MemoryCache.Default;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new TNGLuongDbContact();
            ifo = new Info();
            if (Session["username"] != null)
            {
                lblFullName.Text = Session["fullname"].ToString();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            DM_TaiKhoan us = new DM_TaiKhoan();
            string mans = Session["username"].ToString();
            us = db.DM_TaiKhoan.Where(x => x.MaNS.ToUpper() == mans.ToUpper()).FirstOrDefault();
            if(us != null)
            {
                if(!us.PassWord.Equals(ifo.encryptString(txtMatKhauCu.Value.ToString())))
                {
                    lblErr.Text = "Bạn nhập sai mật khẩu cũ.";
                    return;
                }
                else if(txtMatKhauCu.Value.ToString().Equals(txtPassMoi.Value.ToString()))
                {
                    lblErr.Text = "Mật khẩu mới giống mật khẩu cũ.";
                    return;
                }
                else
                {
                    us.PassWord = ifo.encryptString(txtPassMoi.Value.ToString());
                    us.UpdatePass = DateTime.Now;
                    int id = db.SaveChanges();
                    if(id != 0)
                    {
                        cache.Remove("Users");
                        List<View_Web_ThongTinNS> lst = new List<View_Web_ThongTinNS>();
                        lst = db.View_Web_ThongTinNS.ToList();
                        if (lst != null && lst.Count > 0)
                            cache.Set("Users", lst, DateTimeOffset.UtcNow.AddHours(10));
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        lblErr.Text = "Lỗi đổi mật khẩu vui lồng kiểm tra lại.";
                        return;
                    }
                }
            }
        }
    }
}