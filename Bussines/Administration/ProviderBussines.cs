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
    public class Provider
    {
        public int id { get; set; }
        public string companyName { get; set; }


        [DataType(DataType.MultilineText)]
        public string detail { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> upDateDate { get; set; }
        public Nullable<System.DateTime> deleteDate { get; set; }
        public string state { get; set; }
    }

    public class ProviderBussines
    {

        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetProviderResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public Provider Provider { get; set; }
            public List<Provider> ProviderList { get; set; }
        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetProviderRequest
        {
            public int ProviderID { get; set; }
        }

        #endregion


        #region Select Data
        public class Select
        {

            /// <summary>
            /// Return Provider List
            /// </summary>
            /// <returns>Provider List</returns>
            public static GetProviderResponse GetProviderList()
            {
                GetProviderResponse response = new GetProviderResponse();
                response.ProviderList = new List<Provider>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var provider = ProviderData.Select.GetProviderList();
                    if (!provider.Item1.Error)
                    {
                        foreach (var item in provider.Item2)
                        {
                            response.ProviderList.Add(new Provider()
                            {
                                id = item.id,
                                companyName = item.companyName,
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
                        response.Error.InfoError(provider.Item1);
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }
                return response;
            }

            /// <summary>
            /// Return Provider Information
            /// </summary>
            /// <param name="request">Provider ID</param>
            /// <returns>Provider Information</returns>
            public static GetProviderResponse GetProvider(GetProviderRequest request)
            {
                GetProviderResponse response = new GetProviderResponse();
                response.Error = new Handler.ErrorObject();
                response.Provider = new Provider();
                try
                {
                    var provider = ProviderData.Select.GetProvider(request.ProviderID);
                    if (!provider.Item1.Error)
                    {
                        response.Provider = new Provider()
                        {
                            id = provider.Item2.id,
                            companyName = provider.Item2.companyName,
                            detail = provider.Item2.detail,
                            createDate = provider.Item2.createDate,
                            upDateDate = provider.Item2.upDateDate,
                            deleteDate = provider.Item2.deleteDate,
                            state = provider.Item2.state
                        };

                    }
                    else
                    {
                        response.Error.InfoError(provider.Item1);
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
            /// <param name="request">Provider Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetProviderResponse Provider(GetProviderResponse request)
            {
                GetProviderResponse response = new GetProviderResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblProvider provider = new tblProvider()
                    {
                        id = request.Provider.id,
                        companyName = request.Provider.companyName,
                        detail = request.Provider.detail,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = ProviderData.Insert.Provider(provider);
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
            /// <param name="request">Provider Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetProviderResponse Provider(GetProviderResponse request)
            {

                GetProviderResponse response = new GetProviderResponse();
                try
                {
                    tblProvider provider = new tblProvider()
                    {
                        id = request.Provider.id,
                        companyName = request.Provider.companyName,
                        detail = request.Provider.detail,
                        createDate = request.Provider.createDate,
                        upDateDate = DateTime.Now,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = ProviderData.Update.Provider(provider);
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
            /// <param name="ProviderID">ProviderID</param>
            /// <param name="state">State</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetProviderResponse ProviderDisable(int ProviderID, string state)
            {
                GetProviderResponse response = new GetProviderResponse();
                try
                {
                    var result = ProviderData.Delete.ProviderDisable(ProviderID, state);
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
