using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Models;
using TNGLuong.ModelsView;
using System.Data.Entity;
using System.Web.DynamicData;
using System.Drawing;

namespace TNGLuong
{
    public partial class MTKN_BaoCao_ToTruong : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        TNGLuongDbContact dblog = null;
        int iID_LanDanhGia, iID_NhanSu;
        DropDownList dmXepHang;
        bool isAllowEdit = false;

        List<MTKN_XepHang_NhanSu_New> lstXepHangBeforeChanged;

        protected struct TenVaIDNhanSu {
            public int MaNS_ID { get; set; }
            public string HoTen { get; set; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSearchTenCN.ServerClick += new EventHandler(btnSearchTenCN_Click);
            //btnSearchTenCD.ServerClick += new EventHandler(btnSearchTenCD_Click);
            //closeNhanSuCongDoan.ServerClick += new EventHandler(closeNhanSuCongDoan_Click);
            if (Session["ChucVu"] == null)
                Response.Redirect("/");
            db = new TNG_CTLDbContact();
            if (Session["username"] != null)
            {
                iID_NhanSu = Int32.Parse(Session["userid"].ToString());
                Session["DTLanDanhGia"] = null;
                int phongid_ns = Convert.ToInt32(Session["ChucVu"].ToString());//1559
                if (Session["TenDonVi"] != null)
                    txtTenDonVi.Text = Session["TenDonVi"].ToString();
                View_ToMay tm = new View_ToMay();
                tm = db.View_ToMay.Where(x => x.PhongBanID == phongid_ns).SingleOrDefault();
                if (tm != null)
                    Session["DonViID_CN"] = tm.DonViID;

                //gridLoaiMay.DataSource = null;

                LoadDMXepHang(); // load danh mục xếp hạng.

                loadDataToMay();
                if (!IsPostBack)
                {
                    loadDataLanDanhGia();// load dropdownlist lần đánh giá
                } 
                Load_dtNhanSu(phongid_ns); // Load thông tin nhân sự ở col 1
                getDataset(); // load các cột loại máy và 3 row mục tiêu, thực hiện, đào tạo.

                if (!IsPostBack)
                {

                    lockControls(true);
                    showHiddenCols(false); // ẩn các cột không có mục tiêu khi ở chế độ view.
                }

                //}
                addDropdownlist();// thêm dropdownlist vào từng cell.
                iID_LanDanhGia = Convert.ToInt32(ddlLanDanhGia.SelectedValue.ToString());
                LoadData_XepHang(phongid_ns, iID_LanDanhGia); //set giá trị cho các dropdownlist cell.
                TinhLevelC(); // tính và hiển thị row level C cho mỗi loại máy.
                checkAllowEdit();
            }

            else
                Response.Redirect("Login.aspx");
        }

        protected bool checkAllowEdit()
        {
            string sqlQuery = "SELECT COUNT(*) FROM MTKN_ToTruong_XacNhan WHERE ID_NhanSu = @ID_NhanSu AND ID_LanDanhGia = @ID_LanDanhGia";
            DbContext dbc = new DbContext("name=TNG_QLKT");
            //string te = dbc.Database.SqlQuery<string>(sqlQuery, sqlPro2).ToString();
            int iRowCount = dbc.Database.SqlQuery<int>(sqlQuery,
            new SqlParameter("@ID_NhanSu", iID_NhanSu),
            new SqlParameter("@ID_LanDanhGia", iID_LanDanhGia))
            .FirstOrDefault();
            dbc.Dispose();
            if(iRowCount > 0)
            {
                btnUpdate.Enabled = false;
                btnSave.Enabled = false;
                btnHuy.Enabled = false;
                btnXacNhan.Enabled = false;
                return false;
            }
            return true;
        }

        private void showHiddenCols(bool status)
        {
            int rowThucHien = 1;
            int rowCanDaoTao = 2;
            for (int j = 2; j < gridLoaiMay.Columns.Count; j++)
            {
                if (gridLoaiMay.Rows[rowThucHien].Cells[j].Text.Trim() == "0" && gridLoaiMay.Rows[rowCanDaoTao].Cells[j].Text.Trim() == "0")
                {
                    gridLoaiMay.Columns[j].Visible = status;
                }
            }
        }

