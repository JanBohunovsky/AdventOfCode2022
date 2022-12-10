namespace Day10.Instructions;

public interface IInstruction
{
	/// <summary>
	/// Processes one cycle of the instruction.
	/// </summary>
	/// <param name="registerX">X Register</param>
	/// <returns>True if the instruction requires more cycles to complete. False when the instruction is completed.</returns>
	bool Process(ref int registerX);

	public static IInstruction Parse(string instruction)
	{
		var data = instruction.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		return data[0] switch
		{
			"noop" => new NoOperationInstruction(),
			"addx" => new AddToRegisterXInstruction(int.Parse(data[1])),
			_ => throw new Exception($"Unknown instruction '{data[0]}'"),
		};
	}
}