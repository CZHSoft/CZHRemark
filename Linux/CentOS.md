# CentOS
终端 $开头，普通用户；su 命令，输入root密码，变为#开头，管理员角色。  

## 常规操作
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

## 设置SSH
设置SSH允许远程登陆。  
> * 1.终端使用root账户。  
> * 2.yum install openssh-server  
> * 3.vim /etc/ssh/sshd_config  
Port 22  
ListenAddress 0.0.0.0  
ListenAddress ::  
PermitRootLogin yes  
PasswordAuthentication yes  
> * 4.sudo service sshd start
> * 5.ps -e | grep sshd
 
