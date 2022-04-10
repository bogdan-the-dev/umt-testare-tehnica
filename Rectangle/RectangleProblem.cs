using Microsoft.VisualBasic.CompilerServices;

namespace Rectangle;

public class RectangleProblem
{
    public static int Compute()
    {
        //in primul dictionar vom pune ca si key pozita pe axa OX, iar ca value avem un alt dictionar unde vom pune drept key coordonatele pe axa OY iar value un bool(nu il vom folosi). In acest fel,
        //putem vedea usor care punce sunt pe aceleasi axe
        Dictionary<int, Dictionary<int, bool>> points = new Dictionary<int, Dictionary<int, bool>>();
        //citim fisierul de intrare, dam discard la paranteze iar apoi split dupa caracterul ',', rezultand un array de stringui cu coordonatele punctelor
        string[] allPointsString = System.IO.File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "../../../input.txt")).Replace("(","").Replace(")","").Split(",");
        //vom folosi un array de perechi key value reprezentand punctele, in care key este coordonata pe axa ox, iar value coordonata pe axa oy
        List<KeyValuePair<int, int>> allPoints = new List<KeyValuePair<int, int>>();
        //initializam numarul de dreptunghiuri cu 0
        int nrRectangle = 0;
        //in acest for transformam coordonatele din string in int si le introducem in dictionar si in array-ul de puncte
        for (int i = 0; i < allPointsString.Length; i += 2)
        {
            int x = IntegerType.FromString(allPointsString[i]), y = IntegerType.FromString(allPointsString[i + 1]);
            allPoints.Add(new KeyValuePair<int, int>(x,y));
            //intai verificam daca pe acea axa oy exista un dictionar, iar daca nu exista il vom crea
            if (!points.ContainsKey(x))
            {
                points[x] = new Dictionary<int, bool>();
            } 
            //adaugam coordonata de pe oy in al 2lea dictionar
            points[x][y] = true;
        }
        //in aceste 2 for-uri vom lua toate perechile de 2 cate 2 puncte
        for (int i = 0; i < allPoints.Count; i++)
        {
            for (int j = i + 1; j < allPoints.Count; j++)
            {
                KeyValuePair<int, int> point1 = allPoints[i], point2 = allPoints[j];
                //verificam daca cele 2 puncte sunt pe aceleasi axe ox sau oy, in caz afirmativ trecem la urmatoarea pereche de puncte
                if (point1.Key != point2.Key && point1.Value != point2.Value)
                {
                    //aici verificam daca cele 2 puncte creaza diagonala unu dreptunghi. Daca exista un punct in dictionar la coordonata x a punctului 1, coordonata y a punctului 2 si inca un punct
                    //la coordonata x a punctului 2, coordonata y a punctului 1 inseamna ca aceste 2 puncte reprezinta diagonala dreptungului si incrementam numarul de dreptunghiuri
                    if (points[point1.Key].ContainsKey(point2.Value) && points[point2.Key].ContainsKey(point1.Value))
                    {
                        nrRectangle++;
                    }
                }
                
            }
        }
        //impartim numarul de dreptunghiuri la 2 deoarece prin parcurgerea punctelor in al 2-lea for de la j = i + 1 vom considera acealsi dreptunghi de 2 ori (in loc de 4 in cazul j = 0) deoarece consideram ambele diagonale
        return nrRectangle / 2;
    }
}