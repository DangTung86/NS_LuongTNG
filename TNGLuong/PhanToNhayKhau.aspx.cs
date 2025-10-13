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
    public partial class PhanToNhayKhau : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new TNG_CTLDbContact();
            btnclose.ServerClick += new EventHandler(btnclose_Click);
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    Session["DonViID_CN"] = null;
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    if (!string.IsNullOrEmpty(txtDate.Text))
                    {
                        int phongid_ns = 0;
                        if (Session["ChucVu"] != null)
                            phongid_ns = Convert.ToInt32(Session["ChucVu"].ToString());
                        View_ToMay tm = new View_ToMay();
                        tm = db.View_ToMay.Where(x => x.PhongBanID == phongid_ns).SingleOrDefault();
                        if (tm != null)
                            Session["DonViID_CN"] = tm.DonViID;
                        loadDataGrid();
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void loadDataGrid()
        {
            try
            {
                int phongid_ns = 0;
                if (Session["ChucVu"] != null)
                    phongid_ns = Convert.ToInt32(Session["ChucVu"].ToString());
                DateTime dte = Convert.ToDateTime(txtDate.Text);
                object[] sqlPr =
                {
                    new SqlParameter("@PhongBanID", phongid_ns),
                    new SqlParameter("@Ngay", dte.Date)
                };
                string sqlQuery = "[dbo].[pr_WEB_LCB_Select_PhanToNhayKhau_ToTruong] @PhongBanID,@Ngay";
                List<PhanToNK> lst = new List<PhanToNK>();
                lst = db.Database.SqlQuery<PhanToNK>(sqlQuery, sqlPr).ToList();
                //DataTable dt = ultils.CreateDataTable<PhanToNK>(lst);
                if (lst != null && lst.Count > 0)
                {
                    gridPhanToNK.DataSource = lst;
                    gridPhanToNK.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("MaNS_ID", typeof(int)));
                    dt.Columns.Add(new DataColumn("PhongBanID_NS", typeof(int)));
                    dt.Columns.Add(new DataColumn("HoTen", typeof(string)));
                    dt.Columns.Add(new DataColumn("ToNK1", typeof(int)));
                    dt.Columns.Add(new DataColumn("ToNK2", typeof(int)));
                    dt.Columns.Add(new DataColumn("ToNK3", typeof(int)));
                    DataRow dr = dt.NewRow();
                    dr["MaNS_ID"] = 0;
                    dr["PhongBanID_NS"] = 0;
                    dr["HoTen"] = "s";
                    dr["ToNK1"] = 0;
                    dr["ToNK2"] = 0;
                    dr["ToNK3"] = 0;
                    dt.Rows.Add(dr);

                    gridPhanToNK.DataSource = dt;
                    gridPhanToNK.DataBind();
                    gridPhanToNK.Rows[0].Cells.Clear();
                    gridPhanToNK.Rows[0].Cells.Add(new TableCell());
                    gridPhanToNK.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                    gridPhanToNK.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridPhanToNK.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex) { }
        }

        protected List<View_ToMay> loadListDataPhongBan()
        {
            try
            {                
                if (Session["DonViID_CN"] != null)
                {
                    string donviid = Session["DonViID_CN"].ToString();
                    List<View_ToMay> lst = new List<View_ToMay>();
                    lst = db.View_ToMay.Where(x => x.DonViID == donviid).OrderBy(x => x.TenPhongban).ToList();
                    return lst;
                }
                else
                    return null;
            }
            catch { return null; }
        }

        protected void gridPhanToNK_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlPhongBan1 = (DropDownList)e.Row.FindControl("ddlPhongBan1");
                    DropDownList ddlPhongBan2 = (DropDownList)e.Row.FindControl("ddlPhongBan2");
                    DropDownList ddlPhongBan3 = (DropDownList)e.Row.FindControl("ddlPhongBan3");

                    if (ddlPhongBan1 != null)
                    {
                        List<View_ToMay> lst1 = new List<View_ToMay>();
                        lst1 = loadListDataPhongBan();
                        ddlPhongBan1.DataSource = lst1;
                        ddlPhongBan1.DataTextField = "TenPhongban";
                        ddlPhongBan1.DataValueField = "PhongBanID";
                        ddlPhongBan1.DataBind();
                        ddlPhongBan1.Items.Insert(0, new ListItem("", "0"));
                    }

                    if (ddlPhongBan2 != null)
                    {
                        List<View_ToMay> lst2 = new List<View_ToMay>();
                        lst2 = loadListDataPhongBan();
                        ddlPhongBan2.DataSource = lst2;
                        ddlPhongBan2.DataTextField = "TenPhongban";
                        ddlPhongBan2.DataValueField = "PhongBanID";
                        ddlPhongBan2.DataBind();
                        ddlPhongBan2.Items.Insert(0, new ListItem("", "0"));
                    }

                    if (ddlPhongBan3 != null)
                    {
                        List<View_ToMay> lst3 = new List<View_ToMay>();
                        lst3 = loadListDataPhongBan();
                        ddlPhongBan3.DataSource = lst3;
                        ddlPhongBan3.DataTextField = "TenPhongban";
                        ddlPhongBan3.DataValueField = "PhongBanID";
                        ddlPhongBan3.DataBind();
                        ddlPhongBan3.Items.Insert(0, new ListItem("", "0"));
                    }

                    if (DataBinder.Eval(e.Row.DataItem, "ToNK1") != null && !string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ToNK1").ToString()))
                        ddlPhongBan1.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ToNK1").ToString();
                    if (DataBinder.Eval(e.Row.DataItem, "ToNK2") != null && !string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ToNK2").ToString()))
                        ddlPhongBan2.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ToNK2").ToString();
                    if (DataBinder.Eval(e.Row.DataItem, "ToNK3") != null && !string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ToNK3").ToString()))
                        ddlPhongBan3.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ToNK3").ToString();
                }
            }
            catch { }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            loadDataGrid();
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtNhayKhau = new DataTable("dtNhayKhau");
                dtNhayKhau.Columns.Add(new DataColumn("MaNS_ID", typeof(int)));
                dtNhayKhau.Columns.Add(new DataColumn("PhongBanID_NS", typeof(int)));
                dtNhayKhau.Columns.Add(new DataColumn("Ngay", typeof(DateTime)));
                dtNhayKhau.Columns.Add(new DataColumn("PhongBanID_NhayKhau", typeof(int)));
                int id = 0;
                if (gridPhanToNK.Rows.Count > 0)
                {
                    DataRow dr = null;
                    DateTime dte = DateTime.Parse(txtDate.Text);
                    foreach (GridViewRow row in gridPhanToNK.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            string lblMaNS = ((Label)row.Cells[0].FindControl("lblMaNS")).Text;
                            string lblPhongBanID_NS = ((Label)row.Cells[0].FindControl("lblPhongBanID_NS")).Text;
                            DropDownList ddlPhongBan1 = (DropDownList)row.Cells[0].FindControl("ddlPhongBan1");
                            DropDownList ddlPhongBan2 = (DropDownList)row.Cells[0].FindControl("ddlPhongBan2");
                            DropDownList ddlPhongBan3 = (DropDownList)row.Cells[0].FindControl("ddlPhongBan3");
                            if(!string.IsNullOrEmpty(lblMaNS) && !string.IsNullOrEmpty(lblPhongBanID_NS))
                            {                                
                                int mans = int.Parse(lblMaNS);
                                int phongbanid_ns = int.Parse(lblPhongBanID_NS);
                                if(ddlPhongBan1.SelectedValue != null && ddlPhongBan1.SelectedValue.ToString() != "" && ddlPhongBan1.SelectedValue.ToString() != "0")
                                {
                                    dr = dtNhayKhau.NewRow();
                                    dr["MaNS_ID"] = mans;
                                    dr["PhongBanID_NS"] = phongbanid_ns;
                                    dr["Ngay"] = dte.Date;
                                    dr["PhongBanID_NhayKhau"] = ddlPhongBan1.SelectedValue.ToString();
                                    dtNhayKhau.Rows.Add(dr);
                                }
                                if (ddlPhongBan2.SelectedValue != null && ddlPhongBan2.SelectedValue.ToString() != "" && ddlPhongBan2.SelectedValue.ToString() != "0")
                                {
                                    dr = dtNhayKhau.NewRow();
                                    dr["MaNS_ID"] = mans;
                                    dr["PhongBanID_NS"] = phongbanid_ns;
                                    dr["Ngay"] = dte.Date;
                                    dr["PhongBanID_NhayKhau"] = ddlPhongBan2.SelectedValue.ToString();
                                    dtNhayKhau.Rows.Add(dr);
                                }
                                if (ddlPhongBan3.SelectedValue != null && ddlPhongBan3.SelectedValue.ToString() != "" && ddlPhongBan3.SelectedValue.ToString() != "0")
                                {
                                    dr = dtNhayKhau.NewRow();
                                    dr["MaNS_ID"] = mans;
                                    dr["PhongBanID_NS"] = phongbanid_ns;
                                    dr["Ngay"] = dte.Date;
                                    dr["PhongBanID_NhayKhau"] = ddlPhongBan3.SelectedValue.ToString();
                                    dtNhayKhau.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    if (dtNhayKhau != null && dtNhayKhau.Rows.Count > 0)
                    {
                        var parameter = new SqlParameter("@dtSoLuong", SqlDbType.Structured);
                        parameter.Value = dtNhayKhau;
                        parameter.TypeName = "dbo.udt_web_LCB_NhayKhau_DanhSachPhongBan_Update";
                        string sqlQuery = "[dbo].[pr_Web_LCB_NhayKhau_DanhSachPhongBan_Update_PhanToNhayKhau_UDT] @dtSoLuong";
                        id = id + db.Database.ExecuteSqlCommand(sqlQuery, parameter);
                    }
                    if (id != 0)
                    {
                        loadDataGrid();
                        lblMessenger.Text = "Đã cập nhật phân tổ nhảy khâu.";
                        addthismodalContact.Style["display"] = "block";
                    }
                }
            }
            catch { }
        }        
    }
}