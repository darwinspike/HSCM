using Data;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines
{
    /// <summary>
    /// Model Site
    /// </summary>
    public class Category {
        public int id { get; set; }
        public string name { get; set; }
        public int CategoryID { get; set; }

        [DataType(DataType.MultilineText)]
        public string detail { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> upDateDate { get; set; }
        public Nullable<System.DateTime> deleteDate { get; set; }
        public string state { get; set; }
    }

    public class CategoryBussines
    {
        #region Logic, Responces and Requests

        /// <summary>
        /// When You Need To Get Response
        /// </summary>
        public class GetCategoryResponse
        {
            public Handler.ErrorObject Error { get; set; }
            public string Message { get; set; }
            public bool IfFather { get; set; }
            public Category Category { get; set; }
            public List<Category> CategoryList { get; set; }
        }

        /// <summary>
        /// When you Need To Specify Petition
        /// </summary>
        public class GetCategoryRequest
        {
            public int CategoryID { get; set; }
        }

        #endregion

        #region Select Data
        public class Select
        {
            /// <summary>
            /// Return Category List
            /// </summary>
            /// <returns>Category List</returns>
            public static GetCategoryResponse GetCategory()
            {
                GetCategoryResponse response = new GetCategoryResponse();
                response.CategoryList = new List<Category>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var GetCategory = CategoryData.Select.GetCategory();
                    if (!GetCategory.Item1.Error)
                    {
                        foreach (var item in GetCategory.Item2)
                        {
                            response.CategoryList.Add(new Category()
                            {
                                id = item.id,
                                name = item.name,
                                detail = item.detail,
                                CategoryID = ((String.IsNullOrEmpty(item.idCategory.ToString()) ? (0) : ((int)item.idCategory) )),
                                createDate = item.createDate,
                                upDateDate = item.upDateDate,
                                deleteDate = item.deleteDate,
                                state = item.state
                            });
                        }
                    }
                    else
                    {
                        response.Error.InfoError(GetCategory.Item1);
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }
                return response;
            }

            /// <summary>
            /// Return Category Information
            /// </summary>
            /// <param name="request">Category ID</param>
            /// <returns>Category Information</returns>
            public static GetCategoryResponse GetCategory(GetCategoryRequest request)
            {
                GetCategoryResponse response = new GetCategoryResponse();
                response.Error = new Handler.ErrorObject();
                response.Category = new Category();
                try
                {
                    var GetCategory = CategoryData.Select.GetCategory(request.CategoryID);
                    if (!GetCategory.Item1.Error)
                    {
                        response.Category = new Category()
                        {
                            id = GetCategory.Item2.id,
                            name = GetCategory.Item2.name,
                            detail = GetCategory.Item2.detail,
                            CategoryID = ((String.IsNullOrEmpty(GetCategory.Item2.idCategory.ToString()) ? (0) : ((int)GetCategory.Item2.idCategory))),
                            createDate = GetCategory.Item2.createDate,
                            upDateDate = GetCategory.Item2.upDateDate,
                            deleteDate = GetCategory.Item2.deleteDate,
                            state = GetCategory.Item2.state
                        };
                    }
                    else
                    {
                        response.Error.InfoError(GetCategory.Item1);
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }

                return response;
            }

            /// <summary>
            /// Return Category List To Father Category
            /// </summary>
            /// <returns>Category List To Father Category</returns>
            public static GetCategoryResponse GetCategoryToFather()
            {
                GetCategoryResponse response = new GetCategoryResponse();
                response.CategoryList = new List<Category>();
                response.Error = new Handler.ErrorObject();

                try
                {
                    var GetCategory = CategoryData.Select.GetCategoryToFather();
                    if (!GetCategory.Item1.Error)
                    {
                        foreach (var item in GetCategory.Item2)
                        {
                            response.CategoryList.Add(new Category()
                            {
                                id = item.id,
                                name = item.name,
                                detail = item.detail,
                                CategoryID = ((String.IsNullOrEmpty(item.idCategory.ToString()) ? (0) : ((int)item.idCategory))),
                                createDate = item.createDate,
                                upDateDate = item.upDateDate,
                                deleteDate = item.deleteDate,
                                state = item.state
                            });
                        }
                    }
                    else
                    {
                        response.Error.InfoError(GetCategory.Item1);
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }
                return response;
            }

            /// <summary>
            /// Return If It's Pather or If Container Child
            /// </summary>
            /// <param name="request">Category ID</param>
            /// <returns>If It's Pather or If Container Child</returns>
            public static GetCategoryResponse ContainerChild(GetCategoryRequest request)
            {
                GetCategoryResponse response = new GetCategoryResponse();
                response.Error = new Handler.ErrorObject();
                try
                {
                    var result = CategoryData.Select.ContainerChild(request.CategoryID);
                    if (result.Item1.Error)
                    {
                        response.Error.InfoError(result.Item1);
                    }
                    else
                    {
                        response.IfFather = result.Item2;
                    }
                }
                catch (Exception ex)
                {
                    response.Error.InfoError(ex);
                }
                return response;
            }

            public static List<Category> CategoryListOrderByFatherAndChild(List<Category> CList) {

                List<Category> CategoryList = new List<Category>();
                foreach (var item in CList)
                {
                    if (item.CategoryID == 0)
                    {
                        CategoryList.Add(new Category()
                        {
                            id = item.id,
                            name = item.name,
                            CategoryID = 0,
                            detail = item.detail,
                            state = item.state
                        });
                        foreach (var item2 in CList)
                        {
                            if (item2.CategoryID > 0)
                            {
                                if (item.id == item2.CategoryID)
                                {
                                    CategoryList.Add(new Category()
                                    {
                                        id = item2.id,
                                        name = item2.name,
                                        CategoryID = item2.CategoryID,
                                        detail = item2.detail,
                                        state = item2.state
                                    });
                                }
                            }
                        }
                    }
                }
                return CategoryList;
            }

            /// <summary>
            /// Return Category Name To Specific ID
            /// </summary>
            /// <param name="request">CategoryID</param>
            /// <returns>Category Name</returns>
            public static GetCategoryResponse GetCategoryName(int request)
            {
                GetCategoryResponse response = new GetCategoryResponse();
                response.Error = new Handler.ErrorObject();
                response.Category = new Category();
                try
                {
                    var GetCategory = CategoryData.Select.GetCategoryName(request);
                    if (!GetCategory.Item1.Error)
                    {
                        response.Category = new Category()
                        {
                            name = GetCategory.Item2.name
                        };
                    }
                    else
                    {
                        response.Error.InfoError(GetCategory.Item1);
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
            /// <param name="request">Category Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetCategoryResponse Category(GetCategoryResponse request)
            {
                GetCategoryResponse response = new GetCategoryResponse();
                response.Error = new Handler.ErrorObject();

                try
                {
                    tblCategory Category = new tblCategory()
                    {
                        id = request.Category.id,
                        name = request.Category.name,
                        detail = request.Category.detail,
                        idCategory = request.Category.CategoryID,
                        createDate = DateTime.Now,
                        upDateDate = null,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = CategoryData.Insert.Category(Category);
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
            /// <param name="request">Category Information</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetCategoryResponse Category(GetCategoryResponse request)
            {
                GetCategoryResponse response = new GetCategoryResponse();
                response.Error = new Handler.ErrorObject();
                try
                {
                    tblCategory Category = new tblCategory()
                    {
                        id = request.Category.id,
                        name = request.Category.name,
                        detail = request.Category.detail,
                        idCategory = request.Category.CategoryID,
                        createDate = request.Category.createDate,
                        upDateDate = DateTime.Now,
                        deleteDate = null,
                        state = "Active"
                    };

                    var result = CategoryData.Update.Category(Category);
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
            /// <param name="CategoryID">Category ID</param>
            /// <param name="state">State</param>
            /// <returns>Affected Row Or Error If Exist</returns>
            public static GetCategoryResponse CategoryDisable(int CategoryID, string state)
            {
                GetCategoryResponse response = new GetCategoryResponse();
                response.Error = new Handler.ErrorObject();
                try
                {
                    var result = CategoryData.Delete.CategoryDisable(CategoryID, state);
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
