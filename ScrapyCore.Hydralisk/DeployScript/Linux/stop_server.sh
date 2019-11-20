#!/bin/sh

if  [ -f "/opt/supervisor.installed" ];
then
    supervisorctl stop hydralisk
fi

rm -rf /opt/applications/hydralisk/*