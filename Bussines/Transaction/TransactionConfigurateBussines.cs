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

    public class TransactionConfigurate
    {
        public int id { get; set; }
        public Nullable<int> idTransaction { get; set; }
        public Nullable<int> idTransactionType { get; set; }
        public Nullable<int> idAnchorTransaction { get; set; }
        public string detail { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> upDateDate { get; set; }
        public Nullable<System.DateTime> deleteDate { get; set; }
        public string state { get; set; }
    }

    public class TransactionConfigurateBussines
    {

        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetTransactionConfigurateResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public int TotalAmount { get; set; }


            public TransactionConfigurate TransactionConfigurate { get; set; }
            public List<TransactionConfigurate> TransactionConfigurateList { get; set; }

            public Transactions Transaction { get; set; }
            public List<Transactions> TransactionList { get; set; }

            public TransactionType TransactionType { get; set; }
            public List<TransactionType> TransactionTypeList { get; set; }

        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetTransactionConfigurateRequest
        {
            public int TransactionID { get; set; }
            public int TransactionTypeID { get; set; }

            public int AnchorTransactionID { get; set; }
            public int TransactionConfigurateID { get; set; }
        }

        #endregion

        #region Select Data
        public class Select
        {

            public static GetTransactionConfigurateResponse GetTransaction(GetTransactionConfigurateRequest request) {

                GetTransactionConfigurateResponse response = new GetTransactionConfigurateResponse();
                response.Transaction = new Transactions();
                response.TransactionConfigurate = new TransactionConfigurate();
                response.TransactionType = new TransactionType();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var bussines = TransactionConfigurateData.Select.GetTransaction(request.TransactionID);
                    if (!bussines.Item1.Error)
                    {
                        response.Transaction = new Transactions()
                        {
                            id = (int)bussines.Item2.TCidTransaction,
                            amount = bussines.Item2.Tamount,
                            idProvide = bussines.Item2.TidProvide,
                            createDate = bussines.Item2.TcreateDate,
                            detail = bussines.Item2.Tdetail,
                            deleteDate = bussines.Item2.TTdeleteDate,
                            state = bussines.Item2.Tstate,
                            upDateDate = bussines.Item2.TupDateDate
                        };

                        response.TransactionType = new TransactionType()
                        {
                            id = (int)bussines.Item2.TCidTransactionType,
                            createDate = bussines.Item2.TTcreateDate,
                            deleteDate = bussines.Item2.TTdeleteDate,
                            detail = bussines.Item2.TTdetail,
                            name = bussines.Item2.TTname,
                            upDateDate = bussines.Item2.TTupDateDate,
                            state = bussines.Item2.TTstate

                        };

                        response.TransactionConfigurate = new TransactionConfigurate()
                        {
                            id = (int)bussines.Item2.TCid,
                            createDate = bussines.Item2.TCcreateDate,
                            deleteDate = bussines.Item2.TCdeleteDate,
                            detail = bussines.Item2.TCdetail,
                            idAnchorTransaction = bussines.Item2.TCidAnchorTransaction,
                            idTransaction = bussines.Item2.TCidTransaction,
                            idTransactionType = bussines.Item2.TCidTransactionType,
                            state = bussines.Item2.TCstate,
                            upDateDate = bussines.Item2.TCupDateDate
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

            public static GetTransactionConfigurateResponse GetTransactionList(GetTransactionConfigurateRequest request) {
                GetTransactionConfigurateResponse response = new GetTransactionConfigurateResponse();
                response.TransactionList = new List<Transactions>();
                response.TransactionConfigurateList = new List<TransactionConfigurate>();
                response.TransactionTypeList = new List<TransactionType>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var bussines = TransactionConfigurateData.Select.GetTransactionList(request.AnchorTransactionID, request.TransactionTypeID);
                    if (!bussines.Item1.Error)
                    {
                        foreach (var item in bussines.Item2) {
                            response.TransactionList.Add(new Transactions() {
                                id = (int)item.TCidTransaction,
                                amount = item.Tamount,
                                idProvide = item.TidProvide,
                                createDate = item.TcreateDate,
                                detail = item.Tdetail,
                                deleteDate = item.TTdeleteDate,
                                state = item.Tstate,
                                upDateDate = item.TupDateDate
                            });

                            response.TransactionTypeList.Add(new TransactionType()
                            {
                                id = (int)item.TCidTransactionType,
                                createDate = item.TTcreateDate,
                                deleteDate = item.TTdeleteDate,
                                detail = item.TTdetail,
                                name = item.TTname,
                                upDateDate = item.TTupDateDate,
                                state = item.TTstate
                            });

                            response.TransactionConfigurateList.Add(new TransactionConfigurate() {
                                id = item.TCid,
                                createDate = item.TCcreateDate,
                                deleteDate = item.TCdeleteDate,
                                detail = item.TCdetail,
                                idAnchorTransaction = item.TCidAnchorTransaction,
                                idTransaction = item.TCidTransaction,
                                idTransactionType = item.TCidTransactionType,
                                state = item.TCstate,
                                upDateDate = item.TCupDateDate
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

            public static GetTransactionConfigurateResponse GetTotalAmountToTransaction(GetTransactionConfigurateRequest request)
            {
                GetTransactionConfigurateResponse response = new GetTransactionConfigurateResponse();
                response.Error = new Handler.ErrorObject();
                try
                {
                    var bussines = TransactionConfigurateData.Select.GetTotalAmountToTransaction(request.AnchorTransactionID, request.TransactionTypeID);
                    if (!bussines.Item1.Error)
                    {
                        response.TotalAmount = bussines.Item2;
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

            public static GetTransactionConfigurateResponse TransactionConfigurate(int AnchorTransactionID, int TransactionTypeID, Transactions Transaction)
            {
                GetTransactionConfigurateResponse response = new GetTransactionConfigurateResponse();
                response.Error = new Handler.ErrorObject();
                try
                {
                    TransactionBussines.GetTransactionResponse transac = new TransactionBussines.GetTransactionResponse()
                    {
                        Transaction = Transaction
                    };
                    GetTransactionConfigurateRequest request = new GetTransactionConfigurateRequest()
                    {
                        AnchorTransactionID = AnchorTransactionID,
                        TransactionTypeID = TransactionTypeID
                    };
                    request.TransactionID = Convert.ToInt16(TransactionBussines.Insert.Transaction(transac).Message);
                    var result = TransactionConfigurates(request);
                    response.Message = result.Message;
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }
                return response;
            }

            /// <summary>
            /// Return Affected Row Or Error If Exist
            /// </summary>
            /// <param name="request">TransactionType Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            private static GetTransactionConfigurateResponse TransactionConfigurates(GetTransactionConfigurateRequest request)
            {
                GetTransactionConfigurateResponse response = new GetTransactionConfigurateResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblTransactionConfigurate bussines = new tblTransactionConfigurate()
                    {
                        detail = "",
                        idTransaction = request.TransactionID,
                        idTransactionType = request.TransactionTypeID,
                        idAnchorTransaction = request.AnchorTransactionID,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = TransactionConfigurateData.Insert.TransactionConfigurate(bussines);
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

        #region Update
        public class Update {

            public static GetTransactionConfigurateResponse TransactionConfigurate(Transactions Transaction)
            {
                GetTransactionConfigurateResponse response = new GetTransactionConfigurateResponse();
                response.Error = new Handler.ErrorObject();
                try
                {
                    TransactionBussines.GetTransactionResponse transac = new TransactionBussines.GetTransactionResponse()
                    {
                        Transaction = Transaction
                    };

                    string result = TransactionBussines.Update.Transaction(transac).Message;
                    response.Message = result;
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
