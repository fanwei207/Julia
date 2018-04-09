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
            gvSort.Rows[0].Cells[0].Text = "û������";
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
            ltlAlert.Text = "alert('���Ʋ���Ϊ��!');";
            return;
        }

        if (btnAdd.Text == "����")
        {
            nRet = wf.AddNodeSort(txtSortName.Text.Trim(), Convert.ToInt32(Session["uID"]));
            if (nRet == 0)
            {
                ltlAlert.Text = "alert('�����Ѿ����ڣ����������!');";
            }
            else if (nRet == 1)
            {
                txtSortName.Text = string.Empty;
                ltlAlert.Text = "alert('��ӳɹ�!');";
            }
            else
            {
                ltlAlert.Text = "alert('���ʧ��,����ϵ����Ա!');";
            }
        }
        else
        {
            nRet = wf.UpdateNodeSort(Convert.ToInt32(lbID.Text.Trim()), txtSortName.Text.Trim());
            if(nRet == 0)
            {
                ltlAlert.Text = "alert('�����Ѿ����ڣ����������!');";
            }
            else if(nRet == 1)
            {
                txtSortName.Text = string.Empty;
                btnAdd.Text = "����";
                lbID.Text = "0";
                ltlAlert.Text = "alert('����ɹ�!');";
            }
            else
            {
                ltlAlert.Text = "alert('����ʧ��,����ϵ����Ա!');";
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
            ltlAlert.Text = "alert('ֻ�н���ű�����ļ�¼ȫ��ɾ��������ɾ��������¼!');";
        }
        else if (nRet == -1)
        {
            ltlAlert.Text = "alert('ɾ��ʧ��,����ϵ����Ա!');";
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
                btnAdd.Text = "�༭";
            }
            reader.Close();
        }
    }
}
