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
using System.Data.SqlClient;
using TCPNEW;
using adamFuncs;

public partial class new_FixAssetTypeDetail : BasePage
{ 
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            GetParentType();
            BindData();
        }

    }

    protected void GetParentType()
    {
        int parentID = Convert.ToInt32(Request.QueryString["id"]);
        SqlDataReader reader = GetDataTcp.GetParentType(parentID);
        while (reader.Read())
        {
            ParentType.Text = reader["fixtp_name"].ToString();
        
        }
    }

    protected void BindData()
    {
        txtFixDetailLift.Text = string.Empty;
        txtFixTypeDetail.Text = string.Empty;
        int parentID = Convert.ToInt32(Request.QueryString["id"]);
        DataSet ds = GetDataTcp.selectTypeDetailFixAsset(parentID);
        gvType.DataSource = ds;
        gvType.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtFixTypeDetail.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('类型 不能为空！');";
            return;
        }

        if (txtFixDetailLift.Text.Trim().Length > 0)
        {
            try
            {
                Int32 _n = Convert.ToInt32(txtFixDetailLift.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('使用年限 只能是数字！');";
                return; 
            }
        }

        GetDataTcp.SaveOrModifyTypeDetail(Convert.ToInt32(Request.QueryString["id"]), 0, txtFixTypeDetail.Text.Trim(), Convert.ToInt32(txtFixDetailLift.Text.ToString()), Convert.ToInt32(Session["uID"]));
        BindData();
    
    }

    protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvType.EditIndex = e.NewEditIndex;
        BindData();
    }
    
    //删除
    protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GetDataTcp.DeleteTypeDetail(Convert.ToInt32(gvType.DataKeys[e.RowIndex].Value.ToString()), Convert.ToInt32(Session["uID"]));
        BindData();
    }
      

    //更新
    protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        string fixtp_det_name = ((TextBox)(gvType.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
        string fixtp_det_lefttime = ((TextBox)(gvType.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
        GetDataTcp.SaveOrModifyTypeDetail(0, Convert.ToInt32(gvType.DataKeys[e.RowIndex].Value.ToString()), fixtp_det_name, Convert.ToInt32(fixtp_det_lefttime), Convert.ToInt32(Session["uID"]));
        gvType.EditIndex = -1;
        BindData();
    }


    //取消编辑
    protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvType.EditIndex = -1;
        BindData();
    }
    //返回
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/new/FixAssetType.aspx?rm=" + DateTime.Now.ToString(), true);
    }
	
	//翻页
	 protected void PageChange(object sender, GridViewPageEventArgs e)
    {
        gvType.PageIndex = e.NewPageIndex;
        gvType.SelectedIndex = -1;
        BindData();
    }
  

}
