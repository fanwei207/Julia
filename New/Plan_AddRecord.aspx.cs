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

public partial class Plan_AddRecord : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
              
            dropRepairItem.DataSource = RepairItemHelper.SelectAllRepairItems();
            dropRepairItem.DataBind();
            dropRepairItem.Items.Insert(0, new ListItem("--", "--"));

            BindDepartment();
            BindUser();

            txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            txtCreator.Text = Session["uName"].ToString();
        }
    }
    protected void BindDepartment()
    {
        if (dropDept.Items.Count > 0) dropDept.Items.Clear();

        string strsql = "Select 0 As departmentID, '--' As Name Union Select departmentID, name From tcpc" + Session["PlantCode"].ToString() + ".dbo.departments where issalary=1 Order By Name ";

        dropDept.DataSource = Budget.getDept(strsql);
        dropDept.DataBind();
    }
    protected void BindUser()
    {
        if (dropUser.Items.Count > 0) dropUser.Items.Clear();

        int plant = Convert.ToInt32(Session["PlantCode"].ToString());
        int dept = Convert.ToInt32(dropDept.SelectedValue);
        int org = 1;

        dropUser.DataSource = Budget.getUser(plant, dept, org);
        dropUser.DataBind();

        dropUser.Items.Insert(0, new ListItem("--", "0"));
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtFixasNo.Text == string.Empty)
        {
            ltlAlert.Text = "alert('资产编码不能为空！');";
            return;
        }

        if (dropUser.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一个维护人！');";
            return;
        }

        if (txtMaintDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('维护日期不能为空！');";
            return;
        }
        else
        {
            try
            {
                DateTime _time = Convert.ToDateTime(txtMaintDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('维护日期格式不正确！正确格式为yyyy-MM-dd！');";
                return;
            }
        }

        if (dropRepairItem.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一项维修&保养项目！');";
            return;
        }

        if (txtMoney.Text.Trim() != string.Empty)
        {
            try
            {
                Double _d = Convert.ToDouble(txtMoney.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('维护金额的格式不正确，必须是数字！');";
                return;
            }
        }

        Record record = new Record();
        record.FixasNo = txtFixasNo.Text;

        record.Maintor = new User();
        record.Maintor.ID = Convert.ToInt32(dropUser.SelectedValue);
        record.Maintor.Date = Convert.ToDateTime(txtMaintDate.Text);

        record.RepairItem = new RepairItem();
        record.RepairItem.ID = Convert.ToInt32(dropRepairItem.SelectedItem.Value);
        record.RepairItem.Name = dropRepairItem.SelectedItem.Text;

        if (txtMoney.Text.Trim() == string.Empty)
        {
            record.Money = 0.0;
        }
        else
        {
            record.Money = Convert.ToDouble(txtMoney.Text.Trim());
        }

        record.Creator = new User();
        record.Creator.ID = Convert.ToInt32(Session["uID"].ToString());
        record.Creator.Date = DateTime.Now;

        if (record.Insert)
        {
            ltlAlert.Text = "alert('记录增加成功！');";

            txtFixasNo.Text = string.Empty;
            txtFixasNo.Text = string.Empty;
            txtFixasName.Text = string.Empty;
            txtFixasDesc.Text = string.Empty;
            txtType.Text = string.Empty;
            txtEntity.Text = string.Empty;
            txtFixasVouDate.Text = string.Empty;
            txtFixasSupplier.Text = string.Empty;
            txtMaintDate.Text = string.Empty;
            txtMoney.Text = string.Empty;
        }
        else
        {
            ltlAlert.Text = "alert('记录增加失败或资产编号不存在，请刷新后重新操作！');";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Plan_Records.aspx?rm=" + DateTime.Now.ToString());
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

                txtFixasNo.Text = string.Empty;
                txtFixasNo.Text = string.Empty;
                txtFixasName.Text = string.Empty;
                txtFixasDesc.Text = string.Empty;
                txtType.Text = string.Empty;
                txtEntity.Text = string.Empty;
                txtFixasVouDate.Text = string.Empty;
                txtFixasSupplier.Text = string.Empty;
                txtMaintDate.Text = string.Empty;
                txtMoney.Text = string.Empty;

                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('资产编码不能为空！');";
            return;
        }
    }
    protected void dropDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUser();
    }
}
