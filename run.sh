docker run --detach --name nginx-proxy --publish 80:80 --publish 443:443 --volume certs:/etc/nginx/certs --volume vhost:/etc/nginx/vhost.d --volume html:/usr/share/nginx/html --volume /var/run/docker.sock:/tmp/docker.sock:ro nginxproxy/nginx-proxy

docker run --detach --name nginx-proxy-acme --volumes-from nginx-proxy --volume /var/run/docker.sock:/var/run/docker.sock:ro --volume acme:/etc/acme.sh --env "DEFAULT_EMAIL=jonhenry1324@gmail.com" nginxproxy/acme-companion
	
docker run --detach --name l4d2_mh --env-file ./env.list --env "VIRTUAL_HOST=kahdeg.ddns.net" --env "LETSENCRYPT_HOST=kahdeg.ddns.net" l4d2_match_history_blazor_api:latest