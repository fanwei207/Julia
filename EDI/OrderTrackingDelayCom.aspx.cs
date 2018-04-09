using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Web.UI.WebControls;
public partial class EDI_OrderTrackingDelayCom : BasePage
{
    private string connStr = ConfigurationManager.AppSettings["SqlConn.Conn_edi"];
    protected void Page_Load(object sender, EventArgs e)
    {
        btnupdate.Visible = this.Security["44000657"].isValid;
        gvlist.Columns[2].Visible = this.Security["44000657"].isValid;
        BindData();
    }
    private void BindData()
    {
        gvlist.DataSource = GetOrderTracking();
        gvlist.DataBind();
    }
    public DataTable GetOrderTracking()
    {
        return SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "sp_edi_selectOrderTrackingDelayCom").Tables[0];
    }
    public bool insertOrderTracking(string item, string name)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@item", item);
            param[1] = new SqlParameter("@name", name);
            SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "sp_edi_insertOrderTrackingDelayCom", param);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
       
  
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvlist_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (txtItem.Text.Trim() == "")
        {
            ltlAlert.Text = "alert('编号不能为空!');";
            return;
        }
        else if (txtName.Text.Trim() == "")
        {
            ltlAlert.Text = "alert('原因不能为空!');";
            return;
        }
        if (insertOrderTracking(txtItem.Text.Trim(), txtName.Text.Trim()))
        {
            ltlAlert.Text = "alert('新增成功!');";
            BindData();
            return;
        }
        else
        {
            ltlAlert.Text = "alert('新增失败!');";
            BindData();
            return;
        }
    }
    protected void gvlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (this.Security["44000657"].isValid)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@item", gvlist.DataKeys[e.RowIndex].Values["Delay_Item"].ToString());

                SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "sp_edi_deleteOrderTrackingDelayCom", param);


                ltlAlert.Text = "alert('删除成功！');";

            }
            catch
            {
                ltlAlert.Text = "alert('删除失败！请刷新后重新操作一次！');";
            }

            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('你没有删除权限！');";
        }
    }
}