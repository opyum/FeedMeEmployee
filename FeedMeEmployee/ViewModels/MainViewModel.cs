using FeedMeEmployee.Business.Interface;
using System.Text;
using System.Windows.Input;

namespace FeedMeEmployee.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INfcService nfcAdapter;
        private string stringData;

        public string StringData
        {
            get => stringData;
            set => SetProperty(ref stringData, value);
        }

        public ICommand StartNfcTransmissionCommand { get; set; }

        public MainViewModel(INfcService nfcService)
        {
            nfcAdapter = nfcService;
            StartNfcTransmissionCommand = new Command(() => ExecuteNfc());
            stringData = string.Empty;
        }

        public async override void ExecuteOnAppearing()
        {
            base.ExecuteOnAppearing();
            if (await nfcAdapter.OpenNFCSettingsAsync())
            {
                nfcAdapter.ConfigureNfcAdapter();
                nfcAdapter.EnableForegroundDispatch();
            }
        }

        public override void ExecuteOnDisappearing()
        {
            base.ExecuteOnDisappearing();
            nfcAdapter.DisableForegroundDispatch();
            nfcAdapter.UnconfigureNfcAdapter();
        }

        private Task ExecuteNfc()
        {
            byte[] bytes = Encoding.ASCII.GetBytes(stringData);
            return nfcAdapter.SendAsync(bytes);
        }

    }
}

