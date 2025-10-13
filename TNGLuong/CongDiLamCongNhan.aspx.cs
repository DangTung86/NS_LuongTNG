using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.ModelsView;
using TNGLuong.Models;

namespace TNGLuong
{
    
    public partial class CongDiLamCongNhan : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    //Session["PD_PhongBanID"] = Session["ChucVu"].ToString();
                    if (!string.IsNullOrEmpty(txtDate.Text))
                    {
                        var dateTime = DateTime.Now;
                        txtDate.Text = dateTime.ToString("yyyy-MM-dd");
                        int thang = dateTime.Month;
                        int nam = dateTime.Year;
                        loadDataGridCongDiLamCongNhan(thang,nam);
                    }
                }
            }
            else
                Response.Redirect("Login.aspx");
        }
        protected void loadDataGridCongDiLamCongNhan(int thang, int nam)
        {
            try
            {
                db = new TNG_CTLDbContact();
                int idmans = 0;
                
                int tong = 0;
                if (Session["userid"] != null)
                    idmans = Convert.ToInt32(Session["userid"].ToString());
                object[] sqlPr =
                {
                    new SqlParameter("@MaNS_Id", idmans),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam)
                };
                string sqlQuery = "[dbo].[pr_WEB_LCB_Select_CongDiLamCongNhan] @MaNS_Id,@Thang,@Nam";
                List<ListCongDiLam> lst = new List<ListCongDiLam>();
                lst = db.Database.SqlQuery<ListCongDiLam>(sqlQuery, sqlPr).ToList();
                DataTable dtb = ultils.CreateDataTableStr<ListCongDiLam>(lst);
                tong = lst.Count();
                foreach (var item in lst)
                {
                    if (item.CS_GioRa == null && item.CS_GioVao == null)
                    {
                        tong = tong - 1;
                    }
                }
                if (dtb != null && dtb.Rows.Count > 0)
                {
                    lblTongSoCong.Text = tong.ToString();
                    gridCongDiLamCongNhan.DataSource = dtb;
                    gridCongDiLamCongNhan.DataBind();
                    
                }
                else
                {
                    
                    dtb.Rows.Add(dtb.NewRow());
                    gridCongDiLamCongNhan.DataSource = dtb;
                    gridCongDiLamCongNhan.DataBind();
                    gridCongDiLamCongNhan.Rows[0].Cells.Clear();
                    gridCongDiLamCongNhan.Rows[0].Cells.Add(new TableCell());
                    gridCongDiLamCongNhan.Rows[0].Cells[0].ColumnSpan = 14;
                    gridCongDiLamCongNhan.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridCongDiLamCongNhan.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex) {  }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            var date =Convert.ToDateTime(txtDate.Text);
            int thang = date.Month;
            int nam = date.Year;
            loadDataGridCongDiLamCongNhan(thang,nam);
        }
    }
}