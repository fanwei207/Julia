using System;
using System.Collections.Generic;
using adamFuncs;
using Portal.Fixas;

public partial class new_Fixas_maintainPlanAdd : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (lblFixasNo.Text == string.Empty)
        {
            ltlAlert.Text = "alert('该资产编号不存在！')";
        }
        else if (!IsDate(txbPlanMaintainDate.Text.Trim()))
        {
            ltlAlert.Text = "alert('计划保养日期为空或格式不对！')";
        }
        else if (Convert.ToDateTime(txbPlanMaintainDate.Text.Trim()) < DateTime.Now)
        {
            ltlAlert.Text = "alert('计划保养日期不能小于当前时间！')";
        }
        else
        {
            FixasMaintain fixasMaintain = new FixasMaintain();
            fixasMaintain.MaintainOrder = lblMaintainOrder.Text;
            if (!fixasMaintain.IsExists)
            {
                fixasMaintain.FixasInfo.FixasNo = lblFixasNo.Text;
                fixasMaintain.FixasInfo.FixasName = lblFixasName.Text;
                fixasMaintain.FixasInfo.FixasDesc = lblFixasDesc.Text;
                fixasMaintain.FixasInfo.FixasTypeID = lblFixasTypeID.Text == string.Empty ? 0 : Convert.ToInt32(lblFixasTypeID.Text);
                fixasMaintain.FixasInfo.FixasType = lblFixasType.Text;
                fixasMaintain.FixasInfo.FixasSubTypeID = lblFixasSubTypeID.Text == string.Empty ? 0 : Convert.ToInt32(lblFixasSubTypeID.Text);
                fixasMaintain.FixasInfo.FixasSubType = lblFixasSubType.Text;
                fixasMaintain.FixasInfo.FixasEntity = lblFixasEntity.Text;

                fixasMaintain.FixasInfo.FixasVouDate = lblFixasVouDate.Text;
                fixasMaintain.FixasInfo.FixasSupplier = lblFixasSupplier.Text;
                fixasMaintain.PlanMaintainDate = txbPlanMaintainDate.Text;
                fixasMaintain.MaintainDesc = txbMaintainDesc.Text;
                fixasMaintain.PlanCreator.ID = Convert.ToInt32(Session["uID"]);
                fixasMaintain.PlanCreator.Name = Session["uName"].ToString();
                fixasMaintain.PlanCreator.Date = DateTime.Now;

                if (fixasMaintain.InsertPlan)
                {
                    ltlAlert.Text = "window.location.href='/new/Fixas_maintainPlan.aspx?rt=" + DateTime.Now.ToString() + "'";
                }
                else
                {
                    ltlAlert.Text = "alert('添加失败！')";
                }
            }
            else
            {
                ltlAlert.Text = "alert('该保养计划已存在！')";
            }
        }
    }

    protected void txbFixasNo_TextChanged(object sender, EventArgs e)
    {
        panelMaintainOrder.Visible = false;

        lblFixasNo.Text = string.Empty;
        lblFixasName.Text = string.Empty;
        lblFixasDesc.Text = string.Empty;
        lblFixasType.Text = string.Empty;
        lblFixasEntity.Text = string.Empty;
        lblFixasVouDate.Text = string.Empty;
        lblFixasSupplier.Text = string.Empty;

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
            lblFixasVouDate.Text = string.Format("{0:yyyy-MM-dd}", fixas.FixasVouDate);
            lblFixasSupplier.Text = fixas.FixasSupplier;

            if (fixas.FixasSerialNumber == "0" || fixas.FixasSerialNumber == string.Empty)
            {
                ltlAlert.Text = "alert('该固定资产流水号获取失败，请重试！')";
            }
            else
            {
                panelMaintainOrder.Visible = true;
                lblMaintainOrder.Text = "M-" + fixas.FixasNo + "-" + fixas.FixasSerialNumber;
            }
        }
        else
        {
            txbFixasNo.Text = string.Empty;
            lblFixasNo.Text = string.Empty;
            ltlAlert.Text = "alert('该资产编号不存在或不属于本公司！')";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (lblFixasNo.Text == string.Empty)
        {
            ltlAlert.Text = "alert('该资产编号不存在！')";
        }
        else if (!IsDate(txbPlanMaintainDate.Text.Trim()))
        {
            ltlAlert.Text = "alert('计划保养日期为空或格式不对！')";
        }
        else if (Convert.ToDateTime(txbPlanMaintainDate.Text.Trim()) < DateTime.Now)
        {
            ltlAlert.Text = "alert('计划保养日期不能小于当前时间！')";
        }
        else
        {
            FixasMaintain fixasMaintain = new FixasMaintain();
            fixasMaintain.MaintainOrder = lblMaintainOrder.Text;
            fixasMaintain.PlanMaintainDate = txbPlanMaintainDate.Text;
            fixasMaintain.MaintainDesc = txbMaintainDesc.Text;
            fixasMaintain.PlanModifier.ID = Convert.ToInt32(Session["uID"]);
            fixasMaintain.PlanModifier.Name = Session["uName"].ToString();
            fixasMaintain.PlanCreator.Date = DateTime.Now;
            if (fixasMaintain.UpdatePlan)
            {
                ltlAlert.Text = "window.location.href='/new/Fixas_maintainPlan.aspx?rt=" + DateTime.Now.ToString() + "'";
            }
            else
            {
                ltlAlert.Text = "alert('更新失败！')";
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/new/Fixas_maintainPlan.aspx?rt=" + DateTime.Now.ToString());
    }
}