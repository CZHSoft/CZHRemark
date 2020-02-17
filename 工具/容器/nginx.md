# nginx

## 安装
http://nginx.org/en/download.html  
选中合适的版本，解压到目录下。    

## 常用命令
> * start nginx--启动  
> * tasklist /fi "imagename eq nginx.exe"--查看进程  
> * nginx -s stop--快速停止
> * nginx -s quit--关闭 
> * nginx -s reload--重启

## nginx.conf配置
> * worker_processes  1; --工作进程数，一般设置为cpu核心数  

### events节点
> * worker_connections  1024;--最大连接数，一般设置为cpu*2048  

### server 节点
> *  listen 80; --监听的端口号
> * server_name www.abc.com;--访问域名  

#### location子节点 
> * proxy_pass:http://127.0.0.1:8280;--为项目的实际访问地址  
> * index index.html index.htm;--索引文件  

## 负载均衡

### upstream
upstream netsocket {  
	ip_hash;  
    server 192.168.1.102:8080 weight=1;  
    server 192.168.1.103:8080 weight=2;  
}  

例如http:  
http {  
	upstream www.abc.com {  
		ip_hash;  
		server 192.168.1.102:8080 weight=1;  
		server 192.168.1.103:8080 weight=2;  
	}   
	server {  
        listen 80;  
        server_name abc.com www.abc.com;  
        access_log   logs/abc.access.log  main;  
        root         www.abc.com;  
		...  
	}  
	...  
}  
例如tcp:  
stream {  
    upstream dotsocket {  
        server 192.168.1.189:8007;  
    }  
    server {  
        listen 12345;  
        proxy_pass dotsocket;  
    }  
}  

## vuejs部署

### vuejs打包
> * build>>utils.js>>return ExtractTextPlugin.extract({use: loaders,fallback: 'vue-style-loader', publicPath:'../../'})
> * config>>index.js>>build: { assetsPublicPath: './', }
> * npm run build

### nginx配置(nginx.conf)
http {  
    server {  
        listen       9080;  
        server_name  localhost;  
  
        location / {  
            root   D:\webroot\dist;  
            index  index.html index.htm;  
			try_files $uri $uri/ /index.html;  
        }  
         ...  
		 


