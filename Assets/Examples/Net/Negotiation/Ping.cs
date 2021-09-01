using System;
using System.Collections;
using System.Net;
using UnityEngine;

namespace CLARTE.Net.Negotiation
{
	[RequireComponent(typeof(Base))]
	public class Ping : MonoBehaviour
	{
		protected Base network;

		protected void Start()
		{
			network = GetComponent<Base>();
		}

		public void OnConnected(IPAddress address, Guid guid, ushort port)
		{
			if(network is Client)
			{
				StartCoroutine(Init());
			}
		}

		public void OnReceive(IPAddress address, Guid guid, ushort port, Memory.BufferPool.Buffer data)
		{
			Debug.LogFormat("Received packet from {0}. Sending it back.", network is Client ? "client" : "server");

			StartCoroutine(Reply(data));
		}

		protected IEnumerator Init()
        {
			yield return new WaitForSeconds(1f);

			Debug.LogFormat("Sending first packet from {0}.", network is Client ? "client" : "server");

			network.SendAll(0, new Memory.BufferPool.Buffer(null, new byte[1], false));
		}

		protected IEnumerator Reply(Memory.BufferPool.Buffer data)
		{
			yield return new WaitForSeconds(1f);

			network.SendAll(0, data);
		}
	}
}
