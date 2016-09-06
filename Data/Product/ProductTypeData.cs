using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Entity;

namespace Data.Product
{
    public class ProductTypeData
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
            /// Return All Product Type
            /// </summary>
            /// <returns>All Product Type Or null If Exist Error</returns>
            public static Tuple<ErrorObject, List<tblProductType>> GetProductTypeList()
            {
                List<tblProductType> productType = new List<tblProductType>();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        productType = db.tblProductType.ToList();
                    };

                    return new Tuple<ErrorObject, List<tblProductType>>(erros.IfError(false), productType);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblProductType>>(erros, productType);
                }

            }

            /// <summary>
            /// Return Product Type By Specific ID
            /// </summary>
            /// <param name="id">Product Type ID</param>
            /// <returns>Produc Type By Specific ID Or null If Exist Error</returns>
            public static Tuple<ErrorObject, tblProductType> GetProductType(int id)
            {
                tblProductType productType = new tblProductType();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        productType = db.tblProductType.Find(id);
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblProductType>(erros.IfError(false), productType);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblProductType>(erros, productType);
                }
            }
        }
        #endregion

        #region Insert Data
        public class Insert
        {
            /// <summary>
            /// Insert Product Type Information
            /// </summary>
            /// <param name="data">Product Type Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> ProductType(tblProductType data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblProductType.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblCellarArea.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblProductType.Add(data);
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
            /// Update Cellar Area Information
            /// </summary>
            /// <param name="data">Cellar Area Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> ProductType(tblProductType data)
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
            /// Update State Fields To Specific CellarAreaID ID
            /// </summary>
            /// <param name="ProductTypeID">Cellar Area ID</param>
            /// <param name="state">Active Or Disable</param>
            /// <returns>ID Department ID</returns>
            public static Tuple<ErrorObject, string> ProductTypeDisable(int ProductTypeID, string state)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblProductType.Single(p => p.id == ProductTypeID);
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
