using System;
using System.Net;
using UnityEngine;

namespace CLARTE.Net.Negotiation
{
	public class Logs : MonoBehaviour
	{
		public String role = "client";

		public void OnConnected(IPAddress address, Guid guid, ushort channel)
		{
			Debug.LogFormat("Connected as {0} on IP '{1}', connection '{2}' on channel '{3}'.", role, address, guid, channel);
		}

		public void OnDisconnected(IPAddress address, Guid guid, ushort channel)
		{
			Debug.LogFormat("Disconnected as {0} on IP '{1}', connection '{2}' on channel '{3}'.", role, address, guid, channel);
		}

		public void OnException(IPAddress address, Guid guid, ushort channel, Exception exception)
		{
			Debug.LogFormat("Exception thrown for {0} on IP '{1}', connection '{2}' on channel '{3}': {4} - {5}\n{6}.", role, address, guid, channel, exception.GetType(), exception.Message, exception.StackTrace);
		}

		public void OnReceived(IPAddress address, Guid guid, ushort channel, byte[] data)
		{
			Debug.LogFormat("Received data as {0} on IP '{1}', connection '{2}' on channel '{3}': {4} bytes.", role, address, guid, channel, data.Length);
		}

		public void OnProgress(IPAddress address, Guid guid, ushort channel, float progress)
		{
			Debug.LogFormat("Received progress as {0} on IP '{1}', connection '{2}' on channel '{3}': {4} %.", role, address, guid, channel, progress * 100);
		}
	}
}
