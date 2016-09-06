using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data.User;
using Entity;
using System.ComponentModel.DataAnnotations;
using Bussines.Handler;

namespace Bussines.User
{

    public class User
    {
        public int id { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> upDateDate { get; set; }
        public Nullable<System.DateTime> deleteDate { get; set; }
        public string state { get; set; }
    }


    public class UserBussines
    {

        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetUserResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public User User { get; set; }
            public List<User> UserList { get; set; }
        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetUserRequest
        {
            public int UserID { get; set; }
        }

        #endregion

        #region Select Data
        public class Select
        {

            /// <summary>
            /// Return User List
            /// </summary>
            /// <returns>User List</returns>
            public static GetUserResponse GetUserList()
            {
                GetUserResponse response = new GetUserResponse();
                response.UserList = new List<User>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var User = UserData.Select.GetUserList();
                    if (!User.Item1.Error)
                    {
                        foreach (var item in User.Item2)
                        {
                            response.UserList.Add(new User()
                            {
                                id = item.id,
                                user = item.user,
                                password = item.password,
                                createDate = item.createDate,
                                upDateDate = item.upDateDate,
                                deleteDate = item.deleteDate,
                                state = item.state
                            });
                        }
                    }
                    else
                    {
                        response.Error.InfoError(User.Item1);
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }
                return response;
            }

            /// <summary>
            /// Return Users Information
            /// </summary>
            /// <param name="request">User ID</param>
            /// <returns>User Information</returns>
            public static GetUserResponse GetUser(GetUserRequest request)
            {
                GetUserResponse response = new GetUserResponse();
                response.Error = new Handler.ErrorObject();
                response.User = new User();
                try
                {
                    var User = UserData.Select.GetUser(request.UserID);
                    if (!User.Item1.Error)
                    {
                        response.User = new User()
                        {
                            id = User.Item2.id,
                            user = User.Item2.user,
                            password = User.Item2.password,
                            createDate = User.Item2.createDate,
                            upDateDate = User.Item2.upDateDate,
                            deleteDate = User.Item2.deleteDate,
                            state = User.Item2.state
                        };

                    }
                    else
                    {
                        response.Error.InfoError(User.Item1);
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }

                return response;
            }
        }
        #endregion

        #region Insert Data
        public class Insert
        {

            /// <summary>
            /// Return Affected Row Or Error If Exist
            /// </summary>
            /// <param name="request">User Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetUserResponse User(GetUserResponse request)
            {
                GetUserResponse response = new GetUserResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblUser CellarArea = new tblUser()
                    {
                        id = request.User.id,
                        user = request.User.user,
                        password = request.User.password,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = CellarAreaData.Insert.CellarArea(CellarArea);
                    if (result.Item1.Error)
                    {
                        response.Error.InfoError(result.Item1);
                    }
                    else
                    {
                        response.Message = result.Item2;
                    }

                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }

                return response;
            }


        }
        #endregion

        #region Update Data
        public class Update
        {

            /// <summary>
            /// Return Affected Row Or Error If Exist
            /// </summary>
            /// <param name="request">User Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetCellarAreaResponse CellarArea(GetCellarAreaResponse request)
            {

                GetCellarAreaResponse response = new GetCellarAreaResponse();
                try
                {
                    tblCellarArea CellarArea = new tblCellarArea()
                    {
                        id = request.CellarArea.id,
                        name = request.CellarArea.name,
                        detail = request.CellarArea.detail,
                        createDate = request.CellarArea.createDate,
                        upDateDate = DateTime.Now,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = CellarAreaData.Update.CellarArea(CellarArea);
                    if (result.Item1.Error)
                    {
                        response.Error.InfoError(result.Item1);
                    }
                    else
                    {
                        response.Message = result.Item2;
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }

                return response;


            }

        }
        #endregion

        #region Delete or Disable Data
        public class Delete
        {

            /// <summary>
            /// Return Affected Row Or Error If Exist
            /// </summary>
            /// <param name="UserID">User ID</param>
            /// <param name="state">State</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetCellarAreaResponse UserDisable(int UserID, string state)
            {
                GetCellarAreaResponse response = new GetCellarAreaResponse();
                try
                {
                    var result = CellarAreaData.Delete.CellarAreaDisable(UserID, state);
                    if (result.Item1.Error)
                    {
                        response.Error.InfoError(result.Item1);
                    }
                    else
                    {
                        response.Message = result.Item2;
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }
                return response;
            }
        }
        #endregion

    }
}
