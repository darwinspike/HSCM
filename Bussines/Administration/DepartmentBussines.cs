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
    public class Department
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


    public class DepartmentBussines
    {

        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetDepartmentResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public Department Department { get; set; }
            public List<Department> DepartmentList { get; set; }
        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetDepartmentRequest
        {
            public int DepartmentID { get; set; }
        }

        #endregion


        #region Select Data
        public class Select {

            /// <summary>
            /// Return Department List
            /// </summary>
            /// <returns>Department List</returns>
            public static GetDepartmentResponse GetDepartmentList()
            {
                GetDepartmentResponse response = new GetDepartmentResponse();
                response.DepartmentList = new List<Department>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var GetDepartment = DepartmentData.Select.GetDepartment();
                    if (!GetDepartment.Item1.Error)
                    {
                        foreach (var item in GetDepartment.Item2)
                        {
                            response.DepartmentList.Add(new Department()
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
                        response.Error.InfoError(GetDepartment.Item1);
                    }
                }
                catch (Exception ex) {
                    response.Error.InfoError(ex);
                }
                return response;
            }

            /// <summary>
            /// Return Department Information
            /// </summary>
            /// <param name="request">Department ID</param>
            /// <returns>Department Information</returns>
            public static GetDepartmentResponse GetDepartment(GetDepartmentRequest request)
            {
                GetDepartmentResponse response = new GetDepartmentResponse();
                response.Error = new Handler.ErrorObject();
                response.Department = new Department();
                try
                {
                    var GetDepartment = DepartmentData.Select.GetDepartment(request.DepartmentID);
                    if (!GetDepartment.Item1.Error)
                    {
                        response.Department =  new Department()
                        {
                            id = GetDepartment.Item2.id,
                            name = GetDepartment.Item2.name,
                            detail = GetDepartment.Item2.detail,
                            createDate = GetDepartment.Item2.createDate,
                            upDateDate = GetDepartment.Item2.upDateDate,
                            deleteDate = GetDepartment.Item2.deleteDate,
                            state = GetDepartment.Item2.state
                        };

                    }
                    else
                    {
                        response.Error.InfoError(GetDepartment.Item1);
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
        public class Insert {

            /// <summary>
            /// Return Affected Row Or Error If Exist
            /// </summary>
            /// <param name="request">Department Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetDepartmentResponse Department(GetDepartmentResponse request)
            {
                GetDepartmentResponse response = new GetDepartmentResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblDepartment Department = new tblDepartment()
                    {
                        id = request.Department.id,
                        name = request.Department.name,
                        detail = request.Department.detail,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = DepartmentData.Insert.Department(Department);
                    if (result.Item1.Error)
                    {
                        response.Error.InfoError(result.Item1);
                    }
                    else {
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
        public class Update {

            /// <summary>
            /// Return Affected Row Or Error If Exist
            /// </summary>
            /// <param name="request">Department Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetDepartmentResponse Department(GetDepartmentResponse request)
            {

                GetDepartmentResponse response = new GetDepartmentResponse();
                try
                {
                    tblDepartment Department = new tblDepartment()
                    {
                        id = request.Department.id,
                        name = request.Department.name,
                        detail = request.Department.detail,
                        createDate = DateTime.Now,
                        upDateDate = DateTime.Now,
                        deleteDate = null,
                        state = "Active"
                    };

                    request.Department.state = "Active";
                    request.Department.createDate = DateTime.Now;
                    var result = DepartmentData.Update.Department(Department);
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
        public class Delete {

            /// <summary>
            /// Return Affected Row Or Error If Exist
            /// </summary>
            /// <param name="DepartmentID">Department ID</param>
            /// <param name="state">State</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetDepartmentResponse DepartmentDisable(int DepartmentID, string state) {
                GetDepartmentResponse response = new GetDepartmentResponse();
                try
                {
                    var result = DepartmentData.Delete.DepartmentDisable(DepartmentID,state);
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
