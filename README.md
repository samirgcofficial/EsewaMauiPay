# Esewa Maui Payment Gateway 

![EsewaMAUI](https://github.com/samirgcofficial/EsewaMauiPay/assets/55045516/aefba531-daac-4974-a3eb-8007648a7c85)


## Getting Started

To start using the Esewa Payment Plugin in your .NET MAUI project, follow these simple steps:

 **Installation**: Install the Esewa Payment Plugin NuGet package in your .NET MAUI project.
   ```sh
  dotnet add package plugin.maui.esewa.min --version 1.0.0
```

# Maui Android Implementation MainActivity.cs
```csharp

private static int REQUEST_CODE_PAYMENT = 1;

   protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
    {
        base.OnActivityResult(requestCode, resultCode, data);

        if (requestCode == REQUEST_CODE_PAYMENT)
        {
            if (resultCode == Result.Ok)
            {
                if (data == null) return;
                string message = data.GetStringExtra(ESewaPayment.ExtraResultMessage);
                Android.Util.Log.Info("TAG", "Proof of Payment: " + message);
                Toast.MakeText(this, "SUCCESSFUL PAYMENT", ToastLength.Short).Show();
            }
            else if (resultCode == Result.Canceled)
            {
                Toast.MakeText(this, "Canceled By User", ToastLength.Short).Show();
            }
            else if (resultCode == (Result)ESewaPayment.ResultExtrasInvalid)
            {
                if (data == null) return;
                string message = data.GetStringExtra(ESewaPayment.ExtraResultMessage);
                Android.Util.Log.Info("TAG", "Proof of Payment: " + message);
            }
        }
    }
```
# Maui iOS Create payment details AppDelegate.cs

No Configuration :) 

# Dotnet MAUI Implementation 
```csharp

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
```

# Unlock the Full Version
Gain access to the entire plugin, free from the evaluation limitations, by supporting our project with a small contribution. Your support acknowledges our hard work and dedication 🥰. We suggest trying the evaluation copy before purchasing. Plus, I've made it affordable for all customers.
[Support us](https://www.buymeacoffee.com/samirgc/e/222788)

