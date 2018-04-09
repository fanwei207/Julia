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
using adamFuncs;
using Portal.Fixas;

public partial class Plan_RepairItem : System.Web.UI.Page
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            BindData();
        }
    }

    protected void BindData()
    {
        //�������
        gv.DataSource = RepairItemHelper.SelectRepairItems(txtName.Text, txtRmks.Text);

        gv.DataBind();
    }

    /// <summary>
    /// ���ݲ���һҳҲ��ʾGridView��ҳ��
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
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvRDW_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void gvRDW_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                TextBox txName = (TextBox)e.Row.Cells[0].Controls[0];
                txName.Attributes.Add("class", "SmallTextBox4");
                txName.Style.Add("width", "100%");

                TextBox txDesc = (TextBox)e.Row.Cells[1].Controls[0];
                txDesc.Attributes.Add("class", "SmallTextBox4");
                txDesc.Style.Add("width", "100%");
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        RepairItem item = new RepairItem();
        item.Name = txtName.Text;
        item.Description = txtRmks.Text;
        item.Creator = new User();
        item.Creator.ID = Convert.ToInt32(Session["uID"].ToString());
        item.Creator.Date = DateTime.Now;

        if (txtName.Text == string.Empty)
        {
            ltlAlert.Text = "alert('���Ʋ���Ϊ�գ�');";
            return;
        }
        else
        {
            if (item.IsExist)
            {
                ltlAlert.Text = "alert('�����Ѿ����ڣ�������ά��һ�����ƣ�');";
                return;
            }
        }

        if (item.Insert)
        {
            ltlAlert.Text = "alert('��Ŀ���ӳɹ���');";

            txtName.Text = string.Empty;
            txtRmks.Text = string.Empty;
        }
        else
        {
            ltlAlert.Text = "alert('��Ŀ����ʧ�ܣ���ˢ�º����²�����');";
        }

        BindData();
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        RepairItem item = new RepairItem();
        item.ID = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["ID"].ToString());

        if (item.Delete)
        {
            ltlAlert.Text = "alert('��Ŀɾ���ɹ���');";
        }
        else
        {
            ltlAlert.Text = "alert('��Ŀɾ��ʧ�ܣ���ˢ�º����²�����');";
        }

        BindData();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;

        BindData();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txName = (TextBox)gv.Rows[e.RowIndex].Cells[0].Controls[0];
        TextBox txDesc = (TextBox)gv.Rows[e.RowIndex].Cells[1].Controls[0];

        RepairItem item = new RepairItem();
        item.ID = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["ID"].ToString());
        item.Name = txName.Text;
        item.Description = txDesc.Text;

        if (item.IsExist)
        {
            ltlAlert.Text = "alert('�����Ѿ����ڣ�������ά��һ�����ƣ�');";
        }
        else
        {
            item.Modifier = new User();
            item.Modifier.ID = Convert.ToInt32(Session["uID"].ToString());
            item.Modifier.Date = DateTime.Now;

            if (item.Update)
            {
                ltlAlert.Text = "alert('��Ŀ���³ɹ���');";
                gv.EditIndex = -1;
            }
            else
            {
                ltlAlert.Text = "alert('��Ŀ����ʧ�ܣ���ˢ�º����²�����');";
            }
        }

        BindData();
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        BindData();
    }
}
