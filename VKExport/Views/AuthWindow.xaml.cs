using System;
using System.Windows;
using VKExport.Common;
using VkNet;
using VkNet.Enums.Filters;

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

			this.DataContext = App.Locator.AuthWindow;
		}

		private void AuthButton_Click(object sender, RoutedEventArgs arg)
		{
			try
			{
				App.Locator.AuthWindow.Authorize(Login.Text, Password.Password, OnAuthorized);

				this.Close();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}
	}
}
