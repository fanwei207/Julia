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
using System.IO;
using RD_WorkFlow;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class RDW_SKUEdit : BasePage
{
    RDW rdw = new RDW();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindProductCategory();

            if (Request.QueryString["SKU"] != null)
            {
                txtSKU.Text = Request.QueryString["SKU"].ToString();

                txtSKU.ReadOnly = true;

                SKUHelper skuHelper = new SKUHelper();
                SKU sku = skuHelper.Item(txtSKU.Text.Trim());

                txtUPC.Text = sku.UPC;
                txtVoltage.Text = sku.Voltage.ToString();
                if (txtVoltage.Text == "0")
                {
                    txtVoltage.Text = "--";
                }
                txtWattage.Text = sku.Wattage.ToString();
                if (txtWattage.Text == "0")
                {
                    txtWattage.Text = "--";
                }
                txtLumens.Text = sku.Lumens.ToString();
                if (txtLumens.Text == "0")
                {
                    txtLumens.Text = "--";
                }
                txtLPW.Text = sku.LPW.ToString();
                if (txtLPW.Text == "0")
                {
                    txtLPW.Text = "--";
                }
                txtCBCPest.Text = sku.CBCPest.ToString();
                if (txtCBCPest.Text == "0")
                {
                    txtCBCPest.Text = "--";
                }
                txtBeamAngle.Text = sku.BeamAngle.ToString();
                if (txtBeamAngle.Text == "0")
                {
                    txtBeamAngle.Text = "--";
                }
                txtCCT.Text = sku.CCT.ToString();
                if (txtCCT.Text == "0")
                {
                    txtCCT.Text = "--";
                }
                txtCRI.Text = sku.CRI.ToString();
                if (txtCRI.Text == "0")
                {
                    txtCRI.Text = "--";
                }
                txtSTKorMTO.Text = sku.STKorMTO.ToString();
                txtCaseQty.Text = sku.CaseQty.ToString();
                if (txtCaseQty.Text == "0")
                {
                    txtCaseQty.Text = "--";
                }
                txtUL.Text = sku.UL.ToString();
                dropProductCategory.SelectedItem.Text = sku.ProductCategory.ToString();
                txtLEDChipType.Text = sku.LEDChipType.ToString();
                txtLEDChipQty.Text = sku.LEDChipQty.ToString();
                if (txtLEDChipQty.Text == "0")
                {
                    txtLEDChipQty.Text = "--";
                }
                txtDriverType.Text = sku.DriverType.ToString();
                txtCustomerName.Text = sku.CustomerName.ToString();

                btnNew.Text = "Update";
            }
            else
            {
                btnNew.Text = "New";
            }
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (txtSKU.Text.Trim() == string.Empty)
        {
            this.Alert("The param SKU could not be empty.");
            return;
        }
        else
        {
            if (txtSKU.Text.Trim().Length > 30)
            {
                this.Alert("The length of SKU must be less then 30 characters");
                return;
            }
        }

        if (txtUPC.Text.Trim() != string.Empty)
        {
            if (txtUPC.Text.Trim().Length > 12)
            {
                this.Alert("The length of UPC must be less then 12 characters");
                return;
            }
        }

        if (txtVoltage.Text.Trim() != string.Empty)
        {
            if (txtVoltage.Text.Trim() != "--")
            {
                try
                {
                    Int32 _n = Convert.ToInt32(txtVoltage.Text.Trim());
                }
                catch
                {
                    this.Alert("The param Voltage must be a numeric.");
                    return;
                }
            }
        }

        if (txtWattage.Text.Trim() != string.Empty)
        {
            if (txtWattage.Text.Trim() != "--")
            {
                try
                {
                    Int32 _n = Convert.ToInt32(txtWattage.Text.Trim());
                }
                catch
                {
                    this.Alert("The param Wattage must be a numeric.");
                    return;
                }
            }
        }

        if (txtLumens.Text.Trim() != string.Empty)
        {
            if (txtLumens.Text.Trim() != "--")
            {
                try
                {
                    Int32 _n = Convert.ToInt32(txtLumens.Text.Trim());
                }
                catch
                {
                    this.Alert("The param Lumens must be a numeric.");
                    return;
                }
            }
        }

        if (txtLPW.Text.Trim() != string.Empty)
        {
            if (txtLPW.Text.Trim() != "--")
            {
                try
                {
                    Decimal _n = Convert.ToDecimal(txtLPW.Text.Trim());
                }
                catch
                {
                    this.Alert("The param LPW must be a decimal.");
                    return;
                }
            }
        }

        if (txtCBCPest.Text.Trim() != string.Empty)
        {
            if (txtCBCPest.Text.Trim() != "--")
            {
                try
                {
                    Int32 _n = Convert.ToInt32(txtCBCPest.Text.Trim());
                }
                catch
                {
                    this.Alert("The param CBCPest must be a numeric.");
                    return;
                }
            }
        }

        if (txtBeamAngle.Text.Trim() != string.Empty)
        {
            if (txtBeamAngle.Text.Trim() != "--")
            {
                try
                {
                    Int32 _n = Convert.ToInt32(txtBeamAngle.Text.Trim());
                }
                catch
                {
                    this.Alert("The param Beam Angle must be a numeric.");
                    return;
                }
            }
        }

        if (txtCCT.Text.Trim() != string.Empty)
        {
            if (txtCCT.Text.Trim() != "--")
            {
                try
                {
                    Int32 _n = Convert.ToInt32(txtCCT.Text.Trim());
                }
                catch
                {
                    this.Alert("The param CCT must be a numeric.");
                    return;
                }
            }
        }

        if (txtCRI.Text.Trim() != string.Empty)
        {
            if (txtCRI.Text.Trim() != "--")
            {
                try
                {
                    Int32 _n = Convert.ToInt32(txtCRI.Text.Trim());
                }
                catch
                {
                    this.Alert("The param CRI must be a numeric.");
                    return;
                }
            }
        }

        if (txtSTKorMTO.Text.Trim() != string.Empty)
        {
            if (txtSTKorMTO.Text.ToUpper() != "STK" && txtSTKorMTO.Text.ToUpper() != "MTO")
            {
                this.Alert("valid values of STK or MTO.");
                return;
            }
        }

        if (txtCaseQty.Text.Trim() != string.Empty)
        {
            if (txtCaseQty.Text.Trim() != "--")
            {
                try
                {
                    Int32 _n = Convert.ToInt32(txtCaseQty.Text.Trim());
                }
                catch
                {
                    this.Alert("The param Case Qty must be a numeric.");
                    return;
                }
            }
        }

        if (txtUL.Text.Trim() != string.Empty)
        {
            if (txtUL.Text.Trim().Length > 60)
            {
                this.Alert("The length of UL must be less then 60 characters");
                return;
            }
        }

        if (dropProductCategory.SelectedItem.Text == "LED")
        {
            if (txtLEDChipType.Text.Trim() == string.Empty)
            {
                this.Alert("The param LED Chip Type could not be empty.");
                return;
            }
            else
            {
                if (txtLEDChipType.Text.Trim().Length > 30)
                {
                    this.Alert("The length of LED Chip Type must be less then 30 characters");
                    return;
                }
            }

            if (txtLEDChipQty.Text.Trim() != string.Empty)
            {
                if (txtLEDChipQty.Text.Trim() != "--")
                {
                    try
                    {
                        Int32 _n = Convert.ToInt32(txtLEDChipQty.Text.Trim());
                    }
                    catch
                    {
                        this.Alert("The param LED Chip Qty must be a numeric.");
                        return;
                    }
                }
            }

            if (txtDriverType.Text.Trim() != string.Empty)
            {
                if (txtDriverType.Text.Trim().Length > 30)
                {
                    this.Alert("The length of Driver Type must be less then 30 characters.");
                    return;
                }
            }

        }
        if (txtCustomerName.Text.Trim() != string.Empty)
        {
            if (txtCustomerName.Text.Trim().Length > 30)
            {
                this.Alert("The length of Customer Name must be less then 30 characters");
                return;
            }
        }

        SKUHelper skuHelper = new SKUHelper();
        SKU sku = skuHelper.Item(txtSKU.Text.Trim());

        sku.UPC = txtUPC.Text.Trim();
        if (txtVoltage.Text == string.Empty || txtVoltage.Text == "--")
        {
            sku.Voltage = 0;
        }
        else
        {
            sku.Voltage = Convert.ToInt32(txtVoltage.Text);
        }

        if (txtWattage.Text == string.Empty || txtWattage.Text == "--")
        {
            sku.Wattage = 0;
        }
        else
        {
            sku.Wattage = Convert.ToInt32(txtWattage.Text);
        }

        if (txtLumens.Text == string.Empty || txtLumens.Text == "--")
        {
            sku.Lumens = 0;
        }
        else
        {
            sku.Lumens = Convert.ToInt32(txtLumens.Text);
        }

        //LPW实际就是Lumens除以Wattage的计算结果
        if (sku.Lumens != 0 && sku.Wattage != 0)
        {
            sku.LPW = Convert.ToDecimal((float)sku.Lumens / (float)sku.Wattage);
        }
        else
        {
            sku.LPW = 0;
        }


        if (txtCBCPest.Text == string.Empty || txtCBCPest.Text == "--")
        {
            sku.CBCPest = 0;
        }
        else
        {
            sku.CBCPest = Convert.ToInt32(txtCBCPest.Text);
        }
        if (txtBeamAngle.Text == string.Empty || txtBeamAngle.Text == "--")
        {
            sku.BeamAngle = 0;
        }
        else
        {
            sku.BeamAngle = Convert.ToInt32(txtBeamAngle.Text);
        }
        if (txtCCT.Text == string.Empty || txtCCT.Text == "--")
        {
            sku.CCT = 0;
        }
        else
        {
            sku.CCT = Convert.ToInt32(txtCCT.Text);
        }
        if (txtCRI.Text == string.Empty || txtCCT.Text == "--")
        {
            sku.CRI = 0;
        }
        else
        {
            sku.CRI = Convert.ToInt32(txtCRI.Text);
        }

        if (txtSTKorMTO.Text != string.Empty)
        {
            sku.STKorMTO = (SKUType)Enum.Parse(typeof(SKUType), txtSTKorMTO.Text.ToUpper());
        }

        if (txtCaseQty.Text == string.Empty || txtCaseQty.Text == "--")
        {
            sku.CaseQty = 0;
        }
        else
        {
            sku.CaseQty = Convert.ToInt32(txtCaseQty.Text);
        }

        sku.UL = Convert.ToString(txtUL.Text.Trim());

        sku.ProductCategory = Convert.ToString(dropProductCategory.SelectedItem.Text);
        sku.LEDChipType = Convert.ToString(txtLEDChipType.Text);

        if (txtLEDChipQty.Text == string.Empty || txtLEDChipQty.Text == "--")
        {
            sku.LEDChipQty = 0;
        }
        else
        {
            sku.LEDChipQty = Convert.ToInt32(txtLEDChipQty.Text);
        }
        sku.DriverType = Convert.ToString(txtDriverType.Text);
        sku.CustomerName = Convert.ToString(txtCustomerName.Text);


        if (btnNew.Text == "Update")
        {
            sku.ModifiedUser = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);
            sku.ModifiedDate = DateTime.Now;

            skuHelper.Update(sku);
        }
        else
        {
            sku.CreateUser = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);
            sku.CreateDate = DateTime.Now;

            skuHelper.Add(sku);
        }

        this.Redirect("RDW_SKUMaint.aspx?rm=" + DateTime.Now.ToString());
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Redirect("RDW_SKUMaint.aspx?rm=" + DateTime.Now.ToString());
    }

    private DataTable GetProductCategory()
    {
        string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_rdw_selectProductCategory").Tables[0];
    }

    private void BindProductCategory()
    {
        dropProductCategory.Items.Clear();

        DataTable dt = GetProductCategory();

        dropProductCategory.DataSource = dt;
        dropProductCategory.DataBind();

        dropProductCategory.Items.Insert(0, new ListItem("----", "0"));
    }
}
