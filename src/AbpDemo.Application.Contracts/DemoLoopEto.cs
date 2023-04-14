using System;
using Volo.Abp.EventBus;

namespace AbpDemo.Controllers
{
    public class DemoLoopEto
    {
        public DateTime StartDate { get; set; }
        public int Count { get; set; }
    }
}