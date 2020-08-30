using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DesignPatterns.ObserverPattern
{
    class ObserverPattern
    {
    }

    public class FallsIllEventArgs:EventArgs
    {
          public string Address { get; set; }
    }

    public class Person
    {
        public void CatchACold()
        {
            FallsIll?.Invoke(this, new FallsIllEventArgs { Address="123 London Road"});
        }
        public event EventHandler<FallsIllEventArgs> FallsIll;
    }


    #region  Weak Event 

    public class Button
    {
        public event EventHandler Clicked;

        public void Fire()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Window   :IDisposable
    {
        Button button;
        public Window(Button button)
        {
            this.button = button;
            //This method prevents the window from being cleaned up by GC event after it has been set to null
            button.Clicked += ButtonOnClicked;
        }

        private void ButtonOnClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Button clicked (window handler)");
        }

        public void Dispose()
        {
            button.Clicked -= ButtonOnClicked;
        }

        ~Window()
        {
            Console.WriteLine("Window finalized");
        }
    }

    #endregion


    #region ObserverPattern

    public class Market : INotifyPropertyChanged
    {
        private List<float> prices = new List<float>();
        public BindingList<float> Bids = new BindingList<float>();

        public void AddPrice(float price)
        {
            prices.Add(price);
            PriceAdded?.Invoke(this, price);
        }


        public event EventHandler<float> PriceAdded;

        private float volatility;
        public float Volatility
        {
            get => volatility;
            set
            {
                if (value.Equals(volatility))
                    return;

                volatility = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }  


    }

    #endregion

}
