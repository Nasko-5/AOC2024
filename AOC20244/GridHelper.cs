using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Dataflow;

namespace AOC2024
{
    public static class GridHelper<T>
    {
        public static IEnumerable<T> Flatten<T>(IEnumerable<IEnumerable<T>> input)
        {
            return input.SelectMany(a => a);
        }
        public static IEnumerable<T> Flatten<T>(T[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    yield return grid[i, j]; // Return each element one at a time
                }
            }
        }

        public static T[,] To2D<T>(T[][] source)
        {
            try
            {
                int FirstDim = source.Length;
                int SecondDim = source.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular

                var result = new T[FirstDim, SecondDim];
                for (int i = 0; i < FirstDim; ++i)
                    for (int j = 0; j < SecondDim; ++j)
                        result[i, j] = source[i][j];

                return result;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The given jagged array is not rectangular.");
            }
        }

        public static bool validIndex<T>(List<List<T>> matrix, int x, int y) 
        {
            if (x < 0 || y < 0) { return false; }
            else if (y >= matrix.Count() || x >= matrix[y].Count()) { return false; }
            return true;
        }
        public static bool validIndex<T>(T[,] matrix, int x, int y)
        {
            if (x < 0 || y < 0) { return false; }
            else if (y >= matrix.GetLength(1) || x >= matrix.GetLength(0)) { return false; }
            return true;
        }
        public static bool validIndex<T>(List<T> list, int x)
        {
            if (x < 0) { return false; }
            else if (x >= list.Count()) { return false; }
            return true;
        }
        public static bool validIndex<T>(T[] array, int x)
        {
            if (x < 0 ) { return false; }
            else if (x >= array.Length) { return false; }
            return true;
        }

        // 2D NEIGHBOR
        public static List<List<T>> GetNeighbors<T>(List<List<T>> matrix, int x, int y, int size)
        {
            List<List<T>> neighbors = new List<List<T>>();
            int ny = 0;
            for (int blocky = y-size; blocky < y+size; blocky++)
            {
                neighbors.Add(new List<T>());
                for (int blockx = x - size; blockx < x + size; blockx++)
                {
                    if (validIndex(matrix, blockx, blocky)) neighbors[ny].Add(matrix[blocky][blockx]);
                    else neighbors[ny].Add(default);
                } ny++;
            }

            return neighbors;
        }
        public static T[,] GetNeighbors<T>(T[,] matrix, int x, int y, int size)
        {
            T[,] neighbors = new T[(size*2)+1, (size*2)+1];

            int nx = 0;
            int ny = 0;
            for (int blocky = y - size; blocky < y + size; blocky++)
            {
                for (int blockx = x - size; blockx < x + size; blockx++)
                {
                    if (validIndex(matrix, blockx, blocky)) neighbors[nx, ny] = matrix[blocky, blockx];
                    nx++;
                } ny++;
            }

            return neighbors;
        }
        public static List<List<T>> GetNeighborsWrap<T>(List<List<T>> matrix, int x, int y, int size)
        {
            List<List<T>> neighbors = new List<List<T>>();
            int ny = 0;
            for (int blocky = y - size; blocky < y + size; blocky++)
            {
                neighbors.Add(new List<T>());
                for (int blockx = x - size; blockx < x + size; blockx++)
                {
                    neighbors[ny].Add(matrix[blocky % (size * 2) + 1][blockx % (size * 2) + 1]);
                } ny++;
            }
            return neighbors;
        }
        public static T[,] GetNeighborsWrap<T>(T[,] matrix, int x, int y, int size)
        {
            T[,] neighbors = new T[(size*2)+1, (size * 2) + 1];
            int nx = 0;
            int ny = 0;
            for (int blocky = y - size; blocky < y + size; blocky++)
            {
                for (int blockx = x - size; blockx < x + size; blockx++)
                {
                    neighbors[nx, ny] = matrix[blocky % (size * 2) + 1, blockx % (size * 2) + 1];
                    nx++;
                } ny++;
            }
            return neighbors;
        }

