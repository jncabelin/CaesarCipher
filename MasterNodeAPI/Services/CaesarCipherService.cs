using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MasterNodeAPI.Services
{
	public class CaesarCipherService: ICaesarCipherService, IDisposable
	{
        private Socket _listener;
        private ILogger<CaesarCipherService> _logger;

        public CaesarCipherService(ILogger<CaesarCipherService> logger)
        {
            _logger = logger;
            // Establish the local endpoint 
            // for the socket. Dns.GetHostName
            // returns the name of the host 
            // running the application.
            IPHostEntry hostIp = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = hostIp.AddressList[0];
            int serverPort = FreeTcpPort();

            // For Debugging Purposes, write the Information of the Socket
            Console.WriteLine("LISTENING ON:" + ipAddr + ":" + serverPort);
            _logger.LogInformation("LISTENING ON:" + ipAddr + ":" + serverPort);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, serverPort);

            // Creation TCP/IP Socket using 
            // Socket Class Constructor
            _listener = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);

            // Using Bind() method we associate a
            // network address to the Server Socket
            // All client that will connect to this 
            // Server Socket must know this network
            // Address
            _listener.Bind(localEndPoint);

        }

        public void Dispose()
        {
            _listener.Close();
        }

        public async Task<string> ReadEncryptedFile(string filePath)
        {
            string lineRead = string.Empty;
            _logger.LogInformation(filePath);
            using (StreamReader file = new StreamReader(filePath, Encoding.UTF8))
            {
                lineRead = await file.ReadLineAsync();
            }

            return lineRead;
        }

        public async Task<string> SendFileContent(string encryptedText)
        {
            try
            {
                Socket clientSocket = await CreateClientAsync();
                byte[] messageSent = Encoding.UTF8.GetBytes(encryptedText);

                // Send a message to Client 
                // using Send() method
                await clientSocket.SendAsync(messageSent, SocketFlags.None);

                var response = await ReceiveMessage(clientSocket);

                // Close client Socket using the
                // Close() method. After closing,
                // we can use the closed Socket 
                // for a new Client Connection
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();

                return response;
            }

            catch (Exception e)
            {
                return e.Message;
            }
        }

        private async Task<Socket> CreateClientAsync()
        {
            // Using Listen() method we create 
            // the Client list that will want
            // to connect to Server
            _listener.Listen(10);

            Console.WriteLine("Waiting connection ... ");

            // Suspend while waiting for
            // incoming connection Using 
            // Accept() method the server 
            // will accept connection of client
            Socket clientSocket = await _listener.AcceptAsync();
            return clientSocket;
        }

        private async Task<string> ReceiveMessage(Socket clientSocket)
        {
            // Data buffer
            byte[] messageReceived = new byte[500000];

            // We receive the message using 
            // the method Receive(). This 
            // method returns number of bytes
            // received, that we'll use to 
            // convert them to string
            int byteRecv = await clientSocket.ReceiveAsync(messageReceived, SocketFlags.None);
            var response = Encoding.UTF8.GetString(messageReceived,
                                                0, byteRecv);

            return response;
        }

        private int FreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }
    }
}

