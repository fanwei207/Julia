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
using Microsoft.ApplicationBlocks.Data;

public partial class HR_hr_accfound_mstr : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            /// <summary>
            /// Bind dropdownlist for the department /status / Insurance
            /// <summary>
            dropDeptBind();
            dropStatusBind();
            dropInsBind();


            ///<summary>
            /// Bind accfound Data
            /// <summary>
            //BindData();
            txtYear.Text = DateTime.Now.Year.ToString();
            //dropMonth.SelectedItem.Text = DateTime.Now.Month.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();
            createSQL();
            #region Add By Amber Liu
            ddl_DepartmentBind();
            ddl_workshopBind();
            ddl_workgroupBind();
            ddl_roleBind();
            #endregion

        }
    }

    private void dropInsBind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropIns.Items.Add(item);

        DataTable dtDropIns = HR.GetInsurance();
        if (dtDropIns.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropIns.Rows.Count; i++)
            {
                item = new ListItem(dtDropIns.Rows[i].ItemArray[1].ToString(), dtDropIns.Rows[i].ItemArray[0].ToString());
                dropIns.Items.Add(item);
            }
        }
        dropDept.SelectedIndex = 0;
        dtDropIns = null;
    }


    private void dropStatusBind()
    {
        ListItem item;
        item = new ListItem("全部", "0");
        dropStatus.Items.Add(item);

        item = new ListItem("在职", "1");
        dropStatus.Items.Add(item);

        item = new ListItem("离职", "2");
        dropStatus.Items.Add(item);

        dropStatus.SelectedIndex = 1;
    }

    private void dropDeptBind()
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

    #region 录入时ddl的绑定(Add By Amber Liu)
    //绑定部门
    private void ddl_DepartmentBind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        ddl_Department.Items.Add(item);

        DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
        if (dtDropDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropDept.Rows.Count; i++)
            {
                item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                ddl_Department.Items.Add(item);
            }
        }
        ddl_Department.SelectedIndex = 0;
    }
    //绑定工段工序
    private void ddl_workshopBind()
    {
        ddl_workshop.Items.Clear();
        ddl_workshop.Items.Add(new ListItem("--","0"));
        ddl_workshop.SelectedIndex = 0;

        string Query = " SELECT w.id, w.name FROM Workshop w INNER JOIN departments d ON w.departmentID = d.departmentID " 
                  + " WHERE d.name=N'" + ddl_Department.SelectedItem.Text.Trim() + "' AND w.workshopID IS NULL Order by w.code  ";

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.Text, Query).Tables[0];
        for(int i = 0 ; i < dt.Rows.Count ; i++)
        {
            ddl_workshop.Items.Add(new ListItem(dt.Rows[i].ItemArray[1].ToString(),dt.Rows[i].ItemArray[0].ToString()));
        }
        dt.Reset();   
    }
    //绑定职位
    private void ddl_roleBind()
    {
        ddl_role.Items.Clear();
        ddl_role.Items.Add(new ListItem("--","0"));
        ddl_role.SelectedIndex = 0;    

        string strSQL = "" ;
        switch(ddl_RoleType.SelectedIndex)
        {
            case 0:
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = Isnull(roleProportion, 0) " 
                       + " From Roles where roleID>=100 and roleID<300 "
                       + " Order by roleID";
                break;
            case 1:
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = Isnull(roleProportion, 0) " 
                       + " From Roles where roleID>=300 and roleID<500 " 
                       + " Order by roleID";
                break;
            case 2:
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = Isnull(roleProportion, 0) " 
                       + " From Roles where roleID>=500 and roleID<1000 " 
                       + " Order by roleID";
                break;
            case 3:
                    strSQL = " SELECT ID,roleID,roleName,roleProportion = Isnull(roleProportion, 0) " 
                       + " From Roles where roleID>=1000 and roleID<5000 " 
                       + " Order by roleID";
                break;
        }

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.Text, strSQL).Tables[0];

        for(int i = 0 ; i < dt.Rows.Count ; i++)
        {
            ddl_role.Items.Add(new ListItem(dt.Rows[i].ItemArray[2].ToString(),dt.Rows[i].ItemArray[1].ToString()));
        }
        dt.Reset();    
    }
    //绑定班组
    private void ddl_workgroupBind()
    {
        ddl_workgroup.Items.Clear();
        ddl_workgroup.Items.Add(new ListItem("--","0"));
        ddl_workgroup.SelectedIndex = 0;    

        string Query = " SELECT w.id, w.name FROM Workshop w " 
                  + " WHERE w.workshopID=" + ddl_workshop.SelectedValue + " Order by w.code  ";

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.Text, Query).Tables[0];

        for(int i = 0 ; i < dt.Rows.Count ; i++)
        {
            ddl_workgroup.Items.Add(new ListItem(dt.Rows[i].ItemArray[1].ToString(),dt.Rows[i].ItemArray[0].ToString()));
        }
        dt.Reset();
    }
    #endregion


    /// <summary>
    /// gvInsurance数据绑定， 当是编辑状态时，对控件的属性控制
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void MyRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.RowState == DataControlRowState.Edit)
            if ((e.Row.RowState & DataControlRowState.Edit) != 0)
            {
                TextBox txtH = (TextBox)e.Row.Cells[6].Controls[0];
                TextBox txtM = (TextBox)e.Row.Cells[4].Controls[0];
                TextBox txtE = (TextBox)e.Row.Cells[5].Controls[0];
                TextBox txtR = (TextBox)e.Row.Cells[3].Controls[0];
                TextBox txtI = (TextBox)e.Row.Cells[7].Controls[0];

                txtH.Width = Unit.Pixel(80);
                txtM.Width = Unit.Pixel(80);
                txtE.Width = Unit.Pixel(80);
                txtR.Width = Unit.Pixel(80);
                txtI.Width = Unit.Pixel(80);

                if (e.Row.Cells[11].Text == "False")
                {
                    txtH.Enabled = false;
                }

                if (e.Row.Cells[12].Text == "False")
                {
                    txtM.Enabled = false;
                }

                if (e.Row.Cells[13].Text == "False")
                {

                    txtE.Enabled = false;
                }

                if (e.Row.Cells[14].Text == "False")
                {

                    txtR.Enabled = false;
                }

                if (e.Row.Cells[15].Text == "False")
                {

                    txtI.Enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvInsurance.DataBind();
        createSQL();
    }


    protected void MyRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            obdsInsurance.UpdateParameters.Add(new Parameter("hr_accfound_id", TypeCode.Int32, gvInsurance.DataKeys[e.RowIndex].Values[0].ToString()));


            string str = @"<script language='javascript'> alert('保存成功！'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Save", str);
            e.Cancel = false;

            //gvInsurance.DataBind();
        }
        catch
        {
            e.Cancel = true;
            string str = @"<script language='javascript'> alert('保存失败！'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Erro", str);

        }
    }
    protected void MyRowEditing(object sender, GridViewEditEventArgs e)
    {
        this.gvInsurance.EditIndex = e.NewEditIndex;
        this.gvInsurance.DataBind();
    }

    protected void MyRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int intAccfound = Convert.ToInt32(gvInsurance.DataKeys[e.RowIndex].Value);
            HR hr = new HR();
            int intFlag = hr.DeleteIns(intAccfound);
            this.gvInsurance.DataBind();
        }
        catch
        {
            string str = @"<script language='javascript'> alert('删除失败！'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "DeteErro", str);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtHfound.Text.Trim().Length > 0)
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(txtHfound.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('住房公积金 只能是数字！');";
                return;
            }
        }

        if (txtMfound.Text.Trim().Length > 0)
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(txtMfound.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('医疗保险金 只能是数字！');";
                return;
            }
        }

        if (txtEfound.Text.Trim().Length > 0)
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(txtEfound.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('失业保险金 只能是数字！');";
                return;
            }
        }

        if (txtRfound.Text.Trim().Length > 0)
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(txtRfound.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('养老保险金 只能是数字！');";
                return;
            }
        }

        if (txtInjure.Text.Trim().Length > 0)
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(txtInjure.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('工伤 只能是数字！');";
                return;
            }
        }

        try
        {
            HR hr = new HR();
            string strDate = txtYear.Text + "-" + dropMonth.SelectedValue + "-01";

            int intadjust = hr.finAdjust(Convert.ToInt32(txtYear.Text), Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(Session["PlantCode"]), 0);
            if (intadjust < 0)
            {
                string str = @"<script language='javascript'> alert('工资已被财务冻结，不能操作!'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Finadjust", str);
                return;
            }

            int result = hr.SaveAndUpdateIns((lblUid.Text.Trim().Length == 0) ? 0 : Convert.ToInt32(lblUid.Text), txtInputUser.Text, lblInputName.Text,
                (txtHfound.Text.Trim().Length == 0) ? 0 : Convert.ToDecimal(txtHfound.Text),
                (txtMfound.Text.Trim().Length == 0) ? 0 : Convert.ToDecimal(txtMfound.Text),
                (txtEfound.Text.Trim().Length == 0) ? 0 : Convert.ToDecimal(txtEfound.Text),
                (txtRfound.Text.Trim().Length == 0) ? 0 : Convert.ToDecimal(txtRfound.Text),
                (txtInjure.Text.Trim().Length == 0) ? 0 : Convert.ToDecimal(txtInjure.Text),
                 Convert.ToInt32(Session["uID"]), strDate
                 , ddl_role.SelectedValue.ToString(), ddl_Department.SelectedValue.ToString(), ddl_workshop.SelectedValue.ToString()
                 , ddl_workgroup.SelectedValue.ToString(), ddl_RoleType.SelectedValue.ToString() ,Session["PlantCode"].ToString());

            txtInputUser.Text = "";
            lblUid.Text = "";
            lblInputName.Text = "";
            txtHfound.Text = "";
            txtInjure.Text = "";
            txtMfound.Text = "";
            txtRfound.Text = "";
            txtEfound.Text = "";
            txtInputUser.Focus();
            if(result == 1)
            {
                ltlAlert.Text = "alert('保存成功！');";
                isInputShow.Value = "none";
                this.gvInsurance.DataBind();
            }                
            else if( result == 0)
                ltlAlert.Text = "alert('不存在这样类型的员工，请先维护员工信息！');";
            else
                ltlAlert.Text = "alert('保存失败！');";
            
        }
        catch
        {
            ltlAlert.Text = "alert('保存失败！');";
        }
    }



    protected void txtInputUser_TextChanged(object sender, EventArgs e)
    {
        HR hr = new HR();
        string strUserName = hr.SelectUserName(txtInputUser.Text, Convert.ToInt32(Session["PlantCode"]));
        string str;
        switch (strUserName)
        {
            case "":
                lblInputName.Text = "";
                lblUid.Text = "";
                str = @"<script language='javascript'> alert('工号不存在！'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "User", str);
                this.txtInputUser.Focus();
                break;

            case "此员工属于离职员工！":

                lblInputName.Text = "";
                lblUid.Text = "";
                str = @"<script language='javascript'> alert('" + strUserName + "'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "User1", str);
                this.txtInputUser.Focus();
                break;

            default:
                string[] struser = strUserName.Split(',');
                this.txtHfound.Focus();
                lblInputName.Text = struser[1];
                lblUid.Text = struser[0];
                break;
        }

    }

    protected string createSQL()
    {
        HR hr = new HR();
        string str = hr.ExportIns(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(), Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropStatus.SelectedValue), Convert.ToInt32(dropIns.SelectedValue));
        return str;
    }

    protected void ButExcel_Click(object sender, EventArgs e)
    {
        string EXTitle = "<b>工号</b>~^<b>姓名</b>~^<b>部门</b>~^<b>养老保险金</b>~^<b>医疗保险金</b>~^<b>失业保险金</b>~^<b>住房公积金</b>~^<b>工伤</b>~^<b>离职日期</b>~^<b>合计</b>~^<b>序号</b>~^<b>用工性质</b>~^<b>保险类型</b>~^<b>户籍地</b>~^";
        string ExSql = createSQL();
        this.ExportExcel(adam.dsnx(), EXTitle, ExSql, false);
    }
    protected void gvInsurance_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #region 录入时的联动(Add By Amber Liu)
    /// <summary>
    /// 部门变化时-->工段/工序跟着变化-->班组跟着变化
    /// 角色类变化时-->角色变化（职务/岗位）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddl_Department_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddl_Department.SelectedValue != "0" )
            ddl_workshopBind();
        else
        {
            ddl_workshop.Items.Clear();
            ddl_workshop.Items.Add(new ListItem("--","0"));

            ddl_workshop.SelectedIndex = 0;
        }
        ddl_workgroup.Items.Clear();
        ddl_workgroup.Items.Add(new ListItem("--","0"));

        ddl_workgroup.SelectedIndex = 0;
    }
    protected void ddl_RoleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_roleBind();
    }
    protected void ddl_workshop_SelectedIndexChanged(object sender, EventArgs e)
    {        
        if( ddl_workshop.SelectedValue != "0" )
        {
            ddl_workgroupBind();
        }
        else
        {
            ddl_workgroup.Items.Clear();
            ddl_workgroup.Items.Add(new ListItem("--","0"));
            ddl_workgroup.SelectedIndex = 0;
        }
    }
    #endregion
}
