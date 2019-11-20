#!/bin/sh

if  [ -f "/opt/supervisor.installed" ];
then
    supervisorctl stop utralisks
fi

rm -rf /opt/applications/utralisks/*