#!/bin/bash
rm -rf ../Pub
dotnet publish -c debug --self-contained -o ../Pub -r osx-x64
