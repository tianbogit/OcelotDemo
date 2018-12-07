## 服务注册、发现、健康检查

Consul支持配置文件和Api两种方式服务注册、服务发现

#### window下使用
 
window下直接启动 consul.exe 服务

consul目录创建如下：

- conf
- tmp
- ui

启动agent:

consul agent -server -bootstrap-expect 1 -data-dir F:\tool\consul\tmp -advertise 192.168.103.124 -client 192.168.103.124 -ui-dir F:\tool\consul\ui

- config-file 指定配置启动

#### Consul基于配置文件实习那服务注册发现，健康检查

	{
	  "encrypt": "7TnJPB4lKtjEcCWWjN6jSA==",
	  "services": [
	    {
	      "id": "Users",
	      "name": "ApiService",
	      "tags": [ "Api.Users" ],
	      "address": "localhost",
	      "port": 44394,
	      "checks": [
	        {
	          "id": "Users_Check",
	          "name": "Users_Check",
	          "http": "https://localhost:44394/health",
	          "interval": "10s",
	          "tls_skip_verify": false,
	          "method": "GET",
	          "timeout": "1s"
	        }
	      ]
	    },
	    {
	      "id": "Products",
	      "name": "ApiService",
	      "tags": [ "Api.Products" ],
	      "address": "localhost",
	      "port": 44373,
	      "checks": [
	        {
	          "id": "Products_Check",
	          "name": "Products_Check",
	          "http": "https://localhost:44373/health",
	          "interval": "10s",
	          "tls_skip_verify": false,
	          "method": "GET",
	          "timeout": "1s"
	        }
	      ]
	    }
	  ]
	} 


 
## 通过Ocelot+Consul发现服务并实现负载均衡

### 安装 install-package Ocelot 和 Ocelot.Provider.Consul


### 修改Startup.cs

	public void ConfigureServices(IServiceCollection services)
	{
        //注册服务
	    services.AddOcelot(Configuration).AddConsul();
	    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
	}
	
	 public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
		// 加入管道
        app.UseOcelot().Wait();
        app.UseMvc();
    }

### 配置ocelot.json

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
	      "UseServiceDiscovery": true
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
	    // 告诉客户端网关对外暴露的域名
	    "BaseUrl": "https://localhost:44366/"
	  }
	}



访问Api.GateWay（https://localhost:44366/api/values），自动实现了负载均衡。