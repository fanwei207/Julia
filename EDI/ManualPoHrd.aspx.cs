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
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;

public partial class ManualPoHrd : BasePage
{
    adamClass adam = new adamClass();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("10000030", "注册导入权限");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //有导入权限的人，方可删除
            gvlist.Columns[10].Visible = this.Security["10000030"].isValid;

            gvlist.Columns[14].Visible = this.Security["10000090"].isValid;
            
            if (this.Security["10000021"].isValid)
            {
                txtCreatedBy.Enabled = true;
            }
            else
            {
                txtCreatedBy.Enabled = false;
                txtCreatedBy.Text = Session["uName"].ToString();
            }
            txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            BindGridView();
        }
    }

    protected override void BindGridView()
    {
        SqlParameter[] param = new SqlParameter[9];
        param[0] = new SqlParameter("@cust", txtCust.Text.Trim());
        param[1] = new SqlParameter("@nbr", txtPoNbr.Text.Trim());
        param[2] = new SqlParameter("@reqDate", txtReqDate.Text.Trim());
        param[3] = new SqlParameter("@dueDate", txtDueDate.Text.Trim());
        param[4] = new SqlParameter("@shipto", txtShipTo.Text.Trim());
        param[5] = new SqlParameter("@shipvia", txtShipVia.Text.Trim());
        param[6] = new SqlParameter("@createdBy", txtCreatedBy.Text.Trim());
        param[7] = new SqlParameter("@createdDate", txtCreatedDate.Text.Trim());
        param[8] = new SqlParameter("@plantCode", Session["plantCode"].ToString());

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectManualPoHrd", param);

        gvlist.DataSource = ds;
        gvlist.DataBind();
    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //订单整张提交。一旦提交，则不允许更改
            if (Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["mpo_flg_sub"]))
            {
                ((HtmlInputCheckBox)e.Row.FindControl("chkImport")).Disabled = true;
                e.Row.Cells[10].Text = string.Empty;
                e.Row.Cells[13].Text = string.Empty;
            }

            //存在没有QAD号的行，设置背景色
            if (Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["mpo_flg_err"]))
            {
                e.Row.Cells[0].Style.Add("background-color", "Red");
                e.Row.Cells[0].ToolTip = "Some lines do not match QAD successfully";
            }

            //以HomeD订单为例，一般情况下先手工录入，TCP订单来的时候覆盖；有的时候，TCP订单都已经来了，却还没有提交，或才想起来提交
            if (Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["mpo_nbr_exist"]))
            {
                ((HtmlInputCheckBox)e.Row.FindControl("chkImport")).Disabled = true;
                //如果存在追加行，则放开
                if (Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["mpod_isAppended"]))
                {
                    ((HtmlInputCheckBox)e.Row.FindControl("chkImport")).Disabled = false;
                }

                e.Row.Cells[0].Style.Add("background-color", "Blue");
                e.Row.Cells[0].ToolTip = "This order has entered in system";
            }

            if (Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["mpo_isVerify"]))
                {
                    LinkButton linkPlan = (LinkButton)e.Row.FindControl("linkVerify");
                    linkPlan.Enabled = false;
                    linkPlan.Text =  gvlist.DataKeys[e.Row.RowIndex].Values["mpo_isVerifyDate"].ToString();
                    linkPlan.Style.Add("TEXT-DECORATION","none");
                }
                else
                {
                    ((HtmlInputCheckBox)e.Row.FindControl("chkImport")).Disabled = true;
                }
            
        }
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;

        BindGridView();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strIdList = string.Empty;//将选中的mpo_id拼接起来
        string strapp = string.Empty;
        string strMpoNbrlist = string.Empty;
        foreach (GridViewRow row in gvlist.Rows)
        {
            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");

            if (chkImport.Checked)
            {
                if (!checkManualPoDet(chkImport.Value))
                {
                    ltlAlert.Text = "alert('Order：" + gvlist.DataKeys[row.RowIndex]["mpo_nbr"].ToString() + "：Line is required!');";
                    BindGridView();
                    return;
                }
                strMpoNbrlist += gvlist.DataKeys[row.RowIndex]["mpo_nbr"].ToString() + ";";
                strIdList += chkImport.Value + ";";
                if (Convert.ToBoolean(gvlist.DataKeys[row.RowIndex]["mpod_isAppended"]))
                {
                    strapp += gvlist.DataKeys[row.RowIndex]["mpo_nbr"].ToString() + ";";
                }
            }
        }

        if (strIdList.Trim().Length != 0)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@uID", Session["uID"].ToString());
                param[1] = new SqlParameter("@uName", Session["uName"].ToString());
                param[2] = new SqlParameter("@idList", strIdList);
                param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[3].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_submitManualPo", param);
                if (!Convert.ToBoolean(param[3].Value))
                {
                    ltlAlert.Text = "alert('Operation fails!Please try again 1!');";
                }
                else
                {
                    this.SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), "suhuiming@" + baseDomain.Domain[0], "", "有新的手工维护订单进入系统", "有新的手工维护订单进入系统 订单号:" + strMpoNbrlist);
                }
            }
            catch (Exception ee)
            {
                ltlAlert.Text = "alert('Operation fails!Please try again 2!');";
            }
        }
        else
        {
            ltlAlert.Text = "alert('you must choose one item before submitting!');";
        }
        //if (strapp != string.Empty)
        //{
        //    //ltlAlert.Text = "alert('订单号：" + strapp+"');";
        //    //string strsql = "SELECT STUFF((SELECT ';'+email FROM tcpc0.dbo.Users WHERE plantCode=1 AND departmentID=4 AND leaveDate IS NULL AND ISNULL(email,'')<>'' FOR XML PATH('')),1,1,'')";
        //    //string emails = SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn"), CommandType.Text, strsql).ToString();
        //    this.SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), "suhuiming@"+ baseDomain.Domain[0], "", "订单追加了新行", "订单号：" + strapp);
        //}
        BindGridView();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (txtReqDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtReqDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('Req Date format is incorrect!');";
                return;
            }
        }

        if (txtDueDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtDueDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('Due Date format is incorrect!');";
                return;
            }
        }

        if (txtCreatedDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtCreatedDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('Ord Date format is incorrect!');";
                return;
            }
        }

        BindGridView();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('ManualPoExport.aspx?cust=" + txtCust.Text.Trim() + "&nbr=" + txtPoNbr.Text.Trim() + "&reqDate=" + txtReqDate.Text.Trim() + "&dueDate=" + txtDueDate.Text.Trim() + "&shipTo=" + txtShipTo.Text.Trim() + "&shipVia=" + txtShipVia.Text.Trim() + "&createdBy=" + txtCreatedBy.Text.Trim() + "&createdDate=" + txtCreatedDate.Text.Trim() + "&rt=" + DateTime.Now.ToString() + "', '_blank');";
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;

        foreach (GridViewRow row in gvlist.Rows)
        {
            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");

            if (!chkImport.Disabled)
            {
                chkImport.Checked = chkAll.Checked;
            }
        }

        //不能调用BindGridView()
        foreach (GridViewRow row in gvlist.Rows)
        {
            if (Convert.ToBoolean(gvlist.DataKeys[row.RowIndex].Values["mpo_flg_sub"].ToString()))
            {
                row.Cells[10].Text = string.Empty;
            }
        }
    }
    protected void gvlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@id", gvlist.DataKeys[e.RowIndex].Values["mpo_id"].ToString());
            param[1] = new SqlParameter("@uID", Session["uID"].ToString());
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());
            string a = gvlist.DataKeys[e.RowIndex].Values["mpo_id"].ToString();
            string b = Session["uID"].ToString();
            string c = Session["uName"].ToString();

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_deleteManualPoHrd", param);
        }
        catch
        {
            ltlAlert.Text = "alert('Operation fails!Please try again 3!');";
            return;
        }

        BindGridView();
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "plan")
        {
            LinkButton linkPlan = (LinkButton)e.CommandSource;
            int index = ((GridViewRow)linkPlan.Parent.Parent).RowIndex;
            Response.Redirect("../plan/soque_edit.aspx?id=0&hrd_id=" + e.CommandArgument.ToString() + "&cust=" + gvlist.Rows[index].Cells[0].Text.Trim() + "&nbr=" + linkPlan.Text.Trim() + "&reqDate=" + gvlist.Rows[index].Cells[2].Text.Trim() + "&dueDate=" + gvlist.Rows[index].Cells[3].Text.Trim() + "&rm=" + DateTime.Now.ToString());
        }
        else if (e.CommandName == "detail")
        {
            LinkButton linkPlan = (LinkButton)e.CommandSource;
            int index = ((GridViewRow)linkPlan.Parent.Parent).RowIndex;

            Response.Redirect("ManualPoNew.aspx?hrd_id=" + e.CommandArgument.ToString() + "&rm=" + DateTime.Now.ToString());
        }
        else if (e.CommandName == "verify")
        {
            LinkButton linkPlan = (LinkButton)e.CommandSource;
            int index = ((GridViewRow)linkPlan.Parent.Parent).RowIndex;

            Response.Redirect("ManualPoNew.aspx?hrd_id=" + e.CommandArgument.ToString() + "&approve=1&rm=" + DateTime.Now.ToString());
            //linkPlan.Text = "YES";
            //HtmlInputCheckBox chk = (HtmlInputCheckBox)this.gvlist.Rows[index].FindControl("chkImport");
            
            //string id = gvlist.DataKeys[index].Values["mpo_id"].ToString();


            //if (CheeckManualPoDocsExists(id))
            //{
            //    if (!ModifyManualPoIsVerify(id))
            //    {
            //        this.Alert("Verify and create must by different person!");
            //    }
            //    else
            //    {
            //        BindGridView();
            //    }
            //}
            //else
            //{
            //     this.Alert("The order does not have documnet!");
            //}
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateManualPo");
        }
        catch
        {
            ltlAlert.Text = "alert('Operation fails!Please try again 4!');";
            return;
        }

        BindGridView();
    }
    protected void btnHelp_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('../docs/EDI-ManualPO说明文档.rar?rt=" + DateTime.Now.ToString() + "', '_blank');";
    }

    protected bool checkManualPoDet(string id)
    {
        try
        {

            string strName = "sp_edi_CheckManualPoDetIsNull";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strName, param);
            return Convert.ToBoolean(param[1].Value);

        }
        catch (Exception ex)
        {
            return false;
        }

    
    }

}
