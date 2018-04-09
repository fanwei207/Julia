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
using System.Collections.Generic;


public partial class mr_RoomStatusView : BasePage
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
    /// ��ҳ��Ļ�������
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
    /// �󶨲�����Ϣ
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
    /// ���ݵ�ǰ����ѯ���� �������ݲ���
    /// </summary>
    private void BindViewData()
    {
        if (ddl_meetingRooms.SelectedIndex == 0)
        {
            if (string.IsNullOrEmpty(tb_ViewStartDate.Text))
            {
                this.Alert("��ѡ��Ҫ�鿴������");
                return;
            }
        }

        int plantcode = Convert.ToInt32(Session["plantcode"].ToString());
        string RoomID = ddl_meetingRooms.SelectedValue;
        string StartViewDay = tb_ViewStartDate.Text.Trim();

        string EndViewDay = tb_ViewEndDate.Text.Trim();
        string Domain = ddl_Domain.SelectedValue;
        string DepartMent = ddl_Department.SelectedValue;
        
        if (DepartMent == "--")
        {
            DepartMent = "0";
        }
        this.gdv_MROrderList.DataSource = mr.FindOrderMRAndDetails(plantcode, RoomID, StartViewDay, EndViewDay, Domain, Convert.ToInt32(DepartMent), cb_effective.Checked,isCheck.Checked);
        this.gdv_MROrderList.DataBind();
    }

    protected void Smail(string _no)
    {
        DataTable dt = mr.GetMeetingRoomEmail(_no);
        string mailto = dt.Rows[0]["email"].ToString();
        string mailSubject = "ǿ�� - " + dt.Rows[0]["mr_name"].ToString() + "-����������֪ͨ";
        StringBuilder sb = new StringBuilder();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<html>");
                sb.Append("<form>");
                sb.Append("<br>");
                sb.Append("ALL," + "<br>");
                sb.Append("    ���л����ҵ�ʹ�������������! ��ϸ��Ϣ���¡�" + "<br>");
                sb.Append("�������Ҫ��ǰ����ģ����Լ�ʱ��ϵͳ���������롣" + "<br>");
                sb.Append("������������ϵ�ܾ��죬лл" + "<br>");
                sb.Append("<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "���뵥�ţ�" + _no + " ��" + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "��˾��" + dt.Rows[0]["MR_CompanyCode"].ToString() + " ��" + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "�����ң�" + dt.Rows[0]["mr_name"].ToString() + " ��" + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "ʱ��Σ�" + dt.Rows[0]["begindate"].ToString() + "~~" + dt.Rows[0]["enddate"].ToString() + "<br>");

                sb.Append("</body>");
                sb.Append("</form>");
                sb.Append("</html>");
            }
            DataTable d = getHaveAuthority(Convert.ToInt32(Session["uID"].ToString()));
            if (!this.SendEmail(d.Rows[0]["email"].ToString(), mailto, "", mailSubject, sb.ToString()))
            {
                this.Alert("�ʼ�����ʧ�ܣ�");
                return;
            }      
        }
    }

    protected void gdv_MROrderList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CancelOrder")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _no = gdv_MROrderList.DataKeys[index].Values["MR_FormId"].ToString();
            if(Convert.ToDateTime(gdv_MROrderList.DataKeys[index].Values["mr_begintime"]) < DateTime.Now)
            {
                this.Alert("ֻ��ɾ�����ڵ�ǰʱ����������룬����ϵ����Ա!");
                return;
            }
           
            DateTime begintime = Convert.ToDateTime(gdv_MROrderList.DataKeys[index].Values["mr_begintime"]);
            DateTime endtime = Convert.ToDateTime(gdv_MROrderList.DataKeys[index].Values["mr_endtime"]);
            string FormId = gdv_MROrderList.DataKeys[index].Values["MR_FormId"].ToString();

            int count1 = mr.DeleteMeetingRoomApp(Session["plantcode"].ToString(), e.CommandArgument.ToString(), begintime, endtime, Session["uID"].ToString(), Session["uName"].ToString(), FormId);
            if (count1 < 0)
            {
                this.Alert("ɾ��ʧ�ܣ�����ϵ����Ա!");
                return;
            }
            Smail(_no);                      
            this.Alert("������ȡ���ɹ�!");
            BindViewData();
        }        
    }

    protected void gdv_MROrderList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {                          
                    string FormId = gdv_MROrderList.DataKeys[e.Row.RowIndex]["MR_FormId"].ToString();
                    string str = "SELECT MR_IsApprove from  tcpc0.dbo.MR_mstr where MR_FormId = '" + FormId + "' ";
                    string a = SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn"), CommandType.Text, str).ToString();
                    if (a == "1")
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.GreenYellow;
                        e.Row.Cells[0].Enabled = false;
                    }
                    if (a == "0")
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.OrangeRed;
                        e.Row.Cells[0].Enabled = false;
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
        //����ť״̬
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
    protected void btn_Approve_Click(object sender, EventArgs e)
    {
            int num = 0;
            var list = new HashSet<int>();
            string[] formid = new string[100];
            foreach (GridViewRow row in gdv_MROrderList.Rows)
            {
                num++;
                if (((CheckBox)row.FindControl("chk")).Checked)
                {
                    int createby = Convert.ToInt32(gdv_MROrderList.DataKeys[row.RowIndex]["MR_AppEno"]);
                    string FormId = gdv_MROrderList.DataKeys[row.RowIndex]["MR_FormId"].ToString();
                    formid[num++] = FormId;
                    DataTable t = check(FormId);
                    if (t.Rows.Count > 0)
                    {
                        
                        //DataTable dt = mr.CheckHaveReport(rbl_CompanyCode.SelectedItem.Text.Trim(), ddl_RoomID.SelectedValue.ToString().Trim(), gdv_MROrderList.DataKeys[row.RowIndex]["begintime"], gdv_MROrderList.DataKeys[row.RowIndex]["endtime"], gdv_MROrderList.DataKeys[row.RowIndex]["MR_AppName"]);
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < t.Rows.Count; i++)
                        {
                            sb.Append(t.Rows[i]["formid"].ToString() + ";");                                                      
                        }
                        Alert("�������뵥����ʱ���н��棬���뵥��:\\n" + sb.ToString());
                        num = 0;
                        return;
                        sb.Clear();
                    }
                //    DataTable t = getOthers(FormId);
                //    if (t.Rows.Count > 0)
                //    {
                //        StringBuilder sb = new StringBuilder();
                //        for (int i = 0; i < t.Rows.Count; i++)
                //        {                                                        
                //            sb.Append(t.Rows[0]["formid"].ToString()+"<br>");
                //            Alert("����ͬ������뵥���뼰ʱ�ܾ��������뵥��:" + sb.ToString());                            
                //        }
                //        sb.Clear();
                //    }
                    list.Add(createby);
                    if (num > 0)
                    {
                        try
                        {
                            SqlParameter[] param = new SqlParameter[3];

                            param[0] = new SqlParameter("@uID", Session["uID"].ToString());
                            param[1] = new SqlParameter("@uName", Session["uName"].ToString());
                            param[2] = new SqlParameter("@FormId", FormId);
                            SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_mr_saveApprovehistory", param);
                            num--;
                        }

                        catch (Exception ee)
                        {
                            ltlAlert.Text = "alert('����ʧ��');";
                        }          
                    }
                                                      
                }
            }
            foreach (object F in list)
            {                           
                        SApproveEmail(Convert.ToInt32(F.ToString()));
                        ltlAlert.Text = "alert('�����ɹ�')";
                        BindViewData();            
            }                        
    }

    private DataTable check(string FormId)
    {
        string str = "sp_checktimeiscross";
        SqlParameter param = new SqlParameter("@FormId", FormId);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }

    public DataTable getOthers(string FormId)
    {
        string str = "sp_selectMrByApply";
        SqlParameter param = new SqlParameter("@FormId",FormId);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }


 protected void SApproveEmail(int createby)
    {
        DataTable dt = mr.GetApproveMeetingRoomEmail(createby);
        StringBuilder sb = new StringBuilder();
        string mailto = dt.Rows[0]["email"].ToString();
        string mailSubject = "ǿ�����������֪ͨ";
        if (dt.Rows.Count > 0)
        {
            sb.Append("<html>");
            sb.Append("<form>");
            sb.Append("<br>");
            sb.Append("ALL," + "<br>");
            sb.Append("    ���л���������׼ʹ��! ��ϸ��Ϣ���ѯ100ϵͳ��" + "<br>");
            sb.Append("<br>");        
            sb.Append("</body>");
            sb.Append("</form>");
            sb.Append("</html>");
        }
                DataTable d = getHaveAuthority(Convert.ToInt32(Session["uID"].ToString()));        
                if (!this.SendEmail(d.Rows[0]["email"].ToString(), mailto, "", mailSubject, sb.ToString()))
                {
                    this.Alert("�ʼ�����ʧ�ܣ�");
                    return;
                }                
    }

 private DataTable getHaveAuthority(int uID)
 {
     string str = "sp_mr_selectCurrentHaveAuthorityEmail";
     SqlParameter param = new SqlParameter("@uID",uID);

     return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str,param).Tables[0];
 }

 protected void isCheck_CheckedChanged(object sender, EventArgs e)
 {
     BindViewData();
 }
 protected void cb_effective_CheckedChanged(object sender, EventArgs e)
 {
     BindViewData();
 }
}
