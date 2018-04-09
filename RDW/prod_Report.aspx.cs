using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;
using System.Collections.Generic;
using System.Configuration;
using ProdApp;
using RD_WorkFlow;
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
using System.Security.Principal;
using System.Collections.Generic;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Net.Mail;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


public partial class RDW_prod_Report : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];
    Prod prod = new Prod();
    RDW rdw = new RDW();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("1701114", "试流单取消权限，试流单取消权限");
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //如果from不为空，则表示从步骤页面链接，此时 code不可编辑，New按钮可见
            if (Request["from"] != null)
            {
                this.hidMID.Value = Request["mid"];
                this.hidDID.Value = Request["did"];
                txtCode.Text = Request["code"];
                txtProjectName.Text = Request["name"];
                txtCode.Enabled = false;
                txtProjectName.Enabled = false;
                if (this.hidDID.Value.ToString() == string.Empty)
                {
                    btnM5New.Visible = true;
                }
                else
                {
                    btnNew.Visible = true;
                }
            }
            else
            {
                btnNew.Visible = true;
            }
            txtOverDate.Enabled = false;
            txtOverDate.BackColor = System.Drawing.Color.LightGray;
            BindGridView();
            //步骤结束后，不可编辑  step 所有人都完成时不允许修改、新增
            RDW_Detail rd = rdw.SelectRDWDetailEdit(Request["did"], false);
            btnNew.Enabled = rd.RDW_Status != 2;
        }
    }
    protected override void BindGridView()
    {
        string status = string.Empty;
        DataTable dt = getAppList(txtProjectName.Text.Trim(), txtCode.Text.Trim(), txtNo.Text.Trim(), ddlStatus.SelectedValue.ToString()
                                , txtCreateDate1.Text.Trim(), txtPlanDate1.Text.Trim(), txtEndDate1.Text.Trim(), txtCreateDate2.Text.Trim()
                                , txtPlanDate2.Text.Trim(), txtEndDate2.Text.Trim(), txtOverDate.Text.Trim(), ddlType.SelectedValue.ToString(), ddlProdStatus.SelectedValue.ToString());
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable getAppList(string projectname, string prodname, string no, string status, string createdate1, string plandate1, string enddate1, string createdate2, string plandate2, string enddate2, string overdate, string type, string ProdStatus)
    {
        SqlParameter[] pram = new SqlParameter[20];
        pram[0] = new SqlParameter("@projectname", projectname);
        pram[1] = new SqlParameter("@prodname", prodname);
        pram[2] = new SqlParameter("@no", no);
        pram[3] = new SqlParameter("@status", status);
        pram[4] = new SqlParameter("@createdate1", createdate1);
        pram[5] = new SqlParameter("@plandate1", plandate1);
        pram[6] = new SqlParameter("@enddate1", enddate1);
        pram[7] = new SqlParameter("@createdate2", createdate2);
        pram[8] = new SqlParameter("@plandate2", plandate2);
        pram[9] = new SqlParameter("@enddate2", enddate2);
        pram[10] = new SqlParameter("@overdate", overdate);
        pram[11] = new SqlParameter("@type", type);
        pram[12] = new SqlParameter("@prodstatus", ProdStatus);

        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_prod_reachReport", pram).Tables[0];
    }
    protected void btnReach_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_Status"]) == 0)
            {
                e.Row.Cells[13].Text = "进行中";
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_Status"]) == 1)
            {
                e.Row.Cells[13].Text = "工单完工";
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_Status"]) == 2)
            {
                e.Row.Cells[13].Text = "步骤完成";
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_Status"]) == 3)
            {
                e.Row.Cells[13].Text = "项目取消";
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_Status"]) == 4)
            {
                e.Row.Cells[13].Text = "步骤关闭";
            }
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_Status"]) == 5)
            {
                e.Row.Cells[13].Text = "试流单取消";
            }
            if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["wo_nbr"]) != string.Empty)
            {
                e.Row.Cells[11].Text = string.Empty;
            }
            else
            {
                if (!this.Security["1701114"].isValid) //没有权限
                {
                    if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["prod_PlanDate"]) == string.Empty) //计划日期为空
                    {
                        if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["prod_CreateByName"]) != Session["uName"].ToString())
                        {
                            e.Row.Cells[11].Text = string.Empty;
                        }
                        else
                        {
                            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_Status"]) != 1)
                            {
                                e.Row.Cells[11].Text = string.Empty;
                            }
                        }
                    }
                    else //计划日期不为空
                    {
                        e.Row.Cells[11].Text = string.Empty;
                    }
                }
                else //有权限
                {
                    if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["prod_Status"]) != 0) //只有在进行中才可以取消
                    {
                        e.Row.Cells[11].Text = string.Empty;
                    }
                }
            }
            if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["prod_PlanDate"]) != string.Empty)
            {
                if (Convert.ToDateTime(gv.DataKeys[e.Row.RowIndex].Values["prod_PlanDate"]) <= Convert.ToDateTime("2000-01-01"))
                {
                    e.Row.Cells[7].Text = "";
                }
            }
        }
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "det")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            this.Redirect("/RDW/prod_ReportDetial.aspx?" + (Request["from"] == null ? "" : "from=rdw&") + "no=" + gv.DataKeys[index].Values["prod_No"].ToString() + "&name="
                                      + gv.DataKeys[index].Values["prod_ProjectName"].ToString() + "&code="
                                      + gv.DataKeys[index].Values["prod_Code"].ToString() + "&qad="
                                      + gv.DataKeys[index].Values["prod_QAD"].ToString() + "&pcb="
                                      + gv.DataKeys[index].Values["prod_PCB"].ToString() + "&planDate="
                                      + gv.DataKeys[index].Values["prod_PlanDate"].ToString() + "&endDate="
                                      + gv.DataKeys[index].Values["prod_EndDate"].ToString() + "&mid="
                                      + gv.DataKeys[index].Values["prod_mid"].ToString() + "&did="
                                      + gv.DataKeys[index].Values["prod_did"].ToString() + "&typeStatus="
                                      + gv.DataKeys[index].Values["prod_Status"] + "&prodid="
                                      + gv.DataKeys[index].Values["prod_id"]);
        } 
        if (e.CommandName == "test")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            this.Redirect("/RDW/Test_Report.aspx?" + (Request["from"] == null ? "" : "from=rdw&") + "prodno="
                                      + gv.DataKeys[index].Values["prod_No"].ToString() + "&projectname="
                                      + gv.DataKeys[index].Values["prod_ProjectName"].ToString() + "&projectcode="
                                      + gv.DataKeys[index].Values["prod_Code"].ToString() + "&mid="
                                      + gv.DataKeys[index].Values["prod_mid"].ToString() + "&did="
                                      + gv.DataKeys[index].Values["prod_did"].ToString() + "&prodid="
                                      + gv.DataKeys[index].Values["prod_id"]);
        }
        if (e.CommandName == "stepDet")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            if (gv.DataKeys[index].Values["prod_did"].ToString() == string.Empty)
            {
                string _no = gv.DataKeys[index].Values["prod_ProjectName"].ToString();
                this.Redirect("/product/m5_detail.aspx?from=report&no=" + _no + "&rt=" + DateTime.Now.ToFileTime().ToString());
            }
            else
            {
                this.Redirect("/RDW/prod_DetailEdit.aspx?" + (Request["from"] == null ? "" : "from=rdw&") + "no=" + gv.DataKeys[index].Values["prod_No"].ToString() + "&name="
                                          + gv.DataKeys[index].Values["prod_ProjectName"].ToString() + "&code="
                                          + gv.DataKeys[index].Values["prod_Code"].ToString() + "&qad="
                                          + gv.DataKeys[index].Values["prod_QAD"].ToString() + "&pcb="
                                          + gv.DataKeys[index].Values["prod_PCB"].ToString() + "&planDate="
                                          + gv.DataKeys[index].Values["prod_PlanDate"].ToString() + "&endDate="
                                          + gv.DataKeys[index].Values["prod_EndDate"].ToString() + "&mid="
                                          + gv.DataKeys[index].Values["prod_mid"].ToString() + "&did="
                                          + gv.DataKeys[index].Values["prod_did"].ToString() + "&typeStatus="
                                          + gv.DataKeys[index].Values["prod_Status"]);
            }
        }
        if (e.CommandName == "no")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            Response.Redirect("/RDW/prod_AppNew.aspx?" + (Request["from"] == null ? "" : "from=rdw&") + "no=" + gv.DataKeys[index].Values["prod_No"].ToString() + "&name="
                                + gv.DataKeys[index].Values["prod_ProjectName"].ToString() + "&code="
                                + gv.DataKeys[index].Values["prod_Code"].ToString() + "&qad="
                                + gv.DataKeys[index].Values["prod_QAD"].ToString() + "&pcb="
                                + gv.DataKeys[index].Values["prod_PCB"].ToString() + "&planDate="
                                + gv.DataKeys[index].Values["prod_PlanDate"].ToString() + "&endDate="
                                + gv.DataKeys[index].Values["prod_EndDate"].ToString() + "&mid="
                                + gv.DataKeys[index].Values["prod_mid"].ToString() + "&did="
                                + gv.DataKeys[index].Values["prod_did"].ToString() + "&status="
                                + gv.DataKeys[index].Values["prod_Status"].ToString());
        }
        if (e.CommandName == "cancel")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            Response.Redirect("/RDW/prod_cancelApp.aspx?type=cancel&" + (Request["from"] == null ? "" : "from=rdw&") + "no=" + gv.DataKeys[index].Values["prod_No"].ToString() + "&name="
                                + gv.DataKeys[index].Values["prod_ProjectName"].ToString() + "&code="
                                + gv.DataKeys[index].Values["prod_Code"].ToString() + "&qad="
                                + gv.DataKeys[index].Values["prod_QAD"].ToString() + "&pcb="
                                + gv.DataKeys[index].Values["prod_PCB"].ToString() + "&planDate="
                                + gv.DataKeys[index].Values["prod_PlanDate"].ToString() + "&endDate="
                                + gv.DataKeys[index].Values["prod_EndDate"].ToString() + "&mid="
                                + gv.DataKeys[index].Values["prod_mid"].ToString() + "&did="
                                + gv.DataKeys[index].Values["prod_did"].ToString());
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dtExport = new DataTable("Datas");
        dtExport.Columns.Add("prod_no", typeof(string));
        dtExport.Columns.Add("prod_projectName", typeof(string));
        dtExport.Columns.Add("prod_code", typeof(string));
        dtExport.Columns.Add("prod_qad", typeof(string));
        dtExport.Columns.Add("prod_pcb", typeof(string));
        dtExport.Columns.Add("prod_createName", typeof(string));
        dtExport.Columns.Add("prod_createDate", typeof(string));
        dtExport.Columns.Add("prod_planDate", typeof(string));
        dtExport.Columns.Add("prod_endDate", typeof(string));
        dtExport.Columns.Add("prod_status", typeof(string));
        dtExport.Columns.Add("wo_nbr", typeof(string));
        dtExport.Columns.Add("wo_domain", typeof(string));
        dtExport.Columns.Add("wo_part", typeof(string));
        dtExport.Columns.Add("wo_status", typeof(string));
        dtExport.Columns.Add("wo_rel_date", typeof(string));
        dtExport.Columns.Add("wo_qty_ord", typeof(string));
        dtExport.Columns.Add("wo_qty_comp", typeof(string));
        dtExport.Columns.Add("Massage", typeof(string));
        DataTable dt = getAppList(txtProjectName.Text, txtCode.Text, txtNo.Text, ddlStatus.SelectedValue.ToString()
                        , txtCreateDate1.Text, txtPlanDate1.Text, txtEndDate1.Text, txtCreateDate2.Text, txtPlanDate2.Text, txtEndDate2.Text, txtOverDate.Text, ddlType.SelectedValue.ToString(), ddlProdStatus.SelectedValue.ToString());

        foreach (DataRow row in dt.Rows)
        {
            DataRow rows = dtExport.NewRow();

            rows["prod_no"] = row["prod_no"].ToString(); //从总的Datatable中读取行数据赋值给新的Datatable
            rows["prod_projectName"] = row["prod_projectName"].ToString();
            rows["prod_code"] = row["prod_code"].ToString();
            rows["prod_qad"] = row["prod_qad"].ToString();
            rows["prod_pcb"] = row["prod_pcb"].ToString();
            rows["prod_createName"] = row["prod_createByName"].ToString();
            rows["prod_createDate"] = row["prod_createByDate"].ToString();
            rows["prod_planDate"] = row["prod_planDate"].ToString();
            rows["prod_endDate"] = row["prod_endDate"].ToString();
            rows["prod_status"] = row["prod_status"].ToString();
            rows["wo_nbr"] = row["wo_nbr"].ToString();
            rows["wo_domain"] = row["wo_domain"].ToString();
            rows["wo_part"] = row["wo_part"].ToString();
            rows["wo_status"] = row["wo_status"].ToString();
            rows["wo_rel_date"] = row["wo_rel_date"].ToString();
            rows["wo_qty_ord"] = row["wo_qty_ord"].ToString();
            rows["wo_qty_comp"] = row["wo_qty_comp"].ToString();
            rows["Massage"] = "";

            dtExport.Rows.Add(rows);//添加主行
            //以下插入Message
            string strDID = row["prod_did"].ToString();
            //获取消息
            string Message = SelectRDWDetailMessage(strDID);
            DataRow rws = dtExport.NewRow();
            rws["Massage"] = Message;
            dtExport.Rows.Add(rws);//添加消息列
        }
        if (dtExport.Rows.Count <= 0)
        {
            ltlAlert.Text = "alert('无数据！')";
            return;
        }

        string title = "100^<b>跟踪单号</b>~^250^<b>项目名称</b>~^160^<b>项目代码</b>~^120^<b>QAD</b>~^120^<b>PCB</b>~^80^<b>创建人</b>~^90^<b>创建日期</b>~^90^<b>计划日期</b>~^90^<b>截止日期</b>~^60^<b>状态</b>~^90^<b>加工单</b>~^90^<b>域</b>~^90^<b>物料</b>~^90^<b>工单状态</b>~^90^<b>下达日期</b>~^90^<b>订单数量</b>~^90^<b>完成数量</b>~^1000^<b>消息</b>~^";
        this.ExportExcel(title, dtExport, false);
    }
    private string SelectRDWDetailMessage(string strDID)
    {
        try
        {
            string str = "sp_prod_SelectRdwDetailMessage";
            SqlParameter param = new SqlParameter("@did", strDID);

            return Convert.ToString(SqlHelper.ExecuteScalar(strConn,CommandType.StoredProcedure,str,param));
        }
        catch
        {
            return "";
        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlStatus.SelectedValue.ToString()) == 2 || Convert.ToInt32(ddlStatus.SelectedValue.ToString()) == 3)
        {
            txtOverDate.Enabled = true;
            txtOverDate.BackColor = System.Drawing.Color.White;
        }
        else
        {
            txtOverDate.Text = string.Empty;
            txtOverDate.Enabled = false;
            txtOverDate.BackColor = System.Drawing.Color.LightGray;
        }
        BindGridView();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/prod_AppNew.aspx?" + (Request["from"] == null ? "" : "from=rdw&") + "name=" + txtProjectName.Text + "&code=" + txtCode.Text + "&mid=" + Request["mid"] + "&did=" + Request["did"]);
    }
    protected void btnExportLine_Click(object sender, EventArgs e)
    {
        DataTable dtExport = new DataTable("Datas");
        dtExport.Columns.Add("prod_no", typeof(string));
        dtExport.Columns.Add("prod_projectName", typeof(string));
        dtExport.Columns.Add("prod_code", typeof(string));
        dtExport.Columns.Add("prod_qad", typeof(string));
        dtExport.Columns.Add("prod_pcb", typeof(string));
        dtExport.Columns.Add("prod_createName", typeof(string));
        dtExport.Columns.Add("prod_createDate", typeof(string));
        dtExport.Columns.Add("prod_planDate", typeof(string));
        dtExport.Columns.Add("prod_endDate", typeof(string));
        dtExport.Columns.Add("prod_status", typeof(string));
        dtExport.Columns.Add("wo_nbr", typeof(string));
        dtExport.Columns.Add("wo_domain", typeof(string));
        dtExport.Columns.Add("wo_part", typeof(string));
        dtExport.Columns.Add("wo_status", typeof(string));
        dtExport.Columns.Add("wo_rel_date", typeof(string));
        dtExport.Columns.Add("wo_qty_ord", typeof(string));
        dtExport.Columns.Add("wo_qty_comp", typeof(string));
        dtExport.Columns.Add("MassageType", typeof(string));
        dtExport.Columns.Add("Massage", typeof(string));
        DataTable dt = getAppList(txtProjectName.Text, txtCode.Text, txtNo.Text, ddlStatus.SelectedValue.ToString()
                        , txtCreateDate1.Text, txtPlanDate1.Text, txtEndDate1.Text, txtCreateDate2.Text, txtPlanDate2.Text, txtEndDate2.Text, txtOverDate.Text, ddlType.SelectedValue.ToString(), ddlProdStatus.SelectedValue.ToString());

        foreach (DataRow row in dt.Rows)
        {
            //DataRow rows = dtExport.NewRow();

            //rows["prod_no"] = row["prod_no"].ToString(); //从总的Datatable中读取行数据赋值给新的Datatable
            //rows["prod_projectName"] = row["prod_projectName"].ToString();
            //rows["prod_code"] = row["prod_code"].ToString();
            //rows["prod_qad"] = row["prod_qad"].ToString();
            //rows["prod_pcb"] = row["prod_pcb"].ToString();
            //rows["prod_createName"] = row["prod_createByName"].ToString();
            //rows["prod_createDate"] = row["prod_createByDate"].ToString();
            //rows["prod_planDate"] = row["prod_planDate"].ToString();
            //rows["prod_endDate"] = row["prod_endDate"].ToString();
            //rows["prod_status"] = row["prod_status"].ToString();
            //rows["wo_nbr"] = row["wo_nbr"].ToString();
            //rows["wo_domain"] = row["wo_domain"].ToString();
            //rows["wo_part"] = row["wo_part"].ToString();
            //rows["wo_status"] = row["wo_status"].ToString();
            //rows["wo_rel_date"] = row["wo_rel_date"].ToString();
            //rows["wo_qty_ord"] = row["wo_qty_ord"].ToString();
            //rows["wo_qty_comp"] = row["wo_qty_comp"].ToString();
            //rows["MassageType"] = "";
            //rows["Massage"] = "";

            //dtExport.Rows.Add(rows);//添加次行
            //以下插入Message
            string strDID = row["prod_did"].ToString();
            //获取消息列表
            DataTable Message = rdw.SelectProdMassage(row["prod_no"].ToString());
            int i = 0;
            int j = 0;
            string MassageType = string.Empty;
            foreach(DataRow rs in Message.Rows)
            {
                DataRow rws = dtExport.NewRow();

                rws["prod_no"] = row["prod_no"].ToString(); //从总的Datatable中读取行数据赋值给新的Datatable
                rws["prod_projectName"] = row["prod_projectName"].ToString();
                rws["prod_code"] = row["prod_code"].ToString();
                rws["prod_qad"] = row["prod_qad"].ToString();
                rws["prod_pcb"] = row["prod_pcb"].ToString();
                rws["prod_createName"] = row["prod_createByName"].ToString();
                rws["prod_createDate"] = row["prod_createByDate"].ToString();
                rws["prod_planDate"] = row["prod_planDate"].ToString();
                rws["prod_endDate"] = row["prod_endDate"].ToString();
                rws["prod_status"] = row["prod_status"].ToString();
                rws["wo_nbr"] = row["wo_nbr"].ToString();
                rws["wo_domain"] = row["wo_domain"].ToString();
                rws["wo_part"] = row["wo_part"].ToString();
                rws["wo_status"] = row["wo_status"].ToString();
                rws["wo_rel_date"] = row["wo_rel_date"].ToString();
                rws["wo_qty_ord"] = row["wo_qty_ord"].ToString();
                rws["wo_qty_comp"] = row["wo_qty_comp"].ToString();

                rws["MassageType"] = rs["deptname"].ToString();
                rws["Massage"] = rs["Massage"].ToString();
                /*
                if (rs["deptname"].ToString().Trim() != MassageType.Trim())
                {
                    MassageType = rs["deptname"].ToString();
                    i = 0;
                }
                else
                {
                    MassageType = rs["deptname"].ToString();
                    i = 1;
                }
                if (i != 0)
                {
                    rws["MassageType"] = string.Empty;
                }
                else
                {
                    rws["MassageType"] = rs["deptname"].ToString();
                }
                 */
                dtExport.Rows.Add(rws);
                j += 1;
            }
        }
        if (dtExport.Rows.Count <= 0)
        {
            ltlAlert.Text = "alert('无数据！')";
            return;
        }

        string title = "100^<b>跟踪单号</b>~^250^<b>项目名称</b>~^160^<b>项目代码</b>~^120^<b>QAD</b>~^120^<b>PCB</b>~^80^<b>创建人</b>~^90^<b>创建日期</b>~^90^<b>计划日期</b>~^90^<b>截止日期</b>~^60^<b>状态</b>~^90^<b>加工单</b>~^90^<b>域</b>~^90^<b>物料</b>~^90^<b>工单状态</b>~^90^<b>下达日期</b>~^90^<b>订单数量</b>~^90^<b>完成数量</b>~^100^<b>消息类型</b>~^1000^<b>消息</b>~^";
        
        //this.ExportExcel(title, dtExport, false);
        //this.ExportExcels(title, dtExport, false, 1, 0, "prod_no");

        //this.ExportExcels(title, dtExport, false, 18, 0, "MassageType");
        ExportExcels(title, dtExport, false, 17, 0, "prod_no");
        //this.ExportExcel(title, dtExport, false);
    }
    protected void btnM5New_Click(object sender, EventArgs e)
    {
        Response.Redirect("/product/m5_appNew.aspx?" + (Request["from"] == null ? "" : "from=rdw&") + "no=" + txtProjectName.Text);
    }
    private class ExcelTitle
    {
        private string _name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        private int _width;
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }

        public ExcelTitle(string name, int width)
        {
            this.Name = name;
            this.Width = width;
        }

        public ExcelTitle()
        {

        }
    }
    public void ExportExcels(string EXTitle, DataTable EXData, bool fullDateFormat, int mainColCount, int i, params string[] keyFlieds)
    {
        IWorkbook workbook = new HSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("excel");

        IList<ExcelTitle> ItemList = GetExcelTitles(EXTitle);
        int total = ItemList.Count;

        DataTable dt = EXData;
        DataTable dt1 = dt;

        //头栏样式
        ICellStyle styleHeader = SetHeaderStyle(workbook);

        //写标题栏
        IRow rowHeader = sheet.CreateRow(0);
        SetColumnTitleAndStyle(workbook, sheet, ItemList, dt, styleHeader, rowHeader, fullDateFormat);

        if (i == 0)
        {
            SetDetailsValueMerged(sheet, total, dt, 1, mainColCount, fullDateFormat, keyFlieds);
            i = 1;
            dt1 = dt;
        }
        if (i == 1)
        {
            //头栏样式
            ICellStyle styleHeaders = SetHeaderStyle(workbook);

            //写标题栏
            IRow rowHeaders = sheet.CreateRow(0);
            SetColumnTitleAndStyle(workbook, sheet, ItemList, dt1, styleHeaders, rowHeaders, fullDateFormat);

            string[] keyFlied = { "MassageType" };
            SetDetailsValueMerged(sheet, total, dt1, 1, 18, fullDateFormat, keyFlied);
            dt1.Reset();
        }

        dt.Reset();
        string _localFileName = string.Format("{0}.xls", DateTime.Now.ToFileTime().ToString());

        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);

            Stream localFile = new FileStream(Server.MapPath("/Excel/") + _localFileName, FileMode.OpenOrCreate);
            localFile.Write(ms.ToArray(), 0, (int)ms.Length);
            localFile.Dispose();
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            workbook = null;
        }

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ExportExcel", "<script language=\"JavaScript\" type=\"text/javascript\">window.open('/Excel/" + _localFileName + "', '_blank', 'width=800,height=600,top=0,left=0');</script>");
    }
    private void SetDetailsValueMerged(ISheet sheet, int total, DataTable dt, int startRowIndex, int mainColCount, bool fullDateFormat, params string[] keyFields)
    {
        bool mainIsNew = false;
        string[] keyValue = new string[keyFields.Length];
        int mergeStartRowIndex = startRowIndex;
        int n = startRowIndex;
        if (dt.Columns.Count >= total)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + startRowIndex);

                //string[] rowKeyValue = new string[keyFields.Length];
                for (int j = 0; j <= keyFields.Length - 1; j++)
                {
                    if (keyValue[j] != dt.Rows[i][keyFields[j]].ToString().Trim())
                    {
                        mainIsNew = true;
                        keyValue[j] = dt.Rows[i][keyFields[j]].ToString().Trim();
                    }
                }
                if (mainIsNew)
                {
                    if (i > 0 && mergeStartRowIndex != n - 1)
                    {
                        for (int j = 0; j < mainColCount; j++)
                        {
                            sheet.AddMergedRegion(new CellRangeAddress(mergeStartRowIndex, n - 1, j, j));
                        }
                    }
                    mergeStartRowIndex = n;
                    mainIsNew = false;
                }
                for (int j = 1; j <= total; j++)
                {
                    ICell cell = row.CreateCell(j - 1);
                    int _col1 = j - 1 + (dt.Columns.Count - total);
                    cell.SetCellValue(dt.Rows[i][_col1], fullDateFormat);
                }
                n++;
            }

            if (mergeStartRowIndex != n - 1)
            {
                for (int j = 0; j < mainColCount; j++)
                {
                    sheet.AddMergedRegion(new CellRangeAddress(mergeStartRowIndex, n - 1, j, j));
                }
            }
        }
    }
    private IList<ExcelTitle> GetExcelTitles(string EXTitle)
    {
        var ItemList = new List<ExcelTitle>();

        string str = EXTitle;
        int total = 0;
        int ind = 0;
        while (str.Length > 0)
        {
            ind = str.IndexOf("~^");
            if (ind == -1)
            {
                total = total + 1;
                str = "";
                break;
            }
            total = total + 1;
            str = str.Substring(ind + 2);
        }

        str = EXTitle;

        for (int i = 0; i <= total - 1; i++)
        {
            ExcelTitle item = new ExcelTitle();
            int width = 100 * 6000 / 164;
            ind = str.IndexOf("~^");
            if (ind == -1)
            {
                ind = str.IndexOf("L~");
                if (ind > -1)
                {
                    str = str.Substring(2);
                }

                ind = str.IndexOf("^");
                if (ind == -1)
                {
                    item.Name = str.Substring(2);
                    item.Width = width;
                }
                else
                {
                    item.Name = str.Substring(ind + 1);
                    item.Width = Convert.ToInt32(str.Substring(0, ind)) * 6000 / 164;
                }
                str = "";
                break;
            }
            else
            {
                item.Name = str.Substring(0, ind);
                item.Width = width;
                str = str.Substring(ind + 2);

                ind = item.Name.IndexOf("L~");
                if (ind > -1)
                {
                    item.Name = item.Name.Substring(2);
                }

                ind = item.Name.IndexOf("^");
                if (ind > -1)
                {
                    item.Width = Convert.ToInt32(item.Name.Substring(0, ind)) * 6000 / 164;
                    item.Name = item.Name.Substring(ind + 1);
                }
            }

            item.Name = item.Name.Replace("<b>", "").Replace("</b>", "");
            ItemList.Add(item);
        }
        return ItemList;
    }
    private ICellStyle SetHeaderStyle(IWorkbook workbook)
    {
        ICellStyle styleHeader = workbook.CreateCellStyle();
        styleHeader.Alignment = HorizontalAlignment.Center;//居中对齐

        styleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
        styleHeader.FillPattern = FillPattern.SolidForeground;

        styleHeader.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        IFont fontHeader = workbook.CreateFont();
        fontHeader.FontHeightInPoints = 10;
        fontHeader.Boldweight = 600;
        styleHeader.SetFont(fontHeader);

        return styleHeader;
    }
    private void SetColumnTitleAndStyle(IWorkbook workbook, ISheet sheet, IList<ExcelTitle> ItemList, DataTable dt, ICellStyle styleHeader, IRow rowHeader, bool fullDateFormat)
    {
        int total = ItemList.Count;
        foreach (ExcelTitle item in ItemList)
        {
            int titleIndex = ItemList.IndexOf(item);
            sheet.SetColumnWidth(titleIndex, item.Width);

            ICell cell = rowHeader.CreateCell(titleIndex);
            cell.CellStyle = styleHeader;
            cell.SetCellValue(item.Name);

            int dtCol = 0;

            if (dt.Columns.Count == total)
            {
                dtCol = titleIndex;
            }
            else
            {
                dtCol = titleIndex + (dt.Columns.Count - total);
            }

            ICellStyle columnStyle = SetColumnStyleByDataType(workbook, dt.Columns[dtCol].DataType.ToString(), fullDateFormat);
            sheet.SetDefaultColumnStyle(titleIndex, columnStyle);
        }
    }
    private ICellStyle SetColumnStyleByDataType(IWorkbook workbook, string dataType, bool fullDateFormat)
    {
        ICellStyle style = workbook.CreateCellStyle();
        IFont font = workbook.CreateFont();
        style.VerticalAlignment = VerticalAlignment.Center;
        IDataFormat dataFormat = workbook.CreateDataFormat();
        short formatIndex;
        if (fullDateFormat)
        {
            formatIndex = dataFormat.GetFormat("yyyy-MM-dd hh:mm:ss");
        }
        else
        {
            formatIndex = dataFormat.GetFormat("yyyy-MM-dd");
        }
        switch (dataType)
        {
            case "System.DateTime":
                style.Alignment = HorizontalAlignment.Center;
                style.DataFormat = formatIndex;
                break;
            case "System.Int16":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Int32":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Int64":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Decimal":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Double":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Boolean":
                style.Alignment = HorizontalAlignment.Center;
                break;
            case "System.String":
                style.Alignment = HorizontalAlignment.Left;
                style.WrapText = true;
                break;
        }
        style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        font.FontHeightInPoints = 9;
        style.SetFont(font);
        return style;
    }

}