using Day10.Instructions;

namespace Day10.Tests;

public sealed class CentralProcessingUnitTests
{
	[Fact]
	public void Executing_noop_instruction_does_nothing_after_1_cycle()
	{
		var program = new[] { new NoOperationInstruction() };
		var cpu = new CentralProcessingUnit(program);

		while (cpu.Tick())
		{
		}

		cpu.RegisterX.Should().Be(1);
		cpu.Cycle.Should().Be(2);
	}

	[Theory]
	[InlineData(3, 4)]
	[InlineData(-5, -4)]
	public void Executing_addx_instruction_changes_x_register_after_2_cycles(int value, int expectedValueOfRegisterX)
	{
		var program = new[] { new AddToRegisterXInstruction(value) };
		var cpu = new CentralProcessingUnit(program);

		cpu.Tick();
		cpu.RegisterX.Should().Be(1);

		var processing = cpu.Tick();

		processing.Should().BeFalse();
		cpu.RegisterX.Should().Be(expectedValueOfRegisterX);
		cpu.Cycle.Should().Be(3);
	}
}