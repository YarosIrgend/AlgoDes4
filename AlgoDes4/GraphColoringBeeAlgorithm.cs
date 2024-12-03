public class GraphColoringBeeAlgorithm
{
    public int VerticesCount = 100;
    public int MaxDegree = 20;
    public int MinDegree = 1;
    public int BeesCount = 30;
    public int ScoutsCount = 3;
    public int MaxIterations = 1000;
    public int Interval = 20;

    private List<List<int>> graph;
    private List<Bee> bees;

    public GraphColoringBeeAlgorithm()
    {
        graph = new List<List<int>>(VerticesCount);
        bees = new List<Bee>(BeesCount);

        InitializeGraph();
        InitializeBees();
    }

    private void InitializeGraph()
    {
        Random rand = new Random();
        for (int i = 0; i < VerticesCount; i++)
        {
            graph.Add(new List<int>());
        }

        for (int i = 0; i < VerticesCount; i++)
        {
            int degree = rand.Next(MinDegree, MaxDegree + 1);
            var neighbors = new HashSet<int>();

            while (neighbors.Count < degree)
            {
                int neighbor = rand.Next(0, VerticesCount);
                if (neighbor != i)
                {
                    neighbors.Add(neighbor);
                }
            }

            foreach (var neighbor in neighbors)
            {
                graph[i].Add(neighbor);
                graph[neighbor].Add(i); // Оскільки граф неорієнтований
            }
        }
    }

    private void InitializeBees()
    {
        Random rand = new Random();
        for (int i = 0; i < BeesCount; i++)
        {
            if (i < ScoutsCount)
            {
                bees.Add(new BeeScout(graph)); // Створюємо розвідників
            }
            else
            {
                bees.Add(new Bee(graph)); // Створюємо звичайних бджіл
            }
        }
    }

    public void Run()
    {
        List<int> fitnessOverIterations = new List<int>();

        for (int iteration = 1; iteration <= MaxIterations; iteration++)
        {
            foreach (var bee in bees)
            {
                bee.Update(); // Оновлюємо кожну бджолу
            }

            if (iteration % Interval == 0)
            {
                int bestChromaticNumber = bees.Min(b => b.GetChromaticNumber());
                fitnessOverIterations.Add(bestChromaticNumber);
                Console.WriteLine($"Ітерацій: {iteration}: Хром. число = {bestChromaticNumber}");
            }
        }
    }
}