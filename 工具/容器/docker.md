# docker

## 安装
http://mirrors.aliyun.com/docker-toolbox/windows/docker-toolbox/  
选中合适的版本，默认安装。    

## 改变默认docker machine
方法1.Start->Git->Git Bash：输入 notepad .bash_profile ,内容 export MACHINE_STORAGE_PATH='D:\docker' ，并在D盘新建docker文件夹。  
方法2.新添加环境变量 MACHINE_STORAGE_PATH ，值就是docker虚拟机路径。  

### 初始化虚拟机
> * docker-machine create --driver virtualbox default2

### 查询虚拟机状态
> * docker-machine ls

### 检查default2环境
> * docker-machine env default2

### 开启/关闭虚拟机
> * docker-machine start default
> * docker-machine stop default

## docker命令
> * docker version
> * docker info
> * docker ps  -a 
> * docker rm
> * docker run -d -it 
> * docker images
> * docker rmi
> * docker pull 
> * docker stop
> * docker start

## 配置实例(netcore2.2)
> * 新建core项目
> * 发布项目，选择目标平台
> * 发布目录下，新建Dockerfile.txt文件
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base  
WORKDIR /app  
COPY ./ ./  
EXPOSE 5000  
ENTRYPOINT ["dotnet", "dockertest.dll"]  
> * 发布目录下  docker build -t dockertest .
> * 测试 docker run -it -p 5000:80 --name aspnetcore dockertest 

## 配置实例(redis集群)
--新建redis镜像  
> * docker pull redis:3.2.1
--查看redis版本  
> * docker inspect redis:latest | grep -i version
--取出同版本的redis.conf文件  
> * 修改redis.conf，以支持分布式
bind 0.0.0.0
daemonize no
protected-mode no
cluster-enabled yes  
cluster-config-file nodes-6379.conf  
cluster-node-timeout 5000 
appendonly yes   
> * 新建Dockerfile.txt文件
FROM redis:3.2.1  
EXPOSE 6379  
ADD redis.conf /redis.conf  
ENTRYPOINT [ "redis-server", "/redis.conf" ]  
--创建redis-cluster镜像  
> * docker build -t redis-cluster -f ./Dockerfile.txt .
> * route add 172.18.0.0 mask 255.255.0.0 192.168.99.100
--新建桥接网络  
> * docker network create --subnet=172.18.0.0/16  redisnet
--新建ruby镜像  
> * docker pull ruby
> * 新建Dockerfile.txt文件
FROM ruby  
ADD redis-trib.rb /redis-trib.rb  
--创建ruby-redis镜像
> * docker build -t ruby-redis -f ./Dockerfile.txt .
--新建至少六个redis节点的容器  
> * docker run -d --net redisnet --ip 172.18.0.38 -p 8001:6379 --name redis1 redis-cluster
> * docker run -d --net redisnet --ip 172.18.0.39 -p 8002:6379 --name redis2 redis-cluster
> * docker run -d --net redisnet --ip 172.18.0.40 -p 8003:6379 --name redis3 redis-cluster 
> * docker run -d --net redisnet --ip 172.18.0.41 -p 8004:6379 --name redis4 redis-cluster 
> * docker run -d --net redisnet --ip 172.18.0.42 -p 8005:6379 --name redis5 redis-cluster 
> * docker run -d --net redisnet --ip 172.18.0.43 -p 8006:6379 --name redis6 redis-cluster 
--新建一个ruby容器  
> * docker run --net redisnet --ip 172.18.0.50 --name ruby1 -i -d ruby-redis
> * docker exec -it ruby1 /bin/bash
> * gem install redis –v 3.2.1
--redis-trib.rb 创建集群  
> * ./redis-trib.rb create --replicas 1 172.18.0.38:6379 172.18.0.39:6379 172.18.0.40:6379 172.18.0.41:6379 172.18.0.42:6379 172.18.0.43:6379
--测试  
--进入redis1容器  
> * docker exec -it redis1 /bin/bash
--运行redis终端  
> * redis-cli -c
> * set r1 "ok"
--进入redis2容器  
> * docker exec -it redis2 /bin/bash
--运行redis终端   
> * redis-cli -c
> * get r1
--打开windows redis client   
> * ip 192.168.99.100 port 8001
--查看r1数据   





