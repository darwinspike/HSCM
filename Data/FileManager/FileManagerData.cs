using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entity;

namespace Data.FileManager
{
    public class FileManagerData
    {
        #region Property
        private static int result = 0;
        private static ErrorObject erros;
        #endregion

        #region Select Data
        public class Select
        {
            /// <summary>
            /// Return FileManger By Specific Product ID
            /// </summary>
            /// <param name="Productid">ProductID</param>
            /// <returns>FileManger By Specific Product ID</returns>
            public static Tuple<ErrorObject, List<tblFileManager>> GetProductFileManager(int Productid)
            {
                List<tblFileManager> data = new List<tblFileManager>();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = (from FM in db.tblFileManager
                                join PFM in db.tblProductFileManager on FM.id equals PFM.idFileManager
                                join P in db.tblProduct on PFM.idProduct equals P.id
                                where P.id == Productid
                                select FM
                           ).ToList();
                    };
                    return new Tuple<ErrorObject, List<tblFileManager>>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblFileManager>>(erros, data);
                }

            }

        }
        #endregion

        #region Insert Data
        public class Insert
        {

            /// <summary>
            /// Insert FileManager
            /// </summary>
            /// <param name="data">FileManger Information</param>
            /// <returns></returns>
            public static Tuple<ErrorObject, int> FileManager(tblFileManager data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblFileManager.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblFileManager.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblFileManager.Add(data);
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
