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
using CommClass;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using TCPCHINA.ODBCHelper;

public partial class EDI_EDI850InternalQAD : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDateFr.Text = Convert.ToString(DateTime.Now.Year) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day);
            txtDateTo.Text = Convert.ToString(DateTime.Now.Year) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day);
            lblFlag.Text = "";
        }
    }

    protected void btnExportEdi_Click(object sender, EventArgs e)
    {
        if (txtDateFr.Text.Length == 0)
        {
            ltlAlert.Text = "alert('�������� ����Ϊ�գ�');";
            return;
        }
        else
        {
            try
            {
                DateTime _d1 = Convert.ToDateTime(txtDateFr.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('�������� ��ʽ����ȷ��');";
                return;
            }
        }

        if (txtDateTo.Text.Length == 0)
        {
            ltlAlert.Text = "alert('�������� ����Ϊ�գ�');";
            return;
        }
        else
        {
            try
            {
                DateTime _d1 = Convert.ToDateTime(txtDateTo.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('�������� ��ʽ����ȷ��');";
                return;
            }
        }

        if (txtBuyer.Text.Length == 0)
        {
            ltlAlert.Text = "alert('�ɹ�Ա ����Ϊ�գ�');";
            return;
        }

        if (txtDomain.Text.Length == 0)
        {
            ltlAlert.Text = "alert('�� ����Ϊ�գ�');";
            return;
        }

        string strsql;
        DataSet ds, ds1;
        int m, n;
        strsql = "delete from pomstr_temp where uid=" + Session["uID"].ToString().Trim();
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.Text, strsql);

        strsql = "delete from poddet_temp where uid=" + Session["uID"].ToString().Trim();
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.Text, strsql);

        strsql = "select po_nbr,po_vend,po_due_date,po_buyer,po_domain,po_site,po_shipvia,po_contact,po_rmks";
        strsql = strsql + " from pub.po_mstr";
        strsql = strsql + " where po_domain = '" + txtDomain.Text.Trim() + "' and po_buyer = '" + txtBuyer.Text.Trim() + "' and po_ord_date >= '" + txtDateFr.Text.Trim();
        strsql = strsql + "' and po_ord_date <= '" + txtDateTo.Text.Trim() + "' and po_stat <> 'c'  and po_vend in ('S0511014','S0514003','S0021003','S0511031','S0517001')";
        strsql = strsql + " with (nolock) ";

        ds = OdbcHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn9"), CommandType.Text, strsql);

        for (m = 0; m < ds.Tables[0].Rows.Count; m++)
        {
            strsql = "insert into pomstr_temp (po_nbr,po_vend,po_due_date,po_buyer,po_domain,po_site,po_shipvia,po_contact,po_rmks,uid)";
            strsql += " values('" + ds.Tables[0].Rows[m][0].ToString().Trim() + "','" + ds.Tables[0].Rows[m][1].ToString().Trim() + "','" + ds.Tables[0].Rows[m][2].ToString().Trim() + "','" + ds.Tables[0].Rows[m][3].ToString().Trim() + "','" + ds.Tables[0].Rows[m][4].ToString().Trim() + "','" + ds.Tables[0].Rows[m][5].ToString().Trim() + "','" + ds.Tables[0].Rows[m][6].ToString().Trim() + "',N'" + ds.Tables[0].Rows[m][7].ToString().Trim() + "',N'" + ds.Tables[0].Rows[m][8].ToString().Trim() + "'," + Session["uID"].ToString().Trim() + ")";
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.Text, strsql);

            strsql = "select pod_nbr,pod_line,pod_part,pod_um,substring(cast(pod_qty_ord as varchar(50)),1,8),substring(cast(pod_pur_cost as varchar(50)),1,10),pod_need,pod_due_date";
            strsql += " from pub.pod_det";
            strsql += " where pod_nbr='" + ds.Tables[0].Rows[m][0].ToString().Trim() + "' and pod_domain ='" + ds.Tables[0].Rows[m][4].ToString().Trim() + "";
            ds1 = OdbcHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn9"), CommandType.Text, strsql);
            for (n = 0; n < ds1.Tables[0].Rows.Count; n++)
            {
                strsql = "insert into poddet_temp (pod_nbr,pod_line,pod_part,pod_um,pod_qty_ord,pod_pur_cost,pod_need,pod_due_date,uid)";
                strsql += " values('" + ds1.Tables[0].Rows[n][0].ToString().Trim() + "','" + ds1.Tables[0].Rows[n][1].ToString().Trim() + "','" + ds1.Tables[0].Rows[n][2].ToString().Trim() + "','" + ds1.Tables[0].Rows[n][3].ToString().Trim() + "','" + ds1.Tables[0].Rows[n][4].ToString().Trim() + "','" + ds1.Tables[0].Rows[n][5].ToString().Trim() + "','" + ds1.Tables[0].Rows[n][6].ToString().Trim() + "','" + ds1.Tables[0].Rows[n][7].ToString().Trim() + "'," + Session["uID"].ToString().Trim() + ")";
                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.Text, strsql);
            }
            ds1 = null;
        }

        ds = null;

        getEdiData.getQadEdiInternal(Session["uID"].ToString().Trim(), Session["PlantCode"].ToString().Trim());
        ltlAlert.Text = "alert('EDI���������Ѿ������ɹ���')";
        lblFlag.Text = "EDI_TEMP";
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (lblFlag.Text.Trim() == "")
        {
            ltlAlert.Text = "alert('���Ȳ���EDI�ɹ�����,Ȼ���ٵ���Excel����.')";
            return;
        }
        else
        {
            lblFlag.Text = "EDI_CHK";
        }

        //ltlAlert.Text = "window.open('ExportInternal850Excel.aspx')";
        DataTable dt = getEdiData.get850QADExcelDataInternal(Session["uID"].ToString().Trim(), Session["PlantCode"].ToString().Trim()).Tables[0];
        string title = "<b>�ɹ�������</b>~^<b>�ɹ�Ա</b>~^<b>��ֹ����</b>~^<b>���䷽ʽ</b>~^<b>�к�</b>~^<b>���Ϻ�</b>~^<b>��λ</b>~^<b>��������</b>~^<b>����</b>~^<b>����������</b>~^<b>�н�ֹ����</b>~^<b>�ص�</b>~^<b>��ϵ��</b>~^<b>��ע</b>~^";
        this.ExportExcel(title, dt, false);
    }

    protected void btnGenEdi_Click(object sender, EventArgs e)
    {
        if (lblFlag.Text.Trim() == "EDI_TEMP")
        {
            ltlAlert.Text = "alert('���ȵ���EXCEL����ȷ�������Ƿ�׼ȷ��Ȼ����EDI������')";
            return;
        }
        else if (lblFlag.Text.Trim() == "")
        {
            ltlAlert.Text = "alert('���Ȳ���EDI�ɹ�����,Ȼ���ٵ���Excel����.')";
            return;
        }
        else if (lblFlag.Text.Trim() == "EDI_CHK")
        {
            getEdiData.createSamplePo(Session["uID"].ToString().Trim());
            getEdiData.sendEdiPo(Session["uID"].ToString().Trim(), Session["PlantCode"].ToString().Trim());
            ltlAlert.Text = "alert('EDI�ɹ������������.')";
        }
    }
}
