using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entity;

namespace Data.Transaction
{
    public class TransactionConfigurateData
    {

        #region Property
        private static int result = 0;
        private static string Message = "";
        private static ErrorObject erros;
        #endregion

        public class GetTransactionConfigurateDataResponse {

            #region Transaction Configuration
            public int TCid { get; set; }
            public Nullable<int> TCidTransaction { get; set; }
            public Nullable<int> TCidTransactionType { get; set; }
            public Nullable<int> TCidAnchorTransaction { get; set; }
            public string TCdetail { get; set; }
            public Nullable<System.DateTime> TCcreateDate { get; set; }
            public Nullable<System.DateTime> TCupDateDate { get; set; }
            public Nullable<System.DateTime> TCdeleteDate { get; set; }
            public string TCstate { get; set; }
            #endregion

            #region Transaction
            public Nullable<int> Tamount { get; set; }
            public Nullable<int> TidProvide { get; set; }
            public string Tdetail { get; set; }
            public Nullable<System.DateTime> TcreateDate { get; set; }
            public Nullable<System.DateTime> TupDateDate { get; set; }
            public Nullable<System.DateTime> TdeleteDate { get; set; }
            public string Tstate { get; set; }
            #endregion

            #region Transaction type
            public string TTname { get; set; }
            public string TTdetail { get; set; }
            public Nullable<System.DateTime> TTcreateDate { get; set; }
            public Nullable<System.DateTime> TTupDateDate { get; set; }
            public Nullable<System.DateTime> TTdeleteDate { get; set; }
            public string TTstate { get; set; }
            #endregion
        }


        #region Select Data

        public class Select {


            public static Tuple<ErrorObject, GetTransactionConfigurateDataResponse> GetTransaction(int TransactionID)
            {
                GetTransactionConfigurateDataResponse data = new GetTransactionConfigurateDataResponse();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = (from TC in db.tblTransactionConfigurate 
                                join T in db.tblTransaction 
                                on TC.idTransaction equals T.id
                                join TT in db.tblTransactionType
                                on TC.idTransactionType equals TT.id
                                where T.id == TransactionID
                                select new GetTransactionConfigurateDataResponse() {
                                    TCid = TC.id,
                                    TCcreateDate = TC.createDate,
                                    TCdeleteDate = TC.deleteDate,
                                    TCdetail = TC.detail,
                                    TCidAnchorTransaction = TC.idAnchorTransaction,
                                    TCidTransaction = TC.idTransaction,
                                    TCidTransactionType = TC.idTransactionType,
                                    TCstate = TC.state,
                                    TCupDateDate = TC.upDateDate,
                                    Tamount = T.amount,
                                    Tstate = T.state,
                                    TcreateDate = T.createDate,
                                    Tdetail = T.detail,
                                    TdeleteDate = T.deleteDate,
                                    TidProvide = T.idProvide,
                                    TupDateDate = T.upDateDate,
                                    TTdetail = TT.detail,
                                    TTcreateDate = TT.createDate,
                                    TTstate = TT.state,
                                    TTname = TT.name,
                                    TTdeleteDate = TT.deleteDate,
                                    TTupDateDate = TT.upDateDate
                                }).First();
                    };
                    return new Tuple<ErrorObject, GetTransactionConfigurateDataResponse>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, GetTransactionConfigurateDataResponse>(erros, data);
                }
            }

            public static Tuple<ErrorObject, List<GetTransactionConfigurateDataResponse>> GetTransactionList(int AnchorTransactionID, int TransactionTypeID)
            {
                List<GetTransactionConfigurateDataResponse> data = new List<GetTransactionConfigurateDataResponse>();
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data = (from TC in db.tblTransactionConfigurate
                                join T in db.tblTransaction
                                on TC.idTransaction equals T.id
                                join TT in db.tblTransactionType
                                on TC.idTransactionType equals TT.id
                                where TC.idAnchorTransaction == AnchorTransactionID && TC.idTransactionType == TransactionTypeID
                                select new GetTransactionConfigurateDataResponse()
                                {
                                    TCid = TC.id,
                                    TCcreateDate = TC.createDate,
                                    TCdeleteDate = TC.deleteDate,
                                    TCdetail = TC.detail,
                                    TCidAnchorTransaction = TC.idAnchorTransaction,
                                    TCidTransaction = TC.idTransaction,
                                    TCidTransactionType = TC.idTransactionType,
                                    TCstate = TC.state,
                                    TCupDateDate = TC.upDateDate,
                                    Tamount = T.amount,
                                    Tstate = T.state,
                                    TcreateDate = T.createDate,
                                    Tdetail = T.detail,
                                    TdeleteDate = T.deleteDate,
                                    TidProvide = T.idProvide,
                                    TupDateDate = T.upDateDate,
                                    TTdetail = TT.detail,
                                    TTcreateDate = TT.createDate,
                                    TTstate = TT.state,
                                    TTname = TT.name,
                                    TTdeleteDate = TT.deleteDate,
                                    TTupDateDate = TT.upDateDate
                                }).ToList();
                    };
                    return new Tuple<ErrorObject, List<GetTransactionConfigurateDataResponse>>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, List<GetTransactionConfigurateDataResponse>>(erros, data);
                }
            }

            public static Tuple<ErrorObject, int> GetTotalAmountToTransaction(int AnchorTransactionID, int TransactionTypeID)
            {
                int data = 0;
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        data =

                            (from TC in db.tblTransactionConfigurate 
                            join T in db.tblTransaction 
                            on TC.idTransaction equals T.id
                            where TC.idAnchorTransaction == AnchorTransactionID && TC.idTransactionType == TransactionTypeID                             
                            select T.amount
                             ).Sum(T => (int)T.Value);
                            }
                    erros.Error = false;
                    return new Tuple<ErrorObject, int>(erros.IfError(false), data);
                }
                catch (Exception ex)
                {
                    erros.InfoError(ex);
                    return new Tuple<ErrorObject, int>(erros, data);
                }
            }

        }

        #endregion


        #region Insert Data
        public class Insert
        {

            /// <summary>
            /// Insert TransactionConfigurate Information
            /// </summary>
            /// <param name="data">TransactionConfigurate Information</param>
            /// <returns>Number Affected Row</returns>
            public static Tuple<ErrorObject, string> TransactionConfigurate(tblTransactionConfigurate data)
            {
                erros = new ErrorObject();
                try
                {
                    using (HSCMEntities db = new HSCMEntities())
                    {
                        int propertyFind = db.tblTransactionConfigurate.Count();
                        if (propertyFind > 0)
                        {
                            data.id = db.tblTransactionConfigurate.Max(s => s.id);
                        }
                        else
                        {
                            data.id = 1;
                        }
                        db.tblTransactionConfigurate.Add(data);
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

    }
}
