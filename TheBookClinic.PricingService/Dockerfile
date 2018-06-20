FROM microsoft/dotnet-framework:4.7.1-runtime
ARG source
#ENV SET_CONTAINER_TIMEZONE true
#ENV CONTAINER_TIMEZONE Europe/London
WORKDIR /app
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["C:\\app\\TheBookClinic.PricingService.exe"]
