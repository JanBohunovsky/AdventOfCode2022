namespace Day11.Tests;

public sealed class InspectOperationTests
{
	[Theory]
	[InlineData("new = old + 19", InspectOperationType.Add, 19)]
	[InlineData("Operation: new = old * 52", InspectOperationType.Multiply, 52)]
	[InlineData("  Operation: new = old + old", InspectOperationType.Add, null)]
	[InlineData("new = old * old", InspectOperationType.Multiply, null)]
	public void Parsing_valid_input_returns_correct_object(string input, InspectOperationType expectedType, int? expectedValue)
	{
		var result = InspectOperation.Parse(input);

		result.Type.Should().Be(expectedType);
		result.Value.Should().Be(expectedValue);
	}

	[Theory]
	[InlineData("")]
	[InlineData("new = new + 19")]
	[InlineData("new = 20 + old")]
	[InlineData("old = new * new")]
	public void Parsing_invalid_input_throws_exception(string input)
	{
		var act = () => InspectOperation.Parse(input);

		act.Should().Throw<FormatException>();
	}

	[Theory]
	[InlineData(10, InspectOperationType.Add, 20, 30)]
	[InlineData(10, InspectOperationType.Add, null, 20)]
	[InlineData(20, InspectOperationType.Multiply, 4, 80)]
	[InlineData(12, InspectOperationType.Multiply, null, 144)]
	public void Apply_does_the_correct_operation(int item, InspectOperationType type, int? value, int expectedResult)
	{
		var operation = new InspectOperation(type, value);

		var result = operation.Apply(item);

		result.Should().Be(expectedResult);
	}
}