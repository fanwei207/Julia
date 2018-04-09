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
using adamFuncs;
using BudgetProcess;

public partial class bg_ApplyResultList : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        {
            
            txtYear.Text = DateTime.Now.Year.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

            BindData();
        }
    }

    protected void BindData()
    {
        //�������
        string strDept = txtDept.Text.Trim();
        string strUser = txtApplicant.Text.Trim();
        string strKeyword = txtKeyword.Text.Trim();
        string strPlantID = Convert.ToString(Session["plantCode"]);
        string strYear = txtYear.Text.Trim();
        string strMonth = ddlMonth.SelectedValue.ToString();

        gvApply.DataSource = Budget.getApplyResultList(strDept, strUser, strKeyword, strPlantID, strYear, strMonth).Tables[0];
        gvApply.DataBind();

        Session["EXTitle"] = "60^<b>������</b>~^200^<b>��������������</b>~^300^<b>��������</b>~^100^<b>������</b>~^100^<b>����˻�</b>~^200^<b>������;</b>~^200^<b>�ɱ�����</b>~^100^<b>ʵ�ʷ���</b>~^";
        Session["EXSQL"] = Budget.getApplyResultExcel(strDept, strUser, strKeyword, strPlantID, strYear, strMonth);
        Session["EXHeader"] = "";
    }

    protected void gvApply_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvApply.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvApply_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        gvApply.PageIndex = 0;
        BindData();
    }
}
