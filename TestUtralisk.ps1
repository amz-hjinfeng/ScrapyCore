$x = Split-Path -Parent $MyInvocation.MyCommand.Definition

cd $x
cd ScrapyCore.Hydralisk\bin\Debug\netcoreapp2.1
start dotnet ScrapyCore.Hydralisk.dll

cd $x
cd ScrapyCore.Kerrigan\bin\Debug\netcoreapp2.1
start dotnet ScrapyCore.Kerrigan.dll

cd $x


