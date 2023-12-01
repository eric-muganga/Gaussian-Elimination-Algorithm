double[,] M =
{
    {0, 4, 2, -2},
    {-2, 3, 1, -7},
    {4, 5, 2, 4}
};


Console.WriteLine(string.Join(", ", SystemSolve(M)));

double[,] M2 =
{
    {1, 3, 5},
    {2, 6, 5}
};

Console.WriteLine(string.Join(", ", SystemSolve(M2)));

double[,] M3 =
{
    {1, 3, 5},
    {3, -2, 4},
    {4, -1, 9},
    {7, -3, 13}
};

Console.WriteLine(string.Join(", ", SystemSolve(M3)));


double[] SystemSolve(double[,] matrix)
{
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);
    int n = Math.Min(rows, cols - 1); // the number of variables
    double[] solution = new double[n];

    // Perform the Gaussian elimination
    for (int k = 0; k < n; k++)
    {
        // Find a row that has a non-zero value in the k-th column
        int pivot = FindPivot(matrix, k);

        // If no such row exists, the system has no or infinitely many solutions
        if (pivot == -1)
        {
            
            return new double[0];
            
        }

        // Swap this row with the k-th row
        SwapRows(matrix, pivot, k);

        // Scale the k-th row, so the leading entry is equal to 1
        ScaleRow(matrix, k, 1 / matrix[k, k]);

        // Remove the k-th coefficient from the other rows
        for (int i = 0; i < rows; i++)
        {
            if (i != k)
            {
                SubRow(matrix, i, k, matrix[i, k]);
            }
        }
    }

    // Check if the system is consistent
    for (int i = n; i < cols - 1; i++)
    {
        if (matrix[i, i] != 1)
        {
            // There is a row with non-zero constant term only, the system has no solution
            return new double[0];
        }
    }

    // function to find the first non-zero element in a column of a matrix
    int FindPivot(double[,] matrix, int col)
    {
        int rows = matrix.GetLength(0);
        for (int i = col; i < rows; i++)
        {
            if (matrix[i, col] != 0)
            {
                return i;
            }
        }
        return -1;
    }

    // function to swap two rows of a matrix
    void SwapRows(double[,] matrix, int row1, int row2)
    {
        int cols = matrix.GetLength(1);
        for (int j = 0; j < cols; j++)
        {
            double temp = matrix[row1, j];
            matrix[row1, j] = matrix[row2, j];
            matrix[row2, j] = temp;
        }
    }

    // function to scale a row of a matrix by a factor
    void ScaleRow(double[,] matrix, int row, double factor)
    {
        int cols = matrix.GetLength(1);
        for (int j = 0; j < cols; j++)
        {
            matrix[row, j] *= factor;
        }
    }

    // function to subtract a multiple of one row to another row of a matrix
    void SubRow(double[,] matrix, int row1, int row2, double factor)
    {
        int cols = matrix.GetLength(1);
        for (int j = 0; j < cols; j++)
        {
            matrix[row1, j] -= matrix[row2, j] * factor;
        }
    }

    // Read the solution from the last column

    for (int i = 0; i < n; i++)
    {
        solution[i] = matrix[i, cols - 1];
    }

    return solution;
}

