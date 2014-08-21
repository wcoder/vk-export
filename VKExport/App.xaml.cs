using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VKExport.ViewModels;

namespace VKExport
{
	public partial class App : Application
	{
		private static ViewModelLocator _locator;
		private static object _lock = new Object();

		public static ViewModelLocator Locator
		{
			get
			{
				if (_locator == null)
				{
					lock (_lock)
					{
						if (_locator == null)
						{
							_locator = new ViewModelLocator();
						}
					}
				}
				return _locator;
			}
		}
	}
}
