using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Entity;
using Data.Product;
using Bussines.Product;
using Bussines.FileManager;


namespace Bussines.Product
{

    public class ProductFileManager
    {
        public int id { get; set; }
        public Nullable<int> idProduct { get; set; }
        public Nullable<int> idFileManager { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> upDateDate { get; set; }
        public Nullable<System.DateTime> deleteDate { get; set; }
        public string state { get; set; }
    }

    public class ProductFileManagerBussines
    {

        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetProductFileManagerResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public ProductFileManager ProductFileManager { get; set; }
            public List<ProductFileManager> ProductFileManagerList { get; set; }
        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetProductFileManagerRequest
        {
            public int ProductFileManager { get; set; }
        }

        public class GetProductFileManagerFileRequest {
            public ProductBussines.GetProductResponse product { get; set; }
            public FileManagerBussines.GetFileManagerResponse file { get; set; }
            public IEnumerable<HttpPostedFileBase> files { get; set; }
        }

        #endregion

        #region Select Data
        public class Select
        {


        }
        #endregion

        #region Insert Data
        public class Insert
        {

            public static int SaveProductWithFileManager(GetProductFileManagerFileRequest request) {

                #region Properties

                string productID = "";
                string fileManagerID = "";
                GetProductFileManagerResponse pfm = new GetProductFileManagerResponse();
                FileManagerBussines.GetFileManagerResponse fm = new FileManagerBussines.GetFileManagerResponse()
                {
                    FileManager = new FileManagers()
                };
                #endregion

                #region Save Product
                productID = ProductBussines.Insert.Product(request.product).Message;
                #endregion

                foreach (var item in request.files)
                {
                    #region Save File Manager

                    fileManagerID = "";
                    fm.FileManager = new FileManagers()
                    {                        
                        fileName = System.IO.Path.GetFileName(item.FileName),
                        fileType = item.ContentType,
                        fileSize = item.ContentLength,
                        fileDetail = ""
                    };
                    using (var reader = new System.IO.BinaryReader(item.InputStream))
                    {
                        fm.FileManager.fileFile = reader.ReadBytes(item.ContentLength);
                    }

                    fileManagerID = FileManagerBussines.Insert.FileManager(fm).Message;

                    #endregion

                    #region Save ProductFileManager

                    pfm.ProductFileManager = new Product.ProductFileManager()
                    {
                        idFileManager = Convert.ToInt16(fileManagerID),
                        idProduct = Convert.ToInt16(productID)
                    };
                    ProductFileManager(pfm);

                    #endregion
                }

                return 0;
            }

            /// <summary>
            /// Return Affected Row Or Error If Exist
            /// </summary>
            /// <param name="request">Employee Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetProductFileManagerResponse ProductFileManager(GetProductFileManagerResponse request)
            {
                GetProductFileManagerResponse response = new GetProductFileManagerResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblProductFileManager bussines = new tblProductFileManager()
                    {
                        idFileManager = request.ProductFileManager.idFileManager,
                        idProduct = request.ProductFileManager.idProduct,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = ProductFileManagerData.Insert.ProductFileManager(bussines);
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



        }
        #endregion

        #region Delete or Disable Data
        public class Delete
        {


        }
        #endregion

    }
}
