using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Web.UI.WebControls;
using System.Text;
using System.IO;

public partial class NWF_nwf_workflowInstanceReview : BasePage
{
    private NewWorkflow helper = new NewWorkflow();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (BindWorkflow())
            {
                BindFlowNode();
                BindData();
            }
        }
       
        
    }

    private bool BindWorkflow()
    {
        string flowId = Request.QueryString["FlowId"];
        string userId = Session["uID"].ToString() ;
        if (flowId != null)
        {
            tdWorkFlow.Visible = false;
        }
        DataTable dt = helper.GetWorkflow(userId, flowId);
        ddlWorkFlow.DataSource = dt;
        ddlWorkFlow.DataBind();
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void BindFlowNode()
    {
        string userId = Session["uID"].ToString();
        string flowId = ddlWorkFlow.SelectedValue;
        DataTable dtNode = helper.GetFlowNode(flowId, userId);
        menuNode.Items.Clear();
        foreach (DataRow row in dtNode.Rows)
        {
            MenuItem item = new MenuItem();
            item.Text = row["Node_Name"].ToString();
            item.Value = row["Node_Id"].ToString();
            menuNode.Items.Add(item);
        }
        menuNode.Items[0].Selected = true;
    }

    public void BindData()
    {
        hidCheck.Value = ";";
        if (menuNode.SelectedItem != null)
        {
            helper.InitGridView(gvDet, menuNode.SelectedItem.Value, true);
            gvDet.PageIndexChanging += new GridViewPageEventHandler(gvDet_PageIndexChanging);
            gvDet.RowDataBound += new GridViewRowEventHandler(gv_RowDataBound);
            DataTable dt = helper.GetFormDataForApprove(menuNode.SelectedItem.Value, txtCondition.Text.Trim());
            gvDet.DataSource = dt;
            gvDet.DataBind();
        }
        else 
        {
            ltlAlert.Text = "alert('没有审批权限！');";
        }
    }

    protected void ddlWorkFlow_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCondition.Text = "";
        BindFlowNode();
        BindData();
    }
    protected void menuNode_MenuItemClick(object sender, MenuEventArgs e)
    {
        txtCondition.Text = "";
        BindData();
    }
    protected void linkDownload_Click(object sender, EventArgs e)
    {
        DataTable dtHeader = helper.GetFormDesign(menuNode.SelectedItem.Value);
        StringBuilder header = new StringBuilder();
        foreach (DataRow row in dtHeader.Rows)
        {
            header.Append("<b>").Append(row["label"].ToString()).Append("</b>~^");
        }
        DataTable dtData = helper.GetFormDataForApprove(menuNode.SelectedItem.Value, txtCondition.Text.Trim());

        this.ExportExcel(header.ToString(), dtData, true, ExcelVersion.Excel2003);
        BindData();
    }
    protected void btnPass_Click(object sender, EventArgs e)
    {
        if (hidCheck.Value == ";")
        {
            ltlAlert.Text = "alert('请选择数据！');";
        }
        else
        {
            string nodeId = menuNode.SelectedItem.Value;
            string userId = Session["uID"].ToString();
            DataTable table = GetSelectedData();
            string message = "";
           // DataTable dt = helper.PassEmail(nodeId, userId, table, out message);
            int result = helper.Pass(nodeId, userId, table, out message);
            if (result == 1)
            {
                
               
                if (ddlWorkFlow.SelectedValue == "C9BCB464-6767-412E-A3E6-E615D0B43809")
                {
                    QadService.WebService1SoapClient client = new QadService.WebService1SoapClient();
                    client.Product_Add_Submit();
                }

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
        BindData();
    }
    protected void btnFailed_Click(object sender, EventArgs e)
    {
        if (hidCheck.Value == ";")
        {
            ltlAlert.Text = "alert('请选择数据！');";
        }
        else
        {
            string nodeId = menuNode.SelectedItem.Value;
            string userId = Session["uID"].ToString();
            DataTable table = GetSelectedData();
            string message = "";
         
            int result = helper.Fail(nodeId, userId, table, out message);
            if (result == 1)
            {

              
                ltlAlert.Text = "alert('审批成功！');";
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
        BindData();
    }

    private DataTable GetSelectedData()
    {
        string[] indexes = hidCheck.Value.Substring(1, hidCheck.Value.Length - 2).Split(new char[] { ';' });
        DataTable table = new DataTable("FormData");
        foreach (string key in gvDet.DataKeyNames)
        {
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = key;
            table.Columns.Add(column);
        }
        foreach (string index in indexes)
        {
            DataRow row = table.NewRow();
            foreach (string key in gvDet.DataKeyNames)
            {
                object value = gvDet.DataKeys[int.Parse(index)].Values[key];

                if (value.GetType().ToString() == "System.DateTime")
                {
                    DateTime d = Convert.ToDateTime(value);
                    row[key] = d.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
                else
                {
                    row[key] = gvDet.DataKeys[int.Parse(index)].Values[key];
                }
            }
            table.Rows.Add(row);
        }
        return table;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string strFileName = "";
        string strCatFolder = "";
        string strUserFileName = "";
        int intLastBackslash = 0;

        string strUID = Convert.ToString(Session["uID"]);
        string struName = Convert.ToString(Session["uName"]);

        strCatFolder = Server.MapPath("/import");
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }
        }

        strUserFileName = filename.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return;
        }
        strUserFileName = strFileName;

        int i = 0;
        while (i < 1000)
        {
            strFileName = strCatFolder + "\\f" + i.ToString() + strUserFileName;
            if (!File.Exists(strFileName))
            {
                break;
            }
            i += 1;
        }

        if (filename.PostedFile != null)
        {
            if (filename.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                filename.PostedFile.SaveAs(strFileName);//上传 文件
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    string message="";
                    DataTable dtData = this.GetExcelContents(strFileName);
                    if (helper.Import(menuNode.SelectedValue, dtData, out message))
                    {
                        ltlAlert.Text = "alert('导入成功！')";
                    }
                    else
                    {
                        if (message != "")
                        {
                            ltlAlert.Text = "alert('" + message + "')";
                        }
                        else
                        {
                            DataTable dtHeader = helper.GetFormDesign(menuNode.SelectedItem.Value);
                            StringBuilder header = new StringBuilder();
                            foreach (DataRow row in dtHeader.Rows)
                            {
                                header.Append("<b>").Append(row["label"].ToString()).Append("</b>~^");
                            }
                            header.Append("<b>错误信息</b>~^");

                            this.ExportExcel(header.ToString(), dtData, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                   ltlAlert.Text = "alert('导入文件必须是Excel格式'" + ex.Message.ToString() + "'.);";
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
            }
        }
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gv = sender as GridView;
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = sender as GridView;
            if (gv.Attributes["DetailPage"].ToString().Trim() != "")
            {
                StringBuilder url = new StringBuilder(gv.Attributes["DetailPage"]);
                if (gv.DataKeyNames.Length > 0)
                {
                    url.Append("?");
                    foreach (string key in gv.DataKeyNames)
                    {
                        url.Append(key).Append("=");
                        string value = HttpUtility.UrlEncode(gv.DataKeys[e.Row.RowIndex].Values[key].ToString());
                        url.Append(value).Append("&");
                    }
                    url.Append("Node_Id=").Append(menuNode.SelectedValue);
                }
                e.Row.Attributes.Add("ondblclick", "$.window('明细',1000,600,'" + url.ToString() + "', '', true);");
            }
        }
    }
}