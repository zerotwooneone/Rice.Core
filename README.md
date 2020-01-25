# Rice-Core
Remote code execution for .Net!

# To Dev
git clone --recurse-submodules https://github.com/zerotwooneone/Rice

## To Update a submodule
git submodule update --remote Rice.Module

## To Build (from Repo root, powershell)
dotnet build .\Source\Rice.Core.Unity\Rice.Core.Unity.csproj --output .\Out

## Projects
* Rice.Core - code that is necessary for any application that wants to execute modules
* CoreIntegration - simple tests that only rely upon Rice.Core