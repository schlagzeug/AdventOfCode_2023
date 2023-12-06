<Query Kind="Program" />

void Main()
{
	Part1.Run();
	Part2.Run();
}

public static class Part1
{
	public static void Run()
	{
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day01.txt");
		int answer = 0;
		foreach (var line in File.ReadAllLines(filePath))
		{
			int x = GetFirstNumber(line);
			int y = GetLastNumber(line);
			answer += (x * 10 + y);
		}
		answer.Dump("Part 1");
	}

	private static int GetLastNumber(string line)
	{
		foreach (var character in line.Reverse())
		{
			int x = -1;
			if (int.TryParse(character.ToString(), out x))
			{
				return x;
			}
		}
		return -1;
	}

	private static int GetFirstNumber(string line)
	{
		foreach (var character in line)
		{
			int x = -1;
			if (int.TryParse(character.ToString(), out x))
			{
				return x;
			}
		}
		return -1;
	}
}

public static class Part2
{
	public static void Run()
	{
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day01.txt");
		int answer = 0;
		foreach (var line in File.ReadAllLines(filePath))
		{
			int x = GetFirstNumber(line);
			int y = GetLastNumber(new String(line.Reverse().ToArray()));
			answer += (x * 10 + y);
		}
		answer.Dump("Part 2");
	}

	private static int GetLastNumber(string line)
	{
		int index = 10000;
		int value = -1;

		foreach (var number in reverseNumbers)
		{
			int localIndex = line.IndexOf(number);
			if (localIndex < index && localIndex != -1)
			{
				index = localIndex;
				switch (number)
				{
					case "eno":
					case "1":
						value = 1;
						break;
					case "owt":
					case "2":
						value = 2;
						break;
					case "eerht":
					case "3":
						value = 3;
						break;
					case "ruof":
					case "4":
						value = 4;
						break;
					case "evif":
					case "5":
						value = 5;
						break;
					case "xis":
					case "6":
						value = 6;
						break;
					case "neves":
					case "7":
						value = 7;
						break;
					case "thgie":
					case "8":
						value = 8;
						break;
					case "enin":
					case "9":
						value = 9;
						break;
				}
			}
		}

		return value;
	}

	private static int GetFirstNumber(string line)
	{
		int index = 100000;
		int value = -1;
		
		foreach (var number in numbers)
		{
			int localIndex = line.IndexOf(number);
			if (localIndex < index && localIndex != -1)
			{
				index = localIndex;
				switch (number)
				{
					case "one":
					case "1":
						value = 1;
						break;
					case "two":
					case "2":
						value = 2;
						break;
					case "three":
					case "3":
						value = 3;
						break;
					case "four":
					case "4":
						value = 4;
						break;
					case "five":
					case "5":
						value = 5;
						break;
					case "six":
					case "6":
						value = 6;
						break;
					case "seven":
					case "7":
						value = 7;
						break;
					case "eight":
					case "8":
						value = 8;
						break;
					case "nine":
					case "9":
						value = 9;
						break;
				}
			}
		}
		
		return value;
	}
	
	private static List<string> numbers = new List<string>(){
		"one", "two", "three", "four", "five", "six", "seven", "eight", "nine", 
		"1", "2", "3", "4", "5", "6", "7", "8", "9", };

	private static List<string> reverseNumbers = new List<string>(){
		"eno", "owt", "eerht", "ruof", "evif", "xis", "neves", "thgie", "enin",
		"1", "2", "3", "4", "5", "6", "7", "8", "9", };
}


