using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entity;

namespace Data.Transaction
{
    public class TransactionData
    {
        #region Property
        private static int result = 0;
        private static string Message = "";
        private static ErrorObject erros;
        #endregion

        #region Select Data
        public class Select
        {
            /// <summary>
            /// Return Transaction By Specific ID
            /// </summary>
            /// <param name="id">Transaction ID</param>
            /// <returns>Transaction By Specific ID Or null If Exist Error</returns>
            public static Tuple<ErrorObject, tblTransaction> GetTransaction(int id)
            {
                tblTransaction data = new tblTransaction();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = db.tblTransaction.Find(id);
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblTransaction>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblTransaction>(erros, data);
                }
            }
        }
        #endregion

        #region Insert Data
        public class Insert
        {
            /// <summary>
            /// Insert TransactionType Information
            /// </summary>
            /// <param name="data">Transaction Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, int> Transaction(tblTransaction data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblTransaction.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblTransaction.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblTransaction.Add(data);
                        result = db.SaveChanges();
                        Message = "Affected Row: " + result.ToString();
                        if (result > 0)
                        {
                            return new Tuple<ErrorObject, int>(erros.IfError(false), data.id);
                        }
                        else
                        {
                            return new Tuple<ErrorObject, int>(erros.IfError(false), result);
                        }

                    }
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, int>(erros, 0);
                }

            }

        }
        #endregion

        #region Update Data
        public class Update
        {
            /// <summary>
            /// Update Transaction Information
            /// </summary>
            /// <param name="data">Transaction Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> Transaction(tblTransaction data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        db.Entry(data).State = EntityState.Modified;
                        result = db.SaveChanges();
                        Message = "Affected Row: " + result.ToString();

                        return new Tuple<ErrorObject, string>(erros.IfError(false), Message);
                    }
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, string>(erros, String.Empty);
                }
            }
        }
        #endregion

        #region Delete or Disable Data
        public class Delete
        {
            /// <summary>
            /// Update State Fields To Specific Employee ID
            /// </summary>
            /// <param name="TransactionID">Transaction ID</param>
            /// <param name="state">Active Or Disable</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> TransactionDisable(int TransactionID, string state)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblTransaction.Single(p => p.id == TransactionID);
                        row.state = state;
                        row.deleteDate = DateTime.Now;
                        result = db.SaveChanges();

                        Message = "Affected Row: " + result.ToString();
                        erros.Error = false;
                        return new Tuple<ErrorObject, string>(erros.IfError(false), Message);
                    }
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, string>(erros, String.Empty);
                }
            }

        }
        #endregion


    }
}
