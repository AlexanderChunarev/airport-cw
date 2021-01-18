#!/bin/bash
docker build --build-arg CONNECTION_STRING=$1 -t korbendetkatopstelok/airport-cw:0.1.$CIRCLE_BUILD_NUM -f Dockerfile .