        private void LoadDMXepHang()
        {
            dmXepHang = new DropDownList();
            object[] sqlPro2 =
            {
                new SqlParameter("@iErrorCode", DBNull.Value),
            };

            string sqlQuery = "[dbo].[pr_MTKN_DM_XepHang_SelectAll] @iErrorCode";
            DbContext dbc = new DbContext("name=TNG_QLKT");
            List<MTKN_DM_XepHang> lst2 = dbc.Database.SqlQuery<MTKN_DM_XepHang>(sqlQuery, sqlPro2).ToList();
            lst2.Add(new MTKN_DM_XepHang { ID_XepHang = 6, XepHang = "E/D" });
            lst2.Add(new MTKN_DM_XepHang { ID_XepHang = 7, XepHang = "D/C" });
            lst2.Add(new MTKN_DM_XepHang { ID_XepHang = 8, XepHang = "C/B" });
            lst2.Add(new MTKN_DM_XepHang { ID_XepHang = 9, XepHang = "B/A" });
            lst2.Add(new MTKN_DM_XepHang { ID_XepHang = 0, XepHang = "" });
            dbc.Dispose();
            dmXepHang.DataSource = lst2;
            dmXepHang.DataValueField = "ID_XepHang";
            dmXepHang.DataTextField = "XepHang";
            dmXepHang.DataBind();
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
        protected void loadDataLanDanhGia()
        {
            try
            {
                object[] sqlPr =
                {
                    new SqlParameter("@iErrorCode", DBNull.Value),

                };
                string sqlQuery = "[dbo].[pr_MTKN_SelectAll_LanDanhGia] @iErrorCode";

                List<MTKN_DM_LanDanhGia> lst = new List<MTKN_DM_LanDanhGia>();
                lst = db.Database.SqlQuery<MTKN_DM_LanDanhGia>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    //DataTable dt = ultils.CreateDataTable<MTKN_DM_LanDanhGia>(lst);
                    //Session["DTLanDanhGia"] = dt;

                    ddlLanDanhGia.DataSource = lst;
                    ddlLanDanhGia.DataBind();
                    
                    if (Session["iID_LanDanhGia"] != null)
                    {
                        ddlLanDanhGia.SelectedValue = Session["iID_LanDanhGia"].ToString();
                        iID_LanDanhGia = Int32.Parse(Session["iID_LanDanhGia"].ToString());
                    }

                    //if (ddlLanDanhGia.SelectedValue != null && ddlLanDanhGia.SelectedValue.ToString() != "" && ddlLanDanhGia.SelectedValue.ToString() != "0")
                    //{
                    //    long id = long.Parse(ddlLanDanhGia.SelectedValue.ToString());
                    //    DataRow row = dt.Select("STT=" + id).Single();
                    //}
                }
            }
            catch (Exception ex) { }
        }

