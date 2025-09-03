public class StrongestBeam
{
    public static void Main()
    {
        Console.Write("Ingrese la viga:");
        string viga = Console.ReadLine()!;

        if (EsValida(viga))
        {
            if (SoportaPeso(viga))
            {
                Console.WriteLine("La viga soporta el peso!");
            }
            else
            {
                Console.WriteLine("La viga NO soporta el peso!");
            }
        }
        else
        {
            Console.WriteLine("La viga está mal construida!");
        } 
    }

    public static bool EsValida(string viga)
    {
        string Base = viga.Substring(0, 1);
        if (!(Base.Equals("%") || Base.Equals("&") || Base.Equals("#")))
            return false;

        int n = viga.Length;
        int conCon = 0;
        string pieza = "";

        for (int i = 1; i < n; i++) 
        {
            pieza = viga.Substring(i, 1);
            if (!(pieza.Equals("=") || pieza.Equals("*")))
                return false;
        }

        if (pieza.Equals("*"))
        {
            conCon++;
        }
        else
        {
            conCon = 0;
        }
        if (conCon == 2)
        {
            return false;
        }

        return true;
    }

    public static bool SoportaPeso(string viga)
    {
        string Base = viga.Substring(0, 1);

        int n = viga.Length;
        int pesoTotal = 0;
        int pesoSegmento = 0;
        string pieza = "";

        for (int i = 1; i < n; i++)
        {
            pieza = viga.Substring(i, 1);
            if (pieza.Equals("="))
            {
                pesoSegmento++;
            }
            else 
            {
                pesoTotal += pesoSegmento * 3;
                pesoSegmento = 0;
            }
        }

        pesoTotal += pesoSegmento;

        int pesoBase = 0;
        switch (Base)
        {
            case "%":
                pesoBase = 10;
                break;
            case "&":
                pesoBase = 30; 
                break;
            case "#":
                pesoBase = 90;
                break;
        }

        return pesoBase >= pesoTotal;
    }
}
