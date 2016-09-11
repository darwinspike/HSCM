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

    public class Users
    {
        public int id { get; set; }
        public string user { get; set; }

        [Required(ErrorMessage = "Pass word is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Pass word is required")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }

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
            public Users User { get; set; }
            public List<Users> UserList { get; set; }
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
                response.UserList = new List<Users>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var User = UserData.Select.GetUserList();
                    if (!User.Item1.Error)
                    {
                        foreach (var item in User.Item2)
                        {
                            response.UserList.Add(new Users()
                            {
                                id = item.id,
                                user = item.user,
                                password = null,
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
                response.User = new Users();
                try
                {
                    var User = UserData.Select.GetUser(request.UserID);
                    if (!User.Item1.Error)
                    {
                        response.User = new Users()
                        {
                            id = User.Item2.id,
                            user = User.Item2.user,
                            password = null,
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

            /// <summary>
            /// Get User Name To Specific UserID
            /// </summary>
            /// <param name="request">UserID</param>
            /// <returns>User Name To Specific UserID</returns>
            public static GetUserResponse GetUserName(int request) {
                GetUserResponse response = new GetUserResponse();
                response.Error = new Handler.ErrorObject();
                response.User = new Users();
                try
                {
                    var User = UserData.Select.GetUserName(request);
                    if (!User.Item1.Error)
                    {
                        response.User = new Users()
                        {
                            user = User.Item2
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


            /// <summary>
            /// Return User List
            /// </summary>
            /// <returns>User List</returns>
            public static GetUserResponse GetUserButNotConfigurationList()
            {
                GetUserResponse response = new GetUserResponse();
                response.UserList = new List<Users>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var User = UserData.Select.GetUserButNotConfigurationList();
                    if (!User.Item1.Error)
                    {
                        foreach (var item in User.Item2)
                        {
                            response.UserList.Add(new Users()
                            {
                                id = item.id,
                                user = item.user,
                                password = null,
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



            public static GetUserResponse GetUserButConfigurationList()
            {
                GetUserResponse response = new GetUserResponse();
                response.UserList = new List<Users>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var User = UserData.Select.GetUserButConfigurationList();
                    if (!User.Item1.Error)
                    {
                        int id = 0;
                        for (int i = 0; User.Item2.Count > i; i++) {
                            if (id != User.Item2[i].id)
                            {
                                response.UserList.Add(new Users()
                                {
                                    id = User.Item2[i].id,
                                    user = User.Item2[i].user,
                                    state = User.Item2[i].state
                                });
                            }
                            id = User.Item2[i].id;
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
                    tblUser user = new tblUser()
                    {
                        id = request.User.id,
                        user = request.User.user,
                        password = request.User.password,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = UserData.Insert.Users(user);
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
            public static GetUserResponse User(GetUserResponse request)
            {

                GetUserResponse response = new GetUserResponse();
                try
                {
                    tblUser user = new tblUser()
                    {
                        id = request.User.id,
                        user = request.User.user,
                        password = request.User.password,
                        createDate = request.User.createDate,
                        upDateDate = DateTime.Now,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = UserData.Update.Users(user);
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
            public static GetUserResponse UserDisable(int UserID, string state)
            {
                GetUserResponse response = new GetUserResponse();
                try
                {
                    var result = UserData.Delete.UserDisable(UserID, state);
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
