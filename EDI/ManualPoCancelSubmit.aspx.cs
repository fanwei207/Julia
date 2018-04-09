using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using CommClass;

public partial class EDI_ManualPoCancelSubmit : BasePage
{
      
    protected override void OnPreInit(EventArgs e)
    {
        string a = "0";

        this.Security.Register("10000021", "可以查看别人的订单");
        base.OnPreInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //有导入权限的人，方可删除-- 引用才可取消
            gvlist.Columns[12].Visible = this.Security["10000030"].isValid;

            //可以查看别人的订单
            txtCreatedBy.Enabled = this.Security["10000021"].isValid; 
            txtCreatedBy.Text = Session["uName"].ToString(); 
            //BindgvData();
        }
    }

    protected void  BindgvData()
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@cust", txtCust.Text.Trim());
        param[1] = new SqlParameter("@nbr", txtPoNbr.Text.Trim()); 
        param[2] = new SqlParameter("@createdBy", txtCreatedBy.Text.Trim());
        param[3] = new SqlParameter("@createdDate", txtCreatedDate.Text.Trim());
        param[4] = new SqlParameter("@isCancelled", chkCancelled.Checked);

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectManualSubmittedPoHrdForCancel", param);
        
        gvlist.DataSource = ds;
        gvlist.DataBind();
    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (chkCancelled.Checked)
            { 
                e.Row.Cells[9].Text = "";
            }
        }
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;

        BindgvData();
    }

     protected void gvlist_RowEditing(object sender, GridViewEditEventArgs e)
     {
         gvlist.EditIndex = e.NewEditIndex;
         BindgvData();
     }

     protected void gvlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
     {
         gvlist.EditIndex = -1;
         BindgvData();
     }
   
     protected void gvlist_Updating(object sender, GridViewUpdateEventArgs e)
     {
         
         int index = e.RowIndex;
         TextBox txtCancelReason = (TextBox)gvlist.Rows[index].FindControl("txtCancelReason");

         if (txtCancelReason.Text.Trim() == string.Empty)
         {
             ltlAlert.Text = "alert('Enter the Cancel Reason,please')";
             return;
         }
         
         string mpo_id = gvlist.DataKeys[index].Values["mpo_id"].ToString();
         string mpo_nbr = gvlist.DataKeys[index].Values["mpo_nbr"].ToString();
         string cancelReason = txtCancelReason.Text.Trim();
         if (CancelManulPoSubmit(mpo_id, mpo_nbr, cancelReason))
         {
             ltlAlert.Text = "alert('Successfully Canceled!')";
             gvlist.EditIndex = -1;
             BindgvData();
             return;
         }
         else
         {
             ltlAlert.Text = "alert('Falure to Cancel!')";
             return;
         }

      
     }

     private bool CancelManulPoSubmit(string mpo_id, string mpo_nbr, string cancelReason)
     {
         SqlParameter[] param = new SqlParameter[6];
         param[0] = new SqlParameter("@mpo_id", mpo_id);
         param[1] = new SqlParameter("@nbr", mpo_nbr);
         param[2] = new SqlParameter("@canceReason", cancelReason); 
         param[3] = new SqlParameter("@cancelBy", Session["uID"].ToString());
         param[4] = new SqlParameter("@cancelName", Session["uName"].ToString());

         return Convert.ToBoolean(SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_CancelManualPoByNbr", param));        
     }

     
        
    protected void btnQuery_Click(object sender, EventArgs e)
    { 
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

        BindgvData();
    }
     
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "detail")
        {
            LinkButton linkPlan = (LinkButton)e.CommandSource;
            int index = ((GridViewRow)linkPlan.Parent.Parent).RowIndex;

            Response.Redirect("ManualPoNew.aspx?hrd_id=" + e.CommandArgument.ToString() + "&rm=" + DateTime.Now.ToString());
        }
    }
  
    
    protected void btnHelp_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('../docs/EDI-ManualPO说明文档.rar?rt=" + DateTime.Now.ToString() + "', '_blank');";
    } 
}