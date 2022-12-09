namespace Day09;

public class Rope
{
	private static readonly Dictionary<char, Vector2> Directions = new()
	{
		{ 'U', new Vector2(0, -1) },
		{ 'D', new Vector2(0, 1) },
		{ 'L', new Vector2(-1, 0) },
		{ 'R', new Vector2(1, 0) },
	};

	private readonly HashSet<Vector2> _tailVisitedPositions = new();
	private readonly List<Vector2> _knots = new();

	public Vector2 HeadPosition => _knots.First();
	public Vector2 TailPosition => _knots.Last();

	public IEnumerable<Vector2> TailVisitedPositions => _tailVisitedPositions;

	public Rope(int knotCount = 2) : this(Vector2.Zero, knotCount) { }

	public Rope(Vector2 startPosition, int knotCount)
	{
		if (knotCount < 2)
		{
			throw new ArgumentOutOfRangeException(nameof(knotCount), knotCount, "Number of knots must be at least 2");
		}

		for (int i = 0; i < knotCount; i++)
		{
			_knots.Add(startPosition);
		}

		_tailVisitedPositions.Add(Vector2.Zero);
	}

	public void Move(string instruction)
	{
		var direction = Directions[instruction[0]];
		var amount = int.Parse(instruction[2..]);

		for (int i = 0; i < amount; i++)
		{
			_knots[0] += direction;

			for (int k = 1; k < _knots.Count; k++)
			{
				UpdateChildKnotPosition(k);
			}

			_tailVisitedPositions.Add(TailPosition);
		}
	}

	private void UpdateChildKnotPosition(int index)
	{
		if (index <= 0)
		{
			return;
		}

		if (AreKnotsTouching(index - 1, index))
		{
			return;
		}

		var vector = _knots[index - 1] - _knots[index];
		vector = new Vector2(Math.Sign(vector.X), Math.Sign(vector.Y));

		_knots[index] += vector;
	}

	private bool AreKnotsTouching(int index1, int index2)
	{
		var distance = Vector2.Distance(_knots[index1], _knots[index2]);

		return distance < 2;
	}
}