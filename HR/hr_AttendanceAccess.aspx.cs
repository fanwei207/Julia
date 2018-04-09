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

public partial class hr_AttendanceAccess : BasePage
{
    HR hr = new HR();
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        //�������
        string strSensor = txtSensor.Text.Trim();
        string strPlant = Convert.ToString(Session["PlantCode"]);

        gvAccess.DataSource = hr.SelectAttendanceAccess(strSensor, strPlant);
        gvAccess.DataBind();
    }

    /// <summary>
    /// ���ݲ���һҳҲ��ʾGridView��ҳ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAccess_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvAccess_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAccess.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvAccess_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //�������
        string strSensor = gvAccess.DataKeys[e.RowIndex].Value.ToString();
        string strPlant = Convert.ToString(Session["PlantCode"]);

        if (hr.DeleteAttendanceAccess(strSensor, strPlant))
        {
            ltlAlert.Text = "alert('ɾ���ɹ�!'); window.location.href='/HR/hr_AttendanceAccess.aspx?rm=" + DateTime.Now.ToString() + "';";
        }
        else
        {
            ltlAlert.Text = "alert('ɾ�����ݹ��̳���'); ";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtSensor.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('�Ž��豸�� ����Ϊ�գ�'); ";
            return;
        }
        
        //�������
        string strSensor = txtSensor.Text.Trim();
        string strPlant = Convert.ToString(Session["PlantCode"]);

        if (hr.InsertAttendanceAccess(strSensor, strPlant))
        {
            ltlAlert.Text = "alert('���ӳɹ���')";
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('�������ݹ��̳���'); ";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        txtSensor.Text = "";
    }
}