        protected void getDataset()
        {
            //try
            //{
            int phongbanid = 0;
            int tomay = 0;
            int lanDanhGia = 0;

            if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                tomay = Convert.ToInt32(ddlToMay.SelectedValue.ToString());

            if (Session["ChucVu"] != null)
                phongbanid = Convert.ToInt32(Session["ChucVu"].ToString());

            if (ddlLanDanhGia.SelectedValue != null && ddlLanDanhGia.SelectedValue.ToString() != "")
                lanDanhGia = Convert.ToInt32(ddlLanDanhGia.SelectedValue.ToString());

            object[] sqlPro =
            {
                    new SqlParameter("@PhongBanID", phongbanid),
                    new SqlParameter("@ID_LanDanhGia", lanDanhGia),
                    new SqlParameter("@iErrorCode", DBNull.Value),
            };

            string sqlQuery2 = "[dbo].[pr_MTKN_MucTieu_LoaiMay_PhongBan_Select_wDonVi_PhongBan] @PhongBanID,@ID_LanDanhGia,@iErrorCode";

            List<MTKN_MucTieu_XepHang> lst = new List<MTKN_MucTieu_XepHang>();
            lst = db.Database.SqlQuery<MTKN_MucTieu_XepHang>(sqlQuery2, sqlPro).ToList();

            //lst = lst.Where(data => data.ThucHien > 0).ToList();
            if (lst != null && lst.Count > 0)
            {
                txtSoNS.Text = lst.FirstOrDefault().SoNS.ToString();

            }
            else
            {
                //Session["DataUser"] = null;
            }
            var lst2 = getLoaiMay_XepHang();
            if (!IsPostBack)
            {
                foreach (var data in lst2)
                {
                    TemplateField newColumn = new TemplateField();
                    newColumn.HeaderText = data.TenLoaiMay;   // Set the header text
                    newColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    gridLoaiMay.Columns.Add(newColumn);
                }
            }

            //gridLoaiMay.DataSource = lst2;
            gridLoaiMay.DataBind();
            int Index = 2;
            foreach (var data in lst2)
            {
                TextBox txtMucTieu = new TextBox();
                txtMucTieu.Text = data.MucTieu_LoaiMay.ToString();
                txtMucTieu.ID = "txtMucTieu_" + data.ID_LoaiMay.ToString();
                txtMucTieu.Style["padding-left"] = "5px";
                
                gridLoaiMay.Rows[0].Cells[Index].Controls.Clear();
                gridLoaiMay.Rows[0].Cells[Index].Controls.Add(txtMucTieu);
                gridLoaiMay.Rows[0].Cells[Index].BackColor = Color.FromArgb(19, 132, 189);
                gridLoaiMay.Rows[0].Cells[Index].ForeColor = Color.White;
                gridLoaiMay.Rows[1].Cells[Index].Text = data.ThucHien_LoaiMay.ToString();
                gridLoaiMay.Rows[1].Cells[Index].BackColor = Color.FromArgb(19, 132, 189);
                gridLoaiMay.Rows[1].Cells[Index].ForeColor = Color.White;
                gridLoaiMay.Rows[2].Cells[Index].Text = data.DaoTaoThem.ToString();
                gridLoaiMay.Rows[2].Cells[Index].BackColor = Color.FromArgb(19, 132, 189);
                gridLoaiMay.Rows[2].Cells[Index].ForeColor = Color.White;
                gridLoaiMay.Rows[3].Cells[Index].BackColor = Color.FromArgb(19, 132, 189);
                gridLoaiMay.Rows[3].Cells[Index].ForeColor = Color.White;
                Index++;
            }

            gridLoaiMay.Rows[0].Cells[1].BackColor = Color.FromArgb(100, 132, 189);
            gridLoaiMay.Rows[1].Cells[1].BackColor = Color.FromArgb(100, 132, 189);
            gridLoaiMay.Rows[2].Cells[1].BackColor = Color.FromArgb(100, 132, 189);
            gridLoaiMay.Rows[3].Cells[1].BackColor = Color.FromArgb(100, 132, 189);
            //gridLoaiMay.DataBind();
            //}
            //catch (Exception ex) { }
        }
        private List<MTKN_NhanSu_LoaiMay_XepHang> getLoaiMay_XepHang(){

            int phongbanid = 0;
            int tomay = 0;
            int lanDanhGia = 0;

            if (ddlToMay.SelectedValue != null && ddlToMay.SelectedValue.ToString() != "")
                tomay = Convert.ToInt32(ddlToMay.SelectedValue.ToString());

            if (Session["ChucVu"] != null)
                phongbanid = Convert.ToInt32(Session["ChucVu"].ToString());

            if (ddlLanDanhGia.SelectedValue != null && ddlLanDanhGia.SelectedValue.ToString() != "")
                lanDanhGia = Convert.ToInt32(ddlLanDanhGia.SelectedValue.ToString());

            object[] sqlPro2 =
                {
                    new SqlParameter("@ID_PhongBan", phongbanid),
                    new SqlParameter("@ID_LanDanhGia", lanDanhGia),
                    new SqlParameter("@iErrorCode", DBNull.Value),
                };

            string sqlQuery = "[dbo].[pr_MTKN_NhanSu_LoaiMay_XepHang_Select_LoaiMay_New] @ID_PhongBan, @ID_LanDanhGia, @iErrorCode";
            DbContext dbc = new DbContext("name=TNG_QLKT");
            List<MTKN_NhanSu_LoaiMay_XepHang> lst2 = new List<MTKN_NhanSu_LoaiMay_XepHang>();
            lst2 = dbc.Database.SqlQuery<MTKN_NhanSu_LoaiMay_XepHang>(sqlQuery, sqlPro2).ToList();
            dbc.Dispose();
            return lst2;
        }

