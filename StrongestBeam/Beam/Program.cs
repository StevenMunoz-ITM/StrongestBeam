class ProgramaViga
{
    static void Main()
    {
        string opcionContinuar;
        do
        {
            Console.Write("Ingrese la viga: ");
            string viga = Console.ReadLine()!;

            if (!LaVigaEstaBienConstruida(viga))
            {
                Console.WriteLine("La viga está mal construida!");
            }
            else if (LaBaseSoportaElPeso(viga))
            {
                Console.WriteLine("La viga soporta el peso!");
            }
            else
            {
                Console.WriteLine("La viga NO soporta el peso!");
            }

            Console.Write("¿Desea continuar? (S/N): ");
            opcionContinuar = Console.ReadLine()!.Trim().ToUpper();
        } while (opcionContinuar == "S");
    }

    static bool LaVigaEstaBienConstruida(string viga)
    {
        if (string.IsNullOrEmpty(viga))
            return false;

        if (viga[0] != '%' && viga[0] != '&' && viga[0] != '#')
            return false;

        bool ultimaFueConexion = false;
        int i = 1;
        while (i < viga.Length)
        {
            if (viga[i] == '=')
            {
                ultimaFueConexion = false;
                i++;
            }
            else if (viga[i] == '*')
            {
                if (ultimaFueConexion)
                    return false;
                if (i == 1 || viga[i - 1] != '=') return false;
                ultimaFueConexion = true;
                i++;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    static bool LaBaseSoportaElPeso(string viga)
    {
        int resistencia = viga[0] switch
        {
            '%' => 10,
            '&' => 30,
            '#' => 90,
             _ => 0
        };

        int pesoTotal = 0;
        int i = 1;
        while (i < viga.Length)
        {
            int largueros = 0;
            while (i < viga.Length && viga[i] == '=')
            {
                largueros++;
                i++;
            }

            for (int j = 1; j <= largueros; j++)
                pesoTotal += j;

            if (i < viga.Length && viga[i] == '*')
            {
                pesoTotal += largueros * 2;
                i++;
            }
        }
        return resistencia >= pesoTotal;
    }
}