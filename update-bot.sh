#!/bin/bash

# kill current process, move current bot to backup, clear old log file
kill $(ps aux | grep '[t]ellah' | awk '{print $2}')
echo "mv ./chocobot-racing/* ./cr-backup/"
mv ./chocobot-racing/* ./cr-backup/
mv nohup.out ./cr-backup/

# copy uploaded info to standard folder and start process
echo "mv ./cr-upload/* ./chocobot-racing/"
mv ./cr-upload/* ./chocobot-racing/
chmod +x ./chocobot-racing/chocobot-racing
nohup ./chocobot-racing/chocobot-racing >> nohup.out 2>&1 &

# Give the bot some startup time and write out the current log file contents
sleep 3 
cat nohup.out