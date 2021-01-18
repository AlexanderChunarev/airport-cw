#!/bin/bash
TAG=0.1.$CIRCLE_BUILD_NUM
docker build --build-arg CONNECTION_STRING=$1 -t airport-cw-api:$TAG -f Dockerfile . 