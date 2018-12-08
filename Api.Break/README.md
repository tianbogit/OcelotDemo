
## 熔断

关键配置信息：

	"QoSOptions": {
	    "ExceptionsAllowedBeforeBreaking": 2, // 允许多少个异常请求
	    "DurationOfBreak": 10000, // 熔断的时间，单位为秒
	    "TimeoutValue": 3000 // 如果下游请求的处理时间超过多少则视如该请求超时
	  }

### 安装 Ocelot.Provider.Polly 


### Startup添加配置：AddPolly

	public void ConfigureServices(IServiceCollection services)
	{
	    services.AddOcelot(Configuration).AddConsul().AddPolly();
	    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
	}



### 下游服务中模拟超时情况

	 private static int requestCount = 0;
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            requestCount++;
            ////处理3次请求之后让上游服务超时
            if (requestCount > 3)
            {
                System.Threading.Thread.Sleep(5000);
            }
            return new string[] { "Api.Products.value1", "Api.Products.value2" };
        }


### 测试结果

测试实例中采用了负载均衡的机制，从结果可以看出只要是路由到超时的服务，则服务不可用！




