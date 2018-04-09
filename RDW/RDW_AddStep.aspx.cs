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


public partial class RDW_AddStep : BasePage
{
    RDW rdw = new RDW();
    RD_Steps step = new RD_Steps(); 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbMID.Text = Request.QueryString["mid"].ToString();
             
            if (Request.QueryString["id"] != null)
            {
                #region 显示第一个tab
                hidTabIndex.Value = "0";
                head.Visible = false;
                tabs2.Style.Add("display", "none");
                #endregion
                lbID.Text = Request.QueryString["id"].ToString();

                lblprojectname.Visible = false;
                txbprojectname.Visible = false;
                lblprojectcode.Visible = false;
                txbprojectcode.Visible = false;
                lblstep.Visible = false;
                ddlprojectstep.Visible = false;
                ckbExtra.Enabled = false;
                DataSet ds = step.SelectStepById(Convert.ToInt32(lbMID.Text), Convert.ToInt32(lbID.Text));

                txtDescription.Value = ds.Tables[0].Rows[0]["RDW_StepName"].ToString();

                txtDuration.Text = ds.Tables[0].Rows[0]["RDW_Duration"].ToString();

                ckbExtra.Checked = Convert.ToBoolean( ds.Tables[0].Rows[0]["RDW_extraStep"] );

                #region Amber Liu
                //设置默认起始时间 
                txtStartDate.Text = Convert.ToDateTime(step.getStartTimeByPredecessor(dropPredecessor.SelectedValue, lbMID.Text, "")).ToString("yyyy-MM-dd");
                #endregion
                
                if (ds.Tables[0].Rows[0]["RDW_StartDate"].ToString() != string.Empty)
                {
                    txtStartDate.Text = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[0]["RDW_StartDate"].ToString()));
                    if (txtStartDate.Text.ToString() == "1900-01-01")
                    {
                        txtStartDate.Text = string.Empty;
                    }  
                }

                if (ds.Tables[0].Rows[0]["RDW_EndDate"].ToString() != string.Empty)
                { 
                    txtEndDate.Text = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[0]["RDW_EndDate"].ToString()));
                    if (txtEndDate.Text.ToString() == "1900-01-01")
                    {
                        txtEndDate.Text = string.Empty;
                    }  
                }

                txtComments.Text = ds.Tables[0].Rows[0]["RDW_StepDesc"].ToString().Replace("<br>", "");
                LoaddropPredecessor();
                if (ds.Tables[0].Rows[0]["RDW_ParentID"].ToString() == "0")
                {                   
                    dropPredecessor.SelectedValue = ds.Tables[0].Rows[0]["RDW_Predecessor"].ToString();
                }
                else
                {
                    trPredecessor.Visible = false; 
                }

                //判断权限
                //如果是创建者，则默认拥有修改的权限，但之前还需判断项目是否已经有进度，否则也不允许修改-- caixia20140108 现在可以允许修改

                if (Convert.ToInt32(Session["uID"].ToString()) != Convert.ToInt32(ds.Tables[0].Rows[0]["RDW_CreatedBy"].ToString()))
                {
                    //limitedStyle  0 - No permission to modified data /  1 - Only have modified data permission
                    if (this.Security["170020"].isValid)
                        limitedStyle(1);
                    else
                        limitedStyle(0);
                }
                //else
                //{
                //    if (!rdw.CheckModifiedDate(Request.QueryString["mid"].ToString()))
                //    {
                //        limitedStyle(0);
                //    }
                //}
            }
            else
            { 
                lbID.Text = "0";
                LoaddropPredecessor();
                lbl_step.Visible = false;
                ckb_projectStep.Visible = false;
                chb_all.Visible = false;
                BindPlaceBefore();
                BindPredecessor();
                #region Amber Liu
                //设置默认起始时间 
                txtStartDate.Text = Convert.ToDateTime(step.getStartTimeByPredecessor(dropPredecessor.SelectedValue, lbMID.Text, "")).ToString("yyyy-MM-dd");
                #endregion
            }
            
            LoadddlSiblingStep();
            Loadddlprojectstep();

            
           
        }
    }
    #region Add/Copy Step BindDDL
    private void LoadddlSiblingStep()
    {
        DataTable dt;
        if (lbID.Text == "0")
        {
             dt = step.getDetSiblingSteps(lbMID.Text, lbID.Text);
        }
        else
        {
            dt = step.getDetSiblingSteps(lbMID.Text, lbID.Text,"update");
        }
         
         ddlSiblingStep.DataSource = dt;
         ddlSiblingStep.DataBind();
         ListItem item = new ListItem("---default--- ", "0");
         ddlSiblingStep.Items.Insert(0, item);
    }

    private void LoaddropPredecessor()
    {
        DataTable dt;
        if (lbID.Text == "0")
        {
            dt = step.getDetSiblingSteps(lbMID.Text, lbID.Text);
        }
        else
        {
            dt = step.getDetSiblingSteps(lbMID.Text, lbID.Text, "update");
        }

        dropPredecessor.DataSource = dt;
        dropPredecessor.DataBind();
        ListItem item = new ListItem("---default--- ", "0");
        dropPredecessor.Items.Insert(0, item);
    }
    #endregion

    #region Copy Steps BindDDL
    private void BindPlaceBefore()
    {
        DataTable dt;
        if (lbID.Text == "0")
        {
            dt = step.getDetSiblingSteps(lbMID.Text, lbID.Text);
        }
        else
        {
            dt = step.getDetSiblingSteps(lbMID.Text, lbID.Text, "update");
        }

        ddl_placebefore.DataSource = dt;
        ddl_placebefore.DataBind();
        ListItem item = new ListItem("---default--- ", "0");
        ddl_placebefore.Items.Insert(0, item);
    }

    private void BindPredecessor()
    {
        DataTable dt;
        if (lbID.Text == "0")
        {
            dt = step.getDetSiblingSteps(lbMID.Text, lbID.Text);
        }
        else
        {
            dt = step.getDetSiblingSteps(lbMID.Text, lbID.Text, "update");
        }

        ddl_Predecessor.DataSource = dt;
        ddl_Predecessor.DataBind();
        ListItem item = new ListItem("---default--- ", "0");
        ddl_Predecessor.Items.Insert(0, item);
    }
    #endregion

    private DataTable SelectStepContextDate(string stepid)
    {
        //try
        //{
            string strName = "sp_RDW_StepContextDate";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@mid", lbMID.Text);
            parm[1] = new SqlParameter("@stepid", stepid);

            return SqlHelper.ExecuteDataset(System.Configuration.ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, parm).Tables[0];
        //}
        //catch
        //{
        //    return null;
        //}
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txbprojectname.Text.Trim()  == string.Empty && txbprojectcode.Text.Trim() == string.Empty )
        {

            if (txtDescription.Value.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('The param Description could not be empty!'); ";
                return;
            }
            else if (txtDescription.Value.Trim().Replace("\r\n", "").Length > 1000)
            {
                ltlAlert.Text = "alert('The param Description could be only 1000 characters allowed!'); ";
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
            //    ltlAlert.Text = "alert('The Start Start of Step Parent could not be empty !'); ";
            //    return;
            //}
            if ( dt.Rows[0]["RDW_EndDate"].ToString() == string.Empty)
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
                //    ltlAlert.Text = "alert('The Start Date of Step Parent could not be empty !'); ";
                //    return;
                //}
                //else
                //{
                //    if (Convert.ToDateTime(stepRangeStartDate) > Convert.ToDateTime(txtStartDate.Text.Trim()))
                //    {
                //        ltlAlert.Text = "alert('The Start Date of Step could not be earlier than than the Start Date of the Parent Step !'); ";
                //        return;
                //    }
                //    else if (Convert.ToDateTime(stepRangeEndDate) < Convert.ToDateTime(txtEndDate.Text.Trim()) && ckbExtra.Checked == false)
                //    {
                //        ltlAlert.Text = "alert('The End Date of Step could not be later than the End Date of the Parent Step !'); ";
                //        return;
                //    }
                //}
                if (Convert.ToDateTime(stepRangeEndDate) < Convert.ToDateTime(txtEndDate.Text.Trim()) && ckbExtra.Checked == false)
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
                if (Convert.ToDateTime(stepRangeEndDate) < Convert.ToDateTime(txtEndDate.Text.Trim()) && ckbExtra.Checked == false)
                {
                    ltlAlert.Text = "alert('The End Date of Step could not be later than the End Date of the Project!'); ";
                    return;
                }
            }
        }

        try
        {
            string strName = "sp_RDW_InsertSteps";
            SqlParameter[] parm = new SqlParameter[12];
            parm[0] = new SqlParameter("@id", lbID.Text);
            parm[1] = new SqlParameter("@mid", lbMID.Text);
            parm[2] = new SqlParameter("@name", txtDescription.Value.Trim());
            parm[3] = new SqlParameter("@duration", txtDuration.Text);
            parm[4] = new SqlParameter("@predecessor", dropPredecessor.SelectedValue);
            parm[5] = new SqlParameter("@startdate", txtStartDate.Text.Trim());
            parm[6] = new SqlParameter("@enddate", txtEndDate.Text.Trim());
            parm[7] = new SqlParameter("@comments", txtComments.Text.Trim());
            parm[8] = new SqlParameter("@uID", Session["uID"].ToString());
            parm[9] = new SqlParameter("@stepforeId", ddlSiblingStep.SelectedValue);
            parm[10] = new SqlParameter("@detID", ddlprojectstep.SelectedValue);
            parm[11] = new SqlParameter("@ExtraStep", ckbExtra.Checked);

            SqlHelper.ExecuteNonQuery(System.Configuration.ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, parm);

            if (Request.QueryString["cmd"] != null)
            {
                Response.Redirect("RDW_List.aspx?rm=" + DateTime.Now.ToString());
            }
            else
            {
                ltlAlert.Text = "window.location.href='/RDW/RDW_DetailList.aspx?mid=" + lbMID.Text + "&rm=" + DateTime.Now.ToString() + "';";
            }
        }
        catch
        {
            ltlAlert.Text = "alert('Add data error！'); ";
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Back();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Back();
    }
    protected void Back()
    {
        if (Request.QueryString["cmd"] != null)
        {
            Response.Redirect("RDW_List.aspx?rm=" + DateTime.Now.ToString());
        }
        else
        {
            Response.Redirect("/RDW/RDW_DetailList.aspx?mid=" + lbMID.Text + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
        }
    }
   

    private void limitedStyle(int intType)
    {
        txtDescription.Disabled = true; 
        txtComments.Enabled = false;
        if (intType == 0)
        {
            txtDuration.Enabled = false;
            txtStartDate.Enabled = false;
            txtEndDate.Enabled = false;
            btnSave.Enabled = false; 
        } 
    }
    private void Loadddlprojectstep()
    {
        DataSet dt;
        if (txbprojectname.Text.Trim().Length > 0 || txbprojectcode.Text.Trim().Length > 0)
        {
            dt = step.selectprojectstep(txbprojectname.Text.Trim(), txbprojectcode.Text.Trim());
            ddlprojectstep.DataSource = dt;
            ddlprojectstep.DataBind();
        }

        ListItem item = new ListItem("---copy from step--- ", "0");
        this.ddlprojectstep.Items.Insert(0, item);
    }
    private void Loadckb_projectstep()
    {
        DataSet dt;
        if (txb_projectname.Text.Trim().Length > 0 && txb_projectcode.Text.Trim().Length > 0)
        {
            hidTabIndex.Value = "1";
            dt = step.selectprojectstep(txb_projectname.Text.Trim(), txb_projectcode.Text.Trim());
            ckb_projectStep.DataSource = dt;
            ckb_projectStep.DataBind();
            lbl_step.Visible = true;
            ckb_projectStep.Visible = true;
            chb_all.Visible = true;
        }
    }
    #region Add/Copy Step
    protected void txbprojectname_TextChanged(object sender, EventArgs e)
    {
        hidTabIndex.Value = "0";
        Loadddlprojectstep();
    }
    protected void txbprojectcode_TextChanged(object sender, EventArgs e)
    {
        hidTabIndex.Value = "0";
        Loadddlprojectstep();
    }
    #endregion
    #region Copy Steps
    protected void txb_projectname_TextChanged(object sender, EventArgs e)
    {
        hidTabIndex.Value = "1";
        Loadckb_projectstep();
    }
    protected void txb_projectcode_TextChanged(object sender, EventArgs e)
    {
        hidTabIndex.Value = "1";
        Loadckb_projectstep();
    }
    #endregion
    protected void dropPredecessor_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region Amber Liu
        //设置默认起始时间 
        txtStartDate.Text = Convert.ToDateTime(step.getStartTimeByPredecessor(dropPredecessor.SelectedValue, lbMID.Text,"")).ToString("yyyy-MM-dd");
        if(txtDuration.Text != "")
            txtEndDate.Text = Convert.ToDateTime(txtStartDate.Text).AddDays(Convert.ToInt32(txtDuration.Text)).ToString("yyyy-MM-dd");
        #endregion
    }
    protected void txtDuration_TextChanged(object sender, EventArgs e)
    {
        if (txtDuration.Text != "")
            txtEndDate.Text = Convert.ToDateTime(txtStartDate.Text).AddDays(Convert.ToInt32(txtDuration.Text)).ToString("yyyy-MM-dd");
    }
    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        if (txtDuration.Text != "")
            txtEndDate.Text = Convert.ToDateTime(txtStartDate.Text).AddDays(Convert.ToInt32(txtDuration.Text)).ToString("yyyy-MM-dd");
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        CheckBoxList cbl = new CheckBoxList();
        string did = "";
        foreach( ListItem item in ckb_projectStep.Items)
        {
            if(item.Selected)
            {
                did = did + item.Value.ToString() + ";";
            }
        }

        if (step.CopySteps(did, lbMID.Text, Session["uID"].ToString(),ddl_placebefore.SelectedValue) == 1)
        {
            this.Alert("复制成功！");
        }
        else
        {
            this.Alert("复制失败！");
            return;
        }
    }
}
