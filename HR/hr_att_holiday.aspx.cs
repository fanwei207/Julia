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

public partial class HR_hr_att_holiday : BasePage
{
    HR hr_salary = new HR();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropDepartmentBind();

            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();

            lblName.Text = "";
            lbluserID.Text = "0";
        }
    }

    private void dropDepartmentBind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropDepartment.Items.Add(item);

        DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
        if (dtDropDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropDept.Rows.Count; i++)
            {
                item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                dropDepartment.Items.Add(item);
            }
        }
        dropDepartment.SelectedIndex = 0;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvHoliday.PageIndex = 0;
        gvHoliday.DataBind();
    }


    protected void txtInputUser_TextChanged(object sender, EventArgs e)
    {
        string strUserName = hr_salary.SelectUserName(txtInputUser.Text, Convert.ToInt32(Session["PlantCode"]));
        string str;
        switch (strUserName)
        {
            case "":
                lblName.Text = "";
                lbluserID.Text = "";
                str = @"<script language='javascript'> alert('工号不存在！'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "User", str);
                this.txtInputUser.Focus();
                break;

            case "此员工属于离职员工！":

                lblName.Text = "";
                lbluserID.Text = "";
                str = @"<script language='javascript'> alert('" + strUserName + "'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "User1", str);
                this.txtInputUser.Focus();
                break;

            default:
                string[] struser = strUserName.Split(',');
                lblName.Text = struser[1];
                lbluserID.Text = struser[0];
                this.txtWorkHours.Focus();
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal _cost = 0;

        if (txtCost.Text != string.Empty)
        {
            try
            {
                _cost = Convert.ToDecimal(txtCost.Text);

                if (_cost <= 0)
                {
                    ltlAlert.Text = "alert('考勤金额应该大于零');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('考勤金额必须是数字');";
                return;
            }
        }

        int intflag;
        intflag = 0;
        //Judge the current data
        intflag = hr_salary.SaveHolidayData(Convert.ToInt32(lbluserID.Text), txtInputUser.Text, lblName.Text, txtholidaydate.Text, Convert.ToDecimal(txtWorkHours.Text), _cost, Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["Uid"]), 1);
        if (intflag > 0)
        {
            string str = @"<script language='javascript'> alert('该员工记录已存在！'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", str);
            return;
        }
        //Save data
        intflag = hr_salary.SaveHolidayData(Convert.ToInt32(lbluserID.Text), txtInputUser.Text, lblName.Text, txtholidaydate.Text, Convert.ToDecimal(txtWorkHours.Text), _cost, Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["Uid"]), 0);
        if (intflag < 0)
        {
            string str = @"<script language='javascript'> alert('保存出错，请重新操作！'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Erro", str);
        }
        else
        {
            string str = @"<script language='javascript'> alert('保存成功！'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Input", str);
            lblName.Text = "";
            lbluserID.Text = "";
            txtholidaydate.Text = "";
            txtWorkHours.Text = "";
            txtInputUser.Text = "";
            this.txtInputUser.Focus();

            gvHoliday.DataBind();
        }
    }

    protected void MyRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int intAccfound = Convert.ToInt32(gvHoliday.DataKeys[e.RowIndex].Value);
            HR hr = new HR();
            int intFlag = hr.DelHolidayAtt(Convert.ToInt32(Session["plantcode"]),intAccfound);
            if (intFlag < 0)
            {
                string str = @"<script language='javascript'> alert('删除失败！'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "DeteErros", str);
                return;
            }
            this.gvHoliday.DataBind();
        }
        catch
        {
            string str = @"<script language='javascript'> alert('删除失败！'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "DeteErro", str);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.ExportExcel(adam.dsn0()
            , hr_salary.HolidayAtt(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropDepartment.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(Session["PlantCode"]), 1, Convert.ToInt32(Session["uid"]))
            , hr_salary.HolidayAtt(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropDepartment.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(Session["PlantCode"]), 0, Convert.ToInt32(Session["uid"]))
            , false);
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        Response.Redirect("/HR/hr_att_holidayImport.aspx");
    }
}
