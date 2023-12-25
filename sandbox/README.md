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

### Mock Requests Format

There are two types of input files that are required to simulate sending http requests to your webserver:

1. Requests JSON file
2. Timeline JSON file

The request file defines the request that are going to be sent to the http server. It has the following file structure: 

```json
 [
     {
       "id": "string",
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
 ]
```

The second JSON file that is required is the timeline file that defines the events that are happening during the simulation. It defines which and when the requests are being sent over to the designated web server.It has the following structure:

```json
 [ // sends the following requests sequentially
     "1", // sends one request
     ["2", "2", "2"], // this sends the identified requests in parallel
     { // same as above but more compact syntax
         "id": "2",
         "amount": 3,
     },
     [ // same all the requests within the array in parallel
         {
             "id": "1",
             "amount": 2
         },
         "2",
         "3"
     ]
 ]
```

These are the following rules that must be followed within the events input JSON file:

1. There are altogether three types of elements: `string`, `array`, and an `object`.
2. The `string` represents the ID of the request defined in the request input JSON file.
3. Each element in the outer array represents event and is executed sequentially in order. So in the example above, the request with id `"1"` will be sent first.
4. A nested `array` represents a parallel event such that the requests within that array will be sent to the web server in parallel.
5. The `object` represents one request that will be sent in parallel with the defined amount of that request that will be sent.

### Options

These are the following possible configurations that user can set:
| Options     | Parameter     |  Env Name | Default |
| ----------- | ------------- | ------------ | ---------- |
| Max # of parallel requests      | -t \<number>   |  `SB_PARALLEL_REQ`        |4        |
| Intervals between requests    | -i \<ms>          | `SB_INTERVAL_REQ`        | 30ms        |

The following configurations can also be set withint `docker-compose.yml`.
