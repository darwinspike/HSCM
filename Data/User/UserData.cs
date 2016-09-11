using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entity;
using System.Data.Entity;

namespace Data.User
{
    public class UserData
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
            /// Return All Users
            /// </summary>
            /// <returns>All User Or null If Exist Error</returns>
            public static Tuple<ErrorObject, List<tblUser>> GetUserList()
            {
                List<tblUser> Users = new List<tblUser>();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        Users = db.tblUser.ToList();
                    };

                    return new Tuple<ErrorObject, List<tblUser>>(erros.IfError(false), Users);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblUser>>(erros, Users);
                }

            }


            
            public static Tuple<ErrorObject, List<tblUser>> GetUserButNotConfigurationList()
            {
                List<tblUser> Users = new List<tblUser>();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        Users = ( from U in db.tblUser
                                  join UC in db.tblUserConfiguration 
                                  on U.id equals UC.idUser into gj
                                  from newUser in gj.DefaultIfEmpty()
                                  where newUser == null
                                  select U 
                                  ).ToList();
                    };

                    return new Tuple<ErrorObject, List<tblUser>>(erros.IfError(false), Users);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblUser>>(erros, Users);
                }

            }

            public static Tuple<ErrorObject, List<tblUser>> GetUserButConfigurationList()
            {
                List<tblUser> Users = new List<tblUser>();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        Users = (from U in db.tblUser
                                 join UC in db.tblUserConfiguration
                                 on U.id equals UC.idUser 
                                 select U
                                  ).ToList();
                    };

                    return new Tuple<ErrorObject, List<tblUser>>(erros.IfError(false), Users);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblUser>>(erros, Users);
                }

            }


            /// <summary>
            /// Return Users By Specific ID
            /// </summary>
            /// <param name="id">User ID</param>
            /// <returns>User By Specific ID Or null If Exist Error</returns>
            public static Tuple<ErrorObject, tblUser> GetUser(int id)
            {
                tblUser Users = new tblUser();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        Users = db.tblUser.Find(id);
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblUser>(erros.IfError(false), Users);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblUser>(erros, Users);
                }
            }

            /// <summary>
            /// Get User Name To Specific UserID
            /// </summary>
            /// <param name="id">UserID</param>
            /// <returns>User Name To Specific UserID</returns>
            public static Tuple<ErrorObject, string> GetUserName(int id)
            {
                string Users = "";
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        Users = db.tblUser.Find(id).user;
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, string>(erros.IfError(false), Users);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, string>(erros, Users);
                }
            }
        }
        #endregion

        #region Insert Data
        public class Insert
        {
            /// <summary>
            /// Insert Users Information
            /// </summary>
            /// <param name="data">Users Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> Users(tblUser data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblUser.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblUser.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblUser.Add(data);
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
            /// Update Users Information
            /// </summary>
            /// <param name="data">Users Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> Users(tblUser data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblUser.Single(p => p.id == data.id);
                        if (!String.IsNullOrEmpty(data.password)) {
                            row.password = data.password;
                        }
                        row.user = data.user;
                        row.upDateDate = DateTime.Now;
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

        #region Delete or Disable Data
        public class Delete
        {
            /// <summary>
            /// Update State Fields To Specific UserID
            /// </summary>
            /// <param name="UserID">User ID</param>
            /// <param name="state">Active Or Disable</param>
            /// <returns></returns>
            public static Tuple<ErrorObject, string> UserDisable(int UserID, string state)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblUser.Single(p => p.id == UserID);
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
