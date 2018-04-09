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

public partial class TSK_TaskView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["tskNbr"]))
            {
                DataTable table = TaskHelper.SelectTaskMstrByNbr(Request.QueryString["tskNbr"]);
                if (table == null || table.Rows.Count <= 0)
                {
                    ltlAlert.Text = "alert('没有找到任务！请返回上一页！');";
                }
                else
                {
                    txtDesc.Text = table.Rows[0]["tsk_desc"].ToString();
                    txtUserNo.Text = table.Rows[0]["tsk_applyNo"].ToString() + "--" + table.Rows[0]["tsk_applyName"].ToString() + "--" + table.Rows[0]["tsk_applyDomain"].ToString();

                    if (!string.IsNullOrEmpty(table.Rows[0]["tsk_fileName"].ToString()))
                    {
                        hlinkFile.Text = table.Rows[0]["tsk_fileName"].ToString();
                        hlinkFile.NavigateUrl = table.Rows[0]["tsk_filePath"].ToString();
                    }
                    else
                    {
                        hlinkFile.Visible = false;
                    }

                    txtExtreDesc.Text = table.Rows[0]["tsk_extreDesc"].ToString();
                }
            }
            else
            {
                ltlAlert.Text = "alert('没有传回参数！请返回上一页！');";
            }
        }
    }
}