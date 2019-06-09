FROM microsoft/aspnetcore
RUN mkdir -p /app
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "mvccoresb.dll"]

