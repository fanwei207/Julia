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


public partial class part_Npart_partApplyForSupplier : BasePage
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

    private void bindDDLType()
    {
        ddlModleList.Items.Clear();
        ddlModleList.DataSource = help.selectModleTypeForOpenNode(hidNodeID.Value);
        ddlModleList.DataBind();
        ddlModleList.Items.Insert(0, new ListItem("--", "00000000-0000-0000-0000-000000000000"));
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
    }

    private void buildGV()
    {
        string typeID = ddlModleList.SelectedValue;
        string uID = Session["uID"].ToString();
        DataTable dt = help.getGvColByTypeDetForSupplier(typeID);
       

        bool choice = true;

       

        help.createGridView(gvDet, dt, choice,false);

        gvDet.PageIndexChanging += new GridViewPageEventHandler(gvDet_PageIndexChanging);
        gvDet.RowDataBound += new GridViewRowEventHandler(gv_RowDataBound);
     

    }

    private void bindData()
    {

        string typeID = ddlModleList.SelectedValue;
        string uID = Session["uID"].ToString();
        string nodeID = hidNodeID.Value;

        DataTable dts = help.getGvDataByTypeIDForSupplier(typeID, nodeID);
        gvDet.DataSource = dts;
        gvDet.DataBind();
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
    protected void lkbModle_Click(object sender, EventArgs e)
    {

         if (ddlModleList.SelectedValue.Equals("00000000-0000-0000-0000-000000000000"))
         {
            Alert("请选择模板再导出");
            return;
         }

        string title = "100^<b>申请号</b>~^100^<b>物料类型</b>~^180^<b>详细描述</b>~^160^<b>MPQ</b>~^100^<b>MOQ</b>~^100^<b>vend</b>~^100^<b>leadtime</b>~^";
        title+=  "160^<b>原制造商1</b>~^100^<b>型号1</b>~^100^<b>原制造商2</b>~^100^<b>型号2</b>~^100^<b>原制造商3</b>~^100^<b>型号3</b>~^";

        string typeID = ddlModleList.SelectedValue;
        string nodeID = hidNodeID.Value;

        DataTable dtExcel = help.getGvDateBySupplierDown(typeID, nodeID);
        
        ExportExcel(title, dtExcel, false);
    }
    protected void btnImport_Click(object sender, EventArgs e)
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


                    DataTable errDt = null;
                    DataTable dt = null;
                    bool success = false;
                    try
                    {
                        dt = GetExcelContents(strFileName);
                    }
                    catch (Exception ex)
                    {
                        ltlAlert.Text = "alert('导入文件必须是Excel格式a');";

                    }
                    finally
                    {
                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }
                    }

                    string message = "";
                    try
                    {
                        success = help.importForSupplier(dt,  out message, strUID, Session["uName"].ToString(), out errDt);//插入，
                    }
                    catch { }
                    finally
                    {
                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }

                    }
                    if (success)
                    {
                        if (message != "")
                        {
                            ltlAlert.Text = "alert('" + message + "')";
                            bindBehandGVData();
                            
                        }
                    }
                    else
                    {

                        //string title = "100^<b>QAD</b>~^100^<b>物料号</b>~^100^<b>供应商</b>~^160^<b>单位</b>~^100^<b>技术参考价</b>~^100^<b>需求规格</b>~^100^<b>备注</b>~^100^<b>错误信息</b>~^";
                        string title = "100^<b>申请号</b>~^100^<b>物料类型</b>~^180^<b>详细描述</b>~^160^<b>MPQ</b>~^100^<b>MOQ</b>~^100^<b>vend</b>~^100^<b>leadtime</b>~^";
                        title += "160^<b>原制造商1</b>~^100^<b>型号1</b>~^100^<b>原制造商2</b>~^100^<b>型号2</b>~^100^<b>原制造商3</b>~^100^<b>型号3</b>~^400^<b>错误信息</b>~^";

                        ltlAlert.Text = "alert('" + message + "')";
                        if (errDt != null && errDt.Rows.Count > 0)
                        {
                            ExportExcel(title, errDt, false);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式a');";
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

        foreach (GridViewRow gvRow in gvDet.Rows)
        {
            if (gvDet.DataKeys[gvRow.RowIndex].Values["MPQ"].ToString().Equals(string.Empty) || gvDet.DataKeys[gvRow.RowIndex].Values["vend"].ToString().Equals(string.Empty)
                || gvDet.DataKeys[gvRow.RowIndex].Values["MOQ"].ToString().Equals(string.Empty) || gvDet.DataKeys[gvRow.RowIndex].Values["leadtime"].ToString().Equals(string.Empty))
            {
                Alert("您选择通过的项目中，存在有MPQ、MRQ、供应商、采购周期没有填写，请填写后再通过");
                return;
            }
        }

        int result = workFlowHelper.PassForURL(nodeId, userId, table, out message);
        if (result == 1)
        {
            ltlAlert.Text = "alert('审批成功！');";
            #region 发送邮件

            string pass = "pass";

            string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
            string to = help.selectMailTo(table, pass);
            string copy = "";
            string subject = "有零件申请被通过，请处理";
            string body = "";
            #region 写Body
            body += "<font style='font-size: 12px;'>请到物料号新增处理</font><br />";

            body += "<font style='font-size: 12px;'>详情请登陆 " + baseDomain.getPortalWebsite() + " </font><br />";
            body += "<font style='font-size: 12px;'>For details please visit " + baseDomain.getPortalWebsite() + " </font>";
            #endregion
            if (!this.SendEmail(from, to, copy, subject, body))
            {
                this.ltlAlert.Text = "alert('Email sending failure');";
            }
            else
            {
                this.ltlAlert.Text = "alert('Email sending');";
            }
            #endregion
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
                   idList.Append(";" +gvDet.DataKeys[gvRow.RowIndex].Values["id"].ToString());
            }
        }

        if (idList.ToString().Length <= 0)
        {
            Alert("您没有选中审批项目");
            return;
        }

        string url = "/part/Npart_failApplyPage.aspx?nodeId=" + nodeId + "&idList=" + idList.ToString();

        ltlAlert.Text="$.window('上传文档',600,500,'" + url.ToString() + "', '', true);";

        
    }
}