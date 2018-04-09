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

public partial class new_MPOrderRecieve : BasePage
{
    adamClass adam = new adamClass();
    MinorPurchase mp = new MinorPurchase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             
            DropTypedatabind();
            DropDeptdatabind();


            table2.Visible = false;
            table3.Visible = false;
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
            dropDp.Items.Add(item);

            DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
            if (dtDropDept.Rows.Count > 0)
            {
                for (int i = 0; i < dtDropDept.Rows.Count; i++)
                {
                    item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                    dropDept.Items.Add(item);
                    dropDp.Items.Add(item);
                }
            }
            dropDept.SelectedIndex = 0;
            dropDp.SelectedIndex = 0;
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
        gvMPRv.PageIndex = 0;
        gvMPRv.DataBind();
    }

    protected void gvMPRv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "1")
        {
            table1.Visible = false;
            gvMPRv.Visible = false;

            table2.Visible = true;
            table3.Visible = true;

            DataTable dtInfor = mp.MinorPList(Convert.ToInt32(Session["Plantcode"]), 0, Convert.ToInt32(gvMPRv.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString()));
            if (dtInfor.Rows.Count > 0)
            {
                lblApper.Text = dtInfor.Rows[0].ItemArray[3].ToString();
                lblDept.Text = dtInfor.Rows[0].ItemArray[4].ToString();
                lblType.Text = dtInfor.Rows[0].ItemArray[6].ToString();
                lblQuantity.Text = dtInfor.Rows[0].ItemArray[7].ToString();
                lblprice.Text = dtInfor.Rows[0].ItemArray[9].ToString();
                lblPart.Text = dtInfor.Rows[0].ItemArray[5].ToString();
                lblSP.Text = dtInfor.Rows[0].ItemArray[10].ToString();

                lblAid.Text = dtInfor.Rows[0].ItemArray[0].ToString();

                lbltotal.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtInfor.Rows[0].ItemArray[7]) * Convert.ToDecimal(dtInfor.Rows[0].ItemArray[9].ToString()), 3));

            }
        }
        else
        {
            if (e.CommandName.ToString() == "2")
            {
                ltlAlert.Text = "window.open('MPPrint.aspx?mid=" + gvMPRv.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString()  + "','','menubar=No,scrollbars = No,resizable=No,width=473,height=500,top=200,left=300');";
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("MPOrderRecieve.aspx");
    }



    protected void btnR_Click(object sender, EventArgs e)
    {
        if (txtQtyR.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须填写数量！'); ";
            txtQtyR.Focus();
            SaveStatus();
            return;
            
        }
        else
        {
            try
            {
                decimal decQ = Convert.ToDecimal(txtQtyR.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('数量填写不正确，请重新输入！'); ";
                txtQtyR.Focus();
                SaveStatus();
                return;
              
            }
        }
 

        if (mp.SaveMpRecieve(Convert.ToDecimal(txtQtyR.Text.Trim()), Convert.ToInt32(dropDp.SelectedValue), Convert.ToInt32(lblAid.Text), Convert.ToInt32(Session["uid"])) < 0)
        {
            ltlAlert.Text = "alert('操作失败！'); ";
            return;
        }
        else
        {
            ltlAlert.Text = "alert('保存成功！'); ";
            Response.Redirect("MPOrderRecieve.aspx");
        }
    } 
    private void SaveStatus()
    {
        table1.Visible = false;
        gvMPRv.Visible = false;

        table2.Visible = true;
        table3.Visible = true;
    }
}
