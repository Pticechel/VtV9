using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int size = 5; // размер матрицы
        int[,] matrix = new int[size, size]; // инициализация матрицы

        // генерация матрицы случайных чисел
        Task generateTask = Task.Run(() =>
        {
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = rnd.Next(10);
                }
            }
        });

        // расчет суммы элементов
        Task<int> sumTask = generateTask.ContinueWith(t =>
        {
            int sum = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    sum += matrix[i, j];
                }
            }
            return sum;
        });

        // поиск максимального элемента
        Task<int> maxTask = generateTask.ContinueWith(t =>
        {
            int max = matrix[0, 0];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] > max)
                    {
                        max = matrix[i, j];
                    }
                }
            }
            return max;
        });

        // ожидание завершения задач и вывод результатов
        Task.WaitAll(sumTask, maxTask);
        Console.WriteLine($"Сумма элементов: {sumTask.Result}");
        Console.WriteLine($"Максимальный элемент: {maxTask.Result}");
    }
}