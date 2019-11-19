#!/bin/sh



if  [ ! -f "/opt/supervisor.installed" ];
then
easy_install supervisor
mkdir -p /etc/supervisor/conf.d/
cat << EOF > /etc/supervisord.conf

[unix_http_server]
file=/tmp/supervisor.sock   ; the path to the socket file

[supervisord]
logfile=/tmp/supervisord.log ; main log file; default $CWD/supervisord.log
logfile_maxbytes=50MB        ; max main logfile bytes b4 rotation; default 50MB
logfile_backups=10           ; # of main logfile backups; 0 means none, default 10
loglevel=info                ; log level; default info; others: debug,warn,trace
pidfile=/tmp/supervisord.pid ; supervisord pidfile; default supervisord.pid
nodaemon=false               ; start in foreground if true; default false
minfds=1024                  ; min. avail startup file descriptors; default 1024
minprocs=200                 ; min. avail process descriptors;default 200


[rpcinterface:supervisor]
supervisor.rpcinterface_factory = supervisor.rpcinterface:make_main_rpcinterface

[supervisorctl]
serverurl=unix:///tmp/supervisor.sock ; use a unix:// URL  for a unix socket

[include]
files = /etc/supervisor/conf.d/*.conf

EOF
    
cat << EOF > /etc/supervisor/conf.d/heartofswarm.conf
[program:heartofswarm]
command=dotnet ScrapyCore.HeartOfSwarm.dll
directory=/opt/applications/heartofswarm
environment=ASPNETCORE__ENVIRONMENT=Production
user=root
stopsignal=INT
autostart=true
autorestart=true
startsecs=1
stderr_logfile=/opt/applications/heartofswarm/HeartOfSwarm.err.log
stdout_logfile=/opt/applications/heartofswarm/HeartOfSwarm.out.log
EOF

chmod +x /usr/bin/supervisord
chmod +x /usr/bin/supervisorctl
chmod +x /etc/supervisord.conf

supervisord -c  /etc/supervisord.conf

echo installed >> /opt/supervisor.installed
else

echo Installed.

fi

