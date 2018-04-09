using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

public partial class TSK_ViewStoredProcedure : System.Web.UI.Page
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
                string _progName = Request.QueryString["prog"];

                string strName = "Exec tcpc0.dbo.proc_glb_viewStoredProcedure @db = '" + _path + "', @name = '" + _progName + "'";
                string strConn = "Server=10.3.80.95;database=tcpc0;user id=sa;Password=sa";

                DataTable table = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strName).Tables[0];

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string _t = row["source"].ToString().TrimEnd();

                        //删除空行
                        if (_t.Length == 0)
                        {
                            continue;
                        }

                        _t = _t.Replace("\t", "&nbsp;&nbsp;");
                        _t = _t.Replace(" ", "&nbsp;");
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