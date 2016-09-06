using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Entity;

namespace Data
{
    public class ProviderData
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
            /// Return All Provider
            /// </summary>
            /// <returns>All Provider Or null If Exist Error</returns>
            public static Tuple<ErrorObject, List<tblProvider>> GetProviderList()
            {
                List<tblProvider> Provider = new List<tblProvider>();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        Provider = db.tblProvider.ToList();
                    };

                    return new Tuple<ErrorObject, List<tblProvider>>(erros.IfError(false), Provider);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblProvider>>(erros, Provider);
                }

            }

            /// <summary>
            /// Return Provider By Specific ID
            /// </summary>
            /// <param name="id">Provider ID</param>
            /// <returns>Provider By Specific ID Or null If Exist Error</returns>
            public static Tuple<ErrorObject, tblProvider> GetProvider(int id)
            {
                tblProvider Provider = new tblProvider();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        Provider = db.tblProvider.Find(id);
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblProvider>(erros.IfError(false), Provider);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblProvider>(erros, Provider);
                }
            }
        }
        #endregion

        #region Insert Data
        public class Insert
        {
            /// <summary>
            /// Insert Provider Information
            /// </summary>
            /// <param name="data">Provider Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> Provider(tblProvider data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblProvider.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblProvider.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblProvider.Add(data);
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
            /// Update Provider Information
            /// </summary>
            /// <param name="data">Provider Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> Provider(tblProvider data)
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
            /// Update State Fields To Specific Provider ID
            /// </summary>
            /// <param name="ProviderID">Provider ID</param>
            /// <param name="state">Active Or Disable</param>
            /// <returns>ProviderID</returns>
            public static Tuple<ErrorObject, string> ProviderDisable(int ProviderID, string state)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblProvider.Single(p => p.id == ProviderID);
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
