﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

public partial class TSK_ViewTableDefinition : System.Web.UI.Page
{
    /// <summary>
    /// 显示到前台的文本
    /// </summary>
    public string PROGTEXT = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string _path = Request.QueryString["path"];
                string _tableName = Request.QueryString["table"];

                string strName = "Exec tcpc0.dbo.proc_glb_viewTableDefinition @db = '" + _path + "', @table = '" + _tableName + "'";
                string strConn = "Server=10.3.80.95;database=tcpc0;user id=sa;Password=sa";

                DataTable table = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strName).Tables[0];

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string _t = row["source"].ToString();
                        _t = _t.Replace("\t", "&nbsp;&nbsp;");
                        _t = _t.Replace(" ", "&nbsp;");
                        _t = _t.Replace("\n", "<br />");
                        _t += "<br />";

                        PROGTEXT += _t;
                    }
                }
                else
                {
                    ltlAlert.Text = "alert('没有获取到测试库数据！');";
                }
            }
            catch (Exception ex)
            {
                ltlAlert.Text = "alert('连接测试库失败！');";
            }
        }
    }
}