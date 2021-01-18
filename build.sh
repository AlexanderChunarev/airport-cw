#!/bin/bash
docker build --build-arg CONNECTION_STRING=$1 -t airport-api-$CIRCLE_BRANCH:$2 -f Dockerfile .