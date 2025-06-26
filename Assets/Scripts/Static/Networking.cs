using System.Net;
using System.Net.Sockets;
using Unity.Netcode;
using UnityEngine;

public static class Networking
{
    public static string GetLocalIP()
    {
        try
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "No IPv4 address found.";
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error getting local IP: " + ex.Message);
            return "Error";
        }
    }

    public static ClientRpcParams GetTargetClientRpcParams(ulong clientId)
    {
        var clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { clientId }
            }
        };
        return clientRpcParams;
    }
}