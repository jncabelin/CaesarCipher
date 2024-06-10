using System.Net;
using System.Net.Sockets;
using System.Text;
using ClientNode;
using Newtonsoft.Json;

while (true)
{
    Console.WriteLine("CLIENT ACTIVATED...");
    try
    {

        // Establish the remote endpoint 
        // for the socket. This example 
        // uses port 5001 on the local 
        // computer.
        Console.WriteLine("PLEASE ENTER SERVER IP ADD:");
        var serverIpAdd = Console.ReadLine();
        Console.WriteLine("PLEASE ENTER SERVER PORT:");
        var serverPort = int.Parse(Console.ReadLine());
        IPAddress ipAddr = IPAddress.Parse(serverIpAdd);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddr, serverPort);

        // Creation TCP/IP Socket using 
        // Socket Class Constructor
        using Socket client = new(
        localEndPoint.AddressFamily,
        SocketType.Stream,
        ProtocolType.Tcp);

        try
        {

            // Connect Socket to the remote 
            // endpoint using method Connect()
            //while (true) ;
            await client.ConnectAsync(localEndPoint);

            // We print EndPoint information 
            // that we are connected
            Console.WriteLine("Socket connected to -> {0} ",
                            client.RemoteEndPoint.ToString());

            /*
            // Creation of message that
            // we will send to Server
            byte[] messageSent = Encoding.ASCII.GetBytes("Test Client<EOF>");
            int byteSent = sender.Send(messageSent);
            */
            // Data buffer
            byte[] messageReceived = new byte[500000];

            // We receive the message using 
            // the method Receive(). This 
            // method returns number of bytes
            // received, that we'll use to 
            // convert them to string
            int byteRecv = await client.ReceiveAsync(messageReceived, SocketFlags.None);
            var message = Encoding.UTF8.GetString(messageReceived,
                                                0, byteRecv);

            // Decrypt
            string decodedString = await CaesarCipherDecryptionService.DecodeCaesarCypher(message);

            // Send Message
            var jsonDict = await CaesarCipherDecryptionService.GetMaxValues(decodedString);
            var json = JsonConvert.SerializeObject(jsonDict);
            Console.WriteLine("Message Sent:" + json);

            // Creation of message that
            // we will send to Server
            byte[] messageSent = Encoding.UTF8.GetBytes(json);
            int byteSent = await client.SendAsync(messageSent, SocketFlags.None);

            // Close Socket using 
            // the method Close()
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        // Manage of Socket's Exceptions
        catch (ArgumentNullException ane)
        {

            Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
        }

        catch (SocketException se)
        {

            Console.WriteLine("SocketException : {0}", se.ToString());
        }

        catch (Exception e)
        {
            Console.WriteLine("Unexpected exception : {0}", e.ToString());
        }
    }

    catch (Exception e)
    {

        Console.WriteLine(e.ToString());
    }
}