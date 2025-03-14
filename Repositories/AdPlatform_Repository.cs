public class AdPlatform_Repository
{
    private Dictionary<string, List<AdPlatform>> _adPlatformsByLocation = new Dictionary<string, List<AdPlatform>>();

    public void LoadFromFile(string filePath)
    {
        var tempPlatforms = new Dictionary<string, List<AdPlatform>>();

        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            var parts = line.Split(':');
            var name = parts[0];
            var locations = parts[1].Split(',').ToList();
            var adPlatform = new AdPlatform { Name = name, Locations = locations };

            foreach (var location in locations)
            {
                if (!tempPlatforms.ContainsKey(location))
                {
                    tempPlatforms[location] = new List<AdPlatform>();
                }
                tempPlatforms[location].Add(adPlatform);
            }
        }

        _adPlatformsByLocation = tempPlatforms;
    }

    public List<AdPlatform> GetPlatformsForLocation(string location)
    {
        if (_adPlatformsByLocation.TryGetValue(location, out var platforms))
        {
            return platforms;
        }
        return new List<AdPlatform>();
    }
}