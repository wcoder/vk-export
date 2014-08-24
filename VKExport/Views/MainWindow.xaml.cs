using System;
using System.ComponentModel;
using System.Windows;
using VkNet;
using VkNet.Enums.Filters;

namespace VKExport
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			this.DataContext = App.Locator.MainWindow;

			OpenAuthorizationWindow();
		}

		private void OpenAuthorizationWindow()
		{
			var auth = new AuthWindow();
			auth.Authorized += App.Locator.MainWindow.OnAuthorized;
			auth.Closing += OnClosing;
			auth.ShowDialog();			
		}

		private void OnClosing(object sender, CancelEventArgs e)
		{
			if (!App.Locator.MainWindow.IsAuth)
			{
				this.Close();
			}
		}
	}
}
