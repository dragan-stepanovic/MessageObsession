﻿namespace MessageObsession
{
	public abstract class Message
	{
		public abstract override string ToString();
	}

	public class ProductNotFoundMessage : Message
	{
		public string UnknownBarcode { get; }

		public ProductNotFoundMessage(string unknownBarcode)
		{
			UnknownBarcode = unknownBarcode;
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

		public PriceMessage(Price price)
		{
			Price = price;
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

	public class Console : IDisplayOnConsole
	{
		public void Display(Message message)
		{
			System.Console.WriteLine(message);
		}
	}

	public interface IDisplayOnConsole
	{
		void Display(Message message);
	}

	public static class Program
	{
		private const string SomeUnknownBarcode = "SomeUnknownBarcode";

		public static void Main()
		{
			IDisplayOnConsole consoleDisplay = new Console();
			consoleDisplay.Display(new ProductNotFoundMessage(SomeUnknownBarcode));
			consoleDisplay.Display(new EmptyBarCodeMessage());
			consoleDisplay.Display(new PriceMessage(Price.InCents(255)));
			System.Console.ReadKey();
		}
	}
}