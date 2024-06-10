# CaesarCipher
## About The Project
This is a simple demo to decrypt Caesar Cipher Encrypted csv file. The are two Docker containers in this process. The master node is talking to 2 client nodes using sockets. 
1. The MASTER node should do the following:
- [x] Read the text files from the file system
- [x] Output the calculation result to the screen (calculated by the client node)
2. The CLIENT nodes should do the following:
- [x] Receive the contents of the file in a string format from the master node
- [x] Decode the text into a CSV file using the encryption method discussed in the next section
- [x] Calculate the maximum value of each column in the file Return the result in a JSON form

## How to run the project
Swagger UI: `<localhost:port>/swagger/index.html`

**With Docker:**
This will create a container with the application
Make sure that you have Docker installed on your machine & running.
1. Clone the repository
2. Update MasterAPI  DockerFile, WEB_API_Port
3. Update launchSettings applicationUrl to WEB_API_Port
4. In each respective directory, build the container images using the commands:
- `docker build . -t <image_name>`
5. After a successful build, run the two containers using the commands:
- MASTER Node: `docker run --name <server_container_name> --rm -d  -p <tcpPort>:<SOCKET_PORT> <webApiPort>:<WEBAPI_API_Port> <master_img_name>`
- CLIENT Node: `docker run --name <client_container_name> --rm -it -P <client_img_name>`


### Technologies & Features
- [x] ASP.NET Core 6 WebApi
- [x] XUnit with Moq Testing Framework
- [x] REST Standards
- [x] Swagger UI
- [x] Docker 

### Endpoints
**Decrypt File**
- `/decryptFile?fileName=<txtFileName>`

## AWS Implementation 
### AWS Services
- [x] Elastic Load Balancer
- [x] AWS Batch
- [x] Elastic Container Registry (ECR)
- [x] AWS Lambda
- [x] Virtual Private Cloud (VPC)
- [x] Identity and Access Management (IAM)


