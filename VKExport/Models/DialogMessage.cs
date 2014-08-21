using Newtonsoft.Json;
using System.Collections.Generic;

namespace VKExport.Common
{
	public class DialogMessage
	{
		[JsonProperty("mid")]
		public int Id { get; set; }

		[JsonProperty("body")]
		public string Body { get; set; }

		[JsonProperty("uid")]
		public int UserId { get; set; }

		[JsonProperty("from_id")]
		public int FromId { get; set; }

		[JsonProperty("date")]
		public int Date { get; set; }

		[JsonProperty("read_state")]
		public int ReadState { get; set; }

		[JsonProperty("out")]
		public int Out { get; set; }

		[JsonProperty("attachment")]
		public object Attachment { get; set; }

		[JsonProperty("attachments")]
		public object Attachments { get; set; }

		[JsonProperty("fwd_messages")]
		public IEnumerable<DialogMessage> ForwardMessages { get; set; }
	}
}
