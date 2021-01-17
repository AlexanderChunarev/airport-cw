#!/bin/bash
export CONNECTION_STRING=$1
docker build -t rinoceronte -f Dockerfile .