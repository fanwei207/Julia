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

public partial class TSK_Testing : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbtskNbr.Text = Request.QueryString["tskNbr"];

            #region 判断权限：责任人负责导入；更新人负责更新
            DataTable table = TaskHelper.SelectTaskMstrByNbr(lbtskNbr.Text);
            if (table == null || table.Rows.Count <= 0)
            {
                btnDone.Enabled = false;
                this.Alert("任务明细获取失败！请返回前一页！");
            }
            else
            {
                hidTrackEmail.Value = table.Rows[0]["tsk_trackEmail"].ToString();
               
                hidTestBy.Value = table.Rows[0]["tsk_testBy"].ToString();
                hidTestSecondBy.Value = table.Rows[0]["tsk_testSecondBy"].ToString();

                if (Convert.ToInt32(hidTestBy.Value) != Convert.ToInt32(Session["uID"])
                    && Convert.ToInt32(hidTestSecondBy.Value) != Convert.ToInt32(Session["uID"]))
                {
                    btnDone.Enabled = false;
                    btnDone.ToolTip = "你不是该任务的测试员！";
                }
            }
            #endregion

            BindData();
        }
    }
    protected void BindData()
    {
        DataTable table = TaskHelper.SelectTaskTesting(lbtskNbr.Text, chkNotTest.Checked);
        if (table == null)
        {
            btnDone.Enabled = false;
            this.Alert("测试数据拉取失败！请返回前一页面！");
        }
        else
        {
            if (table.Rows.Count == 0)
            {
                btnDone.Enabled = false;
                btnDone.ToolTip = "没有测试数据可供拉取！";
            }

            gv.DataSource = table;
            gv.DataBind();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cmd"] == "tasklist")
        {
            this.Redirect("TSK_TaskList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
        }
        else if (Request.QueryString["cmd"] == "dislist")
        {
            this.Redirect("TSK_DistributeList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
        string _id = gv.DataKeys[index].Values["tskr_id"].ToString();
        bool _isDet = Convert.ToBoolean(gv.DataKeys[index].Values["tskr_table"]);
        string _uID = Session["uID"].ToString();
        string _uName = Session["uName"].ToString();    
        
        if (e.CommandName == "Pass")
        {
            if (!TaskHelper.UpdateTaskTesting(_id, _isDet, "Pass", _uID, _uName))
            {
                this.Alert("通过操作失败！");
                return;
            }
        }
        else if (e.CommandName == "NotPass")
        {
            if (!TaskHelper.UpdateTaskTesting(_id, _isDet, "NotPass", _uID, _uName))
            {
                this.Alert("否决操作失败！");
                return;
            }
        }
        else if (e.CommandName == "New")
        {
            ltlAlert.Text = "window.showModalDialog('TSK_TestingDetail.aspx?tskdID=" + _id + "&rt=" + DateTime.Now.ToString() + "', window, 'dialogHeight: 350px; dialogWidth: 520px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
            ltlAlert.Text += "window.location.href = 'TSK_Testing.aspx?cmd=" + Request.QueryString["cmd"] + "&tskNbr=" + lbtskNbr.Text + "&rt=" + DateTime.Now.ToFileTime().ToString() + "'";
        }
        else if (e.CommandName == "Return")
        {
            if (TaskHelper.CancelTaskTesting(_id, Session["uID"].ToString(), Session["uName"].ToString()))
            {
                string hidDesc = gv.Rows[index].Cells[2].Text;


                #region 发送邮件通知责任人重新开发，并通知跟踪人跟进
                string _from = "isHelp@"+ baseDomain.Domain[0];
                string _to = gv.DataKeys[index].Values["tskr_chargeEmail"].ToString();
                string _copy = hidTrackEmail.Value;
                if (_to == _copy)
                {
                    _copy = string.Empty;
                }
                string _subject = "重新开发通知";
                string _body = "<font style='font-size: 12px;'>你好：<br />";
                _body += "&nbsp;&nbsp;&nbsp;&nbsp;下列任务测试未通过，已被要求重新开发(任务号：" + lbtskNbr.Text + ")：</font><br />";
                _body += "<br />";
                _body += "<font style='font-size: 12px;'>";
                _body += hidDesc;
                _body += "</font><br />";
                _body += "<br /><br />";
                _body += "<font style='font-size: 12px;'>信息部 " + string.Format("{0:yyyy-MM-dd HH:mm}", DateTime.Now) + "</font>";

                if (this.SendEmail(_from, _to, _copy, _subject, _body))
                {
                    this.Redirect("TSK_DistributeList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
                }
                else
                {
                    btnDone.Enabled = false;
                    btnDone.ToolTip = "已经退回，无法操作！";

                    this.Alert("保存成功！但，邮件发送失败！");
                }
                #endregion
            }
            else
            {
                this.Alert("退回操作失败！");
            }
        }
        else if (e.CommandName == "myDelete")
        {
            if (!TaskHelper.DeleteTaskTesting(_id, _uID, _uName))
            {
                this.Alert("删除操作失败！");
                return;
            }
        }

        BindData();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            LinkButton linkNew = (LinkButton)e.Row.FindControl("linkNew");
            LinkButton linkReturn = (LinkButton)e.Row.FindControl("linkReturn");

            if (!Convert.ToBoolean(rowView["tskr_table"]))
            {
                e.Row.Cells[0].Text = "-";
                e.Row.Cells[0].Style.Add("text-align", "right");

                linkReturn.Visible = false;

                linkNew.Text = "<u>删除</u>";
                linkNew.CommandName = "myDelete";

                //如果已做测试结果，则不许操作
                if (Convert.ToInt32(rowView["tskr_result"]) > 0)
                {
                    linkNew.Text = "--";
                    linkNew.Font.Underline = false;
                    linkNew.Enabled = false;
                    linkNew.Style.Add("color", "black");
                    linkNew.ToolTip = "已有测试结果的项禁止操作！";
                }
            }
            else
            {
                //如果已做测试结果，则不许操作
                if (Convert.ToInt32(rowView["tskr_result"]) <= 0)
                {
                    linkReturn.Enabled = false;
                    linkReturn.Style.Add("font-size", "12px");
                    linkReturn.Style.Add("font-weight", "normal");
                    linkReturn.Style.Add("color", "silver");

                    linkReturn.ToolTip = "无法退回！测试尚未进行！";
                }
            }
                 
            //测试结果：0 新建，1 待决，2 不通过，3 通过
            LinkButton linkNotPass = (LinkButton)e.Row.FindControl("linkNotPass");
            LinkButton linkPass = (LinkButton)e.Row.FindControl("linkPass");

            //整个任务完成时
            if (Convert.ToBoolean(gv.DataKeys[e.Row.RowIndex].Values["tskr_isCompleted"]))
            {
                linkNew.Enabled = false;
                linkNew.Style.Add("font-size", "12px");
                linkNew.Style.Add("font-weight", "normal");
                linkNew.Style.Add("color", "silver");
                linkNew.ToolTip = "任务已经完成！";

                linkReturn.Enabled = false;
                linkReturn.Style.Add("font-size", "12px");
                linkReturn.Style.Add("font-weight", "normal");
                linkReturn.Style.Add("color", "silver");
                linkReturn.ToolTip = "任务已经完成！";
            }

            //通过了，就不能点“通过了”；否决了，就不能点“否决”了

            //如果tskd_testing已经完成
            if (Convert.ToInt32(Session["uID"]) == Convert.ToInt32(hidTestBy.Value)
                || Convert.ToInt32(Session["uID"]) == Convert.ToInt32(hidTestSecondBy.Value)
                || Convert.ToInt32(Session["uRole"]) == 1)
            {
                //测试步骤完成时
                if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["tskr_testing"]) == 2)
                {
                    linkPass.Visible = false;
                    linkNotPass.Visible = false;

                    e.Row.Cells[5].Text = string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(gv.DataKeys[e.Row.RowIndex].Values["tskr_testDate"]));

                    linkNew.Enabled = false;
                    linkNew.Style.Add("font-size", "12px");
                    linkNew.Style.Add("font-weight", "normal");
                    linkNew.Style.Add("color", "silver");
                    linkNew.ToolTip = "任务已经完成！";

                    linkReturn.Enabled = false;
                    linkReturn.Style.Add("font-size", "12px");
                    linkReturn.Style.Add("font-weight", "normal");
                    linkReturn.Style.Add("color", "silver");
                    linkReturn.ToolTip = "任务已经完成！";
                }
            }
            else
            {
                linkPass.Text = "--";
                linkPass.Font.Underline = false;
                linkPass.Enabled = false;
                linkPass.Style.Add("color", "black");
                linkPass.ToolTip = "你不是该任务的测试员！";

                linkNotPass.Text = "--";
                linkNotPass.Font.Underline = false;
                linkNotPass.Enabled = false;
                linkNotPass.Style.Add("color", "black");
                linkNotPass.ToolTip = "你不是该任务的测试员！";

                linkNew.Enabled = false;
                linkNew.Style.Add("font-size", "12px");
                linkNew.Style.Add("font-weight", "normal");
                linkNew.Style.Add("color", "silver");
                linkNew.ToolTip = "你不是该任务的测试员！";

                linkReturn.Enabled = false;
                linkReturn.Style.Add("font-size", "12px");
                linkReturn.Style.Add("font-weight", "normal");
                linkReturn.Style.Add("color", "silver");
                linkReturn.ToolTip = "你不是该任务的测试员！";
            }
        }
    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        if (!chkNotTest.Checked)
        {
            this.Alert("仅在勾选 仅未测试 时才可提交！");
            return;
        }

        bool _canDone = true;
        string _detList = string.Empty;
        string _emailList = string.Empty;
        string _devpMsg = string.Empty;
        string _trackMsg = string.Empty;

        foreach (GridViewRow row in gv.Rows)
        {
            //只要主测试任务ID。只要主任务未测试，本次就不提交
            //if (Convert.ToBoolean(gv.DataKeys[row.RowIndex].Values["tskr_table"]))
            //{
                //未作任何动作的，直接跳出
                if (Convert.ToInt32(gv.DataKeys[row.RowIndex].Values["tskr_result"]) == 0)
                {
                    _canDone = false;
                    continue;
                }
                else
                {
                    foreach (GridViewRow r in gv.Rows)
                    {
                        if (!Convert.ToBoolean(gv.DataKeys[r.RowIndex].Values["tskr_table"])
                            && Convert.ToBoolean(gv.DataKeys[row.RowIndex].Values["tskr_id"]) == Convert.ToBoolean(gv.DataKeys[r.RowIndex].Values["tskr_detID"]))
                        {
                            if (Convert.ToInt32(gv.DataKeys[row.RowIndex].Values["tskr_result"]) == 0)
                            {
                                _canDone = false;
                                break;
                            }
                            else if (Convert.ToInt32(gv.DataKeys[row.RowIndex].Values["tskr_result"]) == 1)
                            {
                                //测试任务不论结果
                                if (gv.DataKeys[row.RowIndex].Values["tskr_type"].ToString() != "测试")
                                {
                                    _canDone = false;
                                    break;
                                }
                            }
                        }
                    }
                    //继续判断主任务。能走到这个步骤的，说明子任务都已检测通过
                    //测试任务不论结果，都要继续
                    if (gv.DataKeys[row.RowIndex].Values["tskr_type"].ToString() == "测试")
                    {
                        if (Convert.ToInt32(gv.DataKeys[row.RowIndex].Values["tskr_testing"]) < 2)
                        {
                            _detList += gv.DataKeys[row.RowIndex].Values["tskr_id"].ToString() + ";";
                            _trackMsg += "<br />";
                            _trackMsg += row.Cells[2].Text.Trim();
                            _trackMsg += "<br />";
                        }
                    }
                    else
                    {
                        //开发任务要必须全部通过
                        if (Convert.ToInt32(gv.DataKeys[row.RowIndex].Values["tskr_result"]) == 1)
                        {
                            _canDone = false;
                            break;
                        }
                        else
                        {
                            if (Convert.ToInt32(gv.DataKeys[row.RowIndex].Values["tskr_testing"]) < 2)
                            {
                                //通过时，开发任务要通知责任人，测试任务不需要
                                _detList += gv.DataKeys[row.RowIndex].Values["tskr_id"].ToString() + ";";

                                _emailList = _emailList.Replace(gv.DataKeys[row.RowIndex].Values["tskr_chargeEmail"].ToString() + ";", "");
                                _emailList += gv.DataKeys[row.RowIndex].Values["tskr_chargeEmail"].ToString() + ";";

                                _devpMsg += "<br />";
                                _devpMsg += row.Cells[2].Text.Trim();
                                _devpMsg += "<br />";
                            }
                        }
                    }
                }
           // }
           
        }
      
        if (!_canDone)
        {
            this.Alert("无法继续下一步！存在未通过的测试项！");
            BindData();
            return;
        }

        if (string.IsNullOrEmpty(_detList))
        {
            this.Alert("没有需要提交的任务！");
            BindData();
            return;
        }


           // return;
        if (!TaskHelper.CompleteTaskTesting(lbtskNbr.Text, _detList, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            this.Alert("操作失败！");
        }
        else
        {
            #region 发送邮件通知责任人上传Log，或跟踪人进行跟踪
            _emailList = _emailList.Replace(hidTrackEmail.Value + ";", "");
            string _from = "isHelp@" +baseDomain.Domain[0];
            string _to = hidTrackEmail.Value + ";" + _emailList;
            string _copy = "";

            string _subject = "测试完成通知";
            string _body = "<font style='font-size: 12px;'>你好：<br />";

            if (!string.IsNullOrEmpty(_devpMsg))
            {
                _devpMsg = "请及时上传下列任务的开发Log(任务号：" + lbtskNbr.Text + ")：<br />" + _devpMsg;
            }

            if (!string.IsNullOrEmpty(_trackMsg))
            {
                _trackMsg = "请及时跟踪下列任务(任务号：" + lbtskNbr.Text + ")：<br />" + _trackMsg;
            }

            _body += _devpMsg + "<br />" + _trackMsg;

            _body += "<br /></font>";
            _body += "<br /><br />";
            _body += "<font style='font-size: 12px;'>信息部 " + string.Format("{0:yyyy-MM-dd HH:mm}", DateTime.Now) + "</font>";

            if (this.SendEmail(_from, _to, _copy, _subject, _body))
            {
                this.Alert("测试已完成！系统已通知相关人员！请等待...！");
            }
            else
            {
                this.Alert("保存成功！但，邮件发送失败！");
            }
            #endregion

            BindData();
        }
    }
    protected void chkNotUpdate_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }
}