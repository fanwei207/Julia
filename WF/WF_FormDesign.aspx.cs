using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WF_WF_FormDesign : System.Web.UI.Page
{
    private WorkFlow helper = new WorkFlow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        string id = Request.QueryString["FlowId"];
        gvlist.DataSource = helper.GetFormCols(id);
        gvlist.DataBind();
    }

    public bool check(object value)
    {
        try
        {
            return Convert.ToBoolean(value);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int id = int.Parse(Request.QueryString["FlowId"]);
        DataTable dt = helper.GetFlowNode(id);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl = ((DropDownList)e.Row.FindControl("ddlNode"));
            string nodeId = gvlist.DataKeys[e.Row.RowIndex].Values["Sort_Order"].ToString();
            if (ddl != null)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "Node_Name";
                ddl.DataValueField = "Sort_Order";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--", "0"));
                ddl.SelectedValue = nodeId;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "Flow_ID";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "Sort_Order";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "ColName";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "Label";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Boolean");
        column.ColumnName = "Required";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "Sort";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "CreatedBy";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "CreatedDate";
        table.Columns.Add(column);

        foreach (GridViewRow gvRow in gvlist.Rows)
        {
            CheckBox chkShow = gvRow.FindControl("chkShow") as CheckBox;
            if (chkShow != null && chkShow.Checked)
            {
                TextBox txtLabel = gvRow.FindControl("txtLabel") as TextBox;
                TextBox txtSort = gvRow.FindControl("txtSort") as TextBox;
                int sort = 0;
                if (string.IsNullOrEmpty(txtLabel.Text.Trim()))
                {
                    ltlAlert.Text = "alert('第" + (gvRow.RowIndex + 1).ToString() + "行的标签名称为空！');";
                    return;
                }
                else if (int.TryParse(txtSort.Text.Trim(), out sort))
                {
                    row = table.NewRow();
                    row["Flow_ID"] = int.Parse(Request.QueryString["FlowId"]);
                    int nodeId = 0;
                    DropDownList ddl = gvRow.FindControl("ddlNode") as DropDownList;
                    if (ddl != null)
                    {
                        nodeId = int.Parse(ddl.SelectedValue);
                    }
                    row["Sort_Order"] = nodeId;
                    row["ColName"] = gvlist.DataKeys[gvRow.RowIndex].Values["name"].ToString();
                    row["Label"] = txtLabel.Text; 
                    row["Required"] = (gvRow.FindControl("chkReq") as CheckBox).Checked;
                    row["Sort"] = sort;
                    row["CreatedBy"] = Convert.ToInt32(Session["uID"]);
                    row["CreatedDate"] = DateTime.Now.ToString();
                    table.Rows.Add(row);
                }
                else
                {
                    ltlAlert.Text = "alert('第" + (gvRow.RowIndex + 1).ToString() + "行的排序不是数字！');";
                    return;
                }
            }
        }

        try
        {
            helper.SaveFormDesign(Request.QueryString["FlowId"], Session["uID"].ToString(), table);
            BindData();
            ltlAlert.Text = "alert('保存成功！');";
        }
        catch (Exception ex)
        {
            ltlAlert.Text = "alert('保存失败！');";
        }

    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("WF_WorkFlowTemplateList.aspx?rm=" + DateTime.Now);
    }
}