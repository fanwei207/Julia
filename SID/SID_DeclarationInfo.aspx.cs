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

public partial class SID_SID_DeclarationInfo : BasePage
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

        gvSID.DataSource = sid.SelectDeclarationInfo(strShipNo);
        gvSID.DataBind();

        Session["EXTitle"] = "120^<b>���ط�Ʊ��</b>~^120^<b>˰��Ʊ��</b>~^120^<b>��Ʊ����</b>~^140^<b>���ں�������</b>~^60^<b>ϵ��</b>~^400^<b>��Ʒ����</b>~^120^<b>��Ʒ����</b>~^120^<b>����</b>~^60^<b>����</b>~^120^<b>���</b>~^";
        Session["EXSQL"] = sid.SelectDeclarationInfoExcel(strShipNo);
        Session["EXHeader"] = "/SID/ExportSIDDeclarationInfo.aspx^~^";
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
}
