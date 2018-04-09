using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using IT;

public partial class IT_TSK_DistributeTask : BasePage
{
    public bool IsDistribute
    {
        get
        {
            return Convert.ToBoolean(ViewState["isDistribute"]);
        }
        set
        {
            ViewState["isDistribute"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindHead();
            BindData(); 
        }
    }

    protected void BindHead()
    {
        DataTable table = TaskHelper.SelectTaskMstrByNbr(Request.QueryString["tskNbr"]);

        if (table != null && table.Rows.Count > 0)
        {
            ddlSystem.SelectedIndex = -1;
            try
            {
                ddlSystem.Items.FindByValue(table.Rows[0]["tsk_sys"].ToString()).Selected = true;
            }
            catch { }

            BindModules();
            BindTrackerAndTester();

            chkApplyEmailed.Checked = Convert.ToBoolean(table.Rows[0]["tsk_applyMailed"]);
            hidApplyDesc.Value = table.Rows[0]["tsk_desc"].ToString();
            hidApplyEmail.Value = table.Rows[0]["tsk_applyEmail"].ToString();
            IsDistribute = Convert.ToBoolean(table.Rows[0]["tsk_isDistributeTemp"]);
            linkAdd.Enabled = IsDistribute;

            if (!IsDistribute) 
            {
                linkAdd.ToolTip = "保存成功后，方可分解任务！";
            }

            ddlModule.SelectedIndex = -1;
            try
            {
                ddlModule.Items.FindByValue(table.Rows[0]["tsk_model"].ToString()).Selected = true;
            }
            catch { }

            radlType.SelectedIndex = -1;
            try
            {
                radlType.Items.FindByValue(table.Rows[0]["tsk_type"].ToString()).Selected = true;
            }
            catch { }

            radlDegree.SelectedIndex = -1;
            try
            {
                radlDegree.Items.FindByValue(table.Rows[0]["tsk_degree"].ToString()).Selected = true;
            }
            catch { }

            txtExtreDesc.Text = table.Rows[0]["tsk_extreDesc"].ToString();

            try
            {
                //跟踪人员只有任务创建人（或管理员）才可以修改，且一旦任务分配完成，则无法修改
                dropTracker.Items.FindByValue(table.Rows[0]["tsk_trackBy"].ToString()).Selected = true;
                if (Convert.ToInt32(Session["uID"]) != Convert.ToInt32(table.Rows[0]["tsk_createBy"])
                    && Convert.ToInt32(Session["uRole"]) != 1)
                {
                    dropTracker.Enabled = false;
                    dropTracker.ToolTip = "只有任务创建人（或管理员）才可以修改，且一旦任务分配完成，则无法修改";
                }
                //由于更新人只有一人，目前不开放
                //dropUpdater.SelectedIndex = -1;
                //dropUpdater.Items.FindByValue(table.Rows[0]["tsk_updateBy"].ToString()).Selected = true;

                dropTester1.Items.FindByValue(table.Rows[0]["tsk_testBy"].ToString()).Selected = true;
                dropTester2.Items.FindByValue(table.Rows[0]["tsk_testSecondBy"].ToString()).Selected = true;
            }
            catch { }

            //如果任务已经完成，则无法添加明细，修改保存
            if (Convert.ToBoolean(table.Rows[0]["tsk_isComplete"]))
            {
                txtDone.Enabled = false;
                linkAdd.Enabled = false;
                linkAdd.Style.Add("font-size", "12px");
                linkAdd.Style.Add("font-weight", "normal");
                linkAdd.Style.Add("color", "silver");
                txtDone.ToolTip = "任务已经完成";
                linkAdd.ToolTip = "任务已经完成";
            }
        }
        else
        {
            txtDone.Enabled = false;
            this.Alert("任务获取失败！请返回！");
        }
    }

    protected void BindTrackerAndTester()
    {
        DataTable table = TaskHelper.GetUsers(string.Empty, 404);
        dropTracker.Items.Clear();
        dropTester1.Items.Clear();
        dropTester2.Items.Clear();
        dropUpdater.Items.Clear();

        dropTracker.DataSource = table;
        dropTracker.DataBind();
        dropTracker.Items.Insert(0, new ListItem("请选择跟踪人", "0"));

        dropUpdater.DataSource = table;
        dropUpdater.DataBind();
        dropUpdater.Items.Insert(0, new ListItem("请选择跟踪人", "0"));
        //默认选56110
        dropUpdater.Items.FindByValue("92072").Selected = true;
        dropUpdater.Enabled = false;

        dropTester1.DataSource = table;
        dropTester1.DataBind();
        dropTester1.Items.Insert(0, new ListItem("请选择第一测试员", "0"));

        dropTester2.DataSource = table;
        dropTester2.DataBind();
        dropTester2.Items.Insert(0, new ListItem("请选择第二测试员", "0"));
    }

    protected void BindModules()
    {
        ddlModule.Items.Clear();
        if (ddlSystem.SelectedIndex != 0)
        {
            ListItem item = new ListItem();
            DataTable dtModule = TaskHelper.GetModule(ddlSystem.SelectedValue);
            if (dtModule.Rows.Count > 0)
            {
                for (int i = 0; i < dtModule.Rows.Count; i++)
                {
                    item = new ListItem(dtModule.Rows[i].ItemArray[1].ToString(), dtModule.Rows[i].ItemArray[0].ToString());
                    ddlModule.Items.Add(item);
                }
            }
        }

        ddlModule.Items.Insert(0, new ListItem("--请选择一个模块--", "0"));
        ddlModule.SelectedIndex = 0;
    }

    protected void BindData()
    {
        gv.DataSource = TaskHelper.SelectTaskDet(Request.QueryString["tskNbr"]);
        txtExtreDesc.Text += "DataSource;";
        gv.DataBind();
        txtExtreDesc.Text += gv.Columns.Count + ";";
        txtExtreDesc.Text += gv.Rows.Count;
    }

    protected void ddlSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindModules();
    }