        protected void getDatasetChiTietLoaiMay(int lanDanhGia, int idLoaiMay, int phongBanID)
        {
            try
            {

                object[] sqlPr =
            {
                    new SqlParameter("@PhongBanID", phongBanID),
                    new SqlParameter("@ID_LoaiMay", idLoaiMay),
                    new SqlParameter("@ID_LanDanhGia", lanDanhGia),
                    new SqlParameter("@iErrorCode", DBNull.Value),
                };

                string sqlQuery = "[dbo].[pr_MTKN_MucTieu_LoaiMay_PhongBan_DanhSachThucHien] @PhongBanID,@ID_LoaiMay,@ID_LanDanhGia,@iErrorCode";
                List<MTKN_MucTieu_LoaiMay> lst2 = new List<MTKN_MucTieu_LoaiMay>();
                lst2 = db.Database.SqlQuery<MTKN_MucTieu_LoaiMay>(sqlQuery, sqlPr).ToList();

                if (lst2.Count > 0)
                {
                    //pnameCNDN.Visible = true;

                }
            }
            catch (Exception ex) { }
        }

        protected void gridDanhSachLoaiMay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlXepHang = (DropDownList)e.Row.FindControl("ddlXepHang");
                    DropDownList ddlMucTieu = (DropDownList)e.Row.FindControl("ddlMucTieu");

                    object[] sqlPr = { new SqlParameter("@iErrorCode", DBNull.Value) };
                    string sqlQuery = "[dbo].[pr_MTKN_Select_DM_XepHang] @iErrorCode";
                    List<MTKN_DM_XepHang> lst = new List<MTKN_DM_XepHang>();

                    lst = db.Database.SqlQuery<MTKN_DM_XepHang>(sqlQuery, sqlPr).ToList();

                    if (ddlXepHang != null)
                    {
                        ddlXepHang.DataSource = lst;
                        ddlXepHang.DataTextField = "XepHang";
                        ddlXepHang.DataValueField = "ID_XepHang";
                        ddlXepHang.DataBind();
                        ddlXepHang.Items.Insert(0, new ListItem("", "0"));
                    }
                    if (ddlMucTieu != null)
                    {
                        ddlMucTieu.DataSource = lst;
                        ddlMucTieu.DataTextField = "XepHang";
                        ddlMucTieu.DataValueField = "ID_XepHang";
                        ddlMucTieu.DataBind();
                        ddlMucTieu.Items.Insert(0, new ListItem("", "0"));
                    }
                    if (DataBinder.Eval(e.Row.DataItem, "ID_XepHang") != null && !string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ID_XepHang").ToString()))
                        ddlXepHang.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ID_XepHang").ToString();

                    if (DataBinder.Eval(e.Row.DataItem, "ID_XepHang_MucTieu") != null && !string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ID_XepHang_MucTieu").ToString()))

                        ddlMucTieu.SelectedValue = DataBinder.Eval(e.Row.DataItem, "ID_XepHang_MucTieu").ToString();
                    
                }
            }
            catch (Exception ex) { }
        }

