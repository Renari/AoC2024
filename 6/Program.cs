Console.WriteLine($"Part 1: {Part1()}");
Console.WriteLine($"Part 2: {Part2()}");

return;

int Part1()
{
    const string input = "input.txt";

    if (!File.Exists(input))
    {
        Console.WriteLine("No input file found, aborting!");
        return 0;
    }

    var grid = File.ReadAllLines(input).Select(line => line.ToCharArray()).ToArray();
    var gridXSize = grid[0].Length - 1;
    var gridYSize = grid.Length - 1;
    
    // find the starting point
    Coords currentPosition = new();
    for (var y = 0; y < grid.Length; y++)
    {
        if (!(grid[y].Contains('^') || grid[y].Contains('>') || grid[y].Contains('v') || grid[y].Contains('<'))) continue;

        for (var x = 0; x < grid[y].Length; x++)
        {
            switch (grid[y][x])
            {
                case '^':
                case '>':
                case 'v':
                case '<':
                    currentPosition = new Coords(x, y);
                    break;
            }
        }
        
        break;
    }

    var leftGrid = false;
    while (!leftGrid)
    {
        switch (grid[currentPosition.Y][currentPosition.X])
        {
            case '^':
                if (currentPosition.Y == 0)
                {
                    leftGrid = true;
                    break;
                }
                // obstacle encountered, rotate 90 degrees
                if (grid[currentPosition.Y - 1][currentPosition.X] == '#')
                {
                    grid[currentPosition.Y][currentPosition.X] = '>';
                    break;
                }
                
                // swap the positions
                (grid[currentPosition.Y][currentPosition.X], grid[currentPosition.Y - 1][currentPosition.X]) = (
                    'X', grid[currentPosition.Y][currentPosition.X]);
                currentPosition.Y--;
                break;
            case '>':
                if (currentPosition.X == gridXSize)
                {
                    leftGrid = true;
                    break;
                }
                // obstacle encountered, rotate 90 degrees
                if (grid[currentPosition.Y][currentPosition.X + 1] == '#')
                {
                    grid[currentPosition.Y][currentPosition.X] = 'v';
                    break;
                }
                // swap the positions
                (grid[currentPosition.Y][currentPosition.X], grid[currentPosition.Y][currentPosition.X + 1]) = (
                    'X', grid[currentPosition.Y][currentPosition.X]);
                currentPosition.X++;
                break;
            case 'v':
                if (currentPosition.Y == gridYSize)
                {
                    leftGrid = true;
                    break;
                }
                // obstacle encountered, rotate 90 degrees
                if (grid[currentPosition.Y + 1][currentPosition.X] == '#')
                {
                    grid[currentPosition.Y][currentPosition.X] = '<';
                    break;
                }
                // swap the positions
                (grid[currentPosition.Y][currentPosition.X], grid[currentPosition.Y + 1][currentPosition.X]) = (
                    'X', grid[currentPosition.Y][currentPosition.X]);
                currentPosition.Y++;
                break;
            case '<':
                if (currentPosition.X == 0)
                {
                    leftGrid = true;
                    break;
                }
                // obstacle encountered, rotate 90 degrees
                if (grid[currentPosition.Y][currentPosition.X - 1] == '#')
                {
                    grid[currentPosition.Y][currentPosition.X] = '^';
                    break;
                }
                // swap the positions
                (grid[currentPosition.Y][currentPosition.X], grid[currentPosition.Y][currentPosition.X - 1]) = (
                    'X', grid[currentPosition.Y][currentPosition.X]);
                currentPosition.X--;
                break;
        }
        
    }

    return 1 + grid.Sum(line => line.Count(position => position == 'X'));
}

