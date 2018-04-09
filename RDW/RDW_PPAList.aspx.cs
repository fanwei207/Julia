using Microsoft.ApplicationBlocks.Data;
using RD_WorkFlow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;



public partial class RDW_RDW_PPAList : BasePage
{
    RDW rdw = new RDW();
    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];

    //class CustomGridView : ITemplate
    //{
    //    RDW_RDW_PPAList OutLine1;

    //    public CustomGridView(RDW_RDW_PPAList OutLine)
    //    {
    //        OutLine1 = OutLine;
    //    }
    //    public void InstantiateIn(Control container)
    //    {
    //        LinkButton lkb = new LinkButton();
    //        lkb.ID = "lkbDelete";
    //        lkb.CommandName = "DeletePPA";
    //        lkb.Text = "Delete";
    //        lkb.Click += new EventHandler(lkb_Click);
    //        lkb.DataBinding += new EventHandler(EditButton_DataBinding);
    //        container.Controls.Add(lkb);
    //    }

    //    void EditButton_DataBinding(object sender, EventArgs e)
    //    {
    //        LinkButton button = (LinkButton)sender;
    //        GridViewRow container = (GridViewRow)button.NamingContainer;
    //        DataRowView drv = container.DataItem as DataRowView;
            
    //    }

    //    void lkb_Click(object sender, EventArgs e)
    //    {


    //        OutLine1.gv_RowCommand(sender, (GridViewCommandEventArgs)e);

    //        //GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;

    //        //string ppaid = gv.DataKeys[gvr.RowIndex].Values["ppa_mstrID"].ToString();


    //        //bool flag = deletePPAByMstrID(ppaid);
    //        //if (flag)
    //        //{
    //        //    Alert("删除成功");
    //        //    BindGridView();
    //        //}
    //        //else
    //        //{
    //        //    Alert("删除失败");
    //        //}


    //    }
    //}

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("172225", "PPA list select All authority");
            this.Security.Register("170008", "View All Project");
        }

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }
    protected DataTable GetTable()
    {
        string strSql = "sp_RDW_selectProjPPAs";
        
        try
        {
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@ppa", txt_ppa.Text.Trim());

            if (this.Security["172225"].isValid)
            {
                parm[1] = new SqlParameter("@ppaAll", "1");
            }
            else
            {
                parm[1] = new SqlParameter("@ppaAll", "0");
            }
            parm[2] = new SqlParameter("@uID", Session["uID"].ToString());

            if (this.Security["170008"].isValid)
            {
                parm[3] = new SqlParameter("@projectAll", "1");
            }
            else
            {
                parm[3] = new SqlParameter("@projectAll", "0");
            }


            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, parm).Tables[0];
        }
        catch
        {
            return null;
        }
        
    }
    protected void BindGridView()
    {

        if (Request.QueryString["error"] != null)
        {
            this.Alert("该PPA没有通过审批或没有申请审批，不可查看只能编辑。");
        }
        DataTable dt_data = GetTable();

        gv.Columns.Clear();
        BoundField col6 = new BoundField();
        col6.DataField = "ppa_projIdentifier";
        col6.HeaderText = "Project Identifier";
        gv.Columns.Add(col6);
        ButtonField btn = new ButtonField();
        btn.ButtonType = ButtonType.Link;
        btn.HeaderText = "Detail";
        btn.Text = "Detail";
        btn.CommandName = "ShowDetail";
        gv.Columns.Add(btn);

        ButtonField btnEdit = new ButtonField();
        btnEdit.ButtonType = ButtonType.Link;
        btnEdit.HeaderText = "Edit";
        btnEdit.Text = "Edit";
        btnEdit.CommandName = "EditPPA";
        gv.Columns.Add(btnEdit);

        ButtonField btndelete = new ButtonField();
        btndelete.ButtonType = ButtonType.Link;
        btndelete.HeaderText = "Delete";
        btndelete.Text = "Delete";
        btndelete.CommandName = "DeletePPA";
        gv.Columns.Add(btndelete);

        //TemplateField btndelete = new TemplateField();
        //CustomGridView cg = new CustomGridView(this);
        //btndelete.ItemTemplate = cg;
        ////btndelete.ItemTemplate.InstantiateIn(lkb);
        //btndelete.HeaderText = "Delete";
        //gv.Columns.Add(btndelete);

        BoundField col7 = new BoundField();
        col7.DataField = "ppa_Classification";
        col7.HeaderText = "Classification";
        gv.Columns.Add(col7);
        BoundField col8 = new BoundField();
        col8.DataField = "ppa_ReplaStrategy";
        col8.HeaderText = "Replacement Strategy";
        gv.Columns.Add(col8);
        BoundField col9 = new BoundField();
        col9.DataField = "ppa_bomCost";
        col9.HeaderText = "Target BOM Cost";
        gv.Columns.Add(col9);
        BoundField col10 = new BoundField();
        col10.DataField = "ppa_prodCost";
        col10.HeaderText = "Target Product Cost";
        gv.Columns.Add(col10);

        BoundField col11 = new BoundField();
        col11.DataField = "ppa_forecastF3M";
        col11.HeaderText = "Forecast Orders (First 3 months)";
        gv.Columns.Add(col11);
        BoundField col12 = new BoundField();
        col12.DataField = "ppa_forecastFY";
        col12.HeaderText = "Forecast Orders (First Year)";
        gv.Columns.Add(col12);
        BoundField col13 = new BoundField();
        col13.DataField = "ppa_SalesChannel";
        col13.HeaderText = "Target Sales Channel";
        gv.Columns.Add(col13);
        BoundField col14 = new BoundField();
        col14.DataField = "ppa_rpaDate";
        col14.HeaderText = "Reqested Product Availability Date";
        gv.Columns.Add(col14);
        BoundField col15 = new BoundField();
        col15.DataField = "ppa_cpaDate";
        col15.HeaderText = "Committed Product Availability Date";
        gv.Columns.Add(col15);

        //BoundField col15 = new BoundField();
        //col15.DataField = "ppa_cpaDate";
        //col15.HeaderText = "Committed Product Availability Date";

        //gv.Columns.Add(col15);

        for ( int i = 12; i < dt_data.Columns.Count; i++ )
        {
            BoundField col = new BoundField();
            col.DataField = dt_data.Columns[i].Caption.ToString();
            
            if (dt_data.Columns[i].Caption.ToString().IndexOf("-a")  > 0 )
            {
                col.HeaderText = dt_data.Columns[i].Caption.ToString().Replace("-a","  Actual");
            }
            else
            {
                col.HeaderText = dt_data.Columns[i].Caption.ToString().Replace("-t", "  Target");
            }
            gv.Columns.Add(col);
        }
        gv.AllowPaging = true;
        gv.PageIndexChanging += new GridViewPageEventHandler(gv_PageIndexChanging);
        gv.RowCommand += new GridViewCommandEventHandler(gv_RowCommand);
        gv.RowDataBound += new GridViewRowEventHandler(gv_RowDataBound);
        gv.DataSource = dt_data;
        gv.DataBind();
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gv = sender as GridView;
        gv.PageIndex = e.NewPageIndex;
        BindGridView();

    }
    protected void bnt_query_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataTable dt_data = GetTable();
        string title = "<b>Project Identifier</b>~^"
            + "80^<b>Classification</b>~^160^<b>Replacement Strategy</b>~^<b>Target BOM Cost</b>~^<b>Target Product Cost</b>~^150^<b>Forecast Orders (First 3 months)</b>~^"
            + "200^<b>Forecast Orders (First Year)</b>~^<b>Target Sales Channel</b>~^<b>Reqested Product Availability Date</b>~^<b>Committed Product Availability Date</b>~^";

        for (int i = 11; i < dt_data.Columns.Count; i++ )
        {
            if (dt_data.Columns[i].Caption.ToString().IndexOf("-a")  > 0 )
            {
                title += "<b>" + dt_data.Columns[i].Caption.ToString().Replace("-a", "  Actual") + "</b>~^100^";
            }
            else
            {
                title += "<b>" + dt_data.Columns[i].Caption.ToString().Replace("-t", "  Target") + "</b>~^100^";
            }
            
        }

       ExportExcel(title, dt_data, false);
    }
    public void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetail")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string ppaid = gv.DataKeys[intRow].Values["ppa_mstrID"].ToString();
            Response.Redirect("/RDW/RDW_PPADetail.aspx?from=ppalist&mstrid=" + ppaid + "&appv=0&isView=1", true);
        }
        if (e.CommandName == "EditPPA")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string ppaid = gv.DataKeys[intRow].Values["ppa_mstrID"].ToString();
            Response.Redirect("/RDW/RDW_PPADetail.aspx?from=ppalist&mstrid=" + ppaid + "&appv=0", true);
        }
        if (e.CommandName == "DeletePPA")
        {

            //GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string ppaid = gv.DataKeys[intRow].Values["ppa_mstrID"].ToString();
            

            bool flag = deletePPAByMstrID(ppaid);
            if (flag)
            {
                Alert("删除成功");
                BindGridView();
            }
            else
            {
                Alert("删除失败");
            }

            
        }

        
    }

    private bool deletePPAByMstrID(string ppaid)
    {

        string sqlstr = "sp_RDW_deletePPAByMstrID";

        string sqlstrSelect = "sp_ppa_selectDocs";

        SqlParameter[] paramSelect = new SqlParameter[]{
            new SqlParameter("@mstrId", ppaid)
            , new SqlParameter("@isView","0")
        };


        SqlDataReader sdr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, sqlstrSelect, paramSelect);

        try
        {

            while (sdr.Read())
            {
                if (File.Exists(sdr["ppa_path"].ToString()))
                {
                    File.Delete(sdr["ppa_path"].ToString());
                }

            }
        }
        catch {
            return false; 
        }
        finally { }

        sdr.Dispose();
        sdr.Close();

        SqlParameter param = new SqlParameter("@mstrID", ppaid);

        return Convert.ToBoolean( SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param));


    }
    protected void btn_new_Click(object sender, EventArgs e)
    {
        Response.Redirect("RDW_PPADetail.aspx?appv=0");
    }
    //protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        if (!Convert.ToBoolean(gv.DataKeys[e.Row.RowIndex].Values["isappv"]))
    //        {
    //            e.Row.Cells[1].Text = "";
    //        }
    //    }
    //}
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
          if (e.Row.RowType == DataControlRowType.DataRow)
          {
              string flag = gv.DataKeys[e.Row.RowIndex].Values["canDelete"].ToString();
              if (flag.Equals("0"))
              {
                  //((LinkButton)e.Row.FindControl("lkbDelete")).Text = "";
                  //((LinkButton)e.Row.FindControl("lkbDelete")).CommandName = "DeletePPA";
                  //((LinkButton)e.Row.FindControl("lkbDelete")).CommandArgument = e.Row.RowIndex.ToString();
                  e.Row.Cells[3].Text = "";
              }
             
          }
    }
}

