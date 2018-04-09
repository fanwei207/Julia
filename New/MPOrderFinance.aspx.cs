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

public partial class new_MPOrderFinance : BasePage
{
    adamClass adam = new adamClass();
    MinorPurchase mp = new MinorPurchase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            DropTypedatabind();
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
            //dropDp.Items.Add(item);

            DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
            if (dtDropDept.Rows.Count > 0)
            {
                for (int i = 0; i < dtDropDept.Rows.Count; i++)
                {
                    item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                    dropDept.Items.Add(item);
                    //dropDp.Items.Add(item);
                }
            }
            dropDept.SelectedIndex = 0;
            //dropDp.SelectedIndex = 0;
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
        gvMPFin.PageIndex = 0;
        gvMPFin.DataBind();
    }


    protected void gvMPFin_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "1")
        {
            table1.Visible = false;

            gvMPFin.Visible = false;

            table2.Visible = true;
            table3.Visible = true;

            DataTable dtInfor =mp.MPFinanceSelect( Convert.ToInt32(gvMPFin.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString()),Convert.ToInt32(Session["Plantcode"]));
            if (dtInfor.Rows.Count > 0)
            {
                //Response.Write("pass");
                lblApper.Text = dtInfor.Rows[0].ItemArray[3].ToString();
                lblDept.Text = dtInfor.Rows[0].ItemArray[4].ToString();
                lblType.Text = dtInfor.Rows[0].ItemArray[6].ToString();
                lblQuantity.Text = dtInfor.Rows[0].ItemArray[7].ToString();
                lblprice.Text = dtInfor.Rows[0].ItemArray[9].ToString();
                lblPart.Text = dtInfor.Rows[0].ItemArray[5].ToString();
                lblSP.Text = dtInfor.Rows[0].ItemArray[10].ToString();

                lblAid.Text = dtInfor.Rows[0].ItemArray[0].ToString();

                lbltotal.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtInfor.Rows[0].ItemArray[7]) * Convert.ToDecimal(dtInfor.Rows[0].ItemArray[9].ToString()), 3));

                lblRqty.Text = dtInfor.Rows[0].ItemArray[11].ToString();
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MPOrderFinance.aspx");
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtFinNum.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须填写发票号！'); ";
            txtFinNum.Focus();
            return;
        }

        if (txtFinprice.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须填写发票单价！'); ";
            txtFinprice.Focus();
            return;
        }
        else
        {
            try
            {
                decimal decQ = Convert.ToDecimal(txtFinprice.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('单价填写不正确，请重新输入！'); ";
                txtFinprice.Focus();
                return;
            }
        }



        if (txtFinqty.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须填写发票数量！'); ";
            txtFinqty.Focus();
            return;
        }
        else
        {
            try
            {
                decimal decQ = Convert.ToDecimal(txtFinqty.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('数量填写不正确，请重新输入！'); ";
                txtFinqty.Focus();
                return;
            }
        }


        if (mp.SaveFinance(txtFinNum.Text.Trim(), Convert.ToDecimal(txtFinprice.Text.Trim()), Convert.ToDecimal(txtFinqty.Text.Trim()), Convert.ToInt32(lblAid.Text.Trim()), Convert.ToInt32(Session["plantcode"]), Convert.ToInt32(Session["uid"])) < 0)
        {
            ltlAlert.Text = "alert('核销有误，请重新操作！'); ";
            return;
        }
        else
        {
            ltlAlert.Text = "alert('核销成功！'); ";
            Response.Redirect("MPOrderFinance.aspx");
        }

    }
}
