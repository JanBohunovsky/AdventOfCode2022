namespace Day11;

public record struct InspectResult(bool Throw, int TargetMonkeyId, long Item)
{
	public static InspectResult ThrowToMonkey(int targetMonkeyId, long item)
		=> new(true, targetMonkeyId, item);

	public static InspectResult Finished()
		=> new(false, default, default);
}