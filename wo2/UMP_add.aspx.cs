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
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using RD_WorkFlow;
using System.Net.Mail;
using System.Text;
using System.IO;
using adamFuncs;
using CommClass;


public partial class wo2_UMP_add : BasePage
{
    private adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getmstrdetil();
            if (Request.QueryString["mid"] != null)
            {
               
                lbl_id.Text = Request.QueryString["mid"];
                BindMstrData();
                if (Request.QueryString["iApprove"].ToString() == "0")
                {
                    btn_submit.Visible = false;
                    btn_approve.Visible = false;
                    btn_diaApp.Visible = false;
                    btnDoc.Visible = false;
                    BtnAdd.Visible = false;
                    appv.Visible = false;

                }
                else
                {
                    //权限
                    btn_approve.Visible = this.Security["630090003"].isValid;
                    if (this.Security["630090003"].isValid == true)
                    {
                        btnDoc.Visible = true;
                    }
                }
                

            }
            else
            {
                BtnAdd.Visible = false;
                btn_submit.Visible = false;
                btn_approve.Visible = false;
                btn_diaApp.Visible = false;
                appv.Visible = false;
            }
        }
    }
    public void getmstrdetil()
    {
        det.Visible = false;
        string strSQL = "SELECT qad_site,realdomain FROM dbo.Domain_Mes ";
        ddlsite.DataSource = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSQL);
        ddlsite.DataBind();

         strSQL = " SELECT UMP_typeid,UMP_typename FROM dbo.UMP_type ";
            ddltype.DataSource  = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSQL);
            ddltype.DataBind();
            strSQL = " SELECT  UMP_accountid,UMP_accountname FROM  dbo.UMP_account WHERE UMP_typeid =  '" + ddltype.SelectedValue.ToString() + "' ORDER BY UMP_accountname";
            ddlaccount.DataSource = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSQL);
            ddlaccount.DataBind();
            strSQL = " SELECT UMP_accountdetid = UMP_accountdetcontid,UMP_accountdetname = UMP_accountdetname +'-'+ CAST( UMP_accountdetcontid AS NVARCHAR(10) )FROM dbo.UMP_accountdet WHERE UMP_accountid = '" + ddlaccount.SelectedValue.ToString() + "'";
            ddlaccountdet.DataSource = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSQL);
            ddlaccountdet.DataBind();

            strSQL = " SELECT  d.code FROM dbo.Users us LEFT JOIN tcpc" + Session["PlantCode"].ToString() + "..Departments d ON us.departmentID = d.departmentID WHERE userID = " + Session["uID"].ToString();
            txtcode.Text = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strSQL).ToString();


           strSQL = " SELECT  d.name FROM dbo.Users us LEFT JOIN tcpc" + Session["PlantCode"].ToString() + "..Departments d ON us.departmentID = d.departmentID WHERE userID = " + Session["uID"].ToString();
           lbldeptcode.Text = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strSQL).ToString();
        
            if (Session["PlantCode"].ToString() == "1")
            {
                //txtsite.Text = "1000";
                txtdomain.Text = "SZX";
            }
            else if (Session["PlantCode"].ToString() == "2")
            {
               // txtsite.Text = "2000";
                txtdomain.Text = "ZQL";
            }
            else if (Session["PlantCode"].ToString() == "5")
            {
               // txtsite.Text = "4000";
                txtdomain.Text = "YQL";
            }
            else if (Session["PlantCode"].ToString() == "8")
            {
                //txtsite.Text = "5000";
                txtdomain.Text = "HQL";
            }

    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (txtQad.Text.Trim().Length == 0)
        {
            this.Alert("QAD不能为空");
            return;
        }
        else
        {
            string strSql2 = " SELECT * FROM QAD_Data..pt_mstr WHERE pt_part ='" + txtQad.Text.Trim() + "'" + "and pt_domain = '" + txtdomain.Text.Trim()+"'";
            DataSet ds2;
            ds2 = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql2);
            if (!(ds2 != null && ds2.Tables[0].Rows.Count > 0))
            {
                this.Alert("QAD不存在.");
                return;
            }
        }
       
        if (txtnum.Text.Trim() == string.Empty)
        {
            this.Alert("数量不能为空.");
            return;
        }
        else
        {
            int _line;
            try
            {
                _line = Convert.ToInt32(txtnum.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('数量必须是数字!');";

                return;
            }
            if (_line <= 0)
            {
                ltlAlert.Text = "alert('数量必须大于零!');";
            }
        }
        SqlParameter[] sqlParam = new SqlParameter[10];
        sqlParam[0] = new SqlParameter("@uID", Session["uID"].ToString());
        sqlParam[1] = new SqlParameter("@uName", Session["uName"].ToString());
        sqlParam[3] = new SqlParameter("@mstr_id", lbl_id.Text);
        sqlParam[4] = new SqlParameter("@remark", txtremark.Text.Trim());
        sqlParam[5] = new SqlParameter("@qad", txtQad.Text.Trim());
        sqlParam[6] = new SqlParameter("@num", txtnum.Text.Trim());
        sqlParam[7] = new SqlParameter("@site", ddlsite.SelectedItem.Text);
        sqlParam[2] = new SqlParameter("@retValue", DbType.Boolean);
        sqlParam[2].Direction = ParameterDirection.Output;
        sqlParam[8] = new SqlParameter("@domain", txtdomain.Text);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_UMP_insertUMPdetline", sqlParam);
        txtQad.Text = "";
        txtnum.Text = "";
        txtremark.Text = "";

        BindMstrData();
    }
    private void BindDetailData()
    {
        gvRWDQad.DataSource = GetProductStruApplyDetail(lbl_id.Text);
        gvRWDQad.DataBind();
        gvApprove.DataSource = GetProductStruApply(lbl_id.Text);
        gvApprove.DataBind();
    }
    public DataTable GetProductStruApplyDetail(string id)
    {
        try
        {
            string strName = "sp_UMP_selectUMPDetail";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable GetProductStruApply(string id)
    {
        try
        {
            string strName = "sp_UMP_selectUMPApplydetList";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@mid", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
   
    protected void btn_submit_Click(object sender, EventArgs e)
    {
      
        
        if (txtApplyReason.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请输入理由!'); ";
            return;
        }
        if (txtApproveName.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请选择提交人!'); ";
            return;
        }

        if (chkEmail.Checked)
        {
            if (txt_ApproveEmail.Text == string.Empty)
            {
                ltlAlert.Text = "alert('你选择的提交人没有邮箱!'); ";
                return;
            }
        }

        string mid = lbl_id.Text;
        string strApprover = txt_approveID.Text.ToString();
        string strApproverName = txtApproveName.Text.ToString();
        string applyReason = txtApplyReason.Text.Trim().ToString();
        int uID = int.Parse(Session["uID"].ToString());
        string uName = Convert.ToString(Session["uName"]);
        try
        {
            bool isSuccess = true;
            string message = "";
             AddProjQadLink(mid, strApprover, strApproverName, applyReason, uID, uName,"1");
             if (chkEmail.Checked)
             {
                 isSuccess = SendMail(mid,  txtUMPcode.Text.Trim(), txt_ApproveEmail.Text.Trim(), strApproverName, Session["Email"].ToString(), Session["uName"].ToString(), applyReason,  out message);
             }
             if (isSuccess)
             {
                 ltlAlert.Text = "alert('Apply successfully!');";

             }
             ltlAlert.Text += " window.location.href='UMP_ApproverList.aspx?code=" + txtUMPcode + "rm=" + DateTime.Now.ToString() + "';";
        }
        catch (Exception)
        {

            ltlAlert.Text = "alert('Database Operation Failed!');";
            return;
        }
      
       
    }
    public bool SendMail(string mid,  string projectCode, string toEmailAddress, string strApproverName, string applyEmailAddress, string applyName, string applyReason,  out string returnMessage)
    {
        StringBuilder sb = new StringBuilder();

        MailAddress from = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
        MailAddress to;
        MailMessage mail = new MailMessage();
        mail.From = from;
        Boolean isSuccess = false;
        try
        {
            to = new MailAddress(toEmailAddress, strApproverName);
            mail.To.Add(to);
        }
        catch
        {
            returnMessage = "the email address of " + toEmailAddress + "is incorrect.Pls correct!";
            return false;
        }

        if (applyEmailAddress != null && applyEmailAddress != "")
        {
            MailAddress cc = new MailAddress(applyEmailAddress, applyName);
            mail.CC.Add(cc);
        }

        try
        {
            mail.Subject = "[Notify]计划外出入库有需要你审批的数据";
            sb.Append("<html>");
            sb.Append("<body>");
            sb.Append("<form>");
            sb.Append(" 审批员你好  <br>");
            
            sb.Append("     申请单号:" + projectCode + "<br>");
            sb.Append("     申请说明:" + applyReason + "<br>");
            sb.Append("     申请者: " + applyName + "在此项目添加了计划外出入库申请，请链接以下地址时查看并审批申请<br><br>");
        
            sb.Append("         Internet: <a href='"+baseDomain.getPortalWebsite()+"/wo2/UMP_ApproverList.aspx' rel='external' target='_blank'>"+baseDomain.getPortalWebsite()+"/wo2/UMP_ApproverList.aspx</a>");
            sb.Append("</form>");
            sb.Append("</html>");

            mail.Body = Convert.ToString(sb);
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            sb.Remove(0, sb.Length);
            isSuccess = true;
            returnMessage = "Send Mail Success!";
        }
        catch (Exception ex)
        {
            isSuccess = false;
            returnMessage = "Send Mail Failed!";
        }

        return isSuccess;


    }
    public int AddProjQadLink(string mid, string strApprover, string strApproverName, string applyReason, int uID, string uName,string status)
    {
        string strSql = "sp_UMP_InsertProjQadApply";
        SqlParameter[] sqlParam = new SqlParameter[7];
        sqlParam[0] = new SqlParameter("@mid", mid);
        sqlParam[1] = new SqlParameter("@approverBy", strApprover);
        sqlParam[2] = new SqlParameter("@approverName", strApproverName);
        sqlParam[3] = new SqlParameter("@applyReason", applyReason);
        sqlParam[4] = new SqlParameter("@uId", uID);
        sqlParam[5] = new SqlParameter("@uName", uName);
        sqlParam[6] = new SqlParameter("@status", status);

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam));
    }

    protected void btn_approve_Click(object sender, EventArgs e)
    {
        if (txtApplyReason.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请输入理由!'); ";
            return;
        }
        

        string mid = lbl_id.Text;
        string strApprover = txt_approveID.Text.ToString();
        string strApproverName = txtApproveName.Text.ToString();
        string applyReason = txtApplyReason.Text.Trim().ToString();
        int uID = int.Parse(Session["uID"].ToString());
        string uName = Convert.ToString(Session["uName"]);
        try
        {
            bool isSuccess = true;
            string message = "";
            AddProjQadLink(mid, strApprover, strApproverName, applyReason, uID, uName, "2");
          
            ltlAlert.Text += " window.location.href='UMP_ApproverList.aspx?code=" + txtUMPcode + "rm=" + DateTime.Now.ToString() + "';";
        }
        catch (Exception)
        {

            ltlAlert.Text = "alert('Database Operation Failed!');";
            return;
        }
    }
    protected void btn_diaApp_Click(object sender, EventArgs e)
    {
        if (txtApplyReason.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请输入理由!'); ";
            return;
        }


        string mid = lbl_id.Text;
        string strApprover = txt_approveID.Text.ToString();
        string strApproverName = txtApproveName.Text.ToString();
        string applyReason = txtApplyReason.Text.Trim().ToString();
        int uID = int.Parse(Session["uID"].ToString());
        string uName = Convert.ToString(Session["uName"]);
        try
        {
            bool isSuccess = true;
            string message = "";
            AddProjQadLink(mid, strApprover, strApproverName, applyReason, uID, uName, "-1");

            ltlAlert.Text += " window.location.href='UMP_ApproverList.aspx?code=" + txtUMPcode + "rm=" + DateTime.Now.ToString() + "';";
        }
        catch (Exception)
        {

            ltlAlert.Text = "alert('Database Operation Failed!');";
            return;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string strQuy = Request.QueryString["islook"] == null ? "" : Convert.ToString(Request.QueryString["islook"]);
        if (strQuy == "no")
        {
            ltlAlert.Text += " window.location.href='UMP_finishList.aspx?code=" + txtUMPcode + "rm=" + DateTime.Now.ToString() + "';";
        }
        else
        {
            ltlAlert.Text += " window.location.href='UMP_ApproverList.aspx?code=" + txtUMPcode + "rm=" + DateTime.Now.ToString() + "';";
        }
       
    }
    protected void btn_Approver_Click(object sender, EventArgs e)
    {
        //int mId = int.Parse(Request.QueryString["mid"].ToString());
       
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        ltlAlert.Text = "var w=window.open('UMP_getapprover.aspx','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
        
    }
    protected void btnSelectAll_Click(object sender, EventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
    protected void gvRWDQad_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRWDQad.PageIndex = e.NewPageIndex;
        BindMstrData();
    }
    protected void gvRWDQad_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvRWDQad_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvRWDQad_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strID = gvRWDQad.DataKeys[e.RowIndex]["UMP_id"].ToString();
        if (DeleteRdwQad(strID))
        {
            BindMstrData();
        }
        else
        {
            ltlAlert.Text = "alert('delete data failed!'); ";
            BindMstrData();
            return;
        }
    }
    public bool DeleteRdwQad(string id)
    {
        try
        {
            string strName = "sp_UMP_deleteUMPList";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);
            return Convert.ToBoolean(param[1].Value);

        }
        catch (Exception ex)
        {
            return false;
        }
    }
    protected void gvRDW_PreRender(object sender, EventArgs e)
    {

    }
    protected void gvRDW_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvRDW_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    public string AddProductStruApplyMstr(string userId, string userName, string domain, string site, string code, string UMP_accountid, string UMP_typeid, string UMP_typename, string UMP_accountname, string UMP_accountdetname)
    {
        try
        {
            string strName = "sp_UMP_InsertUMPmstr";
            SqlParameter[] parm = new SqlParameter[12];

            parm[1] = new SqlParameter("@userId", userId);
            parm[2] = new SqlParameter("@userName", userName);
            parm[3] = new SqlParameter("@domain", domain);
            parm[4] = new SqlParameter("@site", site);
            parm[5] = new SqlParameter("@code", code);
            parm[6] = new SqlParameter("@UMP_accountid", UMP_accountid);
            parm[7] = new SqlParameter("@UMP_typeid", UMP_typeid);
            parm[8] = new SqlParameter("@UMP_typename", UMP_typename);
            parm[9] = new SqlParameter("@UMP_accountname", UMP_accountname);
            parm[10] = new SqlParameter("@UMP_accountdetname", UMP_accountdetname);
            parm[11] = new SqlParameter("@UMP_nbr", txtUMPcode.Text);
            return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        }
        catch
        {
            return "0";
        }
    }
    protected void btnDoc_Click(object sender, EventArgs e)
    {
       
            string id = AddProductStruApplyMstr( Session["uID"].ToString(), Session["uName"].ToString(),txtdomain.Text,ddlsite.SelectedItem.Text,txtcode.Text,ddlaccount.SelectedValue,ddltype.SelectedValue,ddltype.SelectedItem.Text,ddlaccount.SelectedItem.Text,ddlaccountdet.SelectedValue);
            if (lbl_id.Text == "")
            {
                if (id != "0")
                {
                    lbl_id.Text = id;
                    BindMstrData();
                }
                else
                {
                    this.Alert("申请保存失败");
                    return;
                }
            }
            else
            {
                BindMstrData();
            }
        
    }
    private void BindMstrData()
    {
        det.Visible = true;
        string status="";
        IDataReader reader = GetProductStruApplyMstr(lbl_id.Text);
        if (reader.Read())
        {
            txtUMPcode.Text = reader["UMP_code"].ToString();
            txtdomain.Text = reader["UMP_domain"].ToString();
            txtcode.Text = reader["UMP_Departmentscode"].ToString();
            ddlsite.SelectedValue = reader["UMP_site"].ToString();
            ddltype.SelectedValue = reader["UMP_typeid"].ToString();
            status = reader["UMP_status"].ToString();
            string strSQL = " SELECT  UMP_accountid,UMP_accountname FROM  dbo.UMP_account WHERE UMP_typeid =  '" + ddltype.SelectedValue.ToString() + "' ORDER BY UMP_accountname";
            ddlaccount.DataSource = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSQL);
            ddlaccount.DataBind();
           
            ddlaccount.SelectedValue = reader["UMP_accountid"].ToString();
            strSQL = " SELECT UMP_accountdetid = UMP_accountdetcontid,UMP_accountdetname = UMP_accountdetname +'-'+ CAST( UMP_accountdetcontid AS NVARCHAR(10) )FROM dbo.UMP_accountdet WHERE UMP_accountid = '" + ddlaccount.SelectedValue.ToString() + "'";
            ddlaccountdet.DataSource = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSQL);
            ddlaccountdet.DataBind();
            ddlaccountdet.SelectedValue = reader["UMP_accountdetname"].ToString();
            

        }
      

        reader.Close();
        if (status == "0")
        {
            BtnAdd.Visible = true;
            btn_submit.Visible = true;
            btn_approve.Visible = false;
            btn_diaApp.Visible = false;
            btnDoc.Visible = true;
            appv.Visible = true;
        }
        else if (status == "1")
        {
            BtnAdd.Visible = false;
            btn_submit.Visible = true;
            //btn_approve.Visible = false;
            btn_diaApp.Visible = true;
            btnDoc.Visible = false;
            gvRWDQad.Columns[6].Visible = false;
            appv.Visible = true;
        }
        else 
        {
            BtnAdd.Visible = false;
            btn_submit.Visible = false;
            btn_approve.Visible = false;
            btn_diaApp.Visible = false;
            btnDoc.Visible = false;
            gvRWDQad.Columns[6].Visible = false;
            appv.Visible = false;
        }
        BindDetailData();
    }
    public SqlDataReader GetProductStruApplyMstr(string id)
    {
        try
        {
            string strName = "sp_UMP_selectUMPmstr";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return null;
        }
    }
    protected void ItemCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        int index = ((GridViewRow)(chk.NamingContainer)).RowIndex + gvRWDQad.PageSize * gvRWDQad.PageIndex;    //通过NamingContainer可以获取当前checkbox所在容器对象，即gridviewrow   
        //DataTable dt = dtQad;

        //if (chk.Checked)
        //{
        //    dt.Rows[index]["selected"] = 1;
        //}
        //else
        //{
        //    dt.Rows[index]["selected"] = 0;
        //}
        //BindAddQADData();
    }
    public bool check(object value)
    {
        //bool chk = false;
        //if (bool.TryParse(value.ToString(),out chk))
        //{
        //    return chk;
        //}
        //else
        //{
        //    return false;
        //}
        try
        {
            return Convert.ToBoolean(value);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strSQL = " SELECT  UMP_accountid,UMP_accountname FROM  dbo.UMP_account WHERE UMP_typeid =  '" + ddltype.SelectedValue.ToString() + "' ORDER BY UMP_accountname";
        ddlaccount.DataSource = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSQL);
        ddlaccount.DataBind();
        strSQL = " SELECT UMP_accountdetid = UMP_accountdetcontid,UMP_accountdetname = UMP_accountdetname +'-'+ CAST( UMP_accountdetcontid AS NVARCHAR(10) )FROM dbo.UMP_accountdet WHERE UMP_accountid = '" + ddlaccount.SelectedValue.ToString() + "'";
        ddlaccountdet.DataSource = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSQL);
        ddlaccountdet.DataBind(); 
    }

    protected void ddlsite_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strSQL = " SELECT  realdomain FROM  dbo.Domain_Mes WHERE qad_site = '"+ddlsite.SelectedValue+"'";
        txtdomain.Text = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strSQL).ToString();
    }
    protected void txtcode_TextChanged(object sender, EventArgs e)
    {
        string strSQL = " SELECT cc_desc FROM QAD_Data.dbo.cc_mstr  WHERE  cc_active=1 AND cc_ctr= '" + txtcode.Text.Trim() + "'";
        SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strSQL);
        if (reader.Read())
        {
            lbldeptcode.Text = reader["cc_desc"].ToString();
        }
        else
        { 
            txtcode.Text="";
            lbldeptcode.Text = "";
            this.Alert("成本中心不存在");
        }
        reader.Close();
    }
}