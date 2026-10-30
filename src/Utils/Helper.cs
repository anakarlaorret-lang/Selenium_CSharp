using System.IO;
using Newtonsoft.Json;

public class TestData
{
    public static dynamic LoadData()
    {
        var json = File.ReadAllText("testData.json");
        return JsonConvert.DeserializeObject<dynamic>(json)!;
    }
}