// it's the same as part 1 except when we make turns we need to check if cross a position we already passed
int Part2()
{
    const string input = "input.txt";

    if (!File.Exists(input))
    {
        Console.WriteLine("No input file found, aborting!");
        return 0;
    }

    var grid = File.ReadAllLines(input).Select(line => line.ToCharArray()).ToArray();
    var gridSizeX = grid[0].Length - 1;
    var gridSizeY = grid.Length - 1;
    var loops = 0;
    List<Coords> positionsTraversed = [];
    
    // find the starting point
    Coords currentPosition = new();
    var currentFacing = '\0';
    for (var y = 0; y < grid.Length; y++)
    {
        if (!(grid[y].Contains('^') || grid[y].Contains('>') || grid[y].Contains('v') || grid[y].Contains('<'))) continue;

        for (var x = 0; x < grid[y].Length; x++)
        {
            switch (grid[y][x])
            {
                case '^':
                    currentFacing = '^';
                    currentPosition = new Coords(x, y);
                    break;
                case '>':
                    currentFacing = '>';
                    currentPosition = new Coords(x, y);
                    break;
                case 'v':
                    currentFacing = 'v';
                    currentPosition = new Coords(x, y);
                    break;
                case '<':
                    currentFacing = '<';
                    currentPosition = new Coords(x, y);
                    break;
            }
        }
        
        break;
    }
    
    positionsTraversed.Add(currentPosition);

    var leftGrid = false;
    while (!leftGrid)
    {
        switch (grid[currentPosition.Y][currentPosition.X])
        {
            case '^':
                if (currentPosition.Y == 0)
                {
                    leftGrid = true;
                    break;
                }
                // obstacle encountered, rotate 90 degrees
                if (grid[currentPosition.Y - 1][currentPosition.X] == '#')
                {
                    currentFacing = '>';
                    grid[currentPosition.Y][currentPosition.X] = currentFacing;
                    break;
                }
                
                // swap the positions
                (grid[currentPosition.Y][currentPosition.X], grid[currentPosition.Y - 1][currentPosition.X]) = (
                    grid[currentPosition.Y - 1][currentPosition.X], grid[currentPosition.Y][currentPosition.X]);
                currentPosition.Y--;
                break;
            case '>':
                if (currentPosition.X == gridSizeX)
                {
                    leftGrid = true;
                    break;
                }
                // obstacle encountered, rotate 90 degrees
                if (grid[currentPosition.Y][currentPosition.X + 1] == '#')
                {
                    currentFacing = 'v';
                    grid[currentPosition.Y][currentPosition.X] = currentFacing;
                    break;
                }
                // swap the positions
                (grid[currentPosition.Y][currentPosition.X], grid[currentPosition.Y][currentPosition.X + 1]) = (
                    grid[currentPosition.Y][currentPosition.X + 1], grid[currentPosition.Y][currentPosition.X]);
                currentPosition.X++;
                break;
            case 'v':
                if (currentPosition.Y == gridSizeY)
                {
                    leftGrid = true;
                    break;
                }
                // obstacle encountered, rotate 90 degrees
                if (grid[currentPosition.Y + 1][currentPosition.X] == '#')
                {
                    currentFacing = '<';
                    grid[currentPosition.Y][currentPosition.X] = currentFacing;
                    break;
                }
                // swap the positions
                (grid[currentPosition.Y][currentPosition.X], grid[currentPosition.Y + 1][currentPosition.X]) = (
                    grid[currentPosition.Y + 1][currentPosition.X], grid[currentPosition.Y][currentPosition.X]);
                currentPosition.Y++;
                break;
            case '<':
                if (currentPosition.X == 0)
                {
                    leftGrid = true;
                    break;
                }
                // obstacle encountered, rotate 90 degrees
                if (grid[currentPosition.Y][currentPosition.X - 1] == '#')
                {
                    currentFacing = '^';
                    grid[currentPosition.Y][currentPosition.X] = currentFacing;
                    break;
                }
                // swap the positions
                (grid[currentPosition.Y][currentPosition.X], grid[currentPosition.Y][currentPosition.X - 1]) = (
                    grid[currentPosition.Y][currentPosition.X - 1], grid[currentPosition.Y][currentPosition.X]);
                currentPosition.X--;
                break;
        }
        
        positionsTraversed.Add(currentPosition);

        // check if we made 3 turns here if we could end up back at our current position
        var loopPosition = currentPosition;
        var loopDirection = GetTurnDirection(currentFacing);
        var loopGrid = grid.Select(row => row.ToArray()).ToArray();
switch (currentFacing)
{
    case '^':
        if (currentPosition.Y == 0) continue; // we're about to leave the grid so don't check
        // can't add a wall to somewhere we've already walked
        if (positionsTraversed.Contains(new Coords(currentPosition.X, currentPosition.Y - 1))) continue;
        loopGrid[currentPosition.Y - 1][currentPosition.X] = '#';
        break;
    case '>':
        if (currentPosition.X == gridSizeX) continue; // we're about to leave the grid so don't check
        // can't add a wall to somewhere we've already walked
        if (positionsTraversed.Contains(new Coords(currentPosition.X + 1, currentPosition.Y))) continue;
        loopGrid[currentPosition.Y][currentPosition.X + 1] = '#';
        break;
    case 'v':
        if (currentPosition.Y == gridSizeY) continue; // we're about to leave the grid so don't check
        // can't add a wall to somewhere we've already walked
        if (positionsTraversed.Contains(new Coords(currentPosition.X, currentPosition.Y + 1))) continue;
        loopGrid[currentPosition.Y + 1][currentPosition.X] = '#';
        break;
    case '<':
        if (currentPosition.X == 0) continue; // we're about to leave the grid so don't check
        // can't add a wall to somewhere we've already walked
        if (positionsTraversed.Contains(new Coords(currentPosition.X - 1, currentPosition.Y))) continue;
        loopGrid[currentPosition.Y][currentPosition.X - 1] = '#';
        break;
}

        List<(Coords, char)> visitedLocations = [ new(currentPosition, currentFacing) ];
        while (IsWallInDirection(loopDirection, loopGrid, loopPosition, out var nextPosition))
        {
            // don't count instances where we didn't move
            var entry = (nextPosition, loopDirection);
            if (visitedLocations.Contains(entry))
            {
                loopGrid[loopPosition.Y][loopPosition.X] = 'X'; // mark loop confirmed location
                loops++;
                var sr = new StreamWriter($"{loops}.txt", true);
                foreach (var line in loopGrid)
                {
                    sr.WriteLine(line);
                }
                sr.Close();
                break;
            }
            visitedLocations.Add((nextPosition, loopDirection));
            
            loopDirection = GetTurnDirection(loopDirection);
            loopPosition = nextPosition;
        }
    }

    return loops;
}

