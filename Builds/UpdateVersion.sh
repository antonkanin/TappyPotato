#!/bin/bash

# script parameters
versionfile=../TappyPotato/ProjectSettings/ProjectSettings.asset
# echo $versionfile

# get a string from the file
line=$(grep bundleVersion $versionfile)

# remove carriage return (\r) from the end of the list (since we are on Linux now, the file was saved in Windows)
unix_line=${line%$'\r'}

# save third int value from x.x.x to the $version 
version=$(echo "$unix_line" | sed -e 's/  bundleVersion: [0-9]\+.[0-9]\+.//')

# increment value in the $version by one
version=$((version+1))

# find bundleVersion string in the file and replace the version with the new value
sed -i "s/\(  bundleVersion: [0-9]\+.[0-9]\+.\)[0-9]\+\(.*\)/\1$version\2/" "$versionfile"

# testing code (do not remove)
#echo "$line" | sed "s/\(  bundleVersion: [0-9]\+.[0-9]\+.\)[0-9]\+\(.*\)/\1$version\2/"