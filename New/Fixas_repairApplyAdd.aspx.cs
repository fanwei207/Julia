using System;
using System.Collections.Generic;
using adamFuncs;
using Portal.Fixas;

public partial class new_Fixas_repairApplyAdd : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { }
    }

    protected void txbFixasNo_TextChanged(object sender, EventArgs e)
    {
        FixedAssets fixas = new FixedAssets(txbFixasNo.Text.Trim(), Convert.ToInt32(Session["plantCode"]));
        if (fixas.IsExists)
        {
            lblFixasNo.Text = fixas.FixasNo;
            lblFixasName.Text = fixas.FixasName;
            lblDomain.Text = fixas.Domain;
            lblCC.Text = fixas.CC;
            lblFixasDesc.Text = fixas.FixasDesc;
            lblFixasTypeID.Text = fixas.FixasTypeID.ToString();
            lblFixasType.Text = fixas.FixasType;
            lblFixasSubTypeID.Text = fixas.FixasSubTypeID.ToString();
            lblFixasSubType.Text = fixas.FixasSubType;
            lblFixasEntity.Text = fixas.FixasEntity;
            lblFixasVouDate.Text = fixas.FixasVouDate;
            lblFixasSupplier.Text = fixas.FixasSupplier;

            if (fixas.FixasSerialNumber == "0" || fixas.FixasSerialNumber == string.Empty)
            {
                ltlAlert.Text = "alert('该固定资产流水号获取失败，请重试！')";
            }
            else
            {
                panelRepairOrder.Visible = true;
                lblRepairOrder.Text = "R-" + fixas.FixasNo + "-" + fixas.FixasSerialNumber;
            }
        }
        else
        {
            panelRepairOrder.Visible = false;
            txbFixasNo.Text = string.Empty;
            lblFixasNo.Text = string.Empty;
            lblFixasName.Text = string.Empty;
            lblDomain.Text = string.Empty;
            lblCC.Text = string.Empty;
            lblFixasDesc.Text = string.Empty;
            lblFixasTypeID.Text = string.Empty;
            lblFixasType.Text = string.Empty;
            lblFixasSubTypeID.Text = string.Empty;
            lblFixasSubType.Text = string.Empty;
            lblFixasEntity.Text = string.Empty;
            lblFixasVouDate.Text = string.Empty;
            lblFixasSupplier.Text = string.Empty;
            ltlAlert.Text = "alert('该资产编号不存在或不属于本公司！')";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (lblFixasNo.Text == string.Empty)
        {
            ltlAlert.Text = "alert('该资产编号不存在！')";
        }
        else if (txbProblemDesc.Text == string.Empty)
        {
            ltlAlert.Text = "alert('问题描述不能为空！')";
        }
        else
        {
            FixasRepair fixasRepair = new FixasRepair();
            fixasRepair.RepairOrder = lblRepairOrder.Text;
            if (!fixasRepair.IsExists)
            {
                fixasRepair.FixasInfo.FixasNo = txbFixasNo.Text.Trim();
                fixasRepair.FixasInfo.FixasName = lblFixasName.Text;
                fixasRepair.FixasInfo.FixasDesc = lblFixasDesc.Text;
                fixasRepair.FixasInfo.FixasTypeID = lblFixasTypeID.Text == string.Empty ? 0 : Convert.ToInt32(lblFixasTypeID.Text);
                fixasRepair.FixasInfo.FixasType = lblFixasType.Text;
                fixasRepair.FixasInfo.FixasSubTypeID = lblFixasSubTypeID.Text == string.Empty ? 0 : Convert.ToInt32(lblFixasSubTypeID.Text);
                fixasRepair.FixasInfo.FixasSubType = lblFixasSubType.Text;
                fixasRepair.FixasInfo.FixasEntity = lblFixasEntity.Text;
                fixasRepair.FixasInfo.FixasVouDate = lblFixasVouDate.Text;
                fixasRepair.FixasInfo.FixasSupplier = lblFixasSupplier.Text;
                fixasRepair.ProblemDesc = txbProblemDesc.Text;
                fixasRepair.ApplyCreator.ID = Convert.ToInt32(Session["uID"]);
                fixasRepair.ApplyCreator.Name = Session["uName"].ToString();
                fixasRepair.ApplyCreator.Date = DateTime.Now;

                if (fixasRepair.InsertApply)
                {
                    Response.Redirect("/new/Fixas_repairApply.aspx?rt=" + DateTime.Now.ToString());
                }
                else
                {
                    ltlAlert.Text = "alert('添加失败！')";
                }
            }
            else
            {
                ltlAlert.Text = "alert('该维修申请已存在！')";
            }
        }
    }

    protected bool IsDate(object val)
    {
        try
        {
            DateTime dt = Convert.ToDateTime(val);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/new/Fixas_repairApply.aspx?rt=" + DateTime.Now.ToString());
    }
}