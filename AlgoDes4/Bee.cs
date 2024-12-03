public class Bee
{
    protected List<List<int>> graph;
    protected int[] colors;

    public Bee(List<List<int>> graph)
    {
        this.graph = graph;
        this.colors = new int[graph.Count];
        InitializeRandomColors();
    }

    // Ініціалізація випадкових кольорів для кожної вершини
    private void InitializeRandomColors()
    {
        Random rand = new Random();
        for (int i = 0; i < graph.Count; i++)
        {
            colors[i] = rand.Next(1, graph.Count + 1); // Випадковий колір для кожної вершини
        }
    }

    // Оновлення кольорів (експлуатація)
    public virtual void Update()
    {
        Random rand = new Random();

        // Вибираємо випадкову вершину для зміни кольору
        int vertexToChange = rand.Next(0, graph.Count);
        HashSet<int> neighborColors = new HashSet<int>();

        // Збираємо кольори сусідів
        foreach (var neighbor in graph[vertexToChange])
        {
            neighborColors.Add(colors[neighbor]);
        }

        // Шукаємо доступний колір для зміни
        int newColor = 1;
        while (neighborColors.Contains(newColor))
        {
            newColor++;
        }

        // Оновлюємо колір
        colors[vertexToChange] = newColor;
    }

    public int GetChromaticNumber()
    {
        // Обчислюємо хроматичне число
        return colors.Distinct().Count();
    }
}

public class BeeScout : Bee
{
    public BeeScout(List<List<int>> graph) : base(graph)
    {
    }

    // Оновлення кольорів для розвідника (дослідження)
    public override void Update()
    {
        Random rand = new Random();

        // Для розвідника змінюємо більшу кількість вершин
        int verticesToChange = rand.Next(1, graph.Count / 10);  // Змінюємо до 10% від усіх вершин
        for (int i = 0; i < verticesToChange; i++)
        {
            int vertexToChange = rand.Next(0, graph.Count);
            HashSet<int> neighborColors = new HashSet<int>();

            // Збираємо кольори сусідів для поточної вершини
            foreach (var neighbor in graph[vertexToChange])
            {
                neighborColors.Add(colors[neighbor]);
            }

            // Шукаємо доступний колір для зміни
            int newColor = 1;
            while (neighborColors.Contains(newColor))
            {
                newColor++;
            }

            // Оновлюємо колір
            colors[vertexToChange] = newColor;
        }
    }
}