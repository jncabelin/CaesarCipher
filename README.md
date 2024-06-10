# CaesarCipher
## About The Project
This is a simple demo to decrypt Caesar Cipher Encrypted csv file. The are two Docker containers in this process. The master node is talking to 2 client nodes using sockets. 
1. The MASTER node should do the following:
- [x] Read the text files from the file system
- [x] Output the calculation result to the screen (calculated by the client node)
2. The CLIENT nodes should do the following:
- [x] Receive the contents of the file in a string format from the master node
- [x] Decode the text into a CSV file using the Caesar Cipher encryption method.
- [x] Calculate the maximum value of each column in the file Return the result in a JSON form.

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
- CLIENT Node: `docker run --name <client_container_name> --rm -it -P <client_img_name>`. Supply the corresponding IP Address and Port number from the Master Node.


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
!(./docs/CaesarCipher-AWS-Diagram.png)
### AWS Services
- [x] Elastic Load Balancer
-The load balancer distributes incoming application traffic across multiple targets, such as EC2 instances, in multiple Availability Zones. This allows the isolation between the public API endpoints and the private network for socket communication.
- [x] Elastic Compute Cloud(Amazon EC2)
-Amazon Elastic Compute Cloud (Amazon EC2) provides scalable computing capacity in the AWS Cloud. The containerized Master and Clients will be deployed in the cloud using this service.
- [x] AWS Batch
-AWS Batch allows applications to efficiently run hundreds of thousands of batch computing jobs while optimizing compute resources. New Client instances will be created based on the demand for decryption. For every request to the Master Node, a new tcp connection is established thus new client instances will be needed. The master node will queue a batch job by using the BatchClient API from the AWS SDK for .NET. 
-Batch Jobs are divided into 3 tasks:
1. Computation for the Offset
2. Decoding the text file to CSV
3. Finding the maximum values per column and saving to JSON
- [x] Elastic Container Registry (ECR)
-ECR stores, manages, deploys, and shares Docker containers that will be used by AWS Batch.
- [x] Virtual Private Cloud (VPC)
-EC2 instances are deployed in a logically isolated defined virtual network. This virtual network closely resembles a traditional network.  
- [x] Identity and Access Management (IAM)


