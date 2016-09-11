using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entity;

namespace Data.Transaction
{
    public class TransactionTypeData
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
            /// Return All TransactionType
            /// </summary>
            /// <returns>All TransactionType Or null If Exist Error</returns>
            public static Tuple<ErrorObject, List<tblTransactionType>> GetTransactionTypeList()
            {
                List<tblTransactionType> data = new List<tblTransactionType>();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = db.tblTransactionType.ToList();
                    };
                    return new Tuple<ErrorObject, List<tblTransactionType>>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblTransactionType>>(erros, data);
                }

            }

            /// <summary>
            /// Return TransactionType By Specific ID
            /// </summary>
            /// <param name="id">TransactionType ID</param>
            /// <returns>TransactionType By Specific ID Or null If Exist Error</returns>
            public static Tuple<ErrorObject, tblTransactionType> GetTransactionType(int id)
            {
                tblTransactionType data = new tblTransactionType();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = db.tblTransactionType.Find(id);
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblTransactionType>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblTransactionType>(erros, data);
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
            /// <param name="data">TransactionType Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> TransactionType(tblTransactionType data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblTransactionType.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblTransactionType.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblTransactionType.Add(data);
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

        #region Update Data
        public class Update
        {
            /// <summary>
            /// Update Employee Information
            /// </summary>
            /// <param name="data">Employee Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> TransactionType(tblTransactionType data)
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
            /// <param name="TransactionTypeID">Employee ID</param>
            /// <param name="state">Active Or Disable</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> TransactionTypeDisable(int TransactionTypeID, string state)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblTransactionType.Single(p => p.id == TransactionTypeID);
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
