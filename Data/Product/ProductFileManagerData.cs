using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entity;

namespace Data.Product
{
    public class ProductFileManagerData
    {
        #region Property
        private static int result = 0;
        private static ErrorObject erros;
        #endregion

        #region Select Data
        public class Select{


        }
        #endregion

        #region Insert Data
        public class Insert
        {
            /// <summary>
            /// Insert Prodcuto File Manager Information
            /// </summary>
            /// <param name="data">Product FileManager INformaation</param>
            /// <returns>Product File Manager ID</returns>
            public static Tuple<ErrorObject, int> ProductFileManager(tblProductFileManager data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblProductFileManager.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblProductFileManager.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblProductFileManager.Add(data);
                        result = db.SaveChanges();

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
        public class Update{}
        #endregion

        #region Delete or Disable Data
        public class Delete{}
        #endregion


    }
}
