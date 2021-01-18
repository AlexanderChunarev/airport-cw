#!/bin/bash
TAG=$CIRCLE_BRANCH-$CIRCLE_BUILD_NUM
docker build --build-arg CONNECTION_STRING=$1 -t airport-cw-demo:$TAG -f Dockerfile .