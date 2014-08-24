using System;
using VkNet;

namespace VKExport.Common
{
	public class VkAuthEventArgs : EventArgs
	{
		public VkApi Data { get; set; }
	}
}
