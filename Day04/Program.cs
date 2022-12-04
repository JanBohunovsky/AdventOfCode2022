var input = File.ReadAllLines("Data/input.txt")
	.Select(ParseAssignmentPair)
	.ToArray();

// Part 1
var fullyContainedCount = input.Where(AssignmentFullyContainsOther)
	.Count();

Console.WriteLine($"Part 1: {fullyContainedCount}");

// Part 2
var overlappingAssignmentCount = input.Where(AssignmentsOverlap)
	.Count();

Console.WriteLine($"Part 1: {overlappingAssignmentCount}");


bool AssignmentFullyContainsOther((HashSet<int>, HashSet<int>) pair)
{
	return pair.Item1.IsSubsetOf(pair.Item2) || pair.Item2.IsSubsetOf(pair.Item1);
}

bool AssignmentsOverlap((HashSet<int>, HashSet<int>) pair)
{
	return pair.Item1.Overlaps(pair.Item2);
}

IEnumerable<int> ParseSectionRange(string range)
{
	var bounds = range.Split('-');
	var start = int.Parse(bounds[0]);
	var count = int.Parse(bounds[1]) - start + 1;

	return Enumerable.Range(start, count);
}

(HashSet<int>, HashSet<int>) ParseAssignmentPair(string assignmentPair)
{
	var pair = assignmentPair.Split(',');
	var firstSections = ParseSectionRange(pair[0]).ToHashSet();
	var secondSections = ParseSectionRange(pair[1]).ToHashSet();

	return (firstSections, secondSections);
}