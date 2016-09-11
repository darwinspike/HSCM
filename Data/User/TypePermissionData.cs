using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Entity;
using System.Data.Entity;

namespace Data.User
{
    public class TypePermissionData
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
            /// Return All TypePermission
            /// </summary>
            /// <returns>All TypePermission Or null If Exist Error</returns>
            public static Tuple<ErrorObject, List<tblTypePermission>> GetTypePermissionList()
            {
                List<tblTypePermission> TypePermission = new List<tblTypePermission>();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        TypePermission = db.tblTypePermission.ToList();
                    };

                    return new Tuple<ErrorObject, List<tblTypePermission>>(erros.IfError(false), TypePermission);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblTypePermission>>(erros, TypePermission);
                }

            }

            /// <summary>
            /// Return Type Permission By Specific ID
            /// </summary>
            /// <param name="id">Type PermissionID</param>
            /// <returns>Type Permission By Specific ID Or null If Exist Error</returns>
            public static Tuple<ErrorObject, tblTypePermission> GetTypePermission(int id)
            {
                tblTypePermission TypePermission = new tblTypePermission();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        TypePermission = db.tblTypePermission.Find(id);
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblTypePermission>(erros.IfError(false), TypePermission);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblTypePermission>(erros, TypePermission);
                }
            }

            /// <summary>
            /// Get Type Permission Name By Specific ID
            /// </summary>
            /// <param name="id">TypePermissionID</param>
            /// <returns>Type Permission Name By Specific ID</returns>
            public static Tuple<ErrorObject, tblTypePermission> GetTypePermissionName(int id)
            {
                tblTypePermission TypePermission = new tblTypePermission();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        TypePermission.name = db.tblTypePermission.Find(id).name;
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblTypePermission>(erros.IfError(false), TypePermission);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblTypePermission>(erros, TypePermission);
                }
            }
            
        }
        #endregion

        #region Insert Data
        public class Insert{}
        #endregion

        #region Update Data
        public class Update
        {
            /// <summary>
            /// Update Type Permission Information
            /// </summary>
            /// <param name="data">Type Permission Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> TypePermission(tblTypePermission data)
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
            /// <param name="TypePermissionID">TypePermissionID</param>
            /// <param name="state">Active Or Disable</param>
            /// <returns>TypePermissionID</returns>
            public static Tuple<ErrorObject, string> TypePermissionDisable(int TypePermissionID, string state)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblTypePermission.Single(p => p.id == TypePermissionID);
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
