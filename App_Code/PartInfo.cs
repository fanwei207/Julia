using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using CommClass;
using System.Data.SqlClient;

/// <summary>
/// Summary description for PartInfo
/// </summary>
public class PartInfo
{
	public PartInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataSet GetPartInfo(string part)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@part", part);

            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_part_selectPartInfo", param);
        }
        catch
        {
            return null;
        }
    }

    public static DataTable getPartDesc(string partNo)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@partNo", partNo);

        return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_part_selectgetpartdesc", param).Tables[0];
    }

    public static DataSet AnalysePartInfo(string part)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@part", part);

            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_part_analysePartInfo", param);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 获取QAD号关联的文档
    /// </summary>
    /// <param name="strselectQad"></param>
    /// <param name="uType"></param>
    /// <param name="iUserId"></param>
    /// <param name="bosnbr"></param>
    /// <returns></returns>
    public static DataTable getDocInfoByItem(string strselectQad, string uType, int iUserId, string bosnbr)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@qad", strselectQad);
            param[1] = new SqlParameter("@uType", uType);
            param[2] = new SqlParameter("@userId", iUserId);
            param[3] = new SqlParameter("@bosnbr", bosnbr);

            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.TCPC_Supplier"), CommandType.StoredProcedure, "sp_part_selectDocInfoByItem", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
}
