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
using RD_WorkFlow;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class RDW_HeaderList : BasePage
{
    RDW rdw = new RDW();
    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("170008", "注册查看所有项目的权限");
            this.Security.Register("170006", "注册新增项目的权限");
        }

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            btnAdd.Enabled = this.Security["170006"].isValid;
            SKUHelper skuHelper = new SKUHelper();

            btnComments.Enabled = this.Security["170094"].isValid;
            

            ddl_type.DataSource = rdw.SelectProjType();
            ddl_type.DataBind();
            ddl_type.Items.Insert(0, new ListItem("--", "999"));

            dropCatetory.DataSource = rdw.SelectProjectCategory(string.Empty);
            dropCatetory.DataBind();
            dropCatetory.Items.Insert(0, new ListItem("--", "0"));

            dropSKU.DataSource = skuHelper.Items(string.Empty);
            dropSKU.DataBind();
            dropSKU.Items.Insert(0, new ListItem("--", "--"));

            //恢复参数 Add By Shanzm 2014-01-17
            if (!string.IsNullOrEmpty(Request.QueryString["@__ca"]))
            {
                try
                {
                    dropCatetory.SelectedIndex = -1;
                    dropCatetory.SelectedIndex = Convert.ToInt32(Request.QueryString["@__ca"]);
                }
                catch
                {
                    dropCatetory.SelectedIndex = -1;
                }
            }

            if (!string.IsNullOrEmpty(Request.QueryString["@__pn"]))
            {
                txtProject.Text = Request.QueryString["@__pn"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["@__pc"]))
            {
                txtProjectCode.Text = Request.QueryString["@__pc"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["@__sd"]))
            {
                txtStartDate.Text = Request.QueryString["@__sd"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["@__st"]))
            {
                try
                {
                    dropStatus.SelectedIndex = -1;
                    dropStatus.SelectedIndex = Convert.ToInt32(Request.QueryString["@__st"]);
                }
                catch
                {
                    dropStatus.SelectedIndex = -1;
                }
            }

            if (!string.IsNullOrEmpty(Request.QueryString["@__sk"]))
            {
                try
                {
                    dropSKU.SelectedIndex = -1;
                    dropSKU.SelectedIndex = Convert.ToInt32(Request.QueryString["@__sk"]);
                }
                catch
                {
                    dropSKU.SelectedIndex = -1;
                }
            }

            if (!string.IsNullOrEmpty(Request.QueryString["@__kw"]))
            {
                txtKeyword.Text = Request.QueryString["@__kw"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["@__pg"]))
            {
                gvRDW.PageIndex = Convert.ToInt32(Request.QueryString["@__pg"]);
            }

            ddl_region.DataSource = rdw.SelectProjectRegion(string.Empty);
            ddl_region.DataBind();
            ddl_region.Items.Insert(0, new ListItem("--", "--"));

            BindData();
        }
    }

    

    protected void BindData()
    {
        //定义参数
        string strProj = txtProject.Text.Trim();
        string strProd = txtProjectCode.Text.Trim();
        string strSku = dropSKU.SelectedValue;
        string strStart = txtStartDate.Text.Trim();
        string strStatus = dropStatus.SelectedValue;
        string strMessage = string.Empty;
        string strUID = Convert.ToString(Session["uID"]);
        string strCateid = dropCatetory.SelectedValue;
        string keyword = txtKeyword.Text.Trim();
        string region = ddl_region.SelectedValue;
        string LampType = txtLampType.Text.Trim();

        bool canViewAll = true;
        
        if (!this.Security["170008"].isValid)
        {
            canViewAll = false;
        }

        gvRDW.DataSource = rdw.SelectRDWList(strCateid, strProj, strProd, strSku, strStart, strMessage, strStatus, strUID, keyword, ddl_region.SelectedValue, ddl_type.SelectedValue, canViewAll, LampType);
        gvRDW.DataBind();
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
        BindData();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DateTime date;
        if (txtStartDate.Text.Trim() != "")
        {
            if (!DateTime.TryParse(txtStartDate.Text.Trim(), out date))
            {
                this.Alert("Start Date must be  a date format!");
                return;
            }
        }
        gvRDW.PageIndex = 0;
        //if (dropStatus.SelectedIndex != 2)
        //{
            gvRDW.Columns[10].Visible = false;
            gvRDW.Columns[11].Visible = true;
        //}
        //else
        //{
        //    gvRDW.Columns[9].Visible = true;
        //    gvRDW.Columns[10].Visible = false;
        //}

        BindData();
    }

    protected void gvRDW_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        int intRow = 0;
        string strMID = string.Empty;
        string strProjectName = string.Empty;
        string strProjectCode = string.Empty;

        if (e.CommandName.ToString() == "Detail")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strMID = gvRDW.DataKeys[intRow].Values["RDW_MstrID"].ToString();
            
            string oldMID = gvRDW.DataKeys[intRow].Values["RDW_OldID"].ToString();
            Session["oldmID"] = oldMID;
            if (gvRDW.DataKeys[intRow].Values["RDW_OldID"].ToString() == "0" || gvRDW.DataKeys[intRow].Values["RDW_OldID"].ToString() == "")
                oldMID = strMID;

            Response.Redirect("/RDW/RDW_DetailList.aspx?mid=" + strMID 
                +"&projType=" + ddl_type.SelectedValue 
                + "&region=" + ddl_region.SelectedValue 
                + "&ecnCode=" + gvRDW.DataKeys[intRow].Values["RDW_EcnCode"].ToString() 
                + "&oldmID= " + oldMID 
                + "&@__kw=" + txtKeyword.Text.Trim() 
                + "&@__ca=" + dropCatetory.SelectedIndex.ToString() 
                + "&@__pn=" + txtProject.Text.Trim() 
                + "&@__pc=" + txtProjectCode.Text.Trim() 
                + "&@__sd=" + txtStartDate.Text.Trim() 
                + "&@__st=" + dropStatus.SelectedIndex.ToString() 
                + "&@__sk=" + dropSKU.SelectedIndex.ToString() 
                + "&@__pg=" + gvRDW.PageIndex.ToString() 
                + "&rm=" + DateTime.Now.ToString(), true);
        }
        if (e.CommandName.ToString() == "PPA")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strMID = gvRDW.DataKeys[intRow].Values["RDW_MstrID"].ToString();
            //根据strMID初始化PPA相关数据
            string result = rdw.InitialPPAInfo(strMID,Convert.ToInt32(Session["uID"]),Session["uName"].ToString());

            if (result != "1")
            {
                return;
            }
            else
            {
                if (gvRDW.DataKeys[intRow].Values["RDW_PPAMstrID"].ToString() == string.Empty)
                {
                    ltlAlert.Text = "alert('PPA is empty, please add!'); ";
                    return;
                }
                else
                {
                    this.Redirect("/RDW/RDW_PPADetail.aspx?mstrID=" + gvRDW.DataKeys[intRow].Values["RDW_PPAMstrID"].ToString() + "&from=HL&appv=0&isView=1");
                }
            }
            
        }
        if (e.CommandName.ToString() == "Stop")
        {     
            //intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strMID = e.CommandArgument.ToString();
          
            if (rdw.UpdateRDWHeaderCancel(strMID))
            {
                ltlAlert.Text = "window.location.href='/RDW/RDW_HeaderList.aspx?rm=" + DateTime.Now.ToString() + "';";
            }
            else
            {
                ltlAlert.Text = "alert('Cancel data error!'); ";
            }
        }
        if (e.CommandName.ToString() == "EditStatus")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strMID = gvRDW.DataKeys[intRow].Values["RDW_MstrID"].ToString();
            strProjectName = gvRDW.DataKeys[intRow].Values["RDW_Project"].ToString();
            strProjectCode = gvRDW.DataKeys[intRow].Values["RDW_ProdCode"].ToString();
            ltlAlert.Text = "var w=window.open('/RDW/RDW_EditStatus.aspx?mid=" + strMID + "&nm=" + strProjectName + "&cd=" + strProjectCode + "&rm=" + DateTime.Now.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0,center:Yes'); w.focus();";
        }
        if (e.CommandName.ToString() == "goQAD")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strMID = gvRDW.DataKeys[intRow].Values["RDW_MstrID"].ToString();
            ltlAlert.Text = "var w=window.open('/RDW/RDW_Qad.aspx?mid=" + strMID + "&rm=" + DateTime.Now.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();";
        }
        if (e.CommandName.ToString() == "gobom")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strMID = gvRDW.DataKeys[intRow].Values["RDW_MstrID"].ToString();
            //Response.Redirect("/RDW/RDW_app3.aspx?id=" + strMID + "&rm=" + DateTime.Now.ToString(), true);
            ltlAlert.Text = "var w=window.open('/RDW/RDW_doclist.aspx?mid=" + strMID + "&rm=" + DateTime.Now.ToString() + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();";

        }
    }

    protected void gvRDW_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
       
            try
            {
                //定义参数
                string strMID = string.Empty;
                string strUID = Convert.ToString(Session["uID"]);

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                    LinkButton btnStop = (LinkButton)e.Row.FindControl("btnStop");
                    LinkButton btnChange = (LinkButton)e.Row.FindControl("btnChange");

                    RDW_Header rh = (RDW_Header)e.Row.DataItem;
                    strMID = rh.RDW_MstrID.ToString();

                    if(rh.RDW_TypeName == "ECN")
                    {
                        e.Row.Cells[17].Enabled = false;
                        e.Row.Cells[17].Text = "";                        
                    }

                    if (rh.RDW_Status != "PROCESS")
                    {
                        btnDelete.Enabled = false;
                        btnDelete.Text = "";
                        btnStop.Enabled = false;
                        btnStop.Text = "";
                    }
                    else
                    {

                        if (rh.RDW_CreatedBy.ToString().Trim() == Convert.ToString(Session["uID"]) || rh.RDW_Partner.IndexOf(";" + rh.RDW_CreatedBy.ToString() + ";") >= 0 || Convert.ToInt32(Session["uRole"])==1)
                        {
                            if (rdw.CheckDeleteRDWHeader(strMID, strUID) || Convert.ToInt32(Session["uRole"]) == 1)
                            {
                            }
                            else
                            {
                                btnDelete.Enabled = false;
                                btnDelete.Text = "";
                            }
                            if (rdw.CheckCancelRDWHeader(strMID, strUID) || Convert.ToInt32(Session["uRole"]) == 1)
                            {
                                btnStop.Attributes.Add("onclick", "return confirm('Are you sure you want to cancel this project?')");
                            }
                            else
                            {
                                btnStop.Enabled = false;
                                btnStop.Text = "";
                            }
                        }
                        else
                        {
                            btnDelete.Enabled = false;
                            btnDelete.Text = "";
                            btnStop.Enabled = false;
                            btnStop.Text = "";
                        }
                    }
                }
            }
            catch
            { }
        }
    }
    
    protected void gvRDW_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strID = gvRDW.DataKeys[e.RowIndex].Values["RDW_MstrID"].ToString();

        if (rdw.DeleteRDWHeader(strID))
        {
            ltlAlert.Text = "window.location.href='/RDW/RDW_HeaderList.aspx?rm=" + DateTime.Now.ToString() + "';";
        }
        else
        {
            ltlAlert.Text = "alert('Delete data error!'); ";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_AddNew.aspx?rm=" + DateTime.Now.ToString(), true);
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //定义参数
        string strCateid = dropCatetory.SelectedValue;
        string strProj = txtProject.Text.Trim();
        string strProd = txtProjectCode.Text.Trim();
        string strSku = dropSKU.SelectedValue;
        string strStart = txtStartDate.Text.Trim();
        string strStatus = dropStatus.SelectedValue;
        string strMessage = string.Empty;
        string strUID = Convert.ToString(Session["uID"]);
        string keyword = txtKeyword.Text.Trim();
        string region = ddl_region.SelectedValue;
        string type = ddl_type.SelectedValue;
        bool canViewAll = true;

        if (!this.Security["170008"].isValid)
        {
            canViewAll = false;
        }
        //EngineerTeam
        //ltlAlert.Text = "var w=window.open('RDW_HeaderListExport.aspx?ProjName=" + strProj + "&Code=" + strProd + "&sku=" + strSku + "&cateid=" + strCateid +
        //   "&startdate=" + strStart + "&strUID=" + strUID + "&status=" + strStatus + "&canViewAll=" + canViewAll + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();";

        string title = "100^<b>Project Type</b>~^120^<b>Type</b>~^50^<b>Region</b>~^100^<b>Priority</b>~^100^<b>EngineerTeam</b>~^120^<b>Project Category</b>~^250^<b>Project Name</b>~^100^<b>EStar/DLC</b>~^150^<b>Project Code</b>~^200^<b>PPA</b>~^150^<b>Created Date</b>~^150^<b>Creater</b>~^200^<b>Prodject Describtion</b>~^200^<b>Start Date</b>~^<b>End Date</b>~^<b>Finish Date</b>~^<b>Status</b>~^30^<b>Stage</b>~^<b>Views</b>~^<b>Leaders</b>~^<b>EE</b>~^<b>ME</b>~^500^<b>key specification</b>~^200^<b>Notes</b>~^<b>Last Operator</b>~^<b>Last Operate Date</b>~^300^<b>Comments</b>~^80^<b>Tier</b>~^150^<b>Customer</b>~^";
        DataTable dt = rdw.SelectRDWListExport1(strCateid, strProj, strProd, strSku, strStart, strMessage, strStatus, strUID, keyword,region,type ,canViewAll);
        ExportExcel(title, dt, false);
        

    }
    protected void btnExportWithQad_Click(object sender, EventArgs e)
    {
        //定义参数
        string strCateid = dropCatetory.SelectedValue;
        string strProj = txtProject.Text.Trim();
        string strProd = txtProjectCode.Text.Trim();
        string strSku = dropSKU.SelectedValue;
        string strStart = txtStartDate.Text.Trim();
        string strStatus = dropStatus.SelectedValue;
        string strMessage = string.Empty;
        string strUID = Convert.ToString(Session["uID"]);
        string keyword = txtKeyword.Text.Trim();
        string region = ddl_region.SelectedValue;
        string type = ddl_type.SelectedValue;
        bool canViewAll = true;

        if (!this.Security["170008"].isValid)
        {
            canViewAll = false;
        }

        //ltlAlert.Text = "var w=window.open('RDW_HeaderListExport.aspx?ProjName=" + strProj + "&Code=" + strProd + "&sku=" + strSku + "&cateid=" + strCateid +
        //   "&startdate=" + strStart + "&strUID=" + strUID + "&status=" + strStatus + "&canViewAll=" + canViewAll + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus();";

        string title = "120^<b>Project Category</b>~^250^<b>Project Name</b>~^150^<b>Project Code</b>~^150^<b>Created Date</b>~^150^<b>Creater</b>~^200^<b>Prodject Describtion</b>~^200^<b>Start Date</b>~^<b>End Date</b>~^<b>Finish Date</b>~^<b>Status</b>~^<b>Views</b>~^<b>Leaders</b>~^500^<b>key specification</b>~^200^<b>Notes</b>~^110^<b>QAD No</b>~^80^<b>Approve</b>~^";
        DataTable dt = rdw.SelectRDWListWithQADExport(strCateid, strProj, strProd, strSku, strStart, strMessage, strStatus, strUID, keyword,region,type, canViewAll);
        ExportExcel(title, dt, false, 14, "ID");
    }

    protected void btnComments_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "$.window('Import Comments', 800, 600,'../RDW/RDW_ProjectArgueImport.aspx')";
    }
}
