using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data.User;
using Entity;
using System.ComponentModel.DataAnnotations;

namespace Bussines.User
{
    /// <summary>
    /// Model Site
    /// </summary>
    public class UserType
    {
        public int id { get; set; }
        public string name { get; set; }

        [DataType(DataType.MultilineText)]
        public string detail { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> upDateDate { get; set; }
        public Nullable<System.DateTime> deleteDate { get; set; }
        public string state { get; set; }
    }

    public class UserTypeBussines
    {

        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetUserTypeResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public UserType UserType { get; set; }
            public List<UserType> UserTypeList { get; set; }
        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetUserTypeRequest
        {
            public int UserTypeID { get; set; }
        }

        #endregion


        #region Select Data
        public class Select
        {

            /// <summary>
            /// Return User Type
            /// </summary>
            /// <returns>User Type List</returns>
            public static GetUserTypeResponse GetUserTypeList()
            {
                GetUserTypeResponse response = new GetUserTypeResponse();
                response.UserTypeList = new List<UserType>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var UserType = UserTypeData.Select.GetUserType();
                    if (!UserType.Item1.Error)
                    {
                        foreach (var item in UserType.Item2)
                        {
                            response.UserTypeList.Add(new UserType()
                            {
                                id = item.id,
                                name = item.name,
                                detail = item.detail,
                                createDate = item.createDate,
                                upDateDate = item.upDateDate,
                                deleteDate = item.deleteDate,
                                state = item.state
                            });
                        }
                    }
                    else
                    {
                        response.Error.InfoError(UserType.Item1);
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }
                return response;
            }

            /// <summary>
            /// Return User Type Information
            /// </summary>
            /// <param name="request">User Type ID</param>
            /// <returns>UserType Information</returns>
            public static GetUserTypeResponse GetUserType(GetUserTypeRequest request)
            {
                GetUserTypeResponse response = new GetUserTypeResponse();
                response.Error = new Handler.ErrorObject();
                response.UserType = new UserType();
                try
                {
                    var UserType = UserTypeData.Select.GetUserType(request.UserTypeID);
                    if (!UserType.Item1.Error)
                    {
                        response.UserType = new UserType()
                        {
                            id = UserType.Item2.id,
                            name = UserType.Item2.name,
                            detail = UserType.Item2.detail,
                            createDate = UserType.Item2.createDate,
                            upDateDate = UserType.Item2.upDateDate,
                            deleteDate = UserType.Item2.deleteDate,
                            state = UserType.Item2.state
                        };

                    }
                    else
                    {
                        response.Error.InfoError(UserType.Item1);
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
            /// <param name="request">User Type Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetUserTypeResponse UserType(GetUserTypeResponse request)
            {
                GetUserTypeResponse response = new GetUserTypeResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblUserType UserType = new tblUserType()
                    {
                        id = request.UserType.id,
                        name = request.UserType.name,
                        detail = request.UserType.detail,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = UserTypeData.Insert.UserType(UserType);
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
            /// <param name="request">CellarArea Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetUserTypeResponse UserType(GetUserTypeResponse request)
            {

                GetUserTypeResponse response = new GetUserTypeResponse();
                try
                {
                    tblUserType UserType = new tblUserType()
                    {
                        id = request.UserType.id,
                        name = request.UserType.name,
                        detail = request.UserType.detail,
                        createDate = request.UserType.createDate,
                        upDateDate = DateTime.Now,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = UserTypeData.Update.UserType(UserType);
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
            /// <param name="UserTypeID">UserTypeID</param>
            /// <param name="state">State</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetUserTypeResponse UserTypeDisable(int UserTypeID, string state)
            {
                GetUserTypeResponse response = new GetUserTypeResponse();
                try
                {
                    var result = UserTypeData.Delete.UserTypeDisable(UserTypeID, state);
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
