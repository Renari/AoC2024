const string input = "input.txt";

if (!File.Exists(input))
{
    Console.WriteLine("No input file found, aborting!");
    return;
}

using var sr = File.OpenText(input);
var safeCount = 0;
var safeCountWithDampener = 0;
while (sr.ReadLine() is { } line)
{
    var numbers = line.Split(' ').Select(int.Parse).ToArray();
    if (CheckSafety(numbers))
    {
        safeCount++;
    }
    else
    {
        // see if we can fix it with the dampener
        for (var i = 0; i < numbers.Length; i++)
        {
            // create a copy of this reading with a single entry removed
            var dampenedNumbers = numbers.ToList();
            dampenedNumbers.RemoveAt(i);
            // check if this pattern is now safe
            if (CheckSafety(dampenedNumbers.ToArray()))
            {
                safeCountWithDampener++;
                break;
            }
        }
    }
    
}

Console.WriteLine($"Safe Count: {safeCount}");
Console.WriteLine($"Safe Count With Dampener: {safeCount + safeCountWithDampener}");
return;

bool CheckSafety(int[] numbers)
{
    // a reading with a single entry can't break any of the rules
    if (numbers.Length == 1) return true;
    var descending = numbers[0] > numbers[1];

    for (var i = 1; i < numbers.Length; i++)
    {
        // check for unsafe variance between steps
        var difference = Math.Abs(numbers[i] - numbers[i - 1]);
        if (difference is < 1 or > 3)
        {
            return false;
        }

        switch (descending)
        {
            // descending pattern but found ascending entry
            case true when numbers[i] > numbers[i - 1]:
            // ascending pattern but found descending entry
            case false when numbers[i - 1] > numbers[i]:
                return false;
        }
    }

    return true;
}
