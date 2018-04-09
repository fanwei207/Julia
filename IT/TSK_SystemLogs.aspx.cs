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
using System.Diagnostics;

public partial class TSK_SystemLogs : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            BindData();
        }
    }
    protected void BindData()
    {
        DataTable table = new DataTable();

        table.Columns.Add("Type", System.Type.GetType("System.String"));
        table.Columns.Add("Date", System.Type.GetType("System.String"));
        table.Columns.Add("Time", System.Type.GetType("System.String"));
        table.Columns.Add("Source", System.Type.GetType("System.String"));
        table.Columns.Add("Category", System.Type.GetType("System.String"));
        table.Columns.Add("Event", System.Type.GetType("System.String"));
        table.Columns.Add("User", System.Type.GetType("System.String"));
        table.Columns.Add("Computer", System.Type.GetType("System.String"));
        table.Columns.Add("Message", System.Type.GetType("System.String"));

        EventLog eventLog = new EventLog();

        eventLog.Log = "Application";

        EventLogEntryCollection myCollection = eventLog.Entries;

        foreach (EventLogEntry log in myCollection)
        {
            if (log.TimeWritten < Convert.ToDateTime(txtDate.Text))
            {
                continue;
            }

            if (log.TimeWritten >= Convert.ToDateTime(txtDate.Text).AddDays(1))
            {
                continue;
            }

            if (log.EntryType != EventLogEntryType.Warning)
            {
                continue;
            }

            DataRow row = table.NewRow();

            row["Type"] = log.EntryType.ToString();
            row["Date"] = string.Format("{0:yyyy-MM-dd}", log.TimeWritten);
            row["Time"] = string.Format("{0:HH:mm:ss}", log.TimeWritten);
            row["Source"] = log.Source.ToString();
            row["Category"] = log.Category.ToString();
            row["Event"] = log.InstanceId.ToString();
            row["User"] = "N/A";
            row["Computer"] = log.MachineName.ToString();

            string _message = log.Message.ToString();
            _message = _message.Replace("\n", "<br />");
            _message = _message.Replace("\"", "&quot;");
            //Exception message、Request URL、Request path加粗
            //_message = Server.UrlEncode(_message);
            _message = _message.Replace("Exception message", "<font style='color:blue; font-size:14px; font-weight:bold;'>Exception message</font>");
            _message = _message.Replace("Request URL", "<font style='color:blue; font-size:14px; font-weight:bold;'>Request URL</font>");
            _message = _message.Replace("Request path", "<font style='color:blue; font-size:14px; font-weight:bold;'>Request path</font>");
            row["Message"] = _message;

            table.Rows.Add(row);
        }

        gv.DataSource = table;
        gv.DataBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;

        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (!IsDate(txtDate.Text))
        {
            this.Alert("日期格式不正确！");
            return;
        }

        BindData();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }

}