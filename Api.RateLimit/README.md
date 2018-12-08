## 配置

	{
	  "ReRoutes": [
	    {
	      "DownstreamPathTemplate": "/api/values", // 下游游请求模板
	      "DownstreamScheme": "https", //下游服务 schema
	      "UpstreamPathTemplate": "/api/{controller}", // 上游请求模板
	      "UpstreamHttpMethod": [ "Get", "Post" ], // 上游请求http方法
	      //负载均衡 RoundRobin(轮询)/LeastConnection(最少连接数)/CookieStickySessions(相同的Sessions或Cookie发往同一个地址)/NoLoadBalancer(不使用负载)
	      "LoadBalancerOptions": {
	        "Type": "RoundRobin"
	      },
	      //服务注册、发现
	      "ServiceName": "ApiService",
	      "UseServiceDiscovery": true,
	      "RateLimitOptions": {
	        "ClientWhitelist": [ "admin" ], // 白名单
	        "EnableRateLimiting": true, // 是否启用限流
	        "Period": "1m", // 统计时间段：1s, 5m, 1h, 1d
	        "PeriodTimespan": 15, // 多少秒之后客户端可以重试
	        "Limit": 5 // 在统计时间段内允许的最大请求数量
	      }
	    }
	  ],
	  "GlobalConfiguration": {
	    //Consul服务发现的地址和端口"
	    "ServiceDiscoveryProvider": {
	      "Host": "localhost",
	      "Port": 8500,
	      "Type": "Consul"
	      //"Type": "PollConsul",#从consul中拉取最新的services，而不是每次请求都去consul中请求
	      //"PollingInterval": 100 #频率（ms）
	    },
	    "RateLimitOptions": {
	      "DisableRateLimitHeaders": false, // Http头  X-Rate-Limit 和 Retry-After 是否禁用
	      "QuotaExceededMessage": "Too many requests, are you OK?", // 当请求过载被截断时返回的消息
	      "HttpStatusCode": 999, // 当请求过载被截断时返回的http status
	      "ClientIdHeader": "client_id" // 用来识别客户端的请求头，默认是 ClientId
	    },
	    // 告诉客户端网关对外暴露的域名
	    "BaseUrl": "https://localhost:44366/"
	  }
	}



可参考：

https://www.cnblogs.com/edisonchou/archive/2018/06/17/9180785.html