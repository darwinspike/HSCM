using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entity;
using System.Data.Entity;

namespace Data
{
    public class CategoryData
    {
        #region Property
        private static int result = 0;
        private static string Message = "";
        private static ErrorObject erros;
        #endregion

        #region Select Data
        public class Select
        {
            /// <summary>
            /// Return True Or False If Container Child
            /// </summary>
            /// <param name="id">Category ID</param>
            /// <returns>True Or False If Container Child</returns>
            public static Tuple<ErrorObject, bool> ContainerChild(int CategoryID)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var algo = db.tblCategory.Where(ty => ty.idCategory == CategoryID).ToList();
                        if (algo.Count > 0)
                        {
                            return new Tuple<ErrorObject, bool>(erros, true);
                        }
                        else
                        {
                            return new Tuple<ErrorObject, bool>(erros, false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, bool>(erros, false);
                }
            }

            /// <summary>
            /// Return Category List 
            /// </summary>
            /// <returns>Category List</returns>
            public static Tuple<ErrorObject, List<tblCategory>> GetCategory()
            {
                List<tblCategory> c = new List<tblCategory>();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        c = db.tblCategory.OrderBy(d1 => d1.idCategory).ThenBy(d1 => d1.id).ToList();
                        return new Tuple<ErrorObject, List<tblCategory>>(erros.IfError(false), c);
                    };
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblCategory>>(erros, c);
                }
            }

            /// <summary>
            /// Return Department Information
            /// </summary>
            /// <param name="CategoryID">Category ID</param>
            /// <returns>Department Information</returns>
            public static Tuple<ErrorObject, tblCategory> GetCategory(int CategoryID)
            {
                tblCategory c = new tblCategory();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        c = db.tblCategory.Find(CategoryID);
                        return new Tuple<ErrorObject, tblCategory>(erros.IfError(false), c);
                    }
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblCategory>(erros, c);
                }

            }

            /// <summary>
            /// Return Department List To Only father
            /// </summary>
            /// <returns></returns>
            public static Tuple<ErrorObject, List<tblCategory>> GetCategoryToFather()
            {
                List<tblCategory> c = new List<tblCategory>();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        c = db.tblCategory.Where(f => f.idCategory.ToString() == String.Empty).ToList();
                        return new Tuple<ErrorObject, List<tblCategory>>(erros.IfError(false), c);
                    }
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblCategory>>(erros, c);
                }

            }
        }
        #endregion

        #region Insert Data
        public class Insert
        {
            /// <summary>
            /// Insert Category
            /// </summary>
            /// <param name="data">Information Cateogy</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> Category(tblCategory data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblCategory.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblCategory.Max(s => s.id + 1);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblCategory.Add(data);
                        result = db.SaveChanges();
                        Message = "Affected Row: " + result.ToString();
                        return new Tuple<ErrorObject, string>(erros.IfError(false), Message);
                    }
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, string>(erros, String.Empty);
                }

            }
        }
        #endregion

        #region Update Data
        public class Update
        {
            /// <summary>
            /// Update Category
            /// </summary>
            /// <param name="data">Category Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> Category(tblCategory data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        db.Entry(data).State = EntityState.Modified;
                        result = db.SaveChanges();
                        Message = "Affected Row: " + result.ToString();

                        return new Tuple<ErrorObject, string>(erros.IfError(false), Message);
                    }
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, string>(erros, String.Empty);
                }
            }
        }
        #endregion

        #region Delete or Disable Data
        public class Delete
        {
            /// <summary>
            /// Update State Fields To Specific Category ID
            /// </summary>
            /// <param name="CategoryID">Category ID</param>
            /// <param name="state"></param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> CategoryDisable(int CategoryID, string state)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblCategory.Single(p => p.id == CategoryID);
                        row.state = state;
                        row.deleteDate = DateTime.Now;
                        result = db.SaveChanges();
                        Message = "Affected Row: " + result.ToString();
                        erros.Error = false;
                        return new Tuple<ErrorObject, string>(erros.IfError(false), Message);
                    }
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, string>(erros, String.Empty);
                }
            }
        }
        #endregion
    }
}
