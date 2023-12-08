<Query Kind="Program" />

void Main()
{
	Part1.Run();
	Part2.Run();
}

public static class Part1
{
	private static List<string> myarray = new List<string>();
	
	public static void Run()
	{
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day03.txt");
		myarray = File.ReadAllLines(filePath).ToList();
		int total = 0;
		
		for (int y = 0; y < myarray.Count; y++)
		{
			List<char> numberlist = new List<char>();
			bool isValid = false;
			
			for (int x = 0; x < myarray[y].Length; x++)
			{
				if (char.IsDigit(myarray[y][x]))
				{
					numberlist.Add(myarray[y][x]);
					if (IsNextToSymbol(x, y))
					{
						isValid = true;
					}
					if (x != myarray[y].Length - 1)
					{
						continue;
					}
				}
				
				if (numberlist.Count > 0 && isValid)
				{
					total += GetNumber(numberlist);
				}
				numberlist.Clear();
				isValid = false;
			}
		}
		total.Dump("Part 1");
	}

	private static bool IsNextToSymbol(int x, int y)
	{
		if (IsSymbol(x - 1, y - 1) || IsSymbol(x, y - 1) || IsSymbol(x + 1, y - 1) ||
			IsSymbol(x - 1, y) 	   ||						IsSymbol(x + 1, y) 	   ||
			IsSymbol(x - 1, y + 1) || IsSymbol(x, y + 1) || IsSymbol(x + 1, y + 1))
		{
			return true;
		}

		return false;
	}
	
	private static bool IsSymbol(int x, int y)
	{
		try
		{
			if (!char.IsDigit(myarray[y][x]) && myarray[y][x] != '.')
			{
				return true;
			}
		}
		catch
		{
			return false;
		}
		
		return false;
	}
	
	private static int GetNumber(List<char> charlist)
	{
		string retVal = string.Empty;
		foreach (var character in charlist)
		{
			retVal += character;
		}
		return int.Parse(retVal);
	}
}

public static class Part2
{
	private static List<string> myarray = new List<string>();
	private static List<GearItem> gearlist = new List<GearItem>();
	private static GearItem gearItem;

	public static void Run()
	{
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day03.txt");
		myarray = File.ReadAllLines(filePath).ToList();

		for (int y = 0; y < myarray.Count; y++)
		{
			List<char> numberlist = new List<char>();
			bool isValid = false;
			for (int x = 0; x < myarray[y].Length; x++)
			{
				if (char.IsDigit(myarray[y][x]))
				{
					numberlist.Add(myarray[y][x]);
					if (IsNextToGear(x, y))
					{
						isValid = true;
					}
					if (x != myarray[y].Length - 1)
					{
						continue;
					}
				}

				if (numberlist.Count > 0 && isValid)
				{
					bool isEntered = false;
					foreach (var gear in gearlist)
					{
						if (gear.Equals(gearItem))
						{
							gear.PartList.Add(GetNumber(numberlist));
							isEntered = true;
							break;
						}
					}
					
					if (!isEntered)
					{
						gearItem.PartList.Add(GetNumber(numberlist));
						gearlist.Add(gearItem);
					}
				}
				
				gearItem = null;
				numberlist.Clear();
				isValid = false;
			}
		}

		int total = 0;
		foreach (var gear in gearlist)
		{
			if (gear.PartList.Count == 2)
			{
				// valid gear
				total += gear.PartList[0]*gear.PartList[1];
			}
		}

		total.Dump("Part 2");
	}

	private static bool IsNextToGear(int x, int y)
	{
		if (IsGear(x - 1, y - 1) || IsGear(x, y - 1) || IsGear(x + 1, y - 1) ||
			IsGear(x - 1, y)     ||                     IsGear(x + 1, y) ||
			IsGear(x - 1, y + 1) || IsGear(x, y + 1) || IsGear(x + 1, y + 1))
		{
			return true;
		}

		return false;
	}

	private static bool IsGear(int x, int y)
	{
		try
		{
			if (myarray[y][x] == '*')
			{
				gearItem = new GearItem(x, y);
				return true;
			}
		}
		catch
		{
			return false;
		}

		return false;
	}

	private static int GetNumber(List<char> charlist)
	{
		string retVal = string.Empty;
		foreach (var character in charlist)
		{
			retVal += character;
		}
		return int.Parse(retVal);
	}
}

public class GearItem
{
	public int X { get; set; }
	public int Y { get; set; }
	public List<int> PartList { get; set; }

	public GearItem(int x, int y)
	{
		this.X = x;
		this.Y = y;
		PartList = new List<int>();
	}

	public override bool Equals(object obj)
	{
		if (obj is GearItem)
		{
			var gi = obj as GearItem;
			if (this.X == gi.X && this.Y == gi.Y)
			{
				return true;
			}
		}
		return false;
	}
}