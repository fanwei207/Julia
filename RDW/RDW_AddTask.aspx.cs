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

public partial class RDW_AddTask : BasePage
{
    RDW rdw = new RDW();
    RD_Steps step = new RD_Steps();
    string strConn = System.Configuration.ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            lbMID.Text = Request.QueryString["mid"].ToString();
            BindTempSteps();
            BindTask();
            if (Request.QueryString["id"] != null)
            {
                lbID.Text = Request.QueryString["id"].ToString();
                 
                DataSet ds = step.SelectTaskById(Convert.ToInt32(lbMID.Text), Convert.ToInt32(lbID.Text));
                try
                {
                    ddl_task.SelectedValue = ds.Tables[0].Rows[0]["RDW_Code"].ToString();
                }
                catch
                {
                    ddl_task.SelectedValue = "0";
                }
                txt_desc.Text = ds.Tables[0].Rows[0]["RDW_StepName"].ToString();
                ddl_Predecessor.SelectedValue = ds.Tables[0].Rows[0]["RDW_Predecessor"].ToString();
                chk_isExtra.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["RDW_extraStep"]);
                txtDuration.Text = ds.Tables[0].Rows[0]["RDW_Duration"].ToString();
                if (ds.Tables[0].Rows[0]["RDW_ParentID"].ToString() != "0")
                {
                    tr1.Visible = false;
                }
            }
            else
            {
                lbID.Text = "0"; 
            }
            
            
        }
    }
    protected void BindTask()
    {
        string sql = "sp_RDW_SelectStandardStep";
        ddl_task.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql);
        ddl_task.DataBind();
        ListItem item = new ListItem("---default--- ", "0");
        ddl_task.Items.Insert(0, item);
    }

    protected void BindTempSteps()
    {
        DataTable dt;
        dt = step.getTempSteps(lbMID.Text);
        
        ddl_Predecessor.DataSource = dt;
        ddl_Predecessor.DataBind();
        ListItem item = new ListItem("---default--- ", "0");
        ddl_Predecessor.Items.Insert(0, item);
    }

     
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDuration.Text == "") txtDuration.Text = "0";
        //if (ddl_task.SelectedValue == "0")
        //{
        //    ltlAlert.Text = "alert('The StandardStep could not be empty!'); ";
        //    return;
        //}
        if (txt_desc.Text.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('The Description could not be empty!'); ";
            return;
        }
        if ( Convert.ToInt32(txtDuration.Text) <0)
        {
            ltlAlert.Text = "alert('The Duration must be bigger than 0!'); ";
            return;
        }
        if(lbID.Text != "0")
        {
            string strName = "sp_RDW_SelectParentDuration";

            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@id", lbID.Text);
            parm[1] = new SqlParameter("@parentID", DbType.Int32);
            parm[1].Direction = ParameterDirection.Output;
            parm[2] = new SqlParameter("@ParentDuration", DbType.Int32);
            parm[2].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strName, parm);
            if (Convert.ToInt32(txtDuration.Text) > Convert.ToInt32(parm[2].Value) && parm[1].Value.ToString() != "0")
            {
                ltlAlert.Text = "alert('Duration Of child Step Cannot bigger than parent Step!'); ";
            return;
            }
        }
        try
        {
            string strName = "sp_RDW_InsertTasks";
            SqlParameter[] parm = new SqlParameter[8];
            parm[0] = new SqlParameter("@id", lbID.Text);
            parm[1] = new SqlParameter("@mid", lbMID.Text);
            parm[2] = new SqlParameter("@name", txt_desc.Text.Trim());
            parm[3] = new SqlParameter("@uID", Session["uID"].ToString());
            parm[4] = new SqlParameter("@Predecessor", Convert.ToInt32(ddl_Predecessor.SelectedValue));
            parm[5] = new SqlParameter("@isExtra", chk_isExtra.Checked);
            parm[6] = new SqlParameter("@Duration", txtDuration.Text == "" ? "0" : txtDuration.Text);
            if (ddl_task.SelectedValue != "0")
            {
                parm[7] = new SqlParameter("@Code", ddl_task.SelectedValue);
            }

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, parm);

            ltlAlert.Text = "window.location.href='/RDW/RDW_step.aspx?mid=" + lbMID.Text + "&rm=" + DateTime.Now.ToString() + "';";
        }
        catch
        {
            ltlAlert.Text = "alert('Add data error！'); ";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_step.aspx?mid=" + lbMID.Text + "&rm=" + DateTime.Now.ToString(), true);
    }
}
