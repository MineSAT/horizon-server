# Build stage =========================================================================
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as builder

COPY . /src

#====== Build DME
WORKDIR /src/Server.Dme
RUN dotnet publish -c Release -o out

#===== Build MAS/MLS/NAT
WORKDIR /src/Server.Medius
RUN dotnet publish -c Release -o out

#===== Build MUIS
WORKDIR /src/Server.UniverseInformation
RUN dotnet publish -c Release -o out

# Copy configs
RUN cp /src/docker/dme.json /src/Server.Dme/out/dme.json
RUN cp /src/docker/medius.json /src/Server.Medius/out/medius.json
RUN cp /src/docker/muis.json /src/Server.UniverseInformation/out/muis.json

# Copy patch and plugins into right folders
RUN mkdir -p /src/Server.Medius/out/plugins/bin/patch
RUN mkdir -p /src/Server.Dme/out/plugins


RUN find /src/docker/medius_plugins -name "*" -exec cp {} /src/Server.Medius/out/plugins/ \;
RUN find /src/docker/dme_plugins -name "*" -exec cp {} /src/Server.Dme/out/plugins/ \;

RUN find /src/docker/patch -name "*.bin" -exec cp {} /src/Server.Medius/out/plugins/bin/patch \;
RUN find /src/docker/patch -maxdepth 1 -name "*.bin" -exec cp {} /src/Server.Medius/out/plugins/bin \;


# RUN mv /src/docker/medius_plugins/ /src/Server.Medius/out
# RUN mv /src/docker/dme_plugins/ /src/Server.Dme/out
#
# RUN mv /src/Server.Medius/out/medius_plugins /src/Server.Medius/out/plugins
# RUN mv /src/Server.Dme/out/dme_plugins /src/Server.Dme/out/plugins

# Run stage =========================================================================

FROM mcr.microsoft.com/dotnet/core/sdk:3.1
RUN mkdir /logs
COPY ./docker /docker
RUN chmod a+x /docker/entrypoint.sh

COPY --from=builder /src/Server.Dme/out /dme
COPY --from=builder /src/Server.Medius/out /medius
COPY --from=builder /src/Server.UniverseInformation/out /muis
COPY --from=builder /src/docker /configs

CMD "/docker/entrypoint.sh"
