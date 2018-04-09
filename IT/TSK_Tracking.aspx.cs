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

public partial class TSK_Tracking : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbtskNbr.Text = Request.QueryString["tskNbr"];

            #region 根据任务明细，判断各按钮状态
            DataTable table = TaskHelper.SelectTaskMstrByNbr(lbtskNbr.Text);
            if (table == null || table.Rows.Count <= 0)
            {
                btnDone.Enabled = false;
                this.Alert("任务明细获取失败！请返回前一页！");
            }
            else
            {
                hidNbr.Value = table.Rows[0]["tsk_nbr"].ToString();
                hidDesc.Value = table.Rows[0]["tsk_desc"].ToString();
                hidApplyEmail.Value = table.Rows[0]["tsk_applyEmail"].ToString();
                hidTrackBy.Value = table.Rows[0]["tsk_trackBy"].ToString();
                hidTrackingEmailed.Value = table.Rows[0]["tsk_trackingEmailed"].ToString();

                if (!Convert.ToBoolean(table.Rows[0]["tsk_isComplete"]))
                {
                    if (hidTrackBy.Value != Session["uID"].ToString())
                    {
                        btnSave.Enabled = false;
                        btnDone.Enabled = false;
                        btnCancel.Enabled = false;
                        btnSave.ToolTip = "你不是该任务的跟踪人！";
                        btnDone.ToolTip = "你不是该任务的跟踪人！";
                        btnCancel.ToolTip = "你不是该任务的跟踪人！";
                    }
                }
                else
                {
                    btnSave.Enabled = false;
                    btnDone.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSave.ToolTip = "任务已经完成！";
                    btnDone.ToolTip = "任务已经完成！";
                    btnCancel.ToolTip = "任务已经完成！";
                }
            }
            #endregion

            BindData();
        }
    }

    protected void BindData()
    {
        divTrackingLog.InnerHtml = string.Empty;

        DataTable table = TaskHelper.SelectTaskTracking(lbtskNbr.Text);

        if (table == null)
        {
            btnDone.Enabled = false;
            this.Alert("拉取任务日志失败！请返回前一页面！");
        }
        else
        {
            foreach (DataRow row in table.Rows)
            {
                divTrackingLog.InnerHtml += "&nbsp;" + string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(row["tskt_createDate"])) + "&emsp;";
                divTrackingLog.InnerHtml += "&nbsp;" + row["tskt_createName"].ToString() + "<br />";
                divTrackingLog.InnerHtml += "&nbsp;消息：<br />";
                divTrackingLog.InnerHtml += "&nbsp;&nbsp;&nbsp;&nbsp;" + row["tskt_desc"].ToString() + "\n";
                divTrackingLog.InnerHtml += "<hr align=\"left\" style=\" width:90%; border-top:1px dashed #000; border-bottom:0px dashed #000; height:0px\"><br />";
            }
        }
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(divTrackingLog.InnerHtml))
        {
            this.Alert("截至目前为止，还没有跟踪内容！");
        }
        else
        {
            if (!TaskHelper.CompleteTaskTracking(lbtskNbr.Text, Session["uID"].ToString(), Session["uName"].ToString()))
            {
                this.Alert("操作失败！");
            }
            else
            {
                btnDone.Enabled = false;
                btnSave.Enabled = false;
                btnDone.ToolTip = "任务跟踪已经完成！";

                #region 发送邮件
                string _from = "isHelp@" +baseDomain.Domain[0];
                string _to = hidApplyEmail.Value;
                string _copy = "";

                string _subject = "技术支持答复";
                string _body = "<font style='font-size: 12px;'>你好：<br />";

                if (Convert.ToInt32(hidTrackingEmailed.Value) > 0)
                {
                    _body += "&nbsp;&nbsp;&nbsp;&nbsp;你提出的下列问题，我们进行了进一步的完善！请确认（任务号:" + Request.QueryString["tskNbr"] + "）：</font><br />";
                }
                else
                {
                    _body += "&nbsp;&nbsp;&nbsp;&nbsp;你提出的下列问题，我们已经处理完毕！请确认（任务号:" + Request.QueryString["tskNbr"] + "）：</font><br />";
                }

                _body += "------------------------<br />";
                _body += "<font style='font-size: 12px;'>";
                _body += hidDesc.Value;
                _body += "</font><br />";
                _body += "------------------------<br />";
                _body += "<br /><font style='font-size: 12px;'>信息部 " + string.Format("{0:yyyy-MM-dd HH:mm}", DateTime.Now) + "</font>";

                if (!string.IsNullOrEmpty(_to))
                {
                    //就在发送邮件失败的时候，通知，其他时候就不通知了
                    if (!this.SendEmail(_from, _to, _copy, _subject, _body))
                    {
                        this.Alert("回复申请人的邮件发送失败！请手工回复完成！");
                    }
                }
                else
                {
                    this.Alert("任务保存成功！但，申请人没有维护邮箱！");
                }
                #endregion
            }
        }
    }

    protected void txtBack_Click(object sender, EventArgs e)
    {
        this.Redirect("TSK_DistributeList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtTrackDesc.Text))
        {
            this.Alert("跟踪内容 不能为空！");
            return;
        }
        else if (txtTrackDesc.Text.Length < 10)
        {
            this.Alert("跟踪内容 至少10字以上！");
            return;
        }

        if (!TaskHelper.InsertTaskTracking(lbtskNbr.Text, txtTrackDesc.Text, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            this.Alert("跟踪日志 保存失败！");
        }
        else
        {
            BindData();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Redirect("TSK_TrackingRestart.aspx?tskNbr=" + lbtskNbr.Text + "&rt=" + DateTime.Now.ToFileTime().ToString());
    }
}