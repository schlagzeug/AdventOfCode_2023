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
	private List<long> seeds = new List<long>();
	private ConverterCollection seedToSoil = new ConverterCollection();
	private ConverterCollection soilToFertilizer = new ConverterCollection();
	private ConverterCollection fertilizerToWater = new ConverterCollection();
	private ConverterCollection waterToLight = new ConverterCollection();
	private ConverterCollection lightToTemperature = new ConverterCollection();
	private ConverterCollection temperatureToHumidity = new ConverterCollection();
	private ConverterCollection humidityToLocation = new ConverterCollection();

	public void Run()
	{
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day05.txt");
		var file = File.ReadAllLines(filePath).ToList();
		SetUpSeedsAndConverters(file);
		var seedToLocation = new Dictionary<long,long>();
		
		foreach (var seed in seeds)
		{
			var soil = seedToSoil.Convert(seed);
			var fertilizer = soilToFertilizer.Convert(soil);
			var water = fertilizerToWater.Convert(fertilizer);
			var light = waterToLight.Convert(water);
			var temperature = lightToTemperature.Convert(light);
			var humidity = temperatureToHumidity.Convert(temperature);
			var location = humidityToLocation.Convert(humidity);
			
			seedToLocation.Add(seed, location);
		}
		
		var lowest = long.MaxValue;
		foreach (var seed in seedToLocation)
		{
			if (seed.Value < lowest)
			{
				lowest = seed.Value;
			}
		}
		
		lowest.Dump("Part 1");
	}

	private void SetUpSeedsAndConverters(List<string> file)
	{
		FileSection fileSection = FileSection.seeds;
		
		foreach (var line in file)
		{
			if (line.StartsWith("seeds"))
			{
				var split = line.Split(' ');
				for (int i = 1; i < split.Count(); i++)
				{
					seeds.Add(long.Parse(split[i]));
				}
			}
			else if (line.StartsWith("seed-to-soil"))
			{
				fileSection = FileSection.seedToSoil;
			}
			else if (line.StartsWith("soil-to-fertilizer"))
			{
				fileSection = FileSection.soilToFertilizer;
			}
			else if (line.StartsWith("fertilizer-to-water"))
			{
				fileSection = FileSection.fertilizerToWater;
			}
			else if (line.StartsWith("water-to-light"))
			{
				fileSection = FileSection.waterToLight;
			}
			else if (line.StartsWith("light-to-temperature"))
			{
				fileSection = FileSection.lightToTemperature;
			}
			else if (line.StartsWith("temperature-to-humidity"))
			{
				fileSection = FileSection.temperatureToHumidity;
			}
			else if (line.StartsWith("humidity-to-location"))
			{
				fileSection = FileSection.humidityToLocation;
			}
			else if (string.IsNullOrEmpty(line))
			{
				continue;
			}
			else
			{
				switch (fileSection)
				{
					case FileSection.seedToSoil:
					seedToSoil.Add(new Converter(line));
						break;
					case FileSection.soilToFertilizer:
					soilToFertilizer.Add(new Converter(line));
						break;
					case FileSection.fertilizerToWater:
					fertilizerToWater.Add(new Converter(line));
						break;
					case FileSection.waterToLight:
					waterToLight.Add(new Converter(line));
						break;
					case FileSection.lightToTemperature:
					lightToTemperature.Add(new Converter(line));
						break;
					case FileSection.temperatureToHumidity:
					temperatureToHumidity.Add(new Converter(line));
						break;
					case FileSection.humidityToLocation:
					humidityToLocation.Add(new Converter(line));
						break;
					default:
						break;
				}
			}
		}
	}

	private enum FileSection
	{
		seeds,
		seedToSoil,
		soilToFertilizer,
		fertilizerToWater,
		waterToLight,
		lightToTemperature,
		temperatureToHumidity,
		humidityToLocation		
	}
}

public class Part2
{
	private List<long> seeds = new List<long>();
	private ConverterCollection seedToSoil = new ConverterCollection();
	private ConverterCollection soilToFertilizer = new ConverterCollection();
	private ConverterCollection fertilizerToWater = new ConverterCollection();
	private ConverterCollection waterToLight = new ConverterCollection();
	private ConverterCollection lightToTemperature = new ConverterCollection();
	private ConverterCollection temperatureToHumidity = new ConverterCollection();
	private ConverterCollection humidityToLocation = new ConverterCollection();