    protected void btnSaveLine_Click(object sender, EventArgs e)
    {
        
    }

    protected void txtDone_Click(object sender, EventArgs e)
    {
        bool isDistribute = IsDistribute;
       
        if (dropTester1.SelectedIndex == 0 && dropTester2.SelectedIndex == 0)
        { 
        }
        else if (dropTester1.SelectedIndex == 0 || dropTester2.SelectedIndex == 0 )
        {
            this.Alert("两个测试人员必须同时选择！");
            return;
        }
        else if (dropTester1.SelectedIndex == dropTester2.SelectedIndex)
        {
            this.Alert("测试人员不能相同");
            return;
        }

        if (isDistribute == true && dropTester1.SelectedIndex == 0 && dropTester2.SelectedIndex == 0)
        {
            this.Alert("已分配任务的测试员不能为空！");
            return;
        }
        else if (ddlSystem.SelectedIndex != 0 && ddlModule.SelectedIndex != 0 && radlType.SelectedIndex != -1 && dropTracker.SelectedIndex != 0)
        {
            isDistribute = true;
            linkAdd.Enabled = isDistribute;
           
        }
        else if (isDistribute == true)
        {
            this.Alert("已分配的任务改选项不可为空");
            return;
        }

        string _trackName = dropTracker.SelectedItem.Text.IndexOf("--") < 0 ? dropTracker.SelectedItem.Text : dropTracker.SelectedItem.Text.Substring(0, dropTracker.SelectedItem.Text.IndexOf("--"));
        string _trackEmail = dropTracker.SelectedItem.Text.IndexOf("--") < 0 ? string.Empty : dropTracker.SelectedItem.Text.Substring(dropTracker.SelectedItem.Text.IndexOf("--") + 2);

        string _updateName = dropUpdater.SelectedItem.Text.IndexOf("--") < 0 ? dropUpdater.SelectedItem.Text : dropUpdater.SelectedItem.Text.Substring(0, dropUpdater.SelectedItem.Text.IndexOf("--"));
        string _updateEmail = dropUpdater.SelectedItem.Text.IndexOf("--") < 0 ? string.Empty : dropUpdater.SelectedItem.Text.Substring(dropUpdater.SelectedItem.Text.IndexOf("--") + 2);

        string _testName = dropTester1.SelectedItem.Text.IndexOf("--") < 0 ? dropTester1.SelectedItem.Text : dropTester1.SelectedItem.Text.Substring(0, dropTester1.SelectedItem.Text.IndexOf("--"));
        string _testEmail = dropTester1.SelectedItem.Text.IndexOf("--") < 0 ? string.Empty : dropTester1.SelectedItem.Text.Substring(dropTester1.SelectedItem.Text.IndexOf("--") + 2);

        string _testSecondName = dropTester2.SelectedItem.Text.IndexOf("--") < 0 ? dropTester2.SelectedItem.Text : dropTester2.SelectedItem.Text.Substring(0, dropTester2.SelectedItem.Text.IndexOf("--"));
        string _testSecondEmail = dropTester2.SelectedItem.Text.IndexOf("--") < 0 ? string.Empty : dropTester2.SelectedItem.Text.Substring(dropTester2.SelectedItem.Text.IndexOf("--") + 2);

        #region 验证各相关负责人的邮箱不能为空
        if (string.IsNullOrEmpty(_trackEmail))
        {
            this.Alert("请先维护 跟踪人 的邮箱！");
            return;
        }

        if (string.IsNullOrEmpty(_updateEmail))
        {
            this.Alert("请先维护 更新人 的邮箱！");
            return;
        }

        if (string.IsNullOrEmpty(_testEmail))
        {
            this.Alert("请先维护 第一测试员 的邮箱！");
            return;
        }

        if (string.IsNullOrEmpty(_testSecondEmail))
        {
            this.Alert("请先维护 第二测试员 的邮箱！");
            return;
        }
        #endregion

        if (!TaskHelper.UpdateTaskMstr(Request.QueryString["tskNbr"], ddlSystem.SelectedValue
                        , ddlModule.SelectedValue, radlType.SelectedValue, radlDegree.SelectedValue, txtExtreDesc.Text.Trim()
                        , dropTracker.SelectedValue, _trackName, _trackEmail, dropTester1.SelectedValue, _testName, _testEmail
                        , dropTester2.SelectedValue, _testSecondName, _testSecondEmail, dropUpdater.SelectedValue, _updateName, _updateEmail
                        , Session["uID"].ToString(), Session["uName"].ToString(), isDistribute))
        {
            this.Alert("任务保存失败！");
            return;
        }
        else
        {
            //如果没有发送邮件，则要发送
            if (!chkApplyEmailed.Checked)
            {
                #region 发送邮件
                string _from = "isHelp@" + baseDomain.Domain[0];
                string _to = hidApplyEmail.Value;
                string _copy = "";
                //抄送给跟踪人，但，此时测试员不需要发送邮件
                if (!string.IsNullOrEmpty(_trackEmail))
                {
                    _copy = _trackEmail;
                    //如果跟踪和申请是同一个人的话，则无需发送两次
                    if (_copy == _to)
                    {
                        _copy = string.Empty;
                    }
                }

                string _subject = "技术支持答复";
                string _body = "<font style='font-size: 12px;'>你好：<br />";
                _body += "&nbsp;&nbsp;&nbsp;&nbsp;你提出的问题（如下），我们已经开始安排处理！请等待我们的处理结果（任务号:" + Request.QueryString["tskNbr"] + "）：</font><br />";
                _body += "------------------------<br />";
                _body += "<font style='font-size: 12px;'>";
                _body += hidApplyDesc.Value;
                _body += "</font><br />";
                _body += "------------------------<br />";
                _body += "<br /><font style='font-size: 12px;'>信息部 " + string.Format("{0:yyyy-MM-dd HH:mm}", DateTime.Now) + "</font>";

                if (!string.IsNullOrEmpty(_to))
                {
                    if (this.SendEmail(_from, _to, _copy, _subject, _body))
                    {
                        //发送邮件成功后，保存发送标识，不论成功与否
                        TaskHelper.SetTaskDistributeEmialed(Request.QueryString["tskNbr"]);
                        chkApplyEmailed.Checked = true;

                        this.Alert("任务保存成功！且,邮件发送成功！");
                    }
                    else
                    {
                        this.Alert("任务保存成功！但，邮件发送失败！");
                    }
                }
                else
                {
                    this.Alert("任务保存成功！但，申请人没有维护邮箱！");
                }
                #endregion
            }
            else
            {
                this.Alert("任务保存成功！");
            }
           
        }
       
    }

