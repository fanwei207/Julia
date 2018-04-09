using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using CommClass;
using System.Configuration;

/// <summary>
/// Summary description for LampUpgrade
/// </summary>
public class LampUpgrade
{
    private adamClass adam = new adamClass();
	public LampUpgrade()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public SqlDataReader GetProductStruApplyMstr(string id)
    {
        try
        {
            string strName = "sp_lampup_selectProductStruApplyMstr";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetProductStruApplyDetail(string id)
    {
        try
        {
            string strName = "sp_lampup_selectProductStruApplyDetail";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }


    public DataTable GetLampParent(string lampCode,string mstrId)
    {
        try
        {
            string strName = "sp_lampup_selectParent";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@lampCode", lampCode);
            parm[1] = new SqlParameter("@mstrId", mstrId);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetSelectedLampParent(string ids,string lampCode,string mstrId)
    {
        try
        {
            string strName = "sp_lampup_selectParentById";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@ids", ids);
            parm[1] = new SqlParameter("@lampCode", lampCode);
            parm[2] = new SqlParameter("@mstrId", mstrId);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public Tuple<int, string> FindProdID(string prodCode)
    {
        Tuple<int, string> result = null;
        string strSQL = " Select id,item_qad From Items Where code =N'" + adam.sqlEncode(prodCode) + "' And status<>2 And type=2 ";
        SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strSQL);
        if (reader.Read())
        {
            result = new Tuple<int, string>((int)reader["id"], reader["item_qad"].ToString());

        }
        else
        {
            result = new Tuple<int, string>(0, "");
        }
        reader.Close();
        return result;

    }

    public Tuple<int, string> FindSemiProdID(string semiCode)
    {
        Tuple<int, string> result = null;
        string strSQL = " Select id,item_qad From Items Where code =N'" + adam.sqlEncode(semiCode) + "' And status<>2 And type=1 ";
        SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strSQL);
        if (reader.Read())
        {
            result = new Tuple<int, string>((int)reader["id"], reader["item_qad"].ToString());

        }
        else
        {
            result = new Tuple<int, string>(0, "");
        }
        reader.Close();
        return result;
    }

    public Tuple<int, string> FindPartID(string partCode)
    {
        Tuple<int, string> result;
        string strSQL = " Select id,item_qad From Items Where code =N'" + adam.sqlEncode(partCode) + "' And status<>2 And type=0 ";
        SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strSQL);
        if (reader.Read())
        {
            result = new Tuple<int, string>((int)reader["id"], reader["item_qad"].ToString());
        }
        else
        {
            result = new Tuple<int, string>(0, "");
        }
        reader.Close();
        return result;
    }

    public string AddProductStruApplyMstr(string lampCode,string lampCodeNew, string desc, string userId, string userName)
    {
        try
        {
            string strName = "sp_lampup_InsertProductStruApplyMstr";
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@lampCode", lampCode);
            parm[1] = new SqlParameter("@lampCodeNew", lampCodeNew);
            parm[2] = new SqlParameter("@desc", desc);
            parm[3] = new SqlParameter("@userId", userId);
            parm[4] = new SqlParameter("@userName", userName);
            return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        }
        catch
        {
            return "0";
        }
    }

    public void UpdateProductStruApplyMstr(string id, string lampCode, string lampCodeNew, string desc, string reason, string status, string userId, string userName)
    {
        string strName = "sp_lampup_UpdateProductStruApplyMstr";
        SqlParameter[] parm = new SqlParameter[8];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@lampCode", lampCode);
        parm[2] = new SqlParameter("@lampCodeNew", lampCodeNew);
        parm[3] = new SqlParameter("@desc", desc);
        parm[4] = new SqlParameter("@reason", reason);
        parm[5] = new SqlParameter("@status", status);
        parm[6] = new SqlParameter("@userId", userId);
        parm[7] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public void DeleteProductStruApplyDetail(string mstrId)
    {
        string strSQL = " DELETE FROM dbo.ProductStruApply_LampUpgrade_Det where mstrid='" + mstrId + "'";

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
    }

    public void DeleteProductStruApply_NewProduct(string mstrId)
    {
        string strSQL = " DELETE FROM dbo.ProductStruApply_LampUpgrade_NewProduct where mstrid='" + mstrId + "'";
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
    }

    public void AddProductStruApplyDetail(string mstrId, string oldProductNumber, string productNumber, string childNumber, string itemNumber, int childID, string childQad, string qty, string itemType, string notes,  string itemStr, string plantCode)
    {

        string strName = "sp_lampup_InsertProductStruApplyDetail";
        SqlParameter[] parm = new SqlParameter[12];
        parm[0] = new SqlParameter("@mstrId", mstrId);
        parm[1] = new SqlParameter("@oldProductNumber", oldProductNumber);
        parm[2] = new SqlParameter("@productNumber", productNumber);
        parm[3] = new SqlParameter("@childNumber", childNumber);
        parm[4] = new SqlParameter("@itemNumber", itemNumber);
        parm[5] = new SqlParameter("@childID", childID);
        parm[6] = new SqlParameter("@childQad", childQad);
        parm[7] = new SqlParameter("@numOfChild", qty);
        parm[8] = new SqlParameter("@childCategory ", itemType);
        parm[9] = new SqlParameter("@nodes", notes);
        parm[10] = new SqlParameter("@itemStr", itemStr);
        parm[11] = new SqlParameter("@plantCode", plantCode);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }


    public void AddProductStruApply_NewProduct(string mstrId, string oldProductNumber, string productNumber, string desc,string qad, string desc1,string desc2)
    {

        string strName = "sp_lampup_InsertProductStruApply_NewProduct";
        SqlParameter[] parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@mstrId", mstrId);
        parm[1] = new SqlParameter("@oldProductNumber", oldProductNumber);
        parm[2] = new SqlParameter("@productNumber", productNumber);
        parm[3] = new SqlParameter("@productDesc", desc);
        parm[4] = new SqlParameter("@qad", qad);
        parm[5] = new SqlParameter("@desc1", desc1);
        parm[6] = new SqlParameter("@desc2", desc2);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public int ProductStruImport(string id, string userId, string plantCode,out string message)
    {

        string strName = "sp_lampup_ProductStruImport";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@userId", userId);
        parm[2] = new SqlParameter("@plantCodeID", plantCode);
        parm[3] = new SqlParameter("@errMsg", SqlDbType.NVarChar, 500);
        parm[3].Direction = ParameterDirection.Output;
        int result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        message = parm[3].Value.ToString();
        if (result == 1)
        {
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "Item_CheckUnique");
        }
        return result;
    }

    public DataTable GetProductStruApplyList(string nbr, string lampCode, string newLampCode, string desc, string status)
    {
        try
        {
            string strName = "sp_lampup_selectLampUpgrade_Mstr";
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@nbr", nbr);
            parm[1] = new SqlParameter("@LampCode", lampCode);
            parm[2] = new SqlParameter("@desc", desc);
            parm[3] = new SqlParameter("@LampCodeNew", newLampCode);
            if (status != "")
            {
                parm[4] = new SqlParameter("@status", status);
            }
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public bool NewProductExists(string mstrid, string productNumber, out string oldproductNumber)
    {
        oldproductNumber = "";
        bool result = false;
        string strSQL = " SELECT i.code FROM ProductStruApply_LampUpgrade_NewProduct n inner join items i on n.oldItemId=i.id WHERE mstrId='" + mstrid + "' and  productNumber='" + adam.sqlEncode(productNumber) + "' order by i.itemVersion desc,i.itemSubVersion desc,i.createddate desc";
        SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strSQL);
        if (reader.Read())
        {
            oldproductNumber = reader["code"].ToString();
            result = true;
        }
        reader.Close();
        return result;
    }

    /// <summary>
    /// 发邮件给bom相关的关系人
    /// </summary>
    /// <returns></returns>
    public void SelectRelationEmail(string nbr, string prodCode)
    {
        string sqlstr = "sp_product_SelectRelationEmail";

        string from ="111";
        string to = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr).ToString();
        string copy = "";
        string subject = "BOM新增申请通过通知";
        string body = "";
        body += "<font style='font-size: 12px;'>申请单号：" + nbr + "</font><br />";
        body += "<font style='font-size: 12px;'>项目号：" + prodCode + "</font><br />";
        body += "<font style='font-size: 12px;'>新增申请被通过</font><br />";

        BasePage.SSendEmail(from, to, copy, subject, body);
    }

}