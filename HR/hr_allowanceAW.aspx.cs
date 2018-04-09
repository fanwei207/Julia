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
            string str = @"<script language='javascript'> alert('年月不能为空'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Year", str);
            return;
        }
        try
        {
            DateTime dt = Convert.ToDateTime(strYear + "-1-1");
        }
        catch
        {
            string str = @"<script language='javascript'> alert('年月格式不对。例如：2009'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Date", str);
            return;
        }

        if (strUserNo == string.Empty)
        {
            string str = @"<script language='javascript'> alert('工号不能为空'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UserNo", str);
            return;
        }

        if (strAmount == string.Empty)
        {
            string str = @"<script language='javascript'> alert('金额不能为空'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Amount", str);
            return;
        }

        try
        {
            Decimal dec = Convert.ToDecimal(strAmount);
        }
        catch
        {
            string str = @"<script language='javascript'> alert('金额的格式不正确'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "AmountStyle", str);
            return;
        }

        int intRet = wd.SaveAW(txtUserNo.Text.Trim(),txtCom.Text.Trim(),Convert.ToInt32(txtYear.Text.Trim()),Convert.ToInt32(dropMonth.SelectedValue),Convert.ToDecimal(txtAmount.Text.Trim()),Convert.ToInt32(Session["plantcode"]),Convert.ToInt32(dropType.SelectedValue ));
        if (intRet < 0)
        {
            string str = @"<script language='javascript'> alert('保存失败，请重新操作!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", str);
            this.txtUserNo.Focus();
            return;
        }
        else
        {
            string str = @"<script language='javascript'> alert('保存成功!'); </script>";
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
                string str = @"<script language='javascript'> alert('数据库操作错误!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "adjust1", str);
                return;
            }
            else if (strUserName == "UserHadLeaved")
            {
                string str = @"<script language='javascript'> alert('此员工已经离职!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "adjust2", str);
                return;
            }

            else if (strUserName == "NoThisUser")
            {
                string str = @"<script language='javascript'> alert('此员工不存在!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "adjust3", str);
                return;
            }
            else
            {
                if (strUserName == "Leaved-User")
                {
                    string str = @"<script language='javascript'> alert('此员工属于离职员工!'); </script>";
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
                ltlAlert.Text = "alert('删除失败！');";
                return;
            }
            this.gvAllowance.DataBind();
        }
        catch
        {
            ltlAlert.Text = "alert('删除失败！');";
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
            ltlAlert.Text = "alert('年份不能为空！');";
            return;
        }
        else
        {
            if (!new Regex(year).IsMatch(txtYear.Text.Trim()))
            {
                ltlAlert.Text = "alert('年份不合法！');";
                return;
            }
        }
        if (!wd.InsertFullAttendanceAW(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["plantcode"])))
        {
            ltlAlert.Text = "alert('全勤奖计算失败，请重新计算！');";
            return;
        }
    }
}
