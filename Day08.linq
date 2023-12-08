<Query Kind="Program" />

void Main()
{
	var nodeNetwork = SetUpNodeNetwork();
	Part1.Run(nodeNetwork);
	Part2.Run(nodeNetwork);
}

public Dictionary<string, string> SetUpNodeNetwork()
{
	string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day08.txt");
	var file = File.ReadAllLines(filePath);
	var nodeNetwork = new Dictionary<string, string>();
	
	for (int i = 2; i < file.Length; i++)
	{
		var nodeStrings = file[i].Split('=');
		nodeNetwork.Add(nodeStrings[0].Trim(), nodeStrings[1].Replace("(", string.Empty).Replace(")", string.Empty));
	}
	
	return nodeNetwork.Dump("Node Network");
}

public static class Part1
{
	public static void Run(Dictionary<string, string> nodeNetwork)
	{
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day08.txt");
		var file = File.ReadAllLines(filePath);
		string path = file[0];
		string node = "AAA";
		int count = 0;
		for (int i = 0; !node.Equals("ZZZ"); i++)
		{
			var x = nodeNetwork[node].Split(',');
			if (path[i] == 'L') node = x[0].Trim();
			else node = x[1].Trim();
						
			count++;
			if (i == path.Length - 1) i = -1;
		}
		
		count.Dump("Part 1");
	}
}

public static class Part2
{
	public static void Run(Dictionary<string, string> nodeNetwork)
	{
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day08.txt");
		var file = File.ReadAllLines(filePath);
		string path = file[0];
		List<string> startingNodes = nodeNetwork.Where(n => n.Key.EndsWith("A"))
												.Select(n => n.Key)
												.ToList();
												
		var periods = new List<long>();
		foreach (var node in startingNodes)
		{
			periods.Add(FindPeriod(node, nodeNetwork, path));
		}
		
		periods.Dump();
		
		LCM(periods.ToArray()).Dump("Part 2");
	}

	static long FindPeriod(string startingNode, Dictionary<string, string> nodeNetwork, string path)
	{
		long count = 0;
		string node = startingNode;
		long periodLength = 0;
		bool doTrackPeriod = false;

		for (int i = 0; ; i++)
		{
			var x = nodeNetwork[node].Split(',');
			if (path[i] == 'L') node = x[0].Trim();
			else node = x[1].Trim();

			count++;
			if (node.EndsWith("Z"))
			{
				if (doTrackPeriod) break;

				doTrackPeriod = true;
			}

			if (doTrackPeriod) periodLength++;
			if (i == path.Length - 1) i = -1;
		}
		return periodLength;
	}

	static long GCD(long n1, long n2)
	{
		if (n2 == 0)
		{
			return n1;
		}
		else
		{
			return GCD(n2, n1 % n2);
		}
	}

	public static long LCM(long[] numbers)
	{
		return numbers.Aggregate((S, val) => S * val / GCD(S, val));
	}
}
