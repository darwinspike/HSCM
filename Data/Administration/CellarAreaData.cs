using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Entity;

namespace Data
{
    public class CellarAreaData
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
            /// Return All Cellar Area
            /// </summary>
            /// <returns>All Cellar Area Or null If Exist Error</returns>
            public static Tuple<ErrorObject, List<tblCellarArea>> GetCellarArea()
            {
                List<tblCellarArea> CellarArea = new List<tblCellarArea>();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        CellarArea = db.tblCellarArea.ToList();
                    };

                    return new Tuple<ErrorObject, List<tblCellarArea>>(erros.IfError(false), CellarArea);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblCellarArea>>(erros, CellarArea);
                }

            }

            /// <summary>
            /// Return Cellar Area By Specific ID
            /// </summary>
            /// <param name="id">Cellar Area ID</param>
            /// <returns>Cellar Area By Specific ID Or null If Exist Error</returns>
            public static Tuple<ErrorObject, tblCellarArea> GetCellarArea(int id)
            {
                tblCellarArea CellarArea = new tblCellarArea();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        CellarArea = db.tblCellarArea.Find(id);
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblCellarArea>(erros.IfError(false), CellarArea);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblCellarArea>(erros, CellarArea);
                }
            }
        }
        #endregion

        #region Insert Data
        public class Insert
        {
            /// <summary>
            /// Insert Cellar Area Information
            /// </summary>
            /// <param name="data">Cellar Area Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> CellarArea(tblCellarArea data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblCellarArea.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblCellarArea.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblCellarArea.Add(data);
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
            /// Update Cellar Area Information
            /// </summary>
            /// <param name="data">Cellar Area Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> CellarArea(tblCellarArea data)
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
            /// Update State Fields To Specific CellarAreaID ID
            /// </summary>
            /// <param name="CellarAreaID">Cellar Area ID</param>
            /// <param name="state">Active Or Disable</param>
            /// <returns>ID Department ID</returns>
            public static Tuple<ErrorObject, string> CellarAreaDisable(int CellarAreaID, string state)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblCellarArea.Single(p => p.id == CellarAreaID);
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
