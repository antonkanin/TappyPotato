#!/bin/bash

# configuration
versionfile=TappyPotato/ProjectSettings/ProjectSettings.asset
source_branch=QA
destination_branch=builds
version="x.x.x"

update_version_file()
{
  # get a string from the file
  line=$(grep bundleVersion $versionfile)
  
  # remove carriage return (\r) from the end of the list (since we are on Linux now, the file was saved in Windows)
  unix_line=${line%$'\r'}

  # save third int value from x.x.x to the $version 
  full_version=$(echo "$unix_line" | sed -e 's/  bundleVersion: \([0-9]\+.[0-9].\)[0-9]\+/\1/')

  echo "full version: " "${full_version}"

  minor_version=$(echo "$unix_line" | sed -e 's/  bundleVersion: [0-9]\+.[0-9]\+.//')

  echo "minor version: " "${minor_version}"

  # increment value in the $version by one
  minor_version=$((minor_version+1))

  # find bundleVersion string in the file and replace the version with the new value
  sed -i "s/\(  bundleVersion: [0-9]\+.[0-9]\+.\)[0-9]\+\(.*\)/\1$minor_version\2/" "$versionfile"

  # testing code (do not remove)
  echo "$line" | sed "s/\(  bundleVersion: [0-9]\+.[0-9]\+.\)[0-9]\+\(.*\)/\1$minor_version\2/"

  version=$full_version$minor_version
}

git_initial_setup()
{
  git config --global user.email "travis@travis-ci.org"
  git config --global user.name "Travis CI"

  git checkout -f ${destination_branch}
  git merge ${source_branch}
}

git_file_upload()
{
  git add TappyPotato/ProjectSettings/ProjectSettings.asset
  git commit --message "Travis Version: $version"
 
  # adding new remove remote with the token
  git remote add origin-token https://${GT_TOKEN}@github.com/antonkanin/TappyPotato.git > /dev/null 2>&1
  git push --quiet --set-upstream origin-token ${destination_branch}
}

git_initial_setup
update_version_file
git_file_upload