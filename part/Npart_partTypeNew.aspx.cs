using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class part_Npart_partTypeNew : BasePage
{
    Npart_help help = new Npart_help();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        if (string.Empty.Equals(txtNewModleName.Text.Trim()))
        {
            Alert("请输入模板名");
            return;
        }

        if ("0".Equals(ddlType.SelectedValue))
        {
            Alert("类型");
            return;
        }

        int flag = help.createNewType(txtNewModleName.Text.Trim(), ddlType.SelectedValue, Session["uID"].ToString(), Session["uName"].ToString());

        if (flag == -1)
        {
            Alert("存在相同的模板名");
        }
        else if (flag == 0)
        {
            Alert("新增失败，请联系管理员");
        }
        else
        {
            Alert("新增成功");
            Response.Redirect("Npart_typeManage.aspx?typeName=" + txtNewModleName.Text.Trim());
        }
    }
    protected void btnRetuen_Click(object sender, EventArgs e)
    {
        Response.Redirect("Npart_typeManage.aspx");
    }
}