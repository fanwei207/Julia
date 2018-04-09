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
using Microsoft.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

public partial class WF_WorkFlowTemplateEdit : BasePage
{
    
    WorkFlow wf = new WorkFlow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tblReview.Visible = false;
            string type = Request.QueryString["tp"].ToString();
            if (type != "1")
            {
                txtReqDate.Enabled = false;
                txtDueDate.Enabled = false;
                txtRemark.Enabled = false;
                fileAttach.Disabled = true; 
                btn_upload.Enabled = false; 
                btn_save.Enabled = false;
            }
            else
            {
                if (wf.JudgeIsCanModifiedWithApplier(Request.QueryString["nbr"].ToString(), Convert.ToInt32(Session["uID"]))) 
                {
                    txtReqDate.Enabled = true;
                    txtDueDate.Enabled = true;
                    txtRemark.Enabled = true;
                    fileAttach.Disabled = false; 
                    btn_upload.Enabled = true; 
                    btn_save.Enabled = true;
                }
                else
                {
                    txtReqDate.Enabled = false;
                    txtDueDate.Enabled = false;
                    txtRemark.Enabled = false;
                    fileAttach.Disabled = true; 
                    btn_upload.Enabled = false; 
                    btn_save.Enabled = false;
                }
            }
            dataBind();
            attachBind();
        }
    }

    protected void dataBind()
    {
        string wfn_nbr = Request.QueryString["nbr"].ToString();
        int plantCode = Convert.ToInt32(Session["PlantCode"]);
        int uID = Convert.ToInt32(Session["uID"]);

        int flowSetup = 0;
        DataTable dt = wf.GetFlowNodeInfo(wfn_nbr);
        flowSetup = dt.Rows.Count;

        TabStrip1.Items.Clear();
        for (int i = 0; i < flowSetup; i++)
        {
            TabStrip1.Items.Add(new Tab());
            TabStrip1.Items[i].Text = dt.Rows[i]["Node_Name"].ToString();
        }

        TabStrip1.SelectedIndex = 0;

        //TabStrip1中未操作的需要冻结
        int LastSort = 0;
        SqlDataReader reader3 = wf.GetFlowNodeInstanceLastSort(wfn_nbr);
        if (reader3.Read())
        {
            LastSort = Convert.ToInt32(reader3["Sort_Order"]);
        }
        reader3.Close();

        int ALastSort = 0;
        SqlDataReader reader4 = wf.GetFlowNodeInstanceApprovedLastSort(wfn_nbr);
        if (reader4.Read())
        {
            ALastSort = Convert.ToInt32(reader4["Sort_Order"]);
        }
        reader4.Close();

        for (int i = ALastSort / 10; i < LastSort / 10; i++)
        {
            TabStrip1.Items[i].DefaultStyle.Add("background-image", "url(../images/button17.jpg)");
            TabStrip1.Items[i].Enabled = false;
        }

        //工作流实例信息绑定
        SqlDataReader reader1 = wf.GetWorkFlowInstanceByNbr(wfn_nbr, plantCode);
        if (reader1.Read())
        {
            txtReqNbr.Text = wfn_nbr;
            txtWorkFlowPre.Text = Convert.ToString(reader1["Flow_Req_Pre"]);
            hlForm.Text = Convert.ToString(reader1["WFN_FormName"]);
            hlForm.NavigateUrl = "WF_FormEdit.aspx?nbr=" + wfn_nbr + "&rm=" + DateTime.Now;
            txtReqDate.Text = string.Format("{0:yyyy-MM-dd}", reader1["WFN_ReqDate"]);
            txtDueDate.Text = string.Format("{0:yyyy-MM-dd}", reader1["WFN_DueDate"]);
        }
        reader1.Close();

        SqlDataReader reader2 = wf.GetFlowNodeInstanceCreatedBySelf(wfn_nbr, uID);
        if (reader2.Read())
        {
            txtSortOrder.Text = Convert.ToString(reader2["Sort_Order"]);
        }
        else
        {
            txtSortOrder.Text = "10";
        }

        reader2.Close();

        SqlDataReader reader6 = wf.GetFNIInfoByNbrAndSort(wfn_nbr, Convert.ToInt32(txtSortOrder.Text.Trim() == "" ? "0" : txtSortOrder.Text.Trim()));
        if (reader6.Read())
        {
            txtRemark.Text = Convert.ToString(reader6["FNI_Remark"]);
            txtCreatedBy.Text = Convert.ToString(reader6["userName"]);
            txtCreatedByRole.Text = Convert.ToString(reader6["roleName"]);
            txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader6["FNI_RunDate"]);
        }
        reader6.Close();
        //如果是发起的步骤，则按钮显示提交----王大龙
        if (txtSortOrder.Text == "10")
        {
            btn_save.Text = "提交";
        }
        else
        {
            btn_save.Text = "签署";
        }

        //绑定节点步骤描述
        SqlDataReader reader7 = wf.GetNodeSortDesc(wfn_nbr, txtSortOrder.Text.Trim());
        if (reader7.Read())
        {
            txtNoteDesc.Text = reader7["NodeDesc"].ToString();
        }
        reader7.Close();
    }

    
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        if (fileAttach.Value.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择需要上传的附件!');";
            return;
        }

        string attachName = Path.GetFileName(fileAttach.PostedFile.FileName);
        Stream attachStream = fileAttach.PostedFile.InputStream;
        int attachLength = fileAttach.PostedFile.ContentLength;
        string attachType = fileAttach.PostedFile.ContentType;
        byte[] attachByte = new byte[attachLength];
        attachStream.Read(attachByte, 0, attachLength);

        if (!wf.UploadFniAttachment(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()),attachName,attachType,attachByte,Convert.ToInt32(Session["uID"])))
        {
            ltlAlert.Text = "alert('附件上传失败,请联系管理员!');";
            return;
        }
        attachBind();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (txtReqDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('<申请日期>不能为空！');";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtReqDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('申请日期格式不正确！正确格式是:YYYY-MM-DD');";
                return;
            }
        }

        if (txtDueDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('<截止日期>不能为空！');";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtDueDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('截止日期格式不正确！正确格式是:YYYY-MM-DD');";
                return;
            }
        }

        if (wf.ActiveWorkFlowInstance(txtReqNbr.Text.Trim(), txtReqDate.Text.Trim(), txtDueDate.Text.Trim()))
        {
            if (wf.UpdateFlowNodeInstance(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()), txtRemark.Text.Trim(), 1, Convert.ToInt32(Session["uID"]), hidSql.Value))
            {
                ltlAlert.Text = "alert('保存成功!');";
            }
            else
            {
                ltlAlert.Text = "alert('保存失败，请联系管理员!');";
            }
        }
        else
        {
            ltlAlert.Text = "alert('保存失败，请联系管理员!');";
        }
        attachBind();
    }

    protected void btn_return_Click(object sender, EventArgs e)
    { 
        Response.Redirect("WF_WorkFlowInstanceList.aspx?rm=" + DateTime.Now, true);
    }

    protected void attachBind()
    {
        DataTable dt = wf.GetFniAttach(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim() == "" ? "0" : txtSortOrder.Text.Trim()));
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            this.gvFNIA.DataSource = dt;
            this.gvFNIA.DataBind();
            int ColunmCount = gvFNIA.Rows[0].Cells.Count;
            gvFNIA.Rows[0].Cells.Clear();
            gvFNIA.Rows[0].Cells.Add(new TableCell());
            gvFNIA.Rows[0].Cells[0].ColumnSpan = ColunmCount;
            gvFNIA.Rows[0].Cells[0].Text = "暂无附件!";
            gvFNIA.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            this.gvFNIA.DataSource = dt;
            this.gvFNIA.DataBind();
        }
        FormBind();
    }

    private void FormBind()
    {
        divForm.InnerHtml = wf.GenerateForm(txtReqNbr.Text.Trim(), txtSortOrder.Text, btn_save.Enabled);
    }

    protected void gvFNIA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "download")
        {
            ltlAlert.Text = "var w=window.open('/WF/WF_AttachViewDown.aspx?id=" + e.CommandArgument.ToString().Trim() + "','AttachViewDown',";
            ltlAlert.Text += "'menubar=No,scrollbars = No,resizable=No,width=1,height=1,top=0,left=0'); w.focus();";
        }
    }

    protected void gvFNIA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!wf.JudgeIsCanModifiedWithApplier(Request.QueryString["nbr"].ToString(), Convert.ToInt32(Session["uID"])))
            {
                e.Row.Cells[4].Text = string.Empty;
            }
        }
    }

    protected void gvFNIA_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        wf.DeleteFniAttach(Convert.ToInt32(gvFNIA.DataKeys[e.RowIndex].Value));
        attachBind();
    }

    protected void TabStrip1_SelectedIndexChange(object sender, EventArgs e)
    {
        string wfn_nbr = Request.QueryString["nbr"].ToString();
        string type = Request.QueryString["tp"].ToString();
        int uID = Convert.ToInt32(Session["uID"]);

        txtSortOrder.Text = Convert.ToString((TabStrip1.SelectedIndex + 1) * 10);
        if (txtSortOrder.Text.Trim() == "10")
        {
            tblReview.Visible = false;
            if (wf.JudgeIsCanModifiedWithApplier(wfn_nbr,uID))
            {
                txtReqDate.Enabled = true;
                txtDueDate.Enabled = true;
                txtRemark.Enabled = true;
                fileAttach.Disabled = false; 
                btn_upload.Enabled = true; 
                btn_save.Enabled = true;
            }
            else
            {
                txtReqDate.Enabled = false;
                txtDueDate.Enabled = false;
                txtRemark.Enabled = false;
                fileAttach.Disabled = true; 
                btn_upload.Enabled = false; 
                btn_save.Enabled = false;
            }
        }
        else
        {
            tblReview.Visible = true;
            if (wf.JudgeIsCanModifiedWithApprover(txtReqNbr.Text.Trim(), Convert.ToInt32(txtSortOrder.Text.Trim()), uID))
            {
                txtRemark.Enabled = true;
                fileAttach.Disabled = false;
                radFinish.Enabled = true;
                radStop.Enabled = true; 
                btn_upload.Enabled = true; 
                btn_save.Enabled = true;
            }
            else
            {
                txtRemark.Enabled = false;
                fileAttach.Disabled = true;
                radFinish.Enabled = false;
                radStop.Enabled = false; 
                btn_upload.Enabled = false;
                btn_save.Enabled = false;
            }

        }

        SqlDataReader reader4 = wf.GetFNIInfoByNbrAndSort(wfn_nbr, Convert.ToInt32(txtSortOrder.Text.Trim()));
        if (reader4.Read())
        {
            txtRemark.Text = Convert.ToString(reader4["FNI_Remark"]);
            txtCreatedBy.Text = Convert.ToString(reader4["userName"]);
            txtCreatedByRole.Text = Convert.ToString(reader4["roleName"]);
            txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader4["FNI_RunDate"]);

            if (Convert.ToString(reader4["FNI_Status"]) == "2")
            {
                radFinish.Checked = true;
                radStop.Checked = false;
            }
            else
            {
                radFinish.Checked = false;
                radStop.Checked = true;
            }
        }
        reader4.Close();

        attachBind();
        //如果是发起的步骤，则按钮显示提交----王大龙
        if (txtSortOrder.Text == "10")
        {
            btn_save.Text = "提交";
        }
        else
        {
            btn_save.Text = "签署";
        }

        //绑定节点步骤描述
        SqlDataReader reader7 = wf.GetNodeSortDesc(wfn_nbr, txtSortOrder.Text.Trim());
        if (reader7.Read())
        {
            txtNoteDesc.Text = reader7["NodeDesc"].ToString();
        }
        reader7.Close();
    }
}
