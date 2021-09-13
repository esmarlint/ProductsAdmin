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

    public class RestPaginatedResponse<T> : GenericRestResponse
    {
        public List<T> Payload { get; set; }

        public override bool Ok { get; set; } = true;

        public PaginationData Pagination { get; set; } = new PaginationData();

        public RestPaginatedResponse()
        {
        }

        public RestPaginatedResponse(List<T> payload, PaginationParameters paginationParameters = null)
        {
            this.Payload = payload;
            this.Pagination.Total = payload.Count;

            if (paginationParameters != null)
            {
                Pagination.Page= paginationParameters.Page;
                Pagination.PageSize = paginationParameters.PageSize;
            }
        }

        public class PaginationData
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
            public int Total { get; set; }
        }
    }

    public class RestErrorResponse : GenericRestResponse
    {
        public string ErrorType { get; set; } = "generic_error";

        public string Error { get; set; }

        public override bool Ok { get; set; } = false;

    }

}
