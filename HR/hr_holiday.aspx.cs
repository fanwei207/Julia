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

public partial class hr_holiday : BasePage
{
    adamClass chk = new adamClass();
    HR hr = new HR();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["y"] != null)
            {
                txtYear.Text = Convert.ToString(Request.QueryString["y"]);
            }
            else
            {
                txtYear.Text = DateTime.Now.Year.ToString();
            }

            if (Request.QueryString["m"] != null)
            {
                ddlMonth.SelectedValue = Convert.ToString(Request.QueryString["m"]);
            }
            else
            {
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            }
            BindData();
        }
    }

    public void BindData()
    {
        //�������
        int Year = Convert.ToInt32(txtYear.Text.Trim());
        int Month = Convert.ToInt32(ddlMonth.SelectedValue);
        
        gvHoliday.DataSource = hr.SelectHolidayList(Year, Month);
        gvHoliday.DataBind();
    }

    protected void btn_AddNew_Click(object sender, EventArgs e)
    {
        if (txtHoliday.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('�������� ����Ϊ�գ�');";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtHoliday.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('�������� ��ʽ����ȷ��');";
                return;
            }
        }
        
        //�������
        string strHoliday = txtHoliday.Text.Trim();
        int uID = Convert.ToInt32(Session["uID"]);
        string strParam = string.Empty;
        string strYear = txtYear.Text.Trim();
        string strMonth = ddlMonth.SelectedItem.Text;

        if (hr.InsertHoliday(strHoliday, uID))
        {
            strParam = "?y=" + strYear + "&m=" + strMonth + "&rm=" + DateTime.Now.ToString();
            ltlAlert.Text = "alert('������Ϣ�����ɹ���'); window.location.href='" + Request.ServerVariables["Http_Referer"].Substring(0, Request.ServerVariables["Http_Referer"].ToString().IndexOf("?")) + strParam + "';";
            txtHoliday.Text = "";
        }
        else
        {
            ltlAlert.Text = "alert('�������ݹ����г���');";
        }
    }
    
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    /// <summary>
    /// ����ɾ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvHoliday_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strParam = string.Empty;
        string strYear = txtYear.Text.Trim();
        string strMonth = ddlMonth.SelectedItem.Text;

        if (hr.DeleteHoliday(Convert.ToInt32(gvHoliday.DataKeys[e.RowIndex].Value.ToString())))
        {
            strParam = "?y=" + strYear + "&m=" + strMonth + "&rm=" + DateTime.Now.ToString();
            ltlAlert.Text = "alert('ɾ���������ݳɹ���'); window.location.href='" + Request.ServerVariables["Http_Referer"].Substring(0, Request.ServerVariables["Http_Referer"].ToString().IndexOf("?")) + strParam + "';";
        }
        else
        {
            ltlAlert.Text = "alert('ɾ�����ݹ����г���');";
        }
    }
}
