#!/bin/sh

if  [ -f "/opt/supervisor.installed" ];
then
    supervisorctl stop heartofswarm
fi

rm -rf /opt/applications/heartofswarm/*