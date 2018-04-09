using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class price_pc_chooseApplyPreson : BasePage
{
    PC_price pc = new PC_price();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCompany();

            ddl_Company.SelectedIndex = -1;
            try
            {
                ddl_Company.Items.FindByValue(Session["PlantCode"].ToString()).Selected = true;
            }
            catch
            {
                ddl_Company.SelectedIndex = -1;
            }

            BindDepartment();
            BindApproveUser();
        }
    }

    private void BindCompany()
    {
        ddl_Company.DataSource = admin_AccessApply.getCompanyInfo();
        ddl_Company.DataBind();
    }

    private void BindDepartment()
    {
        ddl_department.DataSource = admin_AccessApply.getDepartmentInfo(Convert.ToInt32(ddl_Company.SelectedValue.ToString()));
        ddl_department.DataBind();
    }

    private void BindApproveUser()
    {
        LoadUser(Convert.ToInt32(ddl_department.SelectedValue));
    }


    protected void ddl_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartment();
    }

    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUser(Convert.ToInt32(ddl_department.SelectedValue));
    }
    private void LoadUser(int departmentId)
    {
        while (radioBtnList_SelecetApprove.Items.Count > 0)
        {
            radioBtnList_SelecetApprove.Items.RemoveAt(0);
        }

        DataTable dt = admin_AccessApply.getUserByDepart(departmentId, txt_user.Text.ToString(), ddl_Company.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            for (int irow = 0; irow < dt.Rows.Count; irow++)
            {
                ListItem item = new ListItem(dt.Rows[irow]["namemail"].ToString());
                item.Value = dt.Rows[irow]["userID"].ToString();
                radioBtnList_SelecetApprove.Items.Add(item);
            }
        }
        dt.Reset();

    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindApproveUser();
    }

    protected void radioBtnList_SelecetApprove_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtApproveID.Text = string.Empty;
        txtApproveName.Text = string.Empty;
        txtApproveEmail.Text = string.Empty;

        txtApproveID.Text = radioBtnList_SelecetApprove.SelectedValue.ToString();
        txtApproveName.Text = radioBtnList_SelecetApprove.SelectedItem.Text.Substring(0, radioBtnList_SelecetApprove.SelectedItem.Text.IndexOf('~'));    //Substring(radioBtnList_SelecetApprove.SelectedItem.Text.IndexOf('-') + 1, radioBtnList_SelecetApprove.SelectedItem.Text.Length);
        int startindex = radioBtnList_SelecetApprove.SelectedItem.Text.IndexOf('~') + 1;
        txtApproveEmail.Text = radioBtnList_SelecetApprove.SelectedItem.Text.Substring(startindex, radioBtnList_SelecetApprove.SelectedItem.Text.Length - startindex);
    }

    protected void btn_close_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.close();";
    }
    protected void btn_sure_Click(object sender, EventArgs e)
    {
        if (pc.addApplyPerson(txtApproveID.Text,Convert.ToInt32(Session["uID"])))
        {
            ltlAlert.Text = "添加成功";
        }
        else
        {
            ltlAlert.Text = "添加失败";
        }
        ltlAlert.Text = "window.close();";
    }
}