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

    public class UserConfiguration
    {
        public int id { get; set; }
        public Nullable<int> idUser { get; set; }
        public Nullable<int> idTypePermission { get; set; }
        public Nullable<int> idCellarArea { get; set; }
        public Nullable<int> idAssignmentType { get; set; }
        public Nullable<int> idAnchorAssignmentType { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> upDateDate { get; set; }
        public Nullable<System.DateTime> deleteDate { get; set; }
        public string state { get; set; }
    }

    public class UserConfigurationBussines
    {

        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetUserConfigurationResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public UserConfiguration UserConfiguration { get; set; }
            public List<UserConfiguration> UserConfigurationList { get; set; }
        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetUserConfigurationRequest
        {
            public int UserConfigurationID { get; set; }
        }

        public class GetUserConfigurationUserIDRequest
        {
            public int UserID { get; set; }
        }

        #endregion

        #region Select Data
        public class Select
        {

            /// <summary>
            /// Return UserConfiguration List
            /// </summary>
            /// <returns>UserConfiguration List</returns>
            public static GetUserConfigurationResponse GetUserConfigurationList()
            {
                GetUserConfigurationResponse response = new GetUserConfigurationResponse();
                response.UserConfigurationList = new List<UserConfiguration>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var bussines = UserConfigurationData.Select.GetUserConfigurationList();
                    if (!bussines.Item1.Error)
                    {
                        foreach (var item in bussines.Item2)
                        {
                            response.UserConfigurationList.Add(new UserConfiguration()
                            {
                                id = item.id,
                                idAnchorAssignmentType = item.idAnchorAssignmentType,
                                idAssignmentType = item.idAssignmentType,
                                idCellarArea = item.idCellarArea,
                                idTypePermission = item.idTypePermission,
                                idUser = item.idUser,
                                createDate = item.createDate,
                                upDateDate = item.upDateDate,
                                deleteDate = item.deleteDate,
                                state = item.state
                            });
                        }
                    }
                    else
                    {
                        response.Error.InfoError(bussines.Item1);
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }
                return response;
            }

            /// <summary>
            /// Return UserConfiguration Information
            /// </summary>
            /// <param name="request">UserConfiguration ID</param>
            /// <returns>UserConfiguration Information</returns>
            public static GetUserConfigurationResponse GetUserConfiguration(GetUserConfigurationRequest request)
            {
                GetUserConfigurationResponse response = new GetUserConfigurationResponse();
                response.Error = new Handler.ErrorObject();
                response.UserConfiguration = new UserConfiguration();
                try
                {
                    var bussines = UserConfigurationData.Select.GetUserConfiguration(request.UserConfigurationID);
                    if (!bussines.Item1.Error)
                    {
                        response.UserConfiguration = new UserConfiguration()
                        {
                            id = bussines.Item2.id,
                            idAnchorAssignmentType = bussines.Item2.idAnchorAssignmentType,
                            idAssignmentType = bussines.Item2.idAssignmentType,
                            idCellarArea = bussines.Item2.idCellarArea,
                            idTypePermission = bussines.Item2.idTypePermission,
                            idUser = bussines.Item2.idUser,
                            createDate = bussines.Item2.createDate,
                            upDateDate = bussines.Item2.upDateDate,
                            deleteDate = bussines.Item2.deleteDate,
                            state = bussines.Item2.state
                        };

                    }
                    else
                    {
                        response.Error.InfoError(bussines.Item1);
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }

                return response;
            }

            /// <summary>
            /// Return All UserConfiguration To Specific User ID
            /// </summary>
            /// <param name="UserID">User ID</param>
            /// <returns>All UserConfiguration To Specific User ID</returns>
            public static GetUserConfigurationResponse GetUserConfigurationListByUserID(GetUserConfigurationUserIDRequest UserID)
            {
                GetUserConfigurationResponse response = new GetUserConfigurationResponse();
                response.UserConfigurationList = new List<UserConfiguration>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    GetUserConfigurationUserIDRequest request = new GetUserConfigurationUserIDRequest()
                    {
                        UserID = UserID.UserID
                    };

                    var bussines = UserConfigurationData.Select.GetUserConfigurationListByUserID(request.UserID);
                    if (!bussines.Item1.Error)
                    {
                        foreach (var item in bussines.Item2)
                        {
                            response.UserConfigurationList.Add(new UserConfiguration()
                            {
                                id = item.id,
                                idAnchorAssignmentType = item.idAnchorAssignmentType,
                                idAssignmentType = item.idAssignmentType,
                                idCellarArea = item.idCellarArea,
                                idTypePermission = item.idTypePermission,
                                idUser = item.idUser,
                                createDate = item.createDate,
                                upDateDate = item.upDateDate,
                                deleteDate = item.deleteDate,
                                state = item.state
                            });
                        }
                    }
                    else
                    {
                        response.Error.InfoError(bussines.Item1);
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
            /// <param name="request">UserConfiguration Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetUserConfigurationResponse UserConfiguration(GetUserConfigurationResponse request)
            {
                GetUserConfigurationResponse response = new GetUserConfigurationResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblUserConfiguration bussines = new tblUserConfiguration()
                    {
                        id = request.UserConfiguration.id,
                        idAnchorAssignmentType = request.UserConfiguration.idAnchorAssignmentType,
                        idAssignmentType = request.UserConfiguration.idAssignmentType,
                        idCellarArea = request.UserConfiguration.idCellarArea,
                        idTypePermission = request.UserConfiguration.idTypePermission,
                        idUser = request.UserConfiguration.idUser,                        
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = UserConfigurationData.Insert.UserConfiguration(bussines);
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
            /// <param name="request">UserConfiguration Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetUserConfigurationResponse UserConfiguration(GetUserConfigurationResponse request)
            {

                GetUserConfigurationResponse response = new GetUserConfigurationResponse();
                try
                {
                    tblUserConfiguration bussines = new tblUserConfiguration()
                    {
                        id = request.UserConfiguration.id,
                        idAnchorAssignmentType = request.UserConfiguration.idAnchorAssignmentType,
                        idAssignmentType = request.UserConfiguration.idAssignmentType,
                        idCellarArea = request.UserConfiguration.idCellarArea,
                        idTypePermission = request.UserConfiguration.idTypePermission,                        
                        idUser = request.UserConfiguration.idUser,
                        createDate = request.UserConfiguration.createDate,
                        upDateDate = DateTime.Now,
                        deleteDate = request.UserConfiguration.deleteDate,
                        state = "Active"
                    };

                    var result = UserConfigurationData.Update.UserConfiguration(bussines);
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
            /// <param name="UserConfigurationID">UserConfiguration ID</param>
            /// <param name="state">State</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetUserConfigurationResponse UserConfigurationDisable(int UserConfigurationID, string state)
            {
                GetUserConfigurationResponse response = new GetUserConfigurationResponse();
                try
                {
                    var result = UserConfigurationData.Delete.UserConfigurationDisable(UserConfigurationID, state);
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
