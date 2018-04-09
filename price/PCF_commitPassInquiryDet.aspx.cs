using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class price_PCF_commitPassInquiryDet : BasePage
{
    PCF_helper helper = new PCF_helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindGv();
        }
    }

    private void bindGv()
    {
        string part = txtPart.Text.Trim();
        string vender = txtVender.Text.Trim();
        string venderName = txtVenderName.Text.Trim();

        gv.DataSource = helper.selectPassInquiryDet(part, vender, venderName);
        gv.DataBind();

    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGv();
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {

        DataTable TempTable = new DataTable("gvTable");
        DataColumn TempColumn;
        DataRow TempRow;


        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "PCF_ID";
        TempTable.Columns.Add(TempColumn);


        string IMID = string.Empty;
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            if (((CheckBox)(gv.Rows[i].Cells[0].FindControl("chk"))).Checked)
            {
                TempRow = TempTable.NewRow();
                TempRow["PCF_ID"] = gv.DataKeys[i].Values["PCF_ID"].ToString();

                TempTable.Rows.Add(TempRow);
            }
        }
        if (TempTable.Rows.Count <= 0)
        {
            ltlAlert.Text = "alert('您未选中提交的项目');";
            return;
        }
        string mailTo = string.Empty;
        DataTable flag =  helper.commitPassInquiryToCheck(TempTable, Session["uID"].ToString(), Session["uName"].ToString(), out mailTo);
        if (flag.Rows[0][0].ToString().Equals("1"))
        {
            ltlAlert.Text = "alert('提交成功');";



            string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
            string to = mailTo;
            string copy = string.Empty;
            string subject = "零星采购有新的申请，请您审批";
            string body = "";

            body += "<font style='font-size: 12px;'>请尽快处理。</font><br />";

            //if (!this.SendEmail(from, to, copy, subject, body))
            //{
            //    this.ltlAlert.Text = "alert('邮件发送失败');";
            //}
            //else
            //{
            //    this.ltlAlert.Text = "alert('邮件发送成功');";
            //}

            bindGv();

        }
        else if (flag.Rows[0][0].ToString().Equals("0"))
        {
            ltlAlert.Text = "alert('提交失败，请联系管理员');";
       
        }
        else
        {
            string title = "100^<b>QAD</b>~^100^<b>供应商</b>~^100^<b>采购单位</b>~^100^<b>基础单位</b>~^";
            ExportExcel(title, flag, false);
        }
    }
}