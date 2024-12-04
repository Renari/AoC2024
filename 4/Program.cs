const string input = "input.txt";

if (!File.Exists(input))
{
    Console.WriteLine("No input file found, aborting!");
    return;
}

var lines = File.ReadAllLines(input);
var xmasInstances = 0;
var crossMasInstance = 0;

// part 1
for (var y = 0; y < lines.Length; y++)
{
    for (var x = 0; x < lines[y].Length; x++)
    {
        if (lines[y][x] != 'X') continue;
        // check if XMAS is to the right
        if (x + 3 < lines[y].Length) // make sure this check won't escape bounds
        {
            var match = lines[y][x + 1] == 'M';
            if (lines[y][x + 2] != 'A') match = false;
            if (lines[y][x + 3] != 'S') match = false;
            if (match) xmasInstances++;
        }
        // check if XMAS is to the left
        if (x >= 3) // make sure this check won't escape bounds
        {
            var match = lines[y][x - 1] == 'M';
            if (lines[y][x - 2] != 'A') match = false;
            if (lines[y][x - 3] != 'S') match = false;
            if (match) xmasInstances++;
        }
        // check if XMAS is up
        if (y >= 3) // make sure this check won't escape bounds
        {
            var match = lines[y - 1][x] == 'M';
            if (lines[y - 2][x] != 'A') match = false;
            if (lines[y - 3][x] != 'S') match = false;
            if (match) xmasInstances++;
        }
        // check if XMAS is down
        if (y + 3 < lines.Length) // make sure this check won't escape bounds
        {
            var match = lines[y + 1][x] == 'M';
            if (lines[y + 2][x] != 'A') match = false;
            if (lines[y + 3][x] != 'S') match = false;
            if (match) xmasInstances++;
        }
        // check if XMAS is diagonal up right (this is a combination of up and right patterns above)
        if (x + 3 < lines[y].Length && y >= 3) // make sure this check won't escape bounds
        {
            var match = lines[y - 1][x + 1] == 'M';
            if (lines[y - 2][x + 2] != 'A') match = false;
            if (lines[y - 3][x + 3] != 'S') match = false;
            if (match) xmasInstances++;
        }
        // check if XMAS is diagonal up left (this is a combination of up and left patterns above)
        if (x >= 3 && y >= 3) // make sure this check won't escape bounds
        {
            var match = lines[y - 1][x - 1] == 'M';
            if (lines[y - 2][x - 2] != 'A') match = false;
            if (lines[y - 3][x - 3] != 'S') match = false;
            if (match) xmasInstances++;
        }
        // check if XMAS is diagonal down right (this is a combination of down and right patterns above)
        if (x + 3 < lines[y].Length && y + 3 < lines.Length) // make sure this check won't escape bounds
        {
            var match = lines[y + 1][x + 1] == 'M';
            if (lines[y + 2][x + 2] != 'A') match = false;
            if (lines[y + 3][x + 3] != 'S') match = false;
            if (match) xmasInstances++;
        }
        // check if XMAS is diagonal down left (this is a combination of down and left patterns above)
        if (x >= 3 && y + 3 < lines.Length) // make sure this check won't escape bounds
        {
            var match = lines[y + 1][x - 1] == 'M';
            if (lines[y + 2][x - 2] != 'A') match = false;
            if (lines[y + 3][x - 3] != 'S') match = false;
            if (match) xmasInstances++;
        }
    }
}

// part 2
// we loop from 1 to length - 1 here because the edges cannot fit a 3x3 pattern
for (var y = 1; y < lines.Length - 1; y++)
{
    for (var x = 1; x < lines[y].Length - 1; x++)
    {
        // we start at the center because this is the easiest place to check from
        if (lines[y][x] != 'A') continue;
        // M.S
        // .A.
        // M.S
        var match = lines[y - 1][x - 1] == 'M';
        if (lines[y + 1][x - 1] != 'M') match = false;
        if (lines[y - 1][x + 1] != 'S') match = false;
        if (lines[y + 1][x + 1] != 'S') match = false;
        if (match) crossMasInstance++;
        // S.S
        // .A.
        // M.M
        match = lines[y - 1][x - 1] == 'S';
        if (lines[y + 1][x - 1] != 'M') match = false;
        if (lines[y - 1][x + 1] != 'S') match = false;
        if (lines[y + 1][x + 1] != 'M') match = false;
        if (match) crossMasInstance++;
        // S.M
        // .A.
        // S.M
        match = lines[y - 1][x - 1] == 'S';
        if (lines[y + 1][x - 1] != 'S') match = false;
        if (lines[y - 1][x + 1] != 'M') match = false;
        if (lines[y + 1][x + 1] != 'M') match = false;
        if (match) crossMasInstance++;
        // M.M
        // .A.
        // S.S
        match = lines[y - 1][x - 1] == 'M';
        if (lines[y + 1][x - 1] != 'S') match = false;
        if (lines[y - 1][x + 1] != 'M') match = false;
        if (lines[y + 1][x + 1] != 'S') match = false;
        if (match) crossMasInstance++;
    }
}

Console.WriteLine($"XMAS Instances: {xmasInstances}");
Console.WriteLine($"X-MAS Instances: {crossMasInstance}");
