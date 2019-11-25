$x = Split-Path -Parent $MyInvocation.MyCommand.Definition

cd $x
dotnet publish  .\ScrapyCore.HeartOfSwarm\
cd ScrapyCore.HeartOfSwarm\bin\Debug\netcoreapp2.1\publish
start dotnet ScrapyCore.HeartOfSwarm.dll

cd $x