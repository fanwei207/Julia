using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class part_Npart_failApplyPage : BasePage
{
    Npart_help help = new Npart_help();
    private NewWorkflow workFlowHelper = new NewWorkflow();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnfail_Click(object sender, EventArgs e)
    {
        if (hidflag.Value == "1")
        {
            Alert("已经拒绝，请点击右上角的小叉关闭页面");
            return;
        }
        if (txtPending.Text.Trim().Equals(string.Empty))
        {
            Alert("拒绝原因不能为空");
            return;
        }

        string nodeID = Request.QueryString["nodeId"].ToString();
        string idList = Request.QueryString["idList"].ToString();
        string uID = Session["uID"].ToString();
        string uName = Session["uName"].ToString();
        string reason = txtPending.Text.Trim();
        string message = string.Empty;
        string[] indexes = idList.Substring(1, idList.Length -1).Split(new char[] { ';' });

        DataTable table = new DataTable("TempTable");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "sourceID";
        table.Columns.Add(column);



        foreach (string  id in indexes)
        {
            
            row = table.NewRow();
            row["sourceID"] = id;
            table.Rows.Add(row);
            
        }



        int result = workFlowHelper.FailForUrl(nodeID, uID, table, out message);
        if (result == 1)
        {

            #region 发送邮件

            string Fail = "fail";

            string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
            string to = help.selectMailTo(table,Fail);
            string copy = "";
            string subject = "您申请的物料号被拒绝";
            string body = "";
            #region 写Body
            body += "<font style='font-size: 12px;'>请到申请页面查看原因</font><br />";

            body += "<font style='font-size: 12px;'>详情请登陆 " + baseDomain.getPortalWebsite() + " </font><br />";
            body += "<font style='font-size: 12px;'>For details please visit " + baseDomain.getPortalWebsite() + " </font>";
            #endregion
            if (!this.SendEmail(from, to, copy, subject, body))
            {
                this.ltlAlert.Text = "alert('Email sending failure');";
            }
            else
            {
                this.ltlAlert.Text = "alert('Email sending');";
            }
            #endregion

            help.updateApplyFail(table, reason, uID, uName);
            ltlAlert.Text = "alert('拒绝成功！');";
            hidflag.Value = "1";
            
        }
        else if (result == -1)
        {
            ltlAlert.Text = "alert('" + message + "');";
        }
        else
        {
            ltlAlert.Text = "alert('操作失败,请联系管理员！');";
        }

    }
}