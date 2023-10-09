docker compose build

docker tag tinycrm.api-gateway.api vietanha29/tinycrm.api-gateway.api
docker tag tinycrm.people.api vietanha29/tinycrm.people.api
docker tag tinycrm.sales.api vietanha29/tinycrm.sales.api
docker tag tinycrm.iam.api vietanha29/tinycrm.iam.api

docker push vietanha29/tinycrm.api-gateway.api
docker push vietanha29/tinycrm.people.api
docker push vietanha29/tinycrm.sales.api
docker push vietanha29/tinycrm.iam.api