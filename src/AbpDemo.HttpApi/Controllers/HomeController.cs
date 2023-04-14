using AbpDemo.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Localization;

namespace AbpDemo.Controllers
{
    /// <summary>
    /// 系统信息
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly IDistributedEventBus _distributedEventBus;
        public HomeController(IDistributedEventBus distributedEventBus)
        {
            _distributedEventBus = distributedEventBus;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task IndexAsync()
        {
            var startDate = DateTime.Now;
            var count = 1;
            Console.WriteLine($"开始循环发布事件===========:{startDate}");
            while(count < 1000)
            {
                var eto = new DemoLoopEto
                {
                    StartDate = startDate,
                    Count = count++
                };
                Console.WriteLine($"循环发布事件中===========:{eto.StartDate},{eto.Count}");
                await _distributedEventBus.PublishAsync(eto, false);
                await Task.Delay( 1000 );
            }
        }
    }
}
