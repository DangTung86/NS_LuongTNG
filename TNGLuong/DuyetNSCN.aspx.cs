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
using TNGLuong.ModelsView;

namespace TNGLuong
{
    public partial class DuyetNSCN : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        DatabaseManager dm = null;
        StoredParameterCollection spc = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            db = new TNG_CTLDbContact();
            dm = new DatabaseManager(db);
            spc = new StoredParameterCollection();
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    Session["PD_PhongBanID"] = Session["ChucVu"].ToString();
                    if (!string.IsNullOrEmpty(txtDate.Text))
                    {
                        getDataset();
                    }
                }
            }
            else
                Response.Redirect("Login.aspx");
        }

        protected void getDataset()
        {
            try
            {
                int phongbanid = 0;
                if (Session["ChucVu"] != null)
                    phongbanid = Convert.ToInt32(Session["ChucVu"].ToString());
                string sqlQuery = "pr_WEB_LCB_Select_DuyetNangSuat_ToTruong";
                spc.Add("@PhongBanID", SqlDbType.Int, ParameterDirection.Input, phongbanid);
                spc.Add("@Ngay", SqlDbType.Date, ParameterDirection.Input, txtDate.Text);

                DataSet ds = new DataSet();
                ds = dm.ExecuteStoredProcedure(sqlQuery, spc);
                if (ds != null && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    ds.Tables[2].DefaultView.Sort = "HoTen ASC";
                    gridCongNhanChuaNhap.DataSource = ds.Tables[2];
                    gridCongNhanChuaNhap.DataBind();
                }
                else
                {
                    List<CongNhanChuaNhap> lst = new List<CongNhanChuaNhap>();
                    DataTable dt = ultils.CreateDataTable<CongNhanChuaNhap>(lst);
                    dt.Rows.Add(dt.NewRow());
                    gridCongNhanChuaNhap.DataSource = dt;
                    gridCongNhanChuaNhap.DataBind();
                    gridCongNhanChuaNhap.Rows[0].Cells.Clear();
                    gridCongNhanChuaNhap.Rows[0].Cells.Add(new TableCell());
                    gridCongNhanChuaNhap.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                    gridCongNhanChuaNhap.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridCongNhanChuaNhap.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
                if (ds != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    Session["DataUserCN"] = ds.Tables[1];
                }
                else
                {
                    Session["DataUserCN"] = null;
                }
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    pnameCNDN.Visible = true;
                    lvUser.DataSource = ds.Tables[0];
                    lvUser.DataBind();
                }
                else
                    pnameCNDN.Visible = false;                           
            }
            catch (Exception ex) { }
        }

        protected void loadDataGrid(GridView gridNangSuatUser, int mansid)
        {
            try
            {
                if(Session["DataUserCN"] != null)
                {
                    DataTable dt = Session["DataUserCN"] as DataTable;
                    dt = dt.Select("MaNS_ID = " + mansid).CopyToDataTable();
                    dt.DefaultView.Sort = "TT ASC,STT_CongDoan ASC";
                    gridNangSuatUser.DataSource = dt;
                    gridNangSuatUser.DataBind();

                    decimal total = 0;
                    gridNangSuatUser.FooterRow.Cells[0].Text = "Tổng lương(VNĐ): ";
                    gridNangSuatUser.FooterRow.Cells[0].Font.Bold = true;
                    gridNangSuatUser.FooterRow.Cells[0].ColumnSpan = 4; 
                    gridNangSuatUser.FooterRow.Cells[1].Visible = false;
                    gridNangSuatUser.FooterRow.Cells[2].Visible = false;
                    gridNangSuatUser.FooterRow.Cells[3].Visible = false;
                    foreach (DataRow row in dt.Rows)
                    {
                        if(row["HeSoK_DonGia"] != null && row["HeSoK_DonGia"].ToString() != "" && row["HeSoK_DonGia"].ToString() != "0")
                            total += (Convert.ToDecimal(row["HeSoK_DonGia"].ToString()) * Convert.ToDecimal(row["DonGia"].ToString()) * Convert.ToDecimal(row["CongNhan"].ToString()));
                        else
                            total += (Convert.ToDecimal(row["DonGia"].ToString()) * Convert.ToDecimal(row["CongNhan"].ToString())); 
                    }
                    gridNangSuatUser.FooterRow.Cells[4].Text = string.Format("{0:#,0.#}", total);
                    gridNangSuatUser.FooterRow.Cells[4].Font.Bold = true;
                    gridNangSuatUser.FooterRow.Cells[4].Style["text-align"] = "right";
                    gridNangSuatUser.FooterRow.Cells[4].Style["padding-right"] = "12px";
                    gridNangSuatUser.FooterRow.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    List<DSNSToTruong> lst = new List<DSNSToTruong>();
                    DataTable dt = ultils.CreateDataTable<DSNSToTruong>(lst);
                    dt.Rows.Add(dt.NewRow());
                    gridNangSuatUser.DataSource = dt;
                    gridNangSuatUser.DataBind();
                    gridNangSuatUser.Rows[0].Cells.Clear();
                    gridNangSuatUser.Rows[0].Cells.Add(new TableCell());
                    gridNangSuatUser.Rows[0].Cells[0].ColumnSpan = 4;
                    gridNangSuatUser.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridNangSuatUser.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

                    decimal total = 0;
                    gridNangSuatUser.FooterRow.Cells[0].Text = "Tổng lương(VNĐ): ";
                    gridNangSuatUser.FooterRow.Cells[0].Font.Bold = true;
                    gridNangSuatUser.FooterRow.Cells[0].ColumnSpan = 4;
                    gridNangSuatUser.FooterRow.Cells[1].Visible = false;
                    gridNangSuatUser.FooterRow.Cells[2].Visible = false;
                    gridNangSuatUser.FooterRow.Cells[3].Visible = false;
                    gridNangSuatUser.FooterRow.Cells[4].Text = string.Format("{0:0.#}", total);
                    gridNangSuatUser.FooterRow.Cells[4].Font.Bold = true;
                    gridNangSuatUser.FooterRow.Cells[4].Style["text-align"] = "right";
                    gridNangSuatUser.FooterRow.Cells[4].Style["padding-right"] = "12px";
                    gridNangSuatUser.FooterRow.BackColor = System.Drawing.Color.Beige;
                }
            }
            catch (Exception ex) {
                List<DSNSToTruong> lst = new List<DSNSToTruong>();
                DataTable dt = ultils.CreateDataTable<DSNSToTruong>(lst);
                dt.Rows.Add(dt.NewRow());
                gridNangSuatUser.DataSource = dt;
                gridNangSuatUser.DataBind();
                gridNangSuatUser.Rows[0].Cells.Clear();
                gridNangSuatUser.Rows[0].Cells.Add(new TableCell());
                gridNangSuatUser.Rows[0].Cells[0].ColumnSpan = 4; 
                gridNangSuatUser.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gridNangSuatUser.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

                decimal total = 0;
                gridNangSuatUser.FooterRow.Cells[0].Text = "Tổng lương(VNĐ): ";
                gridNangSuatUser.FooterRow.Cells[0].Font.Bold = true;
                gridNangSuatUser.FooterRow.Cells[0].ColumnSpan = 4;
                gridNangSuatUser.FooterRow.Cells[1].Visible = false;
                gridNangSuatUser.FooterRow.Cells[2].Visible = false;
                gridNangSuatUser.FooterRow.Cells[3].Visible = false;
                gridNangSuatUser.FooterRow.Cells[4].Text = string.Format("{0:0.#}", total);
                gridNangSuatUser.FooterRow.Cells[4].Font.Bold = true;
                gridNangSuatUser.FooterRow.Cells[4].Style["text-align"] = "right";
                gridNangSuatUser.FooterRow.Cells[4].Style["padding-right"] = "12px";
                gridNangSuatUser.FooterRow.BackColor = System.Drawing.Color.Beige;
            }
        }

        protected void lvUser_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.WebControls.GridView grid = e.Item.FindControl("gridNangSuatUser") as System.Web.UI.WebControls.GridView;
                Label label = e.Item.FindControl("lblMaNS_ID") as Label;
                Label lblHoTen = e.Item.FindControl("lblHoTen") as Label; 
                Label lblTyLeTH = e.Item.FindControl("lblTyLeTH") as Label;
                Label lblPD = e.Item.FindControl("lblPD") as Label;
                if (label != null && !string.IsNullOrEmpty(label.Text))
                {
                    int mansid = Convert.ToInt32(label.Text);
                    bool pd = bool.Parse(lblPD.Text);
                    if (pd == false)
                        lblHoTen.ForeColor = System.Drawing.Color.Red;
                    else
                        lblHoTen.ForeColor = System.Drawing.Color.White;
                    loadDataGrid(grid, mansid);
                }
            }
            catch(Exception ex) { }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtDate.Text))
            {
                getDataset();
            }
        }

        protected void gridNangSuatUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                System.Web.UI.WebControls.GridView grid = sender as System.Web.UI.WebControls.GridView;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string tt = DataBinder.Eval(e.Row.DataItem, "TT").ToString();
                    if (!string.IsNullOrEmpty(tt) && tt.Equals("1"))
                    {
                        e.Row.Cells[0].Style["background-color"] = "red";
                        e.Row.Cells[1].Style["background-color"] = "red";
                        e.Row.Cells[2].Style["background-color"] = "red";
                        e.Row.Cells[3].Style["background-color"] = "red";
                        e.Row.Cells[4].Style["background-color"] = "red";
                        e.Row.Cells[0].Style["color"] = "white";
                        e.Row.Cells[1].Style["color"] = "white";
                        e.Row.Cells[2].Style["color"] = "white";
                        e.Row.Cells[3].Style["color"] = "white";
                        e.Row.Cells[4].Style["color"] = "white";
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
            divThongBao.Style["display"] = "none";
        }
    }
}