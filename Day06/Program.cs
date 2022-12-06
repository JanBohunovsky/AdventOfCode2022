const int startOfPacketSize = 4;
const int startOfMessageSize = 14;
var input = File.ReadAllText("Data/input.txt");

var startOfPacket = FindMarker(input, startOfPacketSize);
Console.WriteLine($"Part 1: {startOfPacket}");

var startOfMessage = FindMarker(input, startOfMessageSize);
Console.WriteLine($"Part 2: {startOfMessage}");


int FindMarker(string stream, int markerSize)
{
	for (int i = 0; i < stream.Length - markerSize; i++)
	{
		var uniqueCharCount = stream
			.Skip(i)
			.Take(markerSize)
			.Distinct()
			.Count();

		if (uniqueCharCount == markerSize)
		{
			return i + markerSize;
		}
	}

	throw new Exception($"Stream does not contain marker that has {markerSize} distinct characters");
}