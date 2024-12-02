const string input = "input.txt";

if (!File.Exists(input))
{
    Console.WriteLine("No input file found, aborting!");
    return;
}

using var sr = File.OpenText(input);
List<uint> leftSide = [];
List<uint> rightSide = [];
Dictionary<uint, uint> rightSideCounts = [];
while (sr.ReadLine() is { } line)
{
    var split = line.Split("   ");
    if (split.Length != 2)
    {
        Console.WriteLine($"Invalid split length ({split.Length}) in input, aborting!");
        return;
    }

    var leftNum = uint.Parse(split[0]);
    var rightNum = uint.Parse(split[1]);

    leftSide.Add(leftNum);
    rightSide.Add(rightNum);
    
    if (rightSideCounts.TryGetValue(rightNum, out var value))
    {
        rightSideCounts[rightNum] = ++value;
    }
    else
    {
        rightSideCounts.Add(rightNum, 1);
    }
}
    
leftSide.Sort();
rightSide.Sort();

uint distance = 0;
uint similarity = 0;
for (var i = 0; i < leftSide.Count; i++)
{
    if (leftSide[i] > rightSide[i])
    {
        distance += leftSide[i] - rightSide[i];
    }
    else
    {
        distance += rightSide[i] - leftSide[i];
    }
    if (rightSideCounts.TryGetValue(leftSide[i], out var value))
    {
        similarity += leftSide[i] * value;
    }
}

Console.WriteLine($"Distance: {distance}");
Console.WriteLine($"Similarity: {similarity}");