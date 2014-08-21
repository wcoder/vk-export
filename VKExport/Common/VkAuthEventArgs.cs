using System;

namespace VKExport.Common
{
	public class VkAuthEventArgs : EventArgs
	{
		public AccessTokenResponse Data { get; set; }
	}
}