        protected void btnHuy_Click(object sender, EventArgs e) {

            Page.Response.Redirect(Page.Request.Url.ToString(), true);

        }
        protected void btnXacNhan_Click(object sender, EventArgs e) {
            try
            {
                iID_LanDanhGia = Int32.Parse(ddlLanDanhGia.SelectedValue.ToString());
                string sqlQuery = "INSERT INTO MTKN_ToTruong_XacNhan VALUES (@ID_NhanSu, @ID_LanDanhGia, GETDATE())";
                DbContext dbc = new DbContext("name=TNG_QLKT");
                dbc.Database.ExecuteSqlCommand(sqlQuery,
                new SqlParameter("@ID_NhanSu", iID_NhanSu),
                new SqlParameter("@ID_LanDanhGia", iID_LanDanhGia));
                dbc.Dispose();
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch { }
        }
        protected void ddlLanDanhGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlLanDanhGia.SelectedValue != null && ddlLanDanhGia.SelectedValue.ToString() != "" && ddlLanDanhGia.SelectedValue.ToString() != "0")
            //{
            //    //getDataset();
            //}
            Session["iID_LanDanhGia"] = iID_LanDanhGia;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        protected void btnSearchTenCN_Click(object sender, EventArgs e)
        {

            List<DanhSachNhanSuGiaoKeHoach> lsts = new List<DanhSachNhanSuGiaoKeHoach>();
            if (Session["DataNhanSu"] != null)
            {
                lsts = Session["DataNhanSu"] as List<DanhSachNhanSuGiaoKeHoach>;
            }

            if (lsts != null && lsts.Count > 0)
            {

                if (!(string.IsNullOrEmpty(txtsearchTenCN.Value)))
                {
                    var ns = lsts.Where(m => m.HoTen.ToLower().Contains(txtsearchTenCN.Value.ToLower()) || m.MaNS.ToLower().Contains(txtsearchTenCN.Value.ToLower())).ToList();
                    //lvUser.DataSource = ns;
                    //lvUser.DataBind();
                }
                else
                {
                    //lvUser.DataSource = lsts;
                    //lvUser.DataBind();
                }
            }
        }
        protected void btnSaveMucTieu_Click(object sender, EventArgs e)
        {
            List<MTKN_XepHang_NhanSu_New> lstPendingChange = new List<MTKN_XepHang_NhanSu_New>();
            var lstNhanSu_LoaiMay = getLoaiMay_XepHang();
            int soNS = Convert.ToInt32(txtSoNS.Text);
            iID_LanDanhGia = Int32.Parse(ddlLanDanhGia.SelectedValue.ToString());

            for (int i = 4; i < gridLoaiMay.Rows.Count; i++)
                for (int j = 2; j < gridLoaiMay.Columns.Count; j++)
                {
                    int idNS = Int32.Parse(gridLoaiMay.Rows[i].Cells[0].Text);
                    string tenLoaiMay = HttpUtility.HtmlDecode(gridLoaiMay.HeaderRow.Cells[j].Text);
                    DropDownList drl = (DropDownList)gridLoaiMay.Rows[i].Cells[j].FindControl("ddl_" + i.ToString() + "_" + j.ToString());
                    int val = Int32.Parse(drl.SelectedValue);
                    var obj = lstXepHangBeforeChanged.Where(dt => dt.ID_NhanSu == idNS && dt.TenLoaiMay == tenLoaiMay).FirstOrDefault();
                    int ID_LoaiMay = lstNhanSu_LoaiMay.Where(dt => dt.TenLoaiMay == tenLoaiMay).FirstOrDefault().ID_LoaiMay;
                    //----------------------------------------- begin update tyledat ------------------------------------
                    if (i == 4)
                    {
                        TextBox txt = (TextBox)gridLoaiMay.Rows[0].Cells[j].FindControl("txtMucTieu_" + ID_LoaiMay.ToString());
                        string sqlQuery = "pr_MTKN_MucTieu_PhongBan_Update_TyLeDat 	@iPhongBanID ,@iID_LoaiMay ,@iID_LanDanhGia ,@iTyLeDat ,@sTenDangNhap";
                        //int t1 = Convert.ToInt32(Session["ChucVu"].ToString());
                        //int t2 = iID_LanDanhGia;
                        //string t3 = Session["username"].ToString();
                        //int t4 = ID_LoaiMay;
                        //int t6 = Int32.Parse(txt.Text.ToString());
                        int iTyLeDat = (int)((Convert.ToDecimal(Int32.Parse(txt.Text)) / soNS) * 100);

                        DbContext dbc = new TNG_CTLDbContact();
                        dbc.Database.ExecuteSqlCommand(sqlQuery,
                        new SqlParameter("@iPhongBanID", Convert.ToInt32(Session["ChucVu"].ToString())),
                        new SqlParameter("@iID_LanDanhGia", iID_LanDanhGia),
                        new SqlParameter("@sTenDangNhap", Session["username"].ToString()),
                        new SqlParameter("@iID_LoaiMay", ID_LoaiMay.ToString()),
                        new SqlParameter("@iTyLeDat", iTyLeDat));
                        dbc.Dispose();
                    }
                    //----------------------------------------- end update tyledat ------------------------------------


                    if (obj == null && drl.SelectedValue == "0") continue;


                    if (obj == null || obj.ID_XepHang != val) 
                    {
                        lstPendingChange.Add(new MTKN_XepHang_NhanSu_New() { 
                            ID_NhanSu = idNS, ID_LoaiMay = ID_LoaiMay, ID_XepHang = val,
                            TenLoaiMay = tenLoaiMay
                        });
                        int a = ID_LoaiMay;
                        a = idNS;
                        a = val;
                        a = iID_LanDanhGia;
                        updateXepHang(ID_LoaiMay, idNS, val, iID_LanDanhGia);
                    }
                }

            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void updateXepHang(int ID_LoaiMay, int ID_NhanSu, int ID_XepHang, int ID_LanDanhGia)
        {
            int iID_XepHang;
            int iID_XepHang_MucTieu;
            if (ID_XepHang == 6)
            {
                iID_XepHang = 1;
                iID_XepHang_MucTieu = 2;

            }
            else if (ID_XepHang == 7)
            {
                iID_XepHang = 2;
                iID_XepHang_MucTieu = 3;
            }
            else if (ID_XepHang == 8)
            {
                iID_XepHang = 3;
                iID_XepHang_MucTieu = 4;
            }
            else if (ID_XepHang == 9)
            {
                iID_XepHang = 4;
                iID_XepHang_MucTieu = 5;
            }
            else
            {
                iID_XepHang = ID_XepHang;
                iID_XepHang_MucTieu = 0;
            }

            object[] sqlPr =
            {
                 new SqlParameter("@ID_NhanSu",ID_NhanSu),
                 new SqlParameter("@ID_LoaiMay",ID_LoaiMay),
                 new SqlParameter("@ID_XepHang",iID_XepHang),
                 new SqlParameter("@ID_LanDanhGia",ID_LanDanhGia),
                 new SqlParameter("@ID_XepHang_MucTieu",iID_XepHang_MucTieu),
                 new SqlParameter("@iErrorCode", DBNull.Value)
            };

            string sqlQuery = "[dbo].[pr_MTKN_NhanSu_LoaiMay_XepHang_CongNhan_Update] @ID_NhanSu,@ID_LanDanhGia, @ID_LoaiMay, @ID_XepHang,  @ID_XepHang_MucTieu, @iErrorCode";
            DbContext dbc = new DbContext("name=TNG_QLKT");
            List<TenVaIDNhanSu> lst2 = new List<TenVaIDNhanSu>();
            dbc.Database.ExecuteSqlCommand(sqlQuery, sqlPr);
            dbc.Dispose();

        }

        protected void gridLoaiMay_ItemCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void txtSearchTenMay_Click(object sender, EventArgs e)
        {
            List<ListCongDoan_KeHoach_CongNhan> lsts = new List<ListCongDoan_KeHoach_CongNhan>();
            if (Session["DataCongDoan"] != null)
            {
                lsts = Session["DataCongDoan"] as List<ListCongDoan_KeHoach_CongNhan>;
            }


        }

        protected List<TenVaIDNhanSu> Select_NhanSu_wPhongBanID(int PhongBanID)
        {
            try
            {
                object[] sqlPr =
                {
                    new SqlParameter("@PhongBanID", PhongBanID),
                    new SqlParameter("@iErrorCode", DBNull.Value)
                };

                string sqlQuery = "[dbo].[pr_MTKN_SelectAll_NhanSu_wPhongBanID] @PhongBanID, @iErrorCode";
                DbContext dbc = new DbContext("name=TNG_QLKT");
                List<TenVaIDNhanSu> lst2 = new List<TenVaIDNhanSu>();
                lst2 = dbc.Database.SqlQuery<TenVaIDNhanSu>(sqlQuery, sqlPr).ToList();
                dbc.Dispose();
                return lst2;
            }
            catch (Exception ex) {
                return null;
            }

        }

        protected void gridLoaiMay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Load_dtNhanSu(int PhongBanID)
        {
            List<TenVaIDNhanSu> lstNhanSu = Select_NhanSu_wPhongBanID(PhongBanID);
            //foreach(TenVaIDNhanSu ten in lstNhanSu)
            //{
            //    // Create a new BoundField
            //    BoundField newColumn = new BoundField();
            //    //newColumn.SetAttribute()
            //    newColumn.HeaderText = ten.HoTen + "|"+ ten.MaNS_ID;   // Set the header text
            //    newColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            //    // Add the new column to the GridView
            //    gridLoaiMay.Columns.Add(newColumn);
            //    // Call DataBind to refresh the GridView
            //}

            lstNhanSu.Insert(0, new TenVaIDNhanSu() { MaNS_ID = -4, HoTen = "Tổng level C" });
            lstNhanSu.Insert(0, new TenVaIDNhanSu() { MaNS_ID = -3, HoTen = "Cần đào tạo thêm" });
            lstNhanSu.Insert(0, new TenVaIDNhanSu() { MaNS_ID = -2, HoTen = "Thực hiện" });
            lstNhanSu.Insert(0, new TenVaIDNhanSu() { MaNS_ID = -1, HoTen = "Mục tiêu" });
            gridLoaiMay.DataSource = lstNhanSu;
            gridLoaiMay.DataBind();
        }
        protected void LoadData_XepHang(int PhongBanID, int LanDanhGia)
        {
            var dt = Select_NhanSu_wPhongBanID_LanDanhGia(PhongBanID, LanDanhGia);
            lstXepHangBeforeChanged = dt;
            var lstLoaiMay = new List<string> { };
            int index = 0;

            for (int i = 4; i < gridLoaiMay.Rows.Count; i++)
                for (int j = 2; j < gridLoaiMay.Columns.Count; j++)
                {
                    string mans = gridLoaiMay.Rows[i].Cells[0].Text;
                    string tenLoaiMay = HttpUtility.HtmlDecode(gridLoaiMay.HeaderRow.Cells[j].Text);
                    var danhGia_LoaiMay = dt.Where(dg => dg.TenLoaiMay.Trim() == tenLoaiMay.Trim() && dg.ID_NhanSu.ToString() == mans).FirstOrDefault();
                    
                    string controlName = "ddl_" + i.ToString() + "_" + j.ToString();
                    DropDownList drl1 = ((DropDownList)gridLoaiMay.Rows[i].Cells[j].FindControl(controlName));
                    if (danhGia_LoaiMay == null || string.IsNullOrEmpty(danhGia_LoaiMay.XepHang))
                    {
                        drl1.SelectedIndex = 9;
                        continue;
                    }   
                    drl1.SelectedValue = danhGia_LoaiMay.ID_XepHang.ToString();
                    string kyHieuXepHang = danhGia_LoaiMay.XepHang;
                    string val = kyHieuXepHang.Trim().Substring(0, 1);
                    loadColor(val, i, j);
                }
        }

        protected void addDropdownlist()
        {
            for (int i = 4; i < gridLoaiMay.Rows.Count; i++)
                for (int j = 2; j < gridLoaiMay.Columns.Count; j++)
                {
                    DropDownList drl = new DropDownList();
                    drl.ID = "ddl_" + i + "_" + j;
                    drl.DataValueField = dmXepHang.DataValueField;
                    drl.DataTextField = dmXepHang.DataTextField;
                    drl.SelectedIndex = dmXepHang.SelectedIndex; // Chọn giá trị mặc định
                    //drl.AutoPostBack = true;
                    drl.SelectedIndexChanged += drlXepHang_SelectedIndexChanged;
                    drl.DataSource = dmXepHang.DataSource; // Sử dụng cùng một nguồn dữ liệu
                    drl.DataBind();
                    gridLoaiMay.Rows[i].Cells[j].Controls.Clear();
                    gridLoaiMay.Rows[i].Cells[j].Controls.Add(drl);
                }
        }

        protected void gridLoaiMay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    for (int j = 2; j < gridLoaiMay.Columns.Count; j++)
            //    {
            //        DropDownList drl = new DropDownList();
            //        drl.ID = "ddl_" + e.Row.RowIndex + "_" + j;
            //        drl.DataValueField = dmXepHang.DataValueField;
            //        drl.DataTextField = dmXepHang.DataTextField;
            //        drl.SelectedIndex = dmXepHang.SelectedIndex; // Chọn giá trị mặc định
            //        drl.SelectedIndexChanged += drlXepHang_SelectedIndexChanged;
            //        drl.DataSource = dmXepHang.DataSource; // Sử dụng cùng một nguồn dữ liệu
            //        drl.DataBind();
            //        e.Row.Cells[j].Controls.Add(drl);
            //    }
            //}
        }
        
