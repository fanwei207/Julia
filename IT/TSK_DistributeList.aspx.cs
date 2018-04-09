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

public partial class IT_TSK_DistributeList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCrtDate1.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

            BindUser();

            try
            {
                dropTracker.SelectedIndex = -1;
                dropStatus.SelectedIndex = -1;

                dropTracker.Items.FindByValue(Session["uID"].ToString()).Selected = true;
                dropStatus.Items.FindByValue("Disting").Selected = true;
            }
            catch { }

            #region 关闭Log后传回的参数
            if (!string.IsNullOrEmpty(Request.QueryString["tskNbr"]))
            {
                txtNbr.Text = Request.QueryString["tskNbr"];
            }

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
                try
                {
                    dropTracker.SelectedIndex = -1;
                    dropTracker.Items.FindByValue(Request.QueryString["usr"]).Selected = true;
                }
                catch { }
            }

            if (!string.IsNullOrEmpty(Request.QueryString["stat"]))
            {
                try
                {
                    dropStatus.SelectedIndex = -1;
                    dropStatus.Items.FindByValue(Request.QueryString["stat"]).Selected = true;
                }
                catch { }
            }
            #endregion

            BindGridView();
        }
    }
    protected override void BindGridView()
    {
        gv.DataSource = TaskHelper.SelectDistributeList(txtNbr.Text, txtCrtDate1.Text.Trim(), txtCrtDate2.Text.Trim(), dropStatus.SelectedValue
                                , dropTracker.SelectedValue, Session["uID"].ToString(), Session["uRole"].ToString());
        gv.DataBind();
    }
    protected void BindUser()
    {
        DataTable table = TaskHelper.GetUsers(string.Empty, 404);
        dropTracker.Items.Clear();

        dropTracker.DataSource = table;
        dropTracker.DataBind();
        dropTracker.Items.Insert(0, new ListItem("--全部--", "0"));
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
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if ((e.CommandSource).GetType().Name == "LinkButton")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _nbr = gv.DataKeys[index].Values["tsk_nbr"].ToString();

            if (e.CommandName == "Distribute")
            {
                this.Redirect("TSK_DistributeTask.aspx?from=dis&tskNbr=" + _nbr + "&rt=" + DateTime.Now.ToFileTime().ToString());
            }
            else if (e.CommandName == "Test")
            {
                this.Redirect("TSK_Testing.aspx?cmd=dislist&tskNbr=" + _nbr + "&rt=" + DateTime.Now.ToFileTime().ToString());
            }
            else if (e.CommandName == "myDelete")
            {

                if (((LinkButton)e.CommandSource).Text == "<u>日志</u>")
                {
                    ltlAlert.Text = "window.showModalDialog('TSK_ViewLog.aspx?tskNbr=" + _nbr + "&rt=" + DateTime.Now.ToString() + "', window, 'dialogHeight: 600px; dialogWidth: 700px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
                    ltlAlert.Text += "window.location.href = 'TSK_DistributeList.aspx?tskNbr=" + txtNbr.Text + "&date1=" + txtCrtDate1.Text + "&date2=" + txtCrtDate2.Text + "&usr=" + dropTracker.SelectedValue + "&stat=" + dropStatus.SelectedValue + "&rt=" + DateTime.Now.ToFileTime().ToString() + "'";
                }
                else
                {
                    if (!TaskHelper.DeleteTaskMstr(_nbr))
                    {
                        this.Alert("任务删除失败！");
                        return;
                    }

                    BindGridView();
                }
            }
            else if (e.CommandName == "myUpdate")
            {
                this.Redirect("TSK_Updating.aspx?tskNbr=" + _nbr + "&rt=" + DateTime.Now.ToFileTime().ToString());
            }
            else if (e.CommandName == "Track")
            {
                this.Redirect("TSK_Tracking.aspx?tskNbr=" + _nbr + "&rt=" + DateTime.Now.ToFileTime().ToString());
            }//CloseTaskMstr
            else if (e.CommandName == "Close")
            {
                if (TaskHelper.CouldTaskMstrClosed(_nbr))
                {
                    if (!TaskHelper.CloseTaskMstr(_nbr, Session["uID"].ToString(), Session["uName"].ToString()))
                    {
                        this.Alert("关闭操作失败！");
                    }
                    else
                    {
                        BindGridView();
                    }
                }
                else
                {
                    this.Alert("无法关闭！存在尚未完成的任务！");
                }
            }
            else if (e.CommandName == "myEdit")
            {
                this.Redirect("TSK_EditTask.aspx?tskNbr=" + _nbr);
            }
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            LinkButton linkDistribute = (LinkButton)e.Row.FindControl("linkDistribute");
            LinkButton linkTest = (LinkButton)e.Row.FindControl("linkTest");
            LinkButton linkUpdate = (LinkButton)e.Row.FindControl("linkUpdate");
            LinkButton linkTrack = (LinkButton)e.Row.FindControl("linkTrack");
            LinkButton linkDelete = (LinkButton)e.Row.FindControl("linkDelete");
            LinkButton linkClose = (LinkButton)e.Row.FindControl("linkClose");

            //已分配的项，不允许删除 tsk_isDistribute
            if (Convert.ToBoolean(rowView["tsk_isDistribute"]))
            {
                linkDelete.Text = "<u>日志</u>";
                //突出最新消息
                if (Convert.ToBoolean(rowView["tskf_isNew"]))
                {
                    linkDelete.ForeColor = System.Drawing.Color.Red;
                    linkDelete.Font.Bold = true;
                }

                linkDistribute.Font.Italic = true;
                linkDistribute.Style.Add("font-size", "11px");
                linkDistribute.ForeColor = System.Drawing.Color.DarkBlue;
            }

            if (Convert.ToInt32(rowView["tsk_testing"]) == 2)
            {
                linkTest.Font.Italic = true;
                linkTest.Style.Add("font-size", "11px");
                linkTest.ForeColor = System.Drawing.Color.DarkBlue;
            }

            if (Convert.ToInt32(rowView["tsk_updating"]) == 2)
            {
                linkUpdate.Font.Italic = true;
                linkUpdate.Style.Add("font-size", "11px");
                linkUpdate.ForeColor = System.Drawing.Color.DarkBlue;
            }

            if (Convert.ToInt32(rowView["tsk_tracking"]) == 2)
            {
                linkTrack.Font.Italic = true;
                linkTrack.Style.Add("font-size", "11px");
                linkTrack.ForeColor = System.Drawing.Color.DarkBlue;
            }

            if (!Convert.ToBoolean(rowView["tsk_isComplete"]))
            {
                //已完成：tsk_testing
                if (Convert.ToInt32(rowView["tsk_testing"]) == 0)
                {
                    linkTest.Enabled = false;
                    linkTest.Style.Add("font-size", "12px");
                    linkTest.Style.Add("font-weight", "normal");
                    linkTest.Style.Add("color", "silver");

                    linkTest.ToolTip = "该任务无需测试，或前置步骤尚未完成！";
                }

                //已完成：tsk_updating
                if (Convert.ToInt32(rowView["tsk_updating"]) == 0)
                {
                    linkUpdate.Enabled = false;
                    linkUpdate.Style.Add("font-size", "12px");
                    linkUpdate.Style.Add("font-weight", "normal");
                    linkUpdate.Style.Add("color", "silver");

                    linkUpdate.ToolTip = "该任务无需更新，或前置步骤尚未完成！";
                }

                //已完成：tsk_tracking
                if (Convert.ToInt32(rowView["tsk_tracking"]) == 0)
                {
                    linkTrack.Enabled = false;
                    linkTrack.Style.Add("font-size", "12px");
                    linkTrack.Style.Add("font-weight", "normal");
                    linkTrack.Style.Add("color", "silver");

                    linkTrack.ToolTip = "该任务前置步骤尚未完成！";
                }
            }
            else
            {
                //如果已完成的，则“分配”显示为“查看”
                linkDistribute.Text = "<u>分配</u>";
                linkUpdate.Text = "<u>更新</u>";
                linkTrack.Text = "<u>跟踪</u>";
                linkDelete.Text = "<u>日志</u>";

                linkClose.Enabled = false;
                linkClose.Style.Add("font-size", "12px");
                linkClose.Style.Add("font-weight", "normal");
                linkClose.Style.Add("color", "silver");
                linkClose.ToolTip = "该任务已经完成！";
                //已关闭的项目(tsk_isComplete = 1)，默认所有步骤均完成，变成蓝色斜体
                linkDistribute.ForeColor = System.Drawing.Color.DarkBlue;
                linkDistribute.Font.Italic = true;
                linkTest.ForeColor = System.Drawing.Color.DarkBlue;
                linkTest.Font.Italic = true;
                linkUpdate.ForeColor = System.Drawing.Color.DarkBlue;
                linkUpdate.Font.Italic = true;
                linkTrack.ForeColor = System.Drawing.Color.DarkBlue;
                linkTrack.Font.Italic = true;
            }
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
}