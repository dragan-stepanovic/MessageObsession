Here, instead of:
	
	console.DisplayProductNotFound(string unknownBarCode);
	
I've used approach of pushing commonalities up the call stack

	console.Display(new ProductNotFoundMessage(SomeUnknownBarcode));

Rendering is done in the ConsoleDisplay, while formatting is done in separate classes (concrete immutable Messages)

Besides removing primitive obsession, I find this approach to achieve context independence and better composition and that it produces more stable interface (IDisplayOnConsole contains only one method and will continue to have no matter how many new types of messages we'll need to display in the future).

	public interface IDisplayOnConsole
	{
		void Display(Message message);
	}

An interesting repercussion of this is that if I have manual stubs/mocks that are faking IDisplayOnConsole, I wouldn't have to change them when I add new types of messages. An opposite case, where we expand the interface and have to add implementations in all fakes, I use as a code smell to detect unstable interfaces. Another example of listening to the tests.
And also, for the same reasons stated above, this interface adheres to Open Closed Principle.

One more thing that I see as advantage, perhaphs, is that we get reusability in terms of messages that we could send out to other objects (which we don't have with separate display messages, since those are tightly coupled to ConsoleDisplay).
Also the knowledge of formatting is encapsulated in the messages. In case we need different formatting, for different types of displays, there could be a separate set of Messages (with their own formatting) in the other other namespaces and outgoing adapters (in terms of Ports and Adapters architecture).