        private void TinhLevelC()
        {
            int rowIndex = 3;
            for (int j = 2; j < gridLoaiMay.Columns.Count; j++) {
                int count = 0;
                for(int m = 4; m< gridLoaiMay.Rows.Count; m++)
                {
                    DropDownList drl = (DropDownList)gridLoaiMay.Rows[m].Cells[j].FindControl("ddl_"+ m.ToString() + "_" + j.ToString());
                    string value = drl.SelectedValue;
                    if (value == "3" || value == "4" || value == "5" || value == "8" || value == "9")
                    {
                        count++;
                    }
                }
                gridLoaiMay.Rows[rowIndex].Cells[j].Text = count.ToString();
                gridLoaiMay.Rows[rowIndex].Cells[j].BackColor = Color.FromArgb(100, 132, 189);


            }


        }

        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            gridLoaiMay.Enabled = true;
            return;
        }

        protected void gridLoaiMay_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            
        }

        protected List<MTKN_XepHang_NhanSu_New> Select_NhanSu_wPhongBanID_LanDanhGia(int phongBanID, int lanDanhGia)
        {
            try
            {
                object[] sqlPr =
                {
                    new SqlParameter("@ID_PhongBan", phongBanID),
                    new SqlParameter("@ID_LanDanhGia", lanDanhGia),
                    new SqlParameter("@iErrorCode", DBNull.Value)
                };

                string sqlQuery = "[dbo].[pr_MTKN_NhanSu_LoaiMay_XepHang_Select_wID_PhongBan_New] @ID_PhongBan, @ID_LanDanhGia, @iErrorCode";
                DbContext dbc = new DbContext("name=TNG_QLKT");
                List<MTKN_XepHang_NhanSu_New> lst2 = new List<MTKN_XepHang_NhanSu_New>();
                lst2 = dbc.Database.SqlQuery<MTKN_XepHang_NhanSu_New>(sqlQuery, sqlPr).ToList();
                dbc.Dispose();
                return lst2;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        protected void drlXepHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Your code here to handle the selection change event
            DropDownList drlXepHang = (DropDownList)sender;
            string drlID = drlXepHang.ID;
            string drlValue = drlXepHang.SelectedValue; 
            int row = Int32.Parse(drlID.Split('_')[1]);
            int col = Int32.Parse(drlID.Split('_')[2]);
            loadColor(drlValue, row, col);
        }

        protected void lockControls(bool status)
        {
            gridLoaiMay.Enabled = !status;
            btnHuy.Enabled = !status;
            btnSave.Enabled = !status;
            btnUpdate.Enabled = status;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lockControls(false);
            showHiddenCols(true);
        }

        protected void loadColor(string val, int row, int col)
        {
            switch (val)
            {
                case "A":
                    gridLoaiMay.Rows[row].Cells[col].BackColor = Color.Blue;
                    break;
                case "B":
                    gridLoaiMay.Rows[row].Cells[col].BackColor = Color.Green;
                    break;
                case "C":
                    gridLoaiMay.Rows[row].Cells[col].BackColor = Color.Yellow;
                    break;
                case "D":
                    gridLoaiMay.Rows[row].Cells[col].BackColor = Color.Orange;
                    break;
                case "E":
                    gridLoaiMay.Rows[row].Cells[col].BackColor = Color.Red;
                    break;
            }
        }
    }
}