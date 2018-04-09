using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class part_Npart_partApplyForCommon : BasePage
{
    Npart_help help = new Npart_help();
    private NewWorkflow workFlowHelper = new NewWorkflow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidFlowID.Value = Request.QueryString["Flow_Id"];
            hidNodeID.Value = Request.QueryString["Node_Id"];

            bindDDLType();//绑定模板

            
        }
        bindBehandGVData();
    }

    private void bindBehandGVData()
    {
        if (ddlModleList.SelectedValue.Equals("00000000-0000-0000-0000-000000000000"))
        {
            gvDet.DataSource = null;
            gvDet.DataBind();
        }
        else
        {
            //if (!ddlModleList.SelectedValue.Equals(hidModleID.Value))
           // {
            //    hidModleID.Value = ddlModleList.SelectedValue;
                buildGV();//制作gv

           // }
            

            bindData();
        }
    }

    private void bindData()
    {


        string typeID = ddlModleList.SelectedValue;
        string uID = Session["uID"].ToString();
        string nodeID = hidNodeID.Value;

        DataTable dts = help.getGvDataByTypeIDForCommon(typeID, nodeID);
        gvDet.DataSource = dts;
        gvDet.DataBind();
    }

    private void buildGV()
    {
        string typeID = ddlModleList.SelectedValue;
        string uID = Session["uID"].ToString();
        DataTable dt = help.getGvColByTypeDet(typeID);
       

        bool choice = true;
        bool canDelete = true;
        canDelete = false;

        help.createGridView(gvDet, dt, choice, canDelete);

        gvDet.PageIndexChanging += new GridViewPageEventHandler(gvDet_PageIndexChanging);
        gvDet.RowDataBound += new GridViewRowEventHandler(gv_RowDataBound);
        
    }
    private void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            GridView gv = sender as GridView;


            string gid = gvDet.DataKeys[e.Row.RowIndex].Values["id"].ToString();
            string code = gvDet.DataKeys[e.Row.RowIndex].Values["partApplyCode"].ToString();
            string flg = "0";

            StringBuilder url = new StringBuilder("/part/qad_documentmain.aspx?gid=" + gid + "&code=" + code + "&flg=" + flg);

            e.Row.Attributes.Add("ondblclick", "$.window('上传文档',1200,1000,'" + url.ToString() + "', '', false);");
        }
    }

    private void gvDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDet.PageIndex = e.NewPageIndex;
        bindData();
    }
    private void bindDDLType()
    {
        ddlModleList.Items.Clear();
        //if (hidNodeID.Value == "9D5267D2-1704-402E-BF80-ED8A74A404EE")//DCC确认
        //{
          
        //    ddlModleList.DataSource = help.selectAllTypeMstr();
           
            
        //}
        //else 
        //{
        //    string flag = string.Empty;

        //    if (hidNodeID.Value == "B49CA5F4-65C3-4CE3-92EF-3AA5A3FE22A1")
        //    {
        //        flag = "10";//电子件确认
        //    }
        //    else if (hidNodeID.Value == "065C05BC-0B1D-4795-A1A4-243A8271AFE2")
        //    {
        //        flag = "20";//结构件确认
        //    }
        //    else if (hidNodeID.Value == "F98889E9-FDB5-4AD3-9218-07B169E760D6")
        //    {
        //        flag = "30";//结构件确认
        //    }
        //    else
        //    {
        //        Alert("节点数据出错，请联系管理员");
        //        return;
        //    }
        //    ddlModleList.DataSource = help.selectTypeMstrByEngineeringType(flag);
        //}

        ddlModleList.DataSource = help.selectModleTypeForOpenNode(hidNodeID.Value);
        ddlModleList.DataBind();
        ddlModleList.Items.Insert(0, new ListItem("--", "00000000-0000-0000-0000-000000000000"));
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bindBehandGVData();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string nodeId = hidNodeID.Value;
        string userId = Session["uID"].ToString();
        DataTable table = GetSelectedId();
        string message = "";

        if (table.Rows.Count <= 0)
        {
            Alert("您没有选中审批项目");
            return;
        }

        int result = workFlowHelper.PassForURL(nodeId, userId, table, out message);
        if (result == 1)
        {
            ltlAlert.Text = "alert('审批成功！');";
            bindBehandGVData();

        }
        else if (result == -1)
        {
            ltlAlert.Text = "alert('" + message + "');";
        }
        else
        {
            ltlAlert.Text = "alert('操作失败,请联系管理员！');";
        }
    }

    private DataTable GetSelectedId()
    {
        DataTable table = new DataTable("TempTable");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "sourceID";
        table.Columns.Add(column);



        foreach (GridViewRow gvRow in gvDet.Rows)
        {
            CheckBox chk = gvRow.FindControl("chk") as CheckBox;
            if (chk != null && chk.Checked)
            {

                row = table.NewRow();
                row["sourceID"] = gvDet.DataKeys[gvRow.RowIndex].Values["id"].ToString();


                table.Rows.Add(row);
            }
        }
        return table;
    }
    protected void btnRefuse_Click(object sender, EventArgs e)
    {
        string nodeId = hidNodeID.Value;
        string userId = Session["uID"].ToString();
        StringBuilder idList = new StringBuilder();



        foreach (GridViewRow gvRow in gvDet.Rows)
        {
            CheckBox chk = gvRow.FindControl("chk") as CheckBox;
            if (chk != null && chk.Checked)
            {
                idList.Append(";" + gvDet.DataKeys[gvRow.RowIndex].Values["id"].ToString());
            }
        }

        if (idList.ToString().Length <= 0)
        {
            Alert("您没有选中审批项目");
            return;
        }

        string url = "/part/Npart_failApplyPage.aspx?nodeId=" + nodeId + "&idList=" + idList.ToString();

        ltlAlert.Text = "$.window('上传文档',600,500,'" + url.ToString() + "', '', true);";
    }
}