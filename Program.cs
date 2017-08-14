namespace MessageObsession
{
	public abstract class Message
	{
		public abstract override string ToString();
	}

	public class ProductNotFoundMessage : Message
	{
		public string UnknownBarcode { get; }

		private ProductNotFoundMessage(string unknownBarcode)
		{
			UnknownBarcode = unknownBarcode;
		}

		public static ProductNotFoundMessage For(string unknownBarcode)
		{
			return new ProductNotFoundMessage(unknownBarcode);
		}

		public override string ToString() => $"Product not found for: {UnknownBarcode}";
	}

	public class EmptyBarCodeMessage : Message
	{
		public override string ToString() => "Scanning error: empty barcode!";
	}

	public class PriceMessage : Message
	{
		public Price Price { get; }

		private PriceMessage(Price price)
		{
			Price = price;
		}

		public static PriceMessage For(Price inCents)
		{
			return new PriceMessage(inCents);
		}

		public override string ToString() => $"Price value is: {Price.DollarValue()}";
	}

	public class Price
	{
		private readonly int _value;

		private Price(int value)
		{
			_value = value;
		}

		public static Price InCents(int cents)
		{
			return new Price(cents);
		}

		public int DollarValue() => _value;
	}

	public class Console : IDisplayMessage
	{
		public void Display(Message message)
		{
			System.Console.WriteLine(message);
		}
	}

	public interface IDisplayMessage
	{
		void Display(Message message);
	}

	public static class Program
	{
		private const string SomeUnknownBarcode = "SomeUnknownBarcode";

		public static void Main()
		{
			IDisplayMessage console = new Console();
			console.Display(ProductNotFoundMessage.For(SomeUnknownBarcode));
			console.Display(new EmptyBarCodeMessage());
			console.Display(PriceMessage.For(Price.InCents(255)));
			System.Console.ReadKey();
		}
	}
}