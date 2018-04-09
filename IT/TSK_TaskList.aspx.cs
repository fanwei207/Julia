using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using IT;

public partial class IT_TSK_TaskList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCrtDate1.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            BindUser();

            try
            {
                dropUsers.SelectedIndex = -1;
                dropUsers.Items.FindByValue(Session["uID"].ToString()).Selected = true;
            }
            catch { }

            #region 模态窗口关闭后的刷新
            if (!string.IsNullOrEmpty(Request.QueryString["date1"]))
            {
                txtCrtDate1.Text = Request.QueryString["date1"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["date2"]))
            {
                txtCrtDate2.Text = Request.QueryString["date2"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["usr"]))
            {
                dropUsers.Items.FindByValue(Request.QueryString["usr"]).Selected = true;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["comp"]))
            {
                if (Request.QueryString["comp"].ToLower() == "true")
                {
                    chkNotComplete.Checked = true;
                }
                else
                {
                    chkNotComplete.Checked = false;
                }
            }

            if (!string.IsNullOrEmpty(Request.QueryString["cancel"]))
            {
                if (Request.QueryString["cancel"].ToLower() == "true")
                {
                    chkNotCancel.Checked = true;
                }
                else
                {
                    chkNotCancel.Checked = false;
                }
            }
            #endregion

            BindGridView();
        }
    }
    protected override void BindGridView()
    {
        gv.DataSource = TaskHelper.SelectTaskList(txtNbr.Text, txtCrtDate1.Text.Trim(), txtCrtDate2.Text.Trim(), chkNotComplete.Checked
                , chkNotCancel.Checked, dropUsers.SelectedValue, Session["uID"].ToString());
        gv.DataBind(); 
    }
    protected void BindUser()
    {
        DataTable table = TaskHelper.GetUsers(string.Empty, 404);
        dropUsers.Items.Clear();

        dropUsers.DataSource = table;
        dropUsers.DataBind();
        dropUsers.Items.Insert(0, new ListItem("--全部--", "0"));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtCrtDate1.Text))
        {
            this.Alert("创建日期 的起始时间不能为空！");
            return;
        }
        else if (!this.IsDate(txtCrtDate1.Text))
        {
            this.Alert("创建日期格式不正确！");
            return;
        }

        if (!string.IsNullOrEmpty(txtCrtDate2.Text))
        {
            if (!this.IsDate(txtCrtDate1.Text))
            {
                this.Alert("创建日期 的截至时间格式不正确！");
                return;
            }
        }

        BindGridView();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            #region 转换各按钮状态
            //0:新建  1:进行中 2：已完成。分析、维护的任务无需测试
            LinkButton linkProcess = e.Row.FindControl("linkProcess") as LinkButton;
            LinkButton linkLog = e.Row.FindControl("linkLog") as LinkButton;

            if (Convert.ToInt32(rowView["tskd_process"]) == 2)
            {
                linkProcess.Font.Italic = true;
                linkProcess.Style.Add("font-size", "11px");
                linkProcess.ForeColor = System.Drawing.Color.DarkBlue;
            }

            if (Convert.ToInt32(rowView["tskd_logging"]) == 2)
            {
                linkLog.Font.Italic = true;
                linkLog.Style.Add("font-size", "11px");
                linkLog.ForeColor = System.Drawing.Color.DarkBlue;
            }

            if (rowView["tskd_type"].ToString() == "维护")
            {
                linkProcess.Text = "<u>维护</u>";

                linkLog.Text = "<u>LOG</u>";
                linkLog.Enabled = false;
                linkLog.Font.Strikeout = true;
                linkLog.Style.Add("font-size", "12px");
                linkLog.Style.Add("font-weight", "normal");
                linkLog.Style.Add("color", "silver");
                linkLog.ToolTip = "维护类的任务，无需上传LOG！";
            }
            else if (rowView["tskd_type"].ToString() == "分析")
            {
                linkProcess.Text = "<u>分析</u>";

                linkLog.Text = "<u>LOG</u>";
                linkLog.Enabled = false;
                linkLog.Font.Strikeout = true;
                linkLog.Style.Add("font-size", "12px");
                linkLog.Style.Add("font-weight", "normal");
                linkLog.Style.Add("color", "silver");
                linkLog.ToolTip = "分析类的任务，无需上传LOG！";
            }
            else if (rowView["tskd_type"].ToString() == "测试")
            {
                linkProcess.Text = "<u>测试</u>";

                linkLog.Text = "<u>LOG</u>";
                linkLog.Enabled = false;
                linkLog.Font.Strikeout = true;
                linkLog.Style.Add("font-size", "12px");
                linkLog.Style.Add("font-weight", "normal");
                linkLog.Style.Add("color", "silver");
                linkLog.ToolTip = "测试类任务，无需上传LOG！";
            }
            else
            {
                if (Convert.ToInt32(rowView["tskd_logging"]) == 0)
                {
                    linkLog.Text = "<u>LOG</u>";
                    linkLog.Enabled = false;
                    linkLog.Style.Add("font-size", "12px");
                    linkLog.Style.Add("font-weight", "normal");
                    linkLog.Style.Add("color", "silver");

                    linkLog.ToolTip = "开发任务尚未完成！";
                }
            }
            #endregion
        }
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if ((e.CommandSource).GetType().Name == "LinkButton")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _id = gv.DataKeys[index].Values["tskd_id"].ToString();
            string _nbr = gv.DataKeys[index].Values["tskd_mstrNbr"].ToString();

            if (e.CommandName == "Process")
            {
                //如果是测试任务，则直接转到测试方案页面
                if (gv.DataKeys[index].Values["tskd_type"].ToString() == "测试")
                {
                    this.Redirect("TSK_Testing.aspx?cmd=tasklist&tskNbr=" + _nbr + "&rt=" + DateTime.Now.ToFileTime().ToString());
                }
                else
                {
                    this.Redirect("TSK_Solution.aspx?tskdID=" + _id + "&rt=" + DateTime.Now.ToFileTime().ToString());
                }
            }
            else if (e.CommandName == "Log")
            {
                this.Redirect("TSK_LoggingPre.aspx?tskNbr=" + _nbr + "&rt=" + DateTime.Now.ToFileTime().ToString());
            }
        }
        
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //毛病的方法
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
}