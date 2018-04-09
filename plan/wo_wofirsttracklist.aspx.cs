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

public partial class plan_wo_wofirsttracklist : BasePage
{
    static string strConn = ConfigurationManager.AppSettings["SqlConn.BarCodeSys"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txtActDateFrom.Text = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");
            //txtActDateTo.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            BindLine();
           // BindGrid();
            if (Convert.ToString(Session["PlantCode"]) == "1")
            {
                ddlDomain.SelectedIndex = 1;
            }
            else if (Convert.ToString(Session["PlantCode"]) == "2")
            {
                ddlDomain.SelectedIndex = 2;
            }
            else if (Convert.ToString(Session["PlantCode"]) == "5")
            {
                ddlDomain.SelectedIndex = 3;
            }
            else if (Convert.ToString(Session["PlantCode"]) == "8")
            {
                ddlDomain.SelectedIndex = 4;
            }
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

    protected void BindGrid()
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
        string domain = ddlDomain.SelectedItem.Text;

        //if (Session["PlantCode"] != null)
        //{
        //    domain = Session["PlantCode"].ToString();
        //    if (domain == "1")
        //    { domain = "SZX"; }
        //    else if (domain == "2")
        //    { domain = "ZQL"; }
        //    else if (domain == "5")
        //    { domain = "YQL"; }
        //    else if (domain == "8")
        //    { domain = "HQL"; }
        //}
        //else
        //{
        //    domain = "--";
        //}
        
        gvlist.DataSource = GetWoTrakingList(woNbr, part, relDateFrom, relDateTo, actDateFrom, actDateTo, domain, dropLine.SelectedValue
                , txtctr.Text.Trim(), ddlGet.SelectedValue.ToString(), ddlststus.SelectedValue.ToString().Trim(), ddlonline.SelectedValue.ToString().Trim()
                , ddlHasTracking.SelectedValue,ddlConfirm.SelectedValue);
        gvlist.DataBind();
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "first")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            this.Redirect("/plan/wo_first.aspx?domain=" + gvlist.DataKeys[index].Values["wo_domain"].ToString() + "&nbr="
                    + gvlist.DataKeys[index].Values["wo_nbr"].ToString()
                    + "&lot=" + gvlist.DataKeys[index].Values["wo_lot"].ToString()
                    + "&routing=" + gvlist.DataKeys[index].Values["wo_routing"].ToString());
        }
        if (e.CommandName == "firstInspection")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            this.Redirect("/plan/wo_firstInspection.aspx?domain=" + gvlist.DataKeys[index].Values["wo_domain"].ToString() + "&nbr="
                    + gvlist.DataKeys[index].Values["wo_nbr"].ToString()
                    + "&lot=" + gvlist.DataKeys[index].Values["wo_lot"].ToString()
                    + "&routing=" + gvlist.DataKeys[index].Values["wo_routing"].ToString()
                    + "&woPlan=1");
        }
    }
    public DataTable GetWoTrakingList(string woNbr, string part, string relDateFrom, string relDateTo, string actDateFrom, string actDateTo
        , string domain, string line, string ctr, string get, string stuts, string online, string hasTracking, string conf)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@woNbr", woNbr);
            param[1] = new SqlParameter("@part", part);
            param[2] = new SqlParameter("@relDateFrom", relDateFrom);
            param[3] = new SqlParameter("@relDateTo", relDateTo);
            param[4] = new SqlParameter("@actDateFrom", actDateFrom);
            param[5] = new SqlParameter("@actDateTo", actDateTo);
            param[6] = new SqlParameter("@domain", domain);
            param[7] = new SqlParameter("@line", line);
            param[8] = new SqlParameter("@ctr", ctr);
            param[9] = new SqlParameter("@get", get);
            param[10] = new SqlParameter("@stuts", stuts);
            param[11] = new SqlParameter("@online", online);
            param[12] = new SqlParameter("@hasTracking", hasTracking);
            param[13] = new SqlParameter("@hasConfirm", conf);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_first_selectExportData", param).Tables[0];
        }
        catch
        {
            return null;
        }

    }

    public DataTable GetWoExportDetail(string woNbr, string part, string relDateFrom, string relDateTo, string actDateFrom, string actDateTo
       , string domain, string line, string ctr, string get, string stuts, string online, string hasTracking, string conf)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@woNbr", woNbr);
            param[1] = new SqlParameter("@part", part);
            param[2] = new SqlParameter("@relDateFrom", relDateFrom);
            param[3] = new SqlParameter("@relDateTo", relDateTo);
            param[4] = new SqlParameter("@actDateFrom", actDateFrom);
            param[5] = new SqlParameter("@actDateTo", actDateTo);
            param[6] = new SqlParameter("@domain", domain);
            param[7] = new SqlParameter("@line", line);
            param[8] = new SqlParameter("@ctr", ctr);
            param[9] = new SqlParameter("@get", get);
            param[10] = new SqlParameter("@stuts", stuts);
            param[11] = new SqlParameter("@online", online);
            param[12] = new SqlParameter("@hasTracking", hasTracking);
            param[13] = new SqlParameter("@hasConfirm", conf);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_first_selectExportDataDetail", param).Tables[0];
        }
        catch
        {
            return null;
        }

    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[9].Text = e.Row.Cells[9].Text.Replace(";", "<br />");
            if (gvlist.DataKeys[e.Row.RowIndex].Values["wo_get"].ToString() != "领料")
            {
              //  e.Row.Cells[14].Text = gvlist.DataKeys[e.Row.RowIndex].Values["wo_get"].ToString();
            }
           if( e.Row.Cells[11].Text != "是")
           {
               ((LinkButton)e.Row.FindControl("firstInspection")).Text = "";
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
            string domain = ddlDomain.SelectedItem.Text;
            
            DataTable table = GetWoExportDetail(woNbr, part, relDateFrom, relDateTo, actDateFrom, actDateTo, domain, dropLine.SelectedValue
                , txtctr.Text.Trim(), ddlGet.SelectedValue.ToString(), ddlststus.SelectedValue.ToString().Trim(), ddlonline.SelectedValue.ToString().Trim()
                , ddlHasTracking.SelectedValue, ddlConfirm.SelectedValue);
          //  ExportData(table);
            string title = "<b>工单</b>~^<b>ID</b>~^120^<b>物料号</b>~^<b>下达日期</b>~^<b>评审日期</b>~^<b>地点</b>~^<b>产线</b>~^<b>成本中心</b>~^150^<b>工单上下线</b>~^<b>状态</b>~^<b>确认状态</b>~^<b>确认人</b>~^100^<b>确认状态（巡检员）</b>~^100^<b>巡检员</b>~^";
            title += "150^<b>物料号</b>~^250^<b>描述</b>~^<b>供应商</b>~^<b>批号</b>~^<b>确认状态</b>~^100^<b>确认状态（巡检员）</b>~^250^<b>备注</b>~^250^<b>备注1</b>~^";
            string[] pa = { "wo_nbr", "wo_lot"};
            ExportExcel(title, table, false, 14, pa);
        }
        catch
        {
            this.Alert("导出失败！请联系管理员！");
        }
    }
  
}