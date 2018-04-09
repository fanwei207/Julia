using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SID_CNT_EntryLeaveList : BasePage//System.Web.UI.Page
{
    adamClass chk = new adamClass();
    CNTManage cntManage = new CNTManage();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("550050010", "进厂登记权限");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {

            txt_enrtyTime1.Text = DateTime.Now.ToString("yyyy-MM-01"); 
            //txt_enrtyTime2.Text = DateTime.Now.AddDays(1).ToString();
            DataBind();
        }
    }
    protected void gv_cnt_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("CNT_CNTRegistration.aspx?type=1");
    }
    protected void DataBind()
    {
        bool seal = false, cnt = false, leave = false;
        if(chk_check.Checked)
        {
            cnt=true;
        }
        if(chk_seal.Checked)
        {
            seal=true;
        }
        if(chk_leave.Checked)
        {
            leave=true;
        }
        gv_cnt.DataSource = cntManage.SelectCntELInfo(txt_enrtyTime1.Text.Trim(), DateTime.Now.AddDays(1).ToString(), cnt, seal, leave);
        gv_cnt.DataBind();
        
    }
    protected void gv_cnt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CntChk")
        {
            int intRow = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex;
            string cntID = gv_cnt.DataKeys[intRow]["cnt_id"].ToString();
            string entryDate = gv_cnt.DataKeys[intRow]["cnt_entrydate"].ToString();
            string sealID = gv_cnt.Rows[intRow].Cells[4].Text;
            string plateNmb = gv_cnt.Rows[intRow].Cells[0].Text;


            this.Redirect("CNT_ContainerChk.aspx?cntID=" + cntID + "&entryDate=" + entryDate + "&plateNmb=" + plateNmb + "&sealID=" + sealID);
            //Response.Redirect("CNT_ContainerChk.aspx?cntID=" + cntID);
        }
        else if (e.CommandName == "SealChk")

        {
            int intRow = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex;
            string cntID = gv_cnt.DataKeys[intRow]["cnt_id"].ToString();
            string entryDate = gv_cnt.DataKeys[intRow]["cnt_entrydate"].ToString();

            this.Redirect("CNT_SealChk.aspx?cntID=" + cntID + "&entryDate=" + entryDate);
        }
        else if (e.CommandName == "TrackChk")
        {
            int intRow = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex;
            string cntID = gv_cnt.DataKeys[intRow]["cnt_id"].ToString();
            string entryDate = gv_cnt.DataKeys[intRow]["cnt_entrydate"].ToString();
            string leaveDate = ((LinkButton)(gv_cnt.Rows[intRow].Cells[5].FindControl("linkLeave"))).Text;
            string plateNmb = gv_cnt.Rows[intRow].Cells[0].Text.Trim();
            string driverName = gv_cnt.Rows[intRow].Cells[3].Text;
            string driverPhone = gv_cnt.DataKeys[intRow]["driver_phone"].ToString();
            string motorcadePhone = gv_cnt.DataKeys[intRow]["motorcade_phone"].ToString();
            

            this.Redirect("CNT_CargoTracking.aspx?cntID=" + cntID + "&entryDate=" + entryDate + "&leaveDate=" + leaveDate + "&driverName=" + driverName + "&driverPhone=" + driverPhone + "&motorcadePhone=" + motorcadePhone +"&plateNmb="+ plateNmb);
        }
        else if (e.CommandName == "Leave")
        {
            int intRow = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex;
            string cntID = gv_cnt.DataKeys[intRow].Values["cnt_id"].ToString();
            string entryDate = gv_cnt.DataKeys[intRow].Values["cnt_entrydate"].ToString();
            string cnt_checkdate = (gv_cnt.Rows[intRow].Cells[6].FindControl("linkCntChk") as LinkButton).Text.Trim();
            string seal_checkdate = (gv_cnt.Rows[intRow].Cells[7].FindControl("linkSealChk") as LinkButton).Text.Trim();

            if(cnt_checkdate=="检查" || seal_checkdate=="检查")
            {
                ltlAlert.Text = "alert('集装箱未检查或封条未检查，不能出厂！')";
                return;
            }

            int re = cntManage.CntLeave(cntID, entryDate,DateTime.Now.ToString());
            if (re == 0)
            {
                ltlAlert.Text = "alert('已经出厂，不可再次出厂！')";
            }
            else
                ltlAlert.Text = "alert('出厂成功！')";
            DataBind();
        }
    }
    protected void gv_cnt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!this.Security["550050010"].isValid)
            {
                //gv_cnt.Columns[5].ItemStyle.e
                (e.Row.Cells[5].FindControl("linkLeave") as LinkButton).Enabled = false;
            }
        }
    }
    protected void gv_cnt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_cnt.PageIndex = e.NewPageIndex;
        DataBind();
    }
}