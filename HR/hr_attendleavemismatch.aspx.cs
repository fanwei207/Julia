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

public partial class HR_hr_attendleavemismatch : BasePage
{
    adamClass chk = new adamClass();
    HR hr = new HR();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01";
            txtEndDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            LoadDepartment();

            BindData();
        }
    }

    protected void LoadDepartment()
    {
        //�������
        int PlantID = Convert.ToInt32(Session["PlantCode"]);

        ListItem li = new ListItem();
        ddlDepartment.Items.Insert(0, new ListItem("--", "0"));

        DataTable dt = HR.GetDepartment(PlantID) ;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            li = new ListItem();
            li.Text = dt.Rows[i].ItemArray[1].ToString();
            li.Value = dt.Rows[i].ItemArray[0].ToString();
            ddlDepartment.Items.Add(li);
        }
        dt.Reset();
        dt = null;        
    }

    protected void BindData()
    {
        //�������
        int departmentID = Convert.ToInt32(ddlDepartment.SelectedValue.ToString());
        string strStart = txtStartDate.Text.Trim();
        string strEnd = txtEndDate.Text.Trim();
        string strUserNo = txtUserNo.Text.Trim();
        string strUserName = txtUserName.Text.Trim();

        gvMatch.DataSource = hr.SelectAttendLeaveMisMatch(strStart, strEnd, strUserNo, strUserName, departmentID);
        gvMatch.DataBind();
    }

    protected void gvMatch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMatch.PageIndex = e.NewPageIndex;
        BindData();
    }

    /// <summary>
    /// ���ݲ���һҳҲ��ʾGridView��ҳ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMatch_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView) sender;
        GridViewRow gvr = (GridViewRow) gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int departmentID = Convert.ToInt32(ddlDepartment.SelectedValue.ToString());
        string strStart = txtStartDate.Text.Trim();
        string strEnd = txtEndDate.Text.Trim();
        string strUserNo = txtUserNo.Text.Trim();
        string strUserName = txtUserName.Text.Trim();

        this.ExportExcel(chk.dsnx()
                , "100^<b>����</b>~^80^<b>����</b>~^80^<b>��������</b>~^150^<b>����</b>~^150^<b>����</b>~^100^<b>��������</b>~^100^<b>�������</b>~^"
                , hr.SelectAttendLeaveMisMatchExcel(strStart, strEnd, strUserNo, strUserName, departmentID)
            , false);
    }
}
