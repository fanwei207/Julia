using System;
using System.Collections.Generic;
using adamFuncs;
using Portal.Fixas;

public partial class new_Fixas_repairApplyAssign : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["repairOrder"] != null)
        {
            FixasRepair fixasRepair = new FixasRepair();
            IList<FixasRepair> fixasRepairList = FixasRepairHelper.SelectRepairOrder(string.Empty, Request.QueryString["repairOrder"], string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, Convert.ToInt32(Session["plantCode"]), string.Empty);
            foreach (FixasRepair repair in fixasRepairList)
            {
                fixasRepair = repair;
            }
            lblRepairOrder.Text = fixasRepair.RepairOrder;
            lblFixasNo.Text = fixasRepair.FixasInfo.FixasNo;
            lblFixasName.Text = fixasRepair.FixasInfo.FixasName;
            lblDomain.Text = fixasRepair.FixasInfo.Domain;
            lblCC.Text = fixasRepair.FixasInfo.CC;
            lblFixasDesc.Text = fixasRepair.FixasInfo.FixasDesc;
            lblFixasType.Text = fixasRepair.FixasInfo.FixasType;
            lblFixasSubType.Text = fixasRepair.FixasInfo.FixasSubType;
            lblFixasEntity.Text = fixasRepair.FixasInfo.FixasEntity;
            lblFixasVouDate.Text = fixasRepair.FixasInfo.FixasVouDate;
            lblFixasSupplier.Text = fixasRepair.FixasInfo.FixasSupplier;

            lblApplyRepaiDate.Text = fixasRepair.ApplyRepairDate;
            lblProblemDesc.Text = fixasRepair.ProblemDesc;
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        if (lblFixasNo.Text == string.Empty)
        {
            ltlAlert.Text = "alert('该资产编号不存在！')";
        }
        else if (txbAssignedToName.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请指派一个维修人！')";
        }
        else
        {
            FixasRepair fixasRepair = new FixasRepair();
            fixasRepair.RepairOrder = lblRepairOrder.Text;
            fixasRepair.FixasInfo.FixasNo = lblFixasNo.Text;
            fixasRepair.RepairAssigner.ID = Convert.ToInt32(Session["uID"]);
            fixasRepair.RepairAssigner.Name = Session["uName"].ToString();
            fixasRepair.RepairAssigner.Date = DateTime.Now;
            fixasRepair.RepairAssignedToName = txbAssignedToName.Text.Trim();
            fixasRepair.RepairAssignedRemark = txbAssignedRemark.Text.Trim();

            if (fixasRepair.AssignApply)
            {
                Response.Redirect("/new/Fixas_repairApply.aspx?rt=" + DateTime.Now.ToString());
            }
            else
            {
                ltlAlert.Text = "alert('指派失败！');";
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/new/Fixas_repairApply.aspx?rt=" + DateTime.Now.ToString());
    }
}