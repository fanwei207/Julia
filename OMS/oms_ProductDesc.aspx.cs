using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OMS_oms_ProductDesc : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           this.DataBind();
        }
    }
    /// <summary>
    ///绑定方法
    /// </summary>
    public void DataBind()
    {
        OMSHelper.GridViewDataBind(gvProduct, OMSHelper.GetProductDesc(txtItemNumber.Text.ToString().Trim(), txtUPCNumber.Text.ToString().Trim()));
    }



    protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           // ((LinkButton)e.Row.FindControl("linkDelete")).Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete：\"" + gvProduct.DataKeys[e.Row.RowIndex][1].ToString() + "\"?')");
        }
    }
    protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "productDelete")
        //{ 
        //    if(pdd.deleteProductDeseByID( Convert.ToInt32( e.CommandArgument.ToString().Trim())))
        //    {
        //         ltlAlert.Text = "alert('Delete success');";
        //          this.dataBind();
        //    }
        //    else
        //    {
        //        ltlAlert.Text = "alert('Delete failed');";
        //          this.dataBind();
        //    }
        //}
    }


    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.DataBind();
    }
    protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProduct.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void gvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        if (OMSHelper.DeleteProductDeseByID(Convert.ToInt32(gvProduct.DataKeys[e.RowIndex].Values[0].ToString())))
        {
            ltlAlert.Text = "alert('Delete success');";
        }
        else
        {
            ltlAlert.Text = "alert('Delete failed');";
        }
        this.DataBind();
    }
}