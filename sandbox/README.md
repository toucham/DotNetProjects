# Sandbox

A docker containers to simulate requests from clients sending to an ASP.NET server for testing out other libaries within this repository. There are altogether two important components for testing the library:

The startup configurations for both services are within `docker-compose.yml` and `Dockerfile` of each directory.

## Server

The server is a simple ASP.NET web API server. It is for testing the other libaries in the same repository.

Instead of creating another container for a database to simulate the entire system, the server will be storing all data in-memory.

## Client

Please read it in [README.md](./SandboxClient/README.md)
