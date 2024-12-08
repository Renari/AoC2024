//#define DEBUG_MODE
const string input = "input.txt";

if (!File.Exists(input))
{
    Console.WriteLine("No input file found, aborting!");
    return;
}
using var sr = File.OpenText(input);
List<ulong> validResults = [];
List<ulong> validResults2 = [];
while (sr.ReadLine() is { } line)
{
    var split = line.Split(": ");
    if (split.Length != 2)
    {
        Console.WriteLine($"Invalid data found '{line}'");
        return;
    }

    var result = ulong.Parse(split[0]);
    var numbers = split[1].Split(' ').Select(ulong.Parse).ToArray();

    var possibleSolutions = (int)Math.Pow(2, numbers.Length - 1);
    var solutions = new ulong[possibleSolutions];
    
    // preload the first number into every solution
    for (var i = 0; i < solutions.Length; i++)
    {
        solutions[i] = numbers[0];
    }

    for (var j = 0; j < solutions.Length; j++)
    {
        var permutationIndex = j;
#if DEBUG_MODE
        Console.Write(solutions[j]);
#endif
        for (var i = 1; i < numbers.Length; i++)
        {
            var operation = permutationIndex % 2 == 0;
            switch (operation)
            {
                // addition
                case false:
#if DEBUG_MODE
                    Console.Write($" + {numbers[i]}");
#endif
                    solutions[j] += numbers[i];
                    break;
                // multiplication
                case true:
#if DEBUG_MODE
                    Console.Write($" * {numbers[i]}");
#endif
                    solutions[j] *= numbers[i];
                    break;
            }
            permutationIndex /= 2;
        }
#if DEBUG_MODE
        Console.WriteLine();
#endif
    }

    if (solutions.Contains(result))
    {
        validResults.Add(result);
    }
    // part 2
    possibleSolutions = (int)Math.Pow(3, numbers.Length - 1);
    solutions = new ulong[possibleSolutions];
    // preload the first number into every solution
    for (var i = 0; i < solutions.Length; i++)
    {
        solutions[i] = numbers[0];
    }

    for (var j = 0; j < solutions.Length; j++)
    {
        var permutationIndex = j;
#if DEBUG_MODE
        Console.Write(solutions[j]);
#endif
        for (var i = 1; i < numbers.Length; i++)
        {
            var operation = permutationIndex % 3;
            switch (operation)
            {
                // addition
                case 0:
#if DEBUG_MODE
                    Console.Write($" + {numbers[i]}");
#endif
                    solutions[j] += numbers[i];
                    break;
                // multiplication
                case 1:
#if DEBUG_MODE
                    Console.Write($" * {numbers[i]}");
#endif
                    solutions[j] *= numbers[i];
                    break;
                case 2:
#if DEBUG_MODE
                    Console.Write($" || {numbers[i]}");
#endif
                    solutions[j] = ulong.Parse(string.Concat(solutions[j], numbers[i]));
                    break;
            }
            permutationIndex /= 3;
        }
#if DEBUG_MODE
        Console.WriteLine();
#endif
    }
    
    if (solutions.Contains(result))
    {
        validResults2.Add(result);
    }
}

Console.WriteLine($"Part 1: {validResults.Aggregate<ulong, ulong>(0, (current, number) => current + number)}");
Console.WriteLine($"Part 2: {validResults2.Aggregate<ulong, ulong>(0, (current, number) => current + number)}");