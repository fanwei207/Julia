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
    /// Summary description for DeptDirector
    /// </summary>
    public class DeptDirector
    {
        private adamClass adam = new adamClass();
        public DeptDirector()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetDeptDirectors(string deptId, string director, string viceDirector)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@deptId", deptId);
            param[1] = new SqlParameter("@DirectorName", director);
            param[2] = new SqlParameter("@ViceDirectorName", viceDirector);


            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_dept_selectDeptDirector", param).Tables[0];
        }

        public DataTable GetDepartments()
        {
            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_selectDepartment").Tables[0];
        }

        public DataTable GetUser(int plantid, int departmentId, string userName)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@plantid", plantid);
            param[1] = new SqlParameter("@departmentId", departmentId);
            param[2] = new SqlParameter("@userName", userName);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_dept_selectUser", param).Tables[0];
        }

        public bool CheckDepartmentExists(string deptId)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@deptId", deptId);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, "sp_dept_checkDeptDirectorExists", param));
        }

        public bool InsertDeptDirectors(string deptId, string directorId,  string viceDirectorId,  string createdBy)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@deptId", deptId);
            param[1] = new SqlParameter("@DirectorId", directorId);
            param[2] = new SqlParameter("@ViceDirectorId", viceDirectorId);
            param[3] = new SqlParameter("@createdBy", createdBy);

            if (SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_dept_insertDeptDirector", param) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateDeptDirectors(string id, string directorId,  string viceDirectorId, string modifiedBy)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@DirectorId", directorId);
            param[2] = new SqlParameter("@ViceDirectorId", viceDirectorId);
            param[3] = new SqlParameter("@modifiedBy", modifiedBy);

            if (SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_dept_updateDeptDirector", param) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteDeptDirectors(string id)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", id);

            if (SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_dept_deleteDeptDirector", param) > 0)
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