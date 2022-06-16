using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Govorun
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Message selectedMessage;
        private readonly ICollection<Message> messages = new ObservableCollection<Message>();

        static int port = 8080; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера

        private string messageText;

        private RelayCommand sendCommand;

        public ViewModel()
        {
            Port = port;
            IpAdress = address;
            //SelectedMessage = SelectedMessage ?? new Message();
            //SendText = "Type here text...";
            sendCommand = new RelayCommand((o) => SendMessage(), IsEmptyMessage);
        }
        public Message SelectedMessage
        {
            get => selectedMessage;
            set
            {
                if (selectedMessage != value)
                {
                    selectedMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public IEnumerable<Message> Messages => messages;

        public bool IsEmptyMessage(object obj)
        {
            if (SendText == "")
                return false;
            return true;
        }
        public void SendMessage()
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IpAdress), Port);
                IPEndPoint remoteIpPoint = new IPEndPoint(IPAddress.Any, 0);
                UdpClient client = new UdpClient();
                string message = "";
                message = SendText;
                byte[] data = Encoding.Unicode.GetBytes(message);

                //socket.SendTo(data, ipPoint);
                client.Send(data, data.Length, ipPoint);

                messages.Add(new Message() { Text=SendText, IsAnswer=false, Time=DateTime.Now});
                //MessageBox.Show($"Sending Text: {SendText}", "Sending Message");
                //MessageBox.Show($"{SelectedMessage.Data}", "Selected Message Text");

                data = client.Receive(ref remoteIpPoint);
                string response = Encoding.Unicode.GetString(data);

                messages.Add(new Message() { Text=response, IsAnswer=true});
                //MessageBox.Show(response, "Server response");

                // закрываем сокет
                client.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ICommand SendMessageCmd => sendCommand;

        public string IpAdress 
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }
        public int Port
        {
            get { return port; }
            set 
            { 
                port = value;
                OnPropertyChanged();
            }
        }

        public string SendText
        {
            get 
            {
                return messageText;
            }
            set
            {
                messageText = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
