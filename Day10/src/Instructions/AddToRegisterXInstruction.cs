namespace Day10.Instructions;

public sealed class AddToRegisterXInstruction : IInstruction
{
	private readonly int _value;
	private bool _firstCycle = true;

	public int Value => _value;

	public AddToRegisterXInstruction(int value)
	{
		_value = value;
	}

	public bool Process(ref int registerX)
	{
		if (_firstCycle)
		{
			_firstCycle = false;
			return true;
		}

		registerX += _value;
		return false;
	}
}