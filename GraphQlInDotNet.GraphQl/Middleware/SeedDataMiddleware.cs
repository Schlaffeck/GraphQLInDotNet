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
        private bool dataSeeded = false;

        public SeedDataMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, ISeeder dataSeeder, IDataContext dataContext)
        {
            if (!dataSeeded)
            {
                dataSeeded = true;
                await dataSeeder.SeedDataAsync(dataContext);
            }
            await this.next(context);
        }
    }
}
