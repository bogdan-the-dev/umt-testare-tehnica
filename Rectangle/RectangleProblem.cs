using Microsoft.VisualBasic.CompilerServices;

namespace Rectangle;

public class RectangleProblem
{
    public static int Compute()
    {
        Dictionary<int, Dictionary<int, bool>> points = new Dictionary<int, Dictionary<int, bool>>();
        string[] allPointsString = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "../../../input.txt")).Replace("(","").Replace(")","").Split(",");
        List<KeyValuePair<int, int>> allPoints = new List<KeyValuePair<int, int>>();
        int nrRectangle = 0;
        for (int i = 0; i < allPointsString.Length; i += 2)
        {
            int x = IntegerType.FromString(allPointsString[i]), y = IntegerType.FromString(allPointsString[i + 1]);
            allPoints.Add(new KeyValuePair<int, int>(x,y));
            if (!points.ContainsKey(x))
            {
                points[x] = new Dictionary<int, bool>();
            } 
            points[x][y] = true;
        }

        for (int i = 0; i < allPoints.Count; i++)
        {
            for (int j = 0; j < allPoints.Count; j++)
            {
                KeyValuePair<int, int> point1 = allPoints[i], point2 = allPoints[j];
                if (!(point1.Key == point2.Key || point1.Value == point2.Value))
                {
                    if (points[point1.Key].ContainsKey(point2.Value) && points[point2.Key].ContainsKey(point1.Value))
                    {
                        nrRectangle++;
                    }
                }
                
            }
        }
        return nrRectangle / 4;
    }
}