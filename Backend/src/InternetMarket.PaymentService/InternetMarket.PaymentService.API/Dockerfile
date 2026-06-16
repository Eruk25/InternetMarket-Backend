FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/InternetMarket.PaymentService/InternetMarket.PaymentService.API/InternetMarket.PaymentService.API.csproj"
RUN dotnet publish "src/InternetMarket.PaymentService/InternetMarket.PaymentService.API/InternetMarket.PaymentService.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5235
EXPOSE 5235
ENTRYPOINT ["dotnet", "InternetMarket.PaymentService.API.dll"]
