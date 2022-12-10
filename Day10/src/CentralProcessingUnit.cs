using Day10.Instructions;

namespace Day10;

public sealed class CentralProcessingUnit
{
	private readonly IEnumerator<IInstruction> _instructions;
	private int _registerX;

	public int Cycle { get; private set; }
	public int RegisterX => _registerX;

	public CentralProcessingUnit(IEnumerable<IInstruction> program)
	{

		_instructions = program.GetEnumerator();
		if (!_instructions.MoveNext())
		{
			throw new ArgumentException("Program must have at least 1 instruction", nameof(program));
		}

		_registerX = 1;
		Cycle = 1;
	}

	public static CentralProcessingUnit Parse(params string[] program)
	{
		return new CentralProcessingUnit(program.Select(IInstruction.Parse));
	}

	/// <summary>
	/// Ticks the CPU
	/// </summary>
	/// <returns>True if there's still something to process. False if the program is finished.</returns>
	public bool Tick()
	{
		var processing = _instructions.Current.Process(ref _registerX);
		Cycle++;

		return processing || _instructions.MoveNext();
	}
}