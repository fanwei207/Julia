using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using adamFuncs;


public partial class HR_app_RecruitmentRequestListDet : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtAppName.Enabled = false; 
            txtAppDate.Enabled = false; 
            txtAppdeprt.Enabled = false; 
            txtAppCop.Enabled = false; 
            txtAppProc.Enabled = false; 
            txtDate.Enabled = false; 
            txtReason.Enabled = false;
            txtNewProc.Enabled = false; 
            txtAppNum.Enabled = false; 
            txtAppExp.Enabled = false; 
            txtSex.Enabled = false; 
            txtAge.Enabled = false; 
            txtEdu.Enabled = false; 
            txtEduRequest.Enabled = false; 
            txtLanguage.Enabled = false; 
            txtNote.Enabled = false;
            txtAppRequest.Enabled = false;
            txtReasonNote.Enabled = false;

            txtAppCop.Text = Request.QueryString["App_Company"];
            txtAppDate.Text = Request.QueryString["App_Date"];
            txtAppName.Text = Request.QueryString["App_userName"];
            txtAppdeprt.Text = Request.QueryString["App_department"];
            txtAppProc.Text = Request.QueryString["App_Process"];
            BindData();                                                                                                                                                                                                                                             


        }
    }
    private void BindData()
    {
        SqlDataReader reader = GetAppListDet(txtAppCop.Text, txtAppDate.Text, txtAppName.Text, txtAppdeprt.Text, txtAppProc.Text);
        if (reader.Read())
        {            
            txtDate.Text = Convert.ToString(reader["App_ArrDate"]);
            txtReason.Text = Convert.ToString(reader["App_reason"]);
            txtNewProc.Text = Convert.ToString(reader["App_NewProc"]);
            txtAppNum.Text = Convert.ToString(reader["App_Num"]);
            txtReasonNote.Text = Convert.ToString(reader["App_ReasonNote"]);
            txtAppExp.Text = Convert.ToString(reader["App_ExpNum"]);
            txtSex.Text = Convert.ToString(reader["App_Sex"]);
            txtAge.Text = Convert.ToString(reader["App_AgeRange"]);
            txtEdu.Text = Convert.ToString(reader["App_Edu"]);
            txtEduRequest.Text = Convert.ToString(reader["App_EduRequest"]); 
            txtLanguage.Text = Convert.ToString(reader["App_Language"]);
            txtNote.Text = Convert.ToString(reader["App_LanguageNote"]);
            txtAppRequest.Text = Convert.ToString(reader["App_Request"]);
        }
        reader.Close();
    }
    protected void btnSub_Click(object sender, EventArgs e)
    {
        Response.Redirect("app_RecruitmentRequestList.aspx");
    }
    public SqlDataReader GetAppListDet(string AppCop,string AppDate,string AppName,string Appdeprt,string AppProc)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@AppCop", AppCop);
        param[1] = new SqlParameter("@AppDate", AppDate);
        param[2] = new SqlParameter("@AppName", AppName);
        param[3] = new SqlParameter("@Appdeprt", Appdeprt);
        param[4] = new SqlParameter("@AppProc", AppProc);
        return SqlHelper.ExecuteReader(chk.dsn0(), CommandType.StoredProcedure, "sp_app_selectAppListDet", param);
    }
}