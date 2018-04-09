using System;
using System.Collections.Generic;
using adamFuncs;
using Portal.Fixas;

public partial class new_Fixas_repairRecordEdit : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRepairOrder();
        }
    }

    protected void BindRepairOrder()
    {
        IList<FixasRepair> fixasRepairList = new List<FixasRepair>();
        fixasRepairList = FixasRepairHelper.SelectRepairOrder(string.Empty, Request.QueryString["repairOrder"], string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, Convert.ToInt32(Session["plantCode"]), string.Empty);
        foreach (FixasRepair fixasRepair in fixasRepairList)
        {
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

            lblApplyRepairDate.Text = fixasRepair.ApplyRepairDate;
            lblProblemDesc.Text = fixasRepair.ProblemDesc;

            txbRepairedName.Text = fixasRepair.RepairedName;
            if (fixasRepair.RepairAssignedRemark != string.Empty)
            {
                lblRemark1.Visible = true;
                lblRemark2.Visible = true;
                lblRemark2.Text = fixasRepair.RepairAssignedRemark;
            }
            txbReapirRecord.Text = fixasRepair.RepairRecord;
            lblRepairRecord.Text = fixasRepair.RepairRecord;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txbRepairedName.Text == string.Empty)
        {
            ltlAlert.Text = "alert('维修人不能为空！')";
        }

        else if (txbReapirRecord.Text == string.Empty)
        {
            ltlAlert.Text = "alert('维修记录不能为空！')";
        }
        else
        {
            FixasRepair fixasRepair = new FixasRepair();
            fixasRepair.RepairOrder = lblRepairOrder.Text;
            fixasRepair.RepairedName = txbRepairedName.Text;
            fixasRepair.RepairRecord = txbReapirRecord.Text.Trim();
            fixasRepair.RecordModifier.ID = Convert.ToInt32(Session["uID"]);
            fixasRepair.RecordModifier.Name = Session["uName"].ToString();
            if (fixasRepair.UpdateRecord)
            {
                Response.Redirect("/new/Fixas_repairRecord.aspx?rt=" + DateTime.Now.ToString());
            }
            else
            {
                ltlAlert.Text = "alert('保存失败，请重试！')";
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
        Response.Redirect("/new/Fixas_repairRecord.aspx?rt=" + DateTime.Now.ToString());
    }

    protected void btnComplete_Click(object sender, EventArgs e)
    {
        if (lblRepairRecord.Text == string.Empty)
        {
            ltlAlert.Text = "alert('维修记录不能为空！')";
        }
        else if (!IsDate(txbEndRepairDate.Text.Trim()))
        {
            ltlAlert.Text = "alert('维修结束时间为空或格式不正确！')";
        }
        else if (Convert.ToDateTime(lblApplyRepairDate.Text.Trim()) > Convert.ToDateTime(txbEndRepairDate.Text.Trim()))
        {
            ltlAlert.Text = "alert('维修结束时间不能小于申请维修时间！')";
        }
        else if (txbConfirmName.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('确认人不能为空！')";
        }
        else
        {
            FixasRepair fixasRepair = new FixasRepair();
            fixasRepair.RepairOrder = lblRepairOrder.Text;
            fixasRepair.RepairConfirmer.Name = txbConfirmName.Text;
            fixasRepair.RepairEndDate = txbEndRepairDate.Text.Trim();
            if (fixasRepair.ConfirmApply)
            {
                Response.Redirect("/new/Fixas_repairRecord.aspx?rt=" + DateTime.Now.ToString());
            }
            else
            {
                ltlAlert.Text = "alert('完成维修操作失败！')";
            }
        }
    }
}