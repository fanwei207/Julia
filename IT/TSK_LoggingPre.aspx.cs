using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using IT;

public partial class TSK_LoggingPre : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidNbr.Value = Request.QueryString["tskNbr"];

            BindData();
        }
    }
    protected void BindData()
    {
        DataTable table = TaskHelper.SelectTaskLoggingPre(hidNbr.Value, Session["uID"].ToString());
        if (table == null)
        {
            btnDone.Enabled = false;
            this.Alert("任务明细拉取失败！请返回前一页面！");
        }
        else
        {
            if (table.Rows.Count == 0)
            {
                btnDone.Enabled = false;
                btnDone.ToolTip = "没有已测试完成的任务！";
            }

            gv.DataSource = table;
            gv.DataBind();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Redirect("TSK_TaskList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
 
        }
    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        string _detList = string.Empty;

        foreach (GridViewRow row in gv.Rows)
        {
            _detList += gv.DataKeys[row.RowIndex].Values["tskd_id"].ToString() + ";";
        }

        this.Redirect("TSK_Logging.aspx?tskNbr=" + hidNbr.Value + "&detlist=" + _detList + "&uID=" + Session["uID"].ToString() + "&rt=" + DateTime.Now.ToFileTime().ToString());
    }
}