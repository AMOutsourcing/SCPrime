﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using nsBaseClass;

namespace SCPrime.Model
{
    public class SCContractRemark
    {
        protected static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int OID { get; set; }
        public int ContractOID { get; set; }
        public DateTime Created { get; set; }
        public int UserId { get; set; }
        public int RemarkType { get; set; }
        public String Info { get; set; }
        public bool isMarkDeleted { get; set; } = false;

        public static List<SCContractRemark> getRemark(int ContractOID)
        {
            clsSqlFactory hSql = new clsSqlFactory();
            List<SCContractRemark> Result = new List<SCContractRemark>();
            try
            {
                clsGlobalVariable objGlobal = new clsGlobalVariable();
                String strSql = "select a.OID as OID, a.ContractOID as ContractOID , a.Created as Created,a.UserId as UserId,a.RemarkType as RemarkType,a.Info as Info " +
                    "FROM ZSC_ContractRemark a " +
                    "WHERE a.ContractOID = ? ORDER BY a.Created DESC ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("ContractOID", ContractOID);
                hSql.ExecuteReader();
                int colId;
                while (hSql.Read())
                {
                    SCContractRemark item = new SCContractRemark();
                    colId = hSql.Reader.GetOrdinal("OID");
                    if (!hSql.Reader.IsDBNull(colId)) item.OID = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("ContractOID");
                    if (!hSql.Reader.IsDBNull(colId)) item.ContractOID = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("Created");
                    if (!hSql.Reader.IsDBNull(colId)) item.Created = hSql.Reader.GetDateTime(colId);
                    colId = hSql.Reader.GetOrdinal("UserId");
                    if (!hSql.Reader.IsDBNull(colId)) item.UserId = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("RemarkType");
                    if (!hSql.Reader.IsDBNull(colId)) item.RemarkType = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("Info");
                    if (!hSql.Reader.IsDBNull(colId)) item.Info = hSql.Reader.GetString(colId);
                    Result.Add(item);
                }
            }
            catch (Exception ex)
            {
                _log.Error("ERROR getRemark " + ContractOID + ": ", ex);
                throw ex;
            }
            finally
            {
                hSql.Close();
            }
            return Result;
        }

        public static bool saveRemark(int ContractOID, List<SCContractRemark> lstData, clsSqlFactory hSql)
        {
            if (lstData == null)
                return true;
            bool bRet = true;
            try
            {
                foreach (SCContractRemark data in lstData)
                {
                    if (data.OID > 0)
                    {
                        if (data.isMarkDeleted == true)
                        {
                            bRet = hSql.NewCommand("delete from ZSC_ContractRemark where OID=?");
                            hSql.Com.Parameters.AddWithValue("OID", data.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                        }
                        else
                        {
                            bRet = hSql.NewCommand("update ZSC_ContractRemark set UserId=?,RemarkType=?,Info=? where OID=?");
                            hSql.Com.Parameters.AddWithValue("UserId", data.UserId);
                            hSql.Com.Parameters.AddWithValue("RemarkType", data.RemarkType);
                            hSql.Com.Parameters.AddWithValue("Info", data.Info);
                            hSql.Com.Parameters.AddWithValue("OID", data.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        bRet = hSql.NewCommand("INSERT INTO ZSC_ContractRemark(ContractOID,Created,UserId,RemarkType,Info) VALUES(?,getdate(),?,?,?)");
                        hSql.Com.Parameters.AddWithValue("ContractOID", data.ContractOID);
                        hSql.Com.Parameters.AddWithValue("UserId", data.UserId);
                        hSql.Com.Parameters.AddWithValue("RemarkType", data.RemarkType);
                        hSql.Com.Parameters.AddWithValue("Info", data.Info);
                        bRet = bRet && hSql.ExecuteNonQuery();
                    }
                }
                hSql.Commit();
            }
            catch (Exception ex)
            {
                _log.Error("ERROR saveRemark " + ContractOID + ": ", ex);
                hSql.Rollback();
                throw ex;
            }
            return bRet;
        }
    }
}
