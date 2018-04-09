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
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using RD_WorkFlow;
using System.Net.Mail;
using System.Text;
using System.IO;

public partial class RDW_RDW_ProjQadApply : BasePage
{
    RDW rdw = new RDW();
    
    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];
    private string qadLog;
    private string qadDate;
    private string qadReason;
    private string qadPkgCode;
    private string qadDesc;
    private DataTable dtQad
    {
        get 
        {
            if (ViewState["dtQad"] == null)
            {
                string mid = "";
                DataTable dt = null;
                if (lblApplyId.Text == "" || lblApplyId.Text == "0")
                {
                    if (Request.QueryString["mid"] != null)
                    {
                        mid = Request.QueryString["mid"].ToString();
                    }
                    int rdw_pqapplyId = 0;
                    dt = rdw.SelectRdwQad(mid, rdw_pqapplyId);
                }
                else
                {
                    int rdw_pqapplyId = int.Parse(lblApplyId.Text);
                    dt = rdw.SelectRdwQad(rdw_pqapplyId);
                }
                ViewState["dtQad"] = dt;
            }
            return ViewState["dtQad"] as DataTable;
        }
        set 
        {
            ViewState["dtQad"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BtnAdd.Enabled = this.Security["170016"].isValid;


            if (Request.QueryString["pqmId"] != null)
            {
                lblApplyId.Text = Request.QueryString["pqmId"];
                btn_submit.Visible = false;
                txtApplyReason.Enabled = false;
            }
            else
            {
                lblApplyId.Text = "0";
                btn_submit.Visible = true;
            }

            btn_approve.Visible = false;
            btn_diaApp.Visible = false;

            //刘毅提出: 申请时QAD号不用添加. --20140417
            BtnAdd.Visible = false;
            txtQad.Visible = false;
            Import.Visible = false;
            if (Request.QueryString["mid"] == null || Request.QueryString["mid"] == "" || Request.QueryString["mid"] == "0")
            {
                ProjectStep.Visible = false;
                ProjectHeader.Visible = false;
                if (lblApplyId.Text == "" || lblApplyId.Text == "0")
                {
                    BtnAdd.Visible = true;
                    txtQad.Visible = true;
                    Import.Visible = true;
                }
                btnSelectAll.Visible = false;
                btnClear.Visible = false;
            }
            else
            {
                ProjectHeader.Visible = true;
                ProjectStep.Visible = true;
                BindProjStepData();
                BindProjData();
                if (lblApplyId.Text == "" || lblApplyId.Text == "0")
                {
                    btnSelectAll.Visible = true;
                    btnClear.Visible = true;
                }
                else
                {
                    btnSelectAll.Visible = false;
                    btnClear.Visible = false;
                }
            }

            
            BindProjQadLink();
            BindQADData();
            BindApprove();

            dropCatetory.DataSource = rdw.SelectProjectCategory(string.Empty);
            dropCatetory.DataBind();
            dropCatetory.Items.Insert(0, new ListItem("--", "0"));





        }
        
        
    }

    private void BindProjData()
    {
        //定义参数
        string strID = Convert.ToString(Request.QueryString["mid"]);
        RDW_Header rh = rdw.SelectRDWHeader(strID);
        try
        {
            dropCatetory.SelectedIndex = -1;
            dropCatetory.Items.FindByValue(rh.RDW_Category).Selected = true;
        }
        catch
        {
            dropCatetory.SelectedIndex = -1;
        }
        lblProjectData.Text = rh.RDW_Project.Trim();
        lblProdCodeData.Text = rh.RDW_ProdCode;
        lblProdDescData.Text = rh.RDW_ProdDesc;
        lblStartDateData.Text = rh.RDW_StartDate;
        lblEndDateData.Text = rh.RDW_EndDate;
        lblPM.Text = rh.RDW_PM.Trim();

    }
  
    private void BindProjQadLink()
    {
        string strID = Convert.ToString(Request.QueryString["mid"]);

        if (lblApplyId.Text != "0")
        {
            int pqapplyId = int.Parse(lblApplyId.Text);
            DataTable dt = rdw.getProjQadLink(strID, pqapplyId);

            if (dt.Rows.Count > 0)
            {
                lblApplyId.Text = dt.Rows[0]["rdw_pqid"].ToString();
                //txtApproveName.Text = dt.Rows[0]["rdw_approverName"].ToString();
                //txt_approveID.Text = dt.Rows[0]["rdw_approverId"].ToString();
                if (dt.Rows[0]["rdw_approveResult"].ToString() == string.Empty)
                {
                    lblCurrentApprover.Text = dt.Rows[0]["rdw_approverId"].ToString();
                    
                }
                else
                {
                    lblAproveDate.Text = "Aprove Date:" + dt.Rows[0]["rdw_approveDate"].ToString();
                    approvalresult.Visible = true;
                }
                txtApplyReason.Text = dt.Rows[0]["rdw_applyReason"].ToString();
                txtApprOpin.Text = dt.Rows[0]["rdw_approveNote"].ToString();
                lblApplyDate.Text = "Apply Date:" + dt.Rows[0]["rdw_applyDate"].ToString();              
                rdb_Result.SelectedValue = dt.Rows[0]["rdw_approveResult"].ToString();
            }
        }
        else
        {
            if (lblApplyId.Text == "" || lblApplyId.Text == "0")
            {
                btn_submit.Visible = true;

            }
            else
            {
                btn_submit.Visible = false;
            }
        }


        if (lblCurrentApprover.Text == Session["uID"].ToString())
        {
            btn_approve.Visible = true;
            btn_diaApp.Visible = true;
            btn_Approver.Enabled = true;
            chkEmail.Visible = true;
            approveopinion.Visible = true;
            //txtApprOpin.Enabled = true;
        }
        else
        {
            btn_approve.Visible = false;
            btn_diaApp.Visible = false;
            btn_Approver.Enabled = false;
            chkEmail.Visible = false;
            approveopinion.Visible = false;
            //txtApprOpin.Enabled = false;
        }

        if (lblApplyDate.Text.ToString() != string.Empty)
        {
            btn_submit.Visible = false;
        }
        else
        {
            btn_Approver.Enabled = true;
            chkEmail.Visible = true;
        }
        //if (lblAproveDate.Text.ToString() != "")
        //{
        //    btn_approve.Visible = false;
        //    btn_diaApp.Visible = false;
        //}
        if (lblApplyId.Text != "0")
        {
            btn_submit.Visible = false;
        } 
        //chkEmail.Visible = btn_submit.Visible;
        //btn_Approver.Enabled = btn_submit.Visible;
    }

    protected void BindQADData()
    {
        string mid = "";
        DataTable dt = null;
        if (lblApplyId.Text == "" || lblApplyId.Text == "0")
        {
            if (Request.QueryString["mid"] != null)
            {
                mid = Request.QueryString["mid"].ToString();
            }
            int rdw_pqapplyId = 0;
            dt = rdw.SelectRdwQad(mid, rdw_pqapplyId);
        }
        else
        {
            int rdw_pqapplyId = int.Parse(lblApplyId.Text);
            dt = rdw.SelectRdwQad(rdw_pqapplyId);
        }
        //int rdw_pqapplyId = int.Parse(lblApplyId.Text);

        //if (dt.Rows.Count == 0)
        //{
        //    dt.Rows.Add(dt.NewRow());
        //    gvRWDQad.DataSource = dt;
        //    gvRWDQad.DataBind();

        //    int columnCount = gvRWDQad.Columns.Count;
        //    gvRWDQad.Rows[0].Cells.Clear();
        //    gvRWDQad.Rows[0].Cells.Add(new TableCell());
        //    gvRWDQad.Rows[0].Cells[0].ColumnSpan = columnCount;
        //    gvRWDQad.Rows[0].Cells[0].Text = "No data";
        //    gvRWDQad.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        //}
        //else
        //{
            gvRWDQad.DataSource = dt;
            gvRWDQad.DataBind();
        //}
        dtQad = dt;
    }

    protected void BindAddQADData()
    {
        gvRWDQad.DataSource = dtQad;
        gvRWDQad.DataBind();
    }

    protected void BindApprove()
    {
        string mid = "";
        if (Request.QueryString["mid"] != null)
        {
            mid = Request.QueryString["mid"].ToString();
        }
        int rdw_pqapplyId = int.Parse(lblApplyId.Text);
        if (rdw_pqapplyId == 0)
        {
            rdw_pqapplyId = -1;
        }
        DataTable dt = rdw.SelectApprove(mid, rdw_pqapplyId);
        //if (dt.Rows.Count == 0)
        //{
        //    dt.Rows.Add(dt.NewRow());
        //    gvApprove.DataSource = dt;
        //    gvApprove.DataBind();
        //    int columnCount = gvRWDQad.Columns.Count;
        //    gvApprove.Rows[0].Cells.Clear();
        //    gvApprove.Rows[0].Cells.Add(new TableCell());
        //    gvApprove.Rows[0].Cells[0].ColumnSpan = columnCount;
        //    gvApprove.Rows[0].Cells[0].Text = "No data";
        //    gvApprove.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        //}
        //else
        //{
            gvApprove.DataSource = dt;
            gvApprove.DataBind();
        //}
    }
    
    protected void btn_Approver_Click(object sender, EventArgs e)
    {
        //int mId = int.Parse(Request.QueryString["mid"].ToString()); 
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        ltlAlert.Text = "var w=window.open('rdw_getProjQadApprover.aspx','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
        
    }
      
    private void BindProjStepData()
    {
        //定义参数
        string strID = Convert.ToString(Request.QueryString["mid"]);
        gvRDW.DataSource = rdw.SelectRDWDetailList(strID);
        gvRDW.DataBind();
    }
    protected void gvRDW_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数 
    }
    protected void gvRDW_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //定义参数

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Add By Shanzm 2014-03-06
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("\r\n", "<br />");
           
            //e.Row.Cells[6].Text = e.Row.Cells[6].Text.Replace("<BR>", "<br />");
            //e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("<BR>", "<br />");

            LinkButton linkMember = (LinkButton)e.Row.FindControl("linkMember");
            LinkButton linkApprover = (LinkButton)e.Row.FindControl("linkApprover");
            e.Row.Cells[6].Text = linkMember.Text.Trim();
            e.Row.Cells[7].Text = linkApprover.Text;
             
        }
    }
    protected void gvRDW_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }
      

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        //if (lblApplyId.Text == "0")
        //{
        //    ltlAlert.Text = "alert('Submit this application first,please!'); ";
        //    BindQADData();
        //    return;
        //}        
        //if (Request.QueryString["mid"] == null)
        //{
        //    ltlAlert.Text = "alert('No project!'); ";
        //    BindQADData();
        //    return;
        //}
        //if (txtQad.Text.Length == 0)
        //{
        //    ltlAlert.Text = "alert('item can not be empty!'); ";
        //    BindAddQADData();
        //    return;
        //}
        //if (!rdw.IsExistQad(txtQad.Text.Trim()))
        //{
        //    ltlAlert.Text = "alert('item does not exists!'); ";
        //    BindAddQADData();
        //    return;
        //}
        //if (IsExistRdwQad(txtQad.Text.Trim()))
        //{
        //    ltlAlert.Text = "alert('this item does already exists!'); ";
        //    BindAddQADData();
        //    return;
        //}
        string message = "";
        if (CheckQad(txtQad.Text.Trim(), 0, "", out message))
        {
            //int rdw_pqapplyId = int.Parse(lblApplyId.Text);
            //if (!rdw.InsertRdwQad(Request.QueryString["mid"], txtQad.Text.Trim(), Convert.ToInt32(Session["uID"]), rdw_pqapplyId))
            //{
            //    ltlAlert.Text = "alert('add item failed!'); ";
            //    BindQADData();
            //    return;
            //}
            //BindQADData(); 

            DataTable dt = dtQad;
            //if (dt.Rows.Count == 0 || dt.Rows[0]["qad"].ToString() == "")
            //{
            //    dt.Rows.Clear();
            //}
            DataRow newRow = dt.NewRow();
            newRow["qad"] = txtQad.Text.Trim();
            newRow["selected"] = 1;
            if (qadLog != "")
            {
                newRow["log"] = qadLog;
            }
            if (qadDate != "")
            {
                newRow["date"] = qadDate;
            }
            newRow["reason"] = qadReason;
            newRow["pkgcode"] = qadPkgCode;
            newRow["description"] = qadDesc;
            dt.Rows.Add(newRow);

        }
        else
        {
            ltlAlert.Text = "alert('" + message + "'); ";
        }
        BindAddQADData();
    }

    private bool CheckQad(string qad, int index, string aa, out string message)
    {
        message = "";
        if (index != 0)
        {
            message = "sheet " + aa + " row " + index + ":";
        }
        if (qad.Length == 0)
        {
            message += "item can not be empty!\\n";
            return false;
        }
        DataTable dt=new DataTable();
        if (!rdw.IsExistQad(qad,out dt))
        {
            message += "item does not exists!\\n";
            return false;
        }
        else
        {
            qadLog = dt.Rows[0]["pt__log02"].ToString();
            qadDate = dt.Rows[0]["pt__dte02"].ToString();
            qadReason = dt.Rows[0]["pt__chr02"].ToString();
            qadPkgCode = dt.Rows[0]["pt_pkg_code"].ToString();
            qadDesc = dt.Rows[0]["pt_desc1"].ToString() + " " + dt.Rows[0]["pt_desc2"].ToString();
        }
        if (IsExistRdwQad(qad))
        {
            if (index == 0)
            {
                message += "this item does already exists!\\n";
            }
            else
            {
                message = "";
            }
            return false; ;
        }
        return true;
    }

    private bool IsExistRdwQad(string qad)
    {
        DataTable dt = dtQad;
        return dt.Select("qad ='" + qad + "'").Length > 0;
    }

    public bool check(object value)
    {
        //bool chk = false;
        //if (bool.TryParse(value.ToString(),out chk))
        //{
        //    return chk;
        //}
        //else
        //{
        //    return false;
        //}
        try
        {
            return Convert.ToBoolean(value);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

//    protected static DataTable SelectRdwQad(string id, int rdw_pqapplyId)
//    {
        
//        try
//        {
//            string strName = "sp_rdw_selectRdwQadList";
//            SqlParameter[] sqlParam = new SqlParameter[2];
//            sqlParam[0] = new SqlParameter("@id", id);
//            sqlParam[1] = new SqlParameter("@rdw_pqapplyId", rdw_pqapplyId);
//            return SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, sqlParam).Tables[0];
//        }
//        catch
//        {
//            return null;
//        }

//    }

//    protected static DataTable SelectApprove(string id, int rdw_pqapplyId)
//    {

//        try
//        {
//            string strName = "sp_RDW_selectProjQadApprove";
//            SqlParameter[] sqlParam = new SqlParameter[2];
//            sqlParam[0] = new SqlParameter("@mid", id);
//            sqlParam[1] = new SqlParameter("@applyid", rdw_pqapplyId);
//            return SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, sqlParam).Tables[0];
//        }
//        catch
//        {
//            return null;
//        }

//    }

//    protected static bool DeleteRdwQad(string id)
//    {
//        try
//        {
//            string strName = "sp_rdw_deleteRdwQadList";
//            SqlParameter[] param = new SqlParameter[2];
//            param[0] = new SqlParameter("@id", id);
//            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
//            param[1].Direction = ParameterDirection.Output;
//            SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);
//            return Convert.ToBoolean(param[1].Value);

//        }
//        catch (Exception ex)
//        {
//            return false;
//        }
//    }

///// <summary>
//    ///   插入项目与QAD的关联
///// </summary>
///// <param name="mid"></param>
///// <param name="qad"></param>
///// <param name="createBy"></param>
///// <param name="rdw_pqapplyId">申请序号</param>
///// <returns></returns>
//    protected static bool InsertRdwQad(string mid, string qad, int createBy, int rdw_pqapplyId)
//    {
//        try
//        {
//            string strName = "sp_rdw_insertRdwQadList";
//            SqlParameter[] param = new SqlParameter[4];
//            param[0] = new SqlParameter("@mid", mid);
//            param[1] = new SqlParameter("@qad", qad);
//            param[2] = new SqlParameter("@createBy", createBy);
//            param[3] = new SqlParameter("@rdw_pqapplyId", rdw_pqapplyId);
//            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
//            param[4].Direction = ParameterDirection.Output;
//            SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);
//            return Convert.ToBoolean(param[4].Value);

//        }
//        catch (Exception ex)
//        {
//            return false;
//        }
//    }

    //protected static bool IsExistRdwQad(string mid, string qad)
    //{
    //    try
    //    {
    //        string strName = "sp_rdw_checkRdwQadList";
    //        SqlParameter[] param = new SqlParameter[3];
    //        param[0] = new SqlParameter("@mid", mid);
    //        param[1] = new SqlParameter("@qad", qad);
    //        param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
    //        param[2].Direction = ParameterDirection.Output;
    //        SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);
    //        return Convert.ToBoolean(param[2].Value);

    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }


    //}

    //protected static bool IsExistQad(string qad)
    //{
    //    try
    //    {
    //        string strName = "sp_rdw_checkQad";
    //        SqlParameter[] param = new SqlParameter[2];
    //        param[0] = new SqlParameter("@qad", qad);
    //        param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
    //        param[1].Direction = ParameterDirection.Output;
    //        SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);
    //        return Convert.ToBoolean(param[1].Value);

    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }


    //}

    protected void gvRWDQad_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRWDQad.PageIndex = e.NewPageIndex;
        BindAddQADData();
    }
    protected void gvRWDQad_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int intRow = 0;
        string part = string.Empty;
        if (e.CommandName.ToString() == "gobom")
        {
            part = e.CommandArgument.ToString();
            ltlAlert.Text = "var w=window.open('/RDW/RDW_BomViewDoc.aspx?part=" + part + "&mid=" + Request.QueryString["mid"] + "&rm=" + DateTime.Now.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();";

        }
    }
    protected void gvRWDQad_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int index = e.Row.RowIndex + gvRWDQad.PageIndex * gvRWDQad.PageSize;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[4].Text = ((DataBoundLiteralControl)e.Row.Cells[4].Controls[0]).Text.ToString().Replace(";", "</br>");
            if (lblApplyId.Text == "" || lblApplyId.Text == "0")
            {
                if (Request.QueryString["mid"] != null && Request.QueryString["mid"].ToString() != "" && Request.QueryString["mid"].ToString() != "0" && (dtQad.Rows[index]["canselect"].ToString() == "1" || dtQad.Rows[index]["log"].ToString().ToLower().Contains("false")))
                //if (Request.QueryString["mid"] != null && Request.QueryString["mid"].ToString() != "" && Request.QueryString["mid"].ToString() != "0")
                {
                    e.Row.Cells[0].Enabled = true;
                }
                else
                {
                    e.Row.Cells[0].Enabled = false;
                }
            }
            else
            {
                e.Row.Cells[0].Enabled = false;
            }
            //e.Row.Cells[4].Enabled = this.Security["170016"].isValid;

            //if (!this.Security["170021"].isValid)
            //{
            //    if (!this.Security["170022"].isValid)
            //    {
            //        e.Row.Cells[3].Enabled = false;
            //        e.Row.Cells[3].Font.Bold = true;
            //    }
            //}

        }
    }

    protected void ItemCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        int index = ((GridViewRow)(chk.NamingContainer)).RowIndex + gvRWDQad.PageSize * gvRWDQad.PageIndex;    //通过NamingContainer可以获取当前checkbox所在容器对象，即gridviewrow   
        DataTable dt = dtQad;

        if (chk.Checked)
        {
            dt.Rows[index]["selected"] = 1;
        }
        else
        {
            dt.Rows[index]["selected"] = 0;
        }
        //BindAddQADData();
    }

    protected void gvRWDQad_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strID = gvRWDQad.DataKeys[e.RowIndex]["id"].ToString();
        if (rdw.DeleteRdwQad(strID))
        {
            BindAddQADData();
        }
        else
        {
            ltlAlert.Text = "alert('delete data failed!'); ";
            BindAddQADData();
            return;
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (!QadIsSelected())
        {
            ltlAlert.Text = "alert('Add Qad Part or Select Qad Part,Please!'); ";
            return;
        }
        if (txtApplyReason.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Enter the Apply Reason,Please!'); ";
            return;
        }
        if (txtApproveName.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Select one Approver,Please!'); ";
            return;
        }

        if (chkEmail.Checked)
        {
            if (txt_ApproveEmail.Text == string.Empty)
            {
                ltlAlert.Text = "alert('Enter the Approver Email if you want to send email,Please!'); ";
                return;
            }
        }
        string mid = Request.QueryString["mid"].ToString();
        string strApprover = txt_approveID.Text.ToString();
        string strApproverName = txtApproveName.Text.ToString();
        string applyReason = txtApplyReason.Text.Trim().ToString();
        int uID = int.Parse(Session["uID"].ToString());
        string uName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);
        int applyId = rdw.AddProjQadLink(mid, strApprover, strApproverName, applyReason, uID, uName, dtQad);
        if (applyId > 0)
        {
            bool isSuccess=true;
            string message = "";
            lblApplyId.Text = applyId.ToString();

            if (chkEmail.Checked)
            {               
                isSuccess=rdw.SendMail(mid, lblProjectData.Text.Trim(), lblProdCodeData.Text.Trim(), txt_ApproveEmail.Text.Trim(), strApproverName, Session["Email"].ToString(), Session["uName"].ToString(), applyReason, applyId.ToString(), out message);
            }
            if (isSuccess)
            {
                ltlAlert.Text = "alert('Apply successfully!');";
                
            }
            else
            {
                if (message != "")
                {
                    ltlAlert.Text = "alert('" + message + "');";
                }
            }
            ltlAlert.Text += " window.location.href='/RDW/RDW_ProjQadApply.aspx?type=" + Request.QueryString["type"].ToString() + "&pqmId=" + applyId + "&mid=" + mid + "&fr= &@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
            
        }
        else
        {
            ltlAlert.Text = "alert('Database Operation Failed!');";
            return;
        }
    }

   

    protected void btn_approve_Click(object sender, EventArgs e)
    {
        if (txtApprOpin.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Enter the Approve Opinion,Please!'); ";
            return;
        }
        string mid = Request.QueryString["mid"].ToString();
        int pqid = int.Parse(Request.QueryString["pqmid"].ToString());
        string strApprover = txt_approveID.Text.ToString();
        string strApproverName = txtApproveName.Text.ToString();
        string applyReason = txtApplyReason.Text.Trim().ToString();
        string applyId = lblApplyId.Text;
        int uID = int.Parse(Session["uID"].ToString());
        string uName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);
        string approveOpoin = txtApprOpin.Text.Trim().ToString();

        if (rdw.UpdateProjQadLink(pqid,strApprover,strApproverName, uID, uName,approveOpoin, true))
        {
            bool isSuccess = true;
            string message = "";
            if (txt_ApproveEmail.Text.Trim() != "" && chkEmail.Checked)
            {
                isSuccess = rdw.SendMail(mid, lblProjectData.Text.Trim(), lblProdCodeData.Text.Trim(), txt_ApproveEmail.Text.Trim(), strApproverName, Session["Email"].ToString(), Session["uName"].ToString(), applyReason, applyId.ToString(), out message);
            }
            if (isSuccess)
            {
                ltlAlert.Text = "alert('Successfully!');";
                
            }
            else
            {
                if (message != "")
                {
                    ltlAlert.Text = "alert('" + message + "');";
                }
            }
            ltlAlert.Text += " window.location.href='/RDW/RDW_ProjQadApply.aspx?type=" + Request.QueryString["type"].ToString() + "&pqmId=" + pqid + "&mid=" + mid + "&fr= &@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
            return;
        }
        else
        {
            ltlAlert.Text = "alert('Database Operation Failed!');";
            return;
        }
    }

    protected void btn_diaApp_Click(object sender, EventArgs e)
    {
        if (txtApprOpin.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Enter the Approve Opinion,Please!'); ";
            return;
        }
        string mid = Request.QueryString["mid"].ToString();
        int pqid = int.Parse(Request.QueryString["pqmid"].ToString());
        string approveOpoin = txtApprOpin.Text.Trim().ToString();
        int uID = int.Parse(Session["uID"].ToString());
        string uName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);

        if (!rdw.UpdateProjQadLink(pqid, null, null, uID, uName, approveOpoin, false))
        {
            ltlAlert.Text = "alert('Database Operation Failed!');";
            return;
        }
        else
        {
            string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
            //ltlAlert.Text = "window.close();window.opener.location='/RDW/RDW_ProjQadApply.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';";
            ltlAlert.Text += " window.location.href='/RDW/RDW_ProjQadApply.aspx?type=" + Request.QueryString["type"].ToString() + "&pqmId=" + pqid + "&mid=" + mid + "&fr= &@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        string strST = Request.QueryString["st"] == null ? "" : Convert.ToString(Request.QueryString["st"]);
        string type = Request.QueryString["type"];
        string strMID = type == "new" ? "0" : Convert.ToString(Request.QueryString["mid"]);

        Response.Redirect("/RDW/rdw_ProjectQadApproveList.aspx?type=" + type + "&mid=" + strMID + "&fr=" + strQuy + "&st=" + strST + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
        
    }
   
    protected void btnDoc_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('/RDW/RDW_doclist.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&rm=" + DateTime.Now.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();";
        //BindData();
    }



    //private DataTable getProjQadLink(string strID, int pqapplyId)
    //{
    //    //try
    //    //{
    //    string strSql = "sp_RDW_selectProjQadApply";

    //        SqlParameter[] sqlParam = new SqlParameter[2];
    //        sqlParam[0] = new SqlParameter("@mid", strID);
    //        sqlParam[1] = new SqlParameter("@applyid", pqapplyId);
    //        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    //throw ex;
    //    //    return null;
    //    //}
    //}

    ///// <summary>
    ///// 新建QAD申请
    ///// </summary>
    ///// <param name="mid"></param>
    ///// <param name="strApprover"></param>
    ///// <param name="strApproverName"></param>
    ///// <param name="applyReason"></param>
    ///// <param name="uID"></param>
    ///// <param name="uName"></param>
    ///// <returns></returns>
    //private int AddProjQadLink(string mid, string strApprover, string strApproverName, string applyReason, int uID, string uName)
    //{
    //    string strSql = "sp_RDW_InsertProjQadApply";

    //    SqlParameter[] sqlParam = new SqlParameter[6];
    //    sqlParam[0] = new SqlParameter("@mid", mid);
    //    sqlParam[1] = new SqlParameter("@approverBy", strApprover);
    //    sqlParam[2] = new SqlParameter("@approverName", strApproverName);
    //    sqlParam[3] = new SqlParameter("@applyReason", applyReason);
    //    sqlParam[4] = new SqlParameter("@uId", uID);
    //    sqlParam[5] = new SqlParameter("@uName", uName); 

    //    return Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));
    //}

    ///// <summary>
    ///// 审批结果
    ///// </summary>
    ///// <param name="mid"></param>
    ///// <param name="approveBy"></param>
    ///// <param name="approveOpoin"></param>
    ///// <param name="p"></param>
    ///// <returns></returns>
    //private bool UpdateProjQadLink(int pqid, string strApprover, string strApproverName, int uID, string uName, string approveOpoin, bool result)
    //{
    //    string strSql = "sp_RDW_UpdateProjQadApprove";

    //    SqlParameter[] sqlParam = new SqlParameter[7];
    //    sqlParam[0] = new SqlParameter("@pqid", pqid);
    //    if (!string.IsNullOrEmpty(strApproverName))
    //    {
    //        sqlParam[1] = new SqlParameter("@approverBy", strApprover);
    //        sqlParam[2] = new SqlParameter("@approverName", strApproverName);
    //    }
    //    sqlParam[3] = new SqlParameter("@uId", uID);
    //    sqlParam[4] = new SqlParameter("@uName", uName);
    //    sqlParam[5] = new SqlParameter("@approveOpinion", approveOpoin);
    //    sqlParam[6] = new SqlParameter("@appresult", result);
    //    return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, sqlParam));

    //}

    protected void BtnExport_Click(object sender, EventArgs e)
    {
        string pqid = lblApplyId.Text;

        //ltlAlert.Text = "window.open('rdw_ProjQadListExport.aspx?pqmid=" + pqid + "', '_blank');";
        string title = "120^<b>Project Category</b>~^300^<b>Project</b>~^100^<b>Project Code</b>~^110^<b>QAD</b>~^300^<b>QAD Desc</b>~^";
        string strsql = @"	 Select  C.cate_code,  M.RDW_Project, M.RDW_ProdCode, Q.qad, pt.pt_desc
	 from  RDW_APPLYQAD Q
	 INNER JOIN dbo.rdw_ProjQadApply A ON Q.rdw_pqapplyid=A.rdw_pqid
	 LEFT Join RDW_Mstr M On A.RDW_MstrID = M.RDW_MstrID
	 Left Join RDW_Category C on M.RDW_Category = C.cate_id
	 Left Join 
		(
			Select distinct pt_part ,pt_desc1 + ' ' + pt_desc2 As pt_desc
			From Qad_Data..pt_mstr 
			Where pt_domain='szx'
		)  pt On pt.pt_part = Q.qad
	 where Q.rdw_pqapplyid = '" + pqid + "' AND Q.selected=1";

        this.ExportExcel(strConn, title, strsql, false);
   
    }
    protected void BtnImport_Click(object sender, EventArgs e)
    {
        ImportExcelFile();      
    }

    private void ImportExcelFile()
    {
        //String strSQL = "";
        DataSet ds = new DataSet();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        //Boolean boolError = false;

        strCatFolder = Server.MapPath("/import");
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }

        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return;
        }
        strUserFileName = strFileName;

        //Modified By Shanzm 2012-12-27：唯一字符串可以设定为“年月日时分秒毫秒”
        string strKey = string.Format("{0:yyyyMMddhhmmssfff}", DateTime.Now);
        strFileName = strCatFolder + "\\" + strKey + strUserFileName;

        if (filename1.PostedFile != null)
        {
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                string[] arrTable = null;
                try
                {
                     arrTable = rdw.GetExcelSheetName(strFileName);
                }
                catch
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                    ltlAlert.Text = "alert('导入文件必须是Excel格式(.xls)或者模板及内容正确!');";
                    return;
                }
                string message = "";
                DataTable dt = dtQad;
                foreach (string aa in arrTable)
                {
                    if (aa != null)
                    {

                        try
                        {
                            ds = rdw.GetExcelContents(strFileName, aa);
                        }
                        catch
                        {
                            if (File.Exists(strFileName))
                            {
                                File.Delete(strFileName);
                            }

                            ltlAlert.Text = "alert('导入文件必须是Excel格式(.xls)或者模板及内容正确!" + aa + "');";
                            return;
                        }

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Columns[0].ColumnName.ToLower() != "qad")
                            {
                                ds.Reset();
                                ltlAlert.Text = "alert('导入文件的模版不正确!');";
                                return;
                            }

                            
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string qad = ds.Tables[0].Rows[i][0].ToString().Trim();
                                string detailMessage="";
                                if (CheckQad(qad, i + 2, aa, out detailMessage))
                                {
                                    DataRow newRow = dt.NewRow();
                                    newRow["qad"] = qad;
                                    newRow["selected"] = 1;
                                    if (qadLog != "")
                                    {
                                        newRow["log"] = qadLog;
                                    }
                                    if (qadDate != "")
                                    {
                                        newRow["date"] = qadDate;
                                    }
                                    newRow["reason"] = qadReason;
                                    newRow["pkgcode"] = qadPkgCode;
                                    newRow["description"] = qadDesc;
                                    dt.Rows.Add(newRow);
                                }
                                else
                                {
                                    message += detailMessage;
                                }
                            }
                        }

                    }
                    if (message != "")
                    {
                        ltlAlert.Text = "alert('" + message + "')";
                    }
                    BindAddQADData();
                }
            }
        }
    }

    private bool QadIsSelected()
    {
        foreach (DataRow row in dtQad.Rows)
        {
            if (row["selected"].ToString() == "1")
            {
                return true;
            }
        }
        return false;
    }
    protected void btnSelectAll_Click(object sender, EventArgs e)
    {
        foreach (DataRow row in dtQad.Rows)
        {
            if (row["canselect"].ToString() == "1" || row["log"].ToString().ToLower() != "true")
            {
                row["selected"] = 1;
            }
        }
        BindAddQADData();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        foreach (DataRow row in dtQad.Rows)
        {
            row["selected"] = 0;
        }
        BindAddQADData();
    }
}