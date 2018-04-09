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
using WOrder;
using adamFuncs;
using Wage;

public partial class Wo2_workhoursdisplay2 : BasePage
{
    adamClass adam = new adamClass();
    WorkOrder wd = new WorkOrder();
    HR hr_salary = new HR();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           //txtStart.Text = string.Format("{0:d}", DateTime.Now.AddDays(-DateTime.Now.Day + 1));
            txtStart.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-7));
            txtEnd.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            
            dropDeptBind();
        }
    }

    private void CheckDateFormat()
    {
        try
        {
           DateTime timeStart =  Convert.ToDateTime(txtStart.Text.ToString().Trim());
           DateTime timeEnd = Convert.ToDateTime(txtEnd.Text.ToString().Trim());
        }
        catch
        {
            ltlAlert.Text = "alert(' 日期格式不对，请确认！ ')";
            return;
        }
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CheckDateFormat();
        gvUsers.PageIndex = 0;
        gvUserBind();
    }

    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUsers.PageIndex = e.NewPageIndex;
        gvUserBind();
    }

    private void gvUserBind()
    {
        try
        {
            CheckDateFormat();
            DataTable dtUser = wd.WorkHourSelect(txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["Plantcode"]), txtCenter.Text, txtSite.Text, txtWorkOrder.Text, txtID.Text, txtUserNo.Text, txtUserName.Text, chkClose.Checked, Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(Session["uRole"]),Convert.ToString(Session["Uid"]));
            gvUsers.DataSource = dtUser;
            gvUsers.DataBind();
            dtUser.Clear();
        }
        catch
        {

        }
    }

    protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        bool blWo2,blWo ;
        blWo2=false ;
        blWo =false ;

        if (e.CommandName == "1")
            blWo2 = true ;
        if (e.CommandName == "2")
             blWo2 = true ;
        if (e.CommandName == "3")
             blWo2 = true ;

        if (e.CommandName == "4")
           blWo =true ;
        if (e.CommandName == "5")
            blWo =true ;
        if (e.CommandName == "6")
            blWo =true ;

        CheckDateFormat();

        if (blWo2)
        {
            Session["EXSQL1"] = wd.Wo2Export(gvUsers.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString(), txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["Plantcode"]), txtCenter.Text, txtSite.Text, txtWorkOrder.Text, txtID.Text, chkClose.Checked, Convert.ToInt32(e.CommandName), Convert.ToInt32(Session["uRole"]), Convert.ToString(Session["Uid"]), 0);
            Session["EXTitle1"] = wd.Wo2Export(gvUsers.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString(), txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["Plantcode"]), txtCenter.Text, txtSite.Text, txtWorkOrder.Text, txtID.Text, chkClose.Checked, Convert.ToInt32(e.CommandName), Convert.ToInt32(Session["uRole"]), Convert.ToString(Session["Uid"]), 1);
            Session["EXHeader1"] = "";
            ltlAlert.Text = "window.open('/public/exportExcel1.aspx','_blank')";
        }
        else
        {
            if (blWo)
            {
               
                Session["EXSQL1"] = wd.WoExport(gvUsers.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString(), txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["Plantcode"]), txtCenter.Text, txtSite.Text, txtWorkOrder.Text, txtID.Text, chkClose.Checked, Convert.ToInt32(e.CommandName), Convert.ToInt32(Session["uRole"]), Convert.ToString(Session["Uid"]), 0);
                Session["EXTitle1"] = wd.WoExport(gvUsers.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString(), txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["Plantcode"]), txtCenter.Text, txtSite.Text, txtWorkOrder.Text, txtID.Text, chkClose.Checked, Convert.ToInt32(e.CommandName), Convert.ToInt32(Session["uRole"]), Convert.ToString(Session["Uid"]), 1);
                Session["EXHeader1"] = "";
                ltlAlert.Text = "window.open('/public/exportExcel1.aspx','_blank')";
            }
        }
    }

    protected void BtnAll_Click(object sender, EventArgs e)
    {
        CheckDateFormat();

        this.ExportExcel(adam.dsn0()
            , wd.Wo2ExportALL(Convert.ToString(txtUserNo.Text.Trim()), txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["Plantcode"]), txtCenter.Text, txtSite.Text, txtWorkOrder.Text, txtID.Text, chkClose.Checked, '1', Convert.ToInt32(Session["uRole"]), Convert.ToString(Session["Uid"]), 1)
            , wd.Wo2ExportALL(Convert.ToString(txtUserNo.Text.Trim()), txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["Plantcode"]), txtCenter.Text, txtSite.Text, txtWorkOrder.Text, txtID.Text, chkClose.Checked, '1', Convert.ToInt32(Session["uRole"]), Convert.ToString(Session["Uid"]), 0)
            , false);
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        CheckDateFormat();

        this.ExportExcel(adam.dsn0()
            , wd.ExportWorkHours(txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["Plantcode"]), txtCenter.Text, txtSite.Text, txtWorkOrder.Text, txtID.Text, txtUserNo.Text, txtUserName.Text, chkClose.Checked, Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(Session["uRole"]), Convert.ToString(Session["Uid"]), 1)
            , wd.ExportWorkHours(txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["Plantcode"]), txtCenter.Text, txtSite.Text, txtWorkOrder.Text, txtID.Text, txtUserNo.Text, txtUserName.Text, chkClose.Checked, Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(Session["uRole"]), Convert.ToString(Session["Uid"]), 0)
            , false);
    }
}
