using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Govorun
{
    public class Message : INotifyPropertyChanged
    {
        private string text;
        private bool isAnswer;
        private DateTime time;

        public Message()
        {
            text = "NONE";
            isAnswer = false;
            time = DateTime.Now;
        }
        public string Text 
        { 
            get { return text; }
            set 
            {
                text = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Data));
            }
        }
        public bool IsAnswer
        {
            get { return isAnswer; }
            set 
            { 
                isAnswer = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Data));
            }
        }
        public DateTime Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Data));
            }
        }

        public string Data => $"{Text}:{Time.ToShortTimeString()}";
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
