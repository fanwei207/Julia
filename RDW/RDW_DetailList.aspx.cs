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

public partial class RDW_DetailList : BasePage
{
    RDW rdw = new RDW();
 
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("170007", "Modify Project");
            this.Security.Register("170008", "注册查看所有项目的权限");
        }

        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //判断该人员是否具有修改项目相关信息的权限。如果是创建人，默认拥有此权限
            if (!this.Security["170020"].isValid)
            {
                chkCanEditProject.Checked = false;
            }
            else
            {
                chkCanEditProject.Checked = true;
            }

            dropCatetory.DataSource = rdw.SelectProjectCategory(string.Empty);
            dropCatetory.DataBind();
            dropCatetory.Items.Insert(0, new ListItem("--", "0"));

            SKUHelper skuHelper = new SKUHelper();

            dropSKU.DataSource = skuHelper.Items(string.Empty);
            dropSKU.DataBind();

            dropSKU.Items.Insert(0, new ListItem("--", "--"));

            if (Request.QueryString["mid"] == null)
            {
                Response.Redirect("/RDW/RDW_HeaderList.aspx?rm=" + DateTime.Now.ToString(), true);
            }
            else
            {
                if (!this.Security["170008"].isValid)
                {
                    //定义参数
                    string strID = Convert.ToString(Request.QueryString["mid"]);
                    string strUID = Convert.ToString(Session["uID"]);
                    string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
                    if (!rdw.CheckViewRDWDetail(strID, strUID))
                    {
                        switch (strQuy.ToLower().Trim())
                        {
                            case "quy":
                                Response.Redirect("/RDW/RDW_List.aspx?@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
                                break;

                            case "rpt":
                                Response.Redirect("/RDW/RDW_RptList.aspx?@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
                                break;

                            default:
                                Response.Redirect("/RDW/RDW_HeaderList.aspx?@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
                                break;
                        }
                    }
                }
                

            }
            //gvRDW.Attributes.Add("style", "word-break:keep-all;word-wrap:normal"); 
            if (Request.QueryString["ecnCode"] != "" )
                lbt_ecnCode.Text = Request.QueryString["ecnCode"];
            else
            {
                lbl_ecnCode.Visible = false;
                lbt_ecnCode.Visible = false;
            }
            BindData();
            
            //ECN的项目的老项目信息
            if (Session["oldmID"] != null && Session["oldmID"].ToString() != "")
                BindGVOldProject();
            else
                gv_oldProject.Visible = false;

            if (lbl_type.Text == "ECN") txt_ppa.Visible = false;

            CheckRDWDeIsCanUpdateDate();
            if (this.Security["170007"].isValid)
            {
                btnSave.Enabled = true;
                btn_mgr.Enabled = true;
            }
        }
    }
    protected void BindGVOldProject()
    {
        gv_oldProject.DataSource = rdw.SelectOldProject( Convert.ToInt32(Session["oldmID"]), Convert.ToInt32(Request.QueryString["mid"]));
        gv_oldProject.DataBind();
    }
    protected void CheckRDWDeIsCanUpdateDate()
    {
        string strID = Convert.ToString(Request.QueryString["mid"]);
        RDW_Header rh = rdw.SelectRDWHeader(strID);
        DataTable dt = rdw.CheckRDWDetIsCanUpdateDate(strID);
        if ((dt.Rows.Count <= 0 && dropStatus.SelectedValue == "PROCESS" &&
                (Convert.ToString(Session["uID"]) == Convert.ToString(rh.RDW_CreatedBy) || rh.RDW_PMID.Trim().IndexOf(";" + Convert.ToString(Session["uID"]) + ";") >= 0))
            || (this.Security["170033"].isValid && dropStatus.SelectedValue == "PROCESS"))
        {
            txtStartDate.Enabled = true;
            txtStartDate.ReadOnly = false;
            txtEndDate.Enabled = true;
            txtEndDate.ReadOnly = false;
            btnSave.Enabled = true;
        }
        else
        {
            txtStartDate.Enabled = false;
            txtEndDate.Enabled = false;
        }
    }

    protected void BindData(bool isClosed = false)
    {
        //定义参数
        string strID = Convert.ToString(Request.QueryString["mid"]);

        RDW_Header rh = rdw.SelectRDWHeader(strID);

        txtProject.Text = rh.RDW_Project.Trim();
        txtProdCode.Text = rh.RDW_ProdCode.Trim();
        txtPPA.Text = rh.RDW_PPA.Trim();
        lblPPAMstrID.Text = rh.RDW_PPAMstrid;
        lbProdCode.Text = rh.RDW_ProdCode.Trim();
        dropStatus.Items.FindByValue(rh.RDW_Status.Trim()).Selected = true;
        try
        {
            dropCatetory.SelectedIndex = -1;
            dropCatetory.Items.FindByValue(rh.RDW_Category).Selected = true;
        }
        catch
        {
            dropCatetory.SelectedIndex = -1;
        }

        try
        {
            dropSKU.SelectedIndex = -1;
            dropSKU.Items.FindByValue(rh.RDW_ProdSKU.Trim()).Selected = true;
        }
        catch
        {
            dropSKU.SelectedIndex = -1;
        }

        txtProdDesc.Text = rh.RDW_ProdDesc.Trim();
        txtStartDate.Text = rh.RDW_StartDate.Trim();
        txtEndDate.Text = rh.RDW_EndDate.Trim();
        txtMemo.Text = rh.RDW_Memo.Trim();
        if (rh.RDW_EcnCode.Trim() != "")
        {
            lbt_ecnCode.Text = rh.RDW_EcnCode.Trim();
            lbt_ecnCode.Visible = true;
        }
        else
        {
            lbl_ecnCode.Visible = false;
            lbt_ecnCode.Visible = false;
        }
        txtStandard.Text = rh.RDW_Standard.Trim();
        lblPartner.Text = rh.RDW_PartnerName.Trim();
        lblPM.Text = "Project Leader:" + rh.RDW_PM.Trim();
        txtMGR.Text =  rh.RDW_MGR.Trim();
        lbl_type.Text = rh.RDW_TypeName.Trim();
        if (rh.RDW_EngineerTeam == "")
            ddl_ET.SelectedValue = "--";
        else
            ddl_ET.SelectedValue = rh.RDW_EngineerTeam;
        txt_customer.Text = rh.RDW_Customer;
        txt_lamptype.Text = rh.RDW_LampType;
        txt_priority.Text = rh.RDW_Priority;
        ddl_tier.SelectedValue = rh.RDW_Tier;
        ddl_EStarDLC.SelectedValue = rh.RDW_EStarDLC;

        if (this.Security["170007"].isValid || Convert.ToString(Session["uID"]) == Convert.ToString(rh.RDW_CreatedBy) || rh.RDW_PMID.Trim().IndexOf(";" + Convert.ToString(Session["uID"]) + ";") >= 0)
        {
            txtProject.ReadOnly = false;
            dropSKU.Enabled = true;
            txtProdDesc.ReadOnly = false;
            txtStartDate.ReadOnly = false;
            txtEndDate.ReadOnly = false;
            txtMemo.ReadOnly = false;
            txtStandard.ReadOnly = false;
            btnSave.Enabled = true;
            btnLeader.Enabled = true;
            btnStep.Enabled = true;
            btnPM.Enabled = true;
            btn_mgr.Enabled = true;
            dropStatus.Enabled = true;
        }
        else
        {
            txtProject.ReadOnly = true;
            dropSKU.Enabled = false;
            txtProdDesc.ReadOnly = true;
            txtStartDate.ReadOnly = true;
            txtEndDate.ReadOnly = true;
            txtMemo.ReadOnly = true;
            txtStandard.ReadOnly = true;
            btnSave.Enabled = false;
            btnLeader.Enabled = false;
            btnStep.Enabled = false;
            btnPM.Enabled = false;
            btn_mgr.Enabled = false;
            dropStatus.Enabled = false;
        }

        if (rh.RDW_Status != "PROCESS")
        {
            txtProject.ReadOnly = true;
            dropSKU.Enabled = false;
            txtProdDesc.ReadOnly = true;
            txtStartDate.ReadOnly = true;
            txtEndDate.ReadOnly = true;
            txtMemo.ReadOnly = true;
            txtStandard.ReadOnly = true;
            btnSave.Enabled = false;
            btnLeader.Enabled = false;
            btnStep.Enabled = false;
            btnPM.Enabled = false;
            btn_mgr.Enabled = false;
            dropStatus.Enabled = false;
        }

        gvRDW.DataSource = rdw.SelectRDWDetailList(strID, isClosed);
        gvRDW.DataBind();

    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (dropCatetory.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('The Project Category could not be empty!'); ";
            return;
        }
        if (txtProject.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('The Project Name could not be empty!'); ";
            return;
        }
        if (txtProdCode.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('The Product Code could not be empty!'); ";
            return;
        }
        if (txtProdCode.Text.Trim() != lbProdCode.Text.Trim())
        {
            if (rdw.CheckIsHavethisProdCode(txtProdCode.Text.Trim().ToString()))
            {
                ltlAlert.Text = "alert('Please Rename the Project Code,because this code already exists in another project!'); ";
                return;
            } 
        }

        if (txtStartDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('The Start Date could not be empty!'); ";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtStartDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('The format of Start Date must be a DateTime value!'); ";
                return;
            }
        }

        if (txtEndDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('The End Date could not be empty!'); ";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtEndDate.Text.Trim());

                DateTime _startdate = Convert.ToDateTime(txtStartDate.Text.Trim());
                if (_startdate > _dt)
                {
                    ltlAlert.Text = "alert('Start Date should not be greater than End Date!'); ";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('The format of End Date must be a DateTime value!'); ";
                return;
            }
        }

        if (txtPPA.Text == string.Empty)
        {
            ltlAlert.Text = "alert('The PPA could not be empty!'); ";
            return;
        }


        int ppaflag = rdw.CheckExistsPPA(txtPPA.Text);
        if (ppaflag == 0)
        {
            ltlAlert.Text = "alert('PPA is invalid!'); ";
            return;
        }
        else if (ppaflag == 2)
        {
            ltlAlert.Text = "alert('PPA did not pass approval!'); ";
            return;
        }
        //定义参数
        string strID = Convert.ToString(Request.QueryString["mid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);

        RDW_Header rh = new RDW_Header();
        rh.RDW_Project = txtProject.Text.Trim();
        rh.RDW_ProdCode = txtProdCode.Text.Trim();
        rh.RDW_PPA = txtPPA.Text.Trim();
        rh.RDW_ProdSKU = dropSKU.SelectedValue;
        rh.RDW_ProdDesc = txtProdDesc.Text.Trim();
        rh.RDW_Standard = txtStandard.Text.Trim();
        rh.RDW_Memo = txtMemo.Text.Trim();
        rh.RDW_StartDate = txtStartDate.Text.Trim();
        rh.RDW_EndDate = txtEndDate.Text.Trim();
        rh.RDW_MstrID = Convert.ToInt32(strID);
        rh.RDW_Category = dropCatetory.SelectedValue;
        rh.RDW_Status = dropStatus.SelectedValue;
        rh.RDW_Customer = txt_customer.Text.Trim();
        rh.RDW_LampType = txt_lamptype.Text.Trim();
        rh.RDW_Priority = txt_priority.Text.Trim();
        rh.RDW_EngineerTeam = ddl_ET.SelectedValue;
        rh.RDW_Tier = ddl_tier.SelectedValue;
        rh.RDW_EStarDLC = ddl_EStarDLC.SelectedValue;
        //////////下载立项模板时向detailedit页面传输的参数
        

        if (rdw.UpdateRDWHeader(rh))
        {
            ltlAlert.Text = "alert('Save data successfully！'); window.location.href='/RDW/RDW_DetailList.aspx?mid=" + strID + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
        }
        else
        {
            ltlAlert.Text = "alert('Save data failure or startdate and enddate of project could not between startdate and enddate of tasks！'); ";
        }

    }

    protected void gvRDW_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        int intRow = 0;
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        string strDID = string.Empty;

        if (e.CommandName.ToString() == "Detail")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strDID = gvRDW.DataKeys[intRow].Values["RDW_DetID"].ToString();
            
            if (Convert.ToBoolean(gvRDW.DataKeys[intRow].Values["RDW_isActive"].ToString()))
           {
                //////////////////////////
                int cate_id = Convert.ToInt32(dropCatetory.SelectedValue);
                string isClosed = "0";
                if (chkClosed.Checked)
                {
                    isClosed = "1";
                }
                Response.Redirect("/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&cateid=" + cate_id + "&did=" + strDID + "&isClosed=" + isClosed + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
            }
            else
            {
                string predecessorID = gvRDW.DataKeys[intRow].Values["RDW_PredtaskID"].ToString();
                ltlAlert.Text = "alert('Predecessor " + predecessorID + " is not finished');";
                BindData();
            }
            
        }
        else if (e.CommandName == "SubTask")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strDID = gvRDW.DataKeys[intRow].Value.ToString();
            Response.Redirect("/RDW/RDW_AddSubStep.aspx?mid=" + strMID + "&id=" + strDID + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
        }
        else if (e.CommandName == "myEdit")
        {
            //新的项目必须有ppa才可以开始项目
            if (!rdw.IsOldOrECNProject(Request.QueryString["mid"]) && rdw.HasPPA(lblPPAMstrID.Text) <= 0)
            {
                ltlAlert.Text = "alert('PPA docs is not uploaded');";
                return;
            }
            strDID = e.CommandArgument.ToString();
            Response.Redirect("/RDW/RDW_AddStep.aspx?mid=" + strMID + "&id=" + strDID + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
        }
        else if (e.CommandName == "Member")
        {
            ltlAlert.Text = "window.open('/RDW/rdw_det_mbr.aspx?t=p&mid=" + strMID + "&did=" + e.CommandArgument.ToString() + "&fr=&rm=" + DateTime.Now + "','','menubar=no,scrollbars=no,resizable=no,width=800,height=500,top=0,left=0');";
            BindData();
        }
        else if (e.CommandName == "Approver")
        {
            ltlAlert.Text = "window.open('/RDW/rdw_det_mbr.aspx?t=m&mid=" + strMID + "&did=" + e.CommandArgument.ToString() + "&fr=&rm=" + DateTime.Now + "','','menubar=no,scrollbars=no,resizable=no,width=800,height=500,top=0,left=0');";
            BindData();
        }
        else if (e.CommandName == "Close")
        {
            strDID = e.CommandArgument.ToString();
            if (rdw.CloseRdwDet(strMID, strDID, Session["uID"].ToString()))
            {
                BindData();
            }
            else
            {
                ltlAlert.Text = "alert('Unsuccessful');";
            }
            
        }
    }
    protected void gvRDW_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //定义参数
        string strDID = string.Empty;
        string strMID = Request.QueryString["mid"];
        string strUID = Convert.ToString(Session["uID"]);



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex;
            bool extra = Convert.ToBoolean(gvRDW.DataKeys[index].Values["RDW_Extra"]);
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("\r\n", "<br />");

            //Add By Shanzm 2014-03-06
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("\r\n", "<br />");

            LinkButton linkNo = (LinkButton)e.Row.FindControl("linkNo");
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            LinkButton btnDetail = (LinkButton)e.Row.FindControl("btnDetail");
            LinkButton linkSubTask = (LinkButton)e.Row.FindControl("linkSubTask");
            LinkButton linkMember = (LinkButton)e.Row.FindControl("linkMember");
            LinkButton linkApprover = (LinkButton)e.Row.FindControl("linkApprover");
            LinkButton btnClose = (LinkButton)e.Row.FindControl("btnClose");
            RDW_Detail rd = (RDW_Detail)e.Row.DataItem;
            strDID = rd.RDW_DetID.ToString();

            //如果登录者和项目创建者不同，则无法做删除操作
            if (rd.RDW_CreatedBy == Convert.ToInt32(strUID) || rdw.SelectRDWHeader(strMID).RDW_PMID.Contains(";" + strUID + ";"))
            {
                bool canAddSubStep;
                if (rdw.CheckDeleteRDWDetail(strDID, out canAddSubStep))
                {
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnDelete.Text = "";                   
                }
                if (!canAddSubStep)
                {
                    linkSubTask.Visible = false;
                }
            }
            else
            {
                btnDelete.Enabled = false;
                btnDelete.Text = "";

                linkSubTask.Visible = false;

                e.Row.Cells[7].Text = linkMember.Text.Trim();
                
            }

            //状态不是PROCESS的项目，也无法操作
            RDW_Header rh = rdw.SelectRDWHeader(strMID);
            if (!extra)
            {
                if (rh.RDW_Status != "PROCESS")
                { 
                    linkSubTask.Visible = false;
                    btnDelete.Enabled = false;
                    btnDelete.Text = "";
                    e.Row.Cells[7].Text = linkMember.Text.Trim();
                }
            }

            if (chkClosed.Checked)
            {
                linkSubTask.Visible = false;
                btnDelete.Enabled = false;
                btnDelete.Text = "";
                e.Row.Cells[7].Text = linkMember.Text.Trim();
            }

            //设置项目颜色
            switch (rd.RDW_Status)
            {
                case 1:
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Yellow;
                    break;

                case 2:
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Green;
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.FromName("#333333");
                    e.Row.Cells[7].Text = linkMember.Text.Trim();
                    break;
            }

            if (e.Row.Cells[4].Text.Replace("&nbsp;", "").Length > 0)
            {

                if (e.Row.Cells[5].Text.Replace("&nbsp;", "").Length > 0)
                {
                    //注释原因：已完成的步骤不显示红色
                    //if (DateTime.Compare(Convert.ToDateTime(e.Row.Cells[4].Text), Convert.ToDateTime(e.Row.Cells[3].Text)) > 0)
                    //{
                    //    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                    //}
                }
                else
                {
                    //if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(gvRDW.DataKeys[index].Values["RDW_StepActEndDate"].ToString())) > 0 && rd.RDW_isActive)
                    //{
                    //    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                    //}
                }
            }


            if (rd.RDW_ParentDetID != 0) //不是一级步骤菜单，不允许添加 Approver
            {

                e.Row.Cells[8].Text = "";
            }

            //如果是临时的，则不允许维护Detail

            btnDetail.Visible = true;

            //if (rd.RDW_StepStartDate == "" || rd.RDW_StepEndDate == "")
            //{
            //    btnDetail.Visible = false;
            //}
            if (rd.RDW_StepEndDate == "")
            {
                btnDetail.Visible = false;
            }
            else
            {
                //验证 Details权限，Leader、Create可查看所有步骤的， 步骤参与人、审核人只可 Detail 当前步骤的 caixia add 20140318
                if (!this.Security["170008"].isValid)
                {
                    if (rh.RDW_PMID.Trim().IndexOf(";" + Convert.ToString(Session["uID"]) + ";") < 0)
                    {
                        if (rh.RDW_Partner.Trim().IndexOf(";" + Convert.ToString(Session["uID"]) + ";") < 0)
                        {
                            if (Convert.ToInt32(Session["uID"]) != rd.RDW_CreatedBy)
                            {
                                if (rd.RDW_Partner.IndexOf(";" + Convert.ToString(Session["uID"]) + ";") < 0 && rd.RDW_EvaluaterID.IndexOf(";" + Convert.ToString(Session["uID"]) + ";") < 0)
                                {
                                    btnDetail.Visible = false;
                                }
                            }
                        }
                    }
                }
            }

        }
    }

    protected void gvRDW_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strID = Convert.ToString(Request.QueryString["mid"]);
        string strDID = gvRDW.DataKeys[e.RowIndex].Value.ToString();
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);

        if (rdw.DeleteRDWDetail(strID, strDID))
        {
            ltlAlert.Text = "alert('Delete data successfully！')";
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('Delete data failure！'); ";
            BindData();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        if (strQuy.StartsWith("ECN"))
        {
            Response.Redirect("/product/m5_detail.aspx?no=" + strQuy, true);
            return;
        }
        switch (strQuy.ToLower().Trim())
        {
            case "quy":
                Response.Redirect("/RDW/RDW_List.aspx?@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
                break;

            case "rpt":
                Response.Redirect("/RDW/RDW_RptList.aspx?@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
                break;
            case "projlog":
                this.Redirect("/RDW/RDW_ProjLog.aspx");
                break;
            default:
                Response.Redirect("/RDW/RDW_HeaderList.aspx?@__kw=" + "&projType=" + Request.QueryString["projType"] + "&region=" + Request.QueryString["region"] + Request.QueryString["@__kw"] + "&@__ca=" + Request.QueryString["@__ca"] + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
                break;
        }
    }

    protected void btnLeader_Click(object sender, EventArgs e)
    {
        //定义参数
        string strID = Convert.ToString(Request.QueryString["mid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        Response.Redirect("/RDW/RDW_Step_Mbr.aspx?t=&mid=" + strID 
            + "&flag=1&fr=" + strQuy 
            + "&@__pn=" + Request.QueryString["@__pn"] 
            + "&@__pc=" + Request.QueryString["@__pc"] 
            + "&@__sd=" + Request.QueryString["@__sd"] 
            + "&@__st=" + Request.QueryString["@__st"] 
            + "&@__sk=" + Request.QueryString["@__sk"] 
            + "&@__pg=" + Request.QueryString["@__pg"] 
            + "&rm=" + DateTime.Now.ToString(), true);
    }

    protected void btnStep_Click(object sender, EventArgs e)
    {
        //定义参数
        string strID = Convert.ToString(Request.QueryString["mid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        Response.Redirect("/RDW/RDW_AddStep.aspx?mid=" + strID  
            + "&fr=" + strQuy 
            + "&@__pn=" + Request.QueryString["@__pn"] 
            + "&@__pc=" + Request.QueryString["@__pc"] 
            + "&@__sd=" + Request.QueryString["@__sd"] 
            + "&@__st=" + Request.QueryString["@__st"] 
            + "&@__sk=" + Request.QueryString["@__sk"] 
            + "&@__pg=" + Request.QueryString["@__pg"] 
            + "&rm=" + DateTime.Now.ToString(), true);
    }

    protected void btnDoc_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('/RDW/RDW_doclist.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&rm=" + DateTime.Now.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();";
        BindData();
    }
    protected void btnPM_Click(object sender, EventArgs e)
    {
        //定义参数
        string strID = Convert.ToString(Request.QueryString["mid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        //flag=1 表示从detailList里面的Leader和Viewer调用的页面
        Response.Redirect("/RDW/RDW_Step_Mbr.aspx?t=pm&flag=1&mid=" + strID + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);

    }
    protected void btn_mgr_Click(object sender, EventArgs e)
    {
        string strID = Convert.ToString(Request.QueryString["mid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        //flag=1 表示从detailList里面的Leader和Viewer调用的页面
        Response.Redirect("/RDW/RDW_Step_Mbr.aspx?t=mgr&flag=1&mid=" + strID + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
    }
    protected void btnQAD_Click(object sender, EventArgs e)
    {
        string strID = Convert.ToString(Request.QueryString["mid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]); 
        Response.Redirect("/RDW/rdw_ProjectQadApproveList.aspx?t=&mid=" + strID + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true); 
   
        //Response.Redirect("/RDW/RDW_ProjQadApply.aspx?t=&mid=" + strID + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true); 
    }
    protected void btnSample_Click(object sender, EventArgs e)
    {
        string strID = Convert.ToString(Request.QueryString["mid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        Response.Redirect("../SampleDelivery/SampleDeliveryList.aspx?t=&mid=" + strID + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true); 
    }
    protected void chkClosed_CheckedChanged(object sender, EventArgs e)
    {
        if (chkClosed.Checked)
        {
            gvRDW.Columns[9].Visible = false;
            gvRDW.Columns[11].Visible = false;
            gvRDW.Columns[12].Visible = false;
        }
        else
        {
            gvRDW.Columns[9].Visible = true;
            gvRDW.Columns[11].Visible = true;
            gvRDW.Columns[12].Visible = true;
        }
        BindData(chkClosed.Checked);
    }
    protected void btnUL_Click(object sender, EventArgs e)
    {
        string strID = Convert.ToString(Request.QueryString["mid"]);
        //if (rdw.insertUL(strID, Convert.ToString(Session["uID"]), Convert.ToString(Session["uName"])))
        //{
        //    //string strID = Convert.ToString(Request.QueryString["mid"]);
        //    string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        //    Response.Redirect("/RDW/RDW_ULmstr.aspx?t=&mid=" + strID + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true); 
        //}
        //else
        //{
        //    ltlAlert.Text = "alert('新建UL失败');";
        //}
    }
    protected void txt_ppa_Click(object sender, EventArgs e)
    {
        string strMID = Request.QueryString["mid"];
        //根据strMID初始化PPA相关数据
        string result = rdw.InitialPPAInfo(strMID,Convert.ToInt32(Session["uID"]),Session["uName"].ToString());

        if (result != "1")
        {
            return;
        }
        else
        {
            Response.Redirect("RDW_PPADetail.aspx?mstrID=" + lblPPAMstrID.Text + "&RDW_mstrID=" + strMID + "&from=DL&appv=0&isView=1");
        }
    }

    protected void gv_oldProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_oldProject.PageIndex = e.NewPageIndex;
        BindGVOldProject();
    }
    protected void gv_oldProject_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int intRow = 0;
        string strMID = string.Empty;
        string ProdCode = string.Empty;
        string Project = string.Empty;
        string Category = string.Empty;
        string Status = string.Empty;
        string StartDate = string.Empty;

        if (e.CommandName.ToString() == "Detail")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strMID = gv_oldProject.DataKeys[intRow].Values["ID"].ToString();
            ProdCode = gv_oldProject.DataKeys[intRow].Values["ProdCode"].ToString();
            Category = gv_oldProject.DataKeys[intRow].Values["RDW_Category"].ToString();
            Project = gv_oldProject.DataKeys[intRow].Values["Project"].ToString();
            Status = gv_oldProject.DataKeys[intRow].Values["Status"].ToString();
            StartDate = gv_oldProject.DataKeys[intRow].Values["StartDate"].ToString();
            ltlAlert.Text = "var w=window.open('/RDW/RDW_DetailList.aspx?fr=ecn&mid=" + strMID + "&ecnCode=" + Request.QueryString["ecnCode"] + "&oldmID=" + Request.QueryString["oldmID"] + "&@__ca=" + Category + "&@__pn=" + Project + "&@__pc=" + ProdCode + "&@__sd=" + StartDate + "&@__st=" + Status + "&@__pg=" + gvRDW.PageIndex.ToString() + "&rm=" + DateTime.Now.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();";
            //Response.Redirect("/RDW/RDW_DetailList.aspx?fr=ecn&mid=" + strMID + "&@__ca=" + Category + "&@__pn=" + Project + "&@__pc=" + ProdCode + "&@__sd=" + StartDate + "&@__st=" + Status + "&@__pg=" + gvRDW.PageIndex.ToString() + "&rm=" + DateTime.Now.ToString(), true);
        }
    }
    protected void lbt_ecnCode_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Product/m5_detail.aspx?no=" + Request.QueryString["ecnCode"] + "&fr=dl" + "&mid=" + Request.QueryString["mid"] + "&oldmID=" + Request.QueryString["oldmID"] );
    }
    protected void btnPCB_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/PCB_ApplyMstr.aspx?ProjectCode=" + txtProdCode.Text.Trim() + "&mid="
            + Request["mid"].ToString() 
            //+ "&projType=" + Request["projType"].ToString() 
            //+ "&region=" + Request["region"].ToString()
            //+ "&ecnCode=" + Request["ecnCode"].ToString() 
            //+ "&oldmID= " + Request["oldmID"].ToString()
            //+ "&@__kw=" + Request["@__kw"].ToString() 
            //+ "&@__ca=" + Request["@__ca"].ToString()
            + "&@__pn=" + Request["@__pn"].ToString() 
            + "&@__pc=" + Request["@__pc"].ToString()
            + "&@__sd=" + Request["@__sd"].ToString() 
            + "&@__st=" + Request["@__st"].ToString()
            + "&@__sk=" + Request["@__sk"].ToString() 
            + "&@__pg=" + Request["@__pg"].ToString()
            + "&rm=" + Request["rm"].ToString());


    }

    protected void lkbtnProjectArgue_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_ProjectArgue.aspx?ProjectCode=" + txtProdCode.Text.Trim() 
            + "&mid="+ Request["mid"].ToString() 
            //+ "&projType=" + Request["projType"].ToString() 
            //+ "&region=" + Request["region"].ToString()
            //+ "&ecnCode=" + Request["ecnCode"].ToString() 
            //+ "&oldmID= " + Request["oldmID"].ToString()
            //+ "&@__kw=" + Request["@__kw"].ToString() 
            //+ "&@__ca=" + Request["@__ca"].ToString()
            + "&@__pn=" + Request["@__pn"].ToString() 
            + "&@__pc=" + Request["@__pc"].ToString()
            + "&@__sd=" + Request["@__sd"].ToString() 
            + "&@__st=" + Request["@__st"].ToString()
            + "&@__sk=" + Request["@__sk"].ToString() 
            + "&@__pg=" + Request["@__pg"].ToString()
            + "&rm=" + Request["rm"].ToString());
    }
}
