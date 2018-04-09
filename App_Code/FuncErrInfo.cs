using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace QCProgress
{
    /// <summary>
    ///枚举类型 函数的集中错误 
    /// </summary>
    public enum FuncErrType
    {
        操作成功,
        数据库操作失败,
        添加的记录已经存在,
        标准维护不完整,请先维护,
        供应商不存在,
        报检员不存在,
        检验员不存在,
        多次添加后的本次数量之和应该小于加工单数量,
        多次添加后的本次投入数之和应该小于总批量,
        客户不存在,
        所添加项目的样本量区间应位于该项目已有样本量区间之外,
        所添加项目已经是国标不允许重复添加,
        所添加的项目将不按国标进行检验请确定样本量区间,
        你没有权限删除这条记录,
        QAD号不一致,
        没有满足条件的加工单,
        线长不存在
    }

    /// <summary>
    /// 主要用于记录函数发生错误时，返回的错误类型
    /// </summary>
    public class FuncErrCollection
    {
        public FuncErrCollection()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 将存储过程操作返回的结果转换成枚举
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public FuncErrType GetProcedureErrInfo(int value) 
        {
            FuncErrType returnVal = FuncErrType.操作成功;
            switch (value) 
            {
                case 0: returnVal = FuncErrType.操作成功; break;
                case 1: returnVal = FuncErrType.添加的记录已经存在; break;
                case 2: returnVal = FuncErrType.标准维护不完整; break;
                case 3: returnVal = FuncErrType.供应商不存在; break;
                case 4: returnVal = FuncErrType.报检员不存在; break;
                case 5: returnVal = FuncErrType.检验员不存在; break;
                case 6: returnVal = FuncErrType.多次添加后的本次数量之和应该小于加工单数量; break;
                case 7: returnVal = FuncErrType.多次添加后的本次投入数之和应该小于总批量; break;
                case 8: returnVal = FuncErrType.客户不存在; break;
                case 9: returnVal = FuncErrType.所添加项目的样本量区间应位于该项目已有样本量区间之外; break;
                case 10: returnVal = FuncErrType.所添加项目已经是国标不允许重复添加; break;
                case 11: returnVal = FuncErrType.所添加的项目将不按国标进行检验请确定样本量区间; break;
                case 12: returnVal = FuncErrType.你没有权限删除这条记录; break;
                case 13: returnVal = FuncErrType.QAD号不一致; break;
                case 14: returnVal = FuncErrType.没有满足条件的加工单; break;
                case 15: returnVal = FuncErrType.线长不存在; break;
                case -1: returnVal = FuncErrType.数据库操作失败; break;
            }
            return returnVal;
        }
    }
}
