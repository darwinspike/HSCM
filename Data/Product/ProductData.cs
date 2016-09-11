using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entity;

namespace Data.Product
{
    public class ProductData
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
            /// Return All Product
            /// </summary>
            /// <returns>All Product</returns>
            public static Tuple<ErrorObject, List<tblProduct>> GetProduct()
            {
                List<tblProduct> data = new List<tblProduct>();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = db.tblProduct.ToList();
                    };
                    return new Tuple<ErrorObject, List<tblProduct>>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblProduct>>(erros, data);
                }

            }

            /// <summary>
            /// Return Product By Specific ID
            /// </summary>
            /// <param name="id">Product ID</param>
            /// <returns>Product By Specific ID Or null If Exist Error</returns>
            public static Tuple<ErrorObject, tblProduct> GetProduct(int id)
            {
                tblProduct data = new tblProduct();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = db.tblProduct.Find(id);
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblProduct>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblProduct>(erros, data);
                }
            }

            /// <summary>
            /// Return Product List To Specific Cella Area ID
            /// </summary>
            /// <param name="CellarAreaID">Cellar Area ID</param>
            /// <returns>Product List To Specific Cella Area ID</returns>
            public static Tuple<ErrorObject, List<tblProduct>> GetProductByAssignmentType(int CellarAreaID)
            {
                List<tblProduct> data = new List<tblProduct>();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = (from P in db.tblProduct
                                join C in db.tblCellar on P.id equals C.idProduct
                                where C.idcellarArea == CellarAreaID
                                select P).ToList();
                    };
                    return new Tuple<ErrorObject, List<tblProduct>>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblProduct>>(erros, data);
                }

            }

            /// <summary>
            /// Return Product List To Special AssignmentType and AnchorAssignment
            /// </summary>
            /// <param name="AssignmentType">AssignmentType</param>
            /// <param name="AnchorAssingment">AnchorAssingment</param>
            /// <returns>Product List</returns>
            public static Tuple<ErrorObject, List<tblProduct>> GetProductsOfAssignment(int AssignmentType, int AnchorAssingment)
            {
                List<tblProduct> data = new List<tblProduct>();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = (
                        from A in db.tblAssignment
                        join P in db.tblProduct
                        on A.idProduct equals P.id
                        where A.idAnchorAssingment == AnchorAssingment && A.idAssignmentType == AssignmentType
                        select P).ToList();
                    };
                    return new Tuple<ErrorObject, List<tblProduct>>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblProduct>>(erros, data);
                }

            }

        }
        #endregion

        #region Insert Data
        public class Insert
        {
            /// <summary>
            /// Insert Employee Information
            /// </summary>
            /// <param name="data">Employee Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, int> product(tblProduct data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblProduct.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblProduct.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblProduct.Add(data);
                        result = db.SaveChanges();
                        Message = "Affected Row: " + result.ToString();

                        if (result > 0)
                        {
                            return new Tuple<ErrorObject, int>(erros.IfError(false), data.id);
                        }
                        else
                        {
                            return new Tuple<ErrorObject, int>(erros.IfError(false), result);
                        }


                    }
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, int>(erros, 0);
                }

            }

        }
        #endregion

        #region Update Data
        public class Update
        {
            /// <summary>
            /// Update Product Information
            /// </summary>
            /// <param name="data">Product Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> product(tblProduct data)
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
            /// Update State Fields To Specific Product ID
            /// </summary>
            /// <param name="ProductID">ProductID</param>
            /// <param name="state">Active Or Disable</param>
            /// <returns>ProductID</returns>
            public static Tuple<ErrorObject, string> productDisable(int ProductID, string state)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblProduct.Single(p => p.id == ProductID);
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
