using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAdmin.Models.Api
{
    public class GenericRestResponse
    {
        public virtual bool Ok { get; set; }
    }

    public class RestOkResponse<T> : GenericRestResponse
    {
        public T Payload { get; set; }
        
        public override bool Ok { get; set; } = true;

        public RestOkResponse()
        {
        }

        public RestOkResponse(T payload)
        {
            this.Payload = payload;
        }
        
    }

    public class RestErrorResponse : GenericRestResponse
    {
        public string ErrorType { get; set; } = "generic_error";

        public string Error { get; set; }

        public override bool Ok { get; set; } = false;

    }

}
