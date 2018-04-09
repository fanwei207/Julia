using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

namespace QCProgress
{
    /// <summary>
    /// Summary description for QCTracking
    /// </summary>
    public class QCTracking
    {
        adamClass adam = new adamClass();

        public DataTable SelectWoAndPoFromSo(string soNbr, string part,string woNbr1,string woNbr2,string orderDate1,string orderDate2)
        {
            string strName = "sp_qc_selectWoAndPoFromSo";
            SqlParameter[] parm = new SqlParameter[6];
            parm[0] = new SqlParameter("@soNbr", soNbr);
            parm[1] = new SqlParameter("@part", part);
            parm[2] = new SqlParameter("@woNbr1", woNbr1); 
            parm[3] = new SqlParameter("@woNbr2", woNbr2);
            parm[4] = new SqlParameter("@orderDate1", orderDate1);
            parm[5] = new SqlParameter("@orderDate2", orderDate2);
            DataSet _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            return _dataset.Tables[0];
        }

        public DataTable SelectProcessDefectItem(string woLot)
        {
            string strName = "sp_qc_selectProcessDefectItem";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@woLot", woLot);
            DataSet _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            return _dataset.Tables[0];
        }

        public DataTable SelectProcessDefectItemNew(string woLot)
        {
            string strName = "sp_qc_selectProcessDefectItem_New";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@woLot", woLot);
            DataSet _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            return _dataset.Tables[0];
        }
    }
}