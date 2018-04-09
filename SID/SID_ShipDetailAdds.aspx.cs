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
using QADSID;

public partial class SID_SID_ShipDetailAdds : BasePage
{
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDID.Text = Request.QueryString["DID_ori"].ToString();
            DataBind();
        }
    }

    protected void DataBind()
    {
        DataTable dt = sid.GetShipDetails(Convert.ToString(Request.QueryString["DID"]));
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());

        }

        gvShipdetails.DataSource = dt;
        gvShipdetails.DataBind();
        dt.Dispose();

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Request.QueryString["DID"]) == "" || Convert.ToString(Request.QueryString["DID"]) == null)
        {
            ltlAlert.Text = "alert('����ʧ��!');Form1.txtSNO.focus();";
            return;
        }

        if (txtSNO.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('ϵ�в���Ϊ��!');Form1.txtSNO.focus();";
            return;
        }

        if (txtQAD.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('���ϱ��벻��Ϊ��!');Form1.txtQAD.focus();";
            return;
        }

        if (txtQtySet.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('������������Ϊ��!');Form1.txtQtySet.focus();";
            return;
        }
        else
        {
            if (!sid.IsNumber(txtQtySet.Text.Trim()))
            {
                ltlAlert.Text = "alert('����������ʽ����!');Form1.txtQtySet.focus();";
                return;
            }
        }

        if (txtQtyBox.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('��������Ϊ��!');Form1.txtQtyBox.focus();";
            return;
        }
        else
        {
            if (!sid.IsNumber(txtQtyBox.Text.Trim()))
            {
                ltlAlert.Text = "alert('������ʽ����!');Form1.txtQtyBox.focus();";
                return;
            }
        }

        if (txtNbr.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('���۶�������Ϊ��!');Form1.txtNbr.focus();";
            return;
        }

        if (txtLine.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�кŲ���Ϊ��!');Form1.txtLine.focus();";
            return;
        }
        else
        {
            if (!sid.IsNumber(txtLine.Text.Trim()))
            {
                ltlAlert.Text = "alert('�к�ӦΪ����!');Form1.txtLine.focus();";
                return;
            }
        }

        if (txtPO.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('TCP��������Ϊ��!');Form1.txtPO.focus();";
            return;
        }

        if (txtCustPart.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�ͻ����ϺŲ���Ϊ��!');Form1.txtCustPart.focus();";
            return;
        }

        if (txtWeight.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('��������Ϊ��!');Form1.txtWeight.focus();";
            return;
        }
        else
        {
            if (!sid.IsNumber(txtWeight.Text.Trim()))
            {
                ltlAlert.Text = "alert('������ʽ����!');Form1.txtWeight.focus();";
                return;
            }
        }

        if (txtVolume.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�������Ϊ��!');Form1.txtVolume.focus();";
            return;
        }
        else
        {
            if (!sid.IsNumber(txtVolume.Text.Trim()))
            {
                ltlAlert.Text = "alert('�����ʽ����!');Form1.txtVolume.focus();";
                return;
            }
        }


        if (txtQtyPcs.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('ֻ������Ϊ��!');Form1.txtQtyPcs.focus();";
            return;
        }
        else
        {
            if (!sid.IsNumber(txtQtyPcs.Text.Trim()))
            {
                ltlAlert.Text = "alert('�����ʽ����!');Form1.txtVolume.focus();";
                return;
            }
        }



        string strUID = Session["uID"].ToString();
        string strDID = Convert.ToString(Request.QueryString["DID"]);
        string strSNO = txtSNO.Text.Trim();
        string strQAD = txtQAD.Text.Trim();
        string strQtySet = txtQtySet.Text.Trim();
        string strQtyBox = txtQtyBox.Text.Trim();
        string strQA = txtQA.Text.Trim();
        string strNbr = txtNbr.Text.Trim();
        string strLine = txtLine.Text.Trim();
        string strWO = txtWO.Text.Trim();
        string strPO = txtPO.Text.Trim();
        string strCustPart = txtCustPart.Text.Trim();
        string strWeight = txtWeight.Text.Trim();
        string strVolume = txtVolume.Text.Trim();
        string strPkgs = txtPkgs.Text.Trim();
        string strQtyPcs = txtQtyPcs.Text.Trim();
        string strFedx = txtFedx.Text.Trim();
        string strFob = txtFob.Text.Trim();
        string strMemo = txtMemo.Text.Trim();

        int nRet = sid.InsertShipDetails(strUID, strDID, strSNO, strQAD, strQtySet, strQtyBox, strQA, strNbr, strLine, strWO, strPO, strCustPart, strWeight, strVolume, strPkgs,
            strQtyPcs, strFedx, strFob, strMemo);

        if (nRet < 0)
        {
            ltlAlert.Text = "alert('����ʧ�ܣ�');";
            return;
        }
        else
        {
            Response.Redirect("/SID/SID_shipdetailAdds.aspx?DID=" + strDID);
        }
    }
    protected void gvShipdetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int32 IErr = 0;
        IErr = sid.DelShipDetails(Convert.ToString(Session["uID"]), gvShipdetails.DataKeys[e.RowIndex].Value.ToString());
        if (IErr < 0)
        {
            ltlAlert.Text = "alert('ɾ��ʧ�ܣ�');";
            return;
        }
        else
        {
            DataBind();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/SID/SID_shipdetail.aspx?DID=" + txtDID.Text.Trim() + "&RAD=False&rt=" + DateTime.Now.ToString());
    }
}