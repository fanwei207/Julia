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


public partial class RDW_rdw_ProjectQadApproveList : BasePage
{
    RDW apply = new RDW();
    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Convert.ToString(Request.QueryString["mid"])))
        { 
            //btnApply.Visible = false;
            btnBack.Visible = false;
            lblmid.Text = "0";
              
        }
        else
        {
            if (Request.QueryString["type"] == "new")
            {
                btnApply.Visible = true;
                btnBack.Visible = false;
                lblmid.Text = "0";
            }
            else
            {
                btnApply.Visible = true;
                lblmid.Text = Convert.ToString(Request.QueryString["mid"]);
                BindProjHeaderData();
            }  
        }
        
        BindProjData();
    }

    private void BindProjData()
    {
        //string strID = Convert.ToString(Request.QueryString["mid"]);
        int strmID = int.Parse(lblmid.Text);
        string strProjName = txtProject.Text.ToString();
        string strProjCode = txtProjectCode.Text.ToString();
        string strApplyDate = txtApplyDate.Text.ToString();
        bool pendingToAppr = chkb_displayToApprove.Checked;
        int userId = int.Parse(Session["uID"].ToString());
        int roleId = int.Parse(Session["uRole"].ToString());
        DataTable dt = apply.getProjQadApplyMstr(strmID, strProjName, strProjCode, strApplyDate, pendingToAppr, userId, roleId);

        gv.DataSource = dt;
        gv.DataBind();
        
    }

    private void BindProjHeaderData()
    {
        //定义参数
        string strID = Convert.ToString(Request.QueryString["mid"]);
        RDW_Header rh = apply.SelectRDWHeader(strID);
        txtProject.Text = rh.RDW_Project.Trim();
        txtProjectCode.Text = rh.RDW_ProdCode;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindProjData();
    } 

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        this.BindProjData();
    } 
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string type;       
        //int index = Convert.ToInt32(e.CommandArgument.ToString());
         
        //string strQpaid = gv.DataKeys[index].Values["rdw_pqid"].ToString();
        //string strmid = gv.DataKeys[index].Values["rdw_MstrID"].ToString();
        if (lblmid.Text == "" || lblmid.Text == "0")
        {
            type = "new";
        }
        else
        {
            type = "proj";
        }

        if (e.CommandName == "look")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string strQpaid = gv.DataKeys[index].Values["rdw_pqid"].ToString();
            string strmid = gv.DataKeys[index].Values["rdw_MstrID"].ToString();
            Response.Redirect("RDW_ProjQadApply.aspx?type=" + type + "&pqmId=" + strQpaid + "&mid=" + strmid + "&islook=yes&iApprove=no", true);
        }
        if (e.CommandName == "Approve")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string strQpaid = gv.DataKeys[index].Values["rdw_pqid"].ToString();
            string strmid = gv.DataKeys[index].Values["rdw_MstrID"].ToString();
            Response.Redirect("RDW_ProjQadApply.aspx?type=" + type + "&pqmId=" + strQpaid + "&mid=" + strmid + "&islook=no&istartApprove=yes&approveId=", true);
        } 
    }

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int iApplyId = Convert.ToInt32(gv.DataKeys[e.RowIndex]["rdw_pqid"].ToString());
        apply.deleteProjQadApply(iApplyId);
       
        BindProjData();
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
 
            ((LinkButton)e.Row.FindControl("lnkDelete")).Style.Add("font-weight", "normal");
            ((LinkButton)e.Row.FindControl("lnkFirstApp")).Style.Add("font-weight", "normal");

            if (gv.DataKeys[e.Row.RowIndex].Values["rdw_applyby"].ToString() == Convert.ToString(Session["uID"]) && e.Row.Cells[9].Text.ToString() == "&nbsp;")
            { 
                ((LinkButton)e.Row.FindControl("lnkDelete")).Enabled = true;
            }

            if (e.Row.Cells[9].Text.ToString() != "&nbsp;")
            {
                ((LinkButton)e.Row.FindControl("lnkFirstApp")).Text = "Approved";

            }
            else
            {
                if (gv.DataKeys[e.Row.RowIndex].Values["rdw_approverId"].ToString() == Convert.ToString(Session["uID"]) && e.Row.Cells[9].Text.ToString() == "&nbsp;")
                {
                    ((LinkButton)e.Row.FindControl("lnkFirstApp")).Enabled = true;
                }
            }
        }
    }
    

   // /// <summary>
   // /// 获取所有申请的记录
   // /// </summary>
   // /// <param name="strProjName"></param>
   // /// <param name="strProjCode"></param>
   // /// <param name="strApplyDate"></param>
   // /// <param name="pendingToAppr"></param>
   // /// <returns></returns>
   // private DataTable getProjQadApplyMstr(int strmID,string strProjName, string strProjCode, string strApplyDate, bool pendingToAppr,int userId,int roleId)
   // {
   //     string strSql = "sp_RDW_selectProjQadApplyList";
   //     SqlParameter[] sqlParam = new SqlParameter[7];
   //     sqlParam[0] = new SqlParameter("@projName", strProjName);
   //     sqlParam[1] = new SqlParameter("@projCode", strProjCode);
   //     sqlParam[2] = new SqlParameter("@applyDate", strApplyDate);
   //     sqlParam[3] = new SqlParameter("@pendingToAppr", pendingToAppr);
   //     sqlParam[4] = new SqlParameter("@strmID", strmID);
   //     sqlParam[5] = new SqlParameter("@uid", userId);
   //     sqlParam[6] = new SqlParameter("@roleid", roleId);

   //     return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
   // }
     
   ///// <summary>
   ///// 删除 
   ///// </summary>
   ///// <param name="iApplyId"></param>
   // private void deleteProjQadApply(int iApplyId)
   // {
   //     string strSql = "sp_RDW_deleteProjQadApply";
   //     SqlParameter[] sqlParam = new SqlParameter[4];
   //     sqlParam[0] = new SqlParameter("@rdw_pqid", iApplyId); 
   //     SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, sqlParam);
   // }
    protected void btnApply_Click(object sender, EventArgs e) 
    {
        string type = "";
        string stMID = Request.QueryString["mid"] == null ? "0" : Convert.ToString(Request.QueryString["mid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        if (lblmid.Text == "" || lblmid.Text == "0")
        {
            type = "new";
        }
        else
        {
            type = "proj";
        }
        Response.Redirect("/RDW/RDW_ProjQadApply.aspx?type=" + type + "&mid=" + stMID + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
  
        Response.Write("");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
        string strST = Request.QueryString["st"] == null ? "" : Convert.ToString(Request.QueryString["st"]);
        Response.Redirect("/RDW/RDW_DetailList.aspx?mid=" + strMID + "&fr=" + strQuy + "&st=" + strST + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
          
    }
    protected void BtnExport_Click(object sender, EventArgs e)
    {
        int strmID = int.Parse(lblmid.Text);
        string strProjName = txtProject.Text.ToString();
        string strProjCode = txtProjectCode.Text.ToString();
        string strApplyDate = txtApplyDate.Text.ToString();
        bool pendingToAppr = chkb_displayToApprove.Checked;
        int userId = int.Parse(Session["uID"].ToString());
        int roleId = int.Parse(Session["uRole"].ToString());

        //ltlAlert.Text = "window.open('rdw_ProjQadListExport.aspx?pqmid=" + pqid + "', '_blank');";
        string title = "120^<b>Project Category</b>~^300^<b>Project</b>~^100^<b>Project Code</b>~^110^<b>QAD</b>~^300^<b>QAD Desc</b>~^<b>Applyer</b>~^<b>Apply Date</b>~^";
        string strsql = @"	 Select distinct C.cate_code,  M.RDW_Project, M.RDW_ProdCode, Q.qad, pt.pt_desc, A.rdw_applyName,A.rdw_applyDate
	 from  RDW_APPLYQAD Q
	 INNER JOIN dbo.rdw_ProjQadApply A ON Q.rdw_pqapplyid=A.rdw_pqid
	 LEFT Join RDW_Mstr M On A.RDW_MstrID = M.RDW_MstrID
	 Left Join RDW_Category C on M.RDW_Category = C.cate_id
     LEFT JOIN dbo.rdw_ProjQadApprove B ON A.rdw_pqid=B.rdw_pqid 
	 Left Join 
		(
			Select distinct pt_part ,pt_desc1 + ' ' + pt_desc2 As pt_desc
			From Qad_Data..pt_mstr 
			Where pt_domain='szx'
		)  pt On pt.pt_part = Q.qad
	 where  Q.selected=1 ";
        if (strmID != 0)
        {
            strsql += " and A.rdw_MstrID =" + strmID.ToString();
        }
        if (strProjName != "")
        {
            strsql += " and M.RDW_Project like REPLACE('" + strProjName + "','*','%') ";
        }
        if (strProjCode != "")
        {
            strsql += " and M.RDW_ProdCode like REPLACE('" + strProjCode + "','*','%') ";
        }
        if (strApplyDate != "")
        {
            strsql += " and A.rdw_applyDate >= '" + strApplyDate + "' AND A.rdw_applyDate < DATEADD(day,1,'" + strApplyDate + "')";
        }
        if (pendingToAppr)
        {
            strsql += " and A.rdw_approveDate is  null";
        }
        if (roleId != 0 && roleId != 1)
        {
            strsql += " AND  (A.rdw_applyby=" + userId + " OR B.rdw_approverid=" + userId + ")";
        }
        this.ExportExcel(strConn, title, strsql, false);
    }
    protected void chkb_displayToApprove_CheckedChanged(object sender, EventArgs e)
    {
        BindProjData();
    }
}