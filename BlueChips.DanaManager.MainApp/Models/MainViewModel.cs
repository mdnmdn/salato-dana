using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using BlueChips.DanaManager.MainApp.Libs;
using System.Collections.ObjectModel;
using BlueChips.DanaManager.MainApp.Logic;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;

namespace BlueChips.DanaManager.MainApp.Models
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            
            Orders = new ObservableCollection<OrderRow>();

            Messenger.Default.Register<ImportMessage>(this, m => Import(m));
        }


        public ObservableCollection<OrderRow> Orders { get; set; }

        bool _hasData;
        public bool HasData { get { return _hasData; } 
            set {
                if (_hasData != value) {
                    _hasData = value;
                    base.RaisePropertyChanged("HasData");
                }
        } }

        string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value) {
                    _message = value;
                    base.RaisePropertyChanged("Message");
                }
            }
        } 

        private void Import(ImportMessage m)
        {
            var parser = new OrderRowManager();
            IList<OrderRow> newOrders = null;
            try {
                newOrders = parser.ParseExcelFile(m.FilePath);
            

                if (newOrders == null || newOrders.Count == 0) {
                    DispatcherHelper.UIDispatcher.BeginInvoke((Action)(() => {
                        Message = "Nessun ordine trovato";
                    }));
                } else {
                    DispatcherHelper.UIDispatcher.BeginInvoke((Action)(() => {
                        Orders.Clear();
                        newOrders.Each(r => Orders.Add(r));
                        HasData = true;
                        Message = "Caricati " + Orders.Count + " ordini";
                    }));
                }
            } catch (PublicMessageException exception) {
                Messenger.Default.Send<ErrorMessage>(new ErrorMessage{ Message = exception.PublicMessage, Error = exception});
            } catch (Exception exception) {
                Messenger.Default.Send<ErrorMessage>(new ErrorMessage { Message = exception.Message, Error = exception });
            }




        }


    }
}