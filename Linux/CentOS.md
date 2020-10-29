# CentOS
终端 $开头，普通用户；su 命令，输入root密码，变为#开头，管理员角色。  

## 常规操作
> * sudo su root (切换为root)
> * kill -s 9 id (杀id进程)
> * uname -a  
> * yum -y update(更新)
> * yum install -y vim(安装vim)
> * yum install -y wget(安装wget)
 
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

## 防火墙
> * systemctl start firewalld.service(启动防火墙)
> * systemctl stop firewalld.service(关闭防火墙)
> * systemctl restart firewalld.service(重启防火墙)
> * systemctl status firewalld.service(查看防火墙状态)
> * systemctl enable firewalld.service(设置开机启动防火墙)
> * systemctl disable firewalld.service(设置开机不启动防火墙)
> * firewall-cmd --list-port
> * firewall-cmd --query-port=80/tcp
> * firewall-cmd --zone=public --add-port=80/tcp --permanent
> * firewall-cmd --zone=public --remove-port=80/tcp --permanent
> * firewall-cmd --reload

## docker
> * yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo 
> * yum list docker-ce --showduplicates | sort -r (查看所有docker版本)  
> * yum install docker-ce(安装docker) 
> * systemctl start docker(启动并开机自启)
> * systemctl enable docker

## pg
> * yum install -y https://download.postgresql.org/pub/repos/yum/reporpms/EL-6-x86_64/pgdg-redhat-repo-latest.noarch.rpm
> * yum list postgresql*
> * yum install -y postgresql12-server.x86_64
> * sudo /usr/pgsql-12/bin/postgresql-12-setup initdb (初始化)
> * sudo systemctl start postgresql-12
> * sudo systemctl enable postgresql-12
> * chkconfig postgresql-12 on

## nginx
> * yum install -y gcc
> * yum install -y pcre pcre-devel
> * yum install -y zlib zlib-devel
> * yum install -y openssl openssl-devel
> * wget -c https://nginx.org/download/nginx-1.18.0.tar.gz
> * tar -zxvf nginx-1.18.0.tar.gz -C /usr/local
> * ./configure
> * make
> * make install
> * whereis nginx
> * ./nginx (启动)
> * ./nginx -s stop
> * ./nginx -s quit
> * ./nginx -s reload
> * vim /etc/rc.local
> * /usr/local/nginx/sbin/nginx
> * chmod 755 /etc/rc.local

## rabbitmq
> * yum install -y gcc glibc-devel make ncurses-devel openssl-devel xmlto perl wget gtk2-devel binutils-devel
> * wget -c https://erlang.org/download/otp_src_23.1.tar.gz
> * tar -zxvf otp_src_23.1.tar.gz -C /usr/local
> * ./configure
> * make
> * make install
> * echo 'export PATH=$PATH:/usr/local/lib/erlang/bin' >> /etc/profile
> * source /etc/profile
> * yum install -y xz
> * wget https://github.com/rabbitmq/rabbitmq-server/releases/download/v3.8.9/rabbitmq-server-generic-unix-3.8.9.tar.xz
> * mv rabbitmq-server-generic-unix-3.8.9.tar.xz /usr/local
> * /bin/xz -d rabbitmq-server-generic-unix-3.8.9.tar.xz
> * tar -xvf rabbitmq-server-generic-unix-3.8.9.tar
> * mv /usr/local/rabbitmq_server-3.8.9  /usr/local/rabbitmq
> * echo 'export PATH=$PATH:/usr/local/rabbitmq/sbin' >> /etc/profile
> * source /etc/profile
> * mkdir /etc/rabbitmq

> * wget https://github.com/rabbitmq/rabbitmq-server/releases/download/v3.8.9/rabbitmq-server-3.8.9-1.sles11.noarch.rpm
> * rpm -ivh rabbitmq-server-3.8.9-1.sles11.noarch.rpm
> * curl -s https://packagecloud.io/install/repositories/rabbitmq/erlang/script.rpm.sh | sudo bash
> * yum install -y erlang
> * rpm --import https://packagecloud.io/rabbitmq/rabbitmq-server/gpgkey
> * rpm --import https://packagecloud.io/gpg.key
> * rpm --import https://www.rabbitmq.com/rabbitmq-release-signing-key.asc
> * curl -s https://packagecloud.io/install/repositories/rabbitmq/rabbitmq-server/script.rpm.sh | sudo bash
> * wget https://github.com/rabbitmq/rabbitmq-server/releases/download/v3.8.9/rabbitmq-server-3.8.9-1.el7.noarch.rpm
> * rpm -ivh rabbitmq-server-3.8.9-1.el7.noarch.rpm
> * vim /etc/hosts (添加 Ip  主机名称 映射)
> * service rabbitmq-server start
> * service rabbitmq-server restart
> * service rabbitmq-server stop
> * rabbitmqctl status
> * rabbitmq-plugins enable rabbitmq_management







