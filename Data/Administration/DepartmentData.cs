using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Entity;

namespace Data
{
    public class DepartmentData
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
            /// Return All Department
            /// </summary>
            /// <returns>All Department Or null If Exist Error</returns>
            public static Tuple<ErrorObject, List<tblDepartment>> GetDepartment()
            {
                List<tblDepartment> Department = new List<tblDepartment>();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        Department = db.tblDepartment.ToList();
                    };

                    return new Tuple<ErrorObject, List<tblDepartment>>(erros.IfError(false), Department);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblDepartment>>(erros, Department);
                }

            }

            /// <summary>
            /// Return Department By Specific ID
            /// </summary>
            /// <param name="id">Department ID</param>
            /// <returns>Department By Specific ID Or null If Exist Error</returns>
            public static Tuple<ErrorObject, tblDepartment> GetDepartment(int id)
            {
                tblDepartment Department = new tblDepartment();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        Department = db.tblDepartment.Find(id);
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblDepartment>(erros.IfError(false), Department);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblDepartment>(erros, Department);
                }
            }

            /// <summary>
            /// Return Department Name To Specific ID
            /// </summary>
            /// <param name="id">Department ID</param>
            /// <returns>Department Name</returns>
            public static Tuple<ErrorObject, tblDepartment> GetDepartmentName(int id)
            {
                tblDepartment Department = new tblDepartment();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        Department.name = db.tblDepartment.Find(id).name;
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblDepartment>(erros.IfError(false), Department);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblDepartment>(erros, Department);
                }
            }
        }
        #endregion

        #region Insert Data
        public class Insert
        {
            /// <summary>
            /// Insert Department Information
            /// </summary>
            /// <param name="data">Department Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> Department(tblDepartment data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblDepartment.Count();
                        if (propertyFind > 0)
                        { 
                            data.id = db.tblDepartment.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblDepartment.Add(data);
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
            /// Update Department Information
            /// </summary>
            /// <param name="data">Department Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> Department(tblDepartment data)
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
            /// Update State Fields To Specific Department ID
            /// </summary>
            /// <param name="DepartmentID">Department ID</param>
            /// <param name="state">Active Or Disable</param>
            /// <returns>ID Department ID</returns>
            public static Tuple<ErrorObject, string> DepartmentDisable(int DepartmentID, string state)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblDepartment.Single(p => p.id == DepartmentID);
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
