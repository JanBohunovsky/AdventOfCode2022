var input = File.ReadAllLines("Data/input.txt");

// Part 1
var totalPriority = input
	.Select(FindExtraItemType)
	.Select(GetItemTypePriority)
	.Sum();

Console.WriteLine($"Part 1: {totalPriority}");

// Part 2
var totalBadgePriority = GroupElves(input)
	.Select(FindBadgeItemType)
	.Select(GetItemTypePriority)
	.Sum();

Console.WriteLine($"Part 2: {totalBadgePriority}");


char FindExtraItemType(string rucksack)
{
	var compartmentSize = rucksack.Length / 2;

	var firstCompartment = rucksack[..compartmentSize];
	var secondCompartment = rucksack[compartmentSize..];

	return firstCompartment
		.Intersect(secondCompartment)
		.Single();
}

char FindBadgeItemType(string[] group)
{
	if (group.Length is not 3)
	{
		throw new ArgumentException("Group must be of size 3.", nameof(group));
	}

	return group[0]
		.Intersect(group[1])
		.Intersect(group[2])
		.Single();
}

IEnumerable<string[]> GroupElves(IReadOnlyCollection<string> rucksacks)
{
	for (int i = 0; i < rucksacks.Count / 3; i++)
	{
		yield return rucksacks.Skip(i * 3)
			.Take(3)
			.ToArray();
	}
}

int GetItemTypePriority(char itemType)
{
	var value = (int)itemType;

	return value switch
	{
		>= 97 and <= 122 => value - 96,
		>= 65 and <= 90 => value - 64 + 26,
		_ => throw new ArgumentException("Item type must be a character from 'a' to 'z' or from 'A' to 'Z'.", nameof(itemType)),
	};
}