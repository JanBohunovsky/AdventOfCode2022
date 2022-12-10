namespace Day10.Tests;

public sealed class CathodeRayTubeTests
{
	[Theory]
	[InlineData(0, '#')]
	[InlineData(1, '#')]
	[InlineData(2, '.')]
	[InlineData(30, '.')]
	[InlineData(-1, '#')]
	public void CRT_returns_correct_symbol_at_position_0(int x, char expectedSymbol)
	{
		var crt = new CathodeRayTube();

		var symbol = crt.GetSymbol(x);

		symbol.Should().Be(expectedSymbol);
	}

	[Theory]
	[InlineData(2, 5, ".###.")]
	[InlineData(7, 20, "......###...........")]
	public void CRT_draws_3_pixels_wide_sprite(int x, int cycles, string expectedImage)
	{
		var crt = new CathodeRayTube();

		for (int i = 0; i < cycles; i++)
		{
			crt.Draw(x);
		}

		crt.Image.Should().Be(expectedImage);
	}

	[Fact]
	public void CRT_wraps_after_40_cycles()
	{
		var crt = new CathodeRayTube();

		for (int i = 0; i < 39; i++)
		{
			crt.Draw(0);
		}

		crt.Position.Should().Be(39);
		crt.Draw(0);
		crt.Position.Should().Be(0);
	}
}