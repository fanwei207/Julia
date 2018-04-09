using System;
using System.Collections.Generic;
using adamFuncs;
using Portal.Fixas;

public partial class new_Fixas_maintainRecordEdit : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMaintainOrder();
        }
    }

    protected void BindMaintainOrder()
    {
        IList<FixasMaintain> fixasMaintainList = new List<FixasMaintain>();
        fixasMaintainList = FixasMaintainHelper.SelectMaintainOrder(string.Empty, Request.QueryString["MaintainOrder"], string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, Convert.ToInt32(Session["plantCode"]), string.Empty);
        foreach (FixasMaintain fixasMaintain in fixasMaintainList)
        {
            lblMaintainOrder.Text = fixasMaintain.MaintainOrder;

            lblFixasNo.Text = fixasMaintain.FixasInfo.FixasNo;
            lblFixasName.Text = fixasMaintain.FixasInfo.FixasName;
            lblDomain.Text = fixasMaintain.FixasInfo.Domain;
            lblCC.Text = fixasMaintain.FixasInfo.CC;
            lblFixasDesc.Text = fixasMaintain.FixasInfo.FixasDesc;
            lblFixasType.Text = fixasMaintain.FixasInfo.FixasType;
            lblFixasSubType.Text = fixasMaintain.FixasInfo.FixasSubType;
            lblFixasEntity.Text = fixasMaintain.FixasInfo.FixasEntity;
            lblFixasVouDate.Text = string.Format("{0:yyyy-MM-dd}", fixasMaintain.FixasInfo.FixasVouDate);

            lblFixasSupplier.Text = fixasMaintain.FixasInfo.FixasSupplier;

            lblPlanMaintainDate.Text = string.Format("{0:yyyy-MM-dd}", fixasMaintain.PlanMaintainDate);
            lblMaintainDesc.Text = fixasMaintain.MaintainDesc;

            txbMaintainedName.Text = fixasMaintain.MaintainedName;
            txbMaintainBeginDate.Text = fixasMaintain.MaintainBeginDate;
            txbMaintainEndDate.Text = fixasMaintain.MaintainEndDate;
            txbMaintainRecord.Text = fixasMaintain.MaintainedRecord;
            lblMaintainRecord.Text = fixasMaintain.MaintainedRecord;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txbMaintainedName.Text == string.Empty)
        {
            ltlAlert.Text = "alert('保养人不能为空！')";
        }
        else if (!IsDate(txbMaintainBeginDate.Text.Trim()) || !IsDate(txbMaintainEndDate.Text.Trim()))
        {
            ltlAlert.Text = "alert('保养开始时间、保养结束时间为空或格式不对！')";
        }
        else if (Convert.ToDateTime(txbMaintainBeginDate.Text.Trim()) > Convert.ToDateTime(txbMaintainEndDate.Text.Trim()))
        {
            ltlAlert.Text = "alert('保养结束时间不能小于保养开始时间！')";
        }
        else if (txbMaintainRecord.Text == string.Empty)
        {
            ltlAlert.Text = "alert('保养记录不能为空！')";
        }
        else
        {
            FixasMaintain fixasMaintain = new FixasMaintain();
            fixasMaintain.MaintainOrder = lblMaintainOrder.Text;
            fixasMaintain.MaintainedName = txbMaintainedName.Text;
            fixasMaintain.MaintainBeginDate = txbMaintainBeginDate.Text.Trim();
            fixasMaintain.MaintainEndDate = txbMaintainEndDate.Text.Trim();
            fixasMaintain.MaintainedRecord = txbMaintainRecord.Text;
            fixasMaintain.MaintainModifier.ID = Convert.ToInt32(Session["uID"]);
            fixasMaintain.MaintainModifier.Name = Session["uName"].ToString();
            if (fixasMaintain.UpdateRecord)
            {
                ltlAlert.Text = "window.location.href='/new/Fixas_maintainRecord.aspx?rt=" + DateTime.Now.ToString() + "'";
            }
            else
            {
                ltlAlert.Text = "alert('保存失败，请重试！')";
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/new/Fixas_maintainRecord.aspx?rt=" + DateTime.Now.ToString());
    }

    protected void btnComplete_Click(object sender, EventArgs e)
    {
        if (lblMaintainRecord.Text == string.Empty)
        {
            ltlAlert.Text = "alert('保养记录为空！')";
        }
        else if (txbConfirmName.Text == string.Empty)
        {
            ltlAlert.Text = "alert('确认人不为为空！')";
        }
        else
        {
            FixasMaintain fixasMaintain = new FixasMaintain();
            fixasMaintain.MaintainOrder = lblMaintainOrder.Text;
            fixasMaintain.PlanConfirmer.Name = txbConfirmName.Text.Trim();
            if (fixasMaintain.ConfirmPlan)
            {
                ltlAlert.Text = "window.location.href='/new/Fixas_maintainRecord.aspx?rt=" + DateTime.Now.ToString() + "'";
            }
            else
            {
                ltlAlert.Text = "alert('完成保养失败！')";
            }
        }
    }
}