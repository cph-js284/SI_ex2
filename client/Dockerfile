FROM mcr.microsoft.com/dotnet/core/sdk:2.2
WORKDIR /app

COPY . .
RUN dotnet publish -c Release -o out

ENTRYPOINT [ "dotnet", "out/client.dll" ]