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
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day02.txt");
		int total = 0;
		foreach (var line in File.ReadAllLines(filePath))
		{
			var g = new Game(line);
			if (g.IsPossible(12, 13, 14))
			{
				total += g.ID;
			}
		}
		
		total.Dump("Part 1");
	}
}

public static class Part2
{
	public static void Run()
	{
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day02.txt");
		int total = 0;
		foreach (var line in File.ReadAllLines(filePath))
		{
			var g = new Game(line);
			total += g.Power;
		}

		total.Dump("Part 2");
	}
}

public class Game
{
	public int ID { get; private set; }
	public int Power
	{
		get
		{
			return maxRed * maxGreen * maxBlue;
		}
	}
	
	private int maxRed = 0;
	private int maxGreen = 0;
	private int maxBlue = 0;
	
	public Game(string game)
	{
		var split = game.Split(':');
		ID = int.Parse(split[0].Trim().Split(' ')[1]);
		ParsePulls(split[1]);
	}
	
	public bool IsPossible(int red, int green, int blue)
	{
		return red >= maxRed && green >= maxGreen && blue >= maxBlue;
	}
	
	private void ParsePulls(string pulls)
	{
		foreach (var pull in pulls.Trim().Split(';'))
		{
			foreach (var colors in pull.Trim().Split(','))
			{
				var color = colors.Trim().Split(' ');
				if (string.Equals(color[1], "red"))
				{
					int amt = int.Parse(color[0]);
					if (amt > maxRed)
					{
						maxRed = amt;
					}
				}
				else if (string.Equals(color[1], "green"))
				{
					int amt = int.Parse(color[0]);
					if (amt > maxGreen)
					{
						maxGreen = amt;
					}
				}
				else if (string.Equals(color[1], "blue"))
				{
					int amt = int.Parse(color[0]);
					if (amt > maxBlue)
					{
						maxBlue = amt;
					}
				}
			}
		}
	}
}
