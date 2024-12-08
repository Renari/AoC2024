//#define DEBUG_MODE
const string input = "input.txt";

if (!File.Exists(input))
{
    Console.WriteLine("No input file found, aborting!");
    return;
}
var grid = File.ReadAllLines(input).Select(line => line.ToCharArray()).ToArray();
var maxX = grid[0].Length;
var maxY = grid.Length;
Dictionary<char, List<(int, int)>> antennas = [];
HashSet<(int, int)> part1Antinodes = [];
HashSet<(int, int)> antinodes = [];

// add all antenna to dictionary
for (var y = 0; y < maxY; y++)
{
    for (var x = 0; x < maxX; x++)
    {
        if (grid[y][x] != '.')
        {
            if (!antennas.TryGetValue(grid[y][x], out var coords))
            {
                coords = [];
                antennas.Add(grid[y][x], coords);
            }
            coords.Add((y, x));
            antinodes.Add((y, x));
        }
    }
}

foreach (var antenna in antennas)
{
    var locations = antenna.Value;
    for (var i = 0; i < locations.Count; i++)
    {
        for (var j = i + 1; j < locations.Count; j++)
        {
            // calculate anitnodes for these two antenna
            var distance = DistanceBetweenTwoPoints(locations[i], locations[j]);
            // location[i] is further north
            if (locations[i].Item1 < locations[j].Item1)
            {
                if (locations[i].Item2 < locations[j].Item2)
                {
                    var antinodeY1Offset = -distance.Item1;
                    var antinodeX1Offset = -distance.Item2;
                    var antinodeY1 = locations[i].Item1 - distance.Item1;
                    var antinodeX1 = locations[i].Item2 - distance.Item2;
                    var pass = 0;
                    while (CheckAntinodeValidity((antinodeY1, antinodeX1)))
                    {
                        if (pass == 0) part1Antinodes.Add((antinodeY1, antinodeX1));
                        antinodes.Add((antinodeY1, antinodeX1));
#if DEBUG_MODE
                        Console.WriteLine($"antenna {locations[i]} and {locations[j]} added an antinode at ({antinodeY1}, {antinodeX1})");
#endif
                        antinodeY1 += antinodeY1Offset;
                        antinodeX1 += antinodeX1Offset;
                        pass++;
                    }
                    var antinodeY2Offset = distance.Item1;
                    var antinodeX2Offset = distance.Item2;
                    var antinodeY2 = locations[j].Item1 + distance.Item1;
                    var antinodeX2 = locations[j].Item2 + distance.Item2;
                    pass = 0;
                    while (CheckAntinodeValidity((antinodeY2, antinodeX2)))
                    {
                        if (pass == 0) part1Antinodes.Add((antinodeY2, antinodeX2));
                        antinodes.Add((antinodeY2, antinodeX2));
#if DEBUG_MODE
                        Console.WriteLine($"antenna {locations[i]} and {locations[j]} added an antinode at ({antinodeY2}, {antinodeX2})");
#endif
                        antinodeY2 += antinodeY2Offset;
                        antinodeX2 += antinodeX2Offset;
                        pass++;
                    }
                }
                else
                {
                    var antinodeY1Offset = -distance.Item1;
                    var antinodeX1Offset = distance.Item2;
                    var antinodeY1 = locations[i].Item1 - distance.Item1;
                    var antinodeX1 = locations[i].Item2 + distance.Item2;
                    var pass = 0;
                    while (CheckAntinodeValidity((antinodeY1, antinodeX1)))
                    {
                        if (pass == 0) part1Antinodes.Add((antinodeY1, antinodeX1));
                        antinodes.Add((antinodeY1, antinodeX1));
#if DEBUG_MODE
                        Console.WriteLine($"antenna {locations[i]} and {locations[j]} added an antinode at ({antinodeY1}, {antinodeX1})");
#endif
                        antinodeY1 += antinodeY1Offset;
                        antinodeX1 += antinodeX1Offset;
                        pass++;
                    }
                    var antinodeY2Offset = distance.Item1;
                    var antinodeX2Offset = -distance.Item2;
                    var antinodeY2 = locations[j].Item1 + distance.Item1;
                    var antinodeX2 = locations[j].Item2 - distance.Item2;
                    pass = 0;
                    while (CheckAntinodeValidity((antinodeY2, antinodeX2)))
                    {
                        if (pass == 0) part1Antinodes.Add((antinodeY2, antinodeX2));
                        antinodes.Add((antinodeY2, antinodeX2));
#if DEBUG_MODE
                        Console.WriteLine($"antenna {locations[i]} and {locations[j]} added an antinode at ({antinodeY2}, {antinodeX2})");
#endif
                        antinodeY2 += antinodeY2Offset;
                        antinodeX2 += antinodeX2Offset;
                        pass++;
                    }
                }
            }
            else
            {
                // location[j] is further north
                // this will also run for perfectly horizontal positions
                // which should be fine because one of our y distance will be 0
                if (locations[i].Item2 < locations[j].Item2)
                {
                    var antinodeY1Offset = distance.Item1;
                    var antinodeX1Offset = -distance.Item2;
                    var antinodeY1 = locations[i].Item1 + distance.Item1;
                    var antinodeX1 = locations[i].Item2 - distance.Item2;
                    // check if the first antinode is off the map
                    while (CheckAntinodeValidity((antinodeY1, antinodeX1)))
                    {
#if DEBUG_MODE
                        Console.WriteLine($"antenna {locations[i]} and {locations[j]} added an antinode at ({antinodeY1}, {antinodeX1})");
#endif
                        antinodes.Add((antinodeY1, antinodeX1));
                        antinodeY1 += antinodeY1Offset;
                        antinodeX1 += antinodeX1Offset;
                    }
                    var antinodeY2Offset = distance.Item1;
                    var antinodeX2Offset = -distance.Item2;
                    var antinodeY2 = locations[j].Item1 + distance.Item1;
                    var antinodeX2 = locations[j].Item2 - distance.Item2;
                    while (CheckAntinodeValidity((antinodeY2, antinodeX2)))
                    {
                        // not off the map, so this is a valid antinode
#if DEBUG_MODE
                        Console.WriteLine($"antenna {locations[i]} and {locations[j]} added an antinode at ({antinodeY2}, {antinodeX2})");
#endif
                        antinodes.Add((antinodeY2, antinodeX2));
                        antinodeY2 += antinodeY2Offset;
                        antinodeX2 += antinodeX2Offset;
                    }
                }
                else
                {
                    var antinodeY1Offset = distance.Item1;
                    var antinodeX1Offset = distance.Item2;
                    var antinodeY1 = locations[i].Item1 + distance.Item1;
                    var antinodeX1 = locations[i].Item2 + distance.Item2;
                    while (CheckAntinodeValidity((antinodeY1, antinodeX1)))
                    {
#if DEBUG_MODE
                        Console.WriteLine($"antenna {locations[i]} and {locations[j]} added an antinode at ({antinodeY1}, {antinodeX1})");
#endif
                        antinodes.Add((antinodeY1, antinodeX1));
                        antinodeY1 += antinodeY1Offset;
                        antinodeX1 += antinodeX1Offset;
                    }
                    var antinodeY2Offset = -distance.Item1;
                    var antinodeX2Offset = -distance.Item2;
                    var antinodeY2 = locations[j].Item1 - distance.Item1;
                    var antinodeX2 = locations[j].Item2 - distance.Item2;
                    while (CheckAntinodeValidity((antinodeY2, antinodeX2)))
                    {
#if DEBUG_MODE
                        Console.WriteLine($"antenna {locations[i]} and {locations[j]} added an antinode at ({antinodeY2}, {antinodeX2})");
#endif
                        antinodes.Add((antinodeY2, antinodeX2));
                        antinodeY2 += antinodeY2Offset;
                        antinodeX2 += antinodeX2Offset;
                    }
                }
            }
        }
    }
}

Console.WriteLine($"Part 1: {part1Antinodes.Count}");
Console.WriteLine($"Part 2: {antinodes.Count}");
return;

(int, int) DistanceBetweenTwoPoints((int, int) pointA, (int, int) pointB)
{
    return (Math.Abs(pointA.Item1 - pointB.Item1), Math.Abs(pointA.Item2 - pointB.Item2));
}

bool CheckAntinodeValidity((int, int) position)
{
    // check if the antinode is off the map
    return !(position.Item1 < 0 || position.Item1 >= maxY || position.Item2 < 0 || position.Item2 >= maxX);
}