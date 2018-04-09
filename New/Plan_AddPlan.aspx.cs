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
using adamFuncs;
using Portal.Fixas;
using TCPNEW;
using BudgetProcess;

public partial class Plan_AddPlan : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            dropRepairItem.DataSource = RepairItemHelper.SelectAllRepairItems();
            dropRepairItem.DataBind();
            dropRepairItem.Items.Insert(0, new ListItem("--", "--"));

            txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            txtCreator.Text = Session["uName"].ToString();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtFixasNo.Text == string.Empty)
        {
            ltlAlert.Text = "alert('资产编码不能为空！');";
            return;
        }

        if (txtPlanDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('计划日期不能为空！');";
            return;
        }
        else
        {
            try
            {
                DateTime _time = Convert.ToDateTime(txtPlanDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('计划日期格式不正确！正确格式为yyyy-MM-dd！');";
                return;
            }
        }

        if (dropRepairItem.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一项维修&保养项目！');";
            return;
        }

        Plan plan = new Plan();
        plan.FixasNo = txtFixasNo.Text;
        plan.Date = Convert.ToDateTime(txtPlanDate.Text);

        plan.RepairItem = new RepairItem();
        plan.RepairItem.ID = Convert.ToInt32(dropRepairItem.SelectedItem.Value);
        plan.RepairItem.Name = dropRepairItem.SelectedItem.Text;

        if (plan.IsExist)
        {
            ltlAlert.Text = "alert('该计划已经存在，请重新维护！');";
            return;
        }

        plan.Creator = new User();
        plan.Creator.ID = Convert.ToInt32(Session["uID"].ToString());
        plan.Creator.Date = DateTime.Now;

        if (plan.Insert)
        {
            ltlAlert.Text = "alert('计划增加成功！');";

            txtFixasNo.Text = string.Empty;
            txtFixasNo.Text = string.Empty;
            txtFixasName.Text = string.Empty;
            txtFixasDesc.Text = string.Empty;
            txtType.Text = string.Empty;
            txtEntity.Text = string.Empty;
            txtFixasVouDate.Text = string.Empty;
            txtFixasSupplier.Text = string.Empty;
            txtPlanDate.Text = string.Empty;
        }
        else
        {
            ltlAlert.Text = "alert('计划增加失败或资产编号不存在，请刷新后重新操作！');";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Plan_Plans.aspx?rm=" + DateTime.Now.ToString());
    }
    protected void txtFixasNo_TextChanged(object sender, EventArgs e)
    {
        if (txtFixasNo.Text != string.Empty)
        {
            if (PlanHelper.IsFixasAlreadyExist(txtFixasNo.Text))
            {
                Plan plan = new Plan(txtFixasNo.Text);

                txtFixasName.Text = plan.FixasName;
                txtFixasDesc.Text = plan.FixasDesc;
                txtType.Text = plan.FixasType;
                txtEntity.Text = plan.FixasEntity;
                txtFixasVouDate.Text = string.Format("{0:yyyy-MM-dd}", plan.FixasVouDate);
                txtFixasSupplier.Text = plan.FixasSupplier;
            }
            else
            {
                ltlAlert.Text = "alert('输入的资产编码不存在，请重新输入！');";

                txtFixasDesc.Text = string.Empty;
                txtFixasName.Text = string.Empty;
                txtType.Text = string.Empty;
                txtEntity.Text = string.Empty;
                txtFixasVouDate.Text = string.Empty;
                txtFixasSupplier.Text = string.Empty;

                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('资产编码不能为空！');";
            return;
        }
    }
}
