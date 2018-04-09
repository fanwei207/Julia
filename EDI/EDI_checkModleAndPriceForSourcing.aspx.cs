using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using adamFuncs;
using System.Data;
using CommClass;


public partial class EDI_EDI_checkModleAndPriceForSourcing : BasePage
{
    adamClass chk = new adamClass();
    string connstr = admClass.getConnectString("SqlConn.Conn_edi");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string part = Request["qadPart"];
            string qty = Request["ordQty"];

            string sqlstr = "sp_EDI_selectBomThenPrice";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@part",part)
                , new SqlParameter("@qty",qty)
            };
            DataSet ds = SqlHelper.ExecuteDataset(connstr, CommandType.StoredProcedure, sqlstr, param);

            gvPrice.DataSource = ds.Tables[0];
            gvPrice.DataBind();

            gvModle.DataSource = ds.Tables[1];
            gvModle.DataBind();
        }
    }
}