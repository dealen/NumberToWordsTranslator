using NumberToWords;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace NumbersToWordsTranslator
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _numberToConvert;
        private string _resultText;
        private readonly NumbersToWordsConverterServiceClient _converter;

        public MainWindowViewModel()
        {
            ConvertCommand = new RelayCommand<object>(ConvertText);
            _converter = new NumbersToWordsConverterServiceClient();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ConvertCommand { get; }

        public string NumberToConvert
        {
            get => _numberToConvert;
            set
            {
                _numberToConvert = value;
                OnPropertyChanged();
            }
        }

        public string ResultText
        {
            get => _resultText;
            set
            {
                _resultText = value;
                OnPropertyChanged();
            }
        }

        private async void ConvertText(object obj)
        {
            try
            {
                ResultText = await _converter.ConvertAsync(NumberToConvert);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while converting number. try again later.");
                Trace.WriteLine($"{ex.Message}");
                Trace.WriteLine($"{ex.StackTrace}");
                Trace.WriteLine($"{ex?.InnerException.Message}");
                Trace.WriteLine($"{ex?.InnerException.StackTrace}");
            }
        }

        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
