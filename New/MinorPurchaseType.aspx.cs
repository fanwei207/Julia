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

public partial class new_MinorPurchaseType : BasePage
{
    adamClass adam = new adamClass();
    MinorPurchase mp = new MinorPurchase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //ExportExcel();
        }
    }
     
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (mp.SaveMPType(txtPartType.Text.Trim()) >= 0)
        {
            ltlAlert.Text = "alert('����ɹ���'); ";
            txtPartType.Text = "";
            gvMP.DataBind();
        }
        else
        {
            ltlAlert.Text = "alert('�������ݹ����г���'); ";
        }

    }

    //protected void ExportExcel()
    //{

    //}
    protected void gvMP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) != 0)
            {

                TextBox txtM = (TextBox)e.Row.Cells[0].Controls[0];
                txtM.Width = Unit.Pixel(200);
            }
        }
    }

    protected void gvMP_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            //TextBox txtM = (TextBox)e.Row.Cells[0].Controls[0]; 
            TextBox txtM = (TextBox)gvMP.Rows[e.RowIndex].Cells[0].Controls[0];
            if (txtM.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('���Ʋ���Ϊ�գ�'); ";
                return;
            }
             
            ltlAlert.Text = "alert('�޸����ݳɹ���'); ";
            gvMP.DataBind(); 
        }
        catch
        {
            ltlAlert.Text = "alert('�������ݳ���'); ";
        }
    }

    protected void gvMP_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        { 
            ltlAlert.Text = "alert(''ɾ�����ݳɹ���'); ";
            gvMP.DataBind(); 
        }
        catch
        {
            ltlAlert.Text = "alert('ɾ�����ݳ���'); ";
        }
    }

    protected void gvMP_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.gvMP.EditIndex = e.NewEditIndex;
        this.gvMP.DataBind();
    }
}
