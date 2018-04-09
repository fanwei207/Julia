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

public partial class HR_hr_leave : BasePage
{
    adamClass chk = new adamClass();
    HR hr = new HR();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01";
            txtEndDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            BindData();
        }
    }

    protected void BindData()
    {
        //�������
        int type = Convert.ToInt32(ddlType.SelectedValue.ToString());
        string strStart = txtStartDate.Text.Trim();
        string strEnd = txtEndDate.Text.Trim();
        string strUserNo = txtUserNo.Text.Trim();
        string strUserName = txtUserName.Text.Trim();
        int uID = Convert.ToInt32(Session["uID"]);
        int uRole = Convert.ToInt32(Session["uRole"]);

        HR hr = new HR();

        gvLeave.DataSource = hr.SelectLeaveList(type, strStart, strEnd, strUserNo, strUserName, uID, uRole);
        gvLeave.DataBind();
    }

    /// <summary>
    /// ���ݲ���һҳҲ��ʾGridView��ҳ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLeave_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvLeave_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //�������
        int LeaveID = Convert.ToInt32(gvLeave.DataKeys[e.RowIndex].Value.ToString());
        int Type = Convert.ToInt32(ddlType.SelectedValue.Trim());

        if (hr.DeleteLeaveInfo(LeaveID))
        {
            ltlAlert.Text = "alert('" + ddlType.SelectedItem.Text + " ɾ���ɹ���');";

            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('ɾ�����ݹ����г���'); ";
        }
    }

    protected void gvLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLeave.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        #region ��֤
        if (txtLaborNo.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('Ա������ ����Ϊ�գ�');";
            return;
        }

        if (txtStartDate.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('��ʼ���� ����Ϊ�գ�');";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtStartDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('��ʼ���� ��ʽ����ȷ��');";
                return;
            }
        }

        if (txtEndDate.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('�������� ����Ϊ�գ�');";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtEndDate.Text.Trim());

                if (_dt < Convert.ToDateTime(txtStartDate.Text.Trim()))
                {
                    ltlAlert.Text = "alert('�������� ����С�� ��ʼ���ڣ�');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('�������� ��ʽ����ȷ��');";
                return;
            }
        }

        if (ddlType.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('��ѡ��һ�� ������ͣ�');";
            return;
        }

        if (txtDays.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('������� ����Ϊ�գ�');";
            return;
        }
        else
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(txtDays.Text.Trim());

                if (_dc <= 0)
                {
                    ltlAlert.Text = "alert('������� ֻ�ܴ���0��');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('������� ֻ�������֣�');";
                return;
            }
        }
        #endregion

        //�������
        string strDay = "0";
        int Type = Convert.ToInt32(ddlType.SelectedValue.ToString());
        string strStart = txtStartDate.Text.Trim();
        string strEnd = txtEndDate.Text.Trim();
        string strMemo = txtMemo.Text.Trim();
        string strLaborID = lblUID.Text.Trim();
        string strLaborNo = txtLaborNo.Text.Trim();
        string strLaborName = lblLaborNameValue.Text.Trim();
        int uID = Convert.ToInt32(Session["uID"]);
        bool Ret = false;
        int cntMerrage = 0;

        if(txtDays.Text.Trim() != "") strDay = txtDays.Text.Trim();

        Ret = hr.CheckLeaveDays(strStart, strEnd, strDay);
        if (Ret)
        {
            if (Type.ToString() == "3")
            {
                InsertMerrageInfo(Type, strStart, strEnd, strDay, strLaborID, strLaborNo, strLaborName, strMemo, uID);
                BindData();
            }
            else
            {
                if (hr.InsertLeaveInfo(Type, strStart, strEnd, strDay, strLaborID, strLaborNo, strLaborName, strMemo, uID))
                {
                    ltlAlert.Text = "alert('" + ddlType.SelectedItem.Text + "�����ɹ���');";
                    
                    txtLaborNo.Text = "";
                    lblLaborNameValue.Text = "";
                    lblUID.Text = "";
                    txtDays.Text = "";
                    txtMemo.Text = "";

                    BindData();
                }
                else
                {
                    ltlAlert.Text = "alert('�������ݹ����г���'); ";
                }
            }
        }
        else
        {
            TimeSpan startdate = new TimeSpan(Convert.ToDateTime(strStart).Ticks);
            TimeSpan enddate = new TimeSpan(Convert.ToDateTime(strEnd).Ticks);
            TimeSpan datediff = enddate.Subtract(startdate).Duration();
            ltlAlert.Text = "alert('�������Ӧ����" + Convert.ToString(datediff.Days) + "��" + Convert.ToString(datediff.Days + 1) + "֮�䣡'); Form1.txtDays.focus();";
            txtDays.Text = "";
        }
    }

    //�ж�����Ա�������Ƿ�Ϸ�
    protected void txtLaborNo_TextChanged(object sender, EventArgs e)
    {
        string strUserNo = txtLaborNo.Text.Trim();
        int PlantID = Convert.ToInt32(Session["PlantCode"]);

        string strUserName = hr.SelectUserName(strUserNo, PlantID);

        switch (strUserName)
        {
            case "":
                ltlAlert.Text = "alert('���Ų����ڣ�'); Form1.txtLaborNo.focus();";
                txtLaborNo.Text = "";
                lblLaborNameValue.Text = "";
                lblUID.Text = "";
                break;

            case "��Ա��������ְԱ����":
                ltlAlert.Text = "alert('" + strUserName + "'); Form1.txtLaborNo.focus();";
                txtLaborNo.Text = "";
                lblLaborNameValue.Text = "";
                lblUID.Text = "";
                break;

            default:
                string[] struser = strUserName.Split(',');
                if (struser[2] != "")
                {
                    ltlAlert.Text = "alert('" + struser[2] + "'); Form1.txtMemo.focus();";
                }
                lblLaborNameValue.Text = struser[1];
                lblUID.Text = struser[0];

                string strLaborID = lblUID.Text.Trim();
                if (ddlType.SelectedValue == "3" && hr.SelectMerrageDaysInfo(strLaborID) > 0)
                {
                    btnAddNew.Attributes.Add("onclick", "return confirm('��Ա���Ѿ�����һ�λ�٣��Ƿ������');");
                }

                break;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    //�ж��Ƿ����ɾ��
    protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HR_LeaveInfo li = (HR_LeaveInfo)e.Row.DataItem;

            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnDelete.Font.Bold = false;
            btnDelete.Font.Size = new FontUnit(8);

            if (Convert.ToInt32(li.CreatedBy) == Convert.ToInt32(Session["uID"]))
            {
                btnDelete.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
            }
        }
    }

    protected void InsertMerrageInfo(int Type, string strStart, string strEnd, string strDay, string strLaborID, string strLaborNo, string strLaborName, string strMemo,int  uID)
    {
        if (hr.InsertLeaveInfo(Type, strStart, strEnd, strDay, strLaborID, strLaborNo, strLaborName, strMemo, uID))
        {
            ltlAlert.Text = "alert('��������ɹ���');";

            txtLaborNo.Text = "";
            lblLaborNameValue.Text = "";
            lblUID.Text = "";
            txtDays.Text = "";
            txtMemo.Text = "";
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('�������ݹ����г���'); ";
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //���
        if (ddlType.SelectedValue == "3" && txtLaborNo.Text.Trim().Length > 0)
        {
            string strLaborID = lblUID.Text.Trim();
            if (hr.SelectMerrageDaysInfo(strLaborID) > 0)
            {
                btnAddNew.Attributes.Add("onclick", "return confirm('��Ա���Ѿ�����һ�λ�٣��Ƿ������');");
            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int type = Convert.ToInt32(ddlType.SelectedValue.ToString());
        string strStart = txtStartDate.Text.Trim();
        string strEnd = txtEndDate.Text.Trim();
        string strUserNo = txtUserNo.Text.Trim();
        string strUserName = txtUserName.Text.Trim();
        int uID = Convert.ToInt32(Session["uID"]);
        int uRole = Convert.ToInt32(Session["uRole"]);

        this.ExportExcel(chk.dsnx()
                , "60^<b>����</b>~^80^<b>����</b>~^100^<b>��ʼ����</b>~^100^<b>��������</b>~^60^<b>����</b>~^80^<b>������</b>~^100^<b>��������</b>~^150^<b>��ע</b>~^<b>����</b>~^"
                , hr.SelectLeaveExcel(type, strStart, strEnd, strUserNo, strUserName, uID, uRole)
                , false);
    }
}
