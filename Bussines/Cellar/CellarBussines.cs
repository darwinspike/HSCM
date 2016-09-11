using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data.Cellar;
using Entity;
using System.ComponentModel.DataAnnotations;
using Bussines.Handler;
using Bussines.Transaction;

namespace Bussines.Cellar
{

    public class Cellars
    {
        public int id { get; set; }
        public Nullable<int> amount { get; set; }
        public Nullable<int> idProduct { get; set; }
        public Nullable<int> idcellarArea { get; set; }
        public string detail { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> upDateDate { get; set; }
        public Nullable<System.DateTime> deleteDate { get; set; }
        public string state { get; set; }
    }

    public class CellarBussines
    {
        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetCellarResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public int amount { get; set; }
            public int id { get; set; }
            public Cellars Cellar { get; set; }
            public List<Cellars> CellarList { get; set; }
            public Transactions transaction { get; set; }
    }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetCellarRequest
        {
            public int CellarID { get; set; }
            public int ProductID { get; set; }
            public int CellarAreaID { get; set; }
            public int TransactionTypeID { get; set; }
            public int amount { get; set; }
        }

        #endregion

        #region Select Data
        public class Select
        {

            /// <summary>
            /// Return Cellar List
            /// </summary>
            /// <returns>Employee List</returns>
            public static GetCellarResponse GetCellar()
            {
                GetCellarResponse response = new GetCellarResponse();
                response.CellarList = new List<Cellars>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var bussines = CellarData.Select.GetCellar();
                    if (!bussines.Item1.Error)
                    {
                        foreach (var item in bussines.Item2)
                        {
                            response.CellarList.Add(new Cellars()
                            {
                                id = item.id,
                                amount = item.amount,
                                detail = item.detail,
                                idProduct = item.idProduct,
                                idcellarArea = item.idcellarArea,
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
            /// Return Cellar Information
            /// </summary>
            /// <param name="request">Cellar ID</param>
            /// <returns>Cellar Information</returns>
            public static GetCellarResponse GetCellar(GetCellarRequest request)
            {
                GetCellarResponse response = new GetCellarResponse();
                response.Error = new Handler.ErrorObject();
                response.Cellar = new Cellars();
                try
                {
                    var bussines = CellarData.Select.GetCellar(request.CellarID);
                    if (!bussines.Item1.Error)
                    {
                        response.Cellar = new Cellars()
                        {
                            id = bussines.Item2.id,
                            amount = bussines.Item2.amount,
                            detail = bussines.Item2.detail,
                            idProduct = bussines.Item2.idProduct,
                            idcellarArea = bussines.Item2.idcellarArea,
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
            /// Return Cellar Information To Specific Cellar ID
            /// </summary>
            /// <param name="request">Cellar ID</param>
            /// <returns>Cellar Information</returns>
            public static GetCellarResponse GetCellarByProductID(GetCellarRequest request)
            {
                GetCellarResponse response = new GetCellarResponse();
                response.CellarList = new List<Cellars>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var bussines = CellarData.Select.GetCellarByProductID(request.ProductID);
                    if (!bussines.Item1.Error)
                    {
                        foreach (var item in bussines.Item2)
                        {
                            response.CellarList.Add(new Cellars()
                            {
                                id = item.id,
                                amount = item.amount,
                                detail = item.detail,
                                idProduct = item.idProduct,
                                idcellarArea = item.idcellarArea,
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
            /// Return Amount To Specific Product ID and Cellar Area ID
            /// </summary>
            /// <param name="request">Product ID and Cellar Area ID</param>
            /// <returns>Amount</returns>
            public static GetCellarResponse GetCellarByProductIDAndCellarArea(GetCellarRequest request)
            {
                GetCellarResponse response = new GetCellarResponse();
                response.Error = new Handler.ErrorObject();
                response.Cellar = new Cellars();
                try
                {
                    var bussines = CellarData.Select.GetCellarByProductIDAndCellarArea(request.ProductID, request.CellarAreaID);
                    if (!bussines.Item1.Error)
                    {
                        response.Cellar = new Cellars()
                        {
                            amount = bussines.Item2
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
            /// Return Cellar ID To Specific Product ID and Cellar Area ID
            /// </summary>
            /// <param name="request">Product ID and Cellar Area ID</param>
            /// <returns>Cellar ID</returns>
            public static GetCellarResponse GetCellarIDByProductIDAndCellarArea(GetCellarRequest request)
            {
                GetCellarResponse response = new GetCellarResponse();
                response.Error = new Handler.ErrorObject();
                response.Cellar = new Cellars();
                try
                {
                    var bussines = CellarData.Select.GetCellarIDByProductIDAndCellarArea(request.ProductID, request.CellarAreaID);
                    if (!bussines.Item1.Error)
                    {
                        response.Cellar = new Cellars()
                        {
                            amount = bussines.Item2
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

            public static GetCellarResponse Cellar(GetCellarResponse CellarInformation, GetCellarRequest request) {
                GetCellarResponse response = new GetCellarResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    request.CellarID = Convert.ToInt16(Cellars(CellarInformation).Message);
                    var result = Transaction.TransactionConfigurateBussines.Insert.TransactionConfigurate(request.CellarID, request.TransactionTypeID, CellarInformation.transaction);
                    response.Message = result.Message.ToString();
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
            /// <param name="request">Employee Information</param>
            /// <returns>Cellar ID</returns>
            private static GetCellarResponse Cellars(GetCellarResponse request)
            {
                GetCellarResponse response = new GetCellarResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblCellar bussines = new tblCellar()
                    {
                        id = request.Cellar.id,
                        amount = request.Cellar.amount,
                        detail = request.Cellar.detail,
                        idProduct = request.Cellar.idProduct,
                        idcellarArea = request.Cellar.idcellarArea,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = CellarData.Insert.Cellar(bussines);
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

            public static GetCellarResponse Cellar(GetCellarResponse cellarInformation, GetCellarRequest request)
            {
                GetCellarResponse response = new GetCellarResponse();
                response.Error = new Handler.ErrorObject();
                try
                {
                    CellarAmount(request);
                    var result = Transaction.TransactionConfigurateBussines.Insert.TransactionConfigurate(request.CellarID, request.TransactionTypeID, cellarInformation.transaction);
                    response.Message = result.Message.ToString();
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }
                return response;
            }

            private static GetCellarResponse Cellars(GetCellarResponse request)
            {

                GetCellarResponse response = new GetCellarResponse();
                try
                {
                    tblCellar bussines = new tblCellar()
                    {
                        id = request.Cellar.id,
                        amount = request.Cellar.amount,
                        detail = request.Cellar.detail,
                        idProduct = request.Cellar.idProduct,
                        idcellarArea = request.Cellar.idcellarArea,
                        createDate = request.Cellar.createDate,
                        upDateDate = DateTime.Now,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = CellarData.Update.Cellar(bussines);
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

            public static GetCellarResponse CellarAmount(GetCellarRequest request)
            {
                GetCellarResponse response = new GetCellarResponse();
                response.Error = new Handler.ErrorObject();
                try
                {
                    var result = CellarData.Update.CellarAmoun(request.CellarID, request.amount);

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
            /// <param name="CellarID">CellarID</param>
            /// <param name="state">State</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetCellarResponse CellarDisable(int CellarID, string state)
            {
                GetCellarResponse response = new GetCellarResponse();
                try
                {
                    var result = CellarData.Delete.CellarDisable(CellarID, state);
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
