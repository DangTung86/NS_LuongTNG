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
    public partial class PhanCumTruong : System.Web.UI.Page
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
                    int phongid_ns = 0;
                    if (Session["ChucVu"] != null)
                        phongid_ns = Convert.ToInt32(Session["ChucVu"].ToString());
                    View_ToMay tm = new View_ToMay();
                    tm = db.View_ToMay.Where(x => x.PhongBanID == phongid_ns).SingleOrDefault();
                    if (tm != null)
                        Session["DonViID_CN"] = tm.DonViID;
                    loadDataToMay();
                    loadDataMaHang();
                    loadListNhanSu();
                    loadDataGrid();
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
                string mahang = "";
                int donviid = 0;
                int phongbanid = 0;
                if(Session["DonViID_CN"] != null)
                    donviid = Convert.ToInt32(Session["DonViID_CN"].ToString());
                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    phongbanid = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                if (Session["MaHang"] != null)
                    mahang = Session["MaHang"].ToString();
                object[] sqlPr =
                {
                    new SqlParameter("@sMaHang", mahang),
                    new SqlParameter("@iDonViID", donviid),
                    new SqlParameter("@iPhongBanID", phongbanid),
                    new SqlParameter("@iNam", DateTime.Now.Year),
                    new SqlParameter("@iThang", DateTime.Now.Month)
                };
                string sqlQuery = "[dbo].[pr_WEB_LCB_PhanCumTruong_Select_DanhSachCum_wMaHang_And_PhongBan_And_ThangNam] @sMaHang,@iDonViID,@iPhongBanID,@iNam,@iThang";
                List<clsPhanCumTruong> lst = new List<clsPhanCumTruong>();
                lst = db.Database.SqlQuery<clsPhanCumTruong>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    gridPhanCumTrg.DataSource = lst;
                    gridPhanCumTrg.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("STT", typeof(long)));
                    dt.Columns.Add(new DataColumn("ID_Cum", typeof(int)));
                    dt.Columns.Add(new DataColumn("TenCum", typeof(string)));
                    dt.Columns.Add(new DataColumn("MaNS_ID", typeof(int)));
                    DataRow dr = dt.NewRow();
                    dr["STT"] = 0;
                    dr["ID_Cum"] = 0;
                    dr["TenCum"] = "s";
                    dr["MaNS_ID"] = 0;
                    dt.Rows.Add(dr);

                    gridPhanCumTrg.DataSource = dt;
                    gridPhanCumTrg.DataBind();
                    gridPhanCumTrg.Rows[0].Cells.Clear();
                    gridPhanCumTrg.Rows[0].Cells.Add(new TableCell());
                    gridPhanCumTrg.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                    gridPhanCumTrg.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridPhanCumTrg.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex) {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("STT", typeof(long)));
                dt.Columns.Add(new DataColumn("ID_Cum", typeof(int)));
                dt.Columns.Add(new DataColumn("TenCum", typeof(string)));
                dt.Columns.Add(new DataColumn("MaNS_ID", typeof(int)));
                DataRow dr = dt.NewRow();
                dr["STT"] = 0;
                dr["ID_Cum"] = 0;
                dr["TenCum"] = "s";
                dr["MaNS_ID"] = 0;
                dt.Rows.Add(dr);

                gridPhanCumTrg.DataSource = dt;
                gridPhanCumTrg.DataBind();
                gridPhanCumTrg.Rows[0].Cells.Clear();
                gridPhanCumTrg.Rows[0].Cells.Add(new TableCell());
                gridPhanCumTrg.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                gridPhanCumTrg.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gridPhanCumTrg.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void loadDataToMay()
        {
            try
            {
                string donviid = "";
                if (Session["DonViID_CN"] != null)
                    donviid = Session["DonViID_CN"].ToString();
                List<View_ToMay> lst = new List<View_ToMay>();
                lst = db.View_ToMay.Where(x => x.DonViID == donviid).OrderBy(x => x.TenPhongban).ToList();
                if (lst != null && lst.Count > 0)
                {
                    Session["lstToMay"] = lst;
                    ddlToMay.DataSource = lst;
                    ddlToMay.DataBind();
                    if (Session["ChucVu"] != null)
                    {
                        ddlToMay.SelectedValue = Session["ChucVu"].ToString();
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void loadDataMaHang()
        {
            try
            {
                int donviid = 0;
                int phongid = 0;
                if (Session["DonViID_CN"] != null)
                    donviid = Convert.ToInt32(Session["DonViID_CN"].ToString());
                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    phongid = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                object[] sqlPr =
                {
                    new SqlParameter("@PhongBanID", phongid),
                    new SqlParameter("@DonViID", donviid)
                };
                string sqlQuery = "[dbo].[pr_WEB_LCB_PhanCumTruong_Select_MaHang_wPhongID_DonViId] @PhongBanID,@DonViID";
                List<MaHangNhayKhau> lst = new List<MaHangNhayKhau>();
                lst = db.Database.SqlQuery<MaHangNhayKhau>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    DataTable dt = ultils.CreateDataTable<MaHangNhayKhau>(lst);
                    Session["DTMaHang"] = dt;
                    ddlMaHang.DataSource = lst;
                    ddlMaHang.DataBind();

                    if (ddlMaHang.SelectedValue != null && ddlMaHang.SelectedValue.ToString() != "" && ddlMaHang.SelectedValue.ToString() != "0")
                    {
                        long id = long.Parse(ddlMaHang.SelectedValue.ToString());
                        DataRow row = dt.Select("STT=" + id).Single();
                        if (row != null)
                        {
                            Session["MaHang"] = row["MaHang"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void loadListNhanSu()
        {
            try
            {
                int phongbanid = 0;
                string mahang = "";
                if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                    phongbanid = Convert.ToInt32(ddlToMay.SelectedValue.ToString());
                if (Session["MaHang"] != null)
                    mahang = Session["MaHang"].ToString();
                object[] sqlPr =
                {
                    new SqlParameter("@sMaHang", mahang),
                    new SqlParameter("@iPhongBanID", phongbanid),
                    new SqlParameter("@iThang", DateTime.Now.Month),
                    new SqlParameter("@iNam", DateTime.Now.Year)
                };
                string sqlQuery = "[dbo].[pr_WEB_DanhSachNhanSu_wPhongBanID_and_Ngay] @sMaHang,@iPhongBanID,@iThang,@iNam";
                List<clsNhanSu> lst = new List<clsNhanSu>();
                lst = db.Database.SqlQuery<clsNhanSu>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                    Session["lstNhanSu"] = lst;
                else
                    Session["lstNhanSu"] = null;
            }
            catch(Exception ex) { Session["lstNhanSu"] = null; }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
        }

        protected void ddlToMay_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMaHang.Items.Clear();
            loadDataMaHang();
            loadListNhanSu();
            loadDataGrid();
        }

        protected void ddlMaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMaHang.SelectedValue != null && ddlMaHang.SelectedValue.ToString() != "" && ddlMaHang.SelectedValue.ToString() != "0")
            {
                long id = long.Parse(ddlMaHang.SelectedValue.ToString());
                DataTable dtmh = (DataTable)Session["DTMaHang"];
                DataRow row = dtmh.Select("STT=" + id).Single();
                if (row != null)
                {
                    Session["MaHang"] = row["MaHang"].ToString();
                }
                loadListNhanSu();
                loadDataGrid();
            }
        }

        protected void gridPhanToNK_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlHoTen = (DropDownList)e.Row.FindControl("ddlHoTen");

                    if (ddlHoTen != null && Session["lstNhanSu"] != null)
                    {
                        List<clsNhanSu> lst = new List<clsNhanSu>();
                        lst = Session["lstNhanSu"] as List<clsNhanSu>;
                        ddlHoTen.DataSource = lst;
                        ddlHoTen.DataTextField = "HoTen";
                        ddlHoTen.DataValueField = "MaNS_ID";
                        ddlHoTen.DataBind();
                        ddlHoTen.Items.Insert(0, new ListItem("", "0"));
                    }
                    
                    if (DataBinder.Eval(e.Row.DataItem, "MaNS_ID") != null && !string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "MaNS_ID").ToString()))
                        ddlHoTen.SelectedValue = DataBinder.Eval(e.Row.DataItem, "MaNS_ID").ToString();
                }
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(!checkPhanCum())
                {
                    lblMessenger.Text = "1 người không được phân 2 cụm. Vui lòng kiểm tra lại.";
                    addthismodalContact.Style["display"] = "block";
                    return;                    
                }
                DataTable dtPhanCum = new DataTable("dtPhanCum");
                dtPhanCum.Columns.Add(new DataColumn("MaHang", typeof(string)));
                dtPhanCum.Columns.Add(new DataColumn("PhongBanID", typeof(int)));
                dtPhanCum.Columns.Add(new DataColumn("ID_Cum", typeof(int)));
                dtPhanCum.Columns.Add(new DataColumn("MaNS_ID", typeof(int)));
                int id = 0;
                if (gridPhanCumTrg.Rows.Count > 0)
                {
                    DataRow dr = null;
                    foreach (GridViewRow row in gridPhanCumTrg.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            string lblID_Cum = ((Label)row.Cells[0].FindControl("lblID_Cum")).Text;
                            string sphongbanid = ddlToMay.SelectedValue.ToString();
                            string mahang = Session["MaHang"].ToString();
                            DropDownList ddlHoTen = (DropDownList)row.Cells[0].FindControl("ddlHoTen");
                            if (!string.IsNullOrEmpty(lblID_Cum) && !string.IsNullOrEmpty(mahang))
                            {
                                int idcum = int.Parse(lblID_Cum);
                                int phongbanid = int.Parse(sphongbanid);
                                if (ddlHoTen.SelectedValue != null && ddlHoTen.SelectedValue.ToString() != "" && ddlHoTen.SelectedValue.ToString() != "0")
                                {
                                    dr = dtPhanCum.NewRow();
                                    dr["MaHang"] = mahang;
                                    dr["PhongBanID"] = phongbanid;
                                    dr["ID_Cum"] = idcum;
                                    dr["MaNS_ID"] = ddlHoTen.SelectedValue.ToString();
                                    dtPhanCum.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    if (dtPhanCum != null && dtPhanCum.Rows.Count > 0)
                    {
                        var parameter = new SqlParameter("@dtPhanCum", SqlDbType.Structured);
                        parameter.Value = dtPhanCum;
                        parameter.TypeName = "dbo.udt_web_LCB_PhanCumTruong_Insert";
                        string sqlQuery = "[dbo].[pr_Web_LCB_PhanCumTruong_Insert_UDT] @dtPhanCum";
                        id = id + db.Database.ExecuteSqlCommand(sqlQuery, parameter);
                    }
                    if (id != 0)
                    {
                        loadDataGrid();
                        lblMessenger.Text = "Đã cập nhật phân cụm trưởng.";
                        addthismodalContact.Style["display"] = "block";
                    }
                    else
                    {
                        loadDataGrid();
                        lblMessenger.Text = "Lỗi cập nhật phân cụm trưởng.";
                        addthismodalContact.Style["display"] = "block";
                    }
                }
            }
            catch(Exception ex) {
                lblMessenger.Text = "Lỗi cập nhật phân cụm trưởng.";
                addthismodalContact.Style["display"] = "block";
            }
        }

        private bool checkPhanCum()
        {
            bool sus = true;
            foreach (GridViewRow row in gridPhanCumTrg.Rows)
            {
                string idcum = ((Label)row.Cells[0].FindControl("lblID_Cum")).Text;
                string mansid = ((DropDownList)row.Cells[0].FindControl("ddlHoTen")).SelectedValue;
                if(mansid != "0")
                foreach (GridViewRow row1 in gridPhanCumTrg.Rows)
                {
                    string idcum1 = ((Label)row1.Cells[0].FindControl("lblID_Cum")).Text;
                    string mansid1 = ((DropDownList)row1.Cells[0].FindControl("ddlHoTen")).SelectedValue;
                    if(mansid1 != "0")
                    {
                        if (idcum1 != idcum && mansid == mansid1)
                        {
                            sus = false;
                            break;
                        }
                    }                    
                }
            }
            return sus;
        }        
    }
}