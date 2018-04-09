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
using QADSID;

public partial class SID_SID_Diff : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDocumentStart.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01";
            txtDocumentEnd.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            txtDeclarationStart.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01";
            txtDeclarationEnd.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            BindData();
        }
    }

    protected void BindData()
    {
        //�������
        string strDocumentStart = txtDocumentStart.Text.Trim();
        string strDocumentEnd = txtDocumentEnd.Text.Trim();
        string strDeclarationStart = txtDeclarationStart.Text.Trim();
        string strDeclarationEnd = txtDeclarationEnd.Text.Trim();

        gvDiff.DataSource = sid.SelectDiffAmountList(strDocumentStart, strDocumentEnd, strDeclarationStart, strDeclarationEnd);
        gvDiff.DataBind();

        Session["EXTitle"] = "120^<b>��֤��Ʊ��</b>~^140^<b>��֤��Ʊ����</b>~^120^<b>���ط�Ʊ��</b>~^140^<b>���ط�Ʊ����</b>~^140^<b>��֤��Ʊ���</b>~^140^<b>���ط�Ʊ���</b>~^140^<b>��Ʊ������</b>~^";
        Session["EXSQL"] = sid.SelectDiffAmountExcel(strDocumentStart, strDocumentEnd, strDeclarationStart, strDeclarationEnd);
        //Session["EXSQL"] = "/SID/ExportSIDDiff.aspx^~^";
        Session["EXHeader"] = "/SID/ExportSIDDiff.aspx^~^";
    }

    /// <summary>
    /// ���ݲ���һҳҲ��ʾGridView��ҳ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDiff_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView) sender;
        GridViewRow gvr = (GridViewRow) gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvDiff_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDiff.PageIndex = e.NewPageIndex;
        BindData();
    }
}
