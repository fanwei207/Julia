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
using RD_WorkFlow;
using System.Text;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
public partial class RDW_RDW_AddStandardStep : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbMID.Text = Request.QueryString["mid"].ToString();
            if (Request.QueryString["id"] != null)
            {
                lbID.Text = Request.QueryString["id"].ToString();

                DataTable dt = getname(lbID.Text);
                txtTitle.Text = dt.Rows[0]["RDW_StepTitle"].ToString().Replace("<br>", "\n");
                txtDescription.Value = dt.Rows[0]["RDW_StepName"].ToString().Replace("<br>", "\n");
                ckb_appv.Checked = Convert.ToBoolean(dt.Rows[0]["RDW_IsApprove"]);
            }
            else
            {
                lbID.Text = "00000000-0000-0000-0000-000000000001";          
            }
        }
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDescription.Value.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('The param Description could not be empty!'); ";
            return;
        }
        else if (txtDescription.Value.Trim().Replace("\r\n", "").Length > 300)
        {
            ltlAlert.Text = "alert('Only 300 characters allowed!'); ";
            return;
        }
       
        try
        {
            string strName = "sp_RDW_InsertStandardStep";
            SqlParameter[] parm = new SqlParameter[6];
            parm[0] = new SqlParameter("@id", lbID.Text);
            parm[1] = new SqlParameter("@name", txtDescription.Value.Trim());
            parm[2] = new SqlParameter("@uID", Session["uID"].ToString());

            parm[3] = new SqlParameter("@isappv", ckb_appv.Checked);
            parm[4] = new SqlParameter("@mstrId", lbMID.Text);
            if (txtTitle.Text.Trim() != "")
            {
                parm[5] = new SqlParameter("@title", txtTitle.Text.Trim());
            }
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, parm);

            Response.Redirect("/RDW/RDW_StandardStep.aspx?mid=" + lbMID.Text + "&rm=" + DateTime.Now.ToString(), true);
        }
        catch
        {
            ltlAlert.Text = "alert('Add data error！'); ";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_StandardStep.aspx?mid=" + lbMID.Text + "&rm=" + DateTime.Now.ToString(), true);
    }
    public DataTable getname(string id)
    {
        string strName = "sp_RDW_SelectStandardStep";
        SqlParameter[] parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@RDW_Code", id);
      

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, parm).Tables[0];
        
    }
}