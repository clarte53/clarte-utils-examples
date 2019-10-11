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

			if(network is Client)
			{
				((Client) network).Connect();
			}
		}

		public void OnConnected(IPAddress address, Guid guid, ushort port)
		{
			if(network is Client)
			{
				network.SendAll(0, new byte[1]);
			}
		}

		public void OnReceive(IPAddress address, Guid guid, ushort port, byte[] data)
		{
			StartCoroutine(Reply(data));
		}

		protected IEnumerator Reply(byte[] data)
		{
			yield return new WaitForSeconds(1f);

			network.SendAll(0, data);
		}
	}
}
