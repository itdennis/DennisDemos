#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-nanoserver-1903 AS build
WORKDIR /src
COPY ["Core3.0WebApp/Core3.0WebApp.csproj", "Core3.0WebApp/"]
RUN dotnet restore "Core3.0WebApp/Core3.0WebApp.csproj"
COPY . .
WORKDIR "/src/Core3.0WebApp"
RUN dotnet build "Core3.0WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Core3.0WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Core3.0WebApp.dll"]