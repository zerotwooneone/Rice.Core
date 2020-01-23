# Rice-Core
Remote code execution for .Net!

# To Dev
git clone --recurse-submodules https://github.com/zerotwooneone/Rice

## To Update a submodule
git submodule update --remote Rice.Module

## To Build
dotnet build .\Rice.Module\Source\Rice.Module.Abstractions\Rice.Module.Abstractions.csproj --output .\Dependencies\Rice\Rice.Module.Abstractions
dotnet build .\Rice.Module\Source\Rice.Module\Rice.Module.csproj --output .\Dependencies\Rice\Rice.Module

...need to move dependencies directory down into Rice.Module after building the above...
dotnet build .\Rice.Module\Source\TestModule\TestModule.csproj --output .\Dependencies\Rice\TestModule

## Projects
* Rice.Core - code that is necessary for any application that wants to execute modules
* CoreIntegration - simple tests that only rely upon Rice.Core