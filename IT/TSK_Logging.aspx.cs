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

public partial class TSK_Logging : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbTskNbr.Text = Request.QueryString["tskNbr"];

            #region 一定要保证这里是从前一个页面传过来的
            if (Request.QueryString["uID"] == null)
            {
                btnDone.Enabled = false;
                btnUpload.Enabled = false;

                this.Alert("非法页面1！请返回上一页面！");
                return;
            }
            else
            {
                if (Convert.ToInt32(Session["uID"].ToString()) != Convert.ToInt32(Request.QueryString["uID"]))
                {
                    btnDone.Enabled = false;
                    btnUpload.Enabled = false;

                    this.Alert("非法页面2！请返回上一页面！");
                    return;
                }
            }

            if (Request.QueryString["detlist"] == null)
            {
                btnDone.Enabled = false;
                btnUpload.Enabled = false;

                this.Alert("非法页面3！请返回上一页面！");
                return;
            }
        #endregion

            #region 判断权限：责任人负责导入；更新人负责更新
            DataTable table = TaskHelper.SelectTaskMstrByNbr(lbTskNbr.Text);
            if (table == null || table.Rows.Count <= 0)
            {
                btnDone.Enabled = false;
                btnUpload.Enabled = false;
                this.Alert("任务明细获取失败！请返回前一页！");
            }
            else
            {
                hidTUpdateEmail.Value = table.Rows[0]["tsk_updateEmail"].ToString();

                if (Convert.ToBoolean(table.Rows[0]["tsk_isComplete"]))
                {
                    btnUpload.Enabled = false;
                    btnDone.Enabled = false;
                    btnUpload.ToolTip = "任务已经完成！";
                    btnDone.ToolTip = "任务已经完成！";
                }
            }
            #endregion

            BindData();
        }
    }
    protected void BindData()
    {
        DataTable table = TaskHelper.SelectTaskLogging(lbTskNbr.Text, Session["uID"].ToString());

        if (table == null)
        {
            btnDone.Enabled = false;
            btnUpload.Enabled = false;

            this.Alert("获取数据失败！请返回上一页面！");
            return;
        }
        else
        {
            btnDone.Enabled = false;
            btnDone.ToolTip = "没有需要更新的Log！";

            //考虑到错传、漏传，故现允许在关闭之前重复上传。所以这里要判断是否有重新上传的Log
            foreach (DataRow row in table.Rows)
            {
                //只要存在一条需要更新的，就允许操作btnDone
                if (!Convert.ToBoolean(row["tskc_isTemp"]))
                {
                    btnDone.Enabled = true;
                    btnDone.ToolTip = "";
                }
            }

            gv.DataSource = table;
            gv.DataBind();
        }
    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        if (!TaskHelper.CompleteTaskLogging(lbTskNbr.Text, Request.QueryString["detlist"], Session["uID"].ToString(), Session["uName"].ToString()))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            #region 发送邮件通知跟踪
            string _from = "isHelp@" + baseDomain.Domain[0];
            string _to = hidTUpdateEmail.Value;
            string _copy = "";

            string _subject = "更新通知";
            string _body = "<font style='font-size: 12px;'>你好：<br />";
            _body += "&nbsp;&nbsp;&nbsp;&nbsp;已有任务上传了开发Log，请及时更新(" + lbTskNbr.Text + ")</font><br /><br />";
            _body += "<font style='font-size: 12px;'>信息部 " + string.Format("{0:yyyy-MM-dd HH:mm}", DateTime.Now) + "</font>";

            if (this.SendEmail(_from, _to, _copy, _subject, _body))
            {
                this.Alert("更新已完成！系统已通知更新！请等待...！");
            }
            else
            {
                this.Alert("保存成功！但，邮件发送失败！");
            }
            #endregion

            btnDone.Enabled = false;
            btnUpload.Enabled = false;
            btnDone.ToolTip = "上传已经完成！";
            btnUpload.ToolTip = "上传已经完成！";
        }
    }
    protected void txtBack_Click(object sender, EventArgs e)
    {
        this.Redirect("TSK_LoggingPre.aspx?tskNbr=" + lbTskNbr.Text + "&rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (filename.PostedFile.FileName == string.Empty)
        {
            this.Alert("选择要导入的文件");
            return;
        }
        else
        {
            string strFileName = filename.PostedFile.FileName.Trim();
            string strExt = strFileName.Substring(strFileName.LastIndexOf('.') + 1);

            if (strExt.ToUpper() != "XLS")
            {
                this.Alert("输入文件的格式不正确");
                return;
            }
            else
            {
                if (filename.PostedFile.ContentLength <= 0 || filename.PostedFile.ContentLength >= 5 * 1024 * 1024)
                {
                    this.Alert("输入文件的大小在5M以内");
                    return;
                }
            }
        }

        string strServerFile = Server.MapPath("/import/") + DateTime.Now.ToFileTime().ToString() + ".xls";
        try
        {
            filename.PostedFile.SaveAs(strServerFile);
        }
        catch
        {
            this.Alert("文件上传失败");
            return;
        }

        DataTable table;

        try
        {
            table = this.GetExcelContents(strServerFile);//adam.getExcelContents(strServerFile).Tables[0];
        }
        catch (Exception ee)
        {
            this.Alert("读取Excel文件失败");
            return;
        }

        if (table.Rows.Count <= 0)
        {
            this.Alert("没有可供读取的数据！");
            return;
        }
        else
        {
            if (table.Columns[0].ColumnName.Replace(" ", "").Trim().ToLower() != "操作" ||
                table.Columns[1].ColumnName.Replace(" ", "").Trim().ToLower() != "文件夹|数据库" ||
                table.Columns[2].ColumnName.Replace(" ", "").Trim().ToLower() != "名称")
            {
                this.Alert("上传的文件列名和模板不一致！");
                return;
            }
            else
            {
                table.Columns[0].ColumnName = "operateType";
                table.Columns[1].ColumnName = "chgInPath";
                table.Columns[2].ColumnName = "chgProgName";

                table.Columns.Add("detID");
                table.Columns.Add("chgType");
                table.Columns.Add("uID");
                table.Columns.Add("uName");
                table.Columns.Add("errMsg");
            }

            foreach (DataRow row in table.Rows)
            {
                row["detID"] = lbTskNbr.Text;
                row["uID"] = Session["uID"].ToString();
                row["uName"] = Session["uName"].ToString();
            }

            #region 批量导入
            if (TaskHelper.DeleteTaskChgTemp(Session["uID"].ToString()))
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(admClass.getConnectString("SqlConn.Conn_WF")))
                {
                    bulkCopy.DestinationTableName = "dbo.IT_TaskChgTemp";
                    //bulkCopy.ColumnMappings.Add("detID", "tskt_detID");
                    bulkCopy.ColumnMappings.Add("chgType", "tskt_chgType");
                    bulkCopy.ColumnMappings.Add("operateType", "tskt_operateType");
                    bulkCopy.ColumnMappings.Add("chgInPath", "tskt_chgInPath");
                    bulkCopy.ColumnMappings.Add("chgProgName", "tskt_chgProgName");
                    bulkCopy.ColumnMappings.Add("uID", "tskt_uploadBy");
                    bulkCopy.ColumnMappings.Add("uName", "tskt_uploadName");
                    bulkCopy.ColumnMappings.Add("errMsg", "tskt_errMsg");

                    try
                    {
                        bulkCopy.WriteToServer(table);
                        //系统认为，在上传Log的时候，表示更新任务的开始
                        table = TaskHelper.SelectBatchTaskChgTemp(lbTskNbr.Text, Session["uID"].ToString(), Session["uName"].ToString());
                        if (table != null)
                        {
                            if (table.Rows.Count != 0)
                            {
                                string EXTitle = "<b>序号</b>~^<b>类别</b>~^<b>操作</b>~^<b>文件夹|数据库</b>~^<b>名称</b>~^<b>错误信息</b>~^";
                                this.ExportExcel(EXTitle, table, false);
                            }
                            else
                            {
                                btnDone.Enabled = true;
                                btnDone.ToolTip = "";
                                this.Alert("更新日志导入成功！");
                                BindData();
                            }

                            table.Dispose();
                        }
                        else
                        {
                            this.Alert("导入时出错，请联系系统管理员2！");
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Alert("导入时出错，请联系系统管理员3！");
                    }
                }
            }
            else
            {
                this.Alert("删除临时表失败，请联系系统管理员2！"); 
            }
            #endregion
        }
    }
    protected void linkDownload_Click(object sender, EventArgs e)
    {
        DataTable table = TaskHelper.SelectTaskLogging(lbTskNbr.Text, Session["uID"].ToString());

        if (table == null || table.Rows.Count == 0)
        {
            this.Alert("没有数据可供导出！");
        }
        else
        {
            string EXTitle = "<b>序号</b>~^<b>类别</b>~^<b>操作</b>~^<b>文件夹|数据库</b>~^<b>名称</b>~^<b>上传时间</b>~^<b>系统时间</b>~^";
            this.ExportExcel(EXTitle, table, true); 
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            if (!Convert.ToBoolean(rowView["tskc_isTemp"]))
            {
                e.Row.Cells[6].Font.Italic = true;
                e.Row.Cells[6].Font.Strikeout = true;
            }
        }
    }
}