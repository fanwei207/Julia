using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;

namespace admin
{
    /// <summary>
    /// Summary description for DeptProductionLine
    /// </summary>
    public class DeptProductionLine
    {
        private adamClass adam = new adamClass();
        public DeptProductionLine()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetDeptProductionLines(string deptId, string productionLine, string lineLeader, string processMonitor)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@deptId", deptId);
            param[1] = new SqlParameter("@line", productionLine);
            param[2] = new SqlParameter("@LineLeader", lineLeader);
            param[3] = new SqlParameter("@ProcessMonitor", processMonitor);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_dept_selectDeptProductionLine", param).Tables[0];
        }

        public DataTable GetDepartments()
        {
            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_selectDepartment").Tables[0];
        }
        public DataTable GetDeptProductionLines(string deptId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Departmentid", deptId);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_selectLine", param).Tables[0];
        }
        public DataTable GetDirectors(string deptId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@deptId", deptId);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_dept_selectDirector", param).Tables[0];
        }

        public DataTable GetViceDirectors(string deptId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@deptId", deptId);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_dept_selectViceDirector", param).Tables[0];
        }

        public DataTable GetUser(int plantid, int departmentId, string userName)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@plantid", plantid);
            param[1] = new SqlParameter("@departmentId", departmentId);
            param[2] = new SqlParameter("@userName", userName);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_dept_selectUser", param).Tables[0];
        }

        public bool InsertDeptProductionLine(string deptId, string productionLine, string lineLeader, string processMonitor, string createdBy, string workshopID)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@deptId", deptId);
            param[1] = new SqlParameter("@line", productionLine);
            param[2] = new SqlParameter("@LineLeaderId", lineLeader);
            param[3] = new SqlParameter("@ProcessMonitorId", processMonitor);
            param[4] = new SqlParameter("@createdBy", createdBy);
            param[5] = new SqlParameter("@workshopID", workshopID);
            if (SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_dept_InsertDeptProductionLine", param) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateDeptProductionLine(string id,string deptId, string productionLine, string lineLeader, string processMonitor, string modifiedBy)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@deptId", deptId);
            param[2] = new SqlParameter("@line", productionLine);
            param[3] = new SqlParameter("@LineLeaderId", lineLeader);
            param[4] = new SqlParameter("@ProcessMonitorId", processMonitor);
            param[5] = new SqlParameter("@modifiedBy", modifiedBy);

            if (SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_dept_UpdateDeptProductionLine", param) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteDeptProductionLine(string id)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", id);

            if (SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_dept_deleteDeptProductionLine", param) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}