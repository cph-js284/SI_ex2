FROM mcr.microsoft.com/dotnet/core/sdk:2.2
WORKDIR /app
EXPOSE 6789
COPY . .
RUN dotnet publish -c Release -o out
ADD data.txt .
ENTRYPOINT [ "dotnet", "out/server.dll" ]