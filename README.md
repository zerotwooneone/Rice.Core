# Rice-Core
Remote code execution for .Net!
This is a **Ridiculously** insecure way to execute code remotely. This is only meant for educational purposes. If you think you can simply make this secure enough to use, you are wrong. Period.

# To Dev
git clone --recurse-submodules https://github.com/zerotwooneone/Rice

## To Update a submodule
git submodule update --remote Rice.Module

## To Build (from Repo root, powershell)
dotnet build .\Source\Rice.Core.Unity\Rice.Core.Unity.csproj --output .\Out
dotnet build .\Source\Rice.Core.Serialize.ProtoBuf\Rice.Core.Serialize.ProtoBuf.csproj --output .\Out

## Projects
* Rice.Core - code that is necessary for any application that wants to execute modules
* CoreIntegration - simple tests that only rely upon Rice.Core