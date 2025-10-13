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
    public partial class NhapThoiGian : System.Web.UI.Page
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
                    Session["XacNhan"] = null;
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    if (!string.IsNullOrEmpty(txtDate.Text))
                        loadDataGrid();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        #region "Đóng"
        //protected void btnnhap_Click(object sender, EventArgs e)
        //{
        //    btnnhap.Attributes["class"] = "w3-bar-item w3-button tablink w3-red";
        //    btnbao.Attributes["class"] = "w3-bar-item w3-button tablink";
        //    divNhap.Style["display"] = "block";
        //    divBao.Style["display"] = "none";
        //}

        //protected void btnbao_Click(object sender, EventArgs e)
        //{
        //    btnnhap.Attributes["class"] = "w3-bar-item w3-button tablink";
        //    btnbao.Attributes["class"] = "w3-bar-item w3-button tablink w3-red";
        //    divNhap.Style["display"] = "none";
        //    divBao.Style["display"] = "block";
        //}
        #endregion

        protected void loadDataGrid()
        {
            try
            {
                int mansid = 0;
                if (Session["userid"] != null)
                    mansid = Convert.ToInt32(Session["userid"].ToString());
                
                DateTime dte = DateTime.Parse(txtDate.Text);
                object[] sqlPr =
                {
                    new SqlParameter("@MaNS_ID", mansid),
                    new SqlParameter("@Ngay", dte.Date),
                };
                string sqlQuery = "[dbo].[pr_WEB_LCB_DM_LyDoNgungViec_GetByMaNS_Ngay] @MaNS_ID,@Ngay";
                List<ListThoiGianCho> lst = new List<ListThoiGianCho>();
                lst = db.Database.SqlQuery<ListThoiGianCho>(sqlQuery, sqlPr).ToList();
                if (lst != null && lst.Count > 0)
                {
                    gridNhapThoiGian.DataSource = lst;
                    gridNhapThoiGian.DataBind();
                    decimal total = 0;
                    gridNhapThoiGian.FooterRow.Cells[0].Text = "Tổng (phút): ";
                    gridNhapThoiGian.FooterRow.Cells[0].Font.Bold = true;
                    gridNhapThoiGian.FooterRow.Cells[0].ColumnSpan = 3;
                    gridNhapThoiGian.FooterRow.Cells[1].Visible = false;
                    gridNhapThoiGian.FooterRow.Cells[2].Visible = false;
                    gridNhapThoiGian.FooterRow.Cells[3].Visible = false;
                    for (int i = 0;i<lst.Count;i++)
                    {
                        ListThoiGianCho ls = lst[i];
                        total += ls.ThoiGian;
                    }
                    gridNhapThoiGian.FooterRow.Cells[5].Text = string.Format("{0:0.#}", total);
                    gridNhapThoiGian.FooterRow.Cells[5].Font.Bold = true;
                    gridNhapThoiGian.FooterRow.Cells[5].Style["text-align"] = "right";
                    gridNhapThoiGian.FooterRow.Cells[5].Style["padding-right"] = "12px";
                    gridNhapThoiGian.FooterRow.BackColor = System.Drawing.Color.Beige;
                    if (lst[0].PheDuyet == true)
                    {
                        btnXacNhan.Enabled = false;
                    }
                    else
                    {
                        btnXacNhan.Enabled = true;
                    }
                }
                else
                {
                    btnXacNhan.Enabled = true;
                    DataTable dt = ultils.CreateDataTable<ListThoiGianCho>(lst);
                    dt.Rows.Add(dt.NewRow());
                    gridNhapThoiGian.DataSource = dt;
                    gridNhapThoiGian.DataBind();
                    gridNhapThoiGian.Rows[0].Cells.Clear();
                    gridNhapThoiGian.Rows[0].Cells.Add(new TableCell());
                    gridNhapThoiGian.Rows[0].Cells[0].ColumnSpan = 5;
                    gridNhapThoiGian.Rows[0].Cells[0].Text = "Chưa có dữ liệu ..!";
                    gridNhapThoiGian.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

                    gridNhapThoiGian.FooterRow.Cells[0].Text = "Tổng (phút): ";
                    gridNhapThoiGian.FooterRow.Cells[0].Font.Bold = true;
                    gridNhapThoiGian.FooterRow.Cells[0].ColumnSpan = 3;
                    gridNhapThoiGian.FooterRow.Cells[1].Visible = false;
                    gridNhapThoiGian.FooterRow.Cells[2].Visible = false;
                    gridNhapThoiGian.FooterRow.Cells[3].Visible = false;
                    decimal total = 0;
                    gridNhapThoiGian.FooterRow.Cells[5].Text = string.Format("{0:0.#}", total);
                    gridNhapThoiGian.FooterRow.Cells[5].Font.Bold = true;
                    gridNhapThoiGian.FooterRow.Cells[5].Style["text-align"] = "right";
                    gridNhapThoiGian.FooterRow.Cells[5].Style["padding-right"] = "12px";
                    gridNhapThoiGian.FooterRow.BackColor = System.Drawing.Color.Beige;
                }
            }
            catch (Exception ex) { }
        }

        protected void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                bool error = false;
                int id = 0;
                DateTime dttuk = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TimeSpan.Parse("22:00").Hours, TimeSpan.Parse("22:00").Minutes, 00);
                DateTime dtduk = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TimeSpan.Parse("07:00").Hours, TimeSpan.Parse("07:00").Minutes, 00);
                if (gridNhapThoiGian.Rows.Count > 0)
                {
                    int mansid = 0, phongbanid = 0;
                    if (Session["userid"] != null)
                        mansid = Convert.ToInt32(Session["userid"].ToString());
                    
                    if (Session["PhongBanID"] != null)
                        phongbanid = Convert.ToInt32(Session["PhongBanID"].ToString());
                    DateTime dte = DateTime.Parse(txtDate.Text);
                    foreach (GridViewRow row in gridNhapThoiGian.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            string xacnhan = ((Label)row.FindControl("lblXacNhan")).Text;
                            Label lblIdLyDo = (Label)row.FindControl("lblIdLyDo");
                            TextBox txtStartDate = (TextBox)row.FindControl("txtStartDate");
                            TextBox txtEndDate = (TextBox)row.FindControl("txtEndDate");
                            TextBox txtThoiGian = (TextBox)row.FindControl("txtThoiGian");
                            TextBox txtGhiChu = (TextBox)row.FindControl("txtGhiChu");
                            if (!string.IsNullOrEmpty(xacnhan) && bool.Parse(xacnhan) == false && lblIdLyDo != null && !string.IsNullOrEmpty(lblIdLyDo.Text) && !string.IsNullOrEmpty(txtStartDate.Text) && txtThoiGian != null && !string.IsNullOrEmpty(txtThoiGian.Text) && Convert.ToDecimal(txtThoiGian.Text)>0)
                            {
                                
                                DateTime dtnowstart = dte.Date + TimeSpan.Parse(txtStartDate.Text);
                                DateTime dtnowend = dte.Date + TimeSpan.Parse(txtEndDate.Text);
                                if(DateTime.Compare(dtnowstart, dttuk) >=0 && DateTime.Compare(dtnowstart, dtduk) <= 0)
                                {
                                    error = false;
                                    break;
                                }
                                if (DateTime.Compare(dtnowend, dttuk) >= 0 && DateTime.Compare(dtnowend, dtduk) <= 0)
                                {
                                    error = false;
                                    break;
                                }
                                byte idlydo = byte.Parse(lblIdLyDo.Text);
                                LCB_ThoiGianNgungViec timengung = new LCB_ThoiGianNgungViec();
                                timengung = db.LCB_ThoiGianNgungViec.Where(x => x.MaNS_ID == mansid && x.Ngay.Year == dte.Year && x.Ngay.Month == dte.Month && x.Ngay.Day == dte.Day && x.ID_LyDoNgungViec == idlydo).FirstOrDefault();
                                if(timengung != null)
                                {
                                    timengung.ThoiGian_BatDau = dte.Date + TimeSpan.Parse(txtStartDate.Text);
                                    timengung.ThoiGian_KetThuc = dte.Date + TimeSpan.Parse(txtEndDate.Text);
                                    timengung.ThoiGian = Convert.ToInt32(txtThoiGian.Text);
                                    timengung.ThoiGian_XacNhan_BatDau = dte.Date + TimeSpan.Parse(txtStartDate.Text);
                                    timengung.ThoiGian_XacNhan_KetThuc = dte.Date + TimeSpan.Parse(txtEndDate.Text);
                                    if (!string.IsNullOrEmpty(txtGhiChu.Text))
                                        timengung.GhiChu = txtGhiChu.Text;
                                    timengung.ThoiGian_XacNhan = Convert.ToInt32(txtThoiGian.Text);
                                    id = id + db.SaveChanges();
                                }
                                else
                                { 
                                    timengung = new LCB_ThoiGianNgungViec();
                                    timengung.MaNS_ID = Convert.ToInt32(mansid);
                                    timengung.Ngay = dte.Date;
                                    timengung.PhongBanID = phongbanid;
                                    timengung.ID_LyDoNgungViec = idlydo;
                                    timengung.ThoiGian_BatDau = dte.Date + TimeSpan.Parse(txtStartDate.Text);
                                    timengung.ThoiGian_KetThuc = dte.Date + TimeSpan.Parse(txtEndDate.Text);
                                    timengung.ThoiGian = Convert.ToInt32(txtThoiGian.Text);
                                    timengung.ThoiGian_XacNhan_BatDau = dte.Date + TimeSpan.Parse(txtStartDate.Text);
                                    timengung.ThoiGian_XacNhan_KetThuc = dte.Date + TimeSpan.Parse(txtEndDate.Text);
                                    if (!string.IsNullOrEmpty(txtGhiChu.Text))
                                        timengung.GhiChu = txtGhiChu.Text;
                                    timengung.ThoiGian_XacNhan = Convert.ToInt32(txtThoiGian.Text);
                                    timengung.XacNhan = false;
                                    db.LCB_ThoiGianNgungViec.Add(timengung);
                                    id = id + db.SaveChanges();
                                }
                            }
                        }
                    }
                    if (id != 0 && error == false)
                    {
                        callstore();
                        loadDataGrid();
                        lblMessenger.Text = "Đã cập nhật thời gian chờ.";
                        addthismodalContact.Style["display"] = "block";
                        divThongBao.Style["display"] = "block";
                    }
                    else
                    {
                        lblMessenger.Text = "Thời gian chờ không đúng, vui lòng kiểm tra lại. (Định dạng thời gian là 24h!)";
                        addthismodalContact.Style["display"] = "block";
                        divThongBao.Style["display"] = "block";
                    }
                }
            }
            catch(Exception ex) { }
        }

        protected void callstore()
        {
            try
            {
                int phongid = 0;
                int mansid = 0;
                if (Session["PhongBanID"] != null && Session["PhongBanID"].ToString() != "")
                {
                    phongid = Convert.ToInt32(Session["PhongBanID"].ToString());
                }
                if (Session["userid"] != null)
                    mansid = Convert.ToInt32(Session["userid"].ToString());
                DateTime dte = DateTime.Parse(txtDate.Text);
                object[] sqlPr =
               {
                    new SqlParameter("@iPhongBanID", phongid),
                    new SqlParameter("@iMaNS_ID", mansid),
                    new SqlParameter("@daNgay", dte.Date)
                };
                string sqlQuery = "exec [dbo].[pr_LCB_LuongNgayCongNhan_TinhLuong] @iPhongBanID,@iMaNS_ID,@daNgay";
                db.Database.ExecuteSqlCommand(sqlQuery, sqlPr);
            }
            catch (Exception ex) { }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDate.Text))
            {
                if(DateTime.Compare(DateTime.Parse(txtDate.Text).Date,DateTime.Now.Date) >= 0)
                    loadDataGrid();
                else
                {
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    lblMessenger.Text = "Không đươc nhập thời gian chờ của những ngày đã qua.";
                    addthismodalContact.Style["display"] = "block";
                    divThongBao.Style["display"] = "block";
                }
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            addthismodalContact.Style["display"] = "none";
            divThongBao.Style["display"] = "none";
        }

        protected void gridNhapThoiGian_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string xacnhan = DataBinder.Eval(e.Row.DataItem, "XacNhan").ToString();
                TextBox txt = (TextBox)e.Row.FindControl("txtThoiGian");
                ImageButton cmdDelete = (ImageButton)e.Row.FindControl("cmdDelete");
                txt.Attributes.Add("type", "number");
                if (!string.IsNullOrEmpty(txt.Text) && txt.Text == "0")
                    txt.Attributes.Add("onclick", "this.value = '';");
                if(!string.IsNullOrEmpty(xacnhan))
                {
                    bool xn = bool.Parse(xacnhan);
                    if (xn == true)
                    {
                        cmdDelete.Enabled = false;
                        txt.Enabled = false;
                    }
                    else
                    {
                        cmdDelete.Enabled = true;
                        txt.Enabled = true;
                    }
                }
            }
        }

        protected void txtStartDate_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            GridViewRow row = (GridViewRow)txt.NamingContainer;
            TextBox end = (TextBox)row.FindControl("txtEndDate");
            TextBox time = (TextBox)row.FindControl("txtThoiGian");
            if (!string.IsNullOrEmpty(txt.Text))
            {
                if (!string.IsNullOrEmpty(end.Text))
                {
                    if(TimeSpan.Parse(txt.Text).Hours < TimeSpan.Parse("12:00").Hours && TimeSpan.Parse(end.Text).Hours >= TimeSpan.Parse("13:00").Hours)
                    {
                        TimeSpan sophut = TimeSpan.Parse(end.Text) - TimeSpan.Parse(txt.Text);
                        time.Text = (sophut.TotalMinutes - 60).ToString();
                    }
                    else
                    {
                        TimeSpan sophut = TimeSpan.Parse(end.Text) - TimeSpan.Parse(txt.Text);
                        time.Text = sophut.TotalMinutes.ToString();
                    }
                }
                else if (!string.IsNullOrEmpty(time.Text) && Convert.ToInt32(time.Text) > 0)
                {
                    TimeSpan t = TimeSpan.FromMinutes(double.Parse(time.Text));
                    TimeSpan enddate = TimeSpan.Parse(txt.Text) + t;
                    if(TimeSpan.Parse(txt.Text).Hours < TimeSpan.Parse("12:00").Hours && enddate.Hours >= TimeSpan.Parse("13:00").Hours)
                    {
                        enddate += TimeSpan.FromHours(1);
                        end.Text = (DateTime.Now.Date + enddate).ToString("HH:mm");
                    }
                    else   
                        end.Text = (DateTime.Now.Date + enddate).ToString("HH:mm");
                }
            }
        }

        protected void txtEndDate_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            GridViewRow row = (GridViewRow)txt.NamingContainer;
            TextBox start = (TextBox)row.FindControl("txtStartDate");
            TextBox time = (TextBox)row.FindControl("txtThoiGian");
            if (!string.IsNullOrEmpty(txt.Text))
            {
                if (!string.IsNullOrEmpty(start.Text))
                {
                    if (TimeSpan.Parse(start.Text).Hours < TimeSpan.Parse("12:00").Hours && TimeSpan.Parse(txt.Text).Hours >= TimeSpan.Parse("13:00").Hours)
                    {
                        TimeSpan sophut = TimeSpan.Parse(txt.Text) - TimeSpan.Parse(start.Text);
                        time.Text = (sophut.TotalMinutes - 60).ToString();
                    }
                    else
                    {
                        TimeSpan sophut = TimeSpan.Parse(txt.Text) - TimeSpan.Parse(start.Text);
                        time.Text = sophut.TotalMinutes.ToString();
                    }                    
                }
            }
        }

        protected void txtThoiGian_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            GridViewRow row = (GridViewRow)txt.NamingContainer;
            TextBox end = (TextBox)row.FindControl("txtEndDate");
            TextBox start = (TextBox)row.FindControl("txtStartDate");
            if (!string.IsNullOrEmpty(txt.Text))
            {
                if (!string.IsNullOrEmpty(start.Text))
                {
                    TimeSpan sophut = TimeSpan.Parse(start.Text) + TimeSpan.FromMinutes(double.Parse(txt.Text));
                    if (TimeSpan.Parse(start.Text).Hours < TimeSpan.Parse("12:00").Hours && sophut.Hours >= TimeSpan.Parse("13:00").Hours)
                    {
                        sophut += TimeSpan.FromHours(1);
                        end.Text = (DateTime.Now.Date + sophut).ToString("HH:mm");
                    }
                    else
                        end.Text = (DateTime.Now.Date + sophut).ToString("HH:mm");
                }
            }
        }

        protected void gridNhapThoiGian_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                LCB_ThoiGianNgungViec tb = new LCB_ThoiGianNgungViec();
                int id = int.Parse(gridNhapThoiGian.DataKeys[e.RowIndex].Value.ToString());
                tb = db.LCB_ThoiGianNgungViec.Where(x => x.ID_ThoiGianNgungViec == id).Single();
                db.LCB_ThoiGianNgungViec.Remove(tb);
                db.SaveChanges();
                loadDataGrid();
            }
            catch
            {
                
            }
        }
    }
}