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
using System.Data.SqlClient;

public partial class WF_NodeSort :BasePage
{
    WorkFlow wf = new WorkFlow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            databind();
        }
    }

    protected void databind()
    {
        DataTable dt = wf.GetNodeSort();
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            this.gvSort.DataSource = dt;
            this.gvSort.DataBind();
            int ColunmCount = gvSort.Rows[0].Cells.Count;
            gvSort.Rows[0].Cells.Clear();
            gvSort.Rows[0].Cells.Add(new TableCell());
            gvSort.Rows[0].Cells[0].ColumnSpan = ColunmCount;
            gvSort.Rows[0].Cells[0].Text = "没有数据";
            gvSort.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            this.gvSort.DataSource = dt;
            this.gvSort.DataBind();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int nRet = 0;
        if (txtSortName.Text == string.Empty)
        {
            ltlAlert.Text = "alert('名称不能为空!');";
            return;
        }

        if (btnAdd.Text == "增加")
        {
            nRet = wf.AddNodeSort(txtSortName.Text.Trim(), Convert.ToInt32(Session["uID"]));
            if (nRet == 0)
            {
                ltlAlert.Text = "alert('名称已经存在，请更换名称!');";
            }
            else if (nRet == 1)
            {
                txtSortName.Text = string.Empty;
                ltlAlert.Text = "alert('添加成功!');";
            }
            else
            {
                ltlAlert.Text = "alert('添加失败,请联系管理员!');";
            }
        }
        else
        {
            nRet = wf.UpdateNodeSort(Convert.ToInt32(lbID.Text.Trim()), txtSortName.Text.Trim());
            if(nRet == 0)
            {
                ltlAlert.Text = "alert('名称已经存在，请更换名称!');";
            }
            else if(nRet == 1)
            {
                txtSortName.Text = string.Empty;
                btnAdd.Text = "增加";
                lbID.Text = "0";
                ltlAlert.Text = "alert('保存成功!');";
            }
            else
            {
                ltlAlert.Text = "alert('保存失败,请联系管理员!');";
            }
        }
        databind();
    }

    protected void gvSort_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvSort.DataKeys[e.RowIndex].Value.ToString());
        int nRet = wf.DeleteNodeSort(id);
        if (nRet == 0)
        {
            ltlAlert.Text = "alert('只有将序号比它大的记录全部删除，才能删除该条记录!');";
        }
        else if (nRet == -1)
        {
            ltlAlert.Text = "alert('删除失败,请联系管理员!');";
        }
        else
        {

        }
        databind();
    }

    protected void gvSort_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }

    protected void gvSort_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myEdit")
        {
            SqlDataReader reader = wf.GetNodeSortByID(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            if (reader.Read())
            {
                lbID.Text = e.CommandArgument.ToString().Trim();
                txtSortName.Text = Convert.ToString(reader["Sort_Name"]);
                btnAdd.Text = "编辑";
            }
            reader.Close();
        }
    }
}
