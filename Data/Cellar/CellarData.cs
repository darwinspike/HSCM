using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entity;

namespace Data.Cellar
{
    public class CellarData
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
            /// Return All Cellar
            /// </summary>
            /// <returns>All Cellar Or null If Exist Error</returns>
            public static Tuple<ErrorObject, List<tblCellar>> GetCellar()
            {
                List<tblCellar> data = new List<tblCellar>();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = db.tblCellar.ToList();
                    };
                    return new Tuple<ErrorObject, List<tblCellar>>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblCellar>>(erros, data);
                }
            }

            /// <summary>
            /// Return Cellar Information
            /// </summary>
            /// <param name="id">Cellar ID</param>
            /// <returns>Cellar Information</returns>
            public static Tuple<ErrorObject, tblCellar> GetCellar(int id)
            {
                tblCellar data = new tblCellar();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = db.tblCellar.Find(id);
                    };
                    return new Tuple<ErrorObject, tblCellar>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblCellar>(erros, data);
                }
            }


            /// <summary>
            /// Return All Cellar To Specific ID
            /// </summary>
            /// <param name="ProductID">ProductID</param>
            /// <returns>All Cellar</returns>
            public static Tuple<ErrorObject, List<tblCellar>> GetCellarByProductID(int ProductID)
            {
                List<tblCellar> data = new List<tblCellar>();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = db.tblCellar.Where(c => c.idProduct == ProductID).ToList();
                    };
                    return new Tuple<ErrorObject, List<tblCellar>>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblCellar>>(erros, data);
                }
            }

            /// <summary>
            /// Return Amount To ProductID and CellarID
            /// </summary>
            /// <param name="ProductID">Product ID</param>
            /// <param name="CellarArea">CellarAreaID</param>
            /// <returns>Amount</returns>
            public static Tuple<ErrorObject, int> GetCellarByProductIDAndCellarArea(int ProductID, int CellarAreaID)
            {
                int amount = 0;
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        amount = (int)db.tblCellar.Single(C => C.idProduct == ProductID && C.idcellarArea == CellarAreaID).amount;
                        return new Tuple<ErrorObject, int>(erros.IfError(false), amount);
                    }
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, int>(erros, 0);
                }
            }

            /// <summary>
            /// Return ID To Product ID and Cellar Area ID
            /// </summary>
            /// <param name="ProductID"></param>
            /// <param name="CellarArea"></param>
            /// <returns></returns>
            public static Tuple<ErrorObject, int> GetCellarIDByProductIDAndCellarArea(int ProductID, int CellarAreaID)
            {
                int id = 0;
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        id = (int)db.tblCellar.Single(C => C.idProduct == ProductID && C.idcellarArea == CellarAreaID).id;
                        return new Tuple<ErrorObject, int>(erros.IfError(false), id);
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

        #region Insert Data
        public class Insert
        {
            /// <summary>
            /// Insert Cellar Information
            /// </summary>
            /// <param name="data">Cellar Information</param>
            /// <returns>Cellar ID</returns>
            public static Tuple<ErrorObject, int> Cellar(tblCellar data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblCellar.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblCellar.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblCellar.Add(data);
                        result = db.SaveChanges();
                        Message = "Affected Row: " + result.ToString();
                        return new Tuple<ErrorObject, int>(erros.IfError(false), data.id);
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
            /// Update Cellar Information
            /// </summary>
            /// <param name="data">Cellar Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> Cellar(tblCellar data)
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

            /// <summary>
            /// Update Cellar Amount TO Specific Cellar ID
            /// </summary>
            /// <param name="CellarID">Cellar ID</param>
            /// <param name="CellarAmount">Cellar Amount</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> CellarAmoun(int CellarID, int CellarAmount)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblCellar.Single(p => p.id == CellarID);
                        row.amount = CellarAmount;
                        row.upDateDate = DateTime.Now;
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
            /// <param name="CellarID">Cellar ID</param>
            /// <param name="state">Active Or Disable</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> CellarDisable(int CellarID, string state)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblEmployee.Single(p => p.id == CellarID);
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
