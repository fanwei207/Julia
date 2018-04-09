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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class SID_EditInvShipNo : BasePage
{
    adamClass chk = new adamClass();
    //SID sid = new SID();
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
        
        string strinv = txt_inv.Text.Trim();
        //string strRef = txtSysPKRef.Text.Trim();
        string strshipno = txt_shipno.Text.Trim();

        gvInv.DataSource = sid.SelectInvShipNoInfo(strinv, strshipno);
        gvInv.DataBind();
    }

    protected void gvPacking_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInv.PageIndex = e.NewPageIndex;
        BindData();
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

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvInv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }

    protected void gvInv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Edit1")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvInv.DataKeys[index].Values["sid_id"].ToString();
            DataTable dt = sid.SelectInvShipNoByID(Convert.ToInt32(id));
            txt_inv.Text = dt.Rows[0]["sid_inv_nbr"].ToString();
            txt_shipno.Text = dt.Rows[0]["sid_shipNo"].ToString();
            hid_id.Value = id;
        }
        //BindData();
    }

    protected void gvInv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInv.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hid_id.Value))
        {
            this.Alert("����ѡ��Ҫ�޸ļ�¼��");
            return;
        }
        string err = checkInvShipNo();
        if (!string.IsNullOrEmpty(err))
        {
            this.Alert(err);
            return;
        }
        string strinv = txt_inv.Text.Trim();
        string strshipno = txt_shipno.Text.Trim();
        int strid = Convert.ToInt32(hid_id.Value);
        sid.SaveInvShipNo(strid, strinv, strshipno, Convert.ToInt32(Session["uID"]));
        BindData();
        this.Alert("�޸ĳɹ���");
    }

    protected string checkInvShipNo()
    {
        string err1 = "";
        string strinv = txt_inv.Text.Trim();
        string strshipno = txt_shipno.Text.Trim();
        int err = sid.CheckInvShipNo(strinv, strshipno);
        if (err == 2)
        {
            err1 = "����Ʊ�Ų�����";
        }
        else if (err == 3)
        {
            err1 = "���ط�Ʊ�Ų�����";
        }
        else if (err == 4)
        {
            err1 = "����Ʊ���뱨�ط�Ʊ�Ų�����";
        }
        return err1;
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        string err = checkInvShipNo();
        if (!string.IsNullOrEmpty(err))
        {
            this.Alert(err);
            return;
        }
        string strinv = txt_inv.Text.Trim();
        string strshipno = txt_shipno.Text.Trim();
        int strid = 0;
        sid.SaveInvShipNo(strid, strinv, strshipno, Convert.ToInt32(Session["uID"]));
        BindData();
        this.Alert("�����ɹ���");
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        txt_inv.Text = "";
        txt_shipno.Text = "";
        BindData();
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string strinv = txt_inv.Text.Trim();
        string strshipno = txt_shipno.Text.Trim();

        DataTable dt = sid.SelectInvShipNoInfo(strinv, strshipno);
        if (dt.Rows.Count <= 0)
        {
            this.Alert("������ѯ���ݣ�");
            return;
        }

        string title = "80^<b>�к�</b>~^120^<b>����Ʊ��</b>~^120^<b>���ط�Ʊ��</b>~^";
        this.ExportExcel(title, dt, false);
    }
}
