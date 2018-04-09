using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class NWF_nwf_workflowInstanceDetail : BasePage
{
    private NewWorkflow helper = new NewWorkflow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFlowNode();
            BindData();
        }


    }

    private void BindFlowNode()
    {
        string userId = Session["uID"].ToString();
        string flowId = Request.QueryString["FlowId"];
        DataTable dtNode = helper.GetFlowNode(flowId);
        menuNode.Items.Clear();
        foreach (DataRow row in dtNode.Rows)
        {
            MenuItem item = new MenuItem();
            item.Text = row["Node_Name"].ToString();
            item.Value = row["Node_Id"].ToString();
            menuNode.Items.Add(item);
        }
        if (Request.QueryString["NodeId"] == null)
        {
            menuNode.Items[0].Selected = true;
        }
        else
        {
            menuNode.FindItem(Request.QueryString["NodeId"]).Selected = true;
        }
    }

    private void BindData()
    {
        string field = Request.QueryString["QueryField"];
        string value = Request.QueryString["QueryValue"];
        hidCheck.Value = ";";
        helper.InitGridViewReview(gvDet, menuNode.SelectedItem.Value, false,Session["uID"].ToString());
        gvDet.PageIndexChanging+=new GridViewPageEventHandler(gvDet_PageIndexChanging);
        gvDet.RowDataBound += new GridViewRowEventHandler(gv_RowDataBound);
        DataTable dt = helper.GetFormData(menuNode.SelectedItem.Value, txtCondition.Text.Trim(), field, value, Session["uID"].ToString());
        gvDet.DataSource = dt;
        gvDet.DataBind();
    }

    protected void ddlWorkFlow_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFlowNode();
        BindData();
    }
    protected void menuNode_MenuItemClick(object sender, MenuEventArgs e)
    {
        BindData();
    }
    protected void linkDownload_Click(object sender, EventArgs e)
    {
        string field = Request.QueryString["QueryField"];
        string value = Request.QueryString["QueryValue"];
        DataTable dtHeader = helper.GetFormDesignReview(menuNode.SelectedItem.Value,Session["uID"].ToString());
        StringBuilder header = new StringBuilder();
        foreach (DataRow row in dtHeader.Rows)
        {
            header.Append("<b>").Append(row["label"].ToString()).Append("</b>~^");
        }
        DataTable dtData = helper.GetFormData(menuNode.SelectedItem.Value, txtCondition.Text.Trim(), field, value, Session["uID"].ToString());

        this.ExportExcel(header.ToString(), dtData, true);
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    string returnUrl = Request.QueryString["returnUrl"];
    //    if (returnUrl != null)
    //    {
    //        returnUrl = HttpUtility.UrlDecode(returnUrl);
    //        Response.Redirect(returnUrl, true);
    //    }
    //}
    protected void gvDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gv = sender as GridView;
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = sender as GridView;
            if (gv.Attributes["DetailPage"].ToString().Trim() != "")
            {
                StringBuilder url = new StringBuilder(gv.Attributes["DetailPage"]);
                if (gv.DataKeyNames.Length > 0)
                {
                    url.Append("?");
                    foreach (string key in gv.DataKeyNames)
                    {
                        url.Append(key).Append("=");
                        string value = gv.DataKeys[e.Row.RowIndex].Values[key].ToString();
                        url.Append(value).Append("&");
                    }
                    url.Remove(url.Length - 1, 1);
                }
                e.Row.Attributes.Add("ondblclick", "$.window('明细',1000,800,'" + url.ToString() + "');");
            }
        }
    }
}