        // 1D NEIGHBOR
        public static List<T> GetNeighbors<T> (List<T> matrix, int x, int size) 
        {
            List<T> neighbors = new List<T>();

            for (int i = x - size; i < x+size; i++)
            {
                if (validIndex(matrix, x)) neighbors.Add(matrix[i]);
                else neighbors.Add(default);
            }

            return neighbors;
        }
        public static T[] GetNeighbors<T>(T[] matrix, int x, int size) 
        {
            T[] neighbors = new T[(size * 2) + 1];
            int nx = 0;

            for (int i = x - size; i < x + size; i++)
            {
                if (validIndex(matrix, x)) neighbors[nx] = matrix[i];;
                nx++;
            }

            return neighbors;
        }
        public static List<T> GetNeighborsWarp<T>(List<T> matrix, int x, int size)
        {
            List<T> neighbors = new List<T>();

            for (int i = x - size; i < x + size; i++)
            {
                neighbors.Add(matrix[i % (size*2)+1]);
            }

            return neighbors;
        }
        public static T[] GetNeighborsWarp<T>(T[] matrix, int x, int size)
        {
            T[] neighbors = new T[(size * 2) + 1];
            int nx = 0;

            for (int i = x - size; i < x + size; i++)
            {
                neighbors[nx] = matrix[i % (size*2)+1];
                nx++;
            }

            return neighbors;
        }

        // ROW
        public static T[] GetRow(T[,] matrix, int rowNumber, int from = 0, int to = int.MaxValue)
        {
            T[] row = new T[to-from];
            int x = 0;

            for (int i = from;  i < to; i++)
            {
                if (validIndex(matrix, i, rowNumber)) row[x] = matrix[rowNumber,i]; 
                x++;
            }

            return row;
        }
        public static List<T> GetRow(List<List<T>> matrix, int rowNumber, int from = 0, int to = int.MaxValue)
        {
            from = Math.Clamp(from, 0, matrix.First().Count());
            to = Math.Clamp(to, from, matrix.First().Count());

            return Enumerable.Range(from, to)
                    .Select(x => matrix[rowNumber][x])
                    .ToList();
        }

        // COLUMN
        public static T[] GetColumn(T[,] matrix, int columnNumber, int from = 0, int to = int.MaxValue)
        {

            T[] col = new T[to - from];
            int x = 0;

            for (int i = from; i < to; i++)
            {
                if (validIndex(matrix, i, columnNumber)) col[x] = matrix[i, columnNumber];
                x++;
            }

            return col;
        }

        public static List<T> GetColumn(List<List<T>> matrix, int columnNumber, int from = 0, int to = int.MaxValue)
        {

            from = Math.Clamp(from, 0, matrix.Count());
            to = Math.Clamp(to, from, matrix.Count());

            return Enumerable.Range(from, to)
                    .Select(x => matrix[x][columnNumber])
                    .ToList();
        }
    
