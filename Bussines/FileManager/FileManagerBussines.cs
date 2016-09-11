using Data.FileManager;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.FileManager
{

    public class FileManagers
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public string fileType { get; set; }
        public Nullable<int> fileSize { get; set; }
        public byte[] fileFile { get; set; }
        public string fileDetail { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> upDateDate { get; set; }
        public Nullable<System.DateTime> deleteDate { get; set; }
        public string state { get; set; }
    }

    public class FileManagerBussines
    {

        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetFileManagerResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public FileManagers FileManager { get; set; }
            public List<FileManagers> FileManagerList { get; set; }
        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetFileManagerRequest
        {
            public int FileManagerID { get; set; }
        }

        #endregion

        #region Select Data
        public class Select
        {

            /// <summary>
            /// Return FileManager List
            /// </summary>
            /// <returns>FileManager List</returns>
            public static GetFileManagerResponse GetProductFileManager(GetFileManagerRequest request)
            {
                GetFileManagerResponse response = new GetFileManagerResponse();
                response.FileManagerList = new List<FileManagers>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var bussines = FileManagerData.Select.GetProductFileManager(request.FileManagerID);
                    if (!bussines.Item1.Error)
                    {
                        foreach (var item in bussines.Item2)
                        {
                            response.FileManagerList.Add(new FileManagers()
                            {
                                id = item.id,
                                fileFile = item.fileFile,
                                fileName = item.fileName,
                                fileDetail = item.fileDetail,
                                fileSize = item.fileSize,
                                fileType = item.fileType,
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
            public static GetFileManagerResponse FileManager(GetFileManagerResponse request)
            {
                GetFileManagerResponse response = new GetFileManagerResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblFileManager bussines = new tblFileManager()
                    {
                        id = request.FileManager.id,
                        fileFile = request.FileManager.fileFile,
                        fileName = request.FileManager.fileName,
                        fileSize = request.FileManager.fileSize,
                        fileType = request.FileManager.fileType,
                        fileDetail = request.FileManager.fileDetail,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = FileManagerData.Insert.FileManager(bussines);
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
