cd ..

docker build . -t horizon-server

docker run \
  -it \
  --rm \
  -e MIDDLEWARE_SERVER_IP=${HORIZON_MIDDLEWARE_SERVER_IP} \
  -e APP_ID=${HORIZON_APP_ID} \
  -e MIDDLEWARE_USER=${HORIZON_MIDDLEWARE_USER} \
	-e MIDDLEWARE_PASSWORD=${HORIZON_MIDDLEWARE_PASSWORD} \
  -e ASPNETCORE_ENVIRONMENT=${HORIZON_ASPNETCORE_ENVIRONMENT} \
  -p 10071:10071 \
  -p 10075:10075 \
  -p 10077:10077 \
  -p 10078:10078 \
  -p 10073:10073 \
  -p 50000-50100:50000-50100/udp \
  -p 10070:10070/udp \
  -v "${PWD}/logs":/logs \
  --name horizon-server \
  horizon-server