public class GridViewTemplate : ITemplate
{
    public delegate void EventHandler(object sender, EventArgs e);
    public event EventHandler eh;

    private DataControlRowType templateType;

    private string columnName;
    private string controlID;
    private string ppaId;



    public GridViewTemplate(DataControlRowType type, string colname)
    {

        templateType = type;

        columnName = colname;


    }
    public GridViewTemplate(DataControlRowType type, string controlID, string colname)
    {
        templateType = type;
        this.controlID = controlID;
        columnName = colname;
    }


    public void InstantiateIn(System.Web.UI.Control container)
    {
        switch (templateType)
        {
            case DataControlRowType.Header:
                Literal lc = new Literal();
                lc.Text = columnName;
                container.Controls.Add(lc);
                break;
            case DataControlRowType.DataRow:
                LinkButton lbtn = new LinkButton();
                lbtn.ID = this.controlID;
                if (eh != null)
                {
                    lbtn.Click += new System.EventHandler(eh);
                }
                lbtn.DataBinding += new System.EventHandler(lbtn_DataBinding);

                container.Controls.Add(lbtn);

                break;
            default:
                break;
        }
    }
    void lbtn_DataBinding(object sender, EventArgs e)
    {
        LinkButton lbtn = sender as LinkButton;
        if (lbtn != null)
        {
            GridViewRow container = lbtn.NamingContainer as GridViewRow;
            if (container != null)
            {
                object dataValue = DataBinder.Eval(container.DataItem, columnName);
                if (dataValue != DBNull.Value)
                {
                    lbtn.Text = dataValue.ToString();
                }
            }
        }
    }
}
