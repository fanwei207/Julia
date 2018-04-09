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
            ltlAlert.Text = "alert('备件编码不能为空！');";
            return;
        }
        else
        {
            SpareItem si = new SpareItem();
            si.No = txtNo.Text;

            if (!si.IsExist)
            {
                ltlAlert.Text = "alert('备件编码不存在！必须先至<备品备件编号维护>中维护！');";
                return;
            }
        }

        if (txtHoderDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('领用日期不能为空！');";
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
                ltlAlert.Text = "alert('领用日期格式不正确！正确格式为yyyy-MM-dd！');";
                return;
            }
        }

        if (dropUser.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一名领用人！');";
            return;
        }

        if (txtQtyIn.Text == string.Empty)
        {
            ltlAlert.Text = "alert('领用数量不能为空！');";
            return;
        }
        else
        {
            try
            {
                Int32 _d = Convert.ToInt32(txtQtyIn.Text.Trim());

                if (_d == 0)
                {
                    ltlAlert.Text = "alert('无法领用！领用数量不能为0！正数可以领用；负数可以冲回！');";
                    return;
                }

                //领用不能超库存
                SpareItem si = new SpareItem(txtNo.Text);
                si.Floor = dropFloor.SelectedItem.Text;

                int stock = si.Inventory;

                if (stock < _d)
                {
                    ltlAlert.Text = "alert('无法领用！备品备件库存数量为" + stock.ToString() + "，小于领用数量！');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('领用数量的格式不正确，必须是数字！');";
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
            ltlAlert.Text = "alert('领用成功！');";

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
            ltlAlert.Text = "alert('领用失败或备件编号不存在，请刷新后重新操作！');";
        }
    }
    protected void txtNo_TextChanged(object sender, EventArgs e)
    {
        SpareItem item = new SpareItem();

        if (txtNo.Text == string.Empty)
        {
            ltlAlert.Text = "alert('备件编码不能为空！');";
            return;
        }
        else
        {
            item.No = txtNo.Text;

            if (!item.IsExist)
            {
                ltlAlert.Text = "alert('备件编码不存在！必须先至<备品备件编号维护>中维护！');";
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
