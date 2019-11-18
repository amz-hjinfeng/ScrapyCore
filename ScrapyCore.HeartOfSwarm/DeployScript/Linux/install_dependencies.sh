#!/bin/sh

yum install supervisor -y

cat << EOF >  /ect/supervisor/conf.d/heartofswarm.conf
[program:heartofswarm]
command=dotnet ScrapyCore.HeartOfSwarm.dll  #要执行的命令
directory=/opt/applications/heartofswarm #命令执行的目录
environment=ASPNETCORE__ENVIRONMENT=Production #环境变量
user=root  #进程执行的用户身份
stopsignal=INT
autostart=false #是否自动启动
autorestart=true #是否自动重启
startsecs=1 #自动重启间隔
stderr_logfile=/var/log/HeartOfSwarm.err.log #标准错误日志
stdout_logfile=/var/log/HeartOfSwarm.out.log #标准输出日志
EOF

supervisorctl shutdown && supervisord -c /etc/supervisor/supervisord.conf