<Query Kind="Program" />

void Main()
{
	var x = new Part1();
	x.Run();
	
	var y = new Part2();
	y.Run();
}

public class Part1
{
	private List<string> myarray = new List<string>();
	
	public void Run()
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

	private bool IsNextToSymbol(int x, int y)
	{
		if (IsSymbol(x - 1, y - 1) || IsSymbol(x, y - 1) || IsSymbol(x + 1, y - 1) ||
			IsSymbol(x - 1, y) 	   ||						IsSymbol(x + 1, y) 	   ||
			IsSymbol(x - 1, y + 1) || IsSymbol(x, y + 1) || IsSymbol(x + 1, y + 1))
		{
			return true;
		}

		return false;
	}
	
	private bool IsSymbol(int x, int y)
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
	
	private int GetNumber(List<char> charlist)
	{
		string retVal = string.Empty;
		foreach (var character in charlist)
		{
			retVal += character;
		}
		return int.Parse(retVal);
	}
}

public class Part2
{
	private List<string> myarray = new List<string>();
	private List<GearItem> gearlist = new List<GearItem>();
	private GearItem gearItem;

	public void Run()
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

	private bool IsNextToGear(int x, int y)
	{
		if (IsGear(x - 1, y - 1) || IsGear(x, y - 1) || IsGear(x + 1, y - 1) ||
			IsGear(x - 1, y)     ||                     IsGear(x + 1, y) ||
			IsGear(x - 1, y + 1) || IsGear(x, y + 1) || IsGear(x + 1, y + 1))
		{
			return true;
		}

		return false;
	}

	private bool IsGear(int x, int y)
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

	private int GetNumber(List<char> charlist)
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