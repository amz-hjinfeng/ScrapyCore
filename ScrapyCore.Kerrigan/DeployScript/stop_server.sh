#!/bin/sh

if  [ -f "/opt/supervisor.installed" ];
then
    supervisorctl stop kerrigan
fi

rm -rf /opt/applications/kerrigan/*