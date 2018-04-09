using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;

public partial class Performance_new_choose : System.Web.UI.Page
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadCompany();
            dropDepartment.Items.Insert(0, new ListItem("--选择部门--", "0"));
        }
    }

    protected void loadCompany()
    {
        string strSQL = "SELECT plantID,description From tcpc0.dbo.plants where isAdmin=0 order by plantID";

        DataSet ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, strSQL);

        dropCompany.DataSource = ds;
        dropCompany.DataBind();
        dropCompany.Items.Insert(0, new ListItem("--选择公司--", "0"));
    }

    protected void loadDepartment()
    {
        dropDepartment.Items.Clear();

        string strSQL = "SELECT departmentID,name From tcpc" + dropCompany.SelectedValue + ".dbo.departments where issalary=1 order by name";

        DataSet ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, strSQL);

        dropDepartment.DataSource = ds;
        dropDepartment.DataBind();

        dropDepartment.Items.Insert(0, new ListItem("--选择部门--", "0"));
    }

    protected void loadUser()
    {
        rblUsers.Items.Clear();

        string strSQL = "SELECT userID, userInfo = userName + '~' + userno + '~' + Isnull(r.roleName, N'')";
        strSQL += " From tcpc0.dbo.users u";
        strSQL += " Left Join Roles r On r.roleID = u.roleID";
        strSQL += " Where plantCode = " + dropCompany.SelectedValue + " and deleted = 0 and isactive = 1 and leavedate is null";
        
        if (dropDepartment.SelectedIndex > 0)
        {
            strSQL += " and DepartmentID = " + dropDepartment.SelectedValue;
        }

        if (txtUser.Text.Trim().Length > 0)
        {
            strSQL += " and username like N'%" + txtUser.Text.Trim() + "%'";
        }

        strSQL += " Order By UserName";

        DataSet ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, strSQL);

        rblUsers.DataSource = ds;
        rblUsers.DataBind();
    }

    protected void dropCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDepartment();
    }
    protected void dropDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUser();
    }
    protected void rblUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in rblUsers.Items)
        {
            if (item.Selected)
            {
                txtChoose.Text = item.Text;
                txtChooseid.Text = item.Value;
                txtUserNo.Text = item.Text.Substring(item.Text.IndexOf("~") + 1, item.Text.LastIndexOf("~") - item.Text.IndexOf("~") - 1);
                txtUserName.Text = item.Text.Substring(0, item.Text.IndexOf("~"));
                txtRole.Text = item.Text.Substring(item.Text.LastIndexOf("~") + 1);

                txtCompany.Text = dropCompany.SelectedItem.Text;
                txtDept.Text = dropDepartment.SelectedItem.Text;
            }
        }
    }
}