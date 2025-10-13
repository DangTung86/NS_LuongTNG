using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Models;

namespace TNGLuong.WebAdmin.production
{
    public partial class PhanQuyenUser : System.Web.UI.UserControl
    {
        TNG_CTLDbContact dbCTL = null;
        TNGLuongDbContact db = null;
        bool bSearch = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Phân quyền quản trị";
            db = new TNGLuongDbContact();
            dbCTL = new TNG_CTLDbContact();
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            txtSearch.ServerChange += new EventHandler(txtSearch_TextChanged);
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    Session["DataOld"] = null;
                    lblMessenger.Text = "";
                    Load_chkListChucNang();
                    LoadDataGrid();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            lblMessenger.Text = "";
            divMesssenger.Style["display"] = "none";
        }

        protected void Load_chkListChucNang()
        {
            List<LCB_WEB_ChucNang> lstChucNang= new List<LCB_WEB_ChucNang>();
            lstChucNang = dbCTL.LCB_WEB_ChucNang.Where(x=>x.ID_NhomChuNang == 1).OrderBy(x => x.ID_ChucNang).ToList();
            if (lstChucNang != null && lstChucNang.Count > 0)
            {
                chkList_ChucNang.DataSource = lstChucNang;
                chkList_ChucNang.DataTextField = "TenChucNang";
                chkList_ChucNang.DataValueField = "ID_ChucNang";
                chkList_ChucNang.DataBind();
            }
        }

        protected void LoadDataGrid()
        {
            try
            {
                List<LCB_WEB_Admin_Extension> lst = new List<LCB_WEB_Admin_Extension>();
                lst = dbCTL.Database.SqlQuery<LCB_WEB_Admin_Extension>("pr_LCB_WEB_Admin_SelectAll").ToList();
                if (lst != null && lst.Count > 0)
                {
                    DataTable dt = ultils.CreateDataTable<LCB_WEB_Admin_Extension>(lst);
                    Session["DataOld"] = dt;
                    gridPhanQuyen.DataSource = lst;
                    gridPhanQuyen.DataBind();
                }
                else
                {
                    Load_ResetGV();
                }
            }
            catch (Exception ex)
            {
                Load_ResetGV();
            }
        }

        protected void Load_ResetGV()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("MaNS", typeof(string)));
            dt.Columns.Add(new DataColumn("TenDonVi", typeof(string)));
            dt.Columns.Add(new DataColumn("HoTen", typeof(string)));
            dt.Columns.Add(new DataColumn("KichHoat", typeof(bool)));
            dt.Columns.Add(new DataColumn("TenChucDanh", typeof(string)));//
            dt.Columns.Add(new DataColumn("VaiTro", typeof(string)));//
            DataRow row = dt.NewRow();
            row["MaNS"] = "";
            row["TenDonVi"] = "";
            row["HoTen"] = "";
            row["KichHoat"] = true;
            row["TenChucDanh"] = "";
            row["VaiTro"] = "";
            dt.Rows.Add(row);
            gridPhanQuyen.DataSource = dt;
            gridPhanQuyen.DataBind();
            gridPhanQuyen.Rows[0].Cells.Clear();
            gridPhanQuyen.Rows[0].Cells.Add(new TableCell());
            gridPhanQuyen.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count + 2;
            gridPhanQuyen.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
            gridPhanQuyen.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }

        protected void gridPhanQuyen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkKichHoat = (CheckBox)e.Row.FindControl("chkKichHoat");

                if (DataBinder.Eval(e.Row.DataItem, "KichHoat") != null && DataBinder.Eval(e.Row.DataItem, "KichHoat").ToString() != "")
                {
                    if (chkKichHoat != null)
                        chkKichHoat.Checked = bool.Parse(DataBinder.Eval(e.Row.DataItem, "KichHoat").ToString());
                }
            }
        }

        protected void gridPhanQuyen_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                LCB_WEB_Admin usA = new LCB_WEB_Admin();
                string mans = gridPhanQuyen.DataKeys[e.RowIndex].Value.ToString();
                usA = dbCTL.LCB_WEB_Admin.Where(x => x.MaNS == mans).SingleOrDefault();
                dbCTL.LCB_WEB_Admin.Remove(usA);
                dbCTL.SaveChanges();
                LoadDataGrid();
            }
            catch (Exception ex)
            {

            }
        }

        protected void gridPhanQuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gridPhanQuyen.SelectedRow;
            txtMaNS.Text = ((Label)row.Cells[0].FindControl("lblMaNS")).Text;
            chkActive.Checked = ((CheckBox)row.Cells[0].FindControl("chkKichHoat")).Checked;
            string svaitro = ((Label)row.Cells[0].FindControl("lblVaiTro")).Text;
            string[] role = svaitro.Split('|');
            if (role.Length > 0)
            {
                foreach (ListItem item in chkList_ChucNang.Items)
                {
                    item.Selected = false;
                }
                for (int i = 0; i < role.Length; i++)
                {
                    foreach (ListItem item in chkList_ChucNang.Items)
                    {
                        if (item.Value.ToString().Equals(role[i].ToString()))
                        {
                            item.Selected = true;
                        }
                    }                    
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNS.Text)) return;
            if (chkList_ChucNang.Items.Count <= 0) return;
            int sus = 0;
            LCB_WEB_Admin usAdmin = new LCB_WEB_Admin();
            usAdmin = dbCTL.LCB_WEB_Admin.Where(x => x.MaNS == txtMaNS.Text.Trim().ToUpper()).FirstOrDefault();
            if (usAdmin != null)
            {
                string role = "0";
                for (int i = 0; i < chkList_ChucNang.Items.Count; i++)
                {
                    if (chkList_ChucNang.Items[i].Selected == true)
                    {
                        role += "|" + chkList_ChucNang.Items[i].Value;
                    }
                }
                usAdmin.KichHoat = chkActive.Checked;
                usAdmin.VaiTro = role;
                sus = dbCTL.SaveChanges();   
            }
            else
            {
                string role = "0";
                for (int i = 0; i < chkList_ChucNang.Items.Count; i++)
                {
                    if (chkList_ChucNang.Items[i].Selected == true)
                    {
                        role += "|" + chkList_ChucNang.Items[i].Value;
                    }
                }
                usAdmin = new LCB_WEB_Admin();
                usAdmin.MaNS = txtMaNS.Text.Trim().ToUpper();
                usAdmin.KichHoat = chkActive.Checked;                
                usAdmin.VaiTro = role;

                dbCTL.LCB_WEB_Admin.Add(usAdmin);
                sus = dbCTL.SaveChanges();
            }
            if (sus != 0)
            {
                divMesssenger.Style["display"] = "block";
                lblMessenger.Text = "Cập nhật thành công!";
                LoadDataGrid();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            txtMaNS.Text = "";
            if (Session["DataOld"] == null) return;
            DataTable dt = Session["DataOld"] as DataTable;
            dt.DefaultView.RowFilter = "MaNS like '%" + txtSearch.Value.Trim() + "%' OR HoTen like '%" + txtSearch.Value.Trim() + "%'";
            if (dt.DefaultView.ToTable() != null && dt.DefaultView.ToTable().Rows.Count > 0)
            {
                gridPhanQuyen.DataSource = dt.DefaultView.ToTable();
                gridPhanQuyen.DataBind();
            }
            else
            {
                Load_ResetGV();
            }
        }
    }
}