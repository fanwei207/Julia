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

public partial class SID_SID_DeclarationList : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        //�������
        string strShipNo = txtShipNo.Text.Trim();
        string strVerfication = txtVerfication.Text.Trim();

        gvSID.DataSource = sid.SelectDeclarationList(strShipNo, strVerfication);
        gvSID.DataBind();

        lblDiffValue.Text = string.Format("{0:#,#0.00}", sid.SelectTotalDiff());
    }

    /// <summary>
    /// ���ݲ���һҳҲ��ʾGridView��ҳ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSID_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvSID_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSID.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvSID_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //�������
        int intRow = 0;
        string strshipno = string.Empty;

        if (e.CommandName.ToString() == "Detail")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strshipno = gvSID.DataKeys[intRow].Value.ToString();
            Response.Redirect("/SID/SID_Declaration.aspx?sno=" + strshipno, true);
        }
    }

    protected void btnViewDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("/SID/SID_CustomerDiff.aspx?rt=" + DateTime.Now.ToString());
    }

    protected void gvSID_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

            btnDelete.Attributes.Add("onclick", "return confirm('ȷ��Ҫɾ���ü�¼��')");
        }
    }

    protected void gvSID_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //�������
        string strShipNo = gvSID.DataKeys[e.RowIndex].Value.ToString();

        if (sid.DeleteDeclaration(strShipNo))
        {
            ltlAlert.Text = "alert('���ص�ɾ���ɹ���'); window.location.href='/SID/SID_DeclarationList.aspx?rm=" + DateTime.Now.ToString() + "';";
        }
        else
        {
            ltlAlert.Text = "alert('ɾ�����ݹ����г���'); ";
            BindData();
        }
    }
}
