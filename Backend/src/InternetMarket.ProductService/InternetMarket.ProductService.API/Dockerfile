FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Backend/src/InternetMarket.ProductService/InternetMarket.ProductService.API/InternetMarket.ProductService.API.csproj"
RUN dotnet publish "Backend/src/InternetMarket.ProductService/InternetMarket.ProductService.API/InternetMarket.ProductService.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5206
EXPOSE 5206
ENTRYPOINT ["dotnet", "InternetMarket.ProductService.API.dll"]
