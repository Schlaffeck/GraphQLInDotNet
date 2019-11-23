using GraphQLInDotNet.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlInDotNet.GraphQl.Middleware
{
    public class SeedDataMiddleware
    {
        private readonly RequestDelegate next;

        public SeedDataMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, ISeeder dataSeeder)
        {
            var dataContext = (IDataContext)context.RequestServices.GetService(typeof(IDataContext));
            await dataSeeder.SeedDataAsync(dataContext);
            await this.next(context);
        }
    }
}
