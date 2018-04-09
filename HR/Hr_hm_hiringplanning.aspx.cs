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

public partial class HR_Hr_hm_hiringplanning : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    HR hr = new HR();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropDepartmentBind();
            droplistBind();

            dropYear.SelectedValue = DateTime.Now.Year.ToString();
            dropYInput.SelectedValue = DateTime.Now.Year.ToString();
            dropExporY.SelectedValue = DateTime.Now.Year.ToString();

            dropMonth.SelectedValue = DateTime.Now.Month.ToString();
            dropMInput.SelectedValue = DateTime.Now.Month.ToString();
            dropExporM.SelectedValue = DateTime.Now.Month.ToString();
            dropExporMax.SelectedValue = DateTime.Now.Month.ToString();
            //btnSave.Attributes.Add("onclick", "javascript:NotDoubleClick(this);");

            BindData();
           
        }
    }

    /// <summary>
    /// Department Information Bind
    /// </summary>
    private void dropDepartmentBind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropDept.Items.Add(item);
        dropDeptInput.Items.Add(item);

        DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
        if (dtDropDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropDept.Rows.Count; i++)
            {
                item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                dropDept.Items.Add(item);
                dropDeptInput.Items.Add(item);
            }
        }
        dropDept.SelectedIndex = 0;
        dropDeptInput.SelectedIndex = 0;
    }
    
    /// <summary>
    /// Year dropdownList bind 
    /// </summary>
    private void droplistBind()
    {
        ListItem item;

        int intYear = DateTime.Now.Year;

        for (int i = intYear - 10; i < intYear + 10; i++)
        {
            item = new ListItem(i.ToString(), i.ToString());

            dropYear.Items.Add(item);
            dropYInput.Items.Add(item);
            dropExporY.Items.Add(item);
        }
    }

    protected void BindData()
    {
        gvHiring.DataSource = hr.SelectDepartmentPlan(Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropYear.SelectedValue), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["Plantcode"])).Tables[0];
        gvHiring.DataBind();
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvHiring.PageIndex = 0;  
        BindData();
    }


    /// <summary>
    /// Save data into Database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (hr.InsertHiringPlaning(Convert.ToInt32(dropDeptInput.SelectedValue),Convert .ToInt32 (dropYInput.SelectedValue),Convert.ToInt32(dropMInput.SelectedValue),Convert.ToInt32(Session["PlantCode"]),Convert.ToDecimal(txtMonthup.Text.Trim()),Convert.ToDecimal(txtMonthdown.Text.Trim()),Convert.ToInt32(Session["Uid"])) > 0)
        {
            string str = @"<script language='javascript'> alert('保存成功！'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Input", str);
            txtMonthup.Text = "";
            txtMonthdown.Text = "";
            dropDeptInput.SelectedIndex = 0;
            BindData();
        }
        else
        {
            string str = @"<script language='javascript'> alert('保存失败，请重新操作！'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error1", str);
        }
    }

    /// <summary>
    /// Delete hiring Planing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvhiring_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int nRet = 0;
        nRet = chk.securityCheck(Convert.ToString(Session["uID"]), Convert.ToString(Session["uRole"]), Convert.ToString(Session["orgID"]), "14020811", true, false);
        if (nRet <= 0)
        {
            string str = @"<script language='javascript'> alert('无权限操作！'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Limited", str);
        }

        if (hr.DeleteHiringData(Convert.ToInt32(gvHiring.DataKeys[e.RowIndex].Value)) > 0)
        {
            string str = @"<script language='javascript'> alert('删除成功！'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Del", str);

            BindData();
        }
        else
        {
            string str = @"<script language='javascript'> alert('删除失败！'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error2", str);
        }

    }



    protected void gvHiring_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHiring.PageIndex = e.NewPageIndex;
        BindData();
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('/hr/ExportHrHiringPlan.aspx?ye=" +dropExporY.SelectedValue+ "&m1="+ dropExporM.SelectedValue +"&m2="+ dropExporMax.SelectedValue +"&rm=" + DateTime.Now.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();";
    }
}
