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

public partial class SID_PackingList1 : BasePage
{
    adamClass chk = new adamClass();
    //SID sid = new SID();
    PackingReport pack = new PackingReport();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindData();
        }
    }

    protected void BindData()
    {
        //�������
        
        string strPK = txtSysPKNo.Text.Trim();
        //string strRef = txtSysPKRef.Text.Trim();
        string strNbr = txtShipNo.Text.Trim();
        //string strDomain = txtDomain.Text.Trim();

        //Add By Shanzm 2011.02.14
        string strShipDate1 = txtShipDate1.Text.Trim();
        string strShipDate2 = txtShipDate2.Text.Trim();



        string pk = txtSysPKNo.Text.Trim();
        string nbr = txtShipNo.Text.Trim();
        string shipdate1 = txtShipDate1.Text.ToString();
        string shipdate2 = txtShipDate2.Text.ToString();
        string cust = txt_cust.Text.Trim();
        string lastcust = txt_lastcust.Text.Trim();
        string bldate1 = txt_bldate1.Text.ToString();
        string bldate2 = txt_bldate2.Text.ToString();
        string packingdate1 = txt_packingdate1.Text.ToString();
        string packingdate2 = txt_packingdate2.Text.ToString();

        //DataTable dt = pack.PackingExport(pk, nbr, shipdate1, shipdate2, cust, lastcust, bldate1, bldate2);

        gvPacking.DataSource = pack.PackingExport(pk, nbr, shipdate1, shipdate2, cust, lastcust, bldate1, bldate2, packingdate1, packingdate2);//pack.SelectSIDList(strPK, "", strNbr, strDomain, strShipDate1, strShipDate2);
        gvPacking.DataBind();
    }


    /// <summary>
    /// ���ݲ���һҳҲ��ʾGridView��ҳ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPacking_PreRender(object sender, EventArgs e)
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
        if (txtShipDate1.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _d1 = Convert.ToDateTime(txtShipDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ��');";
                return;
            }
        }

        if (txtShipDate2.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _d2 = Convert.ToDateTime(txtShipDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ��');";
                return;
            }
        }

        BindData();
    }

    protected void gvPacking_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPacking.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvPacking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text.Trim() == string.Empty)
            {
                e.Row.Cells[5].Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(e.Row.Cells[5].Text));
            }
        }
    }

    protected void gvPacking_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail1")
        {
            if (gvPacking.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
            {
                ltlAlert.Text = "alert('�����ǿ��У�');";
                return;
            }
            Response.Redirect("/SID/SID_PackingDoc.aspx?type=temp", true);
            //Response.Redirect("SID_PackingDoc.aspx?DID=" + Server.UrlEncode(gvPacking.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()) + "&rm=" + DateTime.Now);
        }
    }

    protected void btn_packingexport_Click(object sender, EventArgs e)
    {
        string pk = txtSysPKNo.Text.Trim();
        string nbr = txtShipNo.Text.Trim();
        string shipdate1 = txtShipDate1.Text.ToString();
        string shipdate2 = txtShipDate2.Text.ToString();
        string cust = txt_cust.Text.Trim();
        string lastcust = txt_lastcust.Text.Trim();
        string bldate1 = txt_bldate1.Text.ToString();
        string bldate2 = txt_bldate2.Text.ToString();
        string packingdate1 = txt_packingdate1.Text.ToString();
        string packingdate2 = txt_packingdate2.Text.ToString();

        DataTable dt = pack.PackingExport(pk, nbr, shipdate1, shipdate2, cust, lastcust, bldate1, bldate2, packingdate1, packingdate2);
        if (dt.Rows.Count <= 0)
        {
            this.Alert("������ѯ���ݣ�");
            return;
        }

        string title = "80^<b>���˵���</b>~^110^<b>ϵͳ���˵���</b>~^70^<b>��������</b>~^70^<b>��������~^70^<b>��Ʊ����</b>~^100^<b>װ��ص�</b>~^110^<b>���ϱ���</b>~^200^<b>�ͻ�����</b>~^70^<b>��������</b>~^70^<b>����ֻ��</b>~^100^<b>���۵���</b>~^40^<b>�к�</b>~^100^<b>ATL������</b>~^100^<b>TCP������</b>~^100^<b>�ͻ�������</b>~^40^<b>��λ</b>~^70^<b>ϵͳ��λ</b>~^70^<b>��������</b>~^70^<b>Price0</b>~^70^<b>Price1</b>~^70^<b>Amt1</b>~^70^<b>Price2</b>~^70^<b>Amt2</b>~^70^<b>Price3</b>~^70^<b>Amt3</b>~^80^<b>��Ʊ��</b>~^100^<b>�ͻ�</b>~^150^<b>�ͻ�����</b>~^100^<b>���տͻ�</b>~^150^<b>���տͻ�����</b>~^90^<b>��Ʊ��ӡ����</b>~^40^<b>��</b>~^80^<b>�ᵥ����</b>~^";
        this.ExportExcel(title, dt, false);
    }
}
