using System;
using System.Collections.Generic;
using adamFuncs;
using Portal.Fixas;

public partial class new_Fixas_repairApplyEdit : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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

                txbProblemDesc.Text = fixasRepair.ProblemDesc;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txbProblemDesc.Text == string.Empty)
        {
            ltlAlert.Text = "alert('问题描述不能为空！')";
        }
        else
        {
            FixasRepair fixasRepair = new FixasRepair();
            fixasRepair.RepairOrder = lblRepairOrder.Text;
            fixasRepair.ProblemDesc = txbProblemDesc.Text.Trim();
            fixasRepair.ApplyModifier.ID = Convert.ToInt32(Session["uID"]);
            fixasRepair.ApplyModifier.Name = Session["uName"].ToString();
            fixasRepair.ApplyModifier.Date = DateTime.Now;

            if (fixasRepair.UpdateApply)
            {
                Response.Redirect("/new/Fixas_repairApply.aspx?rt=" + DateTime.Now.ToString());
            }
            else
            {
                ltlAlert.Text = "alert('更新失败！')";
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/new/Fixas_repairApply.aspx?rt=" + DateTime.Now.ToString());
    }
}