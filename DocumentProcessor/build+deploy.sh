#!/bin/bash
rm -rf ../Packages/documentprocessor
dotnet pack

#remove old version and add new package to local repo.
nuget add bin/debug/DocumentProcessor.1.0.0.nupkg -s ../Packages/
#clear all local nuget cache.ß
dotnet nuget locals all --clear
