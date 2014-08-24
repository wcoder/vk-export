using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using VKExport.Common;
using VkNet;
using VkNet.Model;

namespace VKExport.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		#region fields

		private int _offset = 0;

		private long _userId;
		private ObservableCollection<Message> _messages;
		private string _status;
		private int _countMessages;
		private RelayCommand _loadMessagesCommand;
		private RelayCommand _saveMessagesCommand;
		private RelayCommand _clearCommand;

		#endregion

		#region properties

		public VkApi Auth { get; set; }

		public bool IsAuth {
			get
			{
				return Auth != null;
			}
		}
		public long UserId {
			get
			{
				return _userId;
			}
			set
			{
				_userId = value;
				RaisePropertyChanged("UserId");
			}
		}

		public ObservableCollection<Message> Messages
		{
			get
			{
				return _messages;
			}
			set
			{
				_messages = value;
				RaisePropertyChanged("Messages");
			}
		}

		public string Status {
			get
			{
				return _status;
			}
			set
			{
				_status = value;
				RaisePropertyChanged("Status");
			}
		}

		public int CountMessages
		{
			get
			{
				return _countMessages;
			}
			set
			{
				_countMessages = value;
				RaisePropertyChanged("CountMessages");
			}
		}

		public int Offset
		{
			get
			{
				return _offset;
			}
			set
			{
				_offset = value;
				RaisePropertyChanged("Offset");
			}
		}

		public RelayCommand LoadMessagesCommand
		{
			get
			{
				if (_loadMessagesCommand == null)
				{
					_loadMessagesCommand = new RelayCommand(this.LoadData);
				}
				return _loadMessagesCommand;
			}
		}

		public RelayCommand SaveMessagesCommand
		{
			get
			{
				if (_saveMessagesCommand == null)
				{
					_saveMessagesCommand = new RelayCommand(this.SaveMessages);
				}
				return _saveMessagesCommand;
			}
		}

		public RelayCommand ClearCommand
		{
			get
			{
				if (_clearCommand == null)
				{
					_clearCommand = new RelayCommand(this.Clear);
				}
				return _clearCommand;
			}
		}

		#endregion

		public MainViewModel()
		{
			UserId = 1;
			CountMessages = 200;
			Messages = new ObservableCollection<Message>();
		}

		private void LoadData()
		{
			try
			{
				var totalCount = 0;
				var messages = Auth.Messages.GetHistory(UserId, false, out totalCount, Offset, CountMessages);

				foreach (var message in messages)
				{
					Messages.Add(message);
				}
				Offset += messages.Count;

				if (totalCount == 0)
				{
					StatusLog("История сообщений с пуста!");
				}
				else if (Offset == totalCount)
				{
					StatusLog("Загружена вся история сообщений!");
				}
				else
				{
					StatusLog(string.Format("Загружено {0} из {1} сообщений", messages.Count, totalCount));
				}
			}
			catch (Exception e)
			{
				StatusLog("Ошибка: " + e.Message);
			}
		}

		private void SaveMessages()
		{
			var json = JsonConvert.SerializeObject(Messages);

			if (SaveToFile(json))
			{
				StatusLog("Успешно сохранено!");
			}
			else
			{
				StatusLog("Сохранение отменено!");
			}
		}

		private bool SaveToFile(string data)
		{
			var dialog = new SaveFileDialog();
			dialog.Filter = "Все файлы (*.*)|*.*";
			dialog.FileName = "VKMessages.json";
			dialog.Title = "Сохранить как";
			if (dialog.ShowDialog() == true)
			{
				File.WriteAllText(dialog.FileName, data);

				return true;
			}
			return false;
		}

		private void Clear()
		{
			Messages.Clear();
			Offset = 0;

			StatusLog("Успешно очищено!");
		}

		public void OnAuthorized(object sender, VkAuthEventArgs e)
		{
			Auth = e.Data;
		}

		private void StatusLog(string message)
		{
			Status = message;
		}
	}
}