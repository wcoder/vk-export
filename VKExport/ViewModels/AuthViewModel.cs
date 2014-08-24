using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKExport.Common;
using VkNet;
using VkNet.Enums.Filters;

namespace VKExport.ViewModels
{
	public class AuthViewModel : ViewModelBase
	{

		public void Authorize(string login, string password, Action<VkAuthEventArgs> callback)
		{
			var vk = new VkApi();
			vk.Authorize(Configs.AppId, login, password, Settings.Messages);
			callback.Invoke(new VkAuthEventArgs { Data = vk });
		}
	}
}
