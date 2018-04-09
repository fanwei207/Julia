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
using WOrder;
using System.Text.RegularExpressions;

public partial class HR_hr_allowanceAW : BasePage
{
     HR hr_salary = new HR();
    adamClass adam = new adamClass();
    WorkOrder wd = new WorkOrder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["plantCode"].ToString() != "1")
            {
                btnCalculateAttendAward.Visible = false;
            }
            dropDepartmentBind();

            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();

            dropType.SelectedIndex = 0;
            gvAllowance.DataBind();
            
        }
    }


    private void dropDepartmentBind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropDept.Items.Add(item);

        DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
        if (dtDropDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropDept.Rows.Count; i++)
            {
                item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                dropDept.Items.Add(item);
            }
        }
        dropDept.SelectedIndex = 0;
    }
 
    protected void btnSave_Click(object sender, EventArgs e)
    {

        string strYear = txtYear.Text.Trim();
        string strMonth = dropMonth.SelectedValue.Trim();
        string strUserNo = txtUserNo.Text.Trim();
        string strAmount = txtAmount.Text.Trim();

        if (strYear == string.Empty)
        {
            string str = @"<script language='javascript'> alert('���²���Ϊ��'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Year", str);
            return;
        }
        try
        {
            DateTime dt = Convert.ToDateTime(strYear + "-1-1");
        }
        catch
        {
            string str = @"<script language='javascript'> alert('���¸�ʽ���ԡ����磺2009'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Date", str);
            return;
        }

        if (strUserNo == string.Empty)
        {
            string str = @"<script language='javascript'> alert('���Ų���Ϊ��'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UserNo", str);
            return;
        }

        if (strAmount == string.Empty)
        {
            string str = @"<script language='javascript'> alert('����Ϊ��'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Amount", str);
            return;
        }

        try
        {
            Decimal dec = Convert.ToDecimal(strAmount);
        }
        catch
        {
            string str = @"<script language='javascript'> alert('���ĸ�ʽ����ȷ'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "AmountStyle", str);
            return;
        }

        int intRet = wd.SaveAW(txtUserNo.Text.Trim(),txtCom.Text.Trim(),Convert.ToInt32(txtYear.Text.Trim()),Convert.ToInt32(dropMonth.SelectedValue),Convert.ToDecimal(txtAmount.Text.Trim()),Convert.ToInt32(Session["plantcode"]),Convert.ToInt32(dropType.SelectedValue ));
        if (intRet < 0)
        {
            string str = @"<script language='javascript'> alert('����ʧ�ܣ������²���!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", str);
            this.txtUserNo.Focus();
            return;
        }
        else
        {
            string str = @"<script language='javascript'> alert('����ɹ�!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Save", str);
  
            txtUserNo.Text = "";
            txtName.Text = "";
            txtCom.Text = "";
            txtAmount.Text = "";
        }

        gvAllowance.PageIndex = 0;
        gvAllowance.DataBind();
    }


    protected void txtUserNo_TextChanged(object sender, EventArgs e)
    {
        string strDate = txtYear.Text.Trim() + "-" + dropMonth.SelectedValue + "-01";
        string strUserNo = txtUserNo.Text.Trim();
        string strTemp = Session["temp"].ToString().Trim();
        string plantid = Session["PlantCode"].ToString().Trim();

        if (strUserNo != string.Empty)
        {
            string strUserName = hr_salary.GetUserNameByNo(plantid, strTemp, strUserNo, strDate);

            if (strUserName == "DB-Opt-Err")
            {
                string str = @"<script language='javascript'> alert('���ݿ��������!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "adjust1", str);
                return;
            }
            else if (strUserName == "UserHadLeaved")
            {
                string str = @"<script language='javascript'> alert('��Ա���Ѿ���ְ!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "adjust2", str);
                return;
            }

            else if (strUserName == "NoThisUser")
            {
                string str = @"<script language='javascript'> alert('��Ա��������!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "adjust3", str);
                return;
            }
            else
            {
                if (strUserName == "Leaved-User")
                {
                    string str = @"<script language='javascript'> alert('��Ա��������ְԱ��!'); </script>";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "adjust4", str);           
                  
                }
                txtName.Text = strUserName;
                this.txtAmount.Focus();
            }
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {   
        gvAllowance.PageIndex = 0;
        gvAllowance.DataBind();
    }

    protected void MyRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int intAccfound = Convert.ToInt32(gvAllowance.DataKeys[e.RowIndex].Value);
          
            int intFlag = wd.DelAW(Convert.ToInt32(Session["plantcode"]), intAccfound,Convert.ToInt32(dropType.SelectedValue));
            if (intFlag < 0)
            {
                ltlAlert.Text = "alert('ɾ��ʧ�ܣ�');";
                return;
            }
            this.gvAllowance.DataBind();
        }
        catch
        {
            ltlAlert.Text = "alert('ɾ��ʧ�ܣ�');";
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.ExportExcel(adam.dsn0()
                , wd.ExportAW(txtUserNo.Text.Trim(), Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropType.SelectedValue), Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 1, Convert.ToInt32(Session["plantcode"]))
                , wd.ExportAW(txtUserNo.Text.Trim(), Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropType.SelectedValue), Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), 0, Convert.ToInt32(Session["plantcode"]))
                , false);
    }
    protected void btnCalculateAttendAward_Click(object sender, EventArgs e)
    {
        string year = @"^([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})$";
        if (txtYear.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('��ݲ���Ϊ�գ�');";
            return;
        }
        else
        {
            if (!new Regex(year).IsMatch(txtYear.Text.Trim()))
            {
                ltlAlert.Text = "alert('��ݲ��Ϸ���');";
                return;
            }
        }
        if (!wd.InsertFullAttendanceAW(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["plantcode"])))
        {
            ltlAlert.Text = "alert('ȫ�ڽ�����ʧ�ܣ������¼��㣡');";
            return;
        }
    }
}
