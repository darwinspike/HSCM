using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data;
using Entity;
using System.ComponentModel.DataAnnotations;
using Bussines.Handler;

namespace Bussines
{


    /// <summary>
    /// Model Site
    /// </summary>
    public class AssignmentType
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

    public class AssignmentTypeBussines
    {
        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetAssignmentTypeResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public AssignmentType AssignmentType { get; set; }
            public List<AssignmentType> AssignmentTypeList { get; set; }
        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetAssignmentTypeRequest
        {
            public int AssignmentTypeID { get; set; }
        }

        #endregion


        #region Select Data
        public class Select
        {

            /// <summary>
            /// Return AssignmentType List
            /// </summary>
            /// <returns>AssignmentType List</returns>
            public static GetAssignmentTypeResponse GetAssignmentTypeList()
            {
                GetAssignmentTypeResponse response = new GetAssignmentTypeResponse();
                response.AssignmentTypeList = new List<AssignmentType>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var GetAssignmentType = AssignmentTypeData.Select.GetAssignmentType();
                    if (!GetAssignmentType.Item1.Error)
                    {
                        foreach (var item in GetAssignmentType.Item2)
                        {
                            response.AssignmentTypeList.Add(new AssignmentType()
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
                        response.Error.InfoError(GetAssignmentType.Item1);
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }
                return response;
            }

            /// <summary>
            /// Return Assignment Type Information
            /// </summary>
            /// <param name="request">Assignment Type ID</param>
            /// <returns>Assignment Type Information</returns>
            public static GetAssignmentTypeResponse GetAssignmentType(GetAssignmentTypeRequest request)
            {
                GetAssignmentTypeResponse response = new GetAssignmentTypeResponse();
                response.Error = new Handler.ErrorObject();
                response.AssignmentType = new AssignmentType();
                try
                {
                    var GetAssignmentType = AssignmentTypeData.Select.GetAssignmentType(request.AssignmentTypeID);
                    if (!GetAssignmentType.Item1.Error)
                    {
                        response.AssignmentType = new AssignmentType()
                        {
                            id = GetAssignmentType.Item2.id,
                            name = GetAssignmentType.Item2.name,
                            detail = GetAssignmentType.Item2.detail,
                            createDate = GetAssignmentType.Item2.createDate,
                            upDateDate = GetAssignmentType.Item2.upDateDate,
                            deleteDate = GetAssignmentType.Item2.deleteDate,
                            state = GetAssignmentType.Item2.state
                        };

                    }
                    else
                    {
                        response.Error.InfoError(GetAssignmentType.Item1);
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
            /// <param name="request">Assignment Type Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetAssignmentTypeResponse AssignmentType(GetAssignmentTypeResponse request)
            {
                GetAssignmentTypeResponse response = new GetAssignmentTypeResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblAssignmentType AssignmentType = new tblAssignmentType()
                    {
                        id = request.AssignmentType.id,
                        name = request.AssignmentType.name,
                        detail = request.AssignmentType.detail,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = AssignmentTypeData.Insert.AssignmentType(AssignmentType);
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
            /// <param name="request">Assignment Type Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetAssignmentTypeResponse AssignmentType(GetAssignmentTypeResponse request)
            {

                GetAssignmentTypeResponse response = new GetAssignmentTypeResponse();
                try
                {
                    tblAssignmentType AssignmentType = new tblAssignmentType()
                    {
                        id = request.AssignmentType.id,
                        name = request.AssignmentType.name,
                        detail = request.AssignmentType.detail,
                        createDate = request.AssignmentType.createDate,
                        upDateDate = DateTime.Now,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = AssignmentTypeData.Update.AssignmentType(AssignmentType);
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
            /// <param name="AssignmentTypeID">AssignmentTypeID</param>
            /// <param name="state">State</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetAssignmentTypeResponse AssignmentTypeDisable(int AssignmentTypeID, string state)
            {
                GetAssignmentTypeResponse response = new GetAssignmentTypeResponse();
                try
                {
                    var result = AssignmentTypeData.Delete.AssignmentTypeDisable(AssignmentTypeID, state);
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
