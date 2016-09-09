using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data.Employee;
using Entity;
using System.ComponentModel.DataAnnotations;
using Bussines.Handler;

namespace Bussines.Employee
{
    /// <summary>
    /// Model Site
    /// </summary>
    public class Employees
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }
        public Nullable<int> idUserType { get; set; }
        public Nullable<int> idUser { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> upDateDate { get; set; }
        public Nullable<System.DateTime> deleteDate { get; set; }
        public string state { get; set; }
    }

    public class EmployeeBussines
    {

        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetEmployeeResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public Employees Employee { get; set; }
            public List<Employees> EmployeeList { get; set; }
        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetEmployeeRequest
        {
            public int EmployeeID { get; set; }
        }

        #endregion

        #region Select Data
        public class Select
        {

            /// <summary>
            /// Return Employee List
            /// </summary>
            /// <returns>Employee List</returns>
            public static GetEmployeeResponse GetEmployeeList()
            {
                GetEmployeeResponse response = new GetEmployeeResponse();
                response.EmployeeList = new List<Employees>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var bussines = EmployeeData.Select.GetEmployeeList();
                    if (!bussines.Item1.Error)
                    {
                        foreach (var item in bussines.Item2)
                        {
                            response.EmployeeList.Add(new Employees()
                            {
                                id = item.id,
                                firstName = item.firstName,
                                lastName = item.lastName,
                                email = item.email,
                                idUser = item.idUser,
                                idUserType = item.idUserType,
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
            /// Return Employee Information
            /// </summary>
            /// <param name="request">Employee ID</param>
            /// <returns>Employee Information</returns>
            public static GetEmployeeResponse GetEmployee(GetEmployeeRequest request)
            {
                GetEmployeeResponse response = new GetEmployeeResponse();
                response.Error = new Handler.ErrorObject();
                response.Employee = new Employees();
                try
                {
                    var bussines = EmployeeData.Select.GetEmployee(request.EmployeeID);
                    if (!bussines.Item1.Error)
                    {
                        response.Employee = new Employees()
                        {
                            id = bussines.Item2.id,
                            firstName = bussines.Item2.firstName,
                            lastName = bussines.Item2.lastName,
                            email = bussines.Item2.email,
                            idUser = bussines.Item2.idUser,
                            idUserType = bussines.Item2.idUserType,
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
        }
        #endregion

        #region Insert Data
        public class Insert
        {

            /// <summary>
            /// Return Affected Row Or Error If Exist
            /// </summary>
            /// <param name="request">Employee Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetEmployeeResponse Employee(GetEmployeeResponse request)
            {
                GetEmployeeResponse response = new GetEmployeeResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblEmployee bussines = new tblEmployee()
                    {
                        id = request.Employee.id,
                        firstName = request.Employee.firstName,
                        lastName = request.Employee.lastName,
                        email = request.Employee.email,
                        idUser = request.Employee.idUser,
                        idUserType = request.Employee.idUserType,                        
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = EmployeeData.Insert.Employee(bussines);
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
            /// <param name="request">Employee Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetEmployeeResponse Employee(GetEmployeeResponse request)
            {

                GetEmployeeResponse response = new GetEmployeeResponse();
                try
                {
                    tblEmployee bussines = new tblEmployee()
                    {
                        id = request.Employee.id,
                        firstName = request.Employee.firstName,
                        lastName = request.Employee.lastName,
                        email = request.Employee.email,
                        idUser = request.Employee.idUser,
                        idUserType = request.Employee.idUserType,
                        createDate = request.Employee.createDate,
                        upDateDate = DateTime.Now,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = EmployeeData.Update.Employee(bussines);
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
            /// <param name="EmployeeID">Employee ID</param>
            /// <param name="state">State</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetEmployeeResponse EmployeeDisable(int EmployeeID, string state)
            {
                GetEmployeeResponse response = new GetEmployeeResponse();
                try
                {
                    var result = EmployeeData.Delete.EmployeeDisable(EmployeeID, state);
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
