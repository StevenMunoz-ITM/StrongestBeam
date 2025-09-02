class BeamProgram
{
    static void Main()
    {
        string continueOption;
        do
        {
            Console.Write("Ingrese la viga: ");
            string beam = Console.ReadLine()!;

            if (!IsBeamWellConstructed(beam))
            {
                Console.WriteLine("La viga está mal construida!");
            }
            else if (DoesBaseSupportWeight(beam))
            {
                Console.WriteLine("La viga soporta el peso!");
            }
            else
            {
                Console.WriteLine("La viga NO soporta el peso!");
            }

            Console.Write("¿Desea continuar? (S/N): ");
            continueOption = Console.ReadLine()!.Trim().ToUpper();
        } while (continueOption == "S");
    }

    static bool IsBeamWellConstructed(string beam)
    {
        if (string.IsNullOrEmpty(beam))
            return false;

        if (beam[0] != '%' && beam[0] != '&' && beam[0] != '#')
            return false;

        bool lastWasConnection = false;
        int i = 1;
        while (i < beam.Length)
        {
            if (beam[i] == '=')
            {
                lastWasConnection = false;
                i++;
            }
            else if (beam[i] == '*')
            {
                if (lastWasConnection)
                    return false;
                if (i == 1 || beam[i - 1] != '=') return false;
                lastWasConnection = true;
                i++;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    static bool DoesBaseSupportWeight(string beam)
    {
        int resistance = beam[0] switch
        {
            '%' => 10,
            '&' => 30,
            '#' => 90,
            _ => 0
        };

        int totalWeight = 0;
        int i = 1;
        while (i < beam.Length)
        {
            int stringers = 0;
            while (i < beam.Length && beam[i] == '=')
            {
                stringers++;
                i++;
            }

            for (int j = 1; j <= stringers; j++)
                totalWeight += j;

            if (i < beam.Length && beam[i] == '*')
            {
                totalWeight += stringers * 2;
                i++;
            }
        }
        return resistance >= totalWeight;
    }
}