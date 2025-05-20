#!/bin/bash

# Inicializa o ambiente com o docker-compose
echo "Inicializando o ambiente local..."

docker exec superset superset db upgrade

# Cria o usuário administrador do Superset
echo "Criando usuário administrador do Superset..."
docker exec superset superset fab create-admin --username admin --firstname Superset --lastname Admin --email admin@superset.com --password admin

# Inicializa o Superset
echo "Inicializando o Superset..."
docker exec superset superset init

echo "Comandos SQL executados com sucesso!"