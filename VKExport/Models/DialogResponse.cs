using Newtonsoft.Json;
using System.Collections.Generic;

namespace VKExport.Common
{
	public class DialogResponse
	{
		[JsonProperty("response")]
		public IEnumerable<DialogMessage> Response { get; set; }
	}
}
