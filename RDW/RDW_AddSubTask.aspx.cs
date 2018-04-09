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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;


public partial class RDW_AddSubTask : BasePage
{
    RDW rdw = new RDW();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                lbID.Text = Request.QueryString["id"].ToString();
            }
            else
            {
                ltlAlert.Text = "alert('system error.please!'); ";
                return;
            }

            lbMID.Text = Request.QueryString["mid"].ToString(); 
        }
    }

   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDescription.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('The param Description could not be empty!'); ";
            return;
        }
        if(Convert.ToInt32(txtDuration.Text) < 0)
        {
            ltlAlert.Text = "alert('The Duration must be bigger than  0!'); ";
            return;
        }

        if (lbID.Text != "0")
        {
            string strName = "select RDW_Duration From Rdw_Step Where RDW_StepID = " + lbID.Text;

            if (Convert.ToInt32(txtDuration.Text) > Convert.ToInt32(SqlHelper.ExecuteScalar(System.Configuration.ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"], CommandType.Text, strName)))
            {
                ltlAlert.Text = "alert('Duration Of child Step Cannot bigger than parent Step!'); ";
                return;
            }
        }
 
        try
        {
            string strName = "sp_RDW_InsertSubTasks";
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@id", lbID.Text);
            parm[1] = new SqlParameter("@mid", lbMID.Text);
            parm[2] = new SqlParameter("@name", txtDescription.Text.Trim()); 
            parm[3] = new SqlParameter("@uID", Session["uID"].ToString());
            parm[4] = new SqlParameter("@duration", txtDuration.Text == "" ? "0" : txtDuration.Text);

            SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, parm);

            ltlAlert.Text = "window.location.href='/RDW/RDW_step.aspx?mid=" + lbMID.Text + "&rm=" + DateTime.Now.ToString() + "';";
        }
        catch
        {
            ltlAlert.Text = "alert('Add data error！'); ";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("RDW_step.aspx?mid=" + lbMID.Text + "&rm=" + DateTime.Now.ToString(), true);
    }
}
