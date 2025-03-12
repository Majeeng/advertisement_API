public class AdPlatformRepository
{
    private List<AdPlatform> _adPlatforms = new List<AdPlatform>();
    public void LoadFromFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        _adPlatforms.Clear();

        foreach (var line in lines)
        {
            var parts = line.Split(':');
            var name = parts[0];
            var locations = parts[1].Split(',').ToList();
            _adPlatforms.Add(new AdPlatform { Name = name, Locations = locations });
        }
    }

    public List<AdPlatform> GetPlatformsForLocation(string location)
    {
        return _adPlatforms.Where(ap => ap.Locations.Any(loc => IsLocationMatched(location, loc))).ToList();
    }
    private bool IsLocationMatched(string location, string adLocation)
    {
        return location.StartsWith(adLocation);
    }
}