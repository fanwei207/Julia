using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;
using IT;

public partial class TSK_TrackingRestart : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 绑定任务明细
            dropTaskDet.Items.Clear();
            dropTaskDet.Items.Add(new ListItem("--请选择一个任务--", "0"));

            DataTable table = TaskHelper.SelectTaskDet(Request.QueryString["tskNbr"]);

            if (table == null)
            {
                this.Alert("获取任务明细失败！请返回上一页面！");
                btnAggree.Enabled = false;
                return;
            }
            else if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    dropTaskDet.Items.Add(new ListItem(row["tskd_index"].ToString() + "、" + row["tskd_desc"].ToString(), row["tskd_id"].ToString()));
                }
            }

            #endregion
        }
    }
    protected void dropTaskDet_SelectedIndexChanged(object sender, EventArgs e)
    {
        radlType.Items[0].Enabled = false;
        radlType.Items[1].Enabled = false;

        if (dropTaskDet.SelectedIndex != 0)
        {
            DataTable table = TaskHelper.SelectTaskDetById(dropTaskDet.SelectedValue);

            if (table == null || table.Rows.Count == 0)
            {
                dropTaskDet.SelectedIndex = -1;
                this.Alert("获取任务明细的时候发生异常！");
                return;
            }
            else
            {
                hidTestEmail.Value = table.Rows[0]["tsk_testEmail"].ToString() + ";" + table.Rows[0]["tsk_testSecondEmail"].ToString();
                hidChargeEmail.Value = table.Rows[0]["tskd_chargeEmail"].ToString();

                if (Convert.ToBoolean(table.Rows[0]["tskd_isCompleted"]))
                {
                    if (table.Rows[0]["tskd_type"].ToString() == "测试")
                    {
                        radlType.Items[0].Selected = false;
                        radlType.Items[1].Selected = true;
                        radlType.Items[0].Text = "开发";
                        radlType.Items[1].Enabled = true;

                    }
                    else if (table.Rows[0]["tskd_type"].ToString() == "维护" || table.Rows[0]["tskd_type"].ToString() == "维护")
                    {
                        radlType.Items[1].Selected = false;
                        radlType.Items[0].Selected = true;
                        radlType.Items[0].Text = table.Rows[0]["tskd_type"].ToString();
                        radlType.Items[0].Enabled = true;
                    }
                    else
                    {
                        radlType.Items[1].Selected = false;
                        radlType.Items[0].Selected = false;
                        radlType.Items[0].Text = "开发";
                        radlType.Items[0].Enabled = true;
                        radlType.Items[1].Enabled = true;
                    }
                }
                else
                {
                    dropTaskDet.SelectedIndex = -1;
                    this.Alert("未完成的任务，无法取消！");
                    return;
                }
            }
        }
        else
        {
            radlType.Items[0].Selected = false;
            radlType.Items[1].Selected = false;
        }
    }
    protected void btnAggree_Click(object sender, EventArgs e)
    {
        #region 验证
        if (dropTaskDet.SelectedIndex == 0)
        {
            this.Alert("请先选择一个任务！");
            return;
        }

        if (radlType.SelectedIndex == -1)
        {
            this.Alert("请确定需要重启的步骤！");
            return;
        }

        if (string.IsNullOrEmpty(txtReason.Text))
        {
            this.Alert("重启理由不能为空！");
            return;
        }
        else if (txtReason.Text.Length < 10)
        {
            this.Alert("重启理由必须在10字以上！");
            return;
        }

        if (string.IsNullOrEmpty(hidChargeEmail.Value) || string.IsNullOrEmpty(hidTestEmail.Value))
        {
            this.Alert("责任人或测试员邮箱获取失败！无法继续！");
            return;
        }
        #endregion
        /*
         * 测试：直接退回重新测试
         * 维护、分析：直接退回重新维护、分析
         * 开发：可以退回测试；一旦退回开发，直接取消测试结果
         */
        string _mstrNbr = Request.QueryString["tskNbr"];
        if (!TaskHelper.RestartTaskDet(_mstrNbr, dropTaskDet.SelectedValue, radlType.SelectedValue, txtReason.Text, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            this.Alert("重启失败！");
        }
        else
        {
            #region 发送邮件
            string _from = "isHelp@" +baseDomain.Domain[0];
            string _to = hidChargeEmail.Value;
            string _copy = "";

            if (radlType.SelectedValue == "测试")
            {
                _to = hidTestEmail.Value;
            }

            string _subject = "重启任务";
            string _body = "<font style='font-size: 12px;'>你好：<br />";

            _body += "&nbsp;&nbsp;&nbsp;&nbsp;下列任务被要求重新开始" + radlType.SelectedValue + "（任务号:" + Request.QueryString["tskNbr"] + "）：</font><br />";

            _body += "------------------------<br />";
            _body += "<font style='font-size: 12px;'>";
            _body += dropTaskDet.SelectedItem.Text;
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
    protected void txtBack_Click(object sender, EventArgs e)
    {
        this.Redirect("TSK_Tracking.aspx?tskNbr=" + Request.QueryString["tskNbr"] + "&rt=" + DateTime.Now.ToFileTime().ToString());
    }
}