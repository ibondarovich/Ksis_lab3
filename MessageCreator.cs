﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_lab3
{
    public class MessageCreator
    {
		public byte Type;
		public string Content;

		public MessageCreator(byte type, string content)
		{
			Type = type;
			Content = content;
		}

		public MessageCreator(byte[] data)
		{
			Type = data[0];
			Content = Encoding.UTF8.GetString(data, 1, data.Length - 1);
		}

		public byte[] GetBytes()
		{
			var paylod = Encoding.UTF8.GetBytes(Content);
			var data = new byte[paylod.Length + 1];
			data[0] = Type;
			paylod.CopyTo(data, 1);
			return data;
		}
	}
}
