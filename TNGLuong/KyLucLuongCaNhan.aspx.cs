using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNGLuong.Models;

namespace TNGLuong
{
    public partial class KyLucLuongCaNhan : System.Web.UI.Page
    {
        TNG_CTLDbContact db = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new TNG_CTLDbContact();
            if (Session["username"] != null)
            {
                if (!IsPostBack)
                {
                    Load_ddlChon();
                    Load_ChartKLCN();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Load_ddlChon()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("IDTimKiem", typeof(int)));
            dt.Columns.Add(new DataColumn("TimKiem", typeof(string)));
            DataRow dr = dt.NewRow();
            dr["IDTimKiem"] = 1;
            dr["TimKiem"] = "Ngày";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["IDTimKiem"] = 2;
            dr["TimKiem"] = "Tuần";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["IDTimKiem"] = 3;
            dr["TimKiem"] = "Tháng";
            dt.Rows.Add(dr);

            cmbKLCaNhan.DataSource = dt;
            cmbKLCaNhan.DataBind();
            cmbKLCaNhan.SelectedValue = "1";
        }

        protected void Load_ChartKLCN()
        {
            Resize();
            int iMaNS_ID = 0;
            int iTimKiem = 0;
            if (cmbKLCaNhan.SelectedValue != null && cmbKLCaNhan.SelectedValue.ToString() != "")
                iTimKiem = int.Parse(cmbKLCaNhan.SelectedValue.ToString());
            if (Session["userid"] != null)
                iMaNS_ID = int.Parse(Session["userid"].ToString());
            object[] sqlPr =
            {
                new SqlParameter("@iMaNS_ID", iMaNS_ID),
                new SqlParameter("@iLoai", iTimKiem)
            };
            string sqlQuery = "[dbo].[pr_Web_LCB_LuongNgayCongNhan_rpt_KyLucLuongCaNhan] @iMaNS_ID,@iLoai";
            List<clsKyLucLuongCaNhan> lst = new List<clsKyLucLuongCaNhan>();
            lst = db.Database.SqlQuery<clsKyLucLuongCaNhan>(sqlQuery, sqlPr).ToList();
            ChartKLCaNhan.DataSource = lst;
            ChartKLCaNhan.DataBind();

            ChartKLCaNhan.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            ChartKLCaNhan.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            ChartKLCaNhan.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            ChartKLCaNhan.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            ChartKLCaNhan.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

            if(iTimKiem == 1)
                lblTieuDe.Text = "KỶ LỤC 5 NGÀY LƯƠNG CAO NHẤT TỪ NĂM "+ (DateTime.Now.Year - 1) +"-"+ DateTime.Now.Year;
            if (iTimKiem == 2)
                lblTieuDe.Text = "KỶ LỤC 5 TUẦN LƯƠNG CAO NHẤT TỪ NĂM " + (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year;
            if (iTimKiem == 3)
                lblTieuDe.Text = "KỶ LỤC 5 THÁNG LƯƠNG CAO NHẤT TỪ NĂM " + (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year;
        }

        protected void cmbKLCaNhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_ChartKLCN();
        }

        protected void Resize()
        {
            if (Request.Browser["IsMobileDevice"] == "true")
            {
                divChart.Style["padding-left"] = "1px";
                ChartKLCaNhan.Style["padding-top"] = "10px";
                ChartKLCaNhan.Style["width"] = "100%";
                ChartKLCaNhan.Style["height"] = "100%";
            }
            else
            {
                divChart.Style["padding-left"] = "285px";
                ChartKLCaNhan.Style["padding-top"] = "25px";
                ChartKLCaNhan.Style["width"] = "400px";
                ChartKLCaNhan.Style["height"] = "400px";
            }
        }
    }
}