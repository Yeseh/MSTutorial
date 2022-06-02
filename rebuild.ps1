docker compose down
docker compose build
kubectl rollout restart deployment commandservice-deployment
kubectl rollout restart deployment platformservice-deployment