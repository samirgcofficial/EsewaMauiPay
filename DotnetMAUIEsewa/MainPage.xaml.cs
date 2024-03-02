using EsewaMauiPlugin;

namespace DotnetMAUIEsewa;

public partial class MainPage : ContentPage
{
	int count = 0;
    private readonly IEsewaPayServices _myserviceclient;
    public MainPage()
	{
		InitializeComponent();
        _myserviceclient = (IEsewaPayServices)(new EsewaPayServices());
        _myserviceclient.PaymentSuccess+= Esewa_PaymentSuccess;
        _myserviceclient.PaymentError += Esewa_Error;
    }

    private void Esewa_Error(object sender, (string, IDictionary<string, string>) e)
    {
       Console.WriteLine(e.Item1.ToString());
    }

    private void Esewa_PaymentSuccess(object sender, IDictionary<string, object> e)
    {
        if (e.ContainsKey("transactionDetails"))
        {
            var transactionDetails = e["transactionDetails"];
            if (transactionDetails != null)
            {
                string transactionDetailsValue = transactionDetails.ToString();
                App.Current.MainPage.DisplayAlert("Payment Success", $"Transaction Details: {transactionDetailsValue}", "Ok");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error", "Transaction details value is null", "Ok");
            }
        }
        else
        {
            App.Current.MainPage.DisplayAlert("Error", "Transaction details not found", "Ok");
        }
    }


    private async void OnCounterClicked(object sender, EventArgs e)
	{
        var paymentInfo = new PaymentInfo
        {
            ClientId = "JB0BBQ4aD0UqIThFJwAKBgAXEUkEGQUBBAwdOgABHD4DChwUAB0R", // Replace with your actual Client ID provided by eSewa
            SecretKey = "BhwIWQQADhIYSxILExMcAgFXFhcOBwAKBgAXEQ==", // Replace with your actual Secret Key provided by eSewa
            Environment = "development", // Use "live" for production environment
            ProductName = "Test Product",
            ProductId = "TP123",
            ProductPrice = "500", // The price of the product in NPR (Nepalese Rupee)
            CallbackUrl = "https://yourdomain.com/payment/callback", // Your server callback URL
            EbpNo = "OptionalEbpNumber" // This is optional and specific to your implementation
        };
        _myserviceclient.Pay(paymentInfo);
    }
}


