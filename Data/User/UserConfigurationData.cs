using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data.Entity;

namespace Data.User
{
    public class UserConfigurationData
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
            /// Return All UserConfiguration
            /// </summary>
            /// <returns>All UserConfiguration Or null If Exist Error</returns>
            public static Tuple<ErrorObject, List<tblUserConfiguration>> GetUserConfigurationList()
            {
                List<tblUserConfiguration> data = new List<tblUserConfiguration>();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = db.tblUserConfiguration.ToList();
                    };
                    return new Tuple<ErrorObject, List<tblUserConfiguration>>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblUserConfiguration>>(erros, data);
                }

            }

            /// <summary>
            /// Return UserConfiguration By Specific ID
            /// </summary>
            /// <param name="id">UserConfiguration ID</param>
            /// <returns>UserConfiguration By Specific ID Or null If Exist Error</returns>
            public static Tuple<ErrorObject, tblUserConfiguration> GetUserConfiguration(int id)
            {
                tblUserConfiguration data = new tblUserConfiguration();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = db.tblUserConfiguration.Find(id);
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblUserConfiguration>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblUserConfiguration>(erros, data);
                }
            }


            /// <summary>
            /// Return All UserConfiguration To Specific User ID
            /// </summary>
            /// <param name="UserID">UserID</param>
            /// <returns>All UserConfiguration To Specific User ID</returns>
            public static Tuple<ErrorObject, List<tblUserConfiguration>> GetUserConfigurationListByUserID(int UserID)
            {
                List<tblUserConfiguration> data = new List<tblUserConfiguration>();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = db.tblUserConfiguration.Where(u => u.idUser == UserID).ToList();
                    };
                    return new Tuple<ErrorObject, List<tblUserConfiguration>>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblUserConfiguration>>(erros, data);
                }

            }

            
        }
        #endregion

        #region Insert Data
        public class Insert
        {
            /// <summary>
            /// Insert UserConfiguration Information
            /// </summary>
            /// <param name="data">UserConfiguration Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> UserConfiguration(tblUserConfiguration data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblUserConfiguration.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblUserConfiguration.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblUserConfiguration.Add(data);
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
            /// Update UserConfiguration Information
            /// </summary>
            /// <param name="data">UserConfiguration Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> UserConfiguration(tblUserConfiguration data)
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
            /// <param name="UserConfigurationID">UserConfiguration ID</param>
            /// <param name="state">Active Or Disable</param>
            /// <returns>UserConfigurationID</returns>
            public static Tuple<ErrorObject, string> UserConfigurationDisable(int UserConfigurationID, string state)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblUserConfiguration.Single(p => p.id == UserConfigurationID);
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
