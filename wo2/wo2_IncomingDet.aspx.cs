using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class wo2_wo2_IncomingDet : BasePage
{
    static string strConn = ConfigurationManager.AppSettings["SqlConn.BarCodeSys"];
    protected void Page_Load(object sender, EventArgs e)
    {


        lbNbr.Text = Request.QueryString["nbr"];
        lbLot.Text = Request.QueryString["lot"];
        lbQAD.Text = Request.QueryString["qad"];


        if (lbQAD.Text.Substring(0, 4) == "3020")
        {
            lbtype.Text = "灯罩";
        }
        else
        {
            lbtype.Text = "塑件和金属件";
        }

        lbVender.Text = Request.QueryString["vender"];
        lbVenderName.Text = Request.QueryString["venderName"];
        lbNum.Text = Request.QueryString["checkNum"];

        this.bind();
    }

    private void bind()
    {
        string sqlstr = "sp_wo_selectIncomingDet";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@QADID",Request.QueryString["QADID"])
            , new SqlParameter("@woID",Request.QueryString["nbrID"])
            , new SqlParameter("@part",Request.QueryString["qad"])
            , new SqlParameter("@vender",Request.QueryString["vender"])
          
        };


        DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr, param).Tables[0];



        gvInfo.DataSource = dt;
        gvInfo.DataBind();

        
    }
}