	public void Run()
	{
		string filePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Day05.txt");
		var file = File.ReadAllLines(filePath).ToList();
		SetUpSeedsAndConverters(file);
		var seedToLocation = new Dictionary<long, long>();

		var lowest = long.MaxValue;
		for (int i = 0; i < seeds.Count; i += 2)
		{
			var start = seeds[i];
			var count = seeds[i + 1];
			for (long j = start; j <= start + count; j++)
			{
				var soil = seedToSoil.Convert(j);
				var fertilizer = soilToFertilizer.Convert(soil);
				var water = fertilizerToWater.Convert(fertilizer);
				var light = waterToLight.Convert(water);
				var temperature = lightToTemperature.Convert(light);
				var humidity = temperatureToHumidity.Convert(temperature);
				var location = humidityToLocation.Convert(humidity);

				if (location < lowest)
				{
					lowest = location;
				}
			}
		}

		lowest.Dump("Part 2");
	}

	private void SetUpSeedsAndConverters(List<string> file)
	{
		FileSection fileSection = FileSection.seeds;

		foreach (var line in file)
		{
			if (line.StartsWith("seeds"))
			{
				var split = line.Split(' ');
				for (int i = 1; i < split.Count(); i++)
				{
					seeds.Add(long.Parse(split[i]));
				}
			}
			else if (line.StartsWith("seed-to-soil"))
			{
				fileSection = FileSection.seedToSoil;
			}
			else if (line.StartsWith("soil-to-fertilizer"))
			{
				fileSection = FileSection.soilToFertilizer;
			}
			else if (line.StartsWith("fertilizer-to-water"))
			{
				fileSection = FileSection.fertilizerToWater;
			}
			else if (line.StartsWith("water-to-light"))
			{
				fileSection = FileSection.waterToLight;
			}
			else if (line.StartsWith("light-to-temperature"))
			{
				fileSection = FileSection.lightToTemperature;
			}
			else if (line.StartsWith("temperature-to-humidity"))
			{
				fileSection = FileSection.temperatureToHumidity;
			}
			else if (line.StartsWith("humidity-to-location"))
			{
				fileSection = FileSection.humidityToLocation;
			}
			else if (string.IsNullOrEmpty(line))
			{
				continue;
			}
			else
			{
				switch (fileSection)
				{
					case FileSection.seedToSoil:
						seedToSoil.Add(new Converter(line));
						break;
					case FileSection.soilToFertilizer:
						soilToFertilizer.Add(new Converter(line));
						break;
					case FileSection.fertilizerToWater:
						fertilizerToWater.Add(new Converter(line));
						break;
					case FileSection.waterToLight:
						waterToLight.Add(new Converter(line));
						break;
					case FileSection.lightToTemperature:
						lightToTemperature.Add(new Converter(line));
						break;
					case FileSection.temperatureToHumidity:
						temperatureToHumidity.Add(new Converter(line));
						break;
					case FileSection.humidityToLocation:
						humidityToLocation.Add(new Converter(line));
						break;
					default:
						break;
				}
			}
		}
	}

	private enum FileSection
	{
		seeds,
		seedToSoil,
		soilToFertilizer,
		fertilizerToWater,
		waterToLight,
		lightToTemperature,
		temperatureToHumidity,
		humidityToLocation
	}
}

public class Converter
{
	public long Destination { get; set; }
	public long Source { get; set; }
	public long Count { get; set; }
	
	public Converter(string x)
	{
		var split = x.Split(' ');
		Destination = long.Parse(split[0]);
		Source = long.Parse(split[1]);
		Count = long.Parse(split[2]);
	}
	public long Convert(long source)
	{
		if (Source > source || source > Source + Count)
		{
			return source;
		}
		
		return Destination + (source - Source);
	}
}

public class ConverterCollection
{
	public List<Converter> Collection = new List<Converter>();
	
	public void Add(Converter c)
	{
		Collection.Add(c);
	}
	public long Convert(long source)
	{
		foreach (var converter in Collection)
		{
			var destination = converter.Convert(source);
			if (destination != source)
			{
				return destination;
			}
		}
		
		return source;
	}
}