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
using Wage;
using MinorP;

public partial class new_MPApplicationUserCh : BasePage
{
    MinorPurchase mp = new MinorPurchase();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            cbl_user.Attributes.Add("onclick", "CheckSelect()");
            DropDeptdatabind();
        }
    }

    /// <summary>
    ///  初始绑定部门数据
    /// </summary>
    protected void DropDeptdatabind()
    {
        try
        {
            ListItem item;
            item = new ListItem("--", "0");
            dropDept.Items.Add(item);

            DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
            if (dtDropDept.Rows.Count > 0)
            {
                for (int i = 0; i < dtDropDept.Rows.Count; i++)
                {
                    item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                    dropDept.Items.Add(item);
                }
            }
            dropDept.SelectedIndex = 0;
        }
        catch
        {

        }
    }

    protected void dropDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadChkList();
    }
    private void LoadChkList()
    {
        if (cbl_user.Items.Count > 0)
            cbl_user.Items.RemoveAt(0);

        DataTable dtUser = mp.ApplicatUser(Convert.ToInt32(dropDept.SelectedValue), txb_user.Text.Trim(), Convert.ToInt32(Session["Plantcode"]));
        if (dtUser.Rows.Count > 0)
        {
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                ListItem item;
                item = new ListItem(dtUser.Rows[i].ItemArray[1].ToString() + "~" + dtUser.Rows[i].ItemArray[0].ToString(), dtUser.Rows[i].ItemArray[0].ToString());
                cbl_user.Items.Add(item);
            }
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        LoadChkList();
    }
}
