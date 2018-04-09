//summary
//     Author :   Simon
//Create Date :   April 26 ,2009
//Description :   Maintenance the piece work procedure.
//summary

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using TCPNEW;
public partial class new_PieceWorkprocedureTypeMaintenance : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    //summary
    //Adding a new record for workprocedure
    //summary
    protected void btnSaveWorkproceName_Click(object sender, EventArgs e)
    {
        if (txtWorkproceName.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('名称 不能为空！');";
            return;
        }
        
        ProgressDataTcp.SaveWorkPro(adam.sqlEncode(txtWorkproceName.Text));
        gvWorkproceType.DataBind();
        txtWorkproceName.Text = "";
    }

    //summary
    //Delete a record in gridview
    //summary
    protected void MyRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            {
                ProgressDataTcp.DeleteWorkpro(Convert.ToInt32(e.CommandArgument));
                gvWorkproceType.DataBind();
            }
            catch
            {
                return;
            }
        }
    }
    protected void MyRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            //gvWorkproceType.Rows[e.RowIndex].Cells[1].Text
            ProgressDataTcp.UpdateWorkpro(Convert.ToInt32(e.Keys), adam.sqlEncode(Convert.ToString(e.NewValues[0])));
            Response.Write(Convert.ToString(e.NewValues[0]) + "<br>");
            TextBox txttemp = (TextBox)gvWorkproceType.Rows[e.RowIndex].FindControl("txtViewWorkpro");
            Response.Write(txttemp.Text);
            gvWorkproceType.DataBind();
        }
        catch
        {
            //Response.Write(gvWorkproceType.Rows[e.RowIndex].Cells[1].Text.ToString );
        }
    }
}

