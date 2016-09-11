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

    public class Transactions
    {
        public int id { get; set; }
        public Nullable<int> amount { get; set; }
        public Nullable<int> idProvide { get; set; }
        public string detail { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> upDateDate { get; set; }
        public Nullable<System.DateTime> deleteDate { get; set; }
        public string state { get; set; }
    }


    class TransactionBussines
    {
        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetTransactionResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public Transactions Transaction { get; set; }
        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetTransactionRequest
        {
            public int TransactionID { get; set; }
        }

        #endregion

        #region Select Data
        public class Select
        {
            /// <summary>
            /// Return Transaction Information
            /// </summary>
            /// <param name="request">Transaction ID</param>
            /// <returns>Transaction Information</returns>
            public static GetTransactionResponse GetEmployee(GetTransactionRequest request)
            {
                GetTransactionResponse response = new GetTransactionResponse();
                response.Error = new Handler.ErrorObject();
                response.Transaction = new Transactions();
                try
                {
                    var bussines = TransactionData.Select.GetTransaction(request.TransactionID);
                    if (!bussines.Item1.Error)
                    {
                        response.Transaction = new Transactions()
                        {
                            id = bussines.Item2.id,
                            amount = bussines.Item2.amount,
                            idProvide = bussines.Item2.idProvide,
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
            /// <param name="request">Transaction Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetTransactionResponse Transaction(GetTransactionResponse request)
            {
                GetTransactionResponse response = new GetTransactionResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblTransaction bussines = new tblTransaction()
                    {
                        id = request.Transaction.id,
                        amount = request.Transaction.amount,
                        detail = request.Transaction.detail,
                        idProvide = request.Transaction.idProvide,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = TransactionData.Insert.Transaction(bussines);
                    if (result.Item1.Error)
                    {
                        response.Error.InfoError(result.Item1);
                    }
                    else
                    {
                        response.Message = result.Item2.ToString();
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
            /// <param name="request">Transaction Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetTransactionResponse Transaction(GetTransactionResponse request)
            {

                GetTransactionResponse response = new GetTransactionResponse();
                try
                {
                    tblTransaction bussines = new tblTransaction()
                    {
                        id = request.Transaction.id,
                        amount = request.Transaction.amount,
                        detail = request.Transaction.detail,
                        idProvide = request.Transaction.idProvide,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = TransactionData.Update.Transaction(bussines);
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
            /// <param name="TransactionID">Transaction ID</param>
            /// <param name="state">State</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetTransactionResponse TransactionDisable(int TransactionID, string state)
            {
                GetTransactionResponse response = new GetTransactionResponse();
                try
                {
                    var result = TransactionData.Delete.TransactionDisable(TransactionID, state);
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
