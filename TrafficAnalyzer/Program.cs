using Google.Protobuf;
using Protocol;

namespace TrafficAnalyzer;

internal static class Program
{
    private const int StdInSize = 65535;
    private const string ProtoAssembly = "Protocol";
    private const string ProtoNamespace = "Protocol";
    private const string MessageParserProperty = "Parser";

    private static readonly DumpOptions s_objectDumperOpts = new() { DumpStyle = DumpStyle.CSharp, IndentSize = 4, IndentChar = ' ', IgnoreDefaultValues = true };

    private static void Main(string[] args)
    {
        Console.SetIn(new StreamReader(Console.OpenStandardInput(StdInSize), Console.InputEncoding, false, StdInSize));

        List<Tuple<int, byte[]>> inList = [];

        string? idInput;
        string? payloadInput;

        while (!string.IsNullOrEmpty(idInput = Console.ReadLine()) && (payloadInput = Console.ReadLine()) != null)
        {
            int messageId = int.Parse(idInput);
            byte[] payload = Convert.FromHexString(payloadInput);

            inList.Add(Tuple.Create(messageId, payload));
        }

        foreach ((int messageId, byte[] payload) in inList)
        {
            string messageName = ((MessageId)messageId).ToString();

            Type? type = Type.GetType($"{ProtoNamespace}.{messageName},{ProtoAssembly}");
            if (type is null)
            {
                Console.WriteLine($"Message with id {messageName} wasn't found in proto definition.");
                continue;
            }

            MessageParser parser = (MessageParser)type.GetProperty(MessageParserProperty)!.GetValue(null)!;
            IMessage message = parser.ParseFrom(payload);

            string outputInitializer = ObjectDumper.Dump(message, s_objectDumperOpts);

            Console.WriteLine($"Message: {messageName}");
            Console.WriteLine(outputInitializer);
        }
    }
}
