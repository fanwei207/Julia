using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rmInspection_GuestComplaint_SheetList : BasePage
{
    string strconn = ConfigurationManager.AppSettings["SqlConn.rmInspection"];
    protected void Page_Load(object sender, EventArgs e)
    {
        Bind();        
    }

    private DataTable getAgreedPre()
    {
        string str = "sp_comp_getPreAgreed";

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str).Tables[0];
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("../rmInspection/GuestComplaint_NewSheet.aspx?type=new");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bind();
    }

    private void Bind()
    {
        DataTable dt = getGuestCompMstr();
        gv.DataSource = dt;
        gv.DataBind();
    }

    private DataTable getGuestCompMstr()
    {
        string str = "sp_SelectGuestCompMstr";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@no", txtNo.Text);
        param[1] = new SqlParameter("@cust", txtGuest.Text);
        param[2] = new SqlParameter("@createdate", txtCreateDate.Text);
        param[3] = new SqlParameter("@status", ddlSatus.SelectedValue.ToString());

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable errDt = getCustCompMstrExport();//输出错误信息
        string title = "100^<b>投诉单号</b>~^100^<b>客户代码</b>~^100^<b>客户名</b>~^100^<b>严重等级</b>~^100^<b>客户等级</b>~^100^<b>客诉接收日期</b>~^100^<b>问题描述</b>~^100^<b>创建人</b>~^100^<b>创建日期</b>~^"
        + "100^<b>初判结果</b>~^100^<b>初判处理人</b>~^100^<b>责任方</b>~^100^<b>财务审批结果</b>~^100^<b>财务处理人</b>~^100^<b>决定赔偿方式</b>~^100^<b>决定赔偿方式人</b>~^100^<b>总经理审批结果</b>~^100^<b>处理人</b>~^"
        + "100^<b>客户反馈结果</b>~^100^<b>客户反馈处理人</b>~^100^<b>结单结果</b>~^100^<b>结单人</b>~^";
        if (errDt != null && errDt.Rows.Count > 0)
        {
            ExportExcel(title, errDt, false);
        }
    }

    private DataTable getCustCompMstrExport()
    {
        string str = "sp_SelectGuestCompMstrExport";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@no", txtNo.Text);
        param[1] = new SqlParameter("@cust", txtGuest.Text);
        param[2] = new SqlParameter("@createdate", txtCreateDate.Text);
        param[3] = new SqlParameter("@status", ddlSatus.SelectedValue.ToString());

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail")
        {            
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string no = gv.DataKeys[intRow].Values["GuestComplaintNo"].ToString();

            Response.Redirect("../rmInspection/GuestComplaint_SheetDetail.aspx?ID=" + gv.DataKeys[intRow].Values["ID"].ToString().Trim()+"&no=" +no);
        }
        if (e.CommandName == "ViewEdit")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string no = gv.DataKeys[index].Values["GuestComplaintNo"].ToString();
            string checkedItems = gv.DataKeys[index].Values["ApproachNames"].ToString().Replace("\n","").Replace("\r","");
          
            Response.Redirect("../rmInspection/GuestComplaint_NewSheet.aspx?type=edit&no=" + no +
                                                                "&cust=" + gv.DataKeys[index].Values["GuestName"].ToString()+
                                                                "&guestNo=" + gv.DataKeys[index].Values["GuestNo"].ToString()+                                                                                                                     
                                                                "&guestLevel=" + gv.DataKeys[index].Values["GuestLevel"].ToString()+
                                                                "&severity=" + gv.DataKeys[index].Values["SeverityName"].ToString()+                                                             
                                                                "&receivedate=" + gv.DataKeys[index].Values["ReceivedDate"].ToString()+
                                                                "&checkedItems=" + checkedItems +
                                                                "&problemContent=" + gv.DataKeys[index].Values["ProblemContent"].ToString());
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["GuestComp_Staus"]) != 0)
            {
                e.Row.Cells[0].Enabled = false;
                e.Row.Cells[9].Enabled = false;
            }
            //当客诉初判结果同意后，不能再修改
            DataTable dt = getAgreedPre();
            foreach (DataRow row in dt.Rows)
            {
                string no = gv.DataKeys[e.Row.RowIndex].Values["GuestComplaintNo"].ToString();
                if (row["GuestComplaintNo"].ToString() == no)
                {
                    e.Row.Cells[0].Enabled = false;   
                    ((LinkButton)e.Row.FindControl("linkNo")).Style.Value = "TEXT-DECORATION:none";

                }
            }
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["uID"].ToString() == gv.DataKeys[e.RowIndex].Values["createBy"].ToString() || Session["uRole"].ToString() == "1")
        {
            string strID = gv.DataKeys[e.RowIndex].Values["ID"].ToString();
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@ID", strID);

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_DeleteGuestCompMstr", sqlParam);

            Bind();
        } 
        else
        {
            ltlAlert.Text = "alert('您无权删除该单')";
        }
    }
}