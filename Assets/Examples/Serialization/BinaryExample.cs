using System;
using System.Collections;
using UnityEngine;

namespace CLARTE.Serialization {
	public class BinaryExample : MonoBehaviour
	{
		[Serializable]
		public class Data : IBinarySerializable
		{
			public string text;
			public Vector3 position;
			public Color color;

			public Data()
			{
				text = string.Empty;
				position = Vector3.zero;
				color = Color.white;
			}

			public uint FromBytes(Binary serializer, Binary.Buffer buffer, uint start)
			{
				uint read = 0;

				read += serializer.FromBytes(buffer, start + read, out text);
				read += serializer.FromBytes(buffer, start + read, out position);
				read += serializer.FromBytes(buffer, start + read, out color);

				return read;
			}

			public uint ToBytes(Binary serializer, ref Binary.Buffer buffer, uint start)
			{
				uint written = 0;

				written += serializer.ToBytes(ref buffer, start + written, text);
				written += serializer.ToBytes(ref buffer, start + written, position);
				written += serializer.ToBytes(ref buffer, start + written, color);

				return written;
			}
		}

		public Data data;

		protected Binary serializer;

		protected void Start()
		{
			serializer = new Binary();

			StartCoroutine(serializer.Serialize(data, OnSerializationComplete));
		}

		protected void OnSerializationComplete(byte[] data, uint size)
		{
			byte[] msg = new byte[size];

			Array.Copy(data, msg, size);

			Debug.LogFormat("Serialized data (hexadecimal): {0}", BitConverter.ToString(msg).Replace("-", string.Empty));

			StartCoroutine(serializer.Deserialize(msg, OnDeserializationComplete));
		}

		protected void OnDeserializationComplete(object obj)
		{
			Data data = (Data) obj;

			Debug.LogFormat("Deserialized data: text = '{0}', position = '{1}', color = '{2}'", data.text, data.position, data.color);
		}
	}
}
