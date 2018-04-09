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
    RD_Steps step = new RD_Steps();

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
            LoadddlSiblingStep();

            #region Amber Liu
            //设置默认起始时间 
            txtStartDate.Text = Convert.ToDateTime(step.getStartTimeByPredecessor(lbID.Text, lbMID.Text,"Sub")).ToString("yyyy-MM-dd");
            #endregion
        }
    }

    private void LoadddlSiblingStep()
    {
        DataTable dt = step.getDetSiblingSteps(lbMID.Text, lbID.Text);
        ddlSiblingStep.DataSource = dt;
        ddlSiblingStep.DataBind();
        ListItem item = new ListItem("---default---", "0");
        ddlSiblingStep.Items.Insert(0, item);
    }

    private DataTable SelectStepContextDate(string stepid)
    {
        try
        {
        string strName = "sp_RDW_StepContextDate";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@mid", lbMID.Text);
        parm[1] = new SqlParameter("@stepid", stepid);
        parm[2] = new SqlParameter("@NewSubStep", "true");

        return SqlHelper.ExecuteDataset(System.Configuration.ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (txtDescription.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('The param Description could not be empty!'); ";
            return;
        }

        //if (txtDuration.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('The param Duration could not be empty!!'); ";
        //    return;
        //}
        //else
        //{
        //    try
        //    {
        //        Int32 n = Convert.ToInt32(txtDuration.Text.Trim());

        //        if (n < 0)
        //        {
        //            ltlAlert.Text = "alert('The param Duration should be greater than zero!'); ";
        //            return;
        //        }
        //    }
        //    catch
        //    {
        //        ltlAlert.Text = "alert('The param Duration should be a numeric!'); ";
        //        return;
        //    }

        //}

        //if (txtStartDate.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('The param Start Date  could not be empty!'); ";
        //    return;
        //}
        //else
        //{
        //    try
        //    {
        //        DateTime _dt = Convert.ToDateTime(txtStartDate.Text.Trim());
        //    }
        //    catch
        //    {
        //        ltlAlert.Text = "alert('The format of Start Date must be a DateTime value!'); ";
        //        return;
        //    }
        //    txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(txtStartDate.Text.Trim()).AddDays(Convert.ToDouble(txtDuration.Text.Trim())));

        //}

        if (txtEndDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('The param End Date  could not be empty!'); ";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtEndDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('The format of Start Date must be a DateTime value!'); ";
                return;
            }
        }

        String stepId = lbID.Text.ToString();
        DataTable dt = SelectStepContextDate(stepId);
        String stepRangeDateType = dt.Rows[0]["DateType"].ToString();
        //if (dt.Rows[0]["RDW_StartDate"].ToString() == string.Empty || dt.Rows[0]["RDW_EndDate"].ToString() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('The Start Date of  the Parent Step could not be empty!'); ";
        //    return;
        //}
        if (dt.Rows[0]["RDW_EndDate"].ToString() == string.Empty)
        {
            ltlAlert.Text = "alert('The End Date of Step Parent could not be empty !'); ";
            return;
        }
        String stepRangeStartDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dt.Rows[0]["RDW_StartDate"].ToString()));
        String stepRangeEndDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dt.Rows[0]["RDW_EndDate"].ToString()));

        if (stepRangeDateType == "ParentDate")
        {
            //if (stepRangeStartDate == string.Empty)
            //{
            //    ltlAlert.Text = "alert('The Start Date of the Parent Step could not be empty!'); ";
            //    return;
            //}
            //else
            //{
            //    if (Convert.ToDateTime(stepRangeStartDate) > Convert.ToDateTime(txtStartDate.Text.Trim()))
            //    {
            //        ltlAlert.Text = "alert('The Start Date of Step could not be earlier than than the Start Date of the Parent Step!'); ";
            //        return;
            //    }
            //    else if (Convert.ToDateTime(stepRangeEndDate) < Convert.ToDateTime(txtEndDate.Text.Trim()))
            //    {
            //        ltlAlert.Text = "alert('The End Date of Step could not be later than the End Date of the Parent Step !'); ";
            //        return;
            //    }
            //} 
            if (Convert.ToDateTime(stepRangeEndDate) < Convert.ToDateTime(txtEndDate.Text.Trim()))
            {
                ltlAlert.Text = "alert('The End Date of Step could not be later than the End Date of the Parent Step !'); ";
                return;
            }
        }
        else if (stepRangeDateType == "ProjectDate")
        {
            //if (Convert.ToDateTime(stepRangeStartDate) > Convert.ToDateTime(txtStartDate.Text.Trim()))
            //{
            //    ltlAlert.Text = "alert('The Start Date of Step could not be earlier than than the Start Date of the Project !'); ";
            //    return;
            //}
            //else 
            if (Convert.ToDateTime(stepRangeEndDate) < Convert.ToDateTime(txtEndDate.Text.Trim()))
            {
                ltlAlert.Text = "alert('The End Date of Step could not be later than the End Date of the Project !'); ";
                return;
            }
        }

        try
        {
            string strName = "sp_RDW_InsertSubSteps";
            SqlParameter[] parm = new SqlParameter[10];
            parm[0] = new SqlParameter("@id", lbID.Text);
            parm[1] = new SqlParameter("@mid", lbMID.Text);
            parm[2] = new SqlParameter("@name", txtDescription.Text.Trim());
            parm[3] = new SqlParameter("@duration", txtDuration.Text);
            parm[4] = new SqlParameter("@predecessor", "0"); //dropPredecessor.SelectedValue
            parm[5] = new SqlParameter("@startdate", txtStartDate.Text.Trim());
            parm[6] = new SqlParameter("@enddate", txtEndDate.Text.Trim());
            parm[7] = new SqlParameter("@comments", txtComments.Text.Trim());
            parm[8] = new SqlParameter("@uID", Session["uID"].ToString());
            parm[9] = new SqlParameter("@stepforeId", ddlSiblingStep.SelectedValue);

            SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, parm);

            ltlAlert.Text = "window.location.href='/RDW/RDW_DetailList.aspx?mid=" + lbMID.Text + "&rm=" + DateTime.Now.ToString() + "';";
        }
        catch
        {
            ltlAlert.Text = "alert('Add data error！'); ";
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("RDW_DetailList.aspx?mid=" + lbMID.Text + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
    }

    protected void txtDuration_TextChanged(object sender, EventArgs e)
    {
        if (txtDuration.Text != "")
            txtEndDate.Text = Convert.ToDateTime(txtStartDate.Text).AddDays(Convert.ToInt32(txtDuration.Text)).ToString("yyyy-MM-dd");
    }
}
