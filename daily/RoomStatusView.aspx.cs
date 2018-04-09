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


public partial class RoomStatusView : BasePage
{
    MR rm = new MR();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFormContorl();
        }
    }
    /// <summary>
    /// 绑定页面的基础数据
    /// </summary>
    private void BindFormContorl()
    {
        DataTable dt = null;
        if (!string.IsNullOrEmpty(Session["plantcode"].ToString()))
        {
            string plants = "";
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
            dt = rm.GetMeetingRoomNO(plants, "");
        }
        ddl_meetingRooms.Items.Add("所有");
        foreach (DataRow dr in dt.Rows)
        {
            ddl_meetingRooms.Items.Add(new ListItem(dr["mr_name"].ToString(), dr["mr_num"].ToString()));
        }

        for (int i = 2010; i <= DateTime.Now.Year + 1; i++)
        {
            this.ddl_ViewYear.Items.Add(i.ToString());
        }

        for (int j = 1; j <= 12; j++)
        {
            this.ddl_ViewMonth.Items.Add(j.ToString());
        }
        this.ddl_ViewYear.Text = DateTime.Now.Year.ToString();
        this.ddl_ViewMonth.Text = DateTime.Now.Month.ToString();
        ddl_meetingRooms_SelectedIndexChanged(null, EventArgs.Empty);
    }

    protected void ddl_meetingRooms_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_meetingRooms.SelectedIndex == 0)
        {
            this.lb_Date.Visible = true;
            this.tb_ViewDate.Visible = true;
            this.lb_Month.Visible = false;
            this.ddl_ViewMonth.Visible = false;
            this.ddl_ViewYear.Visible = false;
        }
        else
        {
            this.lb_Date.Visible = false;
            this.tb_ViewDate.Visible = false;
            this.lb_Month.Visible = true;
            this.ddl_ViewMonth.Visible = true;
            this.ddl_ViewYear.Visible = true;
        }
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
            if (string.IsNullOrEmpty(tb_ViewDate.Text))
            {
                this.Alert("请选择要查看的日期");
                return;
            }
        }
        else
        {
            if (ddl_ViewMonth.SelectedIndex == -1 || ddl_ViewYear.SelectedIndex == -1)
            {
                this.Alert("请选择要查看的年月");
                return;
            }
        }
        int plantcode = Convert.ToInt32(Session["plantcode"].ToString());
        string RoomID = ddl_meetingRooms.SelectedValue;
        int ViewMonth = Convert.ToInt32(ddl_ViewMonth.SelectedValue);
        int ViewYear = Convert.ToInt32(ddl_ViewYear.SelectedValue);
        DateTime ViewDay;
        if (string.IsNullOrEmpty(tb_ViewDate.Text))
        {
            ViewDay = DateTime.Now;
        }
        else
        {
            ViewDay = Convert.ToDateTime(tb_ViewDate.Text);
        }
        this.gdv_MROrderList.DataSource = rm.FindOrderMRAndDetails(plantcode, RoomID, ViewYear, ViewMonth, ViewDay);
        this.gdv_MROrderList.DataBind();
    }

    protected void Smail(string _no)
    {
        DataTable dt = rm.GetMeetingRoomEmail(_no);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string mailto = dt.Rows[0]["email"].ToString();
                string mailSubject = "强凌 - " + dt.Rows[0]["mr_name"].ToString() + "申请变更通知";
                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("<form>");
                sb.Append("<br>");
                sb.Append("ALL," + "<br>");
                sb.Append("    下列会议室的使用情况已做调整! 详细信息如下。" + "<br>");
                sb.Append("如果有需要提前会议的，可以及时至系统中重新申请。" + "<br>");
                sb.Append("<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "公司：" + dt.Rows[0]["MR_CompanyCode"].ToString() + " ，" + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "会议室：" + dt.Rows[0]["mr_name"].ToString() + " ，" + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "时间段：" + dt.Rows[0]["begindate"].ToString() + "~~" + dt.Rows[0]["enddate"].ToString() + "<br>");

                sb.Append("</body>");
                sb.Append("</form>");
                sb.Append("</html>");

                if (!this.SendEmail("wangliwei@tcp-china.com", mailto, "", mailSubject, sb.ToString()))
                {
                    this.Alert("邮件发送失败！");
                    return;
                }
            }
        }
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
            if(Convert.ToDateTime(gdv_MROrderList.DataKeys[index].Values["mr_begintime"]) < DateTime.Now)
            {
                this.Alert("只能删除大于当前时间会议室申请，请联系管理员!");
                return;
            }
            DateTime begintime = Convert.ToDateTime(gdv_MROrderList.DataKeys[index].Values["mr_begintime"]);
            DateTime endtime = Convert.ToDateTime(gdv_MROrderList.DataKeys[index].Values["mr_endtime"]);

            int count1 = rm.DeleteMeetingRoomApp(Session["plantcode"].ToString(), e.CommandArgument.ToString(), begintime, endtime);
            if (count1 < 0)
            {
                this.Alert("删除失败，请联系管理员!");
                return;
            }
            Smail(_no);
            this.Alert("会议室取消成功!");
            BindViewData();
        }
    }

    protected void gdv_MROrderList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            //鼠标移动到某行上，该行变色
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#999999'");
            //鼠标移开后，恢复
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
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
}
