using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_pc_ApplyPersonList : System.Web.UI.Page
{
    PC_price pc = new PC_price();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
    }
    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnDelete")
        {
            int userID= Convert.ToInt32(e.CommandArgument);
            if (pc.deleteApplyPerson(userID))
            {
                ltlAlert.Text = "alert('删除成功');";
                bind();
            }
            else
            {
                ltlAlert.Text = "alert('删除失败');";
            }

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('pc_chooseApplyPreson.aspx','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
        bind();
    }

    private void bind()
    {
        gvInfo.DataSource = pc.selectApplyPerson();
        gvInfo.DataBind();
    }

}