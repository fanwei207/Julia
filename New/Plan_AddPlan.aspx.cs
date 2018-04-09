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
            ltlAlert.Text = "alert('�ʲ����벻��Ϊ�գ�');";
            return;
        }

        if (txtPlanDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('�ƻ����ڲ���Ϊ�գ�');";
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
                ltlAlert.Text = "alert('�ƻ����ڸ�ʽ����ȷ����ȷ��ʽΪyyyy-MM-dd��');";
                return;
            }
        }

        if (dropRepairItem.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('��ѡ��һ��ά��&������Ŀ��');";
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
            ltlAlert.Text = "alert('�üƻ��Ѿ����ڣ�������ά����');";
            return;
        }

        plan.Creator = new User();
        plan.Creator.ID = Convert.ToInt32(Session["uID"].ToString());
        plan.Creator.Date = DateTime.Now;

        if (plan.Insert)
        {
            ltlAlert.Text = "alert('�ƻ����ӳɹ���');";

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
            ltlAlert.Text = "alert('�ƻ�����ʧ�ܻ��ʲ���Ų����ڣ���ˢ�º����²�����');";
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
                ltlAlert.Text = "alert('������ʲ����벻���ڣ����������룡');";

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
            ltlAlert.Text = "alert('�ʲ����벻��Ϊ�գ�');";
            return;
        }
    }
}
