using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class price_pc_PriceApplyList : BasePage
{

    PC_price pc = new PC_price();

    /// <summary>
    /// ddl
    /// </summary>
    private string  DDLStatus
    {
        get
        {
            if (ViewState["DDLStatus"] == null)
            {
                ViewState["DDLStatus"] = "3";
            }
            return ViewState["DDLStatus"].ToString();
        }
        set
        {
            ViewState["DDLStatus"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            btnAddApply.Visible = this.Security["121000021"].isValid;
            btnAddApplyOut.Visible = this.Security["121000023"].isValid;
            if (Request["DDLStatus"] != null)
            {
                DDLStatus = Request["DDLStatus"].ToString();

            }
            ddlStatus.SelectedValue = DDLStatus;
            bind();
        
        }
    }

    protected void gvApplyList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvApplyList.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void gvApplyList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (Convert.ToBoolean(gvApplyList.DataKeys[e.Row.RowIndex].Values["isOut"]))
            {
                ((Label)e.Row.FindControl("lbIsOut")).Text = "是";
            }


            string a = ((Label)e.Row.Cells[9].FindControl("lbStatus")).Text.ToString();
            int status=Convert.ToInt32(a );

            if(status==1)
            {
                ((Label)e.Row.Cells[8].FindControl("lbStatus")).Text="已提交";
                ((LinkButton)e.Row.Cells[9].FindControl("lkbtnDelete")).Text = "";
            }
            else if(status==-1)
            {
                ((Label)e.Row.Cells[8].FindControl("lbStatus")).Text="驳回";
            }
            else if (status == 2)
            {
                ((Label)e.Row.Cells[8].FindControl("lbStatus")).Text = "已通过";
                ((LinkButton)e.Row.Cells[9].FindControl("lkbtnDelete")).Text = "";
            }
            else//其实是为零
            {
                ((Label)e.Row.Cells[8].FindControl("lbStatus")).Text = "未提交";
            }
            

           
            //if ("已完成".Equals(e.Row.Cells[4].Text.ToString()))
            //{
            //    ((LinkButton)e.Row.Cells[3].FindControl("lkbtnList")).Text = "";
            //}

        }
    }
    protected void gvApplyList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnDelete")
        {
            string PQID = e.CommandArgument.ToString();
            if (pc.deletePQ(PQID))
            {
                ltlAlert.Text = "alert('删除成功');";
            }
            else
            {
                ltlAlert.Text = "alert('删除失败');";
            }
            bind();
        }
        if (e.CommandName == "lkbtnList")//明细
        {
             int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;


                     bool isout = Convert.ToBoolean( gvApplyList.DataKeys[index].Values["isout"]);
                     string PQID = e.CommandArgument.ToString();
                     string Status = gvApplyList.DataKeys[index].Values["Status"].ToString();
                     string uName = gvApplyList.DataKeys[index].Values["ApplyBy"].ToString();
                     string applyDate = gvApplyList.DataKeys[index].Values["ApplyDate"].ToString();
                     string createBy = gvApplyList.DataKeys[index].Values["AppliByID"].ToString();
                     Response.Redirect("pc_PriceApply.aspx?PQID=" + PQID + "&Status=" + Status + "&uName=" + uName + "&ApplyDate=" + applyDate + "&AppliByID=" + createBy + "&DDLStatus=" + ddlStatus.SelectedItem.Value + "&isout=" + isout);
             
            
           

        
        }
        //if (e.CommandName == "lkbtnStates")//查看列表
        //{
        //    int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
        //    string PQID = e.CommandArgument.ToString();
        //    string company = gvApplyList.DataKeys[index].Values["Company"].ToString();
        //    string uName = gvApplyList.DataKeys[index].Values["ApplyBy"].ToString();
        //    string createDate = gvApplyList.DataKeys[index].Values["ApplyDate"].ToString();
        //    string states = gvApplyList.DataKeys[index].Values["States"].ToString();
        //    ltlAlert.Text = "window.showModalDialog('pc_newApplyStatesList.aspx?company=" + company + "&uName=" + uName + "&createDate=" + createDate + "&states=" + states + "&PQID=" + PQID + "', window, 'dialogHeight: 600px; dialogWidth: 800px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
        //}
        
    }


    private void bind()
    {
        int status=0;
        if("已提交".Equals(ddlStatus.SelectedItem.Text))
        {
            status=1;
        }
        else if("驳回".Equals(ddlStatus.SelectedItem.Text))
        {
        
            status=-1;
        }
        else if ("已通过".Equals(ddlStatus.SelectedItem.Text))
        {
            status = 2;
        }
        else if ("全部".Equals(ddlStatus.SelectedItem.Text))
        {
            status = 3;
           
        }

        DataTable dt = pc.selectApplyMstr(txtQAD.Text.ToString().Trim(), txtApplyBy.Text.ToString().Trim(), txtStarDate.Text.ToString().Trim(), txtEndDate.Text.ToString().Trim(), status , ddlType.SelectedItem.Value);
        gvApplyList.DataSource = dt;
        gvApplyList.DataBind();
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bind();
    }

    /// <summary>
    /// 新的报价申请
    /// 1新建一个报价单
    /// 2转跳到下一个页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddApply_Click(object sender, EventArgs e)
    {
        Response.Redirect("../part/NPart_AddPcApply.aspx");
        /* 2015-12-21注释，修改流程
        string PQID = string.Empty;
        string Status = "0";
        string reason = string.Empty;
        string uName = string.Empty;
        bool isout = false;

        if (pc.addNewPQ(Convert.ToInt32(Session["uID"]), out PQID, out uName, isout))
        {
            Response.Redirect("pc_PriceApply.aspx?PQID=" + PQID + "&Status=" + Status + "&uName=" + uName + "&ApplyDate=" + string.Format("{0:d}", DateTime.Now) + "&AppliByID=" + Convert.ToInt32(Session["uID"]) + "&isout=false");
            
        }
        else
        {
             ltlAlert.Text = "alert('添加失败，请重试');";
        }*/
    }
    protected void btnAddApplyOut_Click(object sender, EventArgs e)
    {
        string PQID = string.Empty;
        string Status = "0";
        string reason = string.Empty;
        string uName = string.Empty;
        bool isout = true;

        if (pc.addNewPQ(Convert.ToInt32(Session["uID"]), out PQID, out uName, isout))
        {
            Response.Redirect("pc_PriceApply.aspx?PQID=" + PQID + "&Status=" + Status + "&uName=" + uName + "&ApplyDate=" + string.Format("{0:d}", DateTime.Now) + "&AppliByID=" + Convert.ToInt32(Session["uID"]) + "&isout=true");

        }
        else
        {
            ltlAlert.Text = "alert('添加失败，请重试');";
        }
    }
}