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

public partial class new_MPOrderSummary : BasePage
{
    adamClass adam = new adamClass();
    MinorPurchase mp = new MinorPurchase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropTypedatabind();
            DropDeptdatabind();

            ExportExcel();
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


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvSummary.PageIndex = 0;
        gvSummary.DataBind();
        ExportExcel();
    }


    private void ExportExcel()
    { 
        Session["EXSQL"] = mp.MPSummaryString(txtStart.Text.Trim(), txtEnd.Text.Trim(), Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropType.SelectedValue), txtSupplier.Text.Trim(), rbAll.Checked ? 1 : (rbRe.Checked ? 2 : 3), Convert.ToInt32(Session["plantcode"]), Convert.ToInt32(Session["uid"]), 0, 0);
        Session["EXTitle"] = mp.MPSummaryString("", "", 0, 0, "", rbAll.Checked ? 1 : (rbRe.Checked ? 2 : 3), Convert.ToInt32(Session["plantcode"]), Convert.ToInt32(Session["uid"]), 1, 0);
    }
}
