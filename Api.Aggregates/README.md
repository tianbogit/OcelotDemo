

## Aggregates

微服务中各个业务模块都是独立的，通过Aggregates整合结果，对外暴露一个api

	"Aggregates": [
	    {
	      "ReRouteKeys": [
	        "Products",
	        "Users"
	      ],
	      "UpstreamPathTemplate": "/"
	    }
	  ],