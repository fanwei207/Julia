using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class NWF_NWF_FormDesign : System.Web.UI.Page
{
    private NewWorkflow helper = new NewWorkflow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        if (Request.QueryString["NodeId"] != null)
        {
          
            string id = Request.QueryString["NodeId"];
            gvSource.DataSource = helper.GetSourceTableCols(id);
            gvSource.DataBind();

            gvlist.DataSource = helper.GetFormCols(id);
            gvlist.DataBind();
        }
        else
        {
            string id = Request.QueryString["FlowId"];
            gvSource.DataSource = helper.GetSourceTableColsByFlow(id);
            gvSource.DataBind();

            gvlist.DataSource = helper.GetFormColsByFlow(id);
            gvlist.DataBind();
        }
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable sourceTable = new DataTable();
        DataTable table = new DataTable();
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Guid");
        column.ColumnName = "Node_ID";
        sourceTable.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Guid");
        column.ColumnName = "Flow_ID";
        sourceTable.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "ColName";
        sourceTable.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "Label";
        sourceTable.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Boolean");
        column.ColumnName = "PK";
        sourceTable.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Boolean");
        column.ColumnName = "Query";
        sourceTable.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Boolean");
        column.ColumnName = "hid";
        sourceTable.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "Sort";
        sourceTable.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "CreatedBy";
        sourceTable.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "CreatedDate";
        sourceTable.Columns.Add(column);

        foreach (GridViewRow gvRow in gvSource.Rows)
        {
            CheckBox chkShow = gvRow.FindControl("chkShow") as CheckBox;
            if (chkShow != null && chkShow.Checked)
            {
                TextBox txtLabel = gvRow.FindControl("txtLabel") as TextBox;
                TextBox txtSort = gvRow.FindControl("txtSort") as TextBox;
                int sort = 0;
                if (string.IsNullOrEmpty(txtLabel.Text.Trim()))
                {
                    ltlAlert.Text = "alert('源表设置第" + (gvRow.RowIndex + 1).ToString() + "行的标签名称为空！');";
                    return;
                }
                else if (int.TryParse(txtSort.Text.Trim(), out sort))
                {
                    row = sourceTable.NewRow();
                    if (Request.QueryString["NodeId"] != null)
                    {
                        row["Node_ID"] = Guid.Parse(Request.QueryString["NodeId"]);
                    }
                    else
                    {
                        row["Flow_ID"] = Guid.Parse(Request.QueryString["FlowId"]);
                    }
                    row["ColName"] = gvSource.DataKeys[gvRow.RowIndex].Values["name"].ToString();
                    row["Label"] = txtLabel.Text;
                    row["PK"] = (gvRow.FindControl("chkPK") as CheckBox).Checked;
                    row["Query"] = (gvRow.FindControl("chkQuery") as CheckBox).Checked;
                    row["hid"] = (gvRow.FindControl("chkhid") as CheckBox).Checked;
                    row["Sort"] = sort;
                    row["CreatedBy"] = Convert.ToInt32(Session["uID"]);
                    row["CreatedDate"] = DateTime.Now.ToString();
                    sourceTable.Rows.Add(row);
                }
                else
                {
                    ltlAlert.Text = "alert('源表设置第" + (gvRow.RowIndex + 1).ToString() + "行的排序不是数字！');";
                    return;
                }
            }
        }

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Guid");
        column.ColumnName = "Node_ID";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Guid");
        column.ColumnName = "Flow_ID";
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
        column.DataType = System.Type.GetType("System.Boolean");
        column.ColumnName = "ReadOnly";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "Sort";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Boolean");
        column.ColumnName = "hid";
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
                    ltlAlert.Text = "alert('表单设置第" + (gvRow.RowIndex + 1).ToString() + "行的标签名称为空！');";
                    return;
                }
                else if (int.TryParse(txtSort.Text.Trim(), out sort))
                {
                    row = table.NewRow();
                    if (Request.QueryString["NodeId"] != null)
                    {
                        row["Node_ID"] = Guid.Parse(Request.QueryString["NodeId"]);
                    }
                    else
                    {
                        row["Flow_ID"] = Guid.Parse(Request.QueryString["FlowId"]);
                    }
                    row["ColName"] = gvlist.DataKeys[gvRow.RowIndex].Values["name"].ToString();
                    row["Label"] = txtLabel.Text;
                    row["Required"] = (gvRow.FindControl("chkReq") as CheckBox).Checked;
                    row["ReadOnly"] = (gvRow.FindControl("chkReadOnly") as CheckBox).Checked;
                    row["hid"] = (gvRow.FindControl("chkhid") as CheckBox).Checked;
                    row["Sort"] = sort;
                    row["CreatedBy"] = Convert.ToInt32(Session["uID"]);
                    row["CreatedDate"] = DateTime.Now.ToString();
                    table.Rows.Add(row);
                }
                else
                {
                    ltlAlert.Text = "alert('表单设置第" + (gvRow.RowIndex + 1).ToString() + "行的排序不是数字！');";
                    return;
                }
            }
        }

        try
        {
            helper.SaveFormDesign(Request.QueryString["NodeId"], Request.QueryString["FlowId"], Session["uID"].ToString(), sourceTable, table);
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
        Response.Redirect("NWF_FlowNode.aspx?id="+Request.QueryString["FlowId"]+"&rm=" + DateTime.Now);
    }
}