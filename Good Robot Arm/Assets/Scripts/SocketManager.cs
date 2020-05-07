using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public class SocketManager : MonoBehaviour
{
    Socket sender;
    
    // Start is called before the first frame update
    void Start()
    {
        StartClient();
    }

    public void SendAngles()
    {
        float num = UnityEngine.Random.value * 10000;
        string msgString = AngleController.angleMessage;
        byte[] msg = Encoding.ASCII.GetBytes(msgString);
        sender.Send(msg);
    }

    public void StartClient()
    {
        // Data buffer for incoming data.  
        byte[] bytes = new byte[1024];

        // Connect to a remote device.  
        try
        {
            // Establish the remote endpoint for the socket.  
            // This example uses port 11000 on the local computer.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 6000);
            //new IPEndPoint()

            print(ipAddress);

            // Create a TCP/IP  socket.  
            //Socket t = new Socket()
            sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.  
            try
            {
                sender.Connect(remoteEP);

                Debug.Log("Socket connected to {0}" +
                    sender.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.  
                byte[] msg = Encoding.ASCII.GetBytes("This is a test");

                // Send the data through the socket.  
                int bytesSent = sender.Send(msg);

                // Receive the response from the remote device.  
                int bytesRec = sender.Receive(bytes);
                Debug.Log("Echoed test = {0}" +
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));

                // Release the socket.  
                //sender.Shutdown(SocketShutdown.Both);
                //sender.Close();

            }
            catch (ArgumentNullException ane)
            {
                Debug.Log("ArgumentNullException : {0}" + ane.ToString());
            }
            catch (SocketException se)
            {
                Debug.Log("SocketException : {0}" + se.ToString());
            }
            catch (Exception e)
            {
                Debug.Log("Unexpected exception : {0}" + e.ToString());
            }

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void OnApplicationQuit()
    {
        sender.Shutdown(SocketShutdown.Both);
        sender.Close();
    }
}
