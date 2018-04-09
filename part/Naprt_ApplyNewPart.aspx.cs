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


public partial class part_Naprt_ApplyNewPart : BasePage
{
    Npart_help help = new Npart_help();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindDDLType();//绑定模板

            if (!string.Empty.Equals(Request.QueryString["modleID"]))
            {
                ddlModleList.SelectedValue = Request.QueryString["modleID"];
               
                
            }

           
           
        }
        bindBehandGVData(); //绑定数据
        //gvDet.PageIndexChanging += new GridViewPageEventHandler(gvDet_PageIndexChanging);
        //gvDet.RowDataBound += new GridViewRowEventHandler(gv_RowDataBound);
        //gvDet.RowCommand += new GridViewCommandEventHandler(gv_RowCommand);
        
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
            buildGV();//制作gv
            bindData();
        }
        if (ddlStatus.SelectedValue != "0")
        {
            btnSubmit.Enabled = false;
        }
        else
        {
            btnSubmit.Enabled = true;
        }

        if (ddlStatus.SelectedValue.Equals("-10"))
        {
            btnExport.Visible = true;
        }
        else
        {
            btnExport.Visible = false;
        }
      
    }

    private void bindData()
    {
       

        string typeID = ddlModleList.SelectedValue;
        string uID = Session["uID"].ToString();
        string status = ddlStatus.SelectedValue;

        DataTable dts = null;
        if (status.Equals("-10"))
        {
            dts = help.getGvDataByTypeIDAndUIDFail(typeID, uID, status);
        }
        else
        {
            dts = help.getGvDataByTypeIDAndUID(typeID, uID, status);
        }
        gvDet.DataSource = dts;
        gvDet.DataBind();
    }

    private void buildGV()
    {
        string typeID  = ddlModleList.SelectedValue;
        string uID = Session["uID"].ToString();
        DataTable dt = null;
           

        if (ddlStatus.SelectedValue.Equals("-10"))
        {
            dt = help.getGvColByTypeDetForFail(typeID);
        }
        else
        {
            dt = help.getGvColByTypeDet(typeID);
        }

        string status = ddlStatus.SelectedValue;

        bool choice = true;
        bool canDelete =  true;
        canDelete = status == "10" || status == "20" ? false : true;
        
        help.createGridView(gvDet, dt, choice, canDelete);

        gvDet.PageIndexChanging += new GridViewPageEventHandler(gvDet_PageIndexChanging);
        gvDet.RowDataBound += new GridViewRowEventHandler(gv_RowDataBound);
        gvDet.RowCommand += new GridViewCommandEventHandler(gv_RowCommand);

    }

    private void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbDelete")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            string ID = gvDet.DataKeys[index].Values["id"].ToString();

            string uID = Session["uID"].ToString();

            string uName = Session["uName"].ToString();

            string flag = help.deletePartApply(ID, uID, uName);

            if (flag == "1")
            {
                Alert("删除成功");
                bindData();
            }
            else
            {
                Alert("删除失败");
            }

        }
    }

    private void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //GridView gv = sender as GridView;
            //if (gv.Attributes["DetailPage"].ToString().Trim() != "")
            //{
            //    //StringBuilder url = new StringBuilder(gv.Attributes["DetailPage"]);
            //    //if (gv.DataKeyNames.Length > 0)
            //    //{
            //    //    url.Append("?");
            //    //    foreach (string key in gv.DataKeyNames)
            //    //    {
            //    //        url.Append(key).Append("=");
            //    //        string value = HttpUtility.UrlEncode(gv.DataKeys[e.Row.RowIndex].Values[key].ToString());
            //    //        url.Append(value).Append("&");
            //    //    }
            //    //    url.Append("Node_Id=").Append(menuNode.SelectedValue);
            //    //}
            //    e.Row.Attributes.Add("ondblclick", "$.window('明细',1000,800,'" + url.ToString() + "', '', true);");
            //}
            GridView gv = sender as GridView;
          

            string gid = gvDet.DataKeys[e.Row.RowIndex].Values["id"].ToString();
            string code = gvDet.DataKeys[e.Row.RowIndex].Values["partApplyCode"].ToString();
            string flg = string.Empty;
            if (ddlStatus.SelectedValue.Equals("0") || ddlStatus.SelectedValue.Equals("-10"))
            {
                 flg = "1";
            }
            else
            {
                 flg = "0";
            }


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
        ddlModleList.DataSource = help.selectAllTypeMstr();
        ddlModleList.DataBind();
        ddlModleList.Items.Insert(0, new ListItem("--", "00000000-0000-0000-0000-000000000000"));
    }

    private DataTable getCommitDatatable()
    {
        DataTable table = new DataTable("TempTable");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "id";
        table.Columns.Add(column);


        foreach (GridViewRow gvRow in gvDet.Rows)
        {
            CheckBox chk = gvRow.FindControl("chk") as CheckBox;
            if (chk != null && chk.Checked)
            {

                row = table.NewRow();
                row["id"] = gvDet.DataKeys[gvRow.RowIndex].Values["id"].ToString();

                
                table.Rows.Add(row);
            }
        }


        return table;
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bindBehandGVData(); //绑定数据
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable tableID = getCommitDatatable();

        if (tableID.Rows.Count == 0)
        {
            Alert("您没有选中提交的零件");
            return;
        }

        string uID = Session["uID"].ToString();
        string uName = Session["uName"].ToString();

        string flag = help.commitApplyByID(tableID, uID, uName);

        if (flag == "1")
        {
            Alert("提交成功");
            bindData();
        }
        else
        {
            Alert("提交失败");
        }
        
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable tableID = getCommitDatatable();

        if (tableID.Rows.Count == 0)
        {
            Alert("您没有选中导出的零件申请");
            return;
        }

        string title = help.selectAllTypeDetByMstrIDAddFailReturnString(ddlModleList.SelectedValue);

        string typeID = ddlModleList.SelectedValue;
        string uID = Session["uID"].ToString();
        string status = ddlStatus.SelectedValue;

        DataTable dtExcel = help.getGvDataByTypeIDAndUIDFail(typeID, uID, status);

        ExportExcel(title, dtExcel, false);
    }
}