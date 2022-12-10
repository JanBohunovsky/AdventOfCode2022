namespace Day10.Instructions;

public sealed class NoOperationInstruction : IInstruction
{
	public bool Process(ref int registerX)
	{
		return false;
	}
}