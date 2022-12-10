using Day10.Instructions;

namespace Day10.Tests;

public sealed class InstructionTests
{
	[Fact]
	public void Parsing_noop_returns_NoOperationInstruction()
	{
		var result = IInstruction.Parse("noop");

		result.Should().BeOfType<NoOperationInstruction>();
	}

	[Theory]
	[InlineData(1)]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(12345)]
	public void Parsing_addx_returns_AddToRegisterXInstruction(int value)
	{
		var result = IInstruction.Parse($"addx {value}");

		result.Should().BeOfType<AddToRegisterXInstruction>();
		var addxInstruction = result as AddToRegisterXInstruction;
		addxInstruction?.Value.Should().Be(value);
	}
}