// position is the position you would collide with the wall, not the position of the wall
bool IsWallInDirection(char direction, char[][] grid, Coords currentPosition, out Coords position)
{
    switch (direction)
    {
        case '^':
            // check if there's a wall above
            for (var y = currentPosition.Y; y > 0; y--)
            {
                // grid[y][currentPosition.X] = grid[y][currentPosition.X] switch
                // {
                //     '-' => '+',
                //     '.' => '|',
                //     'X' => '|',
                //     _ => grid[y][currentPosition.X]
                // };
                if (grid[y - 1][currentPosition.X] != '#') continue;
                position = new Coords(currentPosition.X, y);
                return true;
            }
            break;
        case '>':
            // check if there's a wall to the right
            for (var x = currentPosition.X; x < grid[0].Length - 1; x++)
            {
                // grid[currentPosition.Y][x] = grid[currentPosition.Y][x] switch
                // {
                //     '|' => '+',
                //     '.' => '-',
                //     'X' => '-',
                //     _ => grid[currentPosition.Y][x]
                // };
                if (grid[currentPosition.Y][x + 1] != '#') continue;
                position = new Coords(x, currentPosition.Y);
                return true;
            }
            break;
        case 'v':
            // check if there's a wall below
            for (var y = currentPosition.Y; y < grid.Length - 1; y++)
            {
                // grid[y][currentPosition.X] = grid[y][currentPosition.X] switch
                // {
                //     '-' => '+',
                //     '.' => '|',
                //     'X' => '|',
                //     _ => grid[y][currentPosition.X]
                // };
                if (grid[y + 1][currentPosition.X] != '#') continue;
                position = new Coords(currentPosition.X, y);
                return true;
            }
            break;
        case '<':
            // check if there's a wall to the left
            for (var x = currentPosition.X; x > 0; x--)
            {
                // grid[currentPosition.Y][x] = grid[currentPosition.Y][x] switch
                // {
                //     '|' => '+',
                //     '.' => '-',
                //     'X' => '-',
                //     _ => grid[currentPosition.Y][x]
                // };
                if (grid[currentPosition.Y][x - 1] != '#') continue;
                position = new Coords(x, currentPosition.Y);
                return true;
            }
            break;
    }

    position = default;
    return false;
}

char GetTurnDirection(char currentDirection)
{
    return currentDirection switch
    {
        '^' => '>',
        '>' => 'v',
        'v' => '<',
        '<' => '^',
        _ => throw new ArgumentOutOfRangeException(nameof(currentDirection))
    };
}

internal struct Coords(int x, int y) : IEquatable<Coords>
{

    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    public override string ToString() => $"({X}, {Y})";
    
    public override bool Equals(object? obj)
    {
        return obj is Coords other && Equals(other);
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(Coords left, Coords right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Coords left, Coords right)
    {
        return !left.Equals(right);
    }

    public bool Equals(Coords other)
    {
        return X == other.X && Y == other.Y;
    }
}