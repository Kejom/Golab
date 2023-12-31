FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /app
EXPOSE 80

COPY Golab.sln Golab.sln
COPY Services/CooService/CooService.csproj Services/CooService/CooService.csproj
COPY Services/CommentsService/CommentsService.csproj Services/CommentsService/CommentsService.csproj
COPY Services/FilesService/FilesService.csproj Services/FilesService/FilesService.csproj
COPY Services/GatewayService/GatewayService.csproj Services/GatewayService/GatewayService.csproj
COPY Services/IdentityService/IdentityService.csproj Services/IdentityService/IdentityService.csproj
COPY Services/LikeService/LikeService.csproj Services/LikeService/LikeService.csproj
COPY Services/UserProfileService/UserProfileService.csproj Services/UserProfileService/UserProfileService.csproj
COPY Services/Models/Models.csproj Services/Models/Models.csproj

RUN dotnet restore Golab.sln

COPY Services/CooService Services/CooService
COPY Services/Models Services/Models
WORKDIR /app/Services/CooService
RUN dotnet publish -c Relese -o /app/src/out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT ["dotnet", "CooService.dll"]