using System.Text;

namespace Day10;

public sealed class CathodeRayTube
{
	private readonly StringBuilder _buffer = new();
	private int _position;

	public string Image => _buffer.ToString();
	public int Position => _position;

	public void Draw(int x)
	{
		_buffer.Append(GetSymbol(x));
		_position++;

		if (_position >= 40)
		{
			_buffer.AppendLine();
			_position = 0;
		}
	}

	public char GetSymbol(int x)
	{
		if (_position >= x - 1 && _position <= x + 1)
		{
			return '#';
		}

		return '.';
	}
}