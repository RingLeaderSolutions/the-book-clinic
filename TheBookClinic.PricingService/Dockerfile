FROM microsoft/dotnet-framework:4.7.1-runtime
ARG source
WORKDIR /app
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["C:\\app\\TheBookClinic.PricingService.exe"]
