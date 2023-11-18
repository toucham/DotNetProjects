# Examples
This directory contains a mock environment for testing the library.

Directories:
- `client` contains the terminal client for sending mock requests to the server to initiate 2P commit
- `server` contains the web server that have two phases commit implemented

All the services are start up using docker compose. Check the `docker-compose.yml` to see the configurations.

## Server
It is implemented using ASP.NET with simple POST and GET controllers for testing the two phases commit.

## Client
It is implemented using .NET that allow user to send simple POST and GET requests to the servers to test the two phase commit implementation.


