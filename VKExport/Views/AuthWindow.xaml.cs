using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VKExport.Common;

namespace VKExport
{
	public partial class AuthWindow : Window
	{
		#region events

		public event EventHandler<VkAuthEventArgs> Authorized;

		private void OnAuthorized(VkAuthEventArgs e)
		{
			EventHandler<VkAuthEventArgs> handler = Authorized;
			if (handler != null) handler(this, e);
		}

		#endregion

		public AuthWindow()
		{
			InitializeComponent();

			LoadAuthPage();
		}

		private void Browser_LoadCompleted(object sender, NavigationEventArgs e)
		{
			if (e.Uri.ToString().Contains(Configs.RedirectUrl))
			{
				var response = VkApiHelpers.ParseAccessToken(e.Uri.Fragment);

				OnAuthorized(new VkAuthEventArgs { Data = response });

				this.Title = "Авторизация прошла успешно!";
				this.Close();
			}
			else if (e.Uri.ToString().Contains("error"))
			{
				LoadAuthPage();
			}
		}

		private void LoadAuthPage()
		{
			this.Title = "Авторизация Вконтакте...";

			var authUrl = string.Format(Configs.AuthUrl,
										Configs.ClientId,
										Configs.Scope,
										Configs.RedirectUrl);

			Browser.Navigate(new Uri(authUrl));
		}
	}
}
