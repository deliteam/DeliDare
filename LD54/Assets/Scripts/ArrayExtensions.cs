using System;
using System.Linq;

public static class ArrayExtensions
{
    public static void Shuffle2D<T>(this T[,] array)
    {
        Random random = new Random();

        int rowCount = array.GetLength(0);
        int colCount = array.GetLength(1);

        // Flatten the 2D array into a 1D array
        T[] flattenedArray = new T[rowCount * colCount];
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                flattenedArray[i * colCount + j] = array[i, j];
            }
        }

        // Shuffle the 1D array
        int n = flattenedArray.Length;
        for (int i = 0; i < n; i++)
        {
            int randIndex = i + random.Next(n - i);
            T temp = flattenedArray[i];
            flattenedArray[i] = flattenedArray[randIndex];
            flattenedArray[randIndex] = temp;
        }

        // Unflatten the shuffled 1D array back into a 2D array
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                array[i, j] = flattenedArray[i * colCount + j];
            }
        }
    }
}