<Query Kind="Program" />

void Main()
{
	Part1.Run();
	Part2.Run();
}

public class Part1
{
	public static void Run()
	{
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day06.txt");
		var file = File.ReadAllLines(filePath).ToList();
		
		var times = file[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
		var distances = file[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
		
		int result = 1;
		for (int i = 1; i < times.Length; i++)
		{
			var race = new Race(times[i], distances[i]);
			result *= race.NumberOfPossibleWins;
		}
		
		result.Dump("Part 1");
	}
}

public class Part2
{
	public static void Run()
	{
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day06.txt");
		var file = File.ReadAllLines(filePath).ToList();

		var time = file[0].Replace("Time:", string.Empty).Replace(" ", string.Empty);
		var distance = file[1].Replace("Distance:", string.Empty).Replace(" ", string.Empty);

		var race = new Race(time, distance);
		race.NumberOfPossibleWins.Dump("Part 2");
	}
}

public class Race
{
	public long Time { get; set; }
	public long Distance { get; set; }
	public int NumberOfPossibleWins
	{
		get
		{
			int count = 0;
			for (int timePressed = 0; timePressed <= Time; timePressed++)
			{
				long distance = (Time - timePressed) * timePressed;
				if (distance > Distance)
				{
					count++;
				}
			}

			return count;
		}
	}

	public Race(string time, string distance)
	{
		Time = long.Parse(time);
		Distance = long.Parse(distance);
	}
}