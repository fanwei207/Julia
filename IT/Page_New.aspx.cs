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
using System.Xml;

public partial class Page_New : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
    string[] str;
    int n = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hidPageID.Value = Request.QueryString["pageID"];
            this.hidType.Value = Request.QueryString["type"];
            this.hidBackPageID.Value = Request.QueryString["pageID"];

            #region 保存前，是否需要执行指定给过程
            DataTable _tblMastr = PageMakerHelper.GetPageMstr(this.hidPageID.Value);
            if (_tblMastr != null && _tblMastr.Rows.Count > 0)
            {
                this.hidSaveProc.Value = _tblMastr.Rows[0]["pm_saveProc"].ToString();
                this.hidEditProc.Value = _tblMastr.Rows[0]["pm_editProc"].ToString();

                #region 替换tcpcx
                this.hidSaveProc.Value = this.hidSaveProc.Value.Replace("tcpcx", "tcpc" + Session["PlantCode"].ToString());
                this.hidEditProc.Value = this.hidEditProc.Value.Replace("tcpcx", "tcpc" + Session["PlantCode"].ToString());
                #endregion
            }
            else
            {
                this.Alert("获取主记录时失败!请联系管理员！");
                btnSave.Enabled = false;
            }
            #endregion

            DesignForm();
        }
    }
    /// <summary>
    /// 建立表单
    /// </summary>
    protected void DesignForm()
    {
        DataTable _table = null;
        DataTable _sourceTable = null;
        if (this.hidType.Value == "add")
        {
            _table = PageMakerHelper.GetAddableFields(this.hidPageID.Value);
        }
        else if (this.hidType.Value == "edit")
        {
            _table = PageMakerHelper.GetEditableFields(this.hidPageID.Value);
            _sourceTable = this.GetSourceTableData(this.hidPageID.Value);
        }

        if (_table == null || _table.Rows.Count == 0)
        {
            this.Alert("fail to retrieve addable|editable fields");
            btnSave.Enabled = false;
            return;
        }

        int _maxRow = Convert.ToInt32(_table.Rows[0]["pd_add_maxRowIndex"]);
        int _maxCol = Convert.ToInt32(_table.Rows[0]["pd_add_maxColIndex"]);
        #region 画表单
        for (int i = 0; i <= _maxRow; i++)
        {
            HtmlTableRow tableRow = new HtmlTableRow();
            #region 循环画表单
            for (int j = 0; j <= _maxCol; j++)
            {
                HtmlTableCell labelCell = new HtmlTableCell();//标签Cell
                HtmlTableCell valueCell = new HtmlTableCell();//控件Cell
                #region 循环获取所在单元格的控件
                foreach (DataRow row in _table.Select("pd_add_rowIndex = " + i.ToString() + " And pd_add_colIndex = " + j.ToString()))
                {     
                    string _defaultVal = row["pd_field_defaultValue"].ToString().ToLower();
                    if(!string.IsNullOrEmpty(_defaultVal))
                    {
                        _defaultVal = (new PageMakerHelper()).SwitchDefaultValue(_defaultVal);
                    }
                    //写标签
                    labelCell.InnerHtml = row["pd_label"].ToString() + ":&nbsp;";
                    labelCell.Width = "80px";
                    labelCell.Style.Add("text-align", "right");
                    tableRow.Cells.Add(labelCell);
                    #region rowSpan & colSpan
                    if (Convert.ToInt32(row["pd_add_rowSpan"]) > 1)
                    {
                        valueCell.RowSpan = Convert.ToInt32(row["pd_add_rowSpan"]);
                    }
                    if (Convert.ToInt32(row["pd_add_colSpan"]) > 1)
                    {
                        valueCell.ColSpan = Convert.ToInt32(row["pd_add_colSpan"]);
                    }

                    if (Convert.ToInt32(row["pd_add_colWidth"]) > 0)
                    {
                        valueCell.Width = row["pd_add_colWidth"].ToString();
                    }
                    if (Convert.ToInt32(row["pd_add_rowHeight"]) > 0)
                    {
                        valueCell.Height = row["pd_add_rowHeight"].ToString();
                    }
                    #endregion
                    //画控件
                    string _control = row["pd_field_control"].ToString().ToLower();
                    if (_control == "checkbox")
                    {
                        #region checkbox
                        valueCell = new HtmlTableCell();
                        CheckBox chk = new CheckBox();
                        chk.ID = row["pd_colName"].ToString();
                        chk.Checked = _sourceTable == null ? false : (_sourceTable.Rows.Count > 0 ? Convert.ToBoolean(string.IsNullOrEmpty(_sourceTable.Rows[0][chk.ID].ToString()) ? false : _sourceTable.Rows[0][chk.ID]) : false);
                        chk.Checked = this.Request.Form[chk.ID] == null ? chk.Checked : (this.Request.Form[chk.ID] == "on" ? true : false);
                        

                        valueCell.Controls.Add(chk);
                        #endregion
                    }
                    else if (_control == "dropdownlist")
                    {
                        #region dropdownlist
                        string _dropValue = PageMakerHelper.GetFieldData(hidPageID.Value, row["pd_colName"].ToString(), "pd_field_controlValue");
                        if (!string.IsNullOrEmpty(_dropValue))
                        {
                            #region pd_gridDrop不为空，按Dropdown显示
                            DropDownList drop = new DropDownList();
                            drop.ID = row["pd_colName"].ToString();
                            drop.Width = Unit.Pixel(Convert.ToInt32(row["pd_add_colWidth"]) == 0 ? 100 : Convert.ToInt32(row["pd_add_colWidth"]));
                            drop.Items.Add(new ListItem("--", ""));
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
                                #region 绑定Sql语句获取的value
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
                                #endregion
                            }

                            //判断是否有闪变事件
                            //if (row["pd_control_selectedIndexChanged"].ToString() == "True")
                            //{
                            //    drop.AutoPostBack = true;
                            //    drop.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
                            //}
                            #region 新增失败时保留值、编辑时初始化值 设置默认值
                            string _SelectedValue = "";
                            try
                            {
                                _SelectedValue = _sourceTable == null ? "" : _sourceTable.Rows[0][drop.ID].ToString();
                                _SelectedValue = string.IsNullOrEmpty(_SelectedValue) ? _defaultVal : _SelectedValue;
                                _SelectedValue = this.Request.Form[drop.ID] == null ? _SelectedValue : this.Request.Form[drop.ID];

                                if (!string.IsNullOrEmpty(_SelectedValue))
                                {
                                    drop.Items.FindByValue(_SelectedValue).Selected = true;
                                }
                            }
                            catch
                            {
                                drop.SelectedIndex = -1;
                            }
                            #endregion

                            valueCell.Controls.Add(drop);
                            #endregion
                        }
                        else
                        {
                            this.Alert("控件指定为DropDownList，但未指定值");
                            return;
                        }
                        #endregion
                    }
                    else if (_control == "textbox")
                    {
                        #region textbox
                        TextBox txt = new TextBox();
                        txt.ID = row["pd_colName"].ToString();
                        txt.CssClass = "SmallTextBox";
                        //add 20150608 独占类
                        if (!string.IsNullOrEmpty(row["pd_cssClass"].ToString()))
                        {
                            txt.CssClass = row["pd_cssClass"].ToString();
                        }
                        txt.MaxLength = Convert.ToInt32(row["pd_dataLength"]);
                        txt.Text = _sourceTable == null ? "" : _sourceTable.Rows[0][txt.ID].ToString();
                        txt.Text = this.Request.Form[txt.ID] == null ? txt.Text : this.Request.Form[txt.ID];
                        if (Convert.ToInt32(row["pd_add_colWidth"]) > 0)
                        {
                            txt.Width = Unit.Pixel(Convert.ToInt32(row["pd_add_colWidth"]));
                        }
                        txt.Text = string.IsNullOrEmpty(txt.Text) ? _defaultVal : txt.Text;
                        #region 根据数据类型，决定固定样式
                        switch (row["pd_dataType"].ToString().ToLower())
                        {
                            case "int": txt.CssClass += " Integer "; break;
                            case "decimal": txt.CssClass += " Decimal "; break;
                            case "datetime": txt.CssClass += " Date "; break;
                        }
                        #endregion

                        valueCell.Controls.Add(txt);
                        #endregion
                    }
                    else if (_control == "textarea")
                    {
                        #region textarea
                        TextBox txt = new TextBox();
                        txt.ID = row["pd_colName"].ToString();
                        txt.MaxLength = Convert.ToInt32(row["pd_dataLength"]);
                        txt.TextMode = TextBoxMode.MultiLine;
                        txt.Height = Unit.Percentage(100);
                        txt.Text = _sourceTable == null ? "" : _sourceTable.Rows[0][txt.ID].ToString();
                        txt.Text = this.Request.Form[txt.ID] == null ? txt.Text : this.Request.Form[txt.ID];
                        if (Convert.ToInt32(row["pd_add_colWidth"]) > 0)
                        {
                            txt.Width = Unit.Pixel(Convert.ToInt32(row["pd_add_colWidth"]));
                        }
                        txt.Text = string.IsNullOrEmpty(txt.Text) ? _defaultVal : txt.Text;
                        valueCell.Controls.Add(txt);
                        #endregion
                    }
                    else if (_control == "fileupload")
                    {
                        #region fileupload
                        FileUpload file = new FileUpload();
                        file.ID = row["pd_colName"].ToString();
                        file.Width = Unit.Percentage(100);
                        if (Convert.ToInt32(row["pd_add_colWidth"]) > 0)
                        {
                            file.Width = Unit.Pixel(Convert.ToInt32(row["pd_add_colWidth"]));
                        }

                        valueCell.Controls.Add(file);
                        #endregion
                    }

                    tableRow.Cells.Add(labelCell);
                    tableRow.Cells.Add(valueCell);
                }
                #endregion
            }
            #endregion
            tblContainer.Rows.Add(tableRow);
        }
        #endregion
        #region 加入保存按钮
        HtmlTableRow saveRow = new HtmlTableRow();
        HtmlTableCell saveCell = new HtmlTableCell();
        saveCell.ColSpan = ++_maxCol * 2;
        saveCell.Controls.Add(btnSave);
        saveCell.Style.Add("text-align", "center");
        saveCell.Style.Add("height", "50px");
        saveRow.Cells.Add(saveCell);
        tblContainer.Rows.Add(saveRow);
        #endregion
    }
    /// <summary>
    /// 编辑时，需要初始化源表的数据
    /// </summary>
    /// <param name="pmID"></param>
    /// <param name="para"></param>
    /// <returns></returns>
    public DataTable GetSourceTableData(string pmID)
    {
        DataTable _pageMstr = PageMakerHelper.GetPageMstr(this.hidPageID.Value);
        string[] strKeys = PageMakerHelper.GetPrimaryKeyFields(hidPageID.Value);

        string _pm_db = _pageMstr.Rows[0]["pm_db"].ToString();
        string _pm_table = _pageMstr.Rows[0]["pm_table"].ToString();
        string _pm_isProc = _pageMstr.Rows[0]["pm_isProc"].ToString();

        try
        {
            if (_pm_isProc.ToLower() == "storedprocedure")
            {
                #region 存储过程
                string strSql = _pm_db + ".dbo." + _pm_table;
                strSql = strSql.Replace("tcpcx", "tcpc" + Session["plantcode"].ToString());

                List<SqlParameter> listParam = new List<SqlParameter>();
                listParam.Add(new SqlParameter("@xmlParam", this.BuildParamToXml()));

                for (int i = 0; i < strKeys.Length; i++)
                {
                    string pkValue = Request["pk" + i.ToString()];
                    pkValue = this.Request.Form[strKeys[i]] == null ? pkValue : this.Request.Form[strKeys[i]];//如果主键已呈现在控件中，则直接取控件
                    listParam.Add(new SqlParameter("@" + strKeys[i], pkValue));
                }
                return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, listParam.ToArray()).Tables[0];
                #endregion
            }
            else
            {
                #region 表、视图
                string strSql = "Select Top 1 * From " + _pm_db + ".dbo." + _pm_table;
                strSql += " Where 1 = 1 ";

                List<SqlParameter> listParam = new List<SqlParameter>();
                for (int i = 0; i < strKeys.Length; i++)
                {
                    string pkValue = Request["pk" + i.ToString()];
                    pkValue = this.Request.Form[strKeys[i]] == null ? pkValue : this.Request.Form[strKeys[i]];//如果主键已呈现在控件中，则直接取控件
                    strSql += " And " + strKeys[i] + " = @" + strKeys[i];
                    listParam.Add(new SqlParameter("@" + strKeys[i], pkValue));
                }
                strSql = strSql.Replace("tcpcx", "tcpc" + Session["plantcode"].ToString());
                return SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSql, listParam.ToArray()).Tables[0];
                #endregion
            }
        }
        catch
        {
            return null;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string _url = this.Request.RawUrl.ToString();
        //将Page_New替换成Page_Viewer，保持url参数不变
        _url = _url.Replace("Page_New", "Page_Viewer");
        _url = _url.Replace("&type=add", "");
        _url = _url.Replace("&type=edit", "");
        this.Redirect(_url);
    }
    /// <summary>
    /// 将Session、PK、FK综合到一个xml参数中，然后传到后台
    /// </summary>
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable _table = null;
        if (this.hidType.Value == "add")
        {
            _table = PageMakerHelper.GetAddableFields(this.hidPageID.Value);
        }
        else if (this.hidType.Value == "edit")
        {
            _table = PageMakerHelper.GetEditableFields(this.hidPageID.Value);
        }

        if (_table != null)
        {
            if (_table.Rows.Count > 0)
            {
                #region 在保存或编辑前岁数据进行基本的验证
                //以下注释代码还要进行完善和测试
                //for (int j = 0; j < _table.Rows.Count; j++)
                //{
                //    string _colName = _table.Rows[j]["pd_colName"].ToString();
                //    string _label = _table.Rows[j]["pd_label"].ToString();
                //    //验证字段能否为空
                //    string isnull = _table.Rows[j]["pd_isNull"].ToString();
                //    if (_table.Rows[j]["pd_isNull"].ToString().ToLower() == "false")
                //    {
                //        if (this.Request.Form[_colName].Length <= 0 || this.Request.Form[_colName].ToString() == string.Empty)
                //        {
                //            this.Alert(_label + "不能为空");
                //            DesignForm();
                //            return;
                //        }
                //    }
                //    //验证 nvarchar 和 varchar 的长度
                //    if (_table.Rows[j]["pd_dataType"].ToString().ToLower() == "nvarchar" || _table.Rows[j]["pd_dataType"].ToString().ToLower() == "varchar")
                //    {
                //        if (this.Request.Form[_colName].Length > Convert.ToInt32(_table.Rows[j]["pd_dataLength"]))
                //        {
                //            this.Alert(_label + "长度不能超过" + Convert.ToInt32(_table.Rows[j]["pd_dataLength"]));
                //            DesignForm();
                //            return;
                //        }
                //    }
                //    //字段是否为整形
                //    if (_table.Rows[j]["pd_dataType"].ToString().ToLower() == "int")
                //    {
                //        if (!string.IsNullOrEmpty(_table.Rows[j]["pd_dataType"].ToString()))
                //        { 
                //            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
                //            byte[] bytestr = ascii.GetBytes(this.Request.Form[_colName].ToString());
                //            foreach (byte c in bytestr)
                //            {
                //                if (c < 48 || c > 57)
                //                {
                //                    this.Alert(_label + "必须为数字");
                //                    return;
                //                }
                //            }
                //        }
                //    }
                //    //字符是否为字符
                //    if (_table.Rows[j]["pd_dataType"].ToString().ToLower() == "datetime")
                //    {
                //        DateTime _date ;
                //        if (!DateTime.TryParse(this.Request.Form[_colName], out _date))
                //        {
                //            this.Alert(_label + "不是日期类型");
                //            DesignForm();
                //            return;
                //        }
                //    }
                //}
                #endregion

                #region 新增
                if (this.hidType.Value == "add")
                {
                    if (string.IsNullOrEmpty(this.hidSaveProc.Value))
                    {
                        #region 没有指定存储过程时
                        try
                        {
                            DataTable _pageMstr = PageMakerHelper.GetPageMstr(this.hidPageID.Value);
                            string _pm_db = _pageMstr.Rows[0]["pm_db"].ToString();
                            string _pm_table = _pageMstr.Rows[0]["pm_table"].ToString();

                            string strSql = "Insert Into " + _pm_db + ".dbo." + _pm_table + "(createBy, createName, createDate";
                            string strValues = " Values(@uID, @uName, GetDate()";

                            List<SqlParameter> listParam = new List<SqlParameter>();
                            listParam.Add(new SqlParameter("@uID", Session["uID"].ToString()));
                            listParam.Add(new SqlParameter("@uName", Session["uName"].ToString()));

                            #region 循环获取保存的值，并传入后台
                            foreach (DataRow row in _table.Rows)
                            {
                                string _colName = row["pd_colName"].ToString();
                                string _colValue = this.Request.Form[_colName] == "on" ? "1" : this.Request.Form[_colName];
                                if (string.IsNullOrEmpty(_colValue))
                                {
                                    if (row["pd_field_control"].ToString() == "CheckBox")
                                    {
                                        _colValue = "0";
                                    }
                                }

                                listParam.Add(new SqlParameter("@" + _colName, _colValue));

                                //拼接Set语句
                                strSql += ", " + _colName;
                                strValues += ", @" + _colName;
                            }
                            #endregion

                            strSql += ")";
                            strValues += ") ";

                            SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, strSql + strValues, listParam.ToArray());

                            this.Alert("保存成功！");
                        }
                        catch
                        {
                            this.Alert("保存失败！请联系管理员！");
                        }
                        #endregion
                    }
                    else
                    {
                        #region 指定了存储过程
                        try
                        {
                            string strName = this.hidSaveProc.Value;
                            List<SqlParameter> listParam = new List<SqlParameter>();
                            //listParam.ToArray();   后面的不需要循环，用这个方法直接转换为数组
                            listParam.Add(new SqlParameter("@pageID", this.hidPageID.Value));
                            listParam.Add(new SqlParameter("@xmlParam", this.BuildParamToXml()));
                            SqlParameter retValue = new SqlParameter("@retValue", SqlDbType.Bit);
                            SqlParameter error = new SqlParameter("@errMsg", SqlDbType.NVarChar, 400);
                            retValue.Direction = ParameterDirection.Output;
                            error.Direction = ParameterDirection.Output;
                            listParam.Add(retValue);
                            listParam.Add(error);
                            #region 循环获取保存的值，并传入后台
                            for (int i = 0; i < _table.Rows.Count; i++)
                            {
                                string _colName = _table.Rows[i]["pd_colName"].ToString();
                                string _colValue = this.Request.Form[_colName] == "on" ? "1" : this.Request.Form[_colName];
                                listParam.Add(new SqlParameter("@" + _colName, _colValue));
                            }
                            #endregion
                            #region 将list转变为param
                            SqlParameter[] param = new SqlParameter[listParam.Count];

                            for (int i = 0; i < listParam.Count; i++)
                            {
                                param[i] = listParam[i];
                            }

                            #endregion
                            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                            string massage = param[3].Value.ToString();

                            if (Convert.ToBoolean(param[2].Value))
                            {
                                this.Alert("保存成功！");
                            }
                            else
                            {
                                if (string.Empty.Equals(massage))
                                {
                                    this.Alert("保存失败！");
                                }
                                else
                                {
                                    this.Alert(massage);
                                }
                            }
                        }
                        catch
                        {
                            this.Alert("保存失败！请联系管理员！");
                        }
                        #endregion
                    }
                }
                #endregion

                #region 编辑、修改

                if (this.hidType.Value == "edit")
                {
                    if (string.IsNullOrEmpty(this.hidEditProc.Value))
                    {
                        #region 没有指定存储过程时
                        try
                        {
                            DataTable _pageMstr = PageMakerHelper.GetPageMstr(this.hidPageID.Value);
                            string _pm_db = _pageMstr.Rows[0]["pm_db"].ToString();
                            string _pm_table = _pageMstr.Rows[0]["pm_table"].ToString();

                            string strSql = "Update " + _pm_db + ".dbo." + _pm_table;
                            string strSet = "";

                            List<SqlParameter> listParam = new List<SqlParameter>();
                            listParam.Add(new SqlParameter("@uID", Session["uID"].ToString()));
                            listParam.Add(new SqlParameter("@uName", Session["uName"].ToString()));

                            #region 循环获取保存的值，并传入后台
                            foreach (DataRow row in _table.Rows)
                            {
                                string _colName = row["pd_colName"].ToString();
                                string _colValue = this.Request.Form[_colName] == "on" ? "1" : this.Request.Form[_colName];
                                if (string.IsNullOrEmpty(_colValue))
                                {
                                    if (row["pd_field_control"].ToString() == "CheckBox")
                                    {
                                        _colValue = "0";
                                    }
                                }

                                listParam.Add(new SqlParameter("@" + _colName, _colValue));

                                //拼接Set语句
                                strSet += string.IsNullOrEmpty(strSet) ? (" " + _colName + " = @" + _colName) : (", " + _colName + " = @" + _colName);
                            }
                            #endregion

                            strSql += " Set " + strSet;

                            #region 获取主键并传值
                            strSql += " Where 1 = 1";
                            string[] strKeys = PageMakerHelper.GetPrimaryKeyFields(hidPageID.Value);
                            for (int i = 0; i < strKeys.Length; i++)
                            {
                                string _colName = strKeys[i];
                                string _colValue = Request["pk" + i.ToString()];
                                listParam.Add(new SqlParameter("@pk_" + _colName, _colValue));
                                //拼接where
                                strSql += " And " + strKeys[i] + " = @pk_" + strKeys[i];
                            }
                            #endregion

                            SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, strSql, listParam.ToArray());

                            this.Alert("更新成功！");
                        }
                        catch
                        {
                            this.Alert("更新失败！请联系管理员！");
                        }
                        #endregion
                    }
                    else
                    {
                        #region 指定了存储过程
                        try 
                        {
                            string strName = this.hidEditProc.Value;
                            IList<SqlParameter> listParam = new List<SqlParameter>();
                            listParam.Add(new SqlParameter("@pageID", this.hidPageID.Value));
                            listParam.Add(new SqlParameter("@xmlParam", this.BuildParamToXml()));
                            //listParam.Add(new SqlParameter("@uID", Session["uID"].ToString()));
                            //listParam.Add(new SqlParameter("@uName", Session["uName"].ToString()));
                            //listParam.Add(new SqlParameter("@PlantCode", Session["PlantCode"].ToString()));

                            SqlParameter retValue = new SqlParameter("@retValue", SqlDbType.Bit);
                            retValue.Direction = ParameterDirection.Output;
                            listParam.Add(retValue);

                            SqlParameter error = new SqlParameter("@errMsg", SqlDbType.NVarChar, 400);
                            error.Direction = ParameterDirection.Output;
                            listParam.Add(error);

                            #region 循环获取保存的值，并传入后台
                            for (int i = 0; i < _table.Rows.Count; i++)
                            {
                                string _colName = _table.Rows[i]["pd_colName"].ToString();
                                string _colValue = this.Request.Form[_colName] == "on" ? "1" : this.Request.Form[_colName];
                                listParam.Add(new SqlParameter("@" + _colName, _colValue));
                            }
                            #endregion
                            #region 没有显示的主键，也要传到后台
                            string[] strKeys = PageMakerHelper.GetPrimaryKeyFields(hidPageID.Value);
                            for (int i = 0; i < strKeys.Length; i++)
                            {
                                //PK不在编辑字段内的时候，要传后台
                                if (_table.Select("pd_colName = '" + strKeys[i] + "'").Length == 0)
                                {
                                    string _colName = strKeys[i];
                                    string _colValue = Request["pk" + i.ToString()];
                                    listParam.Add(new SqlParameter("@" + _colName, _colValue));
                                }
                            }
                            #endregion

                            #region 将list转变为param
                            SqlParameter[] param = new SqlParameter[listParam.Count];

                            for (int i = 0; i < listParam.Count; i++)
                            {
                                param[i] = listParam[i];
                            }

                            #endregion
                            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                            string massage = param[3].Value.ToString();

                            if (Convert.ToBoolean(param[2].Value))
                            {
                                this.Alert("更新成功！");
                            }
                            else
                            {
                                if (string.Empty.Equals(massage))
                                {
                                    this.Alert("更新失败！");
                                }
                                else
                                {
                                    this.Alert(massage);
                                }
                            }
                        }
                        catch
                        {
                            this.Alert("更新失败！请联系管理员！");
                        }
                        #endregion
                    }
                    
                }
                #endregion
            }
        }
        else
        {
            this.Alert("fail to retrieve addable fields");
        }
        //主键允许修改
        DesignForm();
    }
    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        //添加所有dropdownlist的SelectedIndexChanged事件
        this.Alert("测试");
        return;
    }
}
