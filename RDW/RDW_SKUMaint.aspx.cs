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
using System.IO;
using RD_WorkFlow;

public partial class RDW_SKUMaint : BasePage
{
    RDW rdw = new RDW();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //"170015", USA
            if (!this.Security["170015"].isValid)
            {
                chk.Checked = false;//chk用来记录是否有编辑SKU的权限，只要是想在处理绑定的时候，不要重复读取数据库而设置的中间变量
            }
            else
            {
                chk.Checked = true;
            }

            btnNew.Visible = chk.Checked;

            gvRDW.Columns[12].Visible = chk.Checked;

            BindGridView();
        }
    }

    protected override void BindGridView()
    {
        SKUHelper skuHelper = new SKUHelper();

        gvRDW.DataSource = skuHelper.Items(txtSKU.Text);

        gvRDW.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRDW_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvRDW_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRDW.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        gvRDW.AllowPaging = false;
        gvRDW.Columns[9].Visible = true;
        gvRDW.Columns[10].Visible = true;
        BindGridView();
        gvRDW.Columns[8].Visible = false;
        gvRDW.Width = 1200;

        string style = @"<style> .text { mso-number-format:\@; word-break:keep-all; word-wrap:normal; }  </script> ";
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "GB2312";  
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=Project.xls");
        //Response.ContentType = "application/excel";
        Response.ContentType = "application/vnd.xls";
        this.EnableViewState = false; 

        StringWriter sw = new StringWriter();

        HtmlTextWriter htw = new HtmlTextWriter(sw);

        gvRDW.RenderControl(htw);

        // Style is added dynamically
        Response.Write(style);
        Response.Write(sw.ToString());
        Response.End();

        gvRDW.AllowPaging = true;
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("RDW_SKUEdit.aspx");
    }
    protected void gvRDW_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Operation")
        {
            this.Redirect("RDW_SKUEdit.aspx?sku=" + e.CommandArgument.ToString());
        }
    }
    protected void gvRDW_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!chk.Checked)
            {
                LinkButton linkSKU = (LinkButton)e.Row.Cells[0].FindControl("linkSKU");

                e.Row.Cells[0].Text = linkSKU.Text;
            }
            //e.Row.Cells[13].Text
            if (e.Row.Cells[13].Text != Session["uName"].ToString() && e.Row.Cells[13].Text != Session["eName"].ToString())
            {
                ((LinkButton)e.Row.Cells[12].Controls[0]).Enabled = false;
            }

            for (int i = 1; i <= 9; i++)
            {
                if (e.Row.Cells[i].Text == "0")
                {
                    e.Row.Cells[i].Text = "";
                }
            }
        }
    }
    protected void gvRDW_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
     
        SKUHelper skuHelper = new SKUHelper();
        SKU sku = new SKU(gvRDW.DataKeys[e.RowIndex].Values["SKU"].ToString());
        // Need to fix bug that deleting template before delete sku data.
        skuHelper.Delete(sku);

        BindGridView(); 
    }
}
