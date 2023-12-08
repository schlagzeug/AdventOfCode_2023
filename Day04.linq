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
		int total = 0;
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day04.txt");
		foreach (var line in File.ReadAllLines(filePath))
		{
			var x = new Card(line);
			total += x.Points;
		}
		total.Dump("Part 1");
	}
}

public static class Part2
{
	private static Dictionary<int, int> myDict = new Dictionary<int, int>();
	private static List<Card> cards = new List<Card>();
	
	public static void Run()
	{
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day04.txt");
		foreach (var line in File.ReadAllLines(filePath))
		{
			var x = new Card(line);
			myDict.Add(x.CardNumber, 1);
			cards.Add(x);
		}

		foreach (var card in cards)
		{
			PopulateDownstreamCards(card);
		}

		int total = 0;
		foreach (var item in myDict)
		{
			total += item.Value;
		}
		total.Dump("Part 2");
	}
	
	private static void PopulateDownstreamCards(Card x)
	{
		for (int i = 1; i <= x.NumberOfMatches; i++)
		{
			myDict[x.CardNumber + i]++;
			PopulateDownstreamCards(cards[x.CardNumber - 1 + i]);
		}
	}
}

public class Card
{
	public int CardNumber { get; set; }
	public int NumberOfMatches { get; set; }
	public List<int> WinningNumbers { get; set; }
	public List<int> RevealedNumbers { get; set; }
	public int Points { get; set; }

	public Card(string cardString)
	{
		var cardAndNumbers = cardString.Split(':');
		var cardNumber = cardAndNumbers[0].Replace("Card", string.Empty);
		CardNumber = int.Parse(cardNumber);
		var winningAndRevealed = cardAndNumbers[1].Split('|');
		var winning = winningAndRevealed[0].Split(' ');
		var revealed = winningAndRevealed[1].Split(' ');

		WinningNumbers = new List<int>();
		foreach (var num in winning)
		{
			int x = -1;
			if (int.TryParse(num, out x))
			{
				WinningNumbers.Add(x);
			}
		}

		RevealedNumbers = new List<int>();
		foreach (var num in revealed)
		{
			int x = -1;
			if (int.TryParse(num, out x))
			{
				RevealedNumbers.Add(x);
			}
		}

		foreach (var num in RevealedNumbers)
		{
			if (!WinningNumbers.Contains(num))
			{
				continue;
			}
			
			NumberOfMatches++;

			if (Points == 0)
			{
				Points = 1;
			}
			else
			{
				Points *= 2;
			}
		}
	}
}