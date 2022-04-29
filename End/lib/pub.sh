#!/bin/bash
rm -rf ../Pub
dotnet publish -c debug --self-contained -o ../Pub -r osx-x64

#now delete libSystem.Native so it's forced to use shared one (change since .NET6)
rm ../Pub/libSystem.Native.dylib 