using IT;
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
using System.Web.UI.WebControls.Expressions;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.SqlClient;
using System.Web.Mail;
using System.Text;
using Microsoft.Web.UI.WebControls;
using System.IO;

public partial class Page_Det : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindForm();
            //
            if (lbColunm.Text.ToLower() == "createby" || lbColunm.Text.ToLower() == "createname" || lbColunm.Text.ToLower() == "createdate"
                || lbColunm.Text.ToLower() == "modifyby" || lbColunm.Text.ToLower() == "modifyname" || lbColunm.Text.ToLower() == "modifydate")
            {
                chkImport.Enabled = false;
            }

            
        }
    }

    protected void BindForm()
    {
        string _colName = Request.QueryString["pk0"];
        string _pageID = Request.QueryString["pk1"];
        DataTable _table = PageMakerHelper.GetPageDet(_pageID, _colName);

        if (_table == null || _table.Rows.Count == 0)
        {
            this.Alert("获取明细数据失败！");
        }
        else
        {
            foreach (DataRow row in _table.Rows)
            {
                lbDb.Text = row["pm_db"].ToString();
                lbTable.Text = row["pm_table"].ToString();
                lbColunm.Text = row["pd_colName"].ToString();

                #region Information
                txtLabel.Text = row["pd_label"].ToString();
                txtDataType.Text = row["pd_dataType"].ToString();
                txtDataLength.Text = row["pd_dataLength"].ToString();
                chkPKey.Checked = Convert.ToBoolean(row["pd_isPrimaryKey"]);
                chkFKey.Checked = Convert.ToBoolean(row["pd_isForeignKey"]);
                chkIsNull.Checked = Convert.ToBoolean(row["pd_isNull"]);
                chkIsIdentify.Checked = Convert.ToBoolean(row["pd_isSelfGrowth"]);
                dropControl.Text = row["pd_field_control"].ToString();
                txtControlValue.Text = row["pd_field_controlValue"].ToString();
                txtDefValue.Text = row["pd_field_defaultValue"].ToString();
                txtFormatStr.Text = row["pd_field_format"].ToString();
                txtCssClass.Text = row["pd_cssClass"].ToString();
                #endregion

                #region Newing
                chkAdd.Checked = Convert.ToBoolean(row["pd_isAdd"]);
                txtRowIndex.Text = row["pd_add_rowIndex"].ToString();
                txtRowSpan.Text = row["pd_add_rowSpan"].ToString();
                txtRowHeight.Text = row["pd_add_rowHeight"].ToString();
                txtColIndex.Text = row["pd_add_colIndex"].ToString();
                txtColSpan.Text = row["pd_add_colSpan"].ToString();
                txtColWidht.Text = row["pd_add_colWidth"].ToString();
                #endregion

                #region Editting
                chkEdit.Checked = Convert.ToBoolean(row["pd_isEdit"]);
                #endregion

                #region Grid Criteria
                chkQuery.Checked = Convert.ToBoolean(row["pd_isQuery"]);
                txtQueryIndex.Text = row["pd_query_index"].ToString();
                txtQueryWidht.Text = row["pd_query_width"].ToString();
                chkVisible.Checked = Convert.ToBoolean(row["pd_isVisible"]);
                #endregion

                #region Grid View
                chkGrid.Checked = Convert.ToBoolean(row["ps_isGrid"]);
                txtGridWidth.Text = row["pd_grid_width"].ToString();
                dropGridAlign.Text = row["pd_grid_align"].ToString();
                txtFormatValue.Text = row["pd_grid_format"].ToString();

                chkLink.Checked = Convert.ToBoolean(row["pd_isLink"]);
                chkIsCustom.Checked = Convert.ToBoolean(row["pd_link_isCustom"]);
                txtLinkPage.Text = row["pd_link_pageID"].ToString();
                txtLinkParams.Text = row["pd_link_params"].ToString();
                dropLinkTarget.Text = row["pd_link_target"].ToString();
                txtLinkTitle.Text = row["pd_link_title"].ToString();

                #endregion

                #region Importing
                chkImport.Checked = Convert.ToBoolean(row["pd_isImport"]);
                txtImportIndex.Text = row["pd_import_index"].ToString();

                #endregion

                #region Exporting
                chkExport.Checked = Convert.ToBoolean(row["pd_isExport"]);
                txtExportWidth.Text = row["pd_export_width"].ToString();
                txtExportIndex.Text = row["pd_export_index"].ToString();
                #endregion

                #region Order By
                chkOrderBy.Checked = Convert.ToBoolean(row["pd_isOrderBy"]);
                txtOrderIndex.Text = row["pd_orderby_index"].ToString();
                #endregion
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Infomation
        string Label = string.Empty;
        string DataType = string.Empty;
        string DataLength = string.Empty;
        string PKey = string.Empty;
        string FKey = string.Empty;
        string IsNull = string.Empty;        
        string Control = string.Empty;
        string ControlValue = string.Empty;
        string DefValue = string.Empty;
        string FormatStr = txtFormatStr.Text;
        string CssClass = txtCssClass.Text;
        if (txtLabel.Text == string.Empty)
        {
            ltlAlert.Text = "alert('Label is not null!')";
            return;
        }
        else
        {
            Label = txtLabel.Text;
        }
        if (txtDataType.Text == string.Empty)
        {
            ltlAlert.Text = "alert('DataType is not null!')";
            return;
        }
        else
        {
            DataType = txtDataType.Text;
        }
        if (txtDataLength.Text == string.Empty)
        {
            ltlAlert.Text = "alert('DataLength Is Not Null!')";
            return;
        }
        else
        {
            DataLength = txtDataLength.Text;
        }
        if (chkPKey.Checked)
        {
            PKey = "1";
        }
        else
        {
            PKey = "0";
        }
        if (chkFKey.Checked)
        {
            FKey = "1";
        }
        else
        {
            FKey = "0";
        }
        if (chkIsNull.Checked)
        {
            IsNull = "1";
        }
        else
        {
            IsNull = "0";
            if (!chkImport.Checked && lbColunm.Text.ToLower() != "createname" && lbColunm.Text.ToLower() != "createby" && lbColunm.Text.ToLower() != "createdate"
                && lbColunm.Text.ToLower() != "modifyby" && lbColunm.Text.ToLower() != "modifyname" && lbColunm.Text.ToLower() != "modifydate")
            {
                ltlAlert.Text = "alert('Field is not empty, must be into the field!')";
                return;                
            }
        }
        if (dropControl.SelectedItem.ToString() == "--")
        {
            ltlAlert.Text = "alert('dropControl Is Not Null!')";
            return;
        }
        else
        {
            Control = dropControl.SelectedItem.ToString();
            if (dropControl.SelectedItem.ToString().Trim() == "DropDownList")
            {
                if (txtControlValue.Text == string.Empty)
                {
                    ltlAlert.Text = "alert('ControlValue Is Not Null!')";
                    return;
                }
            }
            ControlValue = txtControlValue.Text;
        }
        DefValue = txtDefValue.Text;
        #endregion

        #region Newing
        string Add = string.Empty;
        string RowIndex = string.Empty;
        string RowSpan = string.Empty;
        string RowHeight = string.Empty;
        string ColIndex = string.Empty;
        string ColSpan = string.Empty;
        string ColWidht = string.Empty;

        if (chkAdd.Checked)
        {
            Add = "1";
            if (txtRowHeight.Text == string.Empty || Convert.ToInt32(txtRowHeight.Text) == 0)
            {
                ltlAlert.Text = "alert('Row Height Is Not Null!')";
                return;
            }
            if (txtColWidht.Text == string.Empty || Convert.ToInt32(txtColWidht.Text) == 0)
            {
                ltlAlert.Text = "alert('Col Width Is Not Null!')";
                return;
            }
            if (chkIsIdentify.Checked)
            {
                ltlAlert.Text = "alert('Since the growth field is not allowed to add!')";
                return;
            }
            RowIndex = txtRowIndex.Text;
            RowSpan = txtRowSpan.Text;
            RowHeight = txtRowHeight.Text;
            ColIndex = txtColIndex.Text;
            ColSpan = txtColSpan.Text;
            ColWidht = txtColWidht.Text;
        }
        else
        {
            Add = "0";
        }
        #endregion

        #region Editting
        string Edit = string.Empty;
        if (chkEdit.Checked)
        {
            Edit = "1";
        }
        else
        {
            Edit = "0";
        }
        #endregion

        #region Grid Search Condition
        string Query = string.Empty;
        string QueryIndex= string.Empty;
        string QueryWidht = string.Empty;
        string Visible = string.Empty;
        if (chkQuery.Checked)
        {
            Query = "1";
            if (txtQueryIndex.Text == string.Empty)
            {
                ltlAlert.Text = "alert('QueryIndex Is Not Null!')";
                return;
            }
            QueryIndex = txtQueryIndex.Text;
            if (txtQueryWidht.Text == string.Empty)
            {
                ltlAlert.Text = "alert('QueryWidht Is Not Null!')";
                return;
            }
            QueryWidht = txtQueryWidht.Text;
        }
        else
        {
            Query = "0";
        }
        if (chkVisible.Checked)
        {
            //if (txtDefValue.Text == string.Empty)
            //{
            //    ltlAlert.Text = "alert('The field has no default value, not as a query!')";
            //    return;
            //}
            //else
            //{
            //    if (dropControl.SelectedItem.ToString() == "CheckBox")
            //    { 
            //        if(txtDefValue.Text.ToLower() != "0" || txtDefValue.Text.ToLower() != "1" || txtDefValue.Text.ToLower() != "true" || txtDefValue.Text.ToLower() != "false")
            //        {
            //            ltlAlert.Text = "alert('The space is checkbox, its value is only 0, 1, true, false !')";
            //            return;
            //        }
            //    }
            //    Visible = "1"; 
            //}
            Visible = "0";
        }
        else
        {
            Visible = "0";
        }
        #endregion

        #region Grid View
        string Grid = string.Empty;
        string GridWidth = string.Empty;
        string GridAlign = string.Empty;
        string FormatValue = string.Empty;
        string Link = string.Empty;
        string IsCustom = string.Empty;
        string LinkPage = string.Empty;
        string LinkParams = txtLinkParams.Text;
        string LinkTarget = string.Empty;
        string LinkTitle = txtLinkTitle.Text;
        FormatValue = txtFormatValue.Text;
        if (chkGrid.Checked)
        {
            Grid = "1";
            if (txtGridWidth.Text == string.Empty || Convert.ToInt32(txtGridWidth.Text) == 0)
            {
                ltlAlert.Text = "alert('GridWidth Is Not Null!')";
                return;
            }
            if (dropGridAlign.SelectedItem.ToString() == "--")
            {
                GridAlign = "Left";
            }
            else
            {
                GridAlign = dropGridAlign.SelectedItem.ToString();
            }
        }
        else
        {
            Grid = "0";
        }
        GridWidth = txtGridWidth.Text;
        if (chkLink.Checked)
        {
            Link = "1";
            if (txtLinkPage.Text == string.Empty)
            {
                ltlAlert.Text = "alert('Page/Url Is Not Null!')";
                return;
            }
        }
        else
        {
            Link = "0";
        }
        LinkPage = txtLinkPage.Text;
        if (chkIsCustom.Checked)
        {
            IsCustom = "1";
        }
        else
        {
            IsCustom = "0";
        }
        if (dropLinkTarget.SelectedItem.ToString() == "--")
        {
            LinkTarget = "_blank";
        }
        else
        {
            LinkTarget = dropLinkTarget.SelectedItem.ToString();
        }
        #endregion

        #region Importing
        string Import = string.Empty;
        string ImportIndex = string.Empty;
        if (chkImport.Checked)
        {
            if (chkIsIdentify.Checked)
            {
                ltlAlert.Text = "alert('Field is self growth not need import!')";
                return;
            }
            else
            { 
                if (txtImportIndex.Text == string.Empty)
                {
                    ltlAlert.Text = "alert('ImportIndex is not null!')";
                    return;
                }
                else
                {
                    Import = "1";
                    ImportIndex = txtImportIndex.Text;
                }
            }
        }
        else
        {
            Import = "0";
        }
        #endregion

        #region Exporting
        string Export = string.Empty;
        string ExportWidth = string.Empty;
        string ExportIndex = string.Empty;
        if (chkExport.Checked)
        {
            Export = "1";
            if (txtExportWidth.Text == string.Empty || Convert.ToInt32(txtExportWidth.Text) == 0)
            {
                ltlAlert.Text = "alert('ExportWidth Is Not Null!')";
                return;
            }
            else
            {
                ExportWidth = txtExportWidth.Text;
            }
            if (txtExportIndex.Text == string.Empty)
            {
                ltlAlert.Text = "alert('ExportIndex Is Not Null!')";
                return;
            }
            else
            {
                ExportIndex = txtExportIndex.Text;
            }
        }
        else
        {
            Export = "0";
        }
        #endregion

        #region Order By
        string OrderBy = string.Empty;
        string OrderIndex = string.Empty;
        if (chkOrderBy.Checked)
        {
            OrderBy = "1";
            if (txtOrderIndex.Text == string.Empty)
            {
                ltlAlert.Text = "alert('OrderIndex Is Not Null!')";
                return;
            }
            else
            {
                OrderIndex = txtOrderIndex.Text;
            }
        }
        else
        {
            OrderBy = "0";
        }
        #endregion
        if(SavePageDet(Label,DataType,DataLength,PKey,FKey,IsNull,Control,ControlValue,RowIndex,RowSpan,RowHeight,ColIndex,ColSpan,ColWidht,Edit
        , Query, QueryIndex, QueryWidht, Visible, Grid, GridWidth, GridAlign, Link, IsCustom, LinkPage, LinkParams, LinkTarget, LinkTitle
        , Import, ImportIndex, Export, ExportWidth
        , ExportIndex, OrderBy, OrderIndex, DefValue, FormatValue, FormatStr, CssClass,
        Request.QueryString["pk0"].ToString(), Request.QueryString["pk1"].ToString(), Add, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            ltlAlert.Text = "alert('Saved successfully!')";
            BindForm();
        }
    }
    private bool SavePageDet(string Label, string DataType, string DataLength, string PKey, string FKey, string IsNull, string Control
                        , string ControlValue, string RowIndex, string RowSpan, string RowHeigh, string ColIndex, string ColSpan, string ColWidht
                        , string Edit , string Query, string QueryIndex, string QueryWidht, string Visible, string Grid, string GridWidth, string GridAlign
                        , string Link, string IsCustom, string LinkPage, string LinkParams, string LinkTarget, string LinkTitle, string Import, string ImportIndex
                        , string Export, string ExportWidth, string ExportIndex, string OrderBy, string OrderIndex, string DefValue
                        , string FormatValue, string FormatStr, string CssClass
                        ,  string colname, string pageID
                        , string Add ,string uID,string uName)
    {
        SqlParameter []pram  = new SqlParameter[44];
        pram[0] = new SqlParameter("@Label",Label);        
        pram[1] = new SqlParameter("@DataType",DataType);        
        pram[2] = new SqlParameter("@DataLength",DataLength);
        pram[3] = new SqlParameter("@PKey",PKey);
        pram[4] = new SqlParameter("@FKey",FKey);
        pram[5] = new SqlParameter("@IsNull",IsNull);
        pram[6] = new SqlParameter("@Control",Control);
        pram[7] = new SqlParameter("@ControlValue",ControlValue);
        pram[8] = new SqlParameter("@RowIndex",RowIndex);
        pram[9] = new SqlParameter("@RowSpan",RowSpan);
        pram[10] = new SqlParameter("@RowHeigh",RowHeigh);
        pram[11] = new SqlParameter("@ColIndex",ColIndex);
        pram[12] = new SqlParameter("@ColSpan",ColSpan);
        pram[13] = new SqlParameter("@ColWidht",ColWidht);
        pram[14] = new SqlParameter("@Edit",Edit);
        pram[15] = new SqlParameter("@Query",Query);
        pram[16] = new SqlParameter("@QueryIndex",QueryIndex);
        pram[17] = new SqlParameter("@QueryWidht",QueryWidht);
        pram[18] = new SqlParameter("@Visible",Visible);
        pram[19] = new SqlParameter("@Grid",Grid);
        pram[20] = new SqlParameter("@GridWidth",GridWidth);
        pram[21] = new SqlParameter("@GridAlign",GridAlign);
        pram[22] = new SqlParameter("@Link",Link);
        pram[23] = new SqlParameter("@IsCustom",IsCustom);
        pram[24] = new SqlParameter("@LinkPage",LinkPage);
        pram[25] = new SqlParameter("@LinkParams",LinkParams);
        pram[26] = new SqlParameter("@LinkTarget",LinkTarget);



        pram[27] = new SqlParameter("@LinkTitle", LinkTitle);


        pram[28] = new SqlParameter("@Import",Import);
        pram[29] = new SqlParameter("@ImportIndex",ImportIndex);
        pram[30] = new SqlParameter("@Export",Export);
        pram[31] = new SqlParameter("@ExportWidth",ExportWidth);
        pram[32] = new SqlParameter("@ExportIndex",ExportIndex);
        pram[33] = new SqlParameter("@OrderBy",OrderBy);
        pram[34] = new SqlParameter("@OrderIndex", OrderIndex);
        pram[35] = new SqlParameter("@DefValue", DefValue);
        pram[36] = new SqlParameter("@FormatValue", FormatValue);
        pram[37] = new SqlParameter("@FormatStr", FormatStr);
        pram[38] = new SqlParameter("@CssClass", CssClass);
        pram[39] = new SqlParameter("@pageID", pageID);
        pram[40] = new SqlParameter("@colname", colname);
        pram[41] = new SqlParameter("@Add", Add);
        pram[42] = new SqlParameter("@uID", uID);
        pram[43] = new SqlParameter("@uName", uName);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, "sp_page_SavePageDet", pram));
    }
}