        // DIAGONAL | F - Forward | B - Backward | C - Center | RL - Right to Left | LR - Left to Right |
        public static T[,] GetDiagonalFLR(T[,] matrix, int x, int y, int size)
        {
            T[,] dia = new T[size, size];

            for (int i = 0; i < size; i++)
            {
                if(validIndex(matrix,x+i,y+i)) dia[i,i] = matrix[y + i, x + i];
            }

            return dia;
        }
        public static T[,] GetDiagonalFRL(T[,] matrix, int x, int y, int size)
        {
            T[,] dia = new T[size, size];

            for (int i = 0; i < size; i++)
            {
                if (validIndex(matrix, x + size - i, y + i)) dia[i, i] = matrix[y + i, x + size - i];
            }

            return dia;
        }
        public static T[,] GetDiagonalBLR(T[,] matrix, int x, int y, int size)
        {
            T[,] dia = new T[size, size];

            for (int i = size - 1; i > -1; i--)
            {
                if (validIndex(matrix, x + i, y + i)) dia[i, i] = matrix[y + i, x + i];
            }

            return dia;
        }
        public static T[,] GetDiagonalBRL(T[,] matrix, int x, int y, int size)
        {
            T[,] dia = new T[size, size];

            for (int i = size - 1; i > -1; i--)
            {
                if (validIndex(matrix, x + size - i, y + i)) dia[i, i] = matrix[y + i, x + size - i];
            }

            return dia;
        }
        public static T[,] GetDiagonalCLR(T[,] matrix, int x, int y, int size)
        {
            T[,] dia = new T[(size * 2) + 1, (size * 2) + 1];

            int start = size * 2;
            int nx = x-start, ny = y-start;
            int dx = 0;

            for (int i = 0; i < (size * 2) + 1; i++)
            {
                if (validIndex(matrix, nx + i, ny + i)) dia[dx,dx] = matrix[ny + i, nx + i];
                dx++;
            }

            return dia;
        }
        public static T[,] GetDiagonalCRL(T[,] matrix, int x, int y, int size)
        {
            T[,] dia = new T[(size * 2) + 1, (size * 2) + 1];
            int startX = x + size, startY = y - size;
            for (int i = 0; i < (size * 2) + 1; i++)
            {
                int currentX = startX - i, currentY = startY + i;
                if (validIndex(matrix, currentX, currentY)) dia[i, i] = matrix[currentY, currentX];
            }
            return dia;
        }
        public static T[] GetDiagonalFLR1D(T[,] matrix, int x, int y, int size)
        {
            T[] flatDia = new T[size];

            for (int i = 0; i < size; i++)
            {
                if (validIndex(matrix, x + i, y + i)) flatDia[i] = matrix[y + i, x + i];
            }

            return flatDia;
        }
        public static T[] GetDiagonalFRL1D(T[,] matrix, int x, int y, int size)
        {
            T[] flatDia = new T[size];

            for (int i = 0; i < size; i++)
            {
                if (validIndex(matrix, (x + size)-i, y + i)) flatDia[i] = matrix[y + i, (x + size) - i];
            }

            return flatDia;
        }
        public static T[] GetDiagonalBLR1D(T[,] matrix, int x, int y, int size)
        {
            T[] flatDia = new T[size];

            for (int i = size-1; i > -1; i--)
            {
                if (validIndex(matrix, x + i, y + i)) flatDia[i] = matrix[y + i, x + i];
            }

            return flatDia;
        }
        public static T[] GetDiagonalBRL1D(T[,] matrix, int x, int y, int size)
        {
            T[] flatDia = new T[size];

            for (int i = size - 1; i > -1; i--)
            {
                if (validIndex(matrix, (x+size)-i, y + i)) flatDia[i] = matrix[(x + size) - i, y + i];
            }

            return flatDia;
        }
        public static T[] GetDiagonalCLR1D(T[,] matrix, int x, int y, int size)
        {
            T[] dia = new T[(size * 2) + 1];

            int start = size * 2;
            int nx = x - start, ny = y - start;
            int dx = 0;

            for (int i = 0; i < (size * 2) + 1; i++)
            {
                if (validIndex(matrix, nx + i, ny + i)) dia[dx] = matrix[nx + i, ny + i];
                dx++;
            }

            return dia;
        }
        public static T[] GetDiagonalCRL1D(T[,] matrix, int x, int y, int size)
        {
            T[] dia = new T[(size * 2) + 1];
            int startX = x + size, startY = y - size;
            for (int i = 0; i < (size * 2) + 1; i++)
            {
                int currentX = startX - i, currentY = startY + i;
                if (validIndex(matrix, currentX, currentY)) dia[i] = matrix[currentY, currentX];
            }
            return dia;
        }
        //public static List<List<T>> GetDiagonalFRL(List<List<T>> matrix, int x, int y, int size)
        //{
        //    List<List<T>> dia = new List<List<T>>();

        //    for (int i = 0; i < size; i++)
        //    {
        //        dia.Add(new List<T>());
        //        if (validIndex(matrix, x + i, y + i))
        //            dia[i].Add(matrix[y + i][x + i]);
        //        else
        //            dia[i].Add(default);
        //    }

        //    return dia;
        //}
        //public static List<List<T>> GetDiagonalFLR(List<List<T>> matrix, int x, int y, int size)
        //{
        //    List<List<T>> dia = new List<List<T>>();

        //    for (int i = 0; i < size; i++)
        //    {
        //        dia.Add(new List<T>());
        //        if (validIndex(matrix, x + size - i, y + i))
        //            dia[i].Add(matrix[y + i][x + size - i]);
        //        else
        //            dia[i].Add(default);
        //    }

        //    return dia;
        //}
        //public static List<List<T>> GetDiagonalBRL(List<List<T>> matrix, int x, int y, int size)
        //{
        //    List<List<T>> dia = new List<List<T>>();

        //    for (int i = size; i > 0; i--)
        //    {
        //        dia.Add(new List<T>());
        //        if (validIndex(matrix, x + i, y + i))
        //            dia[size - i].Add(matrix[y + i][x + i]);
        //        else
        //            dia[size - i].Add(default);
        //    }

        //    return dia;
        //}
        //public static List<List<T>> GetDiagonalBLR(List<List<T>> matrix, int x, int y, int size)
        //{
        //    List<List<T>> dia = new List<List<T>>();

