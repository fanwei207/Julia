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
using System.IO;
using RD_WorkFlow;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class RDW_ProjLog : BasePage
{
    RDW rdw = new RDW();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropCatetory.DataSource = rdw.SelectProjectCategory(string.Empty);
            dropCatetory.DataBind();
            dropCatetory.Items.Insert(0, new ListItem("--", "0"));

            txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-1));
            BindStep();
            BindGridView();
        }
    }
    protected void BindStep()
    {
        string sql = "select RDW_Code,RDW_StepName from Rdw_StandardStep Where RDW_Delete is null Order by RDW_Sort";
        ddl_step.DataSource = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.Text, sql);        
        ddl_step.DataBind();
        ddl_step.Items.Insert(0, new ListItem("--", "--"));
    }

    protected override void BindGridView()
    {
        try
        {
            string strSql = @" select ms.rdw_project,rdw_prodcode,rdw_proddesc,rdw_prodsku,rdw_stepname,rdw_stepdesc,rdw_message,RDw_createname,rdw_createdate,d.RDW_TaskID,ms.rdw_mstrid as RDW_MstrID,me.rdw_detid as RDW_DetID
                                from dbo.RDW_Det_Message me inner join rdw_det d on me.rdw_detid = d.rdw_detid
                                inner join rdw_mstr ms on d.rdw_mstrid = ms.rdw_mstrid
                                LEFT JOIN ( 
                                    SELECT MAX(RDW_enterDate) AS RDW_enterDate,RDW_detID,RDW_uID FROM dbo.RDW_EnterLog 
                                    Where RDW_uID = " + Session["uID"].ToString() + @" GROUP BY RDW_detID,RDW_uID ) a
                                ON me.RDW_DetID=a.RDW_detID
                                where 1 = 1 ";
            if(ddl_step.SelectedItem.ToString() != "--")
            {
                strSql += " And d.RDW_Code = '" + ddl_step.SelectedValue + "' ";
            }
            if(ddl_status.SelectedValue != "-1")
            {
                strSql += " And isnull(d.RDW_Status,0) = " + ddl_status.SelectedValue ;
            }
            if (txtProject.Text.Trim().Length > 0)
            {
                strSql += " And ms.rdw_project like Replace('" + txtProject.Text.Trim() + "', '*', '%')";
            }

            if (txtMessage.Text.Trim().Length > 0)
            {
                strSql += " And rdw_message like Replace('" + txtMessage.Text.Trim() + "', '*', '%')";
            }

            if (txtStartDate.Text.Trim().Length > 0)
            {
                try
                {
                    DateTime _dt = Convert.ToDateTime(txtStartDate.Text);
                }
                catch
                {
                    ltlAlert.Text = "alert('Create Date Error!');";
                    return;
                }

                strSql += " And Isnull(rdw_createdate, GetDate()) >= '" + txtStartDate.Text.Trim() + "' ";
            }

            if (txtEndDate.Text.Trim().Length > 0)
            {
                try
                {
                    DateTime _dt = Convert.ToDateTime(txtEndDate.Text);
                }
                catch
                {
                    ltlAlert.Text = "alert('Create Date Error!');";
                    return;
                }

                strSql += " And Isnull(rdw_createdate, GetDate()) < '" + txtEndDate.Text.Trim() + "' ";
            }

            if (txtCreater.Text.Trim().Length > 0)
            {
                strSql += " And RDw_createname = '" + txtCreater.Text.Trim() + "'";
            }
            if (dropCatetory.SelectedIndex > 0)
            {
                strSql += " And RDW_Category = " + dropCatetory.SelectedValue ;
            }
            if ( ddl_Read.SelectedValue != "-1" )
            {
                strSql += " And ( case When me.RDW_CreateDate > isnull(a.RDW_enterDate,'') then 0 else 1 End ) = " + ddl_Read.SelectedValue;
            }
            if (Session["PlantCode"].ToString() == "99")
            {
                strSql += " And RDW_Category <> 147";
            }


            strSql += " Order by rdw_createby,rdw_project,rdw_sort,rdw_createname ";
            DataTable table = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.Text, strSql).Tables[0];

            gvRDW.DataSource = table;
            gvRDW.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRDW_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvRDW_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRDW.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (txtStartDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtStartDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('Create Date Error!');";
                return;
            }
        }

        if (txtEndDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtEndDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('Create Date Error!');";
                return;
            }
        }

        ltlAlert.Text = "window.open('RDW_ProjLogExport.aspx?proj=" + txtProject.Text.Trim() + "&msg=" + txtMessage.Text.Trim() + "&date1=" + txtStartDate.Text.Trim() + "&date2=" + txtEndDate.Text.Trim() + "&crter=" + txtCreater.Text.Trim() + "', '_blank');";
    }
    protected void gvRDW_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Project")
        {
            int intRow = ((e.CommandSource as Control).Parent.Parent as GridViewRow).RowIndex;
            string strMID = gvRDW.DataKeys[intRow].Values["RDW_MstrID"].ToString();
            this.Redirect("/RDW/RDW_DetailList.aspx?mid=" + strMID +"&fr=projlog");
        }
        if (e.CommandName.ToString() == "StepName")
        {

            int intRow = ((e.CommandSource as Control).Parent.Parent as GridViewRow).RowIndex;
            string strMID = gvRDW.DataKeys[intRow].Values["RDW_MstrID"].ToString();
            string strDID= gvRDW.DataKeys[intRow].Values["RDW_DetID"].ToString();
            this.Redirect("/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDID + "&fr=projlog");
        }
    }
    protected void gvRDW_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lb_Pro = (LinkButton)e.Row.FindControl("lb_Pro");
        }
    }
}
