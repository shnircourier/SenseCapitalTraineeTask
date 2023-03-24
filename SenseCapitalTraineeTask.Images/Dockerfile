#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SenseCapitalTraineeTask.Images/SenseCapitalTraineeTask.Images.csproj", "SenseCapitalTraineeTask.Images/"]
COPY ["SC.Internship.Common/SC.Internship.Common/SC.Internship.Common.csproj", "SC.Internship.Common/SC.Internship.Common/"]
RUN dotnet restore "SenseCapitalTraineeTask.Images/SenseCapitalTraineeTask.Images.csproj"
COPY . .
WORKDIR "/src/SenseCapitalTraineeTask.Images"
RUN dotnet build "SenseCapitalTraineeTask.Images.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SenseCapitalTraineeTask.Images.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SenseCapitalTraineeTask.Images.dll"]