using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLayer.Entities.Wrapper
{
    public class CrmResponseWrapper<TEntity>
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }

        [JsonProperty("value")]
        public TEntity Value { get; set; }
    }

    public class ApiPostResponse
    {
        //private string _error;
        //private bool _status;
        public Uri LocationPath { get; set; }
        public string EntityId { get; set; }
        //public bool Status { get { return _status; } set { _status = error == null; } }
        public bool Status { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
        public Error error { get; set; }
    }
}
