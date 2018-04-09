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
using QCProgress;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using IT;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

/// <summary>
///CheckBox模板列
/// </summary>
public class GridViewCheckBoxTemplate : System.Web.UI.ITemplate
{
    private DataControlRowType templateType;
    private string columnName;
    private string cId;
    public GridViewCheckBoxTemplate(DataControlRowType type, string colname, string controlId)
    {
        templateType = type;
        columnName = colname;
        cId = controlId;
    }
    public void InstantiateIn(System.Web.UI.Control container)
    {
        switch (templateType)
        {
            case DataControlRowType.Header:
                CheckBox chkAll = new CheckBox();
                chkAll.ID = cId;
                chkAll.Width = 20;
                container.Controls.Add(chkAll);
                break;
            case DataControlRowType.DataRow:
                CheckBox chkItem = new CheckBox();
                chkItem.ID = cId;
                chkItem.DataBinding += new EventHandler(this.CheckBoxDataBinding);
                chkItem.Width = 20;
                container.Controls.Add(chkItem);
                break;
            default: break;
        }
    }
    private void CheckBoxDataBinding(Object sender, EventArgs e)
    {
        CheckBox myCheckBox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)myCheckBox.NamingContainer;
        //myCheckBox.Checked = Convert.ToBoolean(System.Web.UI.DataBinder.Eval(row.DataItem, columnName));
    }

}

