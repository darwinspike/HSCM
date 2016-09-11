using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data.Transaction;
using Entity;
using System.ComponentModel.DataAnnotations;
using Bussines.Handler;

namespace Bussines.Transaction
{
    public class TransactionType
    {
        public int id { get; set; }
        public string name { get; set; }
        public string detail { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> upDateDate { get; set; }
        public Nullable<System.DateTime> deleteDate { get; set; }
        public string state { get; set; }
    }

    public class TransactionTypeBussines
    {
        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetTransactionTypeResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public TransactionType TransactionType { get; set; }
            public List<TransactionType> TransactionTypeList { get; set; }
        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetTransactionTypeRequest
        {
            public int TransactionTypeID { get; set; }
        }

        #endregion

        #region Select Data
        public class Select
        {

            /// <summary>
            /// Return TransactionType List
            /// </summary>
            /// <returns>TransactionType List</returns>
            public static GetTransactionTypeResponse GetTransactionTypeList()
            {
                GetTransactionTypeResponse response = new GetTransactionTypeResponse();
                response.TransactionTypeList = new List<TransactionType>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var bussines = TransactionTypeData.Select.GetTransactionTypeList();
                    if (!bussines.Item1.Error)
                    {
                        foreach (var item in bussines.Item2)
                        {
                            response.TransactionTypeList.Add(new TransactionType()
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
            /// Return TransactionType Information
            /// </summary>
            /// <param name="request">TransactionType ID</param>
            /// <returns>TransactionType Information</returns>
            public static GetTransactionTypeResponse GetEmployee(GetTransactionTypeRequest request)
            {
                GetTransactionTypeResponse response = new GetTransactionTypeResponse();
                response.Error = new Handler.ErrorObject();
                response.TransactionType = new TransactionType();
                try
                {
                    var bussines = TransactionTypeData.Select.GetTransactionType(request.TransactionTypeID);
                    if (!bussines.Item1.Error)
                    {
                        response.TransactionType = new TransactionType()
                        {
                            id = bussines.Item2.id,
                            name = bussines.Item2.name,
                            detail = bussines.Item2.detail,
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
            /// <param name="request">TransactionType Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetTransactionTypeResponse TransactionType(GetTransactionTypeResponse request)
            {
                GetTransactionTypeResponse response = new GetTransactionTypeResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblTransactionType bussines = new tblTransactionType()
                    {
                        id = request.TransactionType.id,
                        name = request.TransactionType.name,
                        detail = request.TransactionType.detail,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = TransactionTypeData.Insert.TransactionType(bussines);
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
            /// <param name="request">TransactionType Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetTransactionTypeResponse TransactionType(GetTransactionTypeResponse request)
            {

                GetTransactionTypeResponse response = new GetTransactionTypeResponse();
                try
                {
                    tblTransactionType bussines = new tblTransactionType()
                    {
                        id = request.TransactionType.id,
                        name = request.TransactionType.name,
                        detail = request.TransactionType.detail,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = TransactionTypeData.Update.TransactionType(bussines);
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
            public static GetTransactionTypeResponse TransactionTypeDisable(int TransactionTypeID, string state)
            {
                GetTransactionTypeResponse response = new GetTransactionTypeResponse();
                try
                {
                    var result = TransactionTypeData.Delete.TransactionTypeDisable(TransactionTypeID, state);
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
