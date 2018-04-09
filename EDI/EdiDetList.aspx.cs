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
using Microsoft.ApplicationBlocks.Data;
using CommClass;

public partial class EDI_EdiDetList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["orderdir"] = "ASC";
            gvBind(Request["po_id"].ToString().Trim());
            //if (Session["plantCode"].ToString() != "11")
            //{
            //    gvlist.Columns[14].Visible = false;
            //}
        }
    }

    private void gvBind(string po_id)
    {
        DataSet dsPo = getEdiData.getEdiPoDet(po_id);

        if (dsPo.Tables[0].Rows.Count == 0)
        {
            dsPo.Tables[0].Rows.Add(dsPo.Tables[0].NewRow());
        }
        gvlist.DataSource = dsPo;
        gvlist.DataBind();
    }

    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ToPlan")
        {
            getEdiData.updateDetToPlan(e.CommandArgument.ToString().Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            gvBind(Request["po_id"].ToString().Trim());
        }
        else if (e.CommandName == "Reject")
        {
            getEdiData.RejectOrder(e.CommandArgument.ToString().Trim());

            gvBind(Request["po_id"].ToString().Trim());
        }
        else if (e.CommandName == "BigOrder")
        {
            try
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@det_id", e.CommandArgument.ToString().Trim());
                param[1] = new SqlParameter("@uID", Session["uID"]);
                param[2] = new SqlParameter("@uName", Session["uName"]);

                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_showBigOrder", param);
            }
            catch
            {
                ltlAlert.Text = "alert('操作失败！刷新后重新操作一次！');";
                return;
            }

            gvBind(Request["po_id"].ToString().Trim());
        }
        else if (e.CommandName == "need")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            LinkButton linkImport = (LinkButton)gvlist.Rows[index].FindControl("linkImport");
            string type = linkImport.Text;
            string id = gvlist.DataKeys[index].Values["id"].ToString();
            getEdiData.UpdatePoDetNeedProp(id, Session["PlantCode"].ToString(),type);
            gvBind(Request["po_id"].ToString().Trim());
        
        }
        else if (e.CommandName == "CancelDEI")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvlist.DataKeys[index].Values["id"].ToString();
            LinkButton linkCancel = (LinkButton)gvlist.Rows[index].FindControl("linkCancel");

            getEdiData.UpdatePoHrdCancelProp(id, Session["PlantCode"].ToString(), "行");

            gvBind(Request["po_id"].ToString().Trim());
        }
    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //if (((Label)e.Row.FindControl("lbl_err")) != null && (Button)e.Row.FindControl("btn_currect") != null)
            //{
            //    //Label lbl_err = (Label)e.Row.FindControl("lbl_err");
            //    //Button btn_currect = (Button)e.Row.FindControl("btn_currect");

            //    //if (lbl_err.Text.Trim().Length == 0)
            //    //{
            //    //    btn_currect.Visible = false;
            //    //}
            //    //else
            //    //{
            //    //    btn_currect.Visible = true;
            //    //}
            //}

            //if (Request["filter"] == "0" || Request["filter"] == "2")
            //{
            //    if (gvlist.DataKeys[e.Row.RowIndex].Values["isrejected"].ToString() == "True")
            //    {
            //        e.Row.Cells[13].Text = "Rejected";
            //    }
            //}
            //else
            //{
            //    if (gvlist.DataKeys[e.Row.RowIndex].Values["isrejected"].ToString() == "True")
            //    {
            //        e.Row.Cells[13].Text = "Rejected";
            //    }
            //    else
            //    {
            //        e.Row.Cells[13].Text = string.Empty;
            //    }
            //}

            //try
            //{
            //    Button btnBigOrder = (Button)e.Row.FindControl("btnBigOrder");

            //    if (Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["det_inBigOrder"]))
            //    {
            //        btnBigOrder.Text = "不显示";
            //    }
            //    else
            //    {
            //        btnBigOrder.Text = "显示";
            //    }
            //}
            //catch
            //{
            //    ;
            //}


            if ((Label)e.Row.FindControl("lbl_dateInfo") != null)
            {
                Label lbl_dateInfo = (Label)e.Row.FindControl("lbl_dateInfo");
                if (lbl_dateInfo.Text.Trim() == "1900-01-01")
                {
                    lbl_dateInfo.Text = "";
                }
            }
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    try
            //    {
            //        LinkButton linkImport = (LinkButton)e.Row.FindControl("linkImport");
            //        if (Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["notNeeded"]))
            //        {
            //            linkImport.Text = "不导入";
            //        }
            //    }
            //    catch
            //    { }
            //}
        }
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        gvBind(Request["po_id"].ToString().Trim());
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EdiHrdList.aspx?rm=" + DateTime.Now);
    }
    protected void btnToPlan_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow row in gvlist.Rows)
        {
            CheckBox chk = row.FindControl("chk") as CheckBox;
            if (chk != null && chk.Checked)
            {
                count += 1;
                string id = gvlist.DataKeys[row.RowIndex].Values["id"].ToString();
                getEdiData.updateDetToPlan(id, Session["uID"].ToString(), Session["uName"].ToString());
            }
        }    
        if (count == 0)
        {
            ltlAlert.Text = "alert('请选择需要操作的数据！');";
        }
        else
        {
            gvBind(Request["po_id"].ToString().Trim());
            ltlAlert.Text = "alert('操作成功！');";
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow row in gvlist.Rows)
        {
            CheckBox chk = row.FindControl("chk") as CheckBox;
            if (chk != null && chk.Checked)
            {
                count += 1;
                if (gvlist.DataKeys[row.RowIndex].Values["isrejected"].ToString() != "True")
                {
                    string id = gvlist.DataKeys[row.RowIndex].Values["id"].ToString();
                    getEdiData.RejectOrder(id);
                }
            }
        }
        if (count == 0)
        {
            ltlAlert.Text = "alert('请选择需要操作的数据！');";
        }
        else
        {
            gvBind(Request["po_id"].ToString().Trim());
            ltlAlert.Text = "alert('操作成功！');";
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow row in gvlist.Rows)
        {
            CheckBox chk = row.FindControl("chk") as CheckBox;
            if (chk != null && chk.Checked)
            {
                count += 1;
                int index = row.RowIndex;
                LinkButton linkImport = (LinkButton)gvlist.Rows[index].FindControl("linkImport");
                string type = linkImport.Text;
                string id = gvlist.DataKeys[index].Values["id"].ToString();
                getEdiData.UpdatePoDetNeedProp(id, Session["PlantCode"].ToString(), type);
            }
        }
        if (count == 0)
        {
            ltlAlert.Text = "alert('请选择需要操作的数据！');";
        }
        else
        {
            gvBind(Request["po_id"].ToString().Trim());
            ltlAlert.Text = "alert('操作成功！');";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow row in gvlist.Rows)
        {
            CheckBox chk = row.FindControl("chk") as CheckBox;
            if (chk != null && chk.Checked)
            {
                count += 1;
                int index = row.RowIndex;
                string id = gvlist.DataKeys[index].Values["id"].ToString();
                LinkButton linkCancel = (LinkButton)gvlist.Rows[index].FindControl("linkCancel");
                getEdiData.UpdatePoHrdCancelProp(id, Session["PlantCode"].ToString(), "行");
            }
        }
        if (count == 0)
        {
            ltlAlert.Text = "alert('请选择需要操作的数据！');";
        }
        else
        {
            gvBind(Request["po_id"].ToString().Trim());
            ltlAlert.Text = "alert('操作成功！');";
        }
    }
}
