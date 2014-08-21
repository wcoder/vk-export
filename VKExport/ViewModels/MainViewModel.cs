using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using VKExport.Common;

namespace VKExport.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		#region fields

		private int _offset = 0;

		private string _userId;
		private ObservableCollection<DialogMessage> _messages;
		private string _status;
		private int _countMessages;
		private RelayCommand _loadMessagesCommand;
		private RelayCommand _saveMessagesCommand;
		private RelayCommand _clearCommand;

		#endregion

		#region properties

		public AccessTokenResponse Auth { get; set; }

		public bool IsAuth {
			get
			{
				return Auth != null;
			}
		}
		public string UserId {
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

		public ObservableCollection<DialogMessage> Messages {
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
			CountMessages = 200;
			Messages = new ObservableCollection<DialogMessage>();
		}

		private void LoadData()
		{
			Status = "Загрузка сообщений...";

			var messages = VkApiHelpers.LoadMessages(Auth.AccessToken, CountMessages, _offset, UserId);
			var count = 0;

			foreach (var message in messages)
			{
				++count;
				Messages.Add(message);
			}

			if (count == 0)
			{
				Status = "Загружена вся история сообщений!";
			}
			else
			{
				Status = "Загрузка завершена!";
				Offset += count;
			}
		}

		private void SaveMessages()
		{
			var json = JsonConvert.SerializeObject(Messages);

			if (SaveToFile(json))
			{
				Status = "Успешно сохранено!";
			}
			else
			{
				Status = "Сохранение отменено!";
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

			Status = "Успешно очищено!";
		}

		public void OnAuthorized(object sender, VkAuthEventArgs e)
		{
			Auth = e.Data;
		}
	}
}