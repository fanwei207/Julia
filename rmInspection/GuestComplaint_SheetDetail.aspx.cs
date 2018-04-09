using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class rmInspection_GuestComplaint_SheetList : BasePage
{
    string strconn = ConfigurationManager.AppSettings["SqlConn.rmInspection"];

    public string EffectTR = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null || Request.QueryString["ID"] == string.Empty)
            {
                hidLblID.Value = Request.QueryString["ID"];
                hidGuestComplaintNo.Value = Request.QueryString["no"];
                BindDecideApproach();//绑定决定方式模块信息
                BindAfterCheckedApproach();//绑定定了决定方式
            }
            
            BindDecideApproach();
            BindResponsibleParty();
            BindResponsiblePersons(); //绑定处理人                     
            BindData();//绑定财务填写方式模块  
            Bind();
        BindFiles();
        BindImportFiles();
        BindDisposed();
        }
    }

    private void BindAfterCheckedApproach()
    {
        gvDecide.DataSource = selectDecideApproach(hidGuestComplaintNo.Value.ToString(), 1);
        gvDecide.DataBind();
    }

    private void BindImportFiles()
    {
        DataTable dt = getGuestCompImportFiles();
        gvImport.DataSource = dt;
        gvImport.DataBind();
    }

    private DataTable getGuestCompImportFiles()
    {
        string sqlstr = "sp_comp_selectImportFiles";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@compNo", hidGuestComplaintNo.Value.ToString());

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }
    private void BindApprInfo()
    {
        for (int i = 1; i <= 7; i++)
        {
            string moduleEnName = getModuleEnName(i);
            string state = "";
            switch (moduleEnName)
            {
                case "feedback":
                    state = selectModuleInfo(moduleEnName);
                    break;
                case "managerApr":
                    state = selectModuleInfo(moduleEnName);
                    break;
                case "decideAproach":
                    state = selectModuleInfo(moduleEnName);
                    break;
                case "preResult":
                    state = selectModuleInfo(moduleEnName);
                    break;
            }
        }
    }

    private string selectModuleInfo(string moduleEnName)
    {
        string sqlstr = "sp_Comp_selectModuleInfo";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@no", hidGuestComplaintNo.Value.ToString());
        param[1] = new SqlParameter("@moduleEnName", moduleEnName);
        param[2] = new SqlParameter("@message", SqlDbType.NVarChar, 500);
        param[2].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, sqlstr, param).ToString();
        return Convert.ToString(param[2].Value);
    }

    private void BindDisposed()
    {
        for (int i = 1; i <= 7; i++)
        {
            string moduleEnName = getModuleEnName(i);

            switch (moduleEnName)
            {
                case "executor":
                    executorContent.InnerHtml = selectModuleContent(moduleEnName);
                    break;
                case "feedback":
                    feedbackContent.InnerHtml = selectModuleContent(moduleEnName);
                    break;
                case "managerApr":
                    managerAprContent.InnerHtml = selectModuleContent(moduleEnName);
                    break;
                case "preResult":
                    preResultContent.InnerHtml = selectModuleContent(moduleEnName);
                    break;
            }
        }
    }

    private string selectModuleContent(string moduleEnName)
    {
        string sqlstr = "sp_Comp_selectModuleContent";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@no", hidGuestComplaintNo.Value.ToString());
        param[1] = new SqlParameter("@moduleEnName", moduleEnName);
        param[2] = new SqlParameter("@content", SqlDbType.NVarChar, 500);
        param[2].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, sqlstr, param).ToString();
        return Convert.ToString(param[2].Value);
    }

    private void BindFiles()
    {
        DataTable dt = getGuestCompFileList();
        gvFile.DataSource = dt;
        gvFile.DataBind();
    }
    private DataTable getGuestCompFileList()
    {
        string sql = "Select * From GuestComp_FileList Where GuestComp_No = '" + hidGuestComplaintNo.Value.ToString() + "'";

        return SqlHelper.ExecuteDataset(strconn, CommandType.Text, sql).Tables[0];
    }
    private void BindResponsiblePersons()
    {

        for (int i = 1; i <= 7; i++)
        {
            string moduleEnName = getModuleEnName(i);

            switch (moduleEnName)
            {
                case "executor":
                    executor.Text = selectResponsiblePersons(moduleEnName);
                    break;
                case "feedback":
                    feedback.Text = selectResponsiblePersons(moduleEnName);
                    break;
                case "managerApr":
                    managerApr.Text = selectResponsiblePersons(moduleEnName);
                    break;
                case "decideAproach":
                    decideAproach.Text = selectResponsiblePersons(moduleEnName);
                    break;
                case "financeAproach":
                    financeAproach.Text = selectResponsiblePersons(moduleEnName);
                    break;
                case "preResult":
                    preResult.Text = selectResponsiblePersons(moduleEnName);
                    break;
                case "applyList":
                    applyList.Text = selectCreateOrderBy(hidGuestComplaintNo.Value.ToString());
                    break;
            }
        }

    }

    private string selectCreateOrderBy(string GuestCompNo)
    {
        string sqlstr = "sp_comp_getOrderCreateBy";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@GuestCompNo", GuestCompNo);
        param[1] = new SqlParameter("@OrderCreateBy", SqlDbType.NVarChar, 500);
        param[1].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, sqlstr, param).ToString();
        return Convert.ToString(param[1].Value);
    }

    private string getModuleEnName(int moduleID)
    {
        string sqlstr = "sp_getModuleEnName";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@moduleID", moduleID);
        param[1] = new SqlParameter("@moduleEnName", SqlDbType.VarChar, 50);
        param[1].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, sqlstr, param).ToString();
        return Convert.ToString(param[1].Value);
    }

    private string selectResponsiblePersons(string moduleEnName)
    {
        string sqlstr = "sp_selectResponsiblePersons";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@moduleEnName", moduleEnName);
        param[1] = new SqlParameter("@handlePersonName", SqlDbType.NVarChar, 500);
        param[1].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, sqlstr, param).ToString();
        return Convert.ToString(param[1].Value);
    }

    private void BindResponsibleParty()
    {
        radDuty.Items.Clear();
        radDuty.DataSource = selectDutyParty();
        radDuty.DataBind();
    }

    private DataTable selectDutyParty()
    {
        string sqlstr = "sp_selectResponsibleParty";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@dutyname", "");
        param[1] = new SqlParameter("@responsiblename", "");

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, sqlstr, param).Tables[0];

    }
    //初始化页面
    private void Bind()
    {
        DataTable GuestComp = getMstrByNo(hidLblID.Value);

        foreach (DataRow row in GuestComp.Rows)
        {
            lblGuestNo.Text = row["GuestNo"].ToString();
            lblGuestName.Text = row["GuestName"].ToString();
            lblGuestLevel.Text = row["GuestLevel"].ToString();
            lblSeverity.Text = row["SeverityName"].ToString();
            lblReceivedDate.Text = row["ReceivedDate"].ToString();
            lblApproach.Text = row["ApproachNames"].ToString();
            txtNo.Text = row["ShipPlanNo"].ToString();
            txtPartNo.Text = row["ShipPartNo"].ToString();
            txtNum.Text = row["ShipNum"].ToString();
            txtDate.Text = row["ShipDate"].ToString();
            txtActualPrice.Text = row["ActualAmount"].ToString();
            txtFinishedDate.Text = row["FinishedDate"].ToString();

            #region 绑定填写完意见的责任方
            BindCheckedParty();
            #endregion

            #region 绑定按钮部分
            if (string.IsNullOrEmpty(row["PreResult"].ToString()))
            {
                btnPreAgree.Enabled = true;
                btnPreNotAgree.Enabled = true;
                radDuty.Enabled = true;
            }
            else
            {
                if (row["PreResult"].ToString() == "同意")
                {
                    btnPreAgree.Text = "Agree by" + ' ' + row["PreResultName"].ToString();
                    btnPreAgree.Enabled = false;
                    btnPreNotAgree.Enabled = false;
                    radDuty.Enabled = false;
                }
                else
                {
                    btnPreNotAgree.Text = "不同意";
                    btnPreAgree.Enabled = true;
                    btnPreNotAgree.Enabled = true;
                    radDuty.Enabled = true;
                }
            }

            if (string.IsNullOrEmpty(row["FinanceResult"].ToString()))
            {
                btnFinAgree.Enabled = true;
                btnFinDisagree.Enabled = true;
            }
            else
            {
                if (row["FinanceResult"].ToString() == "同意")
                {
                    btnFinAgree.Text = "Agree by" + ' ' + row["FinanceResultName"].ToString();
                    btnFinAgree.Enabled = false;
                    btnFinDisagree.Enabled = false;
                    //隐藏价格编辑按钮
                    gvFinance.Columns[2].Visible = false;
                    BindData();
                }
                else
                {
                    btnFinDisagree.Text = "不同意";
                    btnFinAgree.Enabled = true;
                    btnFinDisagree.Enabled = true;
                    //隐藏价格编辑按钮
                    gvFinance.Columns[2].Visible = true;
                }
            }

            if (string.IsNullOrEmpty(row["DecideResult"].ToString()))
            {
                btnApproachAgree.Enabled = true;
                btnApproachDisagree.Enabled = true;
            }
            else
            {
                if (row["DecideResult"].ToString() == "同意")
                {
                    btnApproachAgree.Text = "Agree by" + ' ' + row["DecideApproachName"].ToString();
                    btnApproachAgree.Enabled = false;
                    btnApproachDisagree.Enabled = false;
                    //将checkbox隐藏
                    gvDecide.Columns[0].Visible = false;
                    BindAfterCheckedApproach();
                }
                else
                {
                    btnApproachDisagree.Text = "不同意";
                    btnApproachAgree.Enabled = true;
                    btnApproachDisagree.Enabled = true;
                    gvDecide.Columns[0].Visible = true;
                    BindDecideApproach();
                }
            }

            if (string.IsNullOrEmpty(row["FeedbackResult"].ToString()))
            {
                feedbackAgree.Enabled = true;
                feedbackDisagree.Enabled = true;
            }
            else
            {
                if (row["FeedbackResult"].ToString() == "同意")
                {
                    feedbackAgree.Text = "Agree by" + ' ' + row["FeedbackResultName"].ToString();
                    feedbackAgree.Enabled = false;
                    feedbackDisagree.Enabled = false;
                    txtNo.Enabled = false;
                    txtPartNo.Enabled = false;
                    txtNum.Enabled = false;
                    txtDate.Enabled = false;
                }
                else
                {
                    feedbackDisagree.Text = "不同意";
                    feedbackAgree.Enabled = true;
                    feedbackDisagree.Enabled = true;
                }
            }

            if (string.IsNullOrEmpty(row["ManagerResult"].ToString()))
            {
                managerAgree.Enabled = true;
                managerDisagree.Enabled = true;
            }
            else
            {
                if (row["ManagerResult"].ToString() == "同意")
                {
                    managerAgree.Text = "Agree by" + ' ' + row["ManagerResultName"].ToString();
                    managerAgree.Enabled = false;
                    managerDisagree.Enabled = false;
                }
                else
                {
                    managerDisagree.Text = "不同意";
                    managerAgree.Enabled = true;
                    managerDisagree.Enabled = true;
                }
            }

            if (string.IsNullOrEmpty(row["ExecuteResult"].ToString()))
            {
                btnFinishOrder.Enabled = true;
            }
            else
            {
                if (row["ExecuteResult"].ToString() == "结单")
                {
                    btnFinishOrder.Text = "Close by" + ' ' + row["ExecuteName"].ToString();
                    btnFinishOrder.Enabled = false;
                    txtActualPrice.Enabled = false;
                    txtFinishedDate.Enabled = false;
                }
            }
            #endregion
        }

    }
    private DataTable GetFinishedPartyInfo(string dutyName)
    {
        string strSql = "sp_comp_selectResponsiblePartyFinished";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@compNo", hidGuestComplaintNo.Value.ToString());
        param[1] = new SqlParameter("@dutyName", dutyName);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, strSql, param).Tables[0];
    }
    private void BindCheckedParty()
    {
        string names = getCheckedDutyByNo();
        string[] sArray2 = names.Split(';');
        for (int i = 0; i < radDuty.Items.Count; i++)
        {
            foreach (string a in sArray2)
            {
                if (radDuty.Items[i].ToString() == a.ToString())
                {
                    radDuty.Items[i].Selected = true;
                }
            }
        }
        BindCheckedPartyInfo();
    }
    private void BindCheckedPartyInfo()
    {
        gv1.DataSource = this.GetCheckedPartyInfo();
        gv1.DataBind();
    }

    private DataTable GetCheckedPartyInfo()
    {
        string checkedDutys = getCheckedDutyByNo();
        string strSql = "sp_selectResponsiblePartyChecked";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@dutyname", checkedDutys);
        param[1] = new SqlParameter("@compNo", hidGuestComplaintNo.Value.ToString());

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, strSql, param).Tables[0];
    }
    private string getCheckedDutyByNo()
    {
        string str = "sp_comp_selectCompCheckedDutyByNo";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@compNo", hidGuestComplaintNo.Value.ToString());
        param[1] = new SqlParameter("@checkduty", SqlDbType.NVarChar, 500);
        param[1].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, str, param).ToString();
        return Convert.ToString(param[1].Value);
    }
    private DataTable getMstrByNo(string complaintID)
    {
        string str = "sp_selectComplaintSheetDetailById";
        SqlParameter param = new SqlParameter("@complaintID", complaintID);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("GuestComplaint_SheetList.aspx");
    }
    protected void radDuty_SelectedIndexChanged(object sender, EventArgs e)
    {
        string preResultLoginNames = getResultLoginNames(preResult.Text);
        string checkedModuleName = "preResult";
        if (checkISAuthority(preResultLoginNames, Session["uID"].ToString(), checkedModuleName) && Session["uRole"].ToString() != "1")
        {
            this.Alert("您没有处理此模块的权限");
            return;
        }
        else
        {
            BindCheckedPartyInfo();
        }
    }
    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {       
        string _no = hidGuestComplaintNo.Value.ToString();
        int _moduleId = 8;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            if (!string.IsNullOrEmpty(rowView["DutyName"].ToString()) && !string.IsNullOrEmpty(rowView["ResDesc"].ToString()))
            {
                EffectTR += "<tr><td class=\"FixedGridHeight\" style=\"height:50px;\"></td>";
                EffectTR += " <td style=\"word-break:break-all; word-wrap:break-word;\" colspan=\"2\">" + rowView["DutyName"].ToString() + "</td>";
                #region 绑定留言，同时设置EffectTR
                EffectTR += " <td colspan=\"13\" style=\"text-align: left;\">";
                foreach (DataRow row in this.GetFinishedPartyInfo(rowView["DutyName"].ToString()).Rows)
                {
                    e.Row.Cells[1].Text += string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(row["CreateDate"])) +
                        "&nbsp;&nbsp;&nbsp;&nbsp;" + row["CreateName"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;";
                    EffectTR += string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(row["CreateDate"])) + "&nbsp;&nbsp;&nbsp;&nbsp;" + row["CreateName"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;";
                    EffectTR += "<br />";
                    EffectTR += row["ResDesc"].ToString();
                    EffectTR += "<br />";
                    if (!string.IsNullOrEmpty(row["ResFileName"].ToString()))
                    {
                        e.Row.Cells[1].Text += "Attachment:<a href='" + row["ResFilePath"].ToString() + "' target='_blank'>" + row["ResFileName"].ToString() + "</a>" + "<br />";
                        EffectTR += "Attachment:<a href='" + row["ResFilePath"].ToString() + "' target='_blank'>" + row["ResFileName"].ToString() + "</a>" + "<br />";
                    }
                }
                EffectTR += " </td>";
                EffectTR += " <td style=\"word-break:break-all; word-wrap:break-word;\" colspan=\"5\">" + rowView["ResponsiblePersonName"].ToString() + "</td>";
                #endregion
                EffectTR += "</tr>";
            }
        } 

            for (int i = 0; i < radDuty.Items.Count; i++)
            {                
                string LoginNames = getResultLoginNames(e.Row.Cells[2].Text);
                if (checkISDutyAuthority(e.Row.Cells[0].Text, LoginNames) && Session["uRole"].ToString() != "1")
                {
                    e.Row.Attributes.Add("OnDblClick", "alert('您没有处理此模块的权限')");
                    return;
                }
                else
                {
                    if (checkISAllPartyFinished(hidGuestComplaintNo.Value.ToString()) && Session["uRole"].ToString() != "1")
                    {                       
                        e.Row.Attributes.Add("OnDblClick", "alert('不允许此操作')");
                        return;
                    }
                    else
                    {                        
                        string url = "/rmInspection/GuestComplaint_ResConent.aspx?no=" + _no + "&moduleId=" + _moduleId + "&dutyName=" + e.Row.Cells[0].Text;
                        e.Row.Attributes.Add("OnDblClick", "$.window('review', '70%', '80%','" + url + "','','true')");
                    }                
                }
            }
            
    }
    //判断责任方是否有权限
    private bool checkISDutyAuthority(string dutyName, string names)
    {
        string strSql = "sp_checkDutyIsAuthority";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@uid", Session["uID"]);
        param[1] = new SqlParameter("@dutyName", dutyName);
        param[2] = new SqlParameter("@names", names);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strconn, CommandType.StoredProcedure, strSql, param));
    }
    //返回选中的责任方
    private string getCheckedItem()
    {
        string checkedItem = "";
        for (int i = 0; i < radDuty.Items.Count; i++)
        {
            if (radDuty.Items[i].Selected)
            {
                checkedItem += radDuty.Items[i] + ";";
            }
        }
        if (checkedItem.Length > 0)
            checkedItem = checkedItem.Substring(0, checkedItem.Length - 1);

        return checkedItem;
    }
    protected void btnPreAgree_Click(object sender, EventArgs e)
    {
        string preResultLoginNames = getResultLoginNames(preResult.Text);
        string checkedModuleName = "preResult";
        string checkedDutys = getCheckedItem();
        if (checkISAuthority(preResultLoginNames, Session["uID"].ToString(), checkedModuleName) && Session["uRole"].ToString() != "1")
        {
            this.Alert("您没有处理此模块的权限");
            return;
        }
        else
        {
            string strName = "sp_comp_approvePreResult";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@no", hidGuestComplaintNo.Value.ToString());
            param[1] = new SqlParameter("@isAgree", "同意");
            param[2] = new SqlParameter("@checkedDutys", checkedDutys);
            param[3] = new SqlParameter("@uid", Session["uID"].ToString());
            param[4] = new SqlParameter("@uname", Session["uName"].ToString());
            param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[5].Direction = System.Data.ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[5].Value))
            {
                this.Alert("操作失败，请联系管理员");
            }
            else
            {
                int num = 5;
                for (int i = 0; i < radDuty.Items.Count; i++)
                {
                    if (radDuty.Items[i].Selected)
                        num--;
                }
                if (num == 5)
                {
                    this.Alert("请选择责任方");
                    return;
                }
                btnPreAgree.Text = "Agree（By " + Session["uName"].ToString() + "）";
                btnPreAgree.Enabled = false;
                btnPreNotAgree.Enabled = false;
                BindCheckedPartyInfo();
                string Dutys = getCheckedDutyByNo();
                Semail(hidGuestComplaintNo.Value.ToString(), checkedModuleName,"resParty", Dutys);            
            }
        }
    }
    //返回loginName
    private string getResultLoginNames(string preResultNames)
    {
        //删除字符串中中文后得到的字符串
        string AfterDelChinese = getDelChineseStr(preResultNames);
        //
        string loginNames = "";
        string names = AfterDelChinese;
        names = names.Replace('-', ' ');
        string[] sArray2 = names.Split(';');
        foreach (string i in sArray2)
        {
            loginNames += i.ToString().Trim() + ';';
        }
        if (loginNames.Length > 0)
            loginNames = loginNames.Substring(0, loginNames.Length - 1);

        return loginNames;
    }
    //删除LoginName中的中文
    private string getDelChineseStr(string preResultNames)
    {
        string retValue = preResultNames;
        if (System.Text.RegularExpressions.Regex.IsMatch(preResultNames, @"[\u4e00-\u9fa5]"))
        {
            retValue = string.Empty;
            var strsStrings = preResultNames.ToCharArray();
            for (int index = 0; index < strsStrings.Length; index++)
            {
                if (strsStrings[index] >= 0x4e00 && strsStrings[index] <= 0x9fa5)
                {
                    continue;
                }
                retValue += strsStrings[index];
            }
        }
        return retValue;
    }
    //判断是否有权限
    private bool checkISAuthority(string preResultNames, string uid, string modleEnName)
    {
        string strSql = "sp_checkIsAuthority";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@preResultNames", preResultNames);
        param[1] = new SqlParameter("@uid", uid);
        param[2] = new SqlParameter("@modleEnName", modleEnName);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strconn, CommandType.StoredProcedure, strSql, param));
    }
    protected void btnPreNotAgree_Click(object sender, EventArgs e)
    {
        string preResultLoginNames = getResultLoginNames(preResult.Text);
        string checkedModuleName = "preResult";
        if (checkISAuthority(preResultLoginNames, Session["uID"].ToString(), checkedModuleName))
        {
            this.Alert("您没有处理此模块的权限");
            return;
        }
        else
        {
            string strName = "sp_comp_approvePreResult";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@no", hidGuestComplaintNo.Value.ToString());
            param[1] = new SqlParameter("@isAgree", "不同意");
            param[2] = new SqlParameter("@checkedDutys", "");
            param[3] = new SqlParameter("@uid", Session["uID"].ToString());
            param[4] = new SqlParameter("@uname", Session["uName"].ToString());
            param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[5].Direction = System.Data.ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[5].Value))
            {
                this.Alert("操作失败，请联系管理员");
            }
            else
            {
                btnPreNotAgree.Text = "Disgree（By " + Session["uName"].ToString() + "）";
                btnPreAgree.Enabled = false;
                btnPreNotAgree.Enabled = false;
                Bind();
            }
        }
    }
    protected void btnFinAgree_Click(object sender, EventArgs e)
    {
        string preResultLoginNames = getResultLoginNames(preResult.Text);
        string checkedModuleName = "financeAproach";
        if (checkISAuthority(preResultLoginNames, Session["uID"].ToString(), checkedModuleName) && Session["uRole"].ToString() != "1")
        {
            this.Alert("您没有处理此模块的权限");
            return;
        }
        else
        {
            if (!checkISAllPartyFinished(hidGuestComplaintNo.Value.ToString()))
            {
                ltlAlert.Text = "alert('责任方还未完成')";
                return;
            }
            if (checkAllPriceIsNull(hidGuestComplaintNo.Value.ToString()))
            {
                ltlAlert.Text = "alert('请填写金额')";
                BindCheckedPartyInfo();
                return;
            }
            string strName = "sp_comp_approveFinResult";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@no", hidGuestComplaintNo.Value.ToString());
            param[1] = new SqlParameter("@isAgree", "同意");
            param[2] = new SqlParameter("@uid", Session["uID"].ToString());
            param[3] = new SqlParameter("@uname", Session["uName"].ToString());
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = System.Data.ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[4].Value))
            {
                this.Alert("操作失败，请联系管理员");
            }
            else
            {
                btnFinAgree.Text = "Agree（By " + Session["uName"].ToString() + "）";
                btnFinDisagree.Enabled = false;
                btnFinAgree.Enabled = false;
                BindData();
                
                gvFinance.Columns[2].Visible = false;                
                Semail(hidGuestComplaintNo.Value.ToString(),checkedModuleName,"decideAproach","");
            }
            BindCheckedPartyInfo();
            BindDecideApproach();
        }
    }

    private bool checkISAllPartyFinished(string GuestCompNo)
    {
        string str = "sp_comp_checkISAllPartyFinished";
        SqlParameter param = new SqlParameter("@GuestCompNo", GuestCompNo);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strconn, CommandType.StoredProcedure, str, param));
    }
    private bool checkAllPriceIsNull(string GuestCompNo)
    {
        string str = "sp_comp_checkALLPriceIsNull";
        SqlParameter param = new SqlParameter("@GuestCompNo", GuestCompNo);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strconn, CommandType.StoredProcedure, str, param));
    }
    //绑定决定的赔偿方式
    private void BindDecideApproach()
    {
        gvDecide.DataSource = selectDecideApproach(hidGuestComplaintNo.Value.ToString(), 0);
        gvDecide.DataBind();
    }
    private DataTable selectDecideApproach(string GuestCompNo, int isHavePrice)
    {
        string str = "sp_comp_selectDecidedApproach";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@GuestCompNo", GuestCompNo);
        param[1] = new SqlParameter("@isHavePrice", isHavePrice);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void gvFinance_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvFinance.EditIndex = -1;
        BindData();
    }
    //绑定销售部选中的赔偿方式
    private void BindData()
    {
        gvFinance.DataSource = SelectCheckedApproach(hidGuestComplaintNo.Value.ToString());
        gvFinance.DataBind();
    }
    private object SelectCheckedApproach(string GuestCompNo)
    {
        string str = "sp_comp_selectDecidingApproach";
        SqlParameter param = new SqlParameter("@GuestCompNo", GuestCompNo);
        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param);
    }
    protected void gvFinance_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string preResultLoginNames = getResultLoginNames(preResult.Text);
        string checkedModuleName = "financeAproach";
        if (checkISAuthority(preResultLoginNames, Session["uID"].ToString(), checkedModuleName) && Session["uRole"].ToString() != "1")
        {
            this.Alert("您没有处理此模块的权限");
            return;
        }
        else
        {
            gvFinance.EditIndex = e.NewEditIndex;
            BindData();
            BindCheckedPartyInfo();
        }
    }
    protected void gvFinance_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = gvFinance.DataKeys[e.RowIndex].Values["ID"].ToString();
        TextBox txtPrice = (TextBox)gvFinance.Rows[e.RowIndex].Cells[1].Controls[0];

        try
        {
            string strSql = "sp_comp_updateDecidingApproach";
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@id", id);
            sqlParam[1] = new SqlParameter("@price", txtPrice.Text.Trim().ToString());
            sqlParam[2] = new SqlParameter("@uID", Session["uID"]);
            sqlParam[3] = new SqlParameter("@uName", Session["uName"]);

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        {
            throw new Exception("DB connection error when updating...");
        }
        gvFinance.EditIndex = -1;
        BindData();
        BindDecideApproach();
        BindCheckedPartyInfo();
    }
    protected void gvFinance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }
    protected void btnApproachAgree_Click(object sender, EventArgs e)
    {
        string preResultLoginNames = getResultLoginNames(preResult.Text);
        string checkedModuleName = "decideAproach";
        string checkedDutys = getCheckedItem();
        if (checkISAuthority(preResultLoginNames, Session["uID"].ToString(), checkedModuleName) && Session["uRole"].ToString() != "1")
        {
            this.Alert("您没有处理此模块的权限");
            return;
        }
        else
        {
            int num = 0;
            string[] ID = new string[100];
            foreach (GridViewRow row in gvDecide.Rows)
            {
                if (((CheckBox)row.FindControl("chk")).Checked)
                {

                    string id = gvDecide.DataKeys[row.RowIndex]["ID"].ToString();
                    ID[num] = id;
                    num += 1;
                }
            }
            if (num == 1 && num != 0)
            {
                string str = "sp_comp_DecideApproach";

                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@id", ID[0]);
                param[1] = new SqlParameter("@isAgree", "同意");
                param[2] = new SqlParameter("@uid", Session["uID"].ToString());
                param[3] = new SqlParameter("@uname", Session["uName"].ToString());

                SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, str, param);
                btnApproachAgree.Text = "Agree By" + Session["uName"].ToString();
                btnApproachDisagree.Enabled = false;
                btnApproachAgree.Enabled = false;
                //将checkbox隐藏
                BindAfterCheckedApproach();
                gvDecide.Columns[0].Visible = false;
                Semail(hidGuestComplaintNo.Value.ToString(), checkedModuleName,"managerApr","");
               // Bind();
            }
            else
            {
                if (num == 0)
                    ltlAlert.Text = "alert('请选择一个解决方式')";
                else
                    ltlAlert.Text = "alert('只能选择一个解决方式')";
                return;
            }
        }
    }
    protected void gvDecide_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }
    protected void feedbackAgree_Click(object sender, EventArgs e)
    {
        string feedbackLoginNames = getResultLoginNames(feedback.Text);
        string checkedModuleName = "feedback";
        string checkedDutys = getCheckedItem();

        if (checkISAuthority(feedbackLoginNames, Session["uID"].ToString(), checkedModuleName) && Session["uRole"].ToString() != "1")
        {
            this.Alert("您没有处理此模块的权限");
            return;
        }
        else
        {
            if (string.IsNullOrEmpty(txtNo.Text))
            {
                ltlAlert.Text = "alert('请填写计划出运单号')";
                return;
            }
            if (string.IsNullOrEmpty(txtPartNo.Text))
            {
                ltlAlert.Text = "alert('请填写物料号')";
                return;
            }
            if (string.IsNullOrEmpty(txtNum.Text))
            {
                ltlAlert.Text = "alert('请填写出运数量')";
                return;
            }
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                ltlAlert.Text = "alert('请填写出运日期')";
                return;
            }
            string strName = "sp_comp_approvefeedback";
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@no", hidGuestComplaintNo.Value.ToString());
            param[1] = new SqlParameter("@isAgree", "同意");
            param[2] = new SqlParameter("@shipNo", txtNo.Text.ToString());
            param[3] = new SqlParameter("@shipPartNo", txtPartNo.Text.ToString());
            param[4] = new SqlParameter("@shipNum", txtNum.Text.ToString());
            param[5] = new SqlParameter("@shipDate", txtDate.Text.ToString());
            param[6] = new SqlParameter("@uid", Session["uID"].ToString());
            param[7] = new SqlParameter("@uname", Session["uName"].ToString());
            param[8] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[8].Direction = System.Data.ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[8].Value))
            {
                this.Alert("操作失败，请联系管理员");
            }
            else
            {
                feedbackAgree.Text = "Agree By " + Session["uName"].ToString();
                feedbackDisagree.Enabled = false;
                feedbackAgree.Enabled = false;
                txtNo.Enabled = false;
                txtPartNo.Enabled = false;
                txtDate.Enabled = false;
                txtNum.Enabled = false;
                Semail(hidGuestComplaintNo.Value.ToString(),checkedModuleName,"executor","");
            }
        }
    }
    protected void btnFinishOrder_Click(object sender, EventArgs e)
    {
        string executorLoginNames = getResultLoginNames(executor.Text);
        string checkedModuleName = "executor";
        string checkedDutys = getCheckedItem();

        if (checkISAuthority(executorLoginNames, Session["uID"].ToString(), checkedModuleName) && Session["uRole"].ToString() != "1")
        {
            this.Alert("您没有处理此模块的权限");
            return;
        }
        else
        {
            if (string.IsNullOrEmpty(txtActualPrice.Text))
            {
                ltlAlert.Text = "alert('请填写实际金额')";
                return;
            }
            if (string.IsNullOrEmpty(txtFinishedDate.Text))
            {
                ltlAlert.Text = "alert('请填写结单时间')";
                return;
            }
            string strName = "sp_comp_approveExecutor";
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@no", hidGuestComplaintNo.Value.ToString());
            param[1] = new SqlParameter("@isAgree", "结单");
            param[2] = new SqlParameter("@actualPrice", txtActualPrice.Text.ToString());
            param[3] = new SqlParameter("@finishedDate", txtFinishedDate.Text.ToString());
            param[4] = new SqlParameter("@uid", Session["uID"].ToString());
            param[5] = new SqlParameter("@uname", Session["uName"].ToString());
            param[6] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[6].Direction = System.Data.ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[6].Value))
            {
                this.Alert("操作失败，请联系管理员");
            }
            else
            {
                btnFinishOrder.Text = "Close By " + Session["uName"].ToString();
                btnFinishOrder.Enabled = false;
                txtActualPrice.Enabled = false;
            }
        }
    }
    protected void btnFinDisagree_Click(object sender, EventArgs e)
    {
        string preResultLoginNames = getResultLoginNames(preResult.Text);
        string checkedModuleName = "financeAproach";
        if (checkISAuthority(preResultLoginNames, Session["uID"].ToString(), checkedModuleName))
        {
            this.Alert("您没有处理此模块的权限");
            return;
        }
        else
        {
            if (!checkISAllPartyFinished(hidGuestComplaintNo.Value.ToString()))
            {
                ltlAlert.Text = "alert('责任方还未完成')";
                return;
            }
            string strName = "sp_comp_approveFinResult";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@no", hidGuestComplaintNo.Value.ToString());
            param[1] = new SqlParameter("@isAgree", "不同意");
            param[2] = new SqlParameter("@uid", Session["uID"].ToString());
            param[3] = new SqlParameter("@uname", Session["uName"].ToString());
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = System.Data.ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[4].Value))
            {
                this.Alert("操作失败，请联系管理员");
            }
            else
            {
                btnFinDisagree.Text = "Disagree By " + Session["uName"].ToString();
                btnFinAgree.Enabled = false;
                btnFinDisagree.Enabled = false;
                BindDecideApproach();
            }
        }
    }
    protected void btnApproachDisagree_Click(object sender, EventArgs e)
    {
        string preResultLoginNames = getResultLoginNames(preResult.Text);
        string checkedModuleName = "decideAproach";
        string checkedDutys = getCheckedItem();
        if (checkISAuthority(preResultLoginNames, Session["uID"].ToString(), checkedModuleName))
        {
            this.Alert("您没有处理此模块的权限");
            return;
        }
        else
        {
            int num = 0;
            string[] ID = new string[100];
            foreach (GridViewRow row in gvDecide.Rows)
            {
                if (((CheckBox)row.FindControl("chk")).Checked)
                {

                    string id = gvDecide.DataKeys[row.RowIndex]["ID"].ToString();
                    ID[num] = id;
                    num += 1;
                }
            }
            if (num == 1 && num != 0)
            {
                string str = "sp_comp_DecideApproach";

                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@id", "");
                param[1] = new SqlParameter("@isAgree", "不同意");
                param[2] = new SqlParameter("@uid", Session["uID"].ToString());
                param[3] = new SqlParameter("@uname", Session["uName"].ToString());

                SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, str, param);
                btnApproachAgree.Text = "Disgree By" + Session["uName"].ToString();
                btnApproachDisagree.Enabled = false;
                btnApproachAgree.Enabled = false;
                BindDecideApproach();
            }
            else
            {
                if (num == 0)
                    ltlAlert.Text = "alert('请选择一个解决方式')";
                else
                    ltlAlert.Text = "alert('只能选择一个解决方式')";
                return;
            }
        }
    }
    protected void managerAgree_Click(object sender, EventArgs e)
    {
        string preResultLoginNames = getResultLoginNames(preResult.Text);
        string checkedModuleName = "managerApr";
        string checkedDutys = getCheckedItem();
        if (checkISAuthority(preResultLoginNames, Session["uID"].ToString(), checkedModuleName) && Session["uRole"].ToString() != "1")
        {
            this.Alert("您没有处理此模块的权限");
            return;
        }
        else
        {
            string strName = "sp_comp_approveManResult";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@no", hidGuestComplaintNo.Value.ToString());
            param[1] = new SqlParameter("@isAgree", "同意");
            param[2] = new SqlParameter("@uid", Session["uID"].ToString());
            param[3] = new SqlParameter("@uname", Session["uName"].ToString());
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = System.Data.ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[4].Value))
            {
                this.Alert("操作未成功，请联系管理员");
            }
            else
            {
                managerAgree.Text = "Agree By " + Session["uName"].ToString();
                managerDisagree.Enabled = false;
                managerAgree.Enabled = false;
                BindDisposed();
                Semail(hidGuestComplaintNo.Value.ToString(),checkedModuleName, "feedback","");
            }
        }
    }
    protected void managerDisagree_Click(object sender, EventArgs e)
    {
        string preResultLoginNames = getResultLoginNames(preResult.Text);
        string checkedModuleName = "managerApr";
        string checkedDutys = getCheckedItem();
        if (checkISAuthority(preResultLoginNames, Session["uID"].ToString(), checkedModuleName))
        {
            this.Alert("您没有处理此模块的权限");
            return;
        }
        else
        {
            string strName = "sp_comp_approveManResult";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@no", hidGuestComplaintNo.Value.ToString());
            param[1] = new SqlParameter("@isAgree", "不同意");
            param[2] = new SqlParameter("@uid", Session["uID"].ToString());
            param[3] = new SqlParameter("@uname", Session["uName"].ToString());
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = System.Data.ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[4].Value))
            {
                this.Alert("操作未成功，请联系管理员");
            }
            else
            {
                managerDisagree.Text = "Disagree By " + Session["uName"].ToString();
                managerAgree.Enabled = false;
                managerDisagree.Enabled = false;
                BindDisposed();
            }
        }
    }
    protected void feedbackDisagree_Click(object sender, EventArgs e)
    {
        string feedbackLoginNames = getResultLoginNames(feedback.Text);
        string checkedModuleName = "feedback";
        string checkedDutys = getCheckedItem();
        if (checkISAuthority(feedbackLoginNames, Session["uID"].ToString(), checkedModuleName))
        {
            this.Alert("您没有处理此模块的权限");
            return;
        }
        else
        {
            string strName = "sp_comp_approvefeedback";
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@no", hidGuestComplaintNo.Value.ToString());
            param[1] = new SqlParameter("@isAgree", "不同意");
            param[2] = new SqlParameter("@shipNo", "");
            param[3] = new SqlParameter("@shipPartNo", "");
            param[4] = new SqlParameter("@shipNum", "");
            param[5] = new SqlParameter("@shipDate", "");
            param[6] = new SqlParameter("@uid", Session["uID"].ToString());
            param[7] = new SqlParameter("@uname", Session["uName"].ToString());
            param[8] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[8].Direction = System.Data.ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[8].Value))
            {
                this.Alert("操作未成功，请联系管理员");
            }
            else
            {
                feedbackDisagree.Text = "Disagree By " + Session["uName"].ToString();
                feedbackAgree.Enabled = false;
                feedbackDisagree.Enabled = false;
            }
        }
    }
    protected void gvFile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = gvFile.DataKeys[intRow].Values["GuestComp_FilePath"].ToString().Trim();
            string fileName = gvFile.DataKeys[intRow].Values["GuestComp_FileName"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
    }

    private void Semail(string _no,string _moduleFrom,string _moduleTo,string remark)
    {
        DataTable dt = GetEmail(_no, _moduleFrom);
        string mailto = dt.Rows[0]["email"].ToString();
        StringBuilder sb = new StringBuilder();
        string mailSubject = "强凌 - " + dt.Rows[0]["GuestComplaintNo"].ToString() + "-客诉审批通知";
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<html>");
                sb.Append("<form>");
                sb.Append("<br>");
                sb.Append("ALL," + "<br>");
                sb.Append("    下列客诉单" + dt.Rows[0]["moduleName"].ToString() + "已审批! 客诉详细信息如下。" + "<br>");
                sb.Append("<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "客诉单号：" + _no + " ，" + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "内容描述：" + dt.Rows[0]["ProblemContent"].ToString() + " ，" + "<br>");
                sb.Append("请尽快进行审批，谢谢" + "<br>");
                sb.Append("详情请登录100系统客诉模块查看，谢谢" + "<br>");
                sb.Append("</body>");
                sb.Append("</form>");
                sb.Append("</html>");
            }

        }

        DataTable d = getHaveAuthority(_moduleTo, remark);
        if (d.Rows.Count > 0)
        {
            for (int i = 0; i < d.Rows.Count; i++)
            {
                if (!this.SendEmail(mailto, d.Rows[i]["email"].ToString(), "", mailSubject, sb.ToString()))
                {
                    this.Alert("邮件发送失败！");
                    return;
                }
            }
        }
    }

    private DataTable getHaveAuthority(string moduleName,string remark)
    {
        string str = "sp_comp_selectModuleEmail";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@moduleName", moduleName);
        param[1] = new SqlParameter("@remark", remark);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }

    private DataTable GetEmail(string No,string moduleName)
    {
        string str = "sp_comp_selectComplaintInfo";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@compNo", No);
        param[1] = new SqlParameter("@uid", Session["uID"]);
        param[2] = new SqlParameter("@moduleName", moduleName);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }
}
