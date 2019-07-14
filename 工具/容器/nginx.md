# nginx

## 安装
http://nginx.org/en/download.html  
选中合适的版本，解压到目录下。    

## 常用命令
--启动  
> * start nginx
--查看进程  
> * tasklist /fi "imagename eq nginx.exe"
--快速停止   
> * nginx -s stop
--关闭  
> * nginx -s quit
--重启  
> * nginx -s reload

## nginx.conf配置
--工作进程数，一般设置为cpu核心数  
> * worker_processes  1;

### events节点
--最大连接数，一般设置为cpu*2048  
> * worker_connections  1024;

### server 节点
--监听的端口号  
> *  listen 80; 
--访问域名  
> * server_name www.abc.com;

#### location子节点 
--为项目的实际访问地址  
> * proxy_pass:http://127.0.0.1:8280;
--索引文件  
> * index index.html index.htm;

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