using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Battle_Simulator.CharacterStuff
{
    public class Attribute : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private AttributeName name;
        public AttributeName Name {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnChanged();
            }
        }

        private int value;

        public int Value {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                OnChanged();
            }
        }
        public int Modifier()
        {
            decimal tmp = Value - 10;
            if(Value < 0)
            {
                return (int)Math.Ceiling((tmp / 2));
            }
            else
            {
                return (int)tmp / 2;
            }
        }

        private void OnChanged()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(""));
            }
        }
    }
}
