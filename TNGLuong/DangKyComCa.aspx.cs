using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Cls_DangKyAnCa;

namespace TNGLuong
{
    public partial class DangKyComCa : System.Web.UI.Page
    {
        dbNSTLContent db;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new dbNSTLContent();
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            btncloseNX.ServerClick += new EventHandler(btncloseNX_Click);
            if (Session["userid"] != null)
            {
                if (!IsPostBack)
                {
                    Session["clsDangKy"] = null;
                    Load_cmbNam();
                    Load_cmbThang();
                    Load_gvDangKy();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Load_cmbNam()
        {
            ListItem item;
            for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 5; i--)
            {
                item = new ListItem();
                item.Text = i.ToString();
                item.Value = i.ToString();
                cmbNam.Items.Add(item);
            }
            cmbNam.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void Load_cmbThang()
        {
            ListItem item;
            for (int i = 1; i <= 12; i++)
            {
                item = new ListItem();
                item.Text = "Tháng " + i.ToString();
                item.Value = i.ToString();
                cmbThang.Items.Add(item);
            }
            cmbThang.SelectedValue = DateTime.Now.Month.ToString();
        }

        protected void Load_gvDangKy()
        {
            if (Session["userid"] == null) return;
            if (cmbNam.SelectedValue == null && cmbNam.SelectedValue.Length < 4) return;
            if (cmbThang.SelectedValue == null && cmbThang.SelectedValue == "" && cmbThang.SelectedValue.Length > 2) return;
            int iMaNS_ID = Convert.ToInt32(Session["userid"].ToString());
            int iThang = Convert.ToInt32(cmbThang.SelectedValue);
            int iNam = Convert.ToInt32(cmbNam.SelectedValue);
            object[] sqlPr =
                {
                    new SqlParameter("@iMaNS_ID", iMaNS_ID),
                    new SqlParameter("@iThang", iThang),
                    new SqlParameter("@iNam", iNam)
                };
            string sqlQuery = "[dbo].[pr_WEB_TAC_DangKy_AnCa_Ngay_SelectAll_wMaNS_ID_and_Date] @iMaNS_ID,@iThang,@iNam";
            List<clsDangKy_AnCa> lst = new List<clsDangKy_AnCa>();
            lst = db.Database.SqlQuery<clsDangKy_AnCa>(sqlQuery, sqlPr).ToList();
            if (lst != null && lst.Count > 0)
            {
                gvDangKy.DataSource = lst;
                gvDangKy.DataBind();
                List<clsDangKy_AnCa> lstAn = new List<clsDangKy_AnCa>();
                lstAn = lst.Where(x => x.AnCa == true).ToList();
                if (lstAn != null && lstAn.Count > 0)
                {
                    lblTongNgayDK.Text = "Số ngày đăng ký: " + lstAn.Count + " - Số ngày không đăng ký: " + (lst.Count - lstAn.Count);
                }
                else
                {
                    lblTongNgayDK.Text = "Số ngày đăng ký: 0 - Số ngày không đăng ký: " + lst.Count;
                }
            }
            else
            {
                List<clsDangKy_AnCa> lstChk = new List<clsDangKy_AnCa>();
                DataTable dt = ultils.CreateDataTable<clsDangKy_AnCa>(lstChk);
                DataRow row = dt.NewRow();
                row["MaNS_ID"] = 0;
                row["Ngay"] = DateTime.Now;
                row["ThuTV"] = "";
                row["AnCa"] = false;
                row["GhiChu"] = "";
                dt.Rows.Add(row);
                gvDangKy.DataSource = dt;
                gvDangKy.DataBind();
                gvDangKy.Rows[0].Cells.Clear();
                gvDangKy.Rows[0].Cells.Add(new TableCell());
                gvDangKy.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                gvDangKy.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gvDangKy.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                lblTongNgayDK.Text = "Số ngày đăng ký: 0 - Số ngày không đăng ký: 0";
            }
        }

        protected void Load_DataNhanXet()
        {
            if (Session["clsDangKy"] == null) return;
            TAC_DangKy_AnCa_Ngay cls = new TAC_DangKy_AnCa_Ngay();
            cls = Session["clsDangKy"] as TAC_DangKy_AnCa_Ngay;
            if (cls != null)
                txtNhanXet.Value = cls.GhiChu;
            else
                txtNhanXet.Value = "";
        }

        protected void gvDangKy_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("nhanxetCC"))
            {
                DateTime daNgay = Convert.ToDateTime(e.CommandArgument.ToString());
                int iMaNS_ID = Convert.ToInt32(Session["userid"].ToString());
                object[] sqlPr =
                {
                    new SqlParameter("@iMaNS_ID", iMaNS_ID),
                    new SqlParameter("@daNgay", daNgay)
                };
                string sqlQuery = "[dbo].[pr_WEB_TAC_DangKy_AnCa_Ngay_SelectOne_wMaNS_ID_and_Ngay] @iMaNS_ID,@daNgay";
                TAC_DangKy_AnCa_Ngay cls = new TAC_DangKy_AnCa_Ngay();
                cls = db.Database.SqlQuery<TAC_DangKy_AnCa_Ngay>(sqlQuery, sqlPr).FirstOrDefault();
                if (cls != null)
                {
                    Session["clsDangKy"] = cls;
                    dtpNgay.Text = daNgay.ToString("yyyy-MM-dd");
                    divNhanXet.Style["display"] = "block";
                    Load_DataNhanXet();
                }
                else
                {
                    lblMessenger.Text = "Chưa đăng ký ăn ca ngày "+ daNgay.ToString("dd/MM/yyyy");
                    addthismodalContact.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session["clsDangKy"] == null) return;
            TAC_DangKy_AnCa_Ngay cls = new TAC_DangKy_AnCa_Ngay();
            cls = Session["clsDangKy"] as TAC_DangKy_AnCa_Ngay;
            if (cls != null)
            {
                if (Session["userid"] == null) return;
                if (string.IsNullOrEmpty(dtpNgay.Text)) return;
                DateTime daNgay = Convert.ToDateTime(dtpNgay.Text);
                int iMaNS_ID = Convert.ToInt32(Session["userid"].ToString());
                string sNhanXet = txtNhanXet.Value;
                SqlParameter[] params_ = new SqlParameter[4];
                params_[0] = new SqlParameter("@iMaNS_ID", iMaNS_ID);
                params_[1] = new SqlParameter("@daNgay", daNgay);
                params_[2] = new SqlParameter("@bAnCa", true);
                params_[3] = new SqlParameter("@sGhiChu", sNhanXet);

                string sqlQuery2 = "[dbo].[pr_WEB_TAC_DangKy_AnCa_Ngay_Update_NhanXet] @iMaNS_ID,@daNgay,@bAnCa,@sGhiChu";
                db.Database.ExecuteSqlCommand(sqlQuery2, params_);

                Load_gvDangKy();
                lblMessenger.Text = "Đã cập nhật thành công.";
                divNhanXet.Style["display"] = "none";
                divThongBao.Style["display"] = "block";
                addthismodalContact.Style["display"] = "block";
            }
        }

        protected void btncloseNX_Click(object sender, EventArgs e)
        {
            divNhanXet.Style["display"] = "none";
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
            divThongBao.Style["display"] = "none";
        }

        protected void chkIsDangKy_CheckedChanged(object sender, EventArgs e)
        {
            if (Session["userid"] == null) return;
            int iRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            Label lbl = (Label)gvDangKy.Rows[iRowIndex].FindControl("lblNgay");
            CheckBox chk = (CheckBox)gvDangKy.Rows[iRowIndex].FindControl("chkIsDangKy");
            if (lbl == null) return;
            if (string.IsNullOrEmpty(lbl.Text)) return;
            if (chk == null) return;
            DateTime daNgay = Convert.ToDateTime(lbl.Text);
            if (DateTime.Compare(daNgay.Date, DateTime.Now.Date) < 0)
            {
                if(chk.Checked == true)
                    chk.Checked = false;
                lblMessenger.Text = "Đã qua ngày đăng ký ăn ca!";
                addthismodalContact.Style["display"] = "block";
                divThongBao.Style["display"] = "block";
                return;
            }
            else if (DateTime.Compare(daNgay.Date, DateTime.Now.Date) == 0 && DateTime.Now.Hour > 7 && DateTime.Now.Minute > 30)
            {
                if (chk.Checked == true)
                    chk.Checked = false;
                lblMessenger.Text = "Thời gian đăng ký cơm ca là trước 7h30!";
                addthismodalContact.Style["display"] = "block";
                divThongBao.Style["display"] = "block";
                return;
            }
            else
            {
                int iMaNS_ID = Convert.ToInt32(Session["userid"].ToString());
                object[] sqlPr =
                    {
                    new SqlParameter("@iMaNS_ID", iMaNS_ID),
                    new SqlParameter("@daNgay", daNgay)
                };
                string sqlQuery = "[dbo].[pr_WEB_TAC_DangKy_AnCa_Ngay_SelectOne_wMaNS_ID_and_Ngay] @iMaNS_ID,@daNgay";
                TAC_DangKy_AnCa_Ngay cls = new TAC_DangKy_AnCa_Ngay();
                cls = db.Database.SqlQuery<TAC_DangKy_AnCa_Ngay>(sqlQuery, sqlPr).FirstOrDefault();
                int sus = 0;
                if (cls != null)
                {
                    SqlParameter[] params_ = new SqlParameter[4];
                    params_[0] = new SqlParameter("@iMaNS_ID", cls.MaNS_ID);
                    params_[1] = new SqlParameter("@daNgay", cls.Ngay);
                    params_[2] = new SqlParameter("@bAnCa", chk.Checked);
                    params_[3] = new SqlParameter("@sGhiChu", cls.GhiChu);

                    string sqlQuery2 = "[dbo].[pr_WEB_TAC_DangKy_AnCa_Ngay_Update_NhanXet] @iMaNS_ID,@daNgay,@bAnCa,@sGhiChu";
                    sus = db.Database.ExecuteSqlCommand(sqlQuery2, params_);
                }
                else
                {
                    cls = new TAC_DangKy_AnCa_Ngay();
                    cls.Ngay = daNgay;
                    cls.MaNS_ID = iMaNS_ID;
                    cls.AnCa = chk.Checked;
                    db.TAC_DangKy_AnCa_Ngay.Add(cls);
                    sus = db.SaveChanges();
                }
                if (sus != 0)
                {
                    Session["clsDangKy"] = cls;
                    lblMessenger.Text = "Đã cập nhật thành công.";
                    addthismodalContact.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                }
            }
        }

        protected void gvDangKy_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblweekday_id");
                if (lbl != null && !string.IsNullOrEmpty(lbl.Text))
                {
                    if (lbl.Text == "1")
                    {
                        e.Row.BackColor = Color.Red;
                        e.Row.ForeColor = Color.White;
                    }
                    else
                    {
                        e.Row.BackColor = Color.White;
                        e.Row.ForeColor = Color.Black;
                    }
                }
            }
        }

        protected void cmbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_gvDangKy();
        }

        protected void cmbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_gvDangKy();
        }
    }
}