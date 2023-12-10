# Sandbox

A docker containers to simulate requests from clients sending to an ASP.NET server for testing out other libaries within this repository.

## Components

There are altogether two important components for testing the library:

1. [Client](#client)
2. [Server](#server)

The startup configurations for both services are within `docker-compose.yml` and `Dockerfile` of each directory.

## Server

The server is a simple ASP.NET web API server. It is for testing the other libaries in the same repository.

Instead of creating another container for a database to simulate the entire system, the server will be storing all data in-memory.

## Client

The client is just a console app that takes a JSON file as an input that can simulate various different of scenarios of incoming requests:

- A single request
- Parallel `n` requests
- Sequential `n` requests

### Input File

The JSON file has the following structure:

```json
{
    "requests": [
        {
          "id": "number",
          "url": "string",
          "method": "string",
          "header": {
              // ...
          },
          "body": {
              // ...
          }
        },
        ///...
    ],
    "order": [ // sends the following requests sequentially
        "1",
        ["2", "2", "2"], // this sends the identified requests in parallel
        "3",
        { // same as above but more compact syntax
            "id": "2",
            "num": 3,
        },
        [ // same all the requests within the array in parallel
            {
                "id": "1",
                "num": 2
            },
            "2",
            "3"
        ]
    ]
}
```

### Options

These are the following possible configurations that user can set:
| Options     | Parameter     |  Env Name | Default |
| ----------- | ------------- | ------------ | ---------- |
| Max # of parallel requests      | -t \<number>   |  `SB_PARALLEL_REQ`        |4        |
| Intervals between requests    | -i \<ms>          | `SB_INTERVAL_REQ`        | 30ms        |

The following configurations can also be set withint `docker-compose.yml`.
