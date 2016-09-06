using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data.Product;
using Entity;
using System.ComponentModel.DataAnnotations;

namespace Bussines.Product
{

    /// <summary>
    /// Model Site
    /// </summary>
    public partial class ProductType
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

    public class ProductTypeBussines
    {

        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetProductTypeResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public ProductType ProductType { get; set; }
            public List<ProductType> ProductTypeList { get; set; }
        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetProductTypeRequest
        {
            public int ProductTypeID { get; set; }
        }

        #endregion


        #region Select Data
        public class Select
        {

            /// <summary>
            /// Return Product Type List
            /// </summary>
            /// <returns>ProductType List</returns>
            public static GetProductTypeResponse GetProductTypeList()
            {
                GetProductTypeResponse response = new GetProductTypeResponse();
                response.ProductTypeList = new List<ProductType>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var ProductType = ProductTypeData.Select.GetProductTypeList();
                    if (!ProductType.Item1.Error)
                    {
                        foreach (var item in ProductType.Item2)
                        {
                            response.ProductTypeList.Add(new ProductType()
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
                        response.Error.InfoError(ProductType.Item1);
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }
                return response;
            }

            /// <summary>
            /// Return ProductType Information
            /// </summary>
            /// <param name="request">ProductType ID</param>
            /// <returns>ProductType Information</returns>
            public static GetProductTypeResponse GetProductType(GetProductTypeRequest request)
            {
                GetProductTypeResponse response = new GetProductTypeResponse();
                response.Error = new Handler.ErrorObject();
                response.ProductType = new ProductType();
                try
                {
                    var ProductType = ProductTypeData.Select.GetProductType(request.ProductTypeID);
                    if (!ProductType.Item1.Error)
                    {
                        response.ProductType = new ProductType()
                        {
                            id = ProductType.Item2.id,
                            name = ProductType.Item2.name,
                            detail = ProductType.Item2.detail,
                            createDate = ProductType.Item2.createDate,
                            upDateDate = ProductType.Item2.upDateDate,
                            deleteDate = ProductType.Item2.deleteDate,
                            state = ProductType.Item2.state
                        };

                    }
                    else
                    {
                        response.Error.InfoError(ProductType.Item1);
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
            /// <param name="request">ProductType Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetProductTypeResponse ProductType(GetProductTypeResponse request)
            {
                GetProductTypeResponse response = new GetProductTypeResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblProductType ProductType = new tblProductType()
                    {
                        id = request.ProductType.id,
                        name = request.ProductType.name,
                        detail = request.ProductType.detail,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = ProductTypeData.Insert.ProductType(ProductType);
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
            /// <param name="request">ProductType Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetProductTypeResponse ProductType(GetProductTypeResponse request)
            {

                GetProductTypeResponse response = new GetProductTypeResponse();
                try
                {
                    tblProductType ProductType = new tblProductType()
                    {
                        id = request.ProductType.id,
                        name = request.ProductType.name,
                        detail = request.ProductType.detail,
                        createDate = request.ProductType.createDate,
                        upDateDate = DateTime.Now,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = ProductTypeData.Update.ProductType(ProductType);
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
            /// <param name="ProductTypeID">ProductTypeID</param>
            /// <param name="state">State</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetProductTypeResponse ProductTypeDisable(int ProductTypeID, string state)
            {
                GetProductTypeResponse response = new GetProductTypeResponse();
                try
                {
                    var result = ProductTypeData.Delete.ProductTypeDisable(ProductTypeID, state);
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
