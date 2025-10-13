using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Models;
using TNGLuong.ModelsView;

namespace TNGLuong
{
    public partial class TTKiemTraCongDiLam : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        TNG_CTLChiNhanhContact dbcn = null;
        DatabaseManager dm = null;
        StoredParameterCollection spc = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            db = new TNG_CTLDbContact();
            dm = new DatabaseManager(db);
            spc = new StoredParameterCollection();
            btnclose.ServerClick += new EventHandler(btnclose_Click);


            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    var date = DateTime.Now;
                    txtDate.Text = date.ToString("yyyy-MM-dd");
                    Session["PD_PhongBanID"] = Session["ChucVu"].ToString();
                    if (!string.IsNullOrEmpty(txtDate.Text))
                    {
                        getDataset(date.Month, date.Year);
                    }
                }
            }
            else
                Response.Redirect("Login.aspx");
        }
        protected string connectionString()
        {
            int idChiNhanh = Convert.ToInt32(Session["DonViID"].ToString());
            try
            {
                string sqlQuery = "[dbo].[pr_WEB_LCB_MayChuChiNhanh] @maChiNhanh";
                object[] sqlPr = {
            new SqlParameter("@maChiNhanh", idChiNhanh)
            };
                MayChuChiNhanh lst = new MayChuChiNhanh();

                lst = db.Database.SqlQuery<MayChuChiNhanh>(sqlQuery, sqlPr).FirstOrDefault();
                var pass = MalgoDecrypt(lst.Pass, 5);
                return "data source=" + lst.IP.Trim() + ";initial catalog=" + lst.TenDatabase.Trim() + ";persist security info=True;user id=" + lst.UserLogin.Trim() + ";password=" + pass.Trim() + ";MultipleActiveResultSets=True;App=EntityFramework;Connection Timeout=10";
            }
            catch (Exception e) { return ""; }


        }
        public static string MalgoDecrypt(string src, int v)
        {
            string str = "";
            int startIndex = 0;
            bool flag = true;
            for (startIndex = 0; startIndex < src.Length; startIndex++)
            {
                str = str + ((char)(Convert.ToUInt16(char.Parse(src.Substring(startIndex, 1))) + (flag ? v : -v)));
            }
            return str;
        }
        protected void lvUser_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                System.Web.UI.WebControls.GridView grid = e.Item.FindControl("gridCongLamViecCongNhan") as System.Web.UI.WebControls.GridView;
                Label label = e.Item.FindControl("lblMaNS_ID") as Label;
                Label lblHoTen = e.Item.FindControl("lblHoTen") as Label;

                if (label != null && !string.IsNullOrEmpty(label.Text))
                {
                    int mansid = Convert.ToInt32(label.Text);
                    lblHoTen.ForeColor = System.Drawing.Color.Red;
                    loadDataGrid(grid, mansid);

                }
            }
            catch (Exception ex) { }
        }
        protected void getDataset(int thang, int nam)
        {
            try
            {
                int phongbanid = 0;
                if (Session["ChucVu"] != null)
                    phongbanid = Convert.ToInt32(Session["ChucVu"].ToString());


                string sqlQuery = "pr_WEB_LCB_Select_CongDiLamToTruong";
                spc.Add("@PhongBanID", SqlDbType.Int, ParameterDirection.Input, phongbanid);
                spc.Add("@Ngay", SqlDbType.Date, ParameterDirection.Input, txtDate.Text);
                spc.Add("@Thang", SqlDbType.Int, ParameterDirection.Input, thang);
                spc.Add("@Nam", SqlDbType.Int, ParameterDirection.Input, nam);

                DataSet ds = new DataSet();
                ds = dm.ExecuteStoredProcedure(sqlQuery, spc);
                var s = connectionString();
                dbcn = new TNG_CTLChiNhanhContact(s);
                var today = DateTime.Today;
                int i = 0;

                object[] sqlPro =
                {
                    new SqlParameter("@PhongBanID", phongbanid),
                };
                string sqlQuery2 = "[dbo].[pr_WEB_LCB_Select_DanhSachCongNhanTheoPhongBan] @PhongBanID";

                List<ListNhanSuTheoPhongBan> lst = new List<ListNhanSuTheoPhongBan>();
                lst = db.Database.SqlQuery<ListNhanSuTheoPhongBan>(sqlQuery2, sqlPro).ToList();

                var listcn = new List<tblCTL_BangCongNgay>();
                var listCong = new List<ListCongDiLamCongNhan>();
                foreach (var item in lst)
                {
                    
                    var congCN =  dbcn.tblCTL_BangCongNgay.Where(n =>n.MaNS_ID == item.ID && DbFunctions.DiffDays(n.Ngay, DateTime.Now) == 0).FirstOrDefault();
                    if (congCN != null)
                    {
                        if(congCN.Ma_Ca != "TS" || congCN.Ma_Ca != "Ro")
                        {
                            i++;
                            ListCongDiLamCongNhan cn = new ListCongDiLamCongNhan();
                            cn.MaNS_ID = item.ID;
                            cn.MaNS = item.MaNS;
                            cn.HoTen = item.HoTen;
                            cn.TenCa = congCN.Ma_Ca;
                            cn.Ngay = congCN.Ngay;
                            cn.CS_GioVao = congCN.GioVao;
                            cn.CS_GioRa = congCN.GioRa;
                            cn.STT = i;
                            listCong.Add(cn);
                        }
                     
                    }
                    else
                    {
                        i++;
                        ListCongDiLamCongNhan cn = new ListCongDiLamCongNhan();
                        cn.MaNS_ID = item.ID;
                        cn.MaNS = item.MaNS;
                        cn.HoTen = item.HoTen;
                        cn.TenCa = "";
                        cn.Ngay =DateTime.Now;
                        cn.CS_GioVao = null;
                        cn.CS_GioRa = null;
                        cn.STT = i;
                        listCong.Add(cn);
                    }
                    
                }
                if (listCong.Count > 0)
                {
                    //pnameCNDN.Visible = true;
                    gridChamCongHomNay.DataSource = listCong;
                    gridChamCongHomNay.DataBind();

                }
                if (ds != null && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    Session["DataUserCN"] = ds.Tables[2];
                }
                else
                {
                    Session["DataUserCN"] = null;
                }
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    //pnameCNDN.Visible = true;
                    lvUser.DataSource = ds.Tables[0];
                    lvUser.DataBind();

                }

                //else
                //pnameCNDN.Visible = false;
            }
            catch (Exception ex) { }
        }
        protected void loadDataGrid(GridView gridCongLamViecCongNhan, int mansid)
        {
            try
            {
                if (Session["DataUserCN"] != null)
                {
                    DataTable dt = Session["DataUserCN"] as DataTable;
                    dt = dt.Select("MaNS_ID = " + mansid).CopyToDataTable();
                    dt.DefaultView.Sort = "Ngay ASC";
                    gridCongLamViecCongNhan.DataSource = dt;
                    gridCongLamViecCongNhan.DataBind();
                    decimal total = 0;

                    total = dt.Rows.Count;
                    foreach (DataRow row in dt.Rows)
                    {

                        object csVao = row[0];
                        object csRa = row[1];
                        if (csVao == DBNull.Value && csRa == DBNull.Value)
                        {
                            total = total - 1;
                        }

                    }

                    gridCongLamViecCongNhan.FooterRow.Cells[0].Text = "Tổng: ";
                    gridCongLamViecCongNhan.FooterRow.Cells[0].Font.Bold = true;
                    gridCongLamViecCongNhan.FooterRow.Cells[1].Font.Bold = true;
                    gridCongLamViecCongNhan.FooterRow.Cells[1].Text = total.ToString();
                    gridCongLamViecCongNhan.FooterRow.Cells[1].Style["text-align"] = "left";
                    gridCongLamViecCongNhan.FooterRow.Cells[1].Style["padding-left"] = "12px";
                    gridCongLamViecCongNhan.FooterRow.Cells[1].ColumnSpan = 2;
                    gridCongLamViecCongNhan.FooterRow.Cells[2].Visible = false;
                    gridCongLamViecCongNhan.FooterRow.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    List<ListCongDiLamCongNhan> lst = new List<ListCongDiLamCongNhan>();
                    DataTable dt = ultils.CreateDataTable<ListCongDiLamCongNhan>(lst);
                    dt.Rows.Add(dt.NewRow());
                    gridCongLamViecCongNhan.DataSource = dt;
                    gridCongLamViecCongNhan.DataBind();
                    gridCongLamViecCongNhan.Rows[0].Cells.Clear();
                    gridCongLamViecCongNhan.Rows[0].Cells.Add(new TableCell());
                    gridCongLamViecCongNhan.Rows[0].Cells[0].ColumnSpan = 4;
                    gridCongLamViecCongNhan.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridCongLamViecCongNhan.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;


                }
            }
            catch (Exception ex)
            {
                List<ListCongDiLamCongNhan> lst = new List<ListCongDiLamCongNhan>();
                DataTable dt = ultils.CreateDataTable<ListCongDiLamCongNhan>(lst);
                dt.Rows.Add(dt.NewRow());
                gridCongLamViecCongNhan.DataSource = dt;
                gridCongLamViecCongNhan.DataBind();
                gridCongLamViecCongNhan.Rows[0].Cells.Clear();
                gridCongLamViecCongNhan.Rows[0].Cells.Add(new TableCell());
                gridCongLamViecCongNhan.Rows[0].Cells[0].ColumnSpan = 4;
                gridCongLamViecCongNhan.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                gridCongLamViecCongNhan.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            int thang = 0;
            int nam = 0;
            var dte = Convert.ToDateTime(txtDate.Text);

            thang = dte.Month;
            nam = dte.Year;
            if (dte.Date != DateTime.Now.Date)
            {
                btnCapNhatLyDo.Visible = false;
            }
            else
            {
                btnCapNhatLyDo.Visible = true;
            }
            getDataset(thang, nam);

        }

        protected void gridChamCongHomNay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlTenCa = (DropDownList)e.Row.FindControl("ddlTenCa");
                    string sqlQuery = "[dbo].[pr_WEB_LCB_Select_LyDoNghi]";
                    object[] sqlPr = { };
                    List<ListDM_Ca> lst = new List<ListDM_Ca>();

                    lst = db.Database.SqlQuery<ListDM_Ca>(sqlQuery, sqlPr).ToList();

                    if (ddlTenCa != null)
                    {
                        ddlTenCa.DataSource = lst;
                        ddlTenCa.DataTextField = "Ten_Ca";
                        ddlTenCa.DataValueField = "Ma_Ca";
                        ddlTenCa.DataBind();
                        ddlTenCa.Items.Insert(0, new ListItem("", "0"));
                    }

                    if (DataBinder.Eval(e.Row.DataItem, "Ma_Ca") != null && !string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "Ma_Ca").ToString()))
                        ddlTenCa.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Ma_Ca").ToString();
                }
            }
            catch (Exception ex) { }
        }

        protected void btnCapNhatLyDo_Click(object sender, EventArgs e)
        {
            var dte = Convert.ToDateTime(txtDate.Text);
            int thang = dte.Month;
            int nam = dte.Year;
            try
            {

                DataTable dtLyDo = new DataTable("dtLyDo");
                dtLyDo.Columns.Add(new DataColumn("MaNS_ID", typeof(int)));
                dtLyDo.Columns.Add(new DataColumn("Ngay", typeof(string)));
                dtLyDo.Columns.Add(new DataColumn("CS_Ma_Ca", typeof(string)));
                dtLyDo.Columns.Add(new DataColumn("PhongBanID", typeof(int)));
                dtLyDo.Columns.Add(new DataColumn("DonViID", typeof(int)));
                int id = 0;
                if (gridChamCongHomNay.Rows.Count > 0)
                {
                    DataRow dr = null;
                    foreach (GridViewRow row in gridChamCongHomNay.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            string lblMaNS_ID = ((Label)row.Cells[0].FindControl("lblMaNS")).Text;
                            string lblDate = txtDate.Text;
                            int phongbanid = 0;
                            int donviid = 0;
                            if (Session["ChucVu"] != null)
                                phongbanid = Convert.ToInt32(Session["ChucVu"].ToString());
                            if (Session["DonViID"] != null)
                                donviid = Convert.ToInt32(Session["DonViID"].ToString());
                            DropDownList ddlTenCa = (DropDownList)row.Cells[0].FindControl("ddlTenCa");
                            if (!string.IsNullOrEmpty(lblMaNS_ID))
                            {
                                int maNSId = int.Parse(lblMaNS_ID);

                                if (ddlTenCa.SelectedValue != null && ddlTenCa.SelectedValue.ToString() != "" && ddlTenCa.SelectedValue.ToString() != "0")
                                {
                                    dr = dtLyDo.NewRow();
                                    dr["MaNS_ID"] = maNSId;
                                    dr["Ngay"] = lblDate;
                                    dr["CS_Ma_Ca"] = ddlTenCa.SelectedValue.ToString();
                                    dr["PhongBanID"] = phongbanid;
                                    dr["DonViID"] = donviid;
                                    dtLyDo.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    if (dtLyDo != null && dtLyDo.Rows.Count > 0)
                    {
                        var s = connectionString();
                        dbcn = new TNG_CTLChiNhanhContact(s);
                        var parameter = new SqlParameter("@dtLyDo", SqlDbType.Structured);
                        parameter.Value = dtLyDo;
                        parameter.TypeName = "dbo.udt_BangCongNgay_CT_LyDoNghi";
                        string sqlQuery = "[dbo].[pr_Web_LCB_CongNgay_LyDoNghi_UDT] @dtLyDo";
                        id = id + dbcn.Database.ExecuteSqlCommand(sqlQuery, parameter);
                    }
                    if (id != 0)
                    {
                        lblMessenger.Text = "Đã cập nhật";
                        addthismodalContact.Style["display"] = "block";
                        divThongBao.Style["display"] = "block";
                        getDataset(thang, nam);
                    }
                    else
                    {

                        lblMessenger.Text = "Đã cập nhật";
                        addthismodalContact.Style["display"] = "block";
                        divThongBao.Style["display"] = "block";
                        getDataset(thang, nam);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessenger.Text = "Lỗi cập nhật.";
                addthismodalContact.Style["display"] = "block";
                divThongBao.Style["display"] = "block";
            }
        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
            divThongBao.Style["display"] = "none";
        }

    }
}