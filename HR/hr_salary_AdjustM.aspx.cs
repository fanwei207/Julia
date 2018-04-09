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

public partial class hr_salary_AdjustM : BasePage
{
    HR hr_salary = new HR();
    adamClass adm = new adamClass();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("6052210", "计件工资调整(财务)");
            this.Security.Register("6052220", "计件工资调整(工资导出)");
        }

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropDeptDatabind();
            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();

            ListItem item;
            item = new ListItem("--", "0");
            dropWorkshop.Items.Add(item);
            dropWorktype.Items.Add(item);
            dropWorkgroup.Items.Add(item);

            lblUsername.Text = "";
            lblUserID.Text = "0";
            lblSalaryID.Text = "0";
            gvSalaryAdjust.DataBind();
        }

    }

    private void dropDeptDatabind()
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
    }

    protected void txtInputUser_TextChanged(object sender, EventArgs e)
    {
        if (txtInputUser.Text.Trim().Length > 0)
        {
            string  strCheckUser = hr_salary.SalaryUserCheck(txtInputUser.Text ,Convert.ToInt32(Session["Plantcode"]),Convert.ToInt32(txtYear.Text),Convert.ToInt32(dropMonth.SelectedValue ),Convert.ToInt32(Session["Uid"]));
            if (strCheckUser.Trim ().Length >0)
            {
                string[] strUser = strCheckUser.Split(',');
                lblUserID.Text =strUser[0];
                lblUsername.Text =strUser [1];
                lblSalaryID.Text = strUser[2];
              
                this.txtPercent.Focus ();
            }
            else
            {
                lblUserID.Text = "0";
                lblUsername.Text = "";
                lblSalaryID.Text = "";
                string str = @"<script language='javascript'> alert('此工号不存在工资结算中或没有权利操作'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "User", str);
                this.txtInputUser.Focus ();
            }
        }
    }

    /// <summary>
    /// 部门选择变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dropDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropWorkshopDatabind();

        dropWorkgroup.Items.Clear();
        dropWorktype.Items.Clear();

        //重置班组与工种
        ListItem item;
        item = new ListItem("--", "0");
        dropWorkgroup.Items.Add(item);
        dropWorktype.Items.Add(item);

        gvSalaryAdjust.DataBind();
    }

    protected void dropWorkshop_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropWorkgroupDatabind();
        dropWorktype.Items.Clear();

        //重置工种
        ListItem item;
        item = new ListItem("--", "0");
        dropWorktype.Items.Clear();
        dropWorktype.Items.Add(item);

        gvSalaryAdjust.DataBind();
    }


    protected void dropWorkgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropWorktypeDatabind();
        gvSalaryAdjust.DataBind();
    }


    private void dropWorkshopDatabind()
    {
        
        ListItem item;
        item = new ListItem("--", "0");
        dropWorkshop.Items.Clear();
        dropWorkshop.Items.Add(item);

        if (dropDept.SelectedIndex > 0)
        {
            DataTable dtWorkshop = HR.GetWorkShop(Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(dropDept.SelectedValue));
            if (dtWorkshop.Rows.Count > 0)
            {
                for (int i = 0; i < dtWorkshop.Rows.Count; i++)
                {
                    item = new ListItem(dtWorkshop.Rows[i].ItemArray[1].ToString(), dtWorkshop.Rows[i].ItemArray[0].ToString());
                    dropWorkshop.Items.Add(item);

                }
            }
        }
    }

    private void dropWorkgroupDatabind()
    {  
        ListItem item;
        item = new ListItem("--", "0");
        dropWorkgroup.Items.Clear();
        dropWorkgroup.Items.Add(item);

        if (dropWorkshop.SelectedIndex > 0)
        {
            DataTable dtWorkgroup = HR.GetWorkGroup(Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(dropWorkshop.SelectedValue));
            if (dtWorkgroup.Rows.Count > 0)
            {
                for (int i = 0; i < dtWorkgroup.Rows.Count; i++)
                {
                    item = new ListItem(dtWorkgroup.Rows[i].ItemArray[1].ToString(), dtWorkgroup.Rows[i].ItemArray[0].ToString());
                    dropWorkgroup.Items.Add(item);
                }
            }
        }
    }

    private void dropWorktypeDatabind()
    {   
        ListItem item;
        item = new ListItem("--", "0");
        dropWorktype.Items.Clear();
        dropWorktype.Items.Add(item);

        if (dropWorkgroup.SelectedIndex > 0)
        {
            DataTable dtWorktype = HR.GetWorkType(Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(dropWorkgroup.SelectedValue));
            if (dtWorktype.Rows.Count > 0)
            {
                for (int i = 0; i < dtWorktype.Rows.Count; i++)
                {
                    item = new ListItem(dtWorktype.Rows[i].ItemArray[1].ToString(), dtWorktype.Rows[i].ItemArray[0].ToString());
                    dropWorktype.Items.Add(item);
                }
            }
        }
    }
    //查询
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvSalaryAdjust.PageIndex = 0;
        gvSalaryAdjust.DataBind();
    }

    //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtPercent.Text.Trim().Length > 0)
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(txtPercent.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('调整比 只能为数字');";
                return;
            }
        }

        if (txtMoney.Text.Trim().Length > 0)
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(txtMoney.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('调整金额 只能为数字');";
                return;
            }
        }

        if (lblUserID.Text == "0" && (dropDept.SelectedIndex == 0 && dropWorkshop.SelectedIndex == 0 && dropWorkgroup.SelectedIndex == 0 && dropWorktype.SelectedIndex == 0))
        {
            string str =@"<script language='javascript'> alert('必须输入一个工号或选择部门,工段等'); </script>";
             Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Save", str);
            return ;
        }

        if (txtReason.Text.Trim().Length == 0)
        {
            string str = @"<script language='javascript'> alert('调整原因不能为空'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Save2", str);
            return;
        }

        if (txtPercent.Text.Trim().Length == 0 && txtMoney.Text.Trim().Length == 0)
        {
            string str = @"<script language='javascript'> alert('调整比和调整金额不能为空'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Save1", str);
            return;
        }

        HR_BaseInfo hr_bi = new HR().SelectHRBaseInfo(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue));
        if (hr_bi.isMinus)
        {
            if (Convert.ToDecimal(txtMoney.Text) > 0)
            {
                this.Alert("调整金额只能是负数！");
                return;
            }
        }


        int intadjust = hr_salary.finAdjust(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), 0);
        if (intadjust < 0)
        {
            string str = @"<script language='javascript'> alert('工资已被财务冻结，不能调整!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Finadjust", str);
            return;
        }

        int intFlag = hr_salary.AdjustSalarySave(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["Plantcode"]), Convert.ToInt32(dropDept.SelectedValue),
                                Convert.ToInt32(dropWorkshop.SelectedValue), Convert.ToInt32(dropWorkgroup.SelectedValue), Convert.ToInt32(dropWorktype.SelectedValue), lblUserID.Text,
                                Convert.ToDecimal((txtPercent.Text.Trim().Length == 0) ? "0" : txtPercent.Text), Convert.ToDecimal((txtMoney.Text.Trim().Length == 0) ? "0" : txtMoney.Text),
                                Convert.ToInt32(Session["Uid"]), Convert.ToInt32(lblSalaryID.Text),txtReason.Text.Trim());

        if (intFlag < 0)
        {
            string str = @"<script language='javascript'> alert('保存出错,请重新调整!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SaveMes", str);
        }
        else
        {
            string str = @"<script language='javascript'> alert('保存成功!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SaveMes1", str);

            lblSalaryID.Text = "0";
            lblUserID.Text = "0";
            lblUsername.Text = "";
            txtInputUser.Text = "";
            txtMoney.Text = "";
            txtPercent.Text = "";
        }
        gvSalaryAdjust.DataBind();
        gvSalaryAdjust.PageIndex = 0;
    }

    protected void btnFin_Click(object sender, EventArgs e)
    {
        if (!this.Security["6052210"].isValid)
        {
            Response.Redirect("~/public/denied.htm");
        }
        // the button can use only o
        int intadjust = hr_salary.finAdjust(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]),0);
        if (intadjust < 0)
        {
            string str = @"<script language='javascript'> alert('工资已被财务冻结，不能重复操作!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Finadjust", str);
            return;
        }
        else
        {
            int intMes = hr_salary.AdjustToFinacial(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["Uid"]));
            if (intMes < 0)
            {
                string str = @"<script language='javascript'> alert('操作出错,请重新操作!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Fin", str);
                return;
            }
        }
    }

    /// <summary>
    /// Recalculate tax in salary.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRecalculate_Click(object sender, EventArgs e)
    {
        if (!this.Security["6052210"].isValid)
        {
            Response.Redirect("~/public/denied.htm");
        }

        int intMes = hr_salary.AdjustPieceSalary(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]),Convert.ToInt32(Session["Uid"]));
        if (intMes < 0)
        {
            string str = @"<script language='javascript'> alert('重置税操作出错,请重新操作!'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Tax", str);
            return;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (!this.Security["6052220"].isValid)
        {
            Response.Redirect("~/public/denied.htm");
        }

        this.ExportExcel(adm.dsnx()
                , hr_salary.ExportFinSalary(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), 1, Convert.ToInt32(Session["PlantCode"]))
                , hr_salary.ExportFinSalary(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), 0, Convert.ToInt32(Session["PlantCode"]))
                , false);
    }
    protected void btnExportAdjust_Click(object sender, EventArgs e)
    {
        this.ExportExcel(adm.dsnx()
        , "<b>工号</b>~^<b>姓名</b>~^<b>部门</b>~^<b>工段</b>~^<b>班组</b>~^<b>工种</b>~^<b>职务</b>~^<b>出勤天</b>~^<b>出勤小时</b>~^<b>工费</b>~^<b>工单工资</b>~^<b>调整比率</b>~^<b>调整金额</b>~^<b>总调整额</b>~^<b>工资</b>~^<b>总应发金额</b>~^<b>调整原因</b>~^"
        , hr_salary.AdjustSalaryExport(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["Plantcode"]), Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropWorkshop.SelectedValue), Convert.ToInt32(dropWorkgroup.SelectedValue), Convert.ToInt32(dropWorktype.SelectedValue), txtUserNo.Text.Trim().Length == 0 ? "" : txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(Session["Uid"]))
        , false);
    }
}
