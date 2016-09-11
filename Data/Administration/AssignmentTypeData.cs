using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Entity;

namespace Data
{

    public class AssignmentTypeData
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
            /// Return All Assignment Type
            /// </summary>
            /// <returns>All Assignment Type Or null If Exist Error</returns>
            public static Tuple<ErrorObject, List<tblAssignmentType>> GetAssignmentType()
            {
                List<tblAssignmentType> AssignmentType = new List<tblAssignmentType>();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        AssignmentType = db.tblAssignmentType.ToList();
                    };

                    return new Tuple<ErrorObject, List<tblAssignmentType>>(erros.IfError(false), AssignmentType);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<tblAssignmentType>>(erros, AssignmentType);
                }

            }

            /// <summary>
            /// Return Assignment Type By Specific ID
            /// </summary>
            /// <param name="id">Assignment Type ID</param>
            /// <returns>Assignment Type By Specific ID Or null If Exist Error</returns>
            public static Tuple<ErrorObject, tblAssignmentType> GetAssignmentType(int id)
            {
                tblAssignmentType AssignmentType = new tblAssignmentType();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        AssignmentType = db.tblAssignmentType.Find(id);
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblAssignmentType>(erros.IfError(false), AssignmentType);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblAssignmentType>(erros, AssignmentType);
                }
            }

            /// <summary>
            /// Return Assignment Type Name
            /// </summary>
            /// <param name="id">Assignment Type ID</param>
            /// <returns>Assignment Type Name</returns>
            public static Tuple<ErrorObject, tblAssignmentType> GetAssignmentTypeName(int id)
            {
                tblAssignmentType AssignmentType = new tblAssignmentType();
                erros = new ErrorObject();

                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        AssignmentType.name = db.tblAssignmentType.Find(id).name;
                    }
                    erros.Error = false;
                    return new Tuple<ErrorObject, tblAssignmentType>(erros.IfError(false), AssignmentType);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, tblAssignmentType>(erros, AssignmentType);
                }
            }
            
        }
        #endregion

        #region Insert Data
        public class Insert
        {
            /// <summary>
            /// Insert Assignment Type Information
            /// </summary>
            /// <param name="data">Assignment Type Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> AssignmentType(tblAssignmentType data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblAssignmentType.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblAssignmentType.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblAssignmentType.Add(data);
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
            /// Update Assignment Type Information
            /// </summary>
            /// <param name="data">Assignment Type Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> AssignmentType(tblAssignmentType data)
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
            /// Update State Fields To Specific AssignmentType ID
            /// </summary>
            /// <param name="AssignmentTypeID">AssignmentType ID</param>
            /// <param name="state">Active Or Disable</param>
            /// <returns>AssignmentType ID</returns>
            public static Tuple<ErrorObject, string> AssignmentTypeDisable(int AssignmentTypeID, string state)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        var row = db.tblAssignmentType.Single(p => p.id == AssignmentTypeID);
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