    protected void txtBack_Click(object sender, EventArgs e)
    {
        if (Request["from"] == "new")
        {
            //新建界面
            Response.Redirect("TSK_NewTask.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
        }
        else if (Request["from"] == "dis")
        {
            //分配界面
            this.Redirect("TSK_DistributeList.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }

    protected void linkAdd_Click(object sender, EventArgs e)
    {
       //ltlAlert.Text = " if(navigator.userAgent.indexOf(\"Chrome\") >0 ){window.open('TSK_TaskDetail.aspx?from=" + Request["from"] + "&tskNbr=" + Request.QueryString["tskNbr"] + "&rt=" + DateTime.Now.ToString() + "', window, 'dialogHeight: 350px; dialogWidth: 500px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');}"
       // ltlAlert.Text += "else {window.showModalDialog('TSK_TaskDetail.aspx?from=" + Request["from"] + "&tskNbr=" + Request.QueryString["tskNbr"] + "&rt=" + DateTime.Now.ToString() + "', window, 'dialogHeight: 350px; dialogWidth: 500px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');}"
        //ltlAlert.Text = "window.showModalDialog('TSK_TaskDetail.aspx?from=" + Request["from"] + "&tskNbr=" + Request.QueryString["tskNbr"] + "&rt=" + DateTime.Now.ToString() + "', window, 'dialogHeight: 350px; dialogWidth: 500px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
        ltlAlert.Text = "window.open('TSK_TaskDetail.aspx?from=" + Request["from"] + "&tskNbr=" + Request.QueryString["tskNbr"] + "&rt=" + DateTime.Now.ToString() + "', window, 'dialogHeight: 350px; dialogWidth: 500px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
        ltlAlert.Text += "window.location.href = 'TSK_DistributeTask.aspx?from=" + Request.QueryString["from"] + "&tskNbr=" + Request.QueryString["tskNbr"] + "&rt=" + DateTime.Now.ToFileTime().ToString() + "'";
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myDelete")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _mstrNbr = gv.DataKeys[index].Values["tskd_mstrNbr"].ToString();
            string _detID = gv.DataKeys[index].Values["tskd_id"].ToString();
            int _process = Convert.ToInt32(gv.DataKeys[index].Values["tskd_process"]);
            string _uID = Session["uID"].ToString();
            string _uName = Session["uName"].ToString();

            if (TaskHelper.DeleteTaskDet(_mstrNbr, _detID, _process, _uID, _uName))
            {
                BindData();
            }
            else
            {
                ltlAlert.Text = "alert('操作失败！')";
            }
        }
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            LinkButton linkDelete = (LinkButton)e.Row.FindControl("linkDelete");
            TableRow row = e.Row;
            txtExtreDesc.Text += "RowDataBound " + row.Cells[1].Text.ToString() + ";";
            //取消的任务，显示“已取消”
            if (Convert.ToBoolean(rowView["tskd_isCanceled"]))
            {
                linkDelete.Enabled = false;
                linkDelete.Text = "x";
                linkDelete.Font.Underline = false;
                linkDelete.ForeColor = System.Drawing.Color.Black;
                linkDelete.ToolTip = "已取消";
            }
            else if (Convert.ToBoolean(rowView["tskd_isCompleted"]))
            {
                linkDelete.Enabled = false;
                linkDelete.Text = "c";
                linkDelete.Font.Underline = false;
                linkDelete.ForeColor = System.Drawing.Color.Black;
                linkDelete.ToolTip = "已完成";
            }
            else
            {
                //如果任务明细已经开始，则无法删除
                if (Convert.ToInt32(rowView["tskd_process"]) > 0
                    || (rowView["tskd_type"].ToString() == "测试" && !string.IsNullOrEmpty(rowView["tskd_testingDate"].ToString())))
                {
                    //当任务进行时，不能删除，只能取消
                    linkDelete.Text = "<u>取消</u>";
                }
            }
        }
    }
    protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableRow row = e.Row;
            txtExtreDesc.Text += "RowCreated " + row.Cells[1].Text.ToString() + ";";
            row.Cells[1].Text = "123";
        }
    }
    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {
        txtExtreDesc.Text += "Sorting;";
    }
    protected void gv_Unload(object sender, EventArgs e)
    {
        txtExtreDesc.Text += "Unload;";
    }
    protected void gv_DataBinding(object sender, EventArgs e)
    {
        
        txtExtreDesc.Text += "DataBinding;";
    }
    protected void gv_DataBound(object sender, EventArgs e)
    {
        txtExtreDesc.Text += "DataBound;";
    }
    protected void gv_Disposed(object sender, EventArgs e)
    {
        txtExtreDesc.Text += "Disposed;";
    }
    protected void gv_Init(object sender, EventArgs e)
    {
        txtExtreDesc.Text += "Init;";
    }
    protected void gv_Load(object sender, EventArgs e)
    {
        txtExtreDesc.Text += "Load;";
       
        try
        {
            GridViewRow gvr = gv.HeaderRow;
            //int count = gvr.Cells.Count;
            //txtExtreDesc.Text += count + ";";
        }
        catch (Exception)
        {
            
            
        }
        
    }
    protected void gv_PreRender(object sender, EventArgs e)
    {
        txtExtreDesc.Text += "PreRender;";
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        txtExtreDesc.Text += "PageIndexChanging;";
    }
}