using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Configuration;
using IT;

public partial class IT_IT_JobSchedule : BasePage
{
    public string CONTENTUP = string.Empty;
    public string CONTENTDOWN = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
        }
    }
    protected void BuildCharge()
    {
        if (!this.IsDate(txtDate.Text))
        {
            this.Alert("日期格式不正确！");
            return;
        }

        DataTable table = TaskHelper.SelectJobsHistoryAndSchedule(txtDate.Text, chk.Checked);

        if (table != null)
        {
            if (table.Rows.Count > 0)
            {
                //循环：AM（0-12）
                for (int i = 0; i < 13; i++)
                {
                    CONTENTUP += "<td><div>";

                    //把对应时间的JOB放进来
                    foreach (DataRow row in table.Select("sch_hour = " + i.ToString()))
                    {
                        CONTENTUP += "<span class=\"JobName\">";

                        switch (Convert.ToInt32(row["run_status"]))
                        {
                            case 0: CONTENTUP += "<img class=\"icon_error\" border=\"0\" alt=\"\" />" + row["step_name"]; break;
                            case 1: CONTENTUP += "<img class=\"icon_accept\" border=\"0\" alt=\"\" />" + row["step_name"]; break;
                            case 2: CONTENTUP += "<img class=\"icon_delete\" border=\"0\" alt=\"\" />" + row["step_name"]; break;
                            case 3: CONTENTUP += "<img class=\"icon_delete\" border=\"0\" alt=\"\" />" + row["step_name"]; break;
                            case 4: CONTENTUP += "<img class=\"icon_process\" border=\"0\" alt=\"\" />" + row["step_name"]; break;
                            case 5: CONTENTUP += "<img class=\"icon_clock\" border=\"0\" alt=\"\" />" + row["step_name"]; break;
                        }

                        CONTENTUP += "</span>";
                    }

                    CONTENTUP += "</div></td>";
                }

                //循环：PM（13-23）
                CONTENTDOWN += "<td></td>";
                for (int i = 13; i < 24; i++)
                {
                    CONTENTDOWN += "<td><div>";

                    //把对应时间的JOB放进来
                    foreach (DataRow row in table.Select("sch_hour = " + i.ToString()))
                    {
                        CONTENTDOWN += "<span class=\"JobName\">";

                        switch (Convert.ToInt32(row["run_status"]))
                        {
                            case 0: CONTENTDOWN += "<img class=\"icon_error\" border=\"0\" alt=\"\" />" + row["step_name"]; break;
                            case 1: CONTENTDOWN += "<img class=\"icon_accept\" border=\"0\" alt=\"\" />" + row["step_name"]; break;
                            case 2: CONTENTDOWN += "<img class=\"icon_delete\" border=\"0\" alt=\"\" />" + row["step_name"]; break;
                            case 3: CONTENTDOWN += "<img class=\"icon_delete\" border=\"0\" alt=\"\" />" + row["step_name"]; break;
                            case 4: CONTENTDOWN += "<img class=\"icon_process\" border=\"0\" alt=\"\" />" + row["step_name"]; break;
                            case 5: CONTENTDOWN += "<img class=\"icon_clock\" border=\"0\" alt=\"\" />" + row["step_name"]; break;
                        }

                        CONTENTDOWN += "</span>";
                    }

                    CONTENTDOWN += "</div></td>";
                }

                CONTENTDOWN += "<td></td>";
            }
            else
            {
                this.Alert("没有获取到JOB数据！");
            }
        }
        else
        {
            this.Alert("获取JOB数据失败！");
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BuildCharge();
    }
}