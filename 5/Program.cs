using System.Collections.ObjectModel;

const string input = "input.txt";

if (!File.Exists(input))
{
    Console.WriteLine("No input file found, aborting!");
    return;
}

List<KeyValuePair<int, int>> orderingRules = [];
var medianSum = 0;
var fixedMedianSum = 0;

using var sr = File.OpenText(input);
while (sr.ReadLine() is { } line)
{
    if (line.Trim() == "") continue;
    
    // line contains an ordering rule
    if (line.Contains('|'))
    {
        var rule = line.Split('|');
        if (rule.Length != 2)
        {
            Console.WriteLine($"Invalid split length ({rule.Length}) in input, at \"{line}\" aborting!");
            return;
        }
        
        orderingRules.Add(new KeyValuePair<int, int>(int.Parse(rule[0]), int.Parse(rule[1])));
        continue;
    }
    
    // line contains a printing order
    var pages = line.Split(',').Select(int.Parse).ToArray();
    if (IsValidPrintingOrder(pages))
    {
        //Console.WriteLine($"Valid Rule: {string.Join(',', pages)}");
        var middle = pages.Length / 2;
        medianSum += pages[middle];
    }
    else
    {
        var fixedOrder = FixInvalidPageOrder(pages);
        while (!IsValidPrintingOrder(fixedOrder))
        {
            fixedOrder = FixInvalidPageOrder(fixedOrder);
        }
        var middle = pages.Length / 2;
        fixedMedianSum += fixedOrder[middle];
    }
}
Console.WriteLine($"Median Sum: {medianSum}");
Console.WriteLine($"Fixed Median Sum: {fixedMedianSum}");
return;

bool IsValidPrintingOrder(int[] order)
{
    foreach (var orderingRule in orderingRules)
    {
        if (!order.Contains(orderingRule.Key)) continue;
        var keyIndex = Array.FindIndex(order, v => v == orderingRule.Key);
        for (var i = 0; i < keyIndex; i++)
        {
            // make sure that the ordering rule is valid by checking pages before this one
            if (order[i] == orderingRule.Value)
            {
                return false;
            }
        }
    }
    return true;
}

// only fixes the first invalid occurance found
int[] FixInvalidPageOrder(int[] order)
{
    var list = new ObservableCollection<int>(order);
    foreach (var orderingRule in orderingRules)
    {
        if (!order.Contains(orderingRule.Key)) continue;
        var keyIndex = Array.FindIndex(order , v => v == orderingRule.Key);
        for (var i = 0; i < keyIndex; i++)
        {
            if (order[i] != orderingRule.Value) continue;
            // invalid rule found fix it by moving the page
            list.Move(keyIndex, i);
            // we have to bail out here because we potentially invalidated previous entries
            return list.ToArray();
        }
    }
    // no invalid entries found?
    return list.ToArray();
}