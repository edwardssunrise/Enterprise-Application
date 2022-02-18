## Data Transfer Object (DTO) Layer

WEB API presents database entities (entities) to the client. The client retrieves data that directly matches your database tables. However, this is not always a good idea. Sometimes you may want to change the format of the data you send to the client:

1. Remove circular references.
2. Hide certain features that clients should not display.
3. Skip some features to reduce payload size.
4. Flatten object graphics containing nested objects to make them more suitable for clients.
5. Avoid "Over-Posting" vulnerabilities.
6. Detach the Service Layer from your database layer.

You can define a DTO to accomplish this. DTO is an object that defines how data is sent over the network.