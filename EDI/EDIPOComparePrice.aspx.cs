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

public partial class EDIPOComparePrice : BasePage
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
            BindGridView();
            btn_submit.Attributes.Add("onclick", "return confirm('EDI价格更新， 请确认是否继续？');");
        }
    }

    protected override void BindGridView()
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@cust", txtCust.Text.Trim());
        param[1] = new SqlParameter("@nbr", txtPoNbr.Text.Trim());
        param[2] = new SqlParameter("@bak", Convert.ToInt32(ddl_hist.SelectedIndex));
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectEdiPoComparePrice", param);

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
                //((HtmlInputCheckBox)e.Row.FindControl("chkImport")).Disabled = true;
            }
        }
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;

        BindGridView();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
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
            //if (Convert.ToBoolean(gvlist.DataKeys[row.RowIndex].Values["mpo_flg_sub"].ToString()))
            //{
            //    row.Cells[10].Text = string.Empty;
            //}
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

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (GridViewRow row in gvlist.Rows)
        {
            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");
            if (chkImport.Checked == true)
            {
                string poNbr = gvlist.DataKeys[row.RowIndex].Values["poNbr"].ToString();
                string poline = gvlist.DataKeys[row.RowIndex].Values["poline"].ToString();
                string Price = gvlist.Rows[row.RowIndex].Cells[10].Text.ToString();
                string NewPrice = gvlist.DataKeys[row.RowIndex].Values["Pi_price3"].ToString();
                string StartDate = "";
                string EndDate = "";
                if (!string.IsNullOrEmpty(gvlist.Rows[row.RowIndex].Cells[12].Text.ToString().Replace("&nbsp;", "")))
                {
                    StartDate = gvlist.Rows[row.RowIndex].Cells[12].Text.ToString();
                }
                else
                {
                    StartDate = null;
                    this.Alert("价格开始日期不能为空！");
                    return;
                }
                if (!string.IsNullOrEmpty(gvlist.Rows[row.RowIndex].Cells[13].Text.ToString().Replace("&nbsp;","")))
                {
                    EndDate = gvlist.Rows[row.RowIndex].Cells[13].Text.ToString();
                }
                else
                {
                    EndDate = null;
                }

                if (string.IsNullOrEmpty(NewPrice))
                {
                    ltlAlert.Text = "alert('New Price can not 0 or Empty!');";
                    return;
                }
                try
                {
                    SqlParameter[] param = new SqlParameter[8];
                    param[0] = new SqlParameter("@poNbr", poNbr);
                    param[1] = new SqlParameter("@poline", poline);
                    param[2] = new SqlParameter("@Price", Price);
                    param[3] = new SqlParameter("@NewPrice", NewPrice);
                    param[4] = new SqlParameter("@StartDate", StartDate);
                    param[5] = new SqlParameter("@EndDate", EndDate);
                    param[6] = new SqlParameter("@uID", Session["uID"].ToString());
                    param[7] = new SqlParameter("@uName", Session["uName"].ToString());
                    SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_UpdateEdiPoPrice", param);
                }
                catch
                {
                    ltlAlert.Text = "alert('Insert fails!Please try again!');";
                    return;
                }
                i++;
            }
        }
        if (i <= 0)
        {
            ltlAlert.Text = "alert('Not select data!');";
            return;
        }
        BindGridView();
    }
}
