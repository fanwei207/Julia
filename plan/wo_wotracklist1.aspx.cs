using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using adamFuncs;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CommClass;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;

public partial class wo_wotracklist1 : BasePage
{
    static string strConn = ConfigurationManager.AppSettings["SqlConn.BarCodeSys"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txtActDateFrom.Text = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");
            //txtActDateTo.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            BindLine();
            //BindGrid();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void BindLine()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@plantCode", Convert.ToString(Session["PlantCode"]));
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_note_selectLine", param);

            dropLine.DataSource = ds;
            dropLine.DataBind();

            dropLine.Items.Insert(0, new ListItem("--", ""));
        }
        catch { }
    }

    protected  void BindGrid()
    {
        try
        {
            if (txtActDateFrom.Text.Trim().Length > 0)
            {
                DateTime dt1 = Convert.ToDateTime(txtActDateFrom.Text.Trim());
            }
            if (txtActDateTo.Text.Trim().Length > 0)
            {
                DateTime dt2 = Convert.ToDateTime(txtActDateTo.Text.Trim());
            }
            
        }
        catch
        {
            this.Alert("日期格式有误，请重新输入！");
            return;
        }
        string woNbr = txtNbr.Text.Trim();
        string part = txtQAD.Text.Trim();
        string actDateFrom = txtActDateFrom.Text.Trim();
        string actDateTo = txtActDateTo.Text.Trim();
        //string domain = ddlDomain.SelectedItem.Text;
        string domain = "";
        if (Session["PlantCode"] != null)
        {
            domain = Session["PlantCode"].ToString();
            if (domain == "1")
            { domain = "SZX"; }
            else if (domain == "2")
            { domain = "ZQL"; }
            else if (domain == "5")
            { domain = "YQL"; }
            else if (domain == "8")
            { domain = "HQL"; }
        }
        else
        {
            domain = "--";
        }
        gvlist.DataSource = GetExportTable(domain, woNbr, part, actDateFrom, actDateTo, dropLine.SelectedValue);
        gvlist.DataBind();
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Tracking")
        {
            int index = ((GridViewRow)(((Button)e.CommandSource).Parent.Parent)).RowIndex;
            this.Redirect("/plan/wo_trackdet.aspx?domain=" + gvlist.DataKeys[index].Values["wo_domain"].ToString() + "&nbr=" 
                    + gvlist.DataKeys[index].Values["wo_nbr"].ToString() 
                    + "&lot=" + gvlist.DataKeys[index].Values["wo_lot"].ToString());
        }
    }
    public DataTable GetWoTrakingList(string domain, string woNbr, string part, string actDateFrom, string actDateTo, string line, int tracktype)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[4] = new SqlParameter("@domain", domain);
            param[0] = new SqlParameter("@nbr", woNbr);
            param[1] = new SqlParameter("@part", part);
            param[2] = new SqlParameter("@stdDate", actDateFrom);
            param[3] = new SqlParameter("@endDate", actDateTo);
            param[5] = new SqlParameter("@line", line);
            param[6] = new SqlParameter("@type", tracktype);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_track_selectWoTrackHourly", param).Tables[0];
        }
        catch
        {
            return null;
        }

    }
    string nbr = "";
    string lot = "";
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[12].Text = e.Row.Cells[12].Text.Replace("\\n", "<br />");
            e.Row.Cells[13].Text = e.Row.Cells[13].Text.Replace("\\n", "<br />");
            e.Row.Cells[15].Text = e.Row.Cells[15].Text.Replace("\\n", "<br />");
            int index = e.Row.RowIndex;
            if (index == 0)
            {
                nbr = e.Row.Cells[0].Text;
                lot = e.Row.Cells[1].Text;
            }
            else
            {
                if (e.Row.Cells[0].Text != nbr && e.Row.Cells[1].Text != lot)
                {
                    nbr = e.Row.Cells[0].Text;
                    lot = e.Row.Cells[1].Text;
                }
                else
                {
                    for (int i = 0; i <= 13; i++)
                    {
                        e.Row.Cells[i].Text = "";
                    }
                }
            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                if (txtActDateFrom.Text.Trim().Length > 0)
                {
                    DateTime dt1 = Convert.ToDateTime(txtActDateFrom.Text.Trim());
                }
                if (txtActDateTo.Text.Trim().Length > 0)
                {
                    DateTime dt2 = Convert.ToDateTime(txtActDateTo.Text.Trim());
                }

            }
            catch
            {
                this.Alert("日期格式有误，请重新输入！");
                return;
            }
            string woNbr = txtNbr.Text.Trim();
            string part = txtQAD.Text.Trim();
            string relDateFrom = "";
            string relDateTo = "";
            string actDateFrom = txtActDateFrom.Text.Trim();
            string actDateTo = txtActDateTo.Text.Trim();
            //string domain = ddlDomain.SelectedItem.Text;
            string domain = "";
            if (Session["PlantCode"] != null)
            {
                domain = Session["PlantCode"].ToString();
                if (domain == "1")
                { domain = "SZX"; }
                else if (domain == "2")
                { domain = "ZQL"; }
                else if (domain == "5")
                { domain = "YQL"; }
                else if (domain == "8")
                { domain = "HQL"; }
            }
            else
            {
                domain = "--";
            }
            DataTable table = GetExportTable(domain, woNbr, part, actDateFrom, actDateTo, dropLine.SelectedValue);
            string title = "<b>工单</b>~^<b>ID</b>~^120^<b>物料号</b>~^<b>域</b>~^<b>地点</b>~^<b>产线</b>~^<b>上线日期</b>~^<b>下线日期</b>~^<b>评审日期</b>~^150^<b>部件号</b>~^<b>工单数</b>~^<b>完工数</b>~^<b>只数</b>~^<b>完成只数</b>~^<b>汇总总数</b>~^<b>一次次品数</b>~^<b>汇报人数</b>~^<b>维修数量</b>~^<b>小时数</b>~^<b>日期</b>~^<b>时段</b>~^<b>次品现象</b>~^<b>次品</b>~^250^<b>次品明细</b>~^150^<b>备注</b>~^";
            //title += "150^<b>物料号</b>~^250^<b>描述</b>~^<b>供应商</b>~^<b>批号</b>~^<b>确认状态</b>~^250^<b>备注</b>~^";
            string[] pa = { "trk_nbr", "trk_lot" };
            ExportExcel(title, table, false, 19, pa);
        }
        catch
        {
            this.Alert("导出失败，请联系管理员！");
        }
    }
    public DataTable GetExportTable(string domain, string woNbr, string part, string actDateFrom, string actDateTo, string line)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[4] = new SqlParameter("@domain", domain);
            param[0] = new SqlParameter("@nbr", woNbr);
            param[1] = new SqlParameter("@part", part);
            param[2] = new SqlParameter("@stdDate", actDateFrom);
            param[3] = new SqlParameter("@endDate", actDateTo);
            param[5] = new SqlParameter("@line", line);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_track_selectWoTrackHourly1", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
}