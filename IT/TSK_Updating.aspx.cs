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
using adamFuncs;
using CommClass;

public partial class TSK_Updating : BasePage
{
    adamClass adam = new adamClass();

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
                hidDesc.Value = table.Rows[0]["tsk_desc"].ToString();
                hidTrackEmail.Value = table.Rows[0]["tsk_trackEmail"].ToString();
                hidUpdateBy.Value = table.Rows[0]["tsk_updateBy"].ToString();
                chkUpdating.Checked = Convert.ToInt32(table.Rows[0]["tsk_updating"]) == 2 ? true : false;

                if (Convert.ToInt32(table.Rows[0]["tsk_updating"]) == 2)
                {
                    btnDone.Enabled = false;
                    btnDone.ToolTip = "更新已经完成！";
                }
                else
                {
                    if (Convert.ToInt32(hidUpdateBy.Value) != Convert.ToInt32(Session["uID"]))
                    {
                        btnDone.Enabled = false;
                        btnDone.ToolTip = "你不是该任务的更新人！";
                    }
                }
            }
            #endregion

            BindData();
        }
    }
    protected void BindData()
    {
        DataTable table = TaskHelper.SelectTaskUpdating(lbtskNbr.Text, chkNotUpdate.Checked);

        if (table == null)
        {
            btnDone.Enabled = false;
            this.Alert("更新Log拉取失败！请返回前一页面！");
        }
        else
        {
            if (table.Rows.Count == 0)
            {
                btnDone.Enabled = false;
                btnDone.ToolTip = "没有可供拉取的更新数据！";
            }

            if (chkNotUpdate.Checked)
            {
                //拿文件的更新时间
                foreach (DataRow row in table.Rows)
                {
                    //程序和文件，获取程序的修改日期
                    if (row["tskc_chgType"].ToString() == "程序" || row["tskc_chgType"].ToString() == "文件")
                    {
                        //非JULIA的要用绝对路径 tsk_sys
                        string _filePath = string.Empty;

                        if (row["tsk_sys"].ToString().ToUpper() == "JULIA")
                        {
                            _filePath = Server.MapPath("/" + row["tskc_chgInPath"].ToString() + "/") + row["tskc_chgProgName"].ToString();
                        }
                        else if (row["tsk_sys"].ToString().ToUpper() == "ANGELA")
                        {
                            _filePath = "D:\\tcpcnew\\Angela\\"+ row["tskc_chgInPath"].ToString() + "\\" + row["tskc_chgProgName"].ToString();
                        }

                        if (string.IsNullOrEmpty(row["tskc_chgSysTime"].ToString()))
                        {
                            if (File.Exists(_filePath))
                            {
                                FileInfo file = new FileInfo(_filePath);
                                row["tskc_chgSysTime"] = file.LastWriteTime;
                            }
                        }
                    }

                    //上传时间大于更新时间的，认为未更新 tskc_isUpdated
                    //判断此时间，无意义了 Add By Shanzm 2014-09-15
                    try
                    {
                        if (Convert.ToDateTime(row["tskc_uploadTime"]) >= Convert.ToDateTime(row["tskc_chgSysTime"]))
                        {
                            row["tskc_isUpdated"] = true;
                        }
                        else
                        {
                            row["tskc_isUpdated"] = true;
                        }
                    }
                    catch { }
                }
            }

            gv.DataSource = table;
            gv.DataBind();
        }
    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        if (!chkNotUpdate.Checked)
        {
            this.Alert("已经更新好的Log，不能重复操作！");
            return;
        }

        //如果存在获取不到的系统时间，则无法完成
        bool bCanUpdate = true;

        //先将时间保存，通过批量上传的方式
        DataTable table = new DataTable();
        table.Columns.Add("chgID");
        table.Columns.Add("chgSysTime");
        table.Columns.Add("uID");

        foreach (GridViewRow row in gv.Rows)
        {
            DataRow r = table.NewRow();

            r["chgID"] = gv.DataKeys[row.RowIndex].Values["tskc_id"].ToString();
            r["chgSysTime"] = gv.DataKeys[row.RowIndex].Values["tskc_chgSysTime"].ToString();
            r["uID"] = Session["uID"].ToString();

            table.Rows.Add(r);

            if (string.IsNullOrEmpty(gv.DataKeys[row.RowIndex].Values["tskc_chgSysTime"].ToString()))
            {
                bCanUpdate = false;
            }
        }

        if (!bCanUpdate)
        {
            this.Alert("存在未得到更新的Log，无法完成！");
            return;
        }

        if (TaskHelper.DeleteTaskChgTemp(Session["uID"].ToString()))
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(admClass.getConnectString("SqlConn.Conn_WF")))
            {
                bulkCopy.DestinationTableName = "dbo.IT_TaskChgTemp";
                bulkCopy.ColumnMappings.Add("chgID", "tskt_detID");
                bulkCopy.ColumnMappings.Add("chgSysTime", "tskt_chgSysTime");
                bulkCopy.ColumnMappings.Add("uID", "tskt_uploadBy");

                try
                {
                    bulkCopy.WriteToServer(table);
                    //系统认为，在上传Log的时候，表示更新任务的开始
                    if (!TaskHelper.CompleteTaskUpdating(lbtskNbr.Text, Session["uID"].ToString(), Session["uName"].ToString()))
                    {
                        this.Alert("处理更新时失败！");
                    }
                    else
                    {
                        #region 发送邮件通知跟踪
                        string _from = "isHelp@" +baseDomain.Domain[0];
                        string _to = hidTrackEmail.Value;
                        string _copy = "";

                        string _subject = "跟踪通知";
                        string _body = "<font style='font-size: 12px;'>你好：<br />";
                        _body += "&nbsp;&nbsp;&nbsp;&nbsp;下列任务已更新完成，请及时跟踪(任务号：" + lbtskNbr.Text + ")：</font><br />";
                        _body += "<br /><font style='font-size: 12px;'>";
                        _body += hidDesc.Value;
                        _body += "</font><br /><br />";
                        _body += "<font style='font-size: 12px;'>信息部 " + string.Format("{0:yyyy-MM-dd HH:mm}", DateTime.Now) + "</font>";

                        if (this.SendEmail(_from, _to, _copy, _subject, _body))
                        {
                            this.Alert("更新已完成！系统已通知跟踪！请等待...！");
                        }
                        else
                        {
                            this.Alert("保存成功！但，邮件发送失败！");
                        }
                        #endregion

                        btnDone.Enabled = false;
                        chkUpdating.Checked = true;
                        btnDone.ToolTip = "更新已经完成！";
                    }
                }
                catch (Exception ex)
                {
                    this.Alert("导入时出错，请联系系统管理员1！");
                }
                finally
                {
                    table.Dispose();
                }
            }
        }
        else
        {
            this.Alert("删除临时表失败，请联系系统管理员1！");
        }
    }
    protected void txtBack_Click(object sender, EventArgs e)
    {
        this.Redirect("TSK_DistributeList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            LinkButton linkDelete = (LinkButton)e.Row.FindControl("linkDelete");

            if (chkUpdating.Checked)
            {
                linkDelete.Enabled = false;
                linkDelete.Style.Add("font-size", "12px");
                linkDelete.Style.Add("font-weight", "normal");
                linkDelete.Style.Add("color", "silver");
                linkDelete.ToolTip = "已经完成，不能删除！";
            }
            else
            {
                if (Convert.ToInt32(hidUpdateBy.Value) != Convert.ToInt32(Session["uID"]))
                {
                    linkDelete.Enabled = false;
                    linkDelete.Style.Add("font-size", "12px");
                    linkDelete.Style.Add("font-weight", "normal");
                    linkDelete.Style.Add("color", "silver");
                    linkDelete.ToolTip = "你不是该任务的更新人！";
                }
            }
        }
    }
    protected void chkNotUpdate_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
        string _id = gv.DataKeys[index].Values["tskc_id"].ToString();
        string _uID = Session["uID"].ToString();
        string _uName = Session["uName"].ToString();

        if (e.CommandName == "myDelete")
        {
            if (!TaskHelper.DeleteTaskUpdating(_id, _uID, _uName))
            {
                this.Alert("删除失败！");
            }
        }

        BindData();
    }
}