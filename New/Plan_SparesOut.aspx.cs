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

public partial class Plan_SparesOut : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
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

        dropFloor.DataSource = Budget.getDept(strsql);
        dropFloor.DataBind();
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
        if (txtNo.Text == string.Empty)
        {
            ltlAlert.Text = "alert('�������벻��Ϊ�գ�');";
            return;
        }
        else
        {
            SpareItem si = new SpareItem();
            si.No = txtNo.Text;

            if (!si.IsExist)
            {
                ltlAlert.Text = "alert('�������벻���ڣ���������<��Ʒ�������ά��>��ά����');";
                return;
            }
        }

        if (txtHoderDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('�������ڲ���Ϊ�գ�');";
            return;
        }
        else
        {
            try
            {
                DateTime _time = Convert.ToDateTime(txtHoderDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ����ȷ��ʽΪyyyy-MM-dd��');";
                return;
            }
        }

        if (dropUser.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('��ѡ��һ�������ˣ�');";
            return;
        }

        if (txtQtyIn.Text == string.Empty)
        {
            ltlAlert.Text = "alert('������������Ϊ�գ�');";
            return;
        }
        else
        {
            try
            {
                Int32 _d = Convert.ToInt32(txtQtyIn.Text.Trim());

                if (_d == 0)
                {
                    ltlAlert.Text = "alert('�޷����ã�������������Ϊ0�������������ã��������Գ�أ�');";
                    return;
                }

                //���ò��ܳ����
                SpareItem si = new SpareItem(txtNo.Text);
                si.Floor = dropFloor.SelectedItem.Text;

                int stock = si.Inventory;

                if (stock < _d)
                {
                    ltlAlert.Text = "alert('�޷����ã���Ʒ�����������Ϊ" + stock.ToString() + "��С������������');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('���������ĸ�ʽ����ȷ�����������֣�');";
                return;
            }
        }

        SpareHist item = new SpareHist();
        item.No = txtNo.Text;
        item.Qty = Convert.ToInt32(txtQtyIn.Text.Trim());
        item.Device = txtDevice.Text;
        item.Floor = dropFloor.SelectedItem.Text;

        item.Holder = new User();
        item.Holder.ID = Convert.ToInt32(dropUser.SelectedValue);
        item.Holder.Date = Convert.ToDateTime(txtHoderDate.Text);

        item.Type = SpareType.StockOut;

        item.Creator = new User();
        item.Creator.ID = Convert.ToInt32(Session["uID"].ToString());
        item.Creator.Date = DateTime.Now;

        if (item.Insert)
        {
            ltlAlert.Text = "alert('���óɹ���');";

            txtNo.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txtDevice.Text = string.Empty;
            txtHoderDate.Text = string.Empty;
            dropFloor.SelectedIndex = -1;
            txtCreator.Text = string.Empty;
            txtCreatedDate.Text = string.Empty;
        }
        else
        {
            ltlAlert.Text = "alert('����ʧ�ܻ򱸼���Ų����ڣ���ˢ�º����²�����');";
        }
    }
    protected void txtNo_TextChanged(object sender, EventArgs e)
    {
        SpareItem item = new SpareItem();

        if (txtNo.Text == string.Empty)
        {
            ltlAlert.Text = "alert('�������벻��Ϊ�գ�');";
            return;
        }
        else
        {
            item.No = txtNo.Text;

            if (!item.IsExist)
            {
                ltlAlert.Text = "alert('�������벻���ڣ���������<��Ʒ�������ά��>��ά����');";
                return;
            }
        }

        item = new SpareItem(txtNo.Text);

        txtDesc.Text = item.Description;
        txtDevice.Text = item.Device;

        try
        {
            dropFloor.SelectedIndex = -1;
            dropFloor.Items.FindByText(item.Floor).Selected = true;
        }
        catch
        {
            dropFloor.SelectedIndex = 0;
        }
    }
    protected void dropDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUser();
    }
}
