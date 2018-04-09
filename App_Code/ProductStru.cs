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
/// Summary description for ProductStru
/// </summary>
public class ProductStru
{
    private adamClass adam = new adamClass();
    public ProductStru()
    {
        adamClass adam = new adamClass();
    }

    public DataTable GetProductStruApplyList(string nbr, string prodCode, string desc, string status)
    {
        try
        {
            string strName = "sp_product_selectProductStruApplyList";
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@nbr", nbr);
            parm[1] = new SqlParameter("@prodCode", prodCode);
            parm[2] = new SqlParameter("@desc", desc);
            if (status != "")
            {
                parm[3] = new SqlParameter("@status", status);
            }
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public SqlDataReader GetProductStruApplyMstr(string id)
    {
        try
        {
            string strName = "sp_product_selectProductStruApplyMstr";
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
            string strName = "sp_product_selectProductStruApplyDetail";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }


    public DataTable GetProductStruApplyNewProduct(string id)
    {
        try
        {
            string strName = "sp_product_selectProductStruApply_NewProduct";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public string AddProductStruApplyMstr(string prodCode, string desc, string userId, string userName)
    {
        try
        {
            string strName = "sp_product_InsertProductStruApplyMstr";
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@prodCode", prodCode);
            parm[1] = new SqlParameter("@desc", desc);
            parm[2] = new SqlParameter("@userId", userId);
            parm[3] = new SqlParameter("@userName", userName);
            return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        }
        catch
        {
            return "0";
        }
    }

    public void UpdateProductStruApplyMstr(string id, string prodCode, string desc, string reason, string status, string userId, string userName)
    {
        string strName = "sp_product_UpdateProductStruApplyMstr";
        SqlParameter[] parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@prodCode", prodCode);
        parm[2] = new SqlParameter("@desc", desc);
        parm[3] = new SqlParameter("@reason", reason);
        parm[4] = new SqlParameter("@status", status);
        parm[5] = new SqlParameter("@userId", userId);
        parm[6] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public void SubmitProductStruApplyMstr(string id, string desc, string userId, string userName)
    {
        string strName = "sp_product_SubmitProductStruApply";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@desc", desc);
        parm[2] = new SqlParameter("@userId", userId);
        parm[3] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public string CheckProductSize(string id)
    {
        string strName = "sp_product_checkExistSize";
        SqlParameter[] parm = new SqlParameter[2];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@message", SqlDbType.NVarChar,1000);
        parm[1].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        return parm[1].Value.ToString();
    }

    public Tuple<int, string> FindProdID(string prodCode)
    {
        Tuple<int, string> result = null;
        string strSQL = " Select top 1 id,item_qad From Items Where code =N'" + adam.sqlEncode(prodCode) + "' And status<>2 And type=2 order by modifiedDate desc ";
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
        string strSQL = " Select top 1 id,item_qad From Items Where code =N'" + adam.sqlEncode(semiCode) + "' And status<>2 And type=1 order by modifiedDate desc";
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
        string strSQL = " Select top 1 id,item_qad From Items Where code =N'" + adam.sqlEncode(partCode) + "' And type=0 order by modifiedDate desc";//"' And status<>2 And type=0 ";
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

    public bool BomItemExist(string strProdCode, int strItemID, string strItemType,string posCode)
    {
        string strSQL = " Select Count(ps.childID) From Items i "
               + " Inner Join Product_stru ps On ps.productID=i.id "
               + " Where code=N'" + adam.sqlEncode(strProdCode) + "' And ps.childID='" + strItemID + "' And ps.childCategory='" + strItemType + "' and (REPLACE(isnull(ps.posCode,''),char(13), ' ')=N'" + adam.sqlEncode(posCode) + "' or REPLACE(isnull(ps.posCode,''), char(10), ' ')=N'" + adam.sqlEncode(posCode) + "' ) And i.status<>2 ";
        if ((int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strSQL) > 0)
            return true;
        else
            return false;
    }

    public bool BomItemExistAndQty(string strProdCode, int strItemID, string posCode, string qty)
    {
        string strSQL = " Select Count(ps.childID) From Items i "
               + " Inner Join Product_stru ps On ps.productID=i.id "
               + " Where code=N'" + adam.sqlEncode(strProdCode) + "' And ps.childID='" + strItemID + "' And ps.numOfChild='" + qty + "' and (REPLACE(isnull(ps.posCode,''),char(13), ' ')=N'" + adam.sqlEncode(posCode) + "' or REPLACE(isnull(ps.posCode,''), char(10), ' ')=N'" + adam.sqlEncode(posCode) + "' ) And i.status<>2 ";
        if ((int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strSQL) > 0)
            return true;
        else
            return false;
    }

    public void AddProductStruApplyDetail(string mstrId, string productNumber, string childNumber, string itemNumber, int productID,string prodQad, int childID, string childQad, string qty, string itemType, string notes, string pos, string itemStr, string plantCode)
    {

        string strName = "sp_product_InsertProductStruApplyDetail";
        SqlParameter[] parm = new SqlParameter[14];
        parm[0] = new SqlParameter("@mstrId", mstrId);
        parm[1] = new SqlParameter("@productNumber", productNumber);
        parm[2] = new SqlParameter("@childNumber", childNumber);
        parm[3] = new SqlParameter("@itemNumber", itemNumber);
        parm[4] = new SqlParameter("@productID", productID);
        parm[5] = new SqlParameter("@productQad", prodQad);
        parm[6] = new SqlParameter("@childID", childID);
        parm[7] = new SqlParameter("@childQad", childQad);
        parm[8] = new SqlParameter("@numOfChild", qty);
        parm[9] = new SqlParameter("@childCategory ", itemType);
        parm[10] = new SqlParameter("@nodes", notes);
        parm[11] = new SqlParameter("@posCode", pos);
        parm[12] = new SqlParameter("@itemStr", itemStr);
        parm[13] = new SqlParameter("@plantCode", plantCode);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public void AddProductStruApply_NewProduct(string mstrId,string source, string productNumber, string desc, string plantCode)
    {

        string strName = "sp_product_InsertProductStruApply_NewProduct";
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@mstrId", mstrId);
        parm[1] = new SqlParameter("@source", source);
        parm[2] = new SqlParameter("@productNumber", productNumber);
        parm[3] = new SqlParameter("@productDesc", desc);
        parm[4] = new SqlParameter("@plantCode", plantCode);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public int ProductStruImport(string id, string userId, string plantCode)
    {

        string strName = "sp_product_ProductStruImport";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@userId", userId);
        parm[2] = new SqlParameter("@plantCodeID", plantCode);
        int result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        if (result == 0)
        {
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "Item_CheckUnique");
        }
        return result;
    }


    public void DeleteProductStruApplyDetail(string mstrId)
    {
        string strSQL = " DELETE FROM dbo.ProductStruApply_Det where mstrid='" + mstrId+"'";

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
    }

    public void DeleteProductStruApply_NewProduct(string mstrId)
    {
        string strSQL = " DELETE FROM dbo.ProductStruApply_NewProduct where isnull(code,'')='' and mstrid='" + mstrId + "'";
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
    }

    public bool ProjectExists(string prodCode)
    {
        bool result = false; 
        string strSQL = " SELECT * FROM RD_Workflow.dbo.RDW_Mstr WHERE RDW_ProdCode='" + prodCode + "'";
        SqlDataReader reader= SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strSQL);
        if (reader.Read())
        {
            result = true;
        }
        reader.Close();
        return result;
    }

    public bool NewProductExists(string mstrid, ref string productNumber)
    {
        bool result = false;
        string strSQL = " SELECT code FROM ProductStruApply_NewProduct WHERE mstrId='" + mstrid + "' and  productNumber='" + adam.sqlEncode(productNumber) + "'";
        SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strSQL);
        if (reader.Read())
        {
            productNumber = reader["code"].ToString();
            result = true;
        }
        reader.Close();
        return result;
    }

    public bool IsLocked(string part)
    {
        string strName = "qaddoc.dbo.sp_qad_checkLockUL";
        SqlParameter[] parm = new SqlParameter[2]; 
        parm[0] = new SqlParameter("@part", part);
        parm[1] = new SqlParameter("@lockPart", SqlDbType.NVarChar);
        parm[1].Size = 500;
        parm[1].Direction = ParameterDirection.Output;
        if ((int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName,parm) == 1)
            return true;
        else
            return false;
    }

    public string[] GetParentQad(string id)
    {
        string strSQL = " SELECT i.item_qad FROM dbo.Product_stru s INNER JOIN dbo.Items i ON s.productID=i.id WHERE s.childID=" + id;
        SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strSQL);
        List<string> result=new List<string>();
        while (reader.Read())
        {
            result.Add(reader["item_qad"].ToString());
        }
        reader.Close();
        return result.ToArray();
    }

    public string[] GetNewZhengDeng(string id)
    {
        string strSQL = " SELECT code FROM tcpc0..ProductStruApply_NewProduct WHERE item_qad LIKE '1%0' and mstrId='" + id + "'";
        SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strSQL);
        List<string> result=new List<string>();
        while (reader.Read())
        {
            result.Add(reader["code"].ToString());
        }
        reader.Close();
        return result.ToArray();
    }

    public string[] GetFlowNodePersonEmail(string id)
    {
        string strSQL = " SELECT u.email FROM WorkFlow.dbo.NWF_FlowNode n INNER JOIN WorkFlow.dbo.NWF_NodePerson p ON n.Node_Id=p.Node_Id INNER JOIN dbo.Users u ON p.Person_UserId=u.userID WHERE ISNULL(u.email,'')<>'' AND  n.Node_Id='" + id + "'";
        SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strSQL);
        List<string> result = new List<string>();
        while (reader.Read())
        {
            result.Add(reader["email"].ToString());
        }
        reader.Close();
        return result.ToArray();
    }


    #region //更新部件信息

    public SqlDataReader GetProductStruApplyUpdateMstr(string id)
    {
        try
        {
            string strName = "sp_product_selectProductStruApplyUpdateMstr";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetProductStruApplyUpdateDetail(string id)
    {
        try
        {
            string strName = "sp_product_selectProductStruApplyUpdateDetail";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetProductStruApplyUpdateProduct(string id)
    {
        try
        {
            string strName = "sp_product_selectProductStruApply_UpdateProduct";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public string UpdateProductStruApplyMstr(string prodCode, string desc, string userId, string userName)
    {
        try
        {
            string strName = "sp_product_InsertProductStruApplyUpdateMstr";
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@prodCode", prodCode);
            parm[1] = new SqlParameter("@desc", desc);
            parm[2] = new SqlParameter("@userId", userId);
            parm[3] = new SqlParameter("@userName", userName);
            return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        }
        catch
        {
            return "0";
        }
    }

    public void ProductStruApplyUpdateMstr(string id, string prodCode, string desc, string reason, string status, string userId, string userName)
    {
        string strName = "sp_product_ProductStruApplyUpdateMstr";
        SqlParameter[] parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@prodCode", prodCode);
        parm[2] = new SqlParameter("@desc", desc);
        parm[3] = new SqlParameter("@reason", reason);
        parm[4] = new SqlParameter("@status", status);
        parm[5] = new SqlParameter("@userId", userId);
        parm[6] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public DataTable CheckDataDouble(string id)
    {
        try
        {
            string strName = "sp_product_CheckDataDouble";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public void SubmitProductStruApplyUpdateMstr(string id, string desc, string userId, string userName)
    {
        string strName = "sp_product_submitProductStruUpdateApply";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@desc", desc);
        parm[2] = new SqlParameter("@userId", userId);
        parm[3] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public void CancelProductStruApplyMstr(string id, string prodCode, string desc, string reason, string status, string userId, string userName)
    {
        string strName = "sp_product_CancelProductStruApplyMstr";
        SqlParameter[] parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@prodCode", prodCode);
        parm[2] = new SqlParameter("@desc", desc);
        parm[3] = new SqlParameter("@reason", reason);
        parm[4] = new SqlParameter("@status", status);
        parm[5] = new SqlParameter("@userId", userId);
        parm[6] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public int ProductStruUpdateImport(string id, string userId, string plantCode)
    {

        string strName = "sp_product_ProductStruUpdateImport";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@userId", userId);
        parm[2] = new SqlParameter("@plantCodeID", plantCode);
        int result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        if (result == 0)
        {
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "Item_CheckUnique");
        }
        return result;
    }

    public void ReturnProductStruApplyMstr(string id, string prodCode, string desc, string reason, string status, string userId, string userName)
    {
        string strName = "sp_product_ReturnProductStruApplyMstr";
        SqlParameter[] parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@prodCode", prodCode);
        parm[2] = new SqlParameter("@desc", desc);
        parm[3] = new SqlParameter("@reason", reason);
        parm[4] = new SqlParameter("@status", status);
        parm[5] = new SqlParameter("@userId", userId);
        parm[6] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public string ProductStruApplyUpdateMstr(string prodCode, string desc, string userId, string userName)
    {
        try
        {
            string strName = "sp_product_InsertProductStruUpdateApplyMstr";
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@prodCode", prodCode);
            parm[1] = new SqlParameter("@desc", desc);
            parm[2] = new SqlParameter("@userId", userId);
            parm[3] = new SqlParameter("@userName", userName);
            return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        }
        catch
        {
            return "0";
        }
    }

    public void DeleteProductStruApplyUpdateDetail(string mstrId)
    {
        string strSQL = " DELETE FROM dbo.ProductStruApplyUpdate_Det where mstrid='" + mstrId + "'";

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
    }

    public void DeleteProductStruApply_UpdateProduct(string mstrId)
    {
        string strSQL = " DELETE FROM dbo.ProductStruApply_UpdateProduct where isnull(code,'')='' and mstrid='" + mstrId + "'";
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
    }

    public DataTable CheckProductExit(string productNumber, string childNumber)
    {
        try
        {
            string strName = "sp_product_CheckProductExit";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@productNumber", productNumber);
            parm[1] = new SqlParameter("@childNumber", childNumber);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable CheckGenZhongHaoExit(string ProdNo)
    {
        try
        {
            string strName = "sp_product_CheckGenZhongDanExit";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@ProdNo", ProdNo);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public void InsertProductStruApplyUpdateDetail(string mstrId, string productNumber, string productQad, string childNumber, string itemNumber, int productID, int childID, string childQad
        , string qty, string itemType, string pos, string itemStr
        , string newChildNumber, int newChildID, string newChildqad, string newPos, string newQty, string reson, string types, string newitemType, string dec, string newdec, string no, string plantCode)
    {

        string strName = "sp_product_InsertProductStruApplyUpdateDetail";
        SqlParameter[] parm = new SqlParameter[24];
        parm[0] = new SqlParameter("@mstrId", mstrId);
        parm[1] = new SqlParameter("@productNumber", productNumber);
        parm[2] = new SqlParameter("@productQad", productQad);
        parm[3] = new SqlParameter("@childNumber", childNumber);
        parm[4] = new SqlParameter("@itemNumber", itemNumber);

        parm[5] = new SqlParameter("@productID", productID);
        parm[6] = new SqlParameter("@childID", childID);
        parm[7] = new SqlParameter("@childQad", childQad);
        parm[8] = new SqlParameter("@numOfChild", qty);
        parm[9] = new SqlParameter("@childCategory ", itemType);

        parm[10] = new SqlParameter("@posCode", pos);
        parm[11] = new SqlParameter("@itemStr", itemStr);
        parm[12] = new SqlParameter("@newChildNumber", newChildNumber);
        parm[13] = new SqlParameter("@newchildID", newChildID);
        parm[14] = new SqlParameter("@newchildqad", newChildqad);

        parm[15] = new SqlParameter("@newposCode", newPos);
        parm[16] = new SqlParameter("@newnumOfChild", newQty);
        parm[17] = new SqlParameter("@reson", reson);
        parm[18] = new SqlParameter("@types", types);
        parm[19] = new SqlParameter("@newitemType", newitemType);
        parm[20] = new SqlParameter("@dec", dec);
        parm[21] = new SqlParameter("@newdec", newdec);

        parm[22] = new SqlParameter("@no", no);
        parm[23] = new SqlParameter("@plantCode", plantCode);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }


    public DataTable GetProductStruApplyUpdateList(string nbr, string prodCode, string desc, string status,int uID)
    {
        try
        {
            string strName = "sp_product_selectProductStruApplyUpdateList";
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@nbr", nbr);
            parm[1] = new SqlParameter("@prodCode", prodCode);
            parm[2] = new SqlParameter("@desc", desc);
            if (status != "")
            {
                parm[3] = new SqlParameter("@status", status);
            }
            parm[4] = new SqlParameter("@uID", uID);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public int CheckQADExit(string prodCode, string partCode, string newpartCode, string poscode, string newposcode, string types)
    {
        try
        {
            int result = -1;
            string strName = "sp_product_CheckQADExit";
            SqlParameter[] parm = new SqlParameter[6];
            parm[0] = new SqlParameter("@prodCode", prodCode);
            parm[1] = new SqlParameter("@partCode", partCode);
            parm[2] = new SqlParameter("@newpartCode", newpartCode);
            parm[3] = new SqlParameter("@poscode", poscode);
            parm[4] = new SqlParameter("@newposcode", newposcode);
            parm[5] = new SqlParameter("@types", types);

            return result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return -2;
        }

    }

    public string CheckLockUl(string prodCode)
    {
        string strLockUL = "";
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@part", prodCode);
            param[1] = new SqlParameter("@retValue", SqlDbType.NVarChar, 50);
            param[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_checkULInf", param);

            strLockUL = param[1].Value.ToString();
        }
        catch
        {
            strLockUL = "ERRER";

        }
        return strLockUL;
    }

    #endregion

    public SqlDataReader FindUsersInfo(string uID)
    {
        string sqlstr = "SELECT userName,userNo,email FROM dbo.Users WHERE userID = " + uID;

        return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, sqlstr);
    }

    #region //整灯修改包装

    public string PackingUpgradeApplyMstr(string prodCode, string desc, string userId, string userName)
    {
        try
        {
            string strName = "sp_PackingUpdate_InsertApplyMstr";
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@prodCode", prodCode);
            parm[1] = new SqlParameter("@desc", desc);
            parm[2] = new SqlParameter("@userId", userId);
            parm[3] = new SqlParameter("@userName", userName);
            return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        }
        catch
        {
            return "0";
        }
    }

    public void DeletePackingUpgradeDetail(string mstrId)
    {
        string strSQL = " DELETE FROM dbo.PackingUpgradeApply_Det where mstrid='" + mstrId + "'";

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
    }

    public void DeletePackingUpgradeDetailNew(string mstrId)
    {
        string strSQL = " DELETE FROM dbo.PackingUpgradeApply_Prodnew where mstrId='" + mstrId + "'";

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
    }

    public void SubmitPackingUpgradeDetailNew(string mstrId)
    {
        string strSQL = " update dbo.PackingUpgradeApply_Mstr set status = 20 where id='" + mstrId + "'";

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
    }

    public DataTable CheckPackingUpgradeDataDouble(string id)
    {
        try
        {
            string strName = "sp_PackingUpgrade_CheckDataDouble";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable CheckPackingUpgradeDetailExists(string id)
    {
        try
        {
            string strName = "sp_PackingUpgrade_CheckDetailExists";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public void Submit_PackingUpgradeApplyMstr(string id, string desc, string userId, string userName)
    {
        string strName = "sp_PackingUpgrade_submitApply";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@desc", desc);
        parm[2] = new SqlParameter("@userId", userId);
        parm[3] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public int PackingUpgradeCheckUpgradeInfo(string prodCode, string childcode, string id)
    {
        try
        {
            int result = -1;
            string strName = "sp_PackingUpdate_CheckUpgradeInfo";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@prodCode", prodCode);
            parm[1] = new SqlParameter("@childcode", childcode);
            parm[2] = new SqlParameter("@id", id);

            return result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return -2;
        }

    }
    public Tuple<int, string> PackingUpgradeFindProdID(string prodCode)
    {
        Tuple<int, string> result = null;
        //string strSQL = " Select TOP 1 id,item_qad From Items Where code  like N'%' + '" + adam.sqlEncode(prodCode) + "' And status<>2 And type=2 And item_qad is not null";
        string strSQL = " Select top 1 id,item_qad From Items Where  code =N'" + adam.sqlEncode(prodCode) + "'  And status<>2 And type=2 And item_qad is not null order by modifiedDate desc";

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

    public Tuple<int, string> PackingUpgradeFindProdIDFind(string prodCode)
    {
        Tuple<int, string> result = null;
        string strSQL = " Select TOP 1 id,item_qad From Items Where code  like N'%' + '" + adam.sqlEncode(prodCode) + "' And status<>2 And type=2 And item_qad is not null  order by modifiedDate desc";
        //string strSQL = " Select id,item_qad From Items Where  code =N'" + adam.sqlEncode(prodCode) + "'  And status<>2 And type=2 And item_qad is not null";

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
    public void InsertPackingUpgradeApplyDetail(string mstrId, string productNumber, string productQad, string productNumbernew, string productQadnew, string childNumber, string childQad
                                , int productID, int childID, string dec, string qad, string dec1, string dec2, string pos, string qty, string itemNums, string itemType
                                , string itemStr, string reson, string uID, string plantCode)
    {

        string strName = "sp_PackingUpgrade_InsertApplyDetail";
        SqlParameter[] parm = new SqlParameter[21];
        parm[0] = new SqlParameter("@mstrId", mstrId);
        parm[1] = new SqlParameter("@productNumber", productNumber);
        parm[2] = new SqlParameter("@productQad", productQad);
        parm[3] = new SqlParameter("@productNumbernew", productNumbernew);
        parm[4] = new SqlParameter("@productQadnew", productQadnew);
        parm[5] = new SqlParameter("@childNumber", childNumber);
        parm[6] = new SqlParameter("@childQad", childQad);
        parm[7] = new SqlParameter("@productID", productID);
        parm[8] = new SqlParameter("@childID", childID);
        parm[9] = new SqlParameter("@dec", dec);
        parm[10] = new SqlParameter("@qad", qad);
        parm[11] = new SqlParameter("@dec1", dec1);
        parm[12] = new SqlParameter("@dec2", dec2);
        parm[13] = new SqlParameter("@posCode", pos);
        parm[14] = new SqlParameter("@numOfChild", qty);
        parm[15] = new SqlParameter("@itemNums", itemNums);
        parm[16] = new SqlParameter("@itemType", itemType);
        parm[17] = new SqlParameter("@itemStr", itemStr);
        parm[18] = new SqlParameter("@reson", reson);
        parm[19] = new SqlParameter("@uID", uID);
        parm[20] = new SqlParameter("@plantCode", plantCode);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public SqlDataReader GetPackingUpgradepplyMstr(string id)
    {
        try
        {
            string strName = "sp_PackingUpgrade_selectApplyMstr";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetPackingUpgradeApplyDetail(string id, string status)
    {
        try
        {
            string strName = "sp_PackingUpgrade_selectApplyDetail";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@id", id);
            parm[1] = new SqlParameter("@status", status);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetPackingUpgradeApplyList(string nbr, string prodCode, string desc, string status, int uID)
    {
        try
        {
            string strName = "sp_PackingUpgrade_selectApplyList";
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@nbr", nbr);
            parm[1] = new SqlParameter("@prodCode", prodCode);
            parm[2] = new SqlParameter("@desc", desc);
            if (status != "")
            {
                parm[3] = new SqlParameter("@status", status);
            }
            parm[4] = new SqlParameter("@uID", uID);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable GetPackingUpgradeVersion(string prodcode, string id, string Status)
    {
        try
        {
            string strName = "sp_PackingUpgrade_selectVersion";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@prodcode", prodcode);
            parm[1] = new SqlParameter("@id", id);
            parm[2] = new SqlParameter("@Status", Status);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable CheckPackingUpgradeExists(string prodcode, string id, string Status)
    {
        try
        {
            string strName = "sp_PackingUpgrade_selectNewBomexists";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@prodcode", prodcode);
            parm[1] = new SqlParameter("@id", id);
            parm[2] = new SqlParameter("@Status", Status);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable GetPackingUpgradeExport(string ids, string prodcode, string id)
    {
        try
        {
            string strName = "sp_PackingUpgrade_selectVersionExport ";
            SqlParameter[] parm = new SqlParameter[3];
            parm[0] = new SqlParameter("@ids", ids);
            parm[1] = new SqlParameter("@prodcode", prodcode);
            parm[2] = new SqlParameter("@id", id);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetPackingUpgradeBomExport(string id)
    {
        try
        {
            string strName = "sp_PackingUpgrade_selectBOMExport";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public int InsertPackingUpgradeApplyDetailNew(string mstrId, string productNumber, string productQad, int productid, string productNumbernew, string productQadnew
            , string dec, string dec1, string dec2, string qad, string userId, string userName, string plantCode)
    {
        int result = -1;
        try
        {
            string strName = "sp_PackingUpgrade_InsertApplyDetailNew";
            SqlParameter[] parm = new SqlParameter[13];
            parm[0] = new SqlParameter("@mstrId", mstrId);
            parm[1] = new SqlParameter("@productNumber", productNumber);
            parm[2] = new SqlParameter("@productQad", productQad);
            parm[3] = new SqlParameter("@productid", productid);
            parm[4] = new SqlParameter("@productNumbernew", productNumbernew);

            parm[5] = new SqlParameter("@productQadnew", productQadnew);
            parm[6] = new SqlParameter("@dec", dec);
            parm[7] = new SqlParameter("@dec1", dec1);
            parm[8] = new SqlParameter("@dec2", dec2);
            parm[9] = new SqlParameter("@qad", qad);

            parm[10] = new SqlParameter("@userId", userId);
            parm[11] = new SqlParameter("@userName", userName);
            parm[12] = new SqlParameter("@plantCode", plantCode);
            return result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return -2;
        }
    }

    public int InsertPackingUpgradeApplyDetailNew1(string mstrId, string productNumber, string productQad, int productid, string productNumbernew, string productQadnew
        , string dec, string dec1, string dec2, string qad, string userId, string userName, string plantCode)
    {
        int result = -1;
        try
        {
            string strName = "sp_PackingUpgrade_InsertApplyDetailNew1";
            SqlParameter[] parm = new SqlParameter[13];
            parm[0] = new SqlParameter("@mstrId", mstrId);
            parm[1] = new SqlParameter("@productNumber", productNumber);
            parm[2] = new SqlParameter("@productQad", productQad);
            parm[3] = new SqlParameter("@productid", productid);
            parm[4] = new SqlParameter("@productNumbernew", productNumbernew);

            parm[5] = new SqlParameter("@productQadnew", productQadnew);
            parm[6] = new SqlParameter("@dec", dec);
            parm[7] = new SqlParameter("@dec1", dec1);
            parm[8] = new SqlParameter("@dec2", dec2);
            parm[9] = new SqlParameter("@qad", qad);

            parm[10] = new SqlParameter("@userId", userId);
            parm[11] = new SqlParameter("@userName", userName);
            parm[12] = new SqlParameter("@plantCode", plantCode);
            return result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return -2;
        }
    }
    public void PackingUpgrade_RejectApplyMstr(string id, string prodCode, string desc, string reason, string status, string userId, string userName)
    {
        string strName = "sp_PackingUpgrade_RejectApplyMstr";
        SqlParameter[] parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@prodCode", prodCode);
        parm[2] = new SqlParameter("@desc", desc);
        parm[3] = new SqlParameter("@reason", reason);
        parm[4] = new SqlParameter("@status", status);
        parm[5] = new SqlParameter("@userId", userId);
        parm[6] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public void CancelPackingUpgradeApplyMstr(string id, string prodCode, string desc, string reason, string status, string userId, string userName)
    {
        string strName = "sp_PackingUpgrade_CancelApplyMstr";
        SqlParameter[] parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@prodCode", prodCode);
        parm[2] = new SqlParameter("@desc", desc);
        parm[3] = new SqlParameter("@reason", reason);
        parm[4] = new SqlParameter("@status", status);
        parm[5] = new SqlParameter("@userId", userId);
        parm[6] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public int PackingUpgrade_Import(string id, string userId, string plantCode)
    {

        string strName = "sp_PackingUpgrade_PackingUpgradeImport";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@userId", userId);
        parm[2] = new SqlParameter("@plantCodeID", plantCode);
        int result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        //if (result == 0)
        //{
        //    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "Item_CheckUnique");
        //}
        return result;
    }

    public int CheckStruExists(string parent,string parentqad,string child,string childqad, bool checkstru)
    {
        string strName = "sp_PackingUpgrade_CheckStruExists";
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@parent", parent);
        parm[1] = new SqlParameter("@parentqad", parentqad);
        parm[2] = new SqlParameter("@child", child);
        parm[3] = new SqlParameter("@childqad", childqad);
        parm[4] = new SqlParameter("@checkstru", checkstru);
        int result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        return result;
    }

    #endregion

    #region //根据ECN更新部件信息

    public SqlDataReader GetProductStruApplyUpdateByECNMstr(string id)
    {
        try
        {
            string strName = "sp_product_selectProductStruApplyUpdateByECNMstr";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetProductStruApplyUpdateByECNDetail(string id)
    {
        try
        {
            string strName = "sp_product_selectProductStruApplyUpdateByECNDetail";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    /*
    public DataTable GetProductStruApplyUpdateProduct(string id)
    {
        try
        {
            string strName = "sp_product_selectProductStruApply_UpdateProduct";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    */
    public string UpdateProductStruApplyByECNMstr(string prodCode, string desc, string userId, string userName)
    {
        try
        {
            string strName = "sp_product_InsertProductStruApplyUpdateByECNMstr";
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@prodCode", prodCode);
            parm[1] = new SqlParameter("@desc", desc);
            parm[2] = new SqlParameter("@userId", userId);
            parm[3] = new SqlParameter("@userName", userName);
            return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        }
        catch
        {
            return "0";
        }
    }

    public void ProductStruApplyUpdateByECNMstr(string id, string prodCode, string desc, string reason, string status, string userId, string userName)
    {
        string strName = "sp_product_ProductStruApplyUpdateByECNMstr";
        SqlParameter[] parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@prodCode", prodCode);
        parm[2] = new SqlParameter("@desc", desc);
        parm[3] = new SqlParameter("@reason", reason);
        parm[4] = new SqlParameter("@status", status);
        parm[5] = new SqlParameter("@userId", userId);
        parm[6] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public DataTable CheckDataDoubleByECN(string id)
    {
        try
        {
            string strName = "sp_product_CheckDataDoubleByECN";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public void SubmitProductStruApplyUpdateByECNMstr(string id, string desc, string optypes, string userId, string userName)
    {
        string strName = "sp_product_submitProductStruUpdateByECNApply";
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@desc", desc);
        parm[2] = new SqlParameter("@optypes", optypes);
        parm[3] = new SqlParameter("@userId", userId);
        parm[4] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public void CancelProductStruApplyByECNMstr(string id, string prodCode, string desc, string reason, string status, string userId, string userName)
    {
        string strName = "sp_product_CancelProductStruApplyByECNMstr";
        SqlParameter[] parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@prodCode", prodCode);
        parm[2] = new SqlParameter("@desc", desc);
        parm[3] = new SqlParameter("@reason", reason);
        parm[4] = new SqlParameter("@status", status);
        parm[5] = new SqlParameter("@userId", userId);
        parm[6] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public int ProductStruUpdateByECNImport(string id, string userId, string plantCode)
    {

        string strName = "sp_product_ProductStruUpdateByECNImport";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@userId", userId);
        parm[2] = new SqlParameter("@plantCodeID", plantCode);
        int result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        if (result == 0)
        {
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "Item_CheckUnique");
        }
        return result;
    }

    public void ReturnProductStruApplyByECNMstr(string id, string prodCode, string desc, string reason, string status, string userId, string userName)
    {
        string strName = "sp_product_ReturnProductStruApplyByECNMstr";
        SqlParameter[] parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@prodCode", prodCode);
        parm[2] = new SqlParameter("@desc", desc);
        parm[3] = new SqlParameter("@reason", reason);
        parm[4] = new SqlParameter("@status", status);
        parm[5] = new SqlParameter("@userId", userId);
        parm[6] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public string ProductStruApplyUpdateByECNMstr(string prodCode, string desc, string userId, string userName)
    {
        try
        {
            string strName = "sp_product_InsertProductStruUpdateApplyByECNMstr";
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@prodCode", prodCode);
            parm[1] = new SqlParameter("@desc", desc);
            parm[2] = new SqlParameter("@userId", userId);
            parm[3] = new SqlParameter("@userName", userName);
            return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        }
        catch
        {
            return "0";
        }
    }

    public int CheckECNStatusByECN(string ecnno, string remark, string userId, string userName)
    {
        try
        {
            int result = -1;
            string strName = "sp_product_CheckECNStatusByECN";
            SqlParameter[] parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@ecnno", ecnno);
            parm[1] = new SqlParameter("@remark", remark);
            parm[2] = new SqlParameter("@userId", userId);
            parm[3] = new SqlParameter("@userName", userName);

            return result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return -2;
        }

    }


    public void DeleteProductStruApplyUpdateByECNDetail(string mstrId)
    {
        string strSQL = " DELETE FROM dbo.ProductStruApplyUpdateByECN_Det where mstrid='" + mstrId + "'";

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
    }

    public void InsertProductStruApplyUpdateByECNDetail(string mstrId, string producttop, string producttopqad, string productNumber, string productQad, string childNumber, string itemNumber, int productID, int childID, string childQad
        , decimal qty, string itemType, string pos, string itemStr
        , string newChildNumber, int newChildID, string newChildqad, string newPos, string newQty, string reson, string types, string newitemType, string dec, string newdec, string plantCode)
    {

        string strName = "sp_product_InsertProductStruApplyUpdateByECNDetail";
        SqlParameter[] parm = new SqlParameter[25];
        parm[0] = new SqlParameter("@mstrId", mstrId);
        parm[1] = new SqlParameter("@productNumber", productNumber);
        parm[2] = new SqlParameter("@productQad", productQad);
        parm[3] = new SqlParameter("@childNumber", childNumber);
        parm[4] = new SqlParameter("@itemNumber", itemNumber);

        parm[5] = new SqlParameter("@productID", productID);
        parm[6] = new SqlParameter("@childID", childID);
        parm[7] = new SqlParameter("@childQad", childQad);
        parm[8] = new SqlParameter("@numOfChild", Convert.ToDecimal(qty));
        parm[9] = new SqlParameter("@childCategory ", itemType);

        parm[10] = new SqlParameter("@posCode", pos);
        parm[11] = new SqlParameter("@itemStr", itemStr);
        parm[12] = new SqlParameter("@newChildNumber", newChildNumber);
        parm[13] = new SqlParameter("@newchildID", newChildID);
        parm[14] = new SqlParameter("@newchildqad", newChildqad);

        parm[15] = new SqlParameter("@newposCode", newPos);
        parm[16] = new SqlParameter("@newnumOfChild", Convert.ToDecimal(newQty));
        parm[17] = new SqlParameter("@reson", reson);
        parm[18] = new SqlParameter("@types", types);
        parm[19] = new SqlParameter("@newitemType", newitemType);
        parm[20] = new SqlParameter("@dec", dec);
        parm[21] = new SqlParameter("@newdec", newdec);

        parm[22] = new SqlParameter("@plantCode", plantCode);
        parm[23] = new SqlParameter("@producttop", producttop);
        parm[24] = new SqlParameter("@producttopqad", producttopqad);

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }


    public DataTable GetProductStruApplyUpdateByECNList(string nbr, string prodCode, string desc, string status, int uID)
    {
        try
        {
            string strName = "sp_product_selectProductStruApplyUpdateByECNList";
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@nbr", nbr);
            parm[1] = new SqlParameter("@prodCode", prodCode);
            parm[2] = new SqlParameter("@desc", desc);
            if (status != "")
            {
                parm[3] = new SqlParameter("@status", status);
            }
            parm[4] = new SqlParameter("@uID", uID);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public int CheckQADExitByECN(string prodCode, string partCode, string newpartCode, string poscode, string newposcode, string types)
    {
        try
        {
            int result = -1;
            string strName = "sp_product_CheckQADExitByECN";
            SqlParameter[] parm = new SqlParameter[6];
            parm[0] = new SqlParameter("@prodCode", prodCode);
            parm[1] = new SqlParameter("@partCode", partCode);
            parm[2] = new SqlParameter("@newpartCode", newpartCode);
            parm[3] = new SqlParameter("@poscode", poscode);
            parm[4] = new SqlParameter("@newposcode", newposcode);
            parm[5] = new SqlParameter("@types", types);

            return result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return -2;
        }

    }

    public int CheckTOPExistsISOnGoing(string mstrId,string top)
    {
        try
        {
            int result = 0;
            string strName = "sp_product_CheckTOPExistsISOnGoing";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@mstrId", mstrId);
            parm[1] = new SqlParameter("@top", top);

            return result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return 0;
        }

    }


    public void updatePackingUpgradeApplyDetail(string mstrId, string productNumber, string productQad,  string childNumber, string childQad
                              , int childID, string pos, string qty, string itemNums, string itemType
                              , string itemStr, string reson,string lschild,string lschildqad)
    {

        string strName = "sp_PackingUpgrade_updateApplyDetail";
        SqlParameter[] parm = new SqlParameter[14];
        parm[0] = new SqlParameter("@mstrId", mstrId);
        parm[1] = new SqlParameter("@productNumber", productNumber);
        parm[2] = new SqlParameter("@productQad", productQad);
        parm[3] = new SqlParameter("@childNumber", childNumber);
        parm[4] = new SqlParameter("@childQad", childQad);
        parm[5] = new SqlParameter("@childID", childID);
        parm[6] = new SqlParameter("@posCode", pos);
        parm[7] = new SqlParameter("@numOfChild", qty);
        parm[8] = new SqlParameter("@itemNums", itemNums);
        parm[9] = new SqlParameter("@itemType", itemType);
        parm[10] = new SqlParameter("@itemStr", itemStr);
        parm[11] = new SqlParameter("@reson", reson);
        parm[12] = new SqlParameter("@oldchild", lschild);
        parm[13] = new SqlParameter("@oldchildqad", lschildqad);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public void delPackingUpgradeApplyDetail(string mstrId, string productNumber, string productQad, string childNumber, string childQad)
    {

        string strName = "sp_PackingUpgrade_delApplyDetail";
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@mstrId", mstrId);
        parm[1] = new SqlParameter("@productNumber", productNumber);
        parm[2] = new SqlParameter("@productQad", productQad);
        parm[3] = new SqlParameter("@childNumber", childNumber);
        parm[4] = new SqlParameter("@childQad", childQad);
    
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public  DataTable getproduct(string misid)
    {
        string sql = "select  ProdCode from  PackingUpgradeApply_Prodnew where  mstrid ='" + misid + "' union select ProdCode='' order by ProdCode";
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
        return dt;
    }


    public DataTable getnewpro(string misid,string oldpro)
    {
        string sql = "select  prodcodenew from  PackingUpgradeApply_Prodnew where  mstrid ='" + misid + "' and prodcode='" + oldpro + "'";
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
        return dt;
    }


    public void InsertPackingUpgradeApplyDetailbymanul(string mstrId, string productNumber, string productQad, string productNumbernew, string productQadnew, string childNumber, string childQad
                               , int productID, int childID,  string pos, string qty, string itemNums, string itemType
                               , string itemStr, string reson, string uID, string plantCode)
    {

        string strName = "sp_PackingUpgrade_InsertApplyDetailbymanual";
        SqlParameter[] parm = new SqlParameter[17];
        parm[0] = new SqlParameter("@mstrId", mstrId);
        parm[1] = new SqlParameter("@productNumber", productNumber);
        parm[2] = new SqlParameter("@productQad", productQad);
        parm[3] = new SqlParameter("@productNumbernew", productNumbernew);
        parm[4] = new SqlParameter("@productQadnew", productQadnew);
        parm[5] = new SqlParameter("@childNumber", childNumber);
        parm[6] = new SqlParameter("@childQad", childQad);
        parm[7] = new SqlParameter("@productID", productID);
        parm[8] = new SqlParameter("@childID", childID);  
        parm[9] = new SqlParameter("@posCode", pos);
        parm[10] = new SqlParameter("@numOfChild", qty);
        parm[11] = new SqlParameter("@itemNums", itemNums);
        parm[12] = new SqlParameter("@itemType", itemType);
        parm[13] = new SqlParameter("@itemStr", itemStr);
        parm[14] = new SqlParameter("@reson", reson);
        parm[15] = new SqlParameter("@uID", uID);
        parm[16] = new SqlParameter("@plantCode", plantCode);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public int checkPackingUpgradeApplyDetailbymanul(string mstrId, string productNumber, string productQad, string productNumbernew, string productQadnew)
    {
        string sql = "select cnt=count(*) from PackingUpgradeApply_Det where mstrId='" + mstrId + "' and  productNumber='" + productNumber + "' and productQad='" + productQad + "' and childNumber='" + productNumbernew.Replace("'", "''") + "' and  childQad='" + productQadnew.Replace("'", "''") + "'";
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
        if (dt != null)
            return int.Parse(dt.Rows[0][0].ToString());
        else
            return 0;
    }


    public int checkPackingUpgradeApplyDetailexist(string mstrId)
    {
        string sql = "select cnt=count(*) from PackingUpgradeApply_Det where mstrId='" + mstrId + "'";
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
        if (dt != null)
            return int.Parse(dt.Rows[0][0].ToString());
        else
            return 0;
    }



    public Tuple<int, string> FindProdIDqad(string qadCode)
    {
        Tuple<int, string> result = null;
        string strSQL = " Select top 1 id,code From Items Where item_qad =N'" + qadCode.Replace("'", "''") + "' And status<>2 And type=2 order by modifiedDate desc ";
        SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strSQL);
        if (reader.Read())
        {
            result = new Tuple<int, string>((int)reader["id"], reader["code"].ToString());

        }
        else
        {
            result = new Tuple<int, string>(0, "");
        }
        reader.Close();
        return result;
    }

    public Tuple<int, string> FindPartIDqad(string qadCode)
    {
        Tuple<int, string> result;
        string strSQL = " Select top 1 id,code From Items Where item_qad =N'" + qadCode.Replace("'", "''") + "' And type=0 order by modifiedDate desc";//"' And status<>2 And type=0 ";
        SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strSQL);
        if (reader.Read())
        {
            result = new Tuple<int, string>((int)reader["id"], reader["code"].ToString());
        }
        else
        {
            result = new Tuple<int, string>(0, "");
        }
        reader.Close();
        return result;
    }

    public Tuple<int, string> FindSemiProdqad(string qadCode)
    {
        Tuple<int, string> result = null;
        string strSQL = " Select top 1 id,code From Items Where item_qad =N'" + qadCode.Replace("'","''") + "' And status<>2 And type=1 order by modifiedDate desc";
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
    #endregion

    /// <summary>
    /// 发邮件给bom相关的关系人
    /// </summary>
    /// <returns></returns>
    public void SelectRelationEmail(string nbr, string prodCode)
    {
        string sqlstr = "sp_product_SelectRelationEmail";

        string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
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