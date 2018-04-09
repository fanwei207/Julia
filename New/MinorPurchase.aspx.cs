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
using adamFuncs;
using MinorP;
using Wage;

public partial class new_MinorPurchase : BasePage
{
    adamClass adam = new adamClass();
    MinorPurchase mp = new MinorPurchase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             
            //ExportExcel();
            DropTypedatabind();
            DropDeptdatabind();
        }

    }


    /// <summary>
    ///  初始绑定物品分类数据
    /// </summary>
    protected void DropTypedatabind()
    {
        try
        {
            ListItem item;
            item = new ListItem("--", "0");
            dropType.Items.Add(item);

            DataTable dtType = mp.MinorPType("");
            if (dtType.Rows.Count > 0)
            {
                for (int i = 0; i < dtType.Rows.Count; i++)
                {
                    item = new ListItem(dtType.Rows[i].ItemArray[1].ToString(), dtType.Rows[i].ItemArray[0].ToString());
                    dropType.Items.Add(item);
                }
            }
            dropType.SelectedIndex = 0;

        }
        catch
        {

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


    protected void gvPA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "1")
        {
            Response.Redirect("/new/MinorPOrder.aspx?Appid=" + gvPA.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString());
          
        }
        else
        {
            if (e.CommandName.ToString() == "2")
            {

                if (gvPA.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[13].Text != Convert.ToString(Session["uid"]))
                {
                    ltlAlert.Text = "alert('你没有权利删除这条申请！'); ";
                    return;
                }

                if (mp.DeleteMP(Convert.ToInt32(gvPA.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString()), Convert.ToInt32(Session["uid"])) < 0)
                {
                    ltlAlert.Text = "alert('删除失败，请重新操作！'); ";
                    return;
                }
                else
                {
                    ltlAlert.Text = "alert('删除成功！'); ";
                    gvPA.DataBind();
                }

            }
        }
    }
     
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("/new/MinorPOrder.aspx?Appid=0");
    } 
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvPA.PageIndex = 0; 
       gvPA.DataBind();
    }
}
