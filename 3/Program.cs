using System.Text.RegularExpressions;

const string input = "input.txt";

if (!File.Exists(input))
{
    Console.WriteLine("No input file found, aborting!");
    return;
}

using var sr = File.OpenText(input);
const string pattern = @"mul\((\d+),(\d+)\)";
var inputData = sr.ReadToEnd();
long total = 0;
long fixedTotal = 0;

// part 1
foreach (Match match in Regex.Matches(inputData, pattern, RegexOptions.Multiline))
{
    if (match.Groups.Count != 3)
    {
        Console.WriteLine("Invalid match found!");
        return;
    }

    total += long.Parse(match.Groups[1].Value) * long.Parse(match.Groups[2].Value);
}

// part 2
var allDoInstances = inputData.Split("do()");
foreach (var segment in allDoInstances)
{
    var sanitizedSegment = segment;
    // check if a "don't()" exists and if so remove all entries after it
    if (segment.Contains("don't()"))
    {
        sanitizedSegment = segment.Split("don't()")[0];
    }
    foreach (Match match in Regex.Matches(sanitizedSegment, pattern, RegexOptions.Multiline))
    {
        fixedTotal += long.Parse(match.Groups[1].Value) * long.Parse(match.Groups[2].Value);
    }
}

Console.WriteLine($"Total: {total}");
Console.WriteLine($"Fixed Total: {fixedTotal}");
