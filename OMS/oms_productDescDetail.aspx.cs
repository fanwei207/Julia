using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OMS_oms_productDescDetail : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["no"]))
            {
                txtItemNo.Text = Request.QueryString["no"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                lblId.Text = Request.QueryString["id"];
            }
            else
            {
                lblId.Text = "0";
            }
            BindData();
        }
    }

    private void BindData()
    {
        DataTable dt = OMSHelper.GetProductDescDetail(lblId.Text, txtItemNo.Text);
        if (dt.Rows.Count > 0)
        { 
            DataRow row=dt.Rows[0];
            lblId.Text = row["Id"].ToString();
            txtItemNo.Text = row["Item_Number"].ToString();
            txtUpcNo.Text = row["UPC_Number"].ToString();
            txtDesc.Text = row["Description"].ToString();
            txtWattage.Text = row["Wattage"].ToString();
            txtEquiv.Text = row["Equiv"].ToString();
            txtLumens.Text = row["Lumens"].ToString();
            txtLPW.Text = row["LPW"].ToString();
            txtCBCPest.Text = row["CBCPest"].ToString();
            txtBeamAngle.Text = row["BeamAngle"].ToString();
            txtCCT.Text = row["CCT"].ToString();
            txtCRI.Text = row["CRI"].ToString();
            txtMOL.Text = row["MOL"].ToString();
            txtDia.Text = row["Dia"].ToString();
            txtIP.Text = row["IP"].ToString();
            txtMP.Text = row["MP"].ToString();
            txtList.Text = row["List"].ToString();
            txtPrice.Text = row["Price"].ToString();
            txtSTKMTO.Text = row["STK/MTO"].ToString();
            txtLM79Life.Text = row["LM79/Life"].ToString();
            txtA4.Text = row["A4"].ToString();
            txtIES.Text = row["IES"].ToString();
            txtUL.Text = row["UL"].ToString();
            txtLDL.Text = row["LDL"].ToString();
            txtEnergyStar.Text = row["EnergyStar"].ToString();
            txtModel.Text = row["Model#"].ToString();
            txtCaution.Text = row["CautionStatement"].ToString();
            txtCountry.Text = row["CountryOfOrigin"].ToString();
            txtPf.Text = row["pf"].ToString();
            txtDateCode.Text = row["DateCode"].ToString();
            txtULFile.Text=row["UL_File#"].ToString();
            txtULControl.Text = row["UL_Control#"].ToString();
            txtULGroup.Text = row["UL_Group"].ToString();
            txtVoltage.Text = row["Voltage"].ToString();
            txtFrequency.Text = row["Frequency"].ToString();
            txtAmperage.Text = row["Amperage"].ToString();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["cmd"] == "omsproduct")
        {
            this.Redirect("/OMS/oms_mstr.aspx?index=1&custCode=" + Request.QueryString["custCode"] + "&custName=" + Request.QueryString["custName"]);
        }
    }
}