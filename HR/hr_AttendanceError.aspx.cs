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
using Wage;
using adamFuncs;

public partial class hr_AttendanceError : System.Web.UI.Page
{
    HR hr = new HR();
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        {
            int nRet = chk.securityCheck(Convert.ToString(Session["uID"]), Convert.ToString(Session["uRole"]), Convert.ToString(Session["orgID"]), "14010320", false, false);
            if (nRet <= 0)
            {
                Response.Redirect("~/public/Message.aspx?type=" + nRet.ToString(), true);
            }

            txtYear.Text = DateTime.Now.Year.ToString();

            dropMonth.SelectedValue = (DateTime.Now.Month - 1).ToString();

            LoadDept();

            BindData();
        }
    }

    protected void BindData()
    {
        //�������
        string strYear = txtYear.Text.Trim();
        string strMonth = dropMonth.SelectedItem.Text.Trim();
        string strUserNo = txtUserNo.Text.Trim();
        string strPlant = Convert.ToString(Session["PlantCode"]);
        string strDept = ddlDept.SelectedValue.ToString().Trim();
        int intSelect = radDoor.Checked ? 1 : 0;

        gvAttendance.DataSource = hr.SelectAttendanceError(strYear, strMonth, strUserNo, strDept, strPlant, intSelect);
        gvAttendance.DataBind();

        Session["EXHeader"] = "";
        Session["EXTitle"] = "100^<b>Ա������</b>~^100^<b>Ա������</b>~^80^<b>���ڱ��</b>~^200^<b>��������</b>~^80^<b>��������</b>~^100^<b>��������</b>~^300^<b>������Ϣ</b>~^";
        Session["EXSQL"] = hr.SelectAttendanceErrorExcel(strYear, strMonth, strUserNo, strDept, strPlant, intSelect);
        
    }

    /// <summary>
    /// ���ݲ���һҳҲ��ʾGridView��ҳ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAttendance_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAttendance.PageIndex = e.NewPageIndex;
        BindData();
    }
    
     protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void LoadDept()
    {
        //�������
        string strPlant = Convert.ToString(Session["plantCode"]);

        ddlDept.Items.Clear();
        ListItem item;
        item = new ListItem("--", "0");
        ddlDept.Items.Add(item);

        DataTable dtbDept = hr.SelectAttendanceErrorDept(strPlant);
        if (dtbDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtbDept.Rows.Count; i++)
            {
                item = new ListItem(dtbDept.Rows[i].ItemArray[1].ToString(), dtbDept.Rows[i].ItemArray[0].ToString());
                ddlDept.Items.Add(item);
            }
        }
        ddlDept.SelectedIndex = 0;
    }
}
