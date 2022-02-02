using Microsoft.EntityFrameworkCore;

namespace MoveisAPI.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParametarsPaginationInHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            if(httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

            double count = await queryable.CountAsync();
            httpContext.Response.Headers.Add("totalAmountOfRecords",count.ToString());
        }
    }
}
