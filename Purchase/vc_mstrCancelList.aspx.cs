using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.Expressions;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;


public partial class Purchase_vc_mstrCancelList : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    String strConn2 = ConfigurationSettings.AppSettings["SqlConn.QAD_DATA"];
    adamClass chk = new adamClass();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("120000110", "赔付维护（修改）");
            this.Security.Register("120000220", "确认赔付，发送邮件权限");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql = "select vcc_id,vcc_name from qadplan.dbo.vc_cate";
            ddl_cate.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql);
            ddl_cate.DataBind();
            ddl_cate.Items.Insert(0, new ListItem("--", "0"));

            ddl_cate.SelectedIndex = 0;

            txtDate1.Text = DateTime.Now.ToString("yyyy-MM-01");
            txt_total.Text = Calculate();
            BindGridView();
        }
    }
    protected void btn_check_Click(object sender, EventArgs e)
    {
        BindGridView();
        txt_total.Text = Calculate();
    }
    protected string Calculate()
    {
        string sql = "sp_vc_calculateamount";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@vc_vend", txtVender.Text.Trim());
        param[1] = new SqlParameter("@vc_catename", ddl_cate.SelectedItem.ToString());
        if (txtDate1.Text.Trim().Length > 0)
            param[2] = new SqlParameter("@vc_date1", Convert.ToDateTime(txtDate1.Text.Trim()));
        else
            param[2] = new SqlParameter("@vc_date1", DateTime.Now.ToString("yyyy-MM-01"));
        if (txtDate2.Text.Trim().Length == 0)
            param[3] = new SqlParameter("@vc_date2", DateTime.Now.AddDays(1));
        else
            param[3] = new SqlParameter("@vc_date2", Convert.ToDateTime(txtDate2.Text.Trim()));
        param[4] = new SqlParameter("@vc_IsCancle", chk_IsCancle.Checked);

        return SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sql, param).ToString();
    }
    protected override void BindGridView()
    {
        if (String.IsNullOrEmpty(txtDate1.Text))
        {
            this.Alert("日期1 不能为空！");
            return;
        }
        else if (!this.IsDate(txtDate1.Text))
        {
            this.Alert("日期1 格式不正确！");
            return;
        }

        if (!String.IsNullOrEmpty(txtDate2.Text))
        {
            if (!this.IsDate(txtDate2.Text))
            {
                this.Alert("日期2 格式不正确！");
                return;
            }
        }

        string sql = "sp_vc_selectMstr";
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@vc_vend", txtVender.Text.Trim());
        param[1] = new SqlParameter("@vc_catename", ddl_cate.SelectedItem.ToString());
        if (txtDate1.Text.Trim().Length > 0)
            param[2] = new SqlParameter("@vc_date1", Convert.ToDateTime(txtDate1.Text.Trim()));
        else
            param[2] = new SqlParameter("@vc_date1", DateTime.Now.ToString("yyyy-MM-01"));
        if (txtDate2.Text.Trim().Length == 0)
            param[3] = new SqlParameter("@vc_date2", DateTime.Now.AddDays(1));
        else
            param[3] = new SqlParameter("@vc_date2", Convert.ToDateTime(txtDate2.Text.Trim()));
        param[4] = new SqlParameter("@vc_IsCancle", chk_IsCancle.Checked);
        param[5] = new SqlParameter("@vc_status", "-2");
        gv.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param);
        gv.DataBind();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "myEdit")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string vc_id = gv.DataKeys[intRow].Values["vc_id"].ToString();
            string isModify = "0";
            if (this.Security["120000110"].isValid) isModify = "1";
            this.Redirect("/Purchase/vc_mstr.aspx?vc_id=" + vc_id + "&isModify=" + isModify + "&vc_cancle=1");
        }
        else if (e.CommandName.ToString() == "Confirm")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string vc_id = gv.DataKeys[intRow].Values["vc_id"].ToString();

            string sql = "sp_vc_confirmInfo";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@vc_id", vc_id);
            param[1] = new SqlParameter("@uID", Convert.ToInt32(Session["uID"]));
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);

            //发送邮件

            string mailto = gv.DataKeys[intRow].Values["vc_email"].ToString();
            string mailSubject = "强凌 - " + gv.DataKeys[intRow].Values["vc_cateName"].ToString() + "通知";
            SendEmail(mailto, mailSubject
                , gv.DataKeys[intRow].Values["usr_companyName"].ToString()
                , gv.DataKeys[intRow].Values["vc_vend"].ToString()
                , gv.DataKeys[intRow].Values["vc_cateName"].ToString()
                , gv.DataKeys[intRow].Values["vc_amount"].ToString()
                , gv.DataKeys[intRow].Values["vc_id"].ToString());

            BindGridView();

        }
    }

    private void SendEmail(string mailto, string mailSubject, string companyName, string vend, string cateName, string amount, string vc_id)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<html>");
        sb.Append("<form>");
        sb.Append("<br>");
        sb.Append(companyName + "(" + vend + ")" + "<br>");
        sb.Append("    您好:" + "<br>");
        sb.Append("<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "在" + System.DateTime.Now + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "我司对贵公司 " + cateName + "罚款金额为" + amount + " ，" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "如有疑问请登陆我司--供应链管理系统<赔付明细>页面查看具体内容，并在48小时内联系我司相关人员，逾期将视为贵司默认，谢谢配合！ " + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "供应链管理系统："+baseDomain.getSupplierWebsite()+" " + "<br>");

        sb.Append("</body>");
        sb.Append("</form>");
        sb.Append("</html>");

        if (!this.SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), mailto, "", mailSubject, sb.ToString()))
        {
            this.Alert("邮件发送失败！");
        }
        else
        {
            VendCompHelper.MarkEmailSended(vc_id, Session["uID"].ToString(), Session["uName"].ToString());
            this.Alert("邮件发送成功！");
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int intRow = e.RowIndex;
        string vc_id = gv.DataKeys[intRow].Values["vc_id"].ToString();
        string sql = "sp_vc_cancelmstr";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@vc_id", vc_id);
        param[1] = new SqlParameter("@reValue", SqlDbType.Bit);
        param[1].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);
        if (Convert.ToInt32(param[1].Value) == 1)
        {
            ltlAlert.Text = "alert('取消申请成功！')";
        }
        else
        {
            ltlAlert.Text = "alert('取消申请失败！')";
        }
        BindGridView();
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataTable dt;
        string sql = "sp_vc_selectmstr";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@vc_vend", txtVender.Text.Trim());
        param[1] = new SqlParameter("@vc_catename", ddl_cate.SelectedItem.ToString());
        param[2] = new SqlParameter("@vc_date1", Convert.ToDateTime(txtDate1.Text.Trim()));
        if (txtDate2.Text.Trim().Length == 0)
            param[3] = new SqlParameter("@vc_date2", DateTime.Now.AddDays(1));
        else
            param[3] = new SqlParameter("@vc_date2", Convert.ToDateTime(txtDate2.Text.Trim()));
        param[4] = new SqlParameter("@vc_IsCancle", chk_IsCancle.Checked);

        dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param).Tables[0];

        if (dt.Rows.Count <= 0)
        {
            this.Alert("无所查询数据！");
            return;
        }
        string title = "100^<b>工厂</b>~^60^<b>地点</b>~^110^<b>供应商代码</b>~^150^<b>供应商名称</b>~^120^<b>科目</b>~^70^<b>状态</b>~^80^<b>金额</b>~^110^<b>比率</b>~^110^<b>日期</b>~^200^<b>备注</b>~^70^<b>提交者</b>~^70^<b>确认人</b>~^110^<b>确认日期</b>~^";
        this.ExportExcel(title, dt, false);
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!this.Security["120000110"].isValid)
            {
                e.Row.Cells[10].Text = "";
            }

            if (!this.Security["120000220"].isValid)
            {
                e.Row.Cells[11].Text = "";
                e.Row.Cells[13].Text = "";
            }
            else
            {
                if (gv.DataKeys[e.Row.RowIndex].Values["vc_confirmName"].ToString() == "")
                {
                    (e.Row.Cells[11].FindControl("btnConfirm") as LinkButton).Text = "Confirm";
                    e.Row.Cells[13].Text = "";
                }
                else
                {
                    try
                    {
                        e.Row.Cells[11].Text = gv.DataKeys[e.Row.RowIndex].Values["vc_confirmName"].ToString();
                    }
                    catch
                    { }
                }
            }
            e.Row.Cells[11].Text = "";
            if (!this.Security["120000250"].isValid)
            {
                e.Row.Cells[12].Text = "";
            }
            else if (gv.DataKeys[e.Row.RowIndex].Values["vc_status"].ToString() == "取消")
            {
                e.Row.Cells[12].Text = "已申请";
            }

            if (gv.DataKeys[e.Row.RowIndex].Values["vc_emailDate"].ToString() != "")
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(gv.DataKeys[e.Row.RowIndex].Values["vc_emailDate"].ToString());
                    e.Row.Cells[13].Text = dt.ToString("yyyy-MM-dd HH:mm");
                }
                catch
                { }
            }
            else
            {
                e.Row.Cells[13].Text = "";
            }
        }
    }
}