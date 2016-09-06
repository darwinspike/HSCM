using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ErrorObject
    {
        public bool Error { get; set; }
        public System.Collections.IDictionary Data { get; set; }
        public string HelpLink { get; set; }
        public int HResult { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public System.Reflection.MethodBase TargetSite { get; set; }
        public Exception InnerException { get; set; }

        /// <summary>
        /// If Only You Know that it's error or not error
        /// </summary>
        /// <param name="isError"></param>
        public ErrorObject IfError(bool isError)
        {
            this.Error = isError;
            return this;
        }

        /// <summary>
        /// Setting Exception Error With Exception Type
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>Exception Values</returns>
        public ErrorObject InfoError(Exception ex)
        {

            this.Error = true;
            this.Data = ex.Data;
            this.HelpLink = ex.HelpLink;
            this.HResult = ex.HResult;
            this.Message = ex.Message;
            this.Source = ex.Source;
            this.StackTrace = ex.StackTrace;
            this.InnerException = ex.InnerException;
            this.TargetSite = ex.TargetSite;

            return this;
        }

        /// <summary>
        /// Setting Exception Error With Objet
        /// </summary>
        /// <param name="ex">Exception Error</param>
        /// <returns>Exception Values</returns>
        public ErrorObject InfoError(ErrorObject ex)
        {

            this.Error = true;
            this.Data = ex.Data;
            this.HelpLink = ex.HelpLink;
            this.HResult = ex.HResult;
            this.Message = ex.Message;
            this.Source = ex.Source;
            this.StackTrace = ex.StackTrace;
            this.InnerException = ex.InnerException;
            this.TargetSite = ex.TargetSite;

            return this;
        }


    }
}
