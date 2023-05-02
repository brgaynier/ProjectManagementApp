using Application.Services.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<IBlockService, BlockService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IChangeOrderService, ChangeOrderService>();
            services.AddScoped<IChecklistService, ChecklistService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IWorkflowService, WorkflowService>();
            services.AddScoped<IWorkflowItemService, WorkflowItemService>();
            services.AddScoped<IEventService, EventService>();


            return services;
        }
    }
}

