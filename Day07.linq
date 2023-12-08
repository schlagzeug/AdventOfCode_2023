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
		List<CamelHand> hands = new List<CamelHand>();
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day07.txt");
		foreach (var line in File.ReadAllLines(filePath))
		{
			var l = line.Split(' ');
			hands.Add(new CamelHand(l[0], l[1]));
		}

		// have to sort 4 times to get it correct?????
		hands.Sort((h1, h2) => h1.CompareTo(h2));
		hands.Sort((h1, h2) => h1.CompareTo(h2));
		hands.Sort((h1, h2) => h1.CompareTo(h2));
		hands.Sort((h1, h2) => h1.CompareTo(h2));
		
		for (int i = 0; i < hands.Count; i++)
		{
			hands[i].Rank = i + 1;
		}
		
		//hands.Dump();
		long totalWinnings = hands.Sum(h1 => (long)(h1.Bid * h1.Rank));
		totalWinnings.Dump("Part 1");
	}
}

public class Part2
{
	public static void Run()
	{
		List<CamelHand> hands = new List<CamelHand>();
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day07.txt");
		foreach (var line in File.ReadAllLines(filePath))
		{
			var l = line.Split(' ');
			hands.Add(new CamelHand(l[0], l[1], true));
		}

		// have to sort 5 times to get it correct?????
		hands.Sort((h1, h2) => h1.CompareTo(h2));
		hands.Sort((h1, h2) => h1.CompareTo(h2));
		hands.Sort((h1, h2) => h1.CompareTo(h2));
		hands.Sort((h1, h2) => h1.CompareTo(h2));
		hands.Sort((h1, h2) => h1.CompareTo(h2));

		for (int i = 0; i < hands.Count; i++)
		{
			hands[i].Rank = i + 1;
		}

		//hands.Dump();
		long totalWinnings = hands.Sum(h1 => (long)(h1.Bid * h1.Rank));
		totalWinnings.Dump("Part 2");
	}
}

public class CamelHand : IComparable<CamelHand>
{
	public int Bid { get; set; }
	public List<CamelCard> Cards { get; set; }
	public HandType Type { get; set; }
	public int Rank { get; set; }
	private bool _isUsingJokers;
	
	public CamelHand(string cards, string bid, bool isUsingJokers = false)
	{
		_isUsingJokers = isUsingJokers;
		Cards = new List<CamelCard>();
		foreach (var card in cards)
		{
			Cards.Add(new CamelCard(card));
		}
		
		Bid = int.Parse(bid);
		
		Type = GetHandType();
	}

	private HandType GetHandType()
	{
		Dictionary<int, int> dict = new Dictionary<int, int>();
		foreach (var card in Cards)
		{
			if (dict.ContainsKey(card.Value))
			{
				dict[card.Value]++;
			}
			else
			{
				dict.Add(card.Value, 1);
			}
		}
		
		if (_isUsingJokers && dict.ContainsKey(11))
		{
			if (dict.Count == 1 || dict.Count == 2) return HandType.FiveOfAKind;
			if (dict.Count == 3)
			{
				// either 4 of a kind or full house
				if (dict[11] > 1) return HandType.FourOfAKind;
				if (dict.ContainsValue(2)) return HandType.FullHouse;
				return HandType.FourOfAKind;
			}
			if (dict.Count == 4) return HandType.ThreeOfAKind;
			if (dict.Count == 5) return HandType.OnePair;
		}
		
		if (dict.Count == 4) return HandType.OnePair;
		if (dict.Count == 3)
		{
			// either 3 of a kind or 2 pair
			foreach (var element in dict)
			{
				if (element.Value == 3) return HandType.ThreeOfAKind;
			}
			return HandType.TwoPair;
		}
		if (dict.Count == 2)
		{
			// either 4 of a kind or full house
			foreach (var element in dict)
			{
				if (element.Value == 4) return HandType.FourOfAKind;
			}
			return HandType.FullHouse;
		}
		if (dict.Count == 1) return HandType.FiveOfAKind;
		return HandType.HighCard;
	}
	
	public int CompareTo(CamelHand otherHand)
	{
		if (otherHand.Type == this.Type)
		{
			for (int i = 0; i < 5; i++)
			{
				int otherCardValue = (_isUsingJokers && otherHand.Cards[i].Value == 11) ?
					1 : otherHand.Cards[i].Value;
				int thisCardValue = (_isUsingJokers && this.Cards[i].Value == 11) ?
					1 : this.Cards[i].Value;
					
				if (otherCardValue == thisCardValue) continue;
				if (otherCardValue > thisCardValue) return -1;
				return 1;
			}
		}
		else if (otherHand.Type > this.Type)
		{
			return -1;
		}
		return 0;
	}
}

public class CamelCard
{
	public int Value { get; set; }

	public CamelCard(char card)
	{
		switch (card)
		{
			case 'T':
				Value = 10;
				break;
			case 'J':
				Value = 11;
				break;
			case 'Q':
				Value = 12;
				break;
			case 'K':
				Value = 13;
				break;
			case 'A':
				Value = 14;
				break;
			default:
				Value = int.Parse(card.ToString());
				break;
		}
	}
}

public enum HandType
{
	FiveOfAKind = 7,
	FourOfAKind = 6,
	FullHouse = 5,
	ThreeOfAKind = 4,
	TwoPair = 3,
	OnePair = 2,
	HighCard = 1
}