        //    for (int i = size; i > 0; i--)
        //    {
        //        dia.Add(new List<T>());
        //        if (validIndex(matrix, x + size - i, y + i))
        //            dia[size - i].Add(matrix[y + i][x + size - i]);
        //        else
        //            dia[size - i].Add(default);
        //    }

        //    return dia;
        //}
        //public static List<List<T>> GetDiagonalCRL(List<List<T>> matrix, int x, int y, int size)
        //{
        //    List<List<T>> dia = new List<List<T>>();

        //    int start = size * 2;
        //    int nx = x - start, ny = y - start;

        //    for (int i = 0; i < (size * 2) + 1; i++)
        //    {
        //        dia.Add(new List<T>());
        //        if (validIndex(matrix, nx + i, ny + i))
        //            dia[i].Add(matrix[ny + i][nx + i]);
        //        else
        //            dia[i].Add(default);
        //    }

        //    return dia;
        //}
        //public static List<List<T>> GetDiagonalCLR(List<List<T>> matrix, int x, int y, int size)
        //{
        //    List<List<T>> dia = new List<List<T>>();

        //    int startX = x + size, startY = y - size;
        //    for (int i = 0; i < (size * 2) + 1; i++)
        //    {
        //        dia.Add(new List<T>());
        //        int currentX = startX - i, currentY = startY + i;
        //        if (validIndex(matrix, currentX, currentY))
        //            dia[i].Add(matrix[currentY][currentX]);
        //        else
        //            dia[i].Add(default);
        //    }

        //    return dia;
        //}
        //public static List<T> GetDiagonalFRL1D(List<List<T>> matrix, int x, int y, int size)
        //{
        //    List<T> flatDia = new List<T>();

        //    for (int i = 0; i < size; i++)
        //    {
        //        if (validIndex(matrix, x + i, y + i))
        //            flatDia.Add(matrix[y + i][x + i]);
        //        else
        //            flatDia.Add(default);
        //    }

        //    return flatDia;
        //}
        //public static List<T> GetDiagonalFLR1D(List<List<T>> matrix, int x, int y, int size)
        //{
        //    List<T> flatDia = new List<T>();

        //    for (int i = 0; i < size; i++)
        //    {
        //        if (validIndex(matrix, x + size - i, y + i))
        //            flatDia.Add(matrix[y + i][x + size - i]);
        //        else
        //            flatDia.Add(default);
        //    }

        //    return flatDia;
        //}
        //public static List<T> GetDiagonalBRL1D(List<List<T>> matrix, int x, int y, int size)
        //{
        //    List<T> flatDia = new List<T>();

        //    for (int i = size; i > 0; i--)
        //    {
        //        if (validIndex(matrix, x + i, y + i))
        //            flatDia.Add(matrix[y + i][x + i]);
        //        else
        //            flatDia.Add(default);
        //    }

        //    return flatDia;
        //}
        //public static List<T> GetDiagonalBLR1D(List<List<T>> matrix, int x, int y, int size)
        //{
        //    List<T> flatDia = new List<T>();

        //    for (int i = size; i > 0; i--)
        //    {
        //        if (validIndex(matrix, x + size - i, y + i))
        //            flatDia.Add(matrix[y + i][x + size - i]);
        //        else
        //            flatDia.Add(default);
        //    }

        //    return flatDia;
        //}
        //public static List<T> GetDiagonalCRL1D(List<List<T>> matrix, int x, int y, int size)
        //{
        //    List<T> flatDia = new List<T>();

        //    int start = size * 2;
        //    int nx = x - start, ny = y - start;

        //    for (int i = 0; i < (size * 2) + 1; i++)
        //    {
        //        if (validIndex(matrix, nx + i, ny + i))
        //            flatDia.Add(matrix[ny + i][nx + i]);
        //        else
        //            flatDia.Add(default);
        //    }

        //    return flatDia;
        //}
        //public static List<T> GetDiagonalCLR1D(List<List<T>> matrix, int x, int y, int size)
        //{
        //    List<T> flatDia = new List<T>();

        //    int startX = x + size, startY = y - size;
        //    for (int i = 0; i < (size * 2) + 1; i++)
        //    {
        //        int currentX = startX - i, currentY = startY + i;
        //        if (validIndex(matrix, currentX, currentY))
        //            flatDia.Add(matrix[currentY][currentX]);
        //        else
        //            flatDia.Add(default);
        //    }

        //    return flatDia;
        //}

    }
}