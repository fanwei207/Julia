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


public partial class SID_SID_ShipDetailAdd : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDID.Text = Request.QueryString["DID"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDID.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('记录不合法，请刷新!');";
            return;
        }

        if (txtSNO.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('系列不能为空!');Form1.txtSNO.focus();";
            return;
        }

        if (txtQAD.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('物料编码不能为空!');Form1.txtQAD.focus();";
            return;
        }

        if (txtQtySet.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('出运套数不能为空!');Form1.txtQtySet.focus();";
            return;
        }
        else
        {
            if (!sid.IsNumber(txtQtySet.Text.Trim()))
            {
                ltlAlert.Text = "alert('出运套数格式不对!');Form1.txtQtySet.focus();";
                return;
            }
        }

        if (txtQtyBox.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('箱数不能为空!');Form1.txtQtyBox.focus();";
            return;
        }
        else
        {
            if (!sid.IsNumber(txtQtyBox.Text.Trim()))
            {
                ltlAlert.Text = "alert('箱数格式不对!');Form1.txtQtyBox.focus();";
                return;
            }
        }

        if (txtNbr.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('销售订单不能为空!');Form1.txtNbr.focus();";
            return;
        }

        if (txtLine.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('行号不能为空!');Form1.txtLine.focus();";
            return;
        }
        else
        {
            if (!sid.IsNumber(txtLine.Text.Trim()))
            {
                ltlAlert.Text = "alert('行号应为数字!');Form1.txtLine.focus();";
                return;
            }
        }

        if (txtPO.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('TCP订单不能为空!');Form1.txtPO.focus();";
            return;
        }

        if (txtCustPart.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('客户物料号不能为空!');Form1.txtCustPart.focus();";
            return;
        }

        if (txtWeight.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('重量不能为空!');Form1.txtWeight.focus();";
            return;
        }
        else
        {
            if (!sid.IsNumber(txtWeight.Text.Trim()))
            {
                ltlAlert.Text = "alert('重量格式不对!');Form1.txtWeight.focus();";
                return;
            }
        }

        if (txtVolume.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('体积不能为空!');Form1.txtVolume.focus();";
            return;
        }
        else
        {
            if (!sid.IsNumber(txtVolume.Text.Trim()))
            {
                ltlAlert.Text = "alert('体积格式不对!');Form1.txtVolume.focus();";
                return;
            }
        }


        if (txtQtyPcs.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('只数不能为空!');Form1.txtQtyPcs.focus();";
            return;
        }
        else
        {
            if (!sid.IsNumber(txtQtyPcs.Text.Trim()))
            {
                ltlAlert.Text = "alert('体积格式不对!');Form1.txtVolume.focus();";
                return;
            }
        }

        string strUID = Session["uID"].ToString();
        string strDID = txtDID.Text.Trim();
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
        string strATL = txtATL.Text.Trim();

        int nRet = sid.InsertShipDetail(strUID, strDID, strSNO, strQAD, strQtySet, strQtyBox, strQA, strNbr, strLine, strWO, strPO, strCustPart, strWeight, strVolume, strPkgs,
            strQtyPcs, strFedx, strFob, strMemo, strATL);

        if (nRet < 0)
        {
            ltlAlert.Text = "alert('保存失败！');";
            return;
        }
        else
        {
            if (sid.IsAirShipInsert(strDID, strPO, strLine))
            {
                ltlAlert.Text = "alert('保存成功!提示:该订单物料为空运!'); ";
            }
            else
            {
                ltlAlert.Text = "alert('保存成功!'); ";
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/SID/SID_shipdetail.aspx?DID=" + txtDID.Text.Trim() + "&RAD=False&rt=" + DateTime.Now.ToString());
    }
}
