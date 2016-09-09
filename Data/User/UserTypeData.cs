using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Entity;

namespace Data.User
{
    public class UserTypeData
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
            /// Return All UserType
            /// </summary>
            /// <returns>All User Type Or null If Exist Error</returns>
            public static Tuple<ErrorObject, List<tblUserType>> GetUserType()
            {
                List<tblUserType> userType = new List<tblUserType>();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        userType = db.tblUserType.ToList();
                    };

                    return new Tuple<ErrorObject, List<tblUserType>>(erros.IfError(false), userType);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblUserType>>(erros, userType);
                }

            }

            /// <summary>
            /// Return UserType By Specific ID
            /// </summary>
            /// <param name="id">UserType ID</param>
            /// <returns>UserType By Specific ID Or null If Exist Error</returns>
            public static Tuple<ErrorObject, tblUserType> GetUserType(int id)
            {
                tblUserType UserType = new tblUserType();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        UserType = db.tblUserType.Find(id);
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblUserType>(erros.IfError(false), UserType);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblUserType>(erros, UserType);
                }
            }

            /// <summary>
            /// Get User Type Name
            /// </summary>
            /// <param name="id">UserTypeID</param>
            /// <returns>User Type Name</returns>
            public static Tuple<ErrorObject, string> GetUserTypeName(int id)
            {
                string UserType = "";
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        UserType = db.tblUserType.Find(id).name;
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, string>(erros.IfError(false), UserType);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, string>(erros, UserType);
                }
            }         
        }
        #endregion

        #region Insert Data
        public class Insert
        {
            /// <summary>
            /// Insert UserType Information
            /// </summary>
            /// <param name="data">UserType Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> UserType(tblUserType data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblUserType.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblUserType.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblUserType.Add(data);
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
            /// Update User Type Information
            /// </summary>
            /// <param name="data">UserType Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> UserType(tblUserType data)
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
            /// Update State Fields To Specific UserType ID
            /// </summary>
            /// <param name="UserTypeID">UserTypeID</param>
            /// <param name="state">Active Or Disable</param>
            /// <returns>UserTypeID</returns>
            public static Tuple<ErrorObject, string> UserTypeDisable(int UserTypeID, string state)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblUserType.Single(p => p.id == UserTypeID);
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
