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
using Wage;
using System.Text.RegularExpressions;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using MRInfo;
using System.Text;
using System.IO;
using System.Xml;
using CommClass;


public partial class mr_roomView : BasePage
{
    MeetingRoom mr = new MeetingRoom();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFormContorl();
            BindViewData();
        }
    }
    /// <summary>
    /// 绑定页面的基础数据
    /// </summary>
    private void BindFormContorl()
    {
        DataTable dt = null;
        string plants = "";
        if (!string.IsNullOrEmpty(Session["plantcode"].ToString()))
        {
            if (Session["plantcode"].ToString() == "1")
            {
                plants = "SZX";
            }
            if (Session["plantcode"].ToString() == "2")
            {
                plants = "ZQL";
            }
            if (Session["plantcode"].ToString() == "5")
            {
                plants = "YQL";
            }
            if (Session["plantcode"].ToString() == "8")
            {
                plants = "HQL";
            }
            dt = mr.GetMeetingRoomNO(plants, "");
        }
        ddl_meetingRooms.Items.Add("--");
        foreach (DataRow dr in dt.Rows)
        {
            ddl_meetingRooms.Items.Add(new ListItem(dr["mr_name"].ToString(), dr["mr_num"].ToString()));
        }
        ddl_Domain.Items.FindByValue(plants).Selected = true;
        BindDepartMent(ddl_Domain.SelectedValue);

        //ddl_meetingRooms_SelectedIndexChanged(null, EventArgs.Empty);
        this.lb_Date.Visible = true;
        tb_ViewStartDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

    }

    /// <summary>
    /// 绑定部门信息
    /// </summary>
    private void BindDepartMent(string Domain)
    {
        DataTable dt = null;

        dt = mr.GeDepartMentInfo(Domain);
        ddl_Department.Items.Clear();
        ddl_Department.Items.Add("--");
        foreach (DataRow dr in dt.Rows)
        {
            ddl_Department.Items.Add(new ListItem(dr["Name"].ToString(), dr["departmentID"].ToString()));
        }
    }


    protected void ddl_meetingRooms_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindViewData();
    }

    protected void btn_View_Click(object sender, EventArgs e)
    {
        BindViewData();
    }

    /// <summary>
    /// 依据当前，查询条件 查找内容并绑定
    /// </summary>
    private void BindViewData()
    {
        if (ddl_meetingRooms.SelectedIndex == 0)
        {
            if (string.IsNullOrEmpty(tb_ViewStartDate.Text))
            {
                this.Alert("请选择要查看的日期");
                return;
            }
        }

        int plantcode = Convert.ToInt32(Session["plantcode"].ToString());
        string RoomID = ddl_meetingRooms.SelectedValue;
        string StartViewDay = tb_ViewStartDate.Text.Trim();

        string EndViewDay = tb_ViewEndDate.Text.Trim();
        string Domain = ddl_Domain.SelectedValue;
        string DepartMent = ddl_Department.SelectedValue;
        //int self = Convert.ToInt32(Session["uID"].ToString());
        if (DepartMent == "--")
        {
            DepartMent = "0";
        }
        //if (Domain == "--")
        //{
        //    Domain = "0";
        //}
        this.gdv_MROrderList.DataSource = mr.FindOrderApprMRAndDetails(plantcode, RoomID, StartViewDay, EndViewDay, Domain, Convert.ToInt32(DepartMent), Convert.ToInt32(Session["uID"].ToString()),self.Checked);
        this.gdv_MROrderList.DataBind();
    }

    protected void gdv_MROrderList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CancelOrder")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _no = gdv_MROrderList.DataKeys[index].Values["MR_FormId"].ToString();
            if (Convert.ToInt32(Session["uID"]) != Convert.ToInt32(gdv_MROrderList.DataKeys[index].Values["MR_AppEno"]))
            {
                this.Alert("不可删除，不是创建人！");
                return;
            }
            if (Convert.ToDateTime(gdv_MROrderList.DataKeys[index].Values["mr_begintime"]) < DateTime.Now)
            {
                this.Alert("只能删除大于当前时间会议室申请，请联系管理员!");
                return;
            }

            DateTime begintime = Convert.ToDateTime(gdv_MROrderList.DataKeys[index].Values["mr_begintime"]);
            DateTime endtime = Convert.ToDateTime(gdv_MROrderList.DataKeys[index].Values["mr_endtime"]);
            string FormId = gdv_MROrderList.DataKeys[index].Values["MR_FormId"].ToString();

            int count1 = mr.DeleteMeetingRoomApp(Session["plantcode"].ToString(), e.CommandArgument.ToString(), begintime, endtime, Session["uID"].ToString(), Session["uName"].ToString(), FormId);
            if (count1 < 0)
            {
                this.Alert("删除失败，请联系管理员!");
                return;
            }            
            this.Alert("会议室取消成功!");
            BindViewData();
        }
        if (e.CommandName == "editReq")
        {
                string MR_FormId = e.CommandArgument.ToString();
                Response.Redirect("/Admin/mr_OrderRoom.aspx?mr_mstr=1" + "&MR_FormId=" + MR_FormId);                                                                              
        }
    }

    protected void gdv_MROrderList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

                string MR_AppEno = gdv_MROrderList.DataKeys[e.Row.RowIndex].Values["MR_AppEno"].ToString();


                if (Session["uID"].ToString() != MR_AppEno && this.Security["6100300"].isValid)
                {
                    ((LinkButton)e.Row.FindControl("mr_reqNo")).Enabled = false;
                    ((LinkButton)e.Row.FindControl("lbtn_Cancel")).Visible = false;

                    string FormId = gdv_MROrderList.DataKeys[e.Row.RowIndex]["MR_FormId"].ToString();
                    string str = "SELECT MR_IsApprove from  tcpc0.dbo.MR_mstr where MR_FormId = '" + FormId + "' ";
                    string a = SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn"), CommandType.Text, str).ToString();
                    if (a == "1")
                    {
                        e.Row.Cells[0].BackColor = System.Drawing.Color.GreenYellow;

                    }     
                }
                else
                {                                
                        ((LinkButton)e.Row.FindControl("mr_reqNo")).Enabled = true;
                        ((LinkButton)e.Row.FindControl("mr_reqNo")).Style.Value = "TEXT-DECORATION:solid";

                        string FormId = gdv_MROrderList.DataKeys[e.Row.RowIndex]["MR_FormId"].ToString();
                        string str = "SELECT MR_IsApprove from  tcpc0.dbo.MR_mstr where MR_FormId = '" + FormId + "' ";
                        string a = SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn"), CommandType.Text, str).ToString();
                        if (a == "1")
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.GreenYellow;

                        }
                        if (a == "0")
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.OrangeRed;
                        }
                        if (a != "1" && a != "0")
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Wheat;
                        }
                    }                                                      
        }
    }
    protected void gdv_MROrderList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView gvw = (GridView)sender;
        if (e.NewPageIndex < 0)
        {
            TextBox pageNum = (TextBox)gvw.BottomPagerRow.FindControl("txtNewPageIndex");
            int Pa = int.Parse(pageNum.Text);
            if (Pa <= 0)
            {
                gvw.PageIndex = 0;
            }
            else
            {
                gvw.PageIndex = Pa - 1;
            }
        }
        else
        {
            gvw.PageIndex = e.NewPageIndex;
        }
        BindViewData();
        //管理按钮状态
        if (gvw.PageIndex == 0 || gvw.PageIndex == gvw.PageCount - 1)
        {
            LinkButton btnF = (LinkButton)gvw.BottomPagerRow.FindControl("btnFirst");
            LinkButton btnP = (LinkButton)gvw.BottomPagerRow.FindControl("btnPrev");
            LinkButton btnN = (LinkButton)gvw.BottomPagerRow.FindControl("btnNext");
            LinkButton btnL = (LinkButton)gvw.BottomPagerRow.FindControl("btnLast");
            if (gvw.PageIndex == 0)
            {
                btnF.Enabled = false;
                btnP.Enabled = false;
                if (gvw.PageCount > 1)
                {
                    btnN.Enabled = true;
                    btnL.Enabled = true;
                }
            }
            else if (gvw.PageIndex == gvw.PageCount - 1)
            {
                btnN.Enabled = false;
                btnL.Enabled = false;
                if (gvw.PageCount > 1)
                {
                    btnF.Enabled = true;
                    btnP.Enabled = true;
                }
            }
        }
    }

    protected void ddl_Domain_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartMent(ddl_Domain.SelectedValue);
        BindViewData();
    }
    protected void ddl_Department_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindViewData();
    }
    protected void self_CheckedChanged(object sender, EventArgs e)
    {
        BindViewData();
    }
}
