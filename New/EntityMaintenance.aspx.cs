//summary
//     Author :   Simon
//Create Date :   May 12 ,2009
//Description :   Maintenance the Entity in basic information module for fix asset. .
//summary

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
using Microsoft.ApplicationBlocks.Data;
using TCPNEW;


public partial class new_EntityMaintenance : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }

    //summary
    //Added and modified  record for Entity
    //summary
    protected void btnSaveEntity_Click(object sender, EventArgs e)
    {
        if (txtEntity.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('���� ����Ϊ�գ�');";
            return;
        }

        ProgressDataTcp.SaveAndModifyEntity(txtEntity.Text,adam.sqlEncode(txtComment.Text ),Convert.ToInt32(lblEntityID.Text) ,Convert.ToInt32(Session["uid"]));

        gvEntity.DataBind();
        txtEntity.Text = "";
        txtComment.Text="";
        if (lblEntityID.Text == "0")
        {
            ltlAlert.Text = "alert('����ɹ���');";
            return;
        }
        else
        {
            ltlAlert.Text = "alert('�޸ĳɹ���');";
            return;
        }
            //Page.RegisterStartupScript("Message", "<script>alert('!');</script>");

       Response.Redirect("EntityMaintenance.aspx");
        
    }

    protected void MyRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            try
            { 
                int intIndex = Convert.ToInt32(e.CommandArgument.ToString());
                txtEntity.Text = gvEntity.Rows[intIndex].Cells[0].Text;
                //Ҳ������  ������ 2012-6-19
                txtComment.Text = (gvEntity.Rows[intIndex].Cells[1].Text == "&nbsp;") ? "" : gvEntity.Rows[intIndex].Cells[1].Text;

                //lblEntityID ��ʲô�� �����ģ�2012-9-19
                lblEntityID.Text = gvEntity.DataKeys[intIndex].Value.ToString ();
                
                gvEntity.DataBind();
            }
            
            catch
            {
                return;
            }
        }
    }

}
