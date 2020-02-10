# CentOS
终端 $开头，普通用户；su 命令，输入root密码，变为#开头，管理员角色。  
> * su
产看内核版本  
> * uname -a  
更新yum包  
> * yum update
yum添加仓库  
> * yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
查看所有docker版本  
> * yum list docker-ce --showduplicates | sort -r
安装docker  
> * yum install docker-ce
启动并开机自启  
> * systemctl start docker
> * systemctl enable docker
 
