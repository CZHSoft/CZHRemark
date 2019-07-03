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


