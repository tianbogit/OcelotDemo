
## 负载均衡

 	  //下游服务的地址，如果使用LoadBalancer的话这里可以填多项
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 44373
        },
        {
          "Host": "localhost",
          "Port": 44394
        }
      ],
      //负载均衡 RoundRobin(轮询)/LeastConnection(最少连接数)/CookieStickySessions(相同的Sessions或Cookie发往同一个地址)/NoLoadBalancer(不使用负载)
      "LoadBalancerOptions": {
        "Type": "LeastConnection"
      }

访问Api.GateWay，如下所示自动实现了负载均衡：



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

#### Consul基于配置文件实习那服务注册、发现

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


 