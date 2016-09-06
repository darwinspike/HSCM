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

    public class TypePermission
    {
        public int id { get; set; }
        public string name { get; set; }

        [DataType(DataType.MultilineText)]
        public string detail { get; set; }
        public Nullable<int> numberLevel { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> upDateDate { get; set; }
        public Nullable<System.DateTime> deleteDate { get; set; }
        public string state { get; set; }
    }

    public class TypePermissionBussines
    {

        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetTypePermissionResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public TypePermission TypePermission { get; set; }
            public List<TypePermission> TypePermissionList { get; set; }
        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetTypePermissionRequest
        {
            public int TypePermissionID { get; set; }
        }

        #endregion


        #region Select Data
        public class Select
        {

            /// <summary>
            /// Return Type Permission List
            /// </summary>
            /// <returns>Type Permission List</returns>
            public static GetTypePermissionResponse GetTypePermissionList()
            {
                GetTypePermissionResponse response = new GetTypePermissionResponse();
                response.TypePermissionList = new List<TypePermission>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var TypePermission = TypePermissionData.Select.GetTypePermissionList();
                    if (!TypePermission.Item1.Error)
                    {
                        foreach (var item in TypePermission.Item2)
                        {
                            response.TypePermissionList.Add(new TypePermission()
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
                        response.Error.InfoError(TypePermission.Item1);
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }
                return response;
            }

            /// <summary>
            /// Return TypePermission Information
            /// </summary>
            /// <param name="request">TypePermission ID</param>
            /// <returns>TypePermission Information</returns>
            public static GetTypePermissionResponse GetTypePermission(GetTypePermissionRequest request)
            {
                GetTypePermissionResponse response = new GetTypePermissionResponse();
                response.Error = new Handler.ErrorObject();
                response.TypePermission = new TypePermission();
                try
                {
                    var TypePermission = TypePermissionData.Select.GetTypePermission(request.TypePermissionID);
                    if (!TypePermission.Item1.Error)
                    {
                        response.TypePermission = new TypePermission()
                        {
                            id = TypePermission.Item2.id,
                            name = TypePermission.Item2.name,
                            detail = TypePermission.Item2.detail,
                            createDate = TypePermission.Item2.createDate,
                            upDateDate = TypePermission.Item2.upDateDate,
                            deleteDate = TypePermission.Item2.deleteDate,
                            state = TypePermission.Item2.state
                        };

                    }
                    else
                    {
                        response.Error.InfoError(TypePermission.Item1);
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
        public class Insert{ }
        #endregion

        #region Update Data
        public class Update
        {

            /// <summary>
            /// Return Affected Row Or Error If Exist
            /// </summary>
            /// <param name="request">TypePermission Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetTypePermissionResponse TypePermission(GetTypePermissionResponse request)
            {
                GetTypePermissionResponse response = new GetTypePermissionResponse();
                try
                {
                    tblTypePermission TypePermission = new tblTypePermission()
                    {
                        id = request.TypePermission.id,
                        name = request.TypePermission.name,
                        detail = request.TypePermission.detail,
                        numberLevel = request.TypePermission.numberLevel,
                        createDate = request.TypePermission.createDate,
                        upDateDate = DateTime.Now,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = TypePermissionData.Update.TypePermission(TypePermission);
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
            /// <param name="TypePermissionID">TypePermission ID</param>
            /// <param name="state">State</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetTypePermissionResponse TypePermissionDisable(int TypePermissionID, string state)
            {
                GetTypePermissionResponse response = new GetTypePermissionResponse();
                try
                {
                    var result = TypePermissionData.Delete.TypePermissionDisable(TypePermissionID, state);
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