public partial class Page_Viewer : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["pageID"] == null)
            {
                this.Alert("缺少pageID参数，无法执行本程序！");
                return;
            }
            this.hidPageID.Value = Request.QueryString["pageID"];
            this.hidBackID.Value = Request.QueryString["pagefrom"] != null ? Request.QueryString["pagefrom"] : string.Empty;
            this.hidDB.Value = Request.QueryString["db"] != null ? Request.QueryString["db"] : string.Empty;
            this.hidTable.Value = Request.QueryString["table"] != null ? Request.QueryString["table"] : string.Empty;

            CreateQueryControls();
            #region 决定页面首次是否自动呈现数据
            DataTable _tbMstr = PageMakerHelper.GetPageMstr(this.hidPageID.Value);
            foreach (DataRow rows in _tbMstr.Rows)
            {
                if (rows["pm_isPostBack"].ToString().ToLower() == "true")
                {
                    BindGridView();
                }
            }
            #endregion
            btnBack.Visible = Request.QueryString["pagefrom"] != null;
        }
        else
        {
            btnBack.Visible = Request.QueryString["pagefrom"] != null;
            CreateQueryControls();
        }
    }
    /// <summary>
    /// 将系统规定的参数组合，转换
    /// </summary>
    /// <param name="oldValue"></param>
    /// <returns></returns>
    protected string SwitchDefaultValue(string oldValue)
    {
        string newValue = string.Empty;
        switch (oldValue.Trim().ToLower())
        {
            case "{uid}": newValue = Session["uID"].ToString(); break;
            case "{uname}": newValue = Session["uName"].ToString(); break;
            case "{uno}": newValue = Session["loginName"].ToString(); break;
            case "{plantcode}": newValue = Session["PlantCode"].ToString(); break;

            case "{today}": newValue = DateTime.Now.ToString("yyyy-MM-dd"); break;

            case "{prevweek}": newValue = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"); break;
            case "{nextweek}": newValue = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"); break;

            case "{prevmonth}": newValue = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01"); break;
            case "{thismonth}": newValue = DateTime.Now.Month.ToString(); break;
            case "{nextmonth}": newValue = DateTime.Now.AddMonths(1).ToString("yyyy-MM-01"); break;

            case "{thisyear}": newValue = DateTime.Now.Year.ToString(); break;
            default: newValue = oldValue; break;
        }

        return newValue;
    }
    /// <summary>
    /// 组织显示查询条件
    /// </summary>
    protected void CreateQueryControls()
    {
        DataTable _pageMstr = PageMakerHelper.GetPageMstr(this.hidPageID.Value);
        if (_pageMstr == null || _pageMstr.Rows.Count == 0)
        {
            this.Alert("主记录维护有误！");
            return;
        }

        #region 这里判断新增、导出、导入按钮是否可见
        btnNew.Visible = Convert.ToBoolean(_pageMstr.Rows[0]["pm_couldNew"]);
        btnExport.Visible = Convert.ToBoolean(_pageMstr.Rows[0]["pm_couldExport"]);
        #endregion

        btnQuery.Visible = false;
        DataTable _tblHeaders = null;
        DataTable _tblQuerys = PageMakerHelper.GetQueryFields(hidPageID.Value);
        #region 合法性判断
        /*
         * pageID永远都不能为null
         * pagefrom=null 表示该页面不是明细页面
         * pagefrom!=null 表示该页面是明细页面
         * pageID=pagefrom 表示外部调用，相当于明细页面
         */
        if (Request.QueryString["pagefrom"] != null)
        {
            //_tblHeaders = PageMakerHelper.GetHeaderFields(hidPageID.Value);
            //if (_tblHeaders == null)
            //{
            //    this.Alert("获取头栏字段时出错，请联系管理员！");
            //    return;
            //}
        }

        if (_tblQuerys == null)
        {
            this.Alert("获取查询字段时出错，请联系管理员！");
            return;
        }
        #endregion

        int nRow = 0;//如果有头栏的话，则头栏在行1，查询条件在行2

        if (_tblHeaders != null)
        {
            foreach (DataRow row in _tblHeaders.Rows)
            {
                tblQuery.Rows.Insert(0, new HtmlTableRow());
                nRow = 1;
                #region 头栏

                #endregion
            }
        }

        foreach (DataRow row in _tblQuerys.Rows)
        {
            #region 查询条件
            btnQuery.Visible = true;
            //设置控件类型
            HtmlTableCell cell;
            if (row["pd_field_control"].ToString().ToLower() == "checkbox")
            {
                #region checkbox
                CheckBox chk = new CheckBox();
                chk.ID = row["pd_colName"].ToString();
                //设置默认值
                chk.Checked = row["pd_field_defaultValue"].ToString().ToLower() == "true";

                cell = new HtmlTableCell();
                cell.Controls.Add(chk);
                // tblQuery.Rows[0].Cells.Insert(1, cell);

                //设置标签
                Label lbl = new Label();
                lbl.Text = row["pd_label"].ToString();
                cell.Controls.Add(lbl);
                tblQuery.Rows[nRow].Cells.Insert(1, cell);
                #endregion
            }
            else if (row["pd_field_control"].ToString().ToLower() == "textbox")
            {
                #region textbox
                TextBox txt = new TextBox();
                txt.ID = row["pd_colName"].ToString();
                txt.MaxLength = Convert.ToInt32(row["pd_dataLength"]);
                if (Convert.ToInt32(row["pd_query_width"]) > 0)
                {
                    txt.Width = Unit.Pixel(Convert.ToInt32(row["pd_query_width"]));
                }
                #region 设置默认值
                txt.Text = this.SwitchDefaultValue(row["pd_field_defaultValue"].ToString());
                #endregion
                #region 根据数据类型，决定固定样式
                switch (row["pd_dataType"].ToString().ToLower())
                {
                    case "int": txt.CssClass += " Integer "; break;
                    case "decimal": txt.CssClass += " Decimal "; break;
                    case "datetime": txt.CssClass += " Date "; break;
                    // 如果是nvarchar  和  varchar 则样式是什么
                    //case "nvarchar": txt.CssClass += " Date "; break;
                    //case "varchar": txt.CssClass += " Date "; break;
                }
                #endregion
                #region 开发人员自己编写的独占类
                if (!string.IsNullOrEmpty(row["pd_cssClass"].ToString()))
                {
                    txt.CssClass += row["pd_cssClass"].ToString();
                }
                #endregion

                cell = new HtmlTableCell();
                cell.Controls.Add(txt);
                tblQuery.Rows[nRow].Cells.Insert(1, cell);

                //设置标签 
                cell = new HtmlTableCell();
                cell.InnerText = row["pd_label"].ToString();
                tblQuery.Rows[nRow].Cells.Insert(1, cell);
                #endregion
            }
            else if (row["pd_field_control"].ToString().ToLower() == "textarea")
            {
                #region textarea
                TextBox txt = new TextBox();
                txt.ID = row["pd_colName"].ToString();
                txt.MaxLength = Convert.ToInt32(row["pd_dataLength"]);
                txt.TextMode = TextBoxMode.MultiLine;
                txt.Height = Unit.Percentage(100);
                if (Convert.ToInt32(row["pd_query_width"]) > 0)
                {
                    txt.Width = Unit.Pixel(Convert.ToInt32(row["pd_query_width"]));
                }
                txt.Text = row["pd_field_defaultValue"].ToString();

                cell = new HtmlTableCell();
                cell.Controls.Add(txt);
                tblQuery.Rows[nRow].Cells.Insert(1, cell);

                //设置标签 
                cell = new HtmlTableCell();
                cell.InnerText = row["pd_label"].ToString();
                tblQuery.Rows[nRow].Cells.Insert(1, cell);
                #endregion
            }
            else if (row["pd_field_control"].ToString().ToLower() == "dropdownlist")
            {
                #region dropdownlist
                string _dropValue = PageMakerHelper.GetFieldData(hidPageID.Value, row["pd_colName"].ToString(), "pd_field_controlValue");
                if (!string.IsNullOrEmpty(_dropValue))
                {
                    #region pd_gridDrop不为空，按Dropdown显示
                    DropDownList drop = new DropDownList();
                    drop.ID = row["pd_colName"].ToString();
                    drop.Width = Unit.Pixel(Convert.ToInt32(row["pd_query_width"]) == 0 ? 100 : Convert.ToInt32(row["pd_query_width"]));
                    drop.Items.Add(new ListItem("--", "0"));
                    //如果包含“;”，则取值，否则认为要执行SQL语句
                    if (_dropValue.IndexOf(';') > 0)
                    {
                        foreach (string val in _dropValue.Split(';'))
                        {
                            drop.Items.Add(new ListItem(val, val));
                        }
                    }
                    else
                    {
                        try
                        {
                            _dropValue = _dropValue.Replace("{uID}", "'" + Session["uID"].ToString() + "'");
                            _dropValue = _dropValue.Replace("{uName}", "N'" + Session["uName"].ToString() + "'");
                            _dropValue = _dropValue.Replace("{PlantCode}", Session["PlantCode"].ToString());
                            _dropValue = _dropValue.Replace("tcpcx", "tcpc" + Session["PlantCode"].ToString());
                            DataTable table = SqlHelper.ExecuteDataset(strConn, CommandType.Text, _dropValue).Tables[0];

                            if (table != null || table.Rows.Count > 0)
                            {
                                foreach (DataRow r in table.Rows)
                                {
                                    if (table.Columns.Count == 1)
                                    {
                                        drop.Items.Add(new ListItem(r[0].ToString(), r[0].ToString()));
                                    }
                                    else
                                    {
                                        drop.Items.Add(new ListItem(r[1].ToString(), r[0].ToString()));
                                    }
                                }
                            }
                        }
                        catch
                        {
                            this.Alert("执行SQL语句时出错，请联系管理员！");
                            return;
                        }
                    }

                    #region 设置默认值
                    try
                    {
                        drop.SelectedIndex = -1;
                        drop.Items.FindByValue(this.SwitchDefaultValue(row["pd_field_defaultValue"].ToString())).Selected = true;
                    }
                    catch
                    {
                        drop.SelectedIndex = -1;
                    }
                    #endregion

                    cell = new HtmlTableCell();
                    cell.Controls.Add(drop);
                    tblQuery.Rows[nRow].Cells.Insert(1, cell);
                    #endregion
                }
                else
                {
                    this.Alert("控件指定为DropDownList，但未指定值");
                    return;
                }

                //设置标签 
                cell = new HtmlTableCell();
                cell.InnerText = row["pd_label"].ToString();
                tblQuery.Rows[nRow].Cells.Insert(1, cell);
                #endregion
            }
            #endregion
        }
    }
    /// <summary>
    /// 组织GridView列显示
    /// </summary>
    protected void CreateGridViewColunms(DataTable _pageMstr)
    {
        try
        {
            string strName = "sp_page_selectGridFields";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@pageID", hidPageID.Value);
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            #region 没有设置Grid显示项，报错
            if (ds.Tables[0].Rows.Count == 0)
            {
                this.Alert("请先指定GridView要显示的字段！");
                return;
            }
            #endregion
            string qwq = string.Empty;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                BoundField boundF = new BoundField();
                boundF.HeaderText = row["pd_label"].ToString();
                boundF.DataField = row["pd_colName"].ToString();
                //数据初始化
                if (!string.IsNullOrEmpty(row["pd_grid_format"].ToString()))
                {
                    boundF.DataFormatString = "{0:" + row["pd_grid_format"].ToString() + "}";
                    boundF.ApplyFormatInEditMode = true;
                }
                boundF.ReadOnly = !Convert.ToBoolean(row["pd_isEdit"]);
                //如果是ReadOnly的，则前缀加“+”
                boundF.SortExpression = boundF.ReadOnly ? "+" + boundF.DataField : boundF.DataField;
                boundF.HeaderStyle.Width = Unit.Pixel(Convert.ToInt32(row["pd_grid_width"]));
                boundF.ItemStyle.Width = Unit.Pixel(Convert.ToInt32(row["pd_grid_width"]));
                #region pd_gridAlign
                switch (row["pd_grid_align"].ToString())
                {
                    case "center": boundF.ItemStyle.HorizontalAlign = HorizontalAlign.Center; break;
                    case "left": boundF.ItemStyle.HorizontalAlign = HorizontalAlign.Left; break;
                    case "right": boundF.ItemStyle.HorizontalAlign = HorizontalAlign.Right; break;
                }
                #endregion
                gv.Columns.Insert(0, boundF);
            }

            if (Convert.ToBoolean(_pageMstr.Rows[0]["pm_couldEdit"]))
            {
                #region 添加编辑列
                CommandField editField = new CommandField();//命令字段
                editField.ButtonType = ButtonType.Link;//超链接样式的按钮
                editField.ShowEditButton = true;//显示编辑按钮
                editField.CausesValidation = false;//引发数据验证为false
                editField.EditText = "<u>编辑</u>";
                editField.UpdateText = "<u>更新</u>";
                editField.CancelText = "<u>取消</u>";
                editField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                editField.HeaderStyle.Width = Unit.Pixel(70);
                gv.Columns.Add(editField);//添加编辑按钮到gridview

                gv.RowEditing += new GridViewEditEventHandler(gv_RowEditing); //添加编辑事件
                gv.RowUpdating += new GridViewUpdateEventHandler(gv_RowUpdating); //添加编辑事件
                gv.RowCancelingEdit += new GridViewCancelEditEventHandler(gv_RowCancelingEdit);//添加删除事件
                #endregion
            }

            if (Convert.ToBoolean(_pageMstr.Rows[0]["pm_couldDelete"]))
            {
                #region 添加删除列
                CommandField delField = new CommandField();
                delField.ButtonType = ButtonType.Link;
                delField.ShowDeleteButton = true;//显示删除按钮
                delField.CausesValidation = false;
                delField.DeleteText = "<u>删除</u>";
                delField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                delField.HeaderStyle.Width = Unit.Pixel(40);
                gv.Columns.Add(delField);

                gv.RowDeleting += new GridViewDeleteEventHandler(gv_RowDeleting);//添加删除事件
                #endregion
            }

            if (!string.IsNullOrEmpty(_pageMstr.Rows[0]["pm_passBtn"].ToString().Trim()))
            {
                #region 通过、拒绝按钮设置
                if (string.IsNullOrEmpty(_pageMstr.Rows[0]["pm_passAccess"].ToString()))
                {
                    btnPass.Visible = true;
                    btnRefuse.Visible = btnPass.Visible;
                    //没设置权限，禁用掉
                    btnPass.Enabled = false;
                    btnRefuse.Enabled = btnPass.Enabled;
                    btnPass.ToolTip = "尚未设置对应的权限";
                }
                else
                {
                    btnPass.Visible = this.AccessAuthorizeMenuByID(Session["uID"].ToString(), _pageMstr.Rows[0]["pm_passAccess"].ToString());
                    btnRefuse.Visible = btnPass.Visible;
                }
                //如果Refuse按钮未设置文本，则隐藏
                if (string.IsNullOrEmpty(_pageMstr.Rows[0]["pm_refuseBtn"].ToString().Trim()))
                {
                    btnRefuse.Visible = false;
                }

                btnPass.Text = _pageMstr.Rows[0]["pm_passBtn"].ToString().Trim();
                btnRefuse.Text = _pageMstr.Rows[0]["pm_refuseBtn"].ToString().Trim();

                hidPassProc.Value = _pageMstr.Rows[0]["pm_passProc"].ToString().Trim().Replace("tcpcx", "tcpc" + Session["PlantCode"].ToString());
                hidRefuseProc.Value = _pageMstr.Rows[0]["pm_refuseProc"].ToString().Trim().Replace("tcpcx", "tcpc" + Session["PlantCode"].ToString());
                #endregion

                #region 首列CheckBox列 string.IsNullOrEmpty(_pageMstr.Rows[0]["pm_comExec0"].ToString() GridViewCheckBoxTemplate
                TemplateField chkField;
                chkField = new TemplateField();
                chkField.HeaderTemplate = new GridViewCheckBoxTemplate(DataControlRowType.Header, "", "chkAll");
                chkField.ItemTemplate = new GridViewCheckBoxTemplate(DataControlRowType.DataRow, "", "chkItem");
                chkField.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                gv.Columns.Insert(0, chkField);  
                #endregion
            }
        }
        catch
        {
            this.Alert("呈现GridView时出错！请联系管理员！");
        }

        #region 将PrimaryKey、ForeignKey放入DataKeys
        ArrayList _keyList = new ArrayList();
        string[] strKeys = PageMakerHelper.GetPrimaryKeyFields(hidPageID.Value);
        if (strKeys.Length > 0)
        {
            foreach (string str in strKeys)
            {
                _keyList.Add(str);
            }
        }
        DataTable _tblForeignKeyFields = PageMakerHelper.GetForeignKeyFields(this.hidPageID.Value, this.hidPageID.Value);
        if (_tblForeignKeyFields != null)
        {
            foreach (DataRow row in _tblForeignKeyFields.Rows)
            {
                if (!_keyList.Contains(row["reference_colName"].ToString()))
                {
                    _keyList.Add(row["reference_colName"].ToString());
                }
            }
        }
        gv.DataKeyNames = _keyList.ToArray(typeof(string)) as string[];
        #endregion
    }
    /// <summary>
    /// 将前台的Session等参数拼接传到后台
    /// </summary>
    /// <returns></returns>
    protected string BuildParamToXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement rootNode = xmlDoc.CreateElement("", "Param", "");
        xmlDoc.AppendChild(rootNode);

        XmlNode root = xmlDoc.SelectSingleNode("Param");
        XmlElement xmlElement = xmlDoc.CreateElement("uID");
        xmlElement.SetAttribute("Value", Session["uID"].ToString());
        root.AppendChild(xmlElement);

        xmlElement = xmlDoc.CreateElement("uName");
        xmlElement.SetAttribute("Value", Session["uName"].ToString());
        root.AppendChild(xmlElement);

        xmlElement = xmlDoc.CreateElement("PlantCode");
        xmlElement.SetAttribute("Value", Session["PlantCode"].ToString());
        root.AppendChild(xmlElement);

        //存入PK
        for (int i = 0; i < 10; i++)
        {
            if (Request["pk" + i.ToString()] != null)
            {
                xmlElement = xmlDoc.CreateElement("pk" + i.ToString());
                xmlElement.SetAttribute("Value", Request["pk" + i.ToString()]);
                root.AppendChild(xmlElement);
            }
        }

        //存入FK
        for (int i = 0; i < 10; i++)
        {
            if (Request["fk" + i.ToString()] != null)
            {
                xmlElement = xmlDoc.CreateElement("fk" + i.ToString());
                xmlElement.SetAttribute("Value", Request["fk" + i.ToString()]);
                root.AppendChild(xmlElement);
            }
        }

        xmlDoc.AppendChild(root);

        return xmlDoc.OuterXml;
    }
    /// <summary>
    /// 获取Gridview、导出源数据
    /// </summary>
    /// <returns></returns>
    protected DataTable GetData(DataTable _pageMstr, bool isExportField)
    {
        string _db = _pageMstr.Rows[0]["pm_db"].ToString();
        string _table = _pageMstr.Rows[0]["pm_table"].ToString();
        #region 获取Select字段
        DataTable _tblQuerys = PageMakerHelper.GetQueryFields(hidPageID.Value);
        string _fields = string.Empty;
        if (isExportField)
        {
            #region 如果是导出时调用的
            foreach (DataRow row in PageMakerHelper.GetExportFields(hidPageID.Value).Rows)
            {
                _fields += ", " + row["pd_colName"].ToString();
            }

            if (string.IsNullOrEmpty(_fields))
            {
                this.Alert("尚未设置导出字段！");
                return null;
            }

            _fields = _fields.Substring(1).Trim();
            #endregion
        }
        _fields = string.IsNullOrEmpty(_fields) ? "*" : _fields;
        #endregion
        string _where = _pageMstr.Rows[0]["pm_where"].ToString();//全局where条件，加在查询条件之前
        #region 替换_where中的{uID}等
        _where = _where.Replace("{uID}", "'" + Session["uID"].ToString() + "'");
        _where = _where.Replace("{uName}", "'" + Session["uName"].ToString() + "'");
        _where = _where.Replace("{eName}", "'" + Session["eName"].ToString() + "'");
        _where = _where.Replace("{PlantCode}", "'" + Session["PlantCode"].ToString() + "'");
        _where = _where.Replace("{uRole}", "'" + Session["uRole"].ToString() + "'");
        _where = _where.Replace("{deptID}", "'" + Session["deptID"].ToString() + "'");
        if (!string.IsNullOrEmpty(Request["pk0"]))
        {
            _where = _where.Replace("{pk0}", "'" + Request["pk0"].ToString() + "'");
        }
        if (!string.IsNullOrEmpty(Request["pk1"]))
        {
            _where = _where.Replace("{pk1}", "'" + Request["pk1"].ToString() + "'");
        }
        #endregion
        if (_pageMstr.Rows[0]["pm_isProc"].ToString() != "StoredProcedure")
        {
            #region 非存储过程
            try
            {
                string strSql = "Select " + _fields;
                strSql += " From " + _db + ".dbo." + _table;
                strSql += string.IsNullOrEmpty(_where) ? " Where 1 = 1" : (" Where " + _where);
                /*
                 * 追加参数时，如下:
                 * strSql += " And pm_page = @pm_page";
                 */

                List<SqlParameter> listParam = new List<SqlParameter>();
                #region 依据查询字段，设置查询条件
                for (int i = 0; i < _tblQuerys.Rows.Count; i++)
                {
                    string _queryField = _tblQuerys.Rows[i]["pd_colName"].ToString();
                    Control queryCtrl = this.Form1.FindControl(_queryField);
                    if (queryCtrl is TextBox)
                    {
                        if (!string.IsNullOrEmpty((queryCtrl as TextBox).Text.Trim()))
                        {
                            strSql += " And " + queryCtrl.ID + " = @" + queryCtrl.ID;
                            listParam.Add(new SqlParameter("@" + queryCtrl.ID, (queryCtrl as TextBox).Text.Trim()));
                        }
                    }
                    else if (queryCtrl is CheckBox)
                    {
                        if ((queryCtrl as CheckBox).Checked)
                        {
                            strSql += " And " + queryCtrl.ID + " = @" + queryCtrl.ID;
                            listParam.Add(new SqlParameter("@" + queryCtrl.ID, (queryCtrl as CheckBox).Checked));
                        }
                    }
                    else if (queryCtrl is DropDownList)
                    {
                        if ((queryCtrl as DropDownList).SelectedIndex > 0)
                        {
                            strSql += " And " + queryCtrl.ID + " = @" + queryCtrl.ID;
                            listParam.Add(new SqlParameter("@" + queryCtrl.ID, (queryCtrl as DropDownList).SelectedValue));
                        }
                    }
                }
                #endregion

                #region 明细页面时加入参数：优先外键，否则主键
                if (Request.QueryString["pagefrom"] != null)
                {
                    if (Request.QueryString["fk0"] != null)
                    {
                        DataTable _tblForeignKeyFields = PageMakerHelper.GetForeignKeyFields(this.hidBackID.Value, this.hidPageID.Value);
                        if (_tblForeignKeyFields != null)
                        {
                            for (int i = 0; i < _tblForeignKeyFields.Rows.Count; i++)
                            {
                                strSql += " And " + _tblForeignKeyFields.Rows[i]["parent_colName"].ToString() + " = @" + _tblForeignKeyFields.Rows[i]["parent_colName"].ToString();
                                listParam.Add(new SqlParameter("@" + _tblForeignKeyFields.Rows[i]["parent_colName"].ToString(), Request.QueryString["fk" + i.ToString()]));
                            }
                        }
                    }
                    else if (Request.QueryString["pk0"] != null)
                    {
                        string[] _primaryKeys = PageMakerHelper.GetPrimaryKeyFields(this.hidPageID.Value);
                        for (int i = 0; i < _primaryKeys.Length; i++)
                        {
                            strSql += " And " + _primaryKeys[i] + " = @" + _primaryKeys[i];
                            listParam.Add(new SqlParameter("@" + _primaryKeys[i], Request.QueryString["pk" + i.ToString()]));
                        }
                    }
                }
                #endregion
                //将tcpcx转化成对应的域
                strSql = strSql.Replace("tcpcx", "tcpc" + Session["plantcode"].ToString());
                return SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSql, listParam.ToArray()).Tables[0];
            }
            catch
            {
                return null;
            }
            #endregion
        }
        else
        {
            #region 调用存储过程
            try
            {
                string strName = _pageMstr.Rows[0]["pm_db"].ToString() + ".dbo." + _pageMstr.Rows[0]["pm_table"].ToString();
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@xmlParam", this.BuildParamToXml()));
                #region 依据查询字段
                for (int i = 0; i < _tblQuerys.Rows.Count; i++)
                {
                    string _field = _tblQuerys.Rows[i]["pd_colName"].ToString();
                    Control ctrl = this.tblQuery.FindControl(_field);

                    if (Request.QueryString.AllKeys.Length > 1)
                    {
                        foreach (string key in Request.QueryString.AllKeys)
                        {
                            if (key.ToLower() == _field.ToLower())
                            {
                                param.Add(new SqlParameter("@" + key, Request.QueryString[key]));
                            }
                        }
                    }
                    else
                    {
                        if (ctrl is TextBox)
                        {
                            param.Add(new SqlParameter("@" + _field, (ctrl as TextBox).Text.Trim()));
                        }
                        else if (ctrl is CheckBox)
                        {
                            param.Add(new SqlParameter("@" + _field, (ctrl as CheckBox).Checked.ToString()));
                        }
                        else if (ctrl is DropDownList)
                        {
                            param.Add(new SqlParameter("@" + _field, (ctrl as DropDownList).SelectedValue));
                        }
                    }
                }
                #endregion
                strName = strName.Replace("tcpcx", "tcpc" + Session["plantcode"].ToString());
                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param.ToArray()).Tables[0];
            }
            catch
            {
                return null;
            }
            #endregion
        }
    }
    protected new void BindGridView()
    {
        DataTable _pageMstr = PageMakerHelper.GetPageMstr(this.hidPageID.Value);
        if (_pageMstr == null || _pageMstr.Rows.Count == 0)
        {
            this.Alert("主记录维护有误！");
            return;
        }

        DataTable _table = this.GetData(_pageMstr, false);

        gv.Columns.Clear();
        CreateGridViewColunms(_pageMstr);
        gv.DataSource = _table;
        gv.DataBind();

        //如果是空记录，则设置宽度，放置表头压缩
        if (_table.Rows.Count == 0)
        {
            gv.Width = Unit.Pixel(Convert.ToInt32(_pageMstr.Rows[0]["pm_gridWidth"]));
        }
        //正常换行
        gv.Attributes.Add("style", "word-break:keep-all;word-warp:normal");
        //自动换行
        gv.Attributes.Add("style", "word-break:break-all;word-warp:break-word");
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        BindGridView();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowState == DataControlRowState.Alternate || e.Row.RowState == DataControlRowState.Normal)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int _cellIndex = 0;
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.Controls.Count == 0) 
                    {
                        cell.Text = Server.HtmlDecode(cell.Text);
                    }

                    string keyss = gv.Columns[_cellIndex].SortExpression;
                    string _key = gv.Columns[_cellIndex].SortExpression.Replace("+", "");
                    if (!string.IsNullOrEmpty(_key))
                    {
                        string _control = PageMakerHelper.GetFieldData(hidPageID.Value, _key, "pd_field_control").ToLower();
                        _control = string.IsNullOrEmpty(_control) ? "textbox" : _control;
                        //如果是Bit类型，则显示CheckBox
                        if (_control == "checkbox")
                        {
                            CheckBox chk = new CheckBox();
                            chk.ID = "row_" + _key;
                            chk.Checked = ("&nbsp;" == cell.Text.Trim() || "" == cell.Text.Trim()) ? false : Convert.ToBoolean(cell.Text.Trim().ToLower());
                            chk.Enabled = false;
                            cell.Controls.Add(chk);
                        }
                        else
                        {
                            bool _hasLink = Convert.ToBoolean(PageMakerHelper.GetFieldData(hidPageID.Value, _key, "pd_isLink"));
                            bool _isCustom = Convert.ToBoolean(PageMakerHelper.GetFieldData(hidPageID.Value, _key, "pd_link_isCustom"));
                            #region 有二级页面
                            if (_hasLink)
                            {
                                string _linkPageID = PageMakerHelper.GetFieldData(hidPageID.Value, _key, "pd_link_pageID");
                                if (string.IsNullOrEmpty(_linkPageID))
                                {
                                    this.Alert("字段指定为超级连接，但是未指定目标页面！");
                                    return;
                                }
                                string _linkTarget = PageMakerHelper.GetFieldData(hidPageID.Value, _key, "pd_link_Target");
                                _linkTarget = string.IsNullOrEmpty(_linkTarget) ? "_self" : _linkTarget;
                                string _linkParams = PageMakerHelper.GetFieldData(hidPageID.Value, _key, "pd_link_params");
                                string _url = _isCustom ? (_linkPageID + "?pagefrom=" + hidPageID.Value) : ("../IT/Page_Viewer.aspx?pagefrom=" + hidPageID.Value + "&pageID=" + _linkPageID);
                                //获取自定义标题页面的
                                string _pdtitle = PageMakerHelper.GetFieldData(hidPageID.Value, _key, "pd_link_title");

                                string _param = string.Empty;
                                #region 将外键拼接成url参数；如果没有外键，则加入主键
                                DataTable _tblForeignKeyFields = PageMakerHelper.GetForeignKeyFields(this.hidPageID.Value, _linkPageID);
                                if (_tblForeignKeyFields != null)
                                {
                                    for (int i = 0; i < _tblForeignKeyFields.Rows.Count; i++)
                                    {
                                        _param += "&fk" + i.ToString() + "=" + gv.DataKeys[e.Row.RowIndex].Values[_tblForeignKeyFields.Rows[i]["reference_colName"].ToString()].ToString();
                                    }
                                }
                                if (string.IsNullOrEmpty(_param))
                                {
                                    string[] _primaryKeys = PageMakerHelper.GetPrimaryKeyFields(this.hidPageID.Value);
                                    for (int i = 0; i < _primaryKeys.Length; i++)
                                    {
                                        _param += "&pk" + i.ToString() + "=" + gv.DataKeys[e.Row.RowIndex].Values[_primaryKeys[i]].ToString();
                                    }
                                }
                                #endregion
                                _param += (string.IsNullOrEmpty(_linkParams) ? "" : ("&" + _linkParams));
                                if (_linkTarget == "_blank")
                                {
                                    #region 自定义页面: 调用$.window方法，此时传的参数，应该是主键
                                    Label lbl = new Label();
                                    lbl.ID = "row_" + _key;
                                    lbl.Font.Underline = true;
                                    lbl.Text = cell.Text.Trim();
                                    lbl.Style.Add("cursor", "pointer");
                                    lbl.Attributes.Add("pagemaker-src", _url + "?" + _param);
                                    //添加标题
                                    lbl.Attributes.Add("pagemaker-linkTitle", _pdtitle);

                                    cell.Controls.Add(lbl);
                                    #endregion
                                }
                                else
                                {
                                    #region 系统定义页面：形成超级链接控件，此时传的参数，应该是外键
                                    HyperLink link = new HyperLink();
                                    link.ID = "row_" + _key;
                                    link.Font.Underline = true;
                                    link.Text = cell.Text.Trim();
                                    link.Target = _linkTarget;
                                    link.NavigateUrl = "javascript:window.location('" + _url + _param + "')";
                                    cell.Controls.Add(link);
                                    #endregion
                                }
                            }
                            #endregion
                        }
                    }

                    _cellIndex++;
                }
            }
        }
        else if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int _cellIndex = 0;

                foreach (TableCell cell in e.Row.Cells)
                {
                    string keys = gv.Columns[_cellIndex].SortExpression;
                    string _key = gv.Columns[_cellIndex].SortExpression.Replace("+", "");
                    bool _readOnly = gv.Columns[_cellIndex].SortExpression.IndexOf("+") == 0 ? true : false;
                    if (!string.IsNullOrEmpty(_key))
                    {
                        string _dataType = PageMakerHelper.GetFieldData(hidPageID.Value, _key, "pd_dataType");
                        if (string.IsNullOrEmpty(_dataType))
                        {
                            this.Alert("获取字段类型时出错！请联系管理员1！");
                            return;
                        }

                        string _cellText = _readOnly ? cell.Text : (cell.Controls[0] as TextBox).Text.Trim();
                        #region 根据不同的类型，按不同的控件显示
                        if (_dataType == "bit")
                        {
                            cell.Text = string.Empty;
                            CheckBox chk = new CheckBox();
                            chk.ID = "row_" + _key;
                            chk.Checked = "" == _cellText.ToLower() ? false : Convert.ToBoolean(_cellText.ToLower());
                            chk.Enabled = true;
                            cell.Controls.Add(chk);
                        }
                        else
                        {
                            #region pd_gridDrop不为空，按Dropdown显示
                            string _dropValue = PageMakerHelper.GetFieldData(hidPageID.Value, _key, "pd_gridDrop");
                            if (!string.IsNullOrEmpty(_dropValue))
                            {
                                cell.Text = string.Empty;
                                DropDownList drop = new DropDownList();
                                drop.ID = "row_" + _key;
                                drop.Items.Add(new ListItem("--", ""));
                                //如果包含“;”，则取值，否则认为要执行SQL语句
                                if (_dropValue.IndexOf(';') > 0)
                                {
                                    foreach (string val in _dropValue.Split(';'))
                                    {
                                        drop.Items.Add(new ListItem(val, val));
                                    }
                                    cell.Controls.Add(drop);
                                }
                                else
                                {
                                    try
                                    {
                                        _dropValue = _dropValue.Replace("{uID}", "'" + Session["uID"].ToString() + "'");
                                        _dropValue = _dropValue.Replace("{uName}", "N'" + Session["uName"].ToString() + "'");
                                        _dropValue = _dropValue.Replace("{PlantCode}", Session["PlantCode"].ToString());
                                        DataTable table = SqlHelper.ExecuteDataset(strConn, CommandType.Text, _dropValue).Tables[0];

                                        if (table != null || table.Rows.Count > 0)
                                        {
                                            foreach (DataRow row in table.Rows)
                                            {
                                                if (table.Columns.Count == 1)
                                                {
                                                    drop.Items.Add(new ListItem(row[0].ToString(), row[0].ToString()));
                                                }
                                                else
                                                {
                                                    drop.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
                                                }
                                            }
                                            cell.Controls.Add(drop);
                                        }
                                    }
                                    catch
                                    {
                                        this.Alert("执行SQL语句时出错，请联系管理员！");
                                        return;
                                    }
                                }
                                //默认选中
                                try
                                {
                                    drop.Items.FindByValue(_cellText).Selected = true;
                                }
                                catch
                                {
                                    drop.SelectedIndex = -1;
                                }
                            }
                            #endregion
                        }
                        #endregion
                        #region 如果是可编辑的，要备份一个值，以便比较后决定是否需要更新
                        if (!_readOnly)
                        {
                            HtmlInputHidden hid = new HtmlInputHidden();
                            hid.ID = "row_hid_" + _key;
                            hid.Value = _cellText;
                            cell.Controls.Add(hid);
                        }
                        #endregion
                    }
                    #region 设置TextBox100%
                    foreach (Control ctrl in cell.Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            TextBox txt = (TextBox)ctrl;
                            txt.Width = Unit.Percentage(100);
                            txt.ID = "row_" + _key;
                        }
                    }
                    #endregion
                    _cellIndex++;
                }
            }
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable _pageMstr = PageMakerHelper.GetPageMstr(this.hidPageID.Value);
        string delProc = string.Empty;
        foreach (DataRow row in _pageMstr.Rows)
        {
            delProc = row["pm_delProc"].ToString();
            delProc = delProc.Replace("tcpcx", "tcpc" + Session["PlantCode"].ToString());
        }

        string[] strKeys = PageMakerHelper.GetPrimaryKeyFields(hidPageID.Value);
        if (strKeys.Length > 0)
        {
            #region 有存储过程删除（自己写删除）
            if (!string.IsNullOrEmpty(delProc))
            {
                try
                {
                    /*
                     *自己写存储过程时要传入参数
                     *1、相对应表的PageID
                     *2、目标表的主键，主键最多为5个值
                     *3、@retValue返回值判断记录是否删除成功
                     */
                    string strName = delProc;
                    strName = strName.Replace("tcpcx", "tcpc" + Session["PlantCode"].ToString());

                    SqlParameter[] param = new SqlParameter[9];
                    param[0] = new SqlParameter("@pageID", this.hidPageID.Value);
                    for (int i = 0; i < strKeys.Length; i++)
                    {
                        param[i + 1] = new SqlParameter("@" + strKeys[i], gv.DataKeys[e.RowIndex].Values[strKeys[i]].ToString());
                    }
                    param[6] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[6].Direction = ParameterDirection.Output;
                    param[7] = new SqlParameter("@errMsg", SqlDbType.NVarChar, 400);
                    param[7].Direction = ParameterDirection.Output;
                    param[8] = new SqlParameter("@uID", Session["uID"].ToString());

                    SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
                    string massage = param[7].Value.ToString();
                    if (Convert.ToBoolean(param[6].Value))
                    {
                        this.Alert("删除成功！");
                    }
                    else
                    {
                        if (string.Empty.Equals(massage))
                        {
                            this.Alert("删除失败！");
                        }
                        else
                        {
                            this.Alert(massage);
                        }
                    }
                }
                catch
                {
                    this.Alert("删除失败！请联系管理员！"); ;
                }
            }
            #endregion

            #region 无存储过程删除（系统自动删除）
            else
            {
                try
                {
                    /*
                     By Shanzm 2015-03-05:
                     * 1、防止SQL注入攻击，故设置关键字段最多5个，且上传值，而不是拼接SQL语句
                     * 2、SQL Server中采用方式：A、先将关键字、值存入临时表 B、按照临时做假性批量删除
                     * 3、因为，关键字段是不可能有注入的，只有其值可能会存在，故通过参数传入存储过程的参数，然后放入临时表
                     */
                    string strName = "sp_page_deleteGridData";
                    SqlParameter[] param = new SqlParameter[12];
                    param[0] = new SqlParameter("@pageID", this.hidPageID.Value);
                    for (int i = 0; i < strKeys.Length; i++)
                    {
                        param[i + 1] = new SqlParameter("@key" + (i + 1).ToString(), strKeys[i]);
                        param[i + 6] = new SqlParameter("@keyValue" + (i + 1).ToString(), gv.DataKeys[e.RowIndex].Values[strKeys[i]].ToString());
                    }
                    param[11] = new SqlParameter("@retValue", SqlDbType.Bit);
                    param[11].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                    if (Convert.ToBoolean(param[11].Value))
                    {
                        this.Alert("删除成功！");
                    }
                    else
                    {
                        this.Alert("删除失败！");
                    }
                }
                catch
                {
                    this.Alert("删除失败！请联系管理员！"); ;
                }
            }
            #endregion


        }
        else
        {
            this.Alert("没有设置关键字段，故无法删除！");
        }

        BindGridView();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string[] pkeyFields = PageMakerHelper.GetPrimaryKeyFields(hidPageID.Value);
        string urlPKey = string.Empty;
        for (int i = 0; i < pkeyFields.Length; i++)
        {
            urlPKey += "&pk" + i.ToString() + "=" + HttpUtility.UrlEncode(gv.DataKeys[e.NewEditIndex].Values[pkeyFields[i]].ToString());
        }

        this.Redirect("../IT/Page_New.aspx?pageID=" + this.hidPageID.Value + urlPKey + "&type=edit");
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string[] strKeys = PageMakerHelper.GetPrimaryKeyFields(hidPageID.Value);
        if (strKeys.Length > 0)
        {
            for (int col = 0; col < gv.Columns.Count; col++)
            {
                //如果列是只读的，SortExpression为空；前面绑定时设定了
                if (gv.Columns[col].SortExpression != string.Empty)
                {
                    string _field = gv.Columns[col].SortExpression;
                    string _fieldValue = "";
                    string _fieldValueH = "";//Hidden值：通过比较，有变化的更新，无变化的，则不更新

                    //gv$ctl07$row_it_dec
                    string _ctrlID = "gv$ctl" + ((e.RowIndex + 2) < 10 ? ("0" + (e.RowIndex + 2).ToString()) : (e.RowIndex + 2).ToString()) + "$row_" + _field;
                    string _hidID = "gv$ctl" + ((e.RowIndex + 2) < 10 ? ("0" + (e.RowIndex + 2).ToString()) : (e.RowIndex + 2).ToString()) + "$row_hid_" + _field;
                    _fieldValue = this.Request.Form[_ctrlID];
                    _fieldValueH = this.Request.Form[_hidID];


                    if (_fieldValue == "on")
                    {
                        if (_fieldValueH.ToLower() == "true")
                        {
                            continue;
                        }
                    }
                    if (_fieldValue == null)
                    {
                        if (_fieldValueH == null)
                        {
                            continue;
                        }
                        else if (_fieldValueH.ToLower() == "false")
                        {
                            continue;
                        }
                    }
                    //if (_fieldValue == null || _fieldValueH == _fieldValue.ToLower())
                    //{
                    //    continue;
                    //}

                    #region 值转换：针对CheckBox之类的
                    if (_fieldValue == null)
                    {
                        _fieldValue = "false";
                    }
                    else if (_fieldValue == "on")
                    {
                        _fieldValue = "true";
                    }
                    #endregion

                    try
                    {
                        string strName = "sp_page_updateGridData";
                        SqlParameter[] param = new SqlParameter[14];
                        param[0] = new SqlParameter("@pageID", this.hidPageID.Value);
                        param[1] = new SqlParameter("@field", _field);
                        param[2] = new SqlParameter("@fieldValue", _fieldValue);
                        for (int i = 0; i < strKeys.Length; i++)
                        {
                            param[i + 3] = new SqlParameter("@key" + (i + 1).ToString(), strKeys[i]);
                            param[i + 8] = new SqlParameter("@keyValue" + (i + 1).ToString(), gv.DataKeys[e.RowIndex].Values[strKeys[i]].ToString());
                        }
                        param[13] = new SqlParameter("@retValue", SqlDbType.Bit);
                        param[13].Direction = ParameterDirection.Output;

                        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                        if (!Convert.ToBoolean(param[13].Value))
                        {
                            this.Alert("更新失败！");
                        }
                    }
                    catch
                    {
                        this.Alert("更新失败！请联系管理员！"); ;
                    }
                }
            }
        }
        else
        {
            this.Alert("尚未设置关键字段，故无法更新！");
        }

        gv.EditIndex = -1;
        BindGridView();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable _pageMstr = PageMakerHelper.GetPageMstr(this.hidPageID.Value);
        if (_pageMstr == null || _pageMstr.Rows.Count == 0)
        {
            this.Alert("主记录维护有误！");
        }
        else
        {
            DataTable ExData = this.GetData(_pageMstr, true);

            if (ExData == null)
            {
                this.Alert("没有数据可供导出！");
            }
            else
            {
                string EXTitle = "";//<b>工号</b>~^250^<b>姓名</b>~^
                try
                {
                    string strName = "sp_page_selectExportFields";
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@pageID", hidPageID.Value);

                    DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        EXTitle += row["pd_export_width"].ToString() + "^<b>" + row["pd_label"].ToString() + "</b>~^";
                    }

                    if (string.IsNullOrEmpty(EXTitle))
                    {
                        this.Alert("没有设置导出字段！");
                    }
                    else
                    {
                        this.ExportExcel(EXTitle, ExData, false);
                    }
                }
                catch
                {
                    this.Alert("数据库操作失败！请联系管理员！");
                }
            }
        }

        BindGridView();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../IT/Page_Viewer.aspx?pageID=" + hidBackID.Value);
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        string _url = this.Request.RawUrl.ToString();
        //将Page_Viewer替换成Page_New，保持url参数不变
        _url = _url.Replace("Page_Viewer", "Page_New");
        this.Redirect(_url + "&type=add");
        //Response.Redirect("../IT/Page_New.aspx?type=add&pageID=" + hidPageID.Value);
    }
    protected string BuildParamToXml(int rowIndex, string[] dataKeyNames)
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement rootNode = xmlDoc.CreateElement("", "Param", "");
        xmlDoc.AppendChild(rootNode);

        XmlNode root = xmlDoc.SelectSingleNode("Param");
        XmlElement xmlElement = xmlDoc.CreateElement("uID");
        xmlElement.SetAttribute("Value", Session["uID"].ToString());
        root.AppendChild(xmlElement);

        xmlElement = xmlDoc.CreateElement("uName");
        xmlElement.SetAttribute("Value", Session["uName"].ToString());
        root.AppendChild(xmlElement);

        xmlElement = xmlDoc.CreateElement("PlantCode");
        xmlElement.SetAttribute("Value", Session["PlantCode"].ToString());
        root.AppendChild(xmlElement);

        //存入DataKeys
        for (int i = 0; i < dataKeyNames.Length; i++)
        {
            xmlElement = xmlDoc.CreateElement(dataKeyNames[i]);
            xmlElement.SetAttribute("Value", gv.DataKeys[rowIndex].Values[dataKeyNames[i]].ToString());
            root.AppendChild(xmlElement);
        }

        xmlDoc.AppendChild(root);

        return xmlDoc.OuterXml;
    }
    protected void btnPass_Click(object sender, System.EventArgs e)
    {
        if (string.IsNullOrEmpty(hidPassProc.Value))
        {
            this.Alert("At first, you should specify a stored proceuder !");
        }
        else
        {
            string massage = string.Empty;//只要出现错误行，立即跳出循环
            int rowIndex = -1;

            if (string.IsNullOrEmpty(hidCheck.Value.Trim()))
            {
                massage = "请先选择一行";
            }
            else
            {
                foreach (string row in hidCheck.Value.Split(';').Distinct())
                {
                    if (string.IsNullOrEmpty(row))
                    {
                        continue;
                    }

                    rowIndex = Convert.ToInt32(row);
                    try
                    {
                        string strName = this.hidPassProc.Value;
                        strName = strName.Replace("tcpcx", "tcpc" + Session["PlantCode"].ToString());

                        List<SqlParameter> listParam = new List<SqlParameter>();
                        listParam.Add(new SqlParameter("@pageID", this.hidPageID.Value));
                        listParam.Add(new SqlParameter("@xmlParam", this.BuildParamToXml(rowIndex, gv.DataKeyNames)));
                        SqlParameter retValue = new SqlParameter("@retValue", SqlDbType.Bit);
                        SqlParameter error = new SqlParameter("@errMsg", SqlDbType.NVarChar, 400);
                        retValue.Direction = ParameterDirection.Output;
                        error.Direction = ParameterDirection.Output;
                        listParam.Add(retValue);
                        listParam.Add(error);

                        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, listParam.ToArray());

                        massage = listParam[3].Value.ToString();

                        if (!string.IsNullOrEmpty(massage))
                        {
                            massage = "行 " + rowIndex.ToString() + "  处发生错误：" + massage;
                            break;
                        }
                    }
                    catch
                    {
                        massage = "数据库操作失败！请联系管理员!";
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(massage))
            {
                this.Alert(massage);
            }
        }

        hidCheck.Value = ";";

        BindGridView();
    }
    protected void btnRefuse_Click(object sender, System.EventArgs e)
    {
        if (string.IsNullOrEmpty(hidRefuseProc.Value))
        {
            this.Alert("At first, you should specify a stored proceuder !");
        }
        else
        {
            string massage = string.Empty;//只要出现错误行，立即跳出循环
            int rowIndex = -1;

            if (string.IsNullOrEmpty(hidCheck.Value.Trim()))
            {
                massage = "请先选择一行";
            }
            else
            {
                foreach (string row in hidCheck.Value.Split(';').Distinct())
                {
                    if (string.IsNullOrEmpty(row))
                    {
                        continue;
                    }

                    rowIndex = Convert.ToInt32(row);
                    try
                    {
                        string strName = this.hidRefuseProc.Value;
                        strName = strName.Replace("tcpcx", "tcpc" + Session["PlantCode"].ToString());

                        List<SqlParameter> listParam = new List<SqlParameter>();
                        listParam.Add(new SqlParameter("@pageID", this.hidPageID.Value));
                        listParam.Add(new SqlParameter("@xmlParam", this.BuildParamToXml(rowIndex, gv.DataKeyNames)));
                        SqlParameter retValue = new SqlParameter("@retValue", SqlDbType.Bit);
                        SqlParameter error = new SqlParameter("@errMsg", SqlDbType.NVarChar, 400);
                        retValue.Direction = ParameterDirection.Output;
                        error.Direction = ParameterDirection.Output;
                        listParam.Add(retValue);
                        listParam.Add(error);

                        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, listParam.ToArray());

                        massage = listParam[3].Value.ToString();

                        if (!string.IsNullOrEmpty(massage))
                        {
                            massage = "行 " + rowIndex.ToString() + "  处发生错误：" + massage;
                            break;
                        }
                    }
                    catch
                    {
                        massage = "数据库操作失败！请联系管理员!";
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(massage))
            {
                this.Alert(massage);
            }
        }

        hidCheck.Value = ";";

        BindGridView();
    }
}
