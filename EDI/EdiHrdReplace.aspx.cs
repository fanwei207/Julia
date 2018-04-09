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
using IT;

public partial class EdiHrdReplace : System.Web.UI.Page
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void txtDone_Click(object sender, EventArgs e)
    {
        string _id = Request.QueryString["id"];
        string _type = Request.QueryString["type"];

        if (string.IsNullOrEmpty(txtNbr.Text))
        {
            ltlAlert.Text = "alert('必须指定一张 客户订单！');";
            return;
        }
        else if (!getEdiData.CheckPoExist(txtNbr.Text))
        {
            ltlAlert.Text = "alert('客户订单 不存在！');";
            return;
        }
        else if (!getEdiData.CheckPodetCompare(_id, txtNbr.Text))
        {
            ltlAlert.Text = "alert('替换的订单与原订单不匹配！');";
            return;
        }
        else if (!getEdiData.UpdatePoHrdNeedProp(_id, txtNbr.Text, Session["PlantCode"].ToString(), _type))
        {
            ltlAlert.Text = "alert('保存失败！请联系系统管理员！');";
            return;
        }
        else
        {
            ltlAlert.Text = "window.close();";
        }
    }
}