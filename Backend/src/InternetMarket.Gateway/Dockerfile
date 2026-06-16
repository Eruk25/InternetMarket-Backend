FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/InternetMarket.Gateway/InternetMarket.Gateway.csproj"
RUN dotnet publish "src/InternetMarket.Gateway/InternetMarket.Gateway.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5146
EXPOSE 5146
ENTRYPOINT ["dotnet", "InternetMarket.Gateway.dll"]
