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

public partial class TSK_Solution : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidTskdID.Value = Request.QueryString["tskdID"];

            #region 判断各按钮状态，及初始化信息
            DataTable table = TaskHelper.SelectTaskDetById(hidTskdID.Value);
            if (table == null || table.Rows.Count <= 0)
            {
                btnSave.Enabled = false;
                btnDone.Enabled = false;
                this.Alert("任务明细获取失败！请返回前一页！");
            }
            else
            {
                hidNbr.Value = table.Rows[0]["tskd_mstrNbr"].ToString();
                hidDesc.Value = table.Rows[0]["tskd_desc"].ToString();
                hidType.Value = table.Rows[0]["tskd_type"].ToString();
                hidTrackEmail.Value = table.Rows[0]["tsk_trackEmail"].ToString();
                hidTestEmail.Value = table.Rows[0]["tsk_testEmail"].ToString();
                hidTestSecondEmail.Value = table.Rows[0]["tsk_testSecondEmail"].ToString();

                //维护、分析类任务，直接提交跟踪
                if (hidType.Value == "维护" || hidType.Value == "分析")
                {
                    btnDone.Text = "TO TRACK";
                }

                if (Convert.ToInt32(table.Rows[0]["tskd_process"]) == 2)
                {
                    btnSave.Enabled = false;
                    btnDone.Enabled = false;
                    btnSave.ToolTip = "开发已经完成！";
                    btnDone.ToolTip = "开发已经完成！";
                }
                else
                {
                    if (Convert.ToBoolean(table.Rows[0]["tskd_isCanceled"]))
                    {
                        btnSave.Enabled = false;
                        btnDone.Enabled = false;
                        btnSave.ToolTip = "任务已被取消！";
                        btnDone.ToolTip = "任务已被取消！";
                    }
                    else if (Convert.ToInt32(table.Rows[0]["tskd_chargeBy"]) != Convert.ToInt32(Session["uID"]))
                    {
                        if (Convert.ToInt32(table.Rows[0]["tsk_trackBy"]) != Convert.ToInt32(Session["uID"]))
                        {
                            btnSave.Enabled = false;
                            btnDone.Enabled = false;
                            btnSave.ToolTip = "你不是该任务的负责人！";
                            btnDone.ToolTip = "你不是该任务的负责人！";
                        }
                    }
                }
            }
            #endregion

            BindData();
            
        }
        txtDemand.Focus();
    }
    protected void BindData()
    {
        //divDevpLog.InnerHtml = string.Empty;

        gv.DataSource = TaskHelper.SelectTaskDevping(hidTskdID.Value);
        gv.DataBind();
           
        
    }
    protected void txtBack_Click(object sender, EventArgs e)
    {
        this.Redirect("TSK_TaskList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtDemand.Text))
        {
            this.Alert("需求分析不能为空！");
            return;
        }
        else if (txtDemand.Text.Length < 10)
        {
            this.Alert("需求分析 至少10字以上！");
            return;
        }
        int stu;

        string status = txtDemand.Text.Trim().Substring(0, 2);
        if (status == "正在")
        {
            stu = 0;
        }
        else if (status == "请问")
        {
            stu = 1;
        }
        else if (status == "完成")
        {
            stu = 2;
        }
        else if (status == "目标")
        {
            stu = 3;
        }
        else
        {
            this.Alert("每条日志的开头，必须是“完成”、“正在”“请问”！");
            return;
        }
        string demand = txtDemand.Text.Trim().Substring(2);
        if (!TaskHelper.InsertTaskDevping(hidTskdID.Value, demand, Session["uID"].ToString(), Session["uName"].ToString(),stu.ToString()))
        {
            this.Alert("保存失败！");
            return;
        }
        else
        {
            txtDemand.Text = string.Empty;

            BindData();
        }
    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        

        if (!TaskHelper.CompleteTaskDevping(hidNbr.Value, hidTskdID.Value, hidType.Value, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            this.Alert("操作失败！");
        }
        else
        {
            #region 发送邮件通知（通知测试，或通知跟踪）
            string _from = "isHelp@" +baseDomain.Domain[0];
            string _to = hidTestEmail.Value + ";" + hidTestSecondEmail.Value;
            string _copy = hidTrackEmail.Value;
            //如果跟踪人包含在测试人员中，则无需抄送
            if (_to.IndexOf(_copy) >= 0)
            {
                _copy = string.Empty;
            }

            string _subject = "测试通知";

            if (hidType.Value == "维护" || hidType.Value == "分析")
            {
                _to = hidTrackEmail.Value;
                _copy = "";
                _subject = "跟踪通知";
            }

            string _body = "<font style='font-size: 12px;'>你好：<br />";
            if (hidType.Value == "维护" || hidType.Value == "分析")
            {
                _body += "&nbsp;&nbsp;&nbsp;&nbsp;请及时对下列任务进行跟踪(任务号：" + hidNbr.Value + ")：</font><br />"; 
            }
            else
            {
                _body += "&nbsp;&nbsp;&nbsp;&nbsp;请及时对下列任务进行测试(任务号：" + hidNbr.Value + ")：</font><br />";
            }

            _body += "<br />";
            _body += "<font style='font-size: 12px;'>";
            _body += hidDesc.Value;
            _body += "</font><br />";
            _body += "<br /><br />";
            _body += "<font style='font-size: 12px;'>信息部 " + string.Format("{0:yyyy-MM-dd HH:mm}", DateTime.Now) + "</font>";

            if (this.SendEmail(_from, _to, _copy, _subject, _body))
            {
                if (hidType.Value == "维护" || hidType.Value == "分析")
                {
                    this.Alert("开发已完成！系统已通知跟踪人！请等待...！");
                }
                else
                {
                    this.Alert("开发已完成！系统已通知测试员！请等待...！");
                }
            }
            else
            {
                this.Alert("保存成功！但，邮件发送失败！");
            }
            #endregion

            btnSave.Enabled = false;
            btnDone.Enabled = false;
            btnSave.ToolTip = "开发已经完成！";
            btnDone.ToolTip = "开发已经完成！";
        }
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "finish")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            int fsId = Convert.ToInt32(gv.DataKeys[index].Values["tskv_id"].ToString());
            TaskHelper.setkfinish(fsId.ToString());
            BindData();
        }
    }
}