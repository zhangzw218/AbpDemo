using AbpDemo.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Identity;

namespace AbpDemo.Web.Service
{
    public class DemoLoopHandler : IDistributedEventHandler<DemoLoopEto>, ITransientDependency
    {
        protected IIdentityUserRepository IdentityUserRepository { get; }
        public DemoLoopHandler(IIdentityUserRepository identityUserRepository)
        {
            IdentityUserRepository = identityUserRepository;
        }
        public async Task HandleEventAsync(DemoLoopEto eventData)
        {
            Console.WriteLine($"DemoLoopHandler.Start===========:{eventData.StartDate},{eventData.Count}");
            var list = await IdentityUserRepository.GetListAsync();
            var query = await IdentityUserRepository.GetDbSetAsync();
            var query2 = query.Where(s => s.Name != "a");
            // Cannot access a disposed context instance. A common cause of this error is disposing a context instance that was resolved from dependency injection and then later trying to use the same context instance elsewhere in your application. This may occur if you are calling 'Dispose' on the context instance, or wrapping it in a using statement. If you are using dependency injection, you should let the dependency injection container take care of disposing context instances.
            var list2 = await query2.ToListAsync();
            Console.WriteLine($"DemoLoopHandler.End===========:{eventData.StartDate},{eventData.Count}");
            await Task.CompletedTask;
        }
    }
}
