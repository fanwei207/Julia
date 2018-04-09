using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class RDW_Test_NewTest : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["mid"] != null)
            {
                //txtProdNo.Enabled = false;
                txtProjectCode.Enabled = false;
                if (Request["from"] == null)
                {
                    hidProdNo.Value = Request["prodno"].ToString();
                    hidProdID.Value = Request["prodid"].ToString();

                    txtProdNo.Text = Request["prodno"].ToString();
                    txtProdNo.Enabled = false;
                }
                hidMID.Value = Request["mid"].ToString();
                hidDID.Value = Request["did"].ToString();
                hidProjectName.Value = Request["projectname"].ToString();
                hidProjectCode.Value = Request["projectcode"].ToString();

                txtProjectCode.Text = Request["projectcode"].ToString();
                txtProjectCode.Enabled = false;
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request["mid"] != null)
        {
            Response.Redirect("../RDW/Test_Report.aspx?" + (Request["from"] == null ? "" : "from=rdw&") + "mid=" + hidMID.Value + "&did=" + hidDID.Value +
                                               "&prodno=" + hidProdNo.Value +
                                               "&projectname=" + hidProjectName.Value +
                                               "&projectcode=" + hidProjectCode.Value +
                                               "&prodid=" + hidProdID.Value);
        }
        else
        {
            Response.Redirect("../RDW/Test_Report.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string electronics = string.Empty;
        string structure = string.Empty;
        if (chkElectronics.Checked)
        {
            electronics = "1";
        }
        else
        {
            electronics = "0";
        }
        if (chkStructure.Checked)
        {
            structure = "1";
        }
        else
        {
            structure = "0";
        }
        if (electronics == "0" && structure == "0")
        {
            ltlAlert.Text = "alert('分类电子或结构至少选择一项！')";
            return;
        }
        if (Request["mid"] == null)
        {
            if (txtProdNo.Text == string.Empty)
            {
                ltlAlert.Text = "alert('请填写跟踪号！')";
                return;
            }
            else
            {
                if (!checkProdNo())
                {
                    ltlAlert.Text = "alert('跟踪号不存在，请重新填写！')";
                    return;
                }
            }
        }
        else
        {
            if (!checkProdNoAndProject())
            {
                ltlAlert.Text = "alert('跟踪号不存在或跟踪号与项目不符，请重新填写！')";
                return;
            }
        }
        if (!insertNewTest(electronics, structure))
        {
            txtFailureTime.Text = string.Empty;
            txtProblemContent.Text = string.Empty;
            ltlAlert.Text = "alert('添加成功！')";
        }
        else
        {
            ltlAlert.Text = "alert('添加失败，请重新填写！')";
            return;
        }
    }
    private bool checkProdNo()
    {
        string str = "sp_test_checkProdNo";
        SqlParameter param = new SqlParameter("@no",txtProdNo.Text.Trim());
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, str, param));
    }
    private bool checkProdNoAndProject()
    {
        string str = "sp_test_checkProdNoAndProject";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@no", txtProdNo.Text.Trim());
        param[1] = new SqlParameter("@mid", Request["mid"].ToString());
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, str, param));
    }
    private bool insertNewTest(string electronics, string structure)
    {
        string str = "sp_test_insertNewTest";
        SqlParameter[] param = new SqlParameter[12];
        param[0] = new SqlParameter("@prodno", txtProdNo.Text.Trim());
        param[1] = new SqlParameter("@projectcode", txtProjectCode.Text.Trim());
        param[2] = new SqlParameter("@projectname", hidProjectName.Value.ToString());
        param[3] = new SqlParameter("@mid", hidMID.Value.ToString());
        param[4] = new SqlParameter("@did", hidDID.Value.ToString());
        param[5] = new SqlParameter("@electronics", electronics);
        param[6] = new SqlParameter("@structure", structure);
        param[7] = new SqlParameter("@failuretime", txtFailureTime.Text.Trim());
        param[8] = new SqlParameter("@problemcontent", txtProblemContent.Text.Trim());
        param[9] = new SqlParameter("@uID", Session["uID"].ToString());
        param[10] = new SqlParameter("@uName", Session["uName"].ToString());
        
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, str, param));
    }
}