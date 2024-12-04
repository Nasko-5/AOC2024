using System.Collections;
using System.Collections.Generic;
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
        
        public static bool validIndex<T>(List<List<T>> matrix, int x, int y) 
        {
            if (x < 0 || y < 0) { return false; }
            else if (y > matrix.Count() || x > matrix.First().Count()) { return false; }
            return true;
        }
        public static bool validIndex<T>(T[,] matrix, int x, int y)
        {
            if (x < 0 || y < 0) { return false; }
            else if (y > matrix.GetLength(0) || x > matrix.GetLength(1)) { return false; }
            return true;
        }
        public static bool validIndex<T>(List<T> list, int x)
        {
            if (x < 0) { return false; }
            else if (x > list.Count()) { return false; }
            return true;
        }
        public static bool validIndex<T>(T[] array, int x)
        {
            if (x < 0 ) { return false; }
            else if (x > array.Length) { return false; }
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
                    if (validIndex(matrix, x, y)) neighbors[nx, ny] = matrix[blocky, blockx];
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
            
            from = Math.Clamp(from, 0, matrix.GetLength(1));
            to = Math.Clamp(from, from, matrix.GetLength(1));

            return Enumerable.Range(from, to)
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
        public static List<T> GetRow(List<List<T>> matrix, int rowNumber, int from = 0, int to = int.MaxValue)
        {
            from = Math.Clamp(from, 0, matrix.First().Count());
            to = Math.Clamp(from, from, matrix.First().Count());

            return Enumerable.Range(from, to)
                    .Select(x => matrix[rowNumber][x])
                    .ToList();
        }


        // COLUMN
        public static T[] GetColumn(T[,] matrix, int columnNumber, int from = 0, int to = int.MaxValue)
        {

            from = Math.Clamp(from, 0, matrix.GetLength(0));
            to = Math.Clamp(from, from, matrix.GetLength(0));

            return Enumerable.Range(from, to)
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }
        public static List<T> GetColumn(List<List<T>> matrix, int columnNumber, int from = 0, int to = int.MaxValue)
        {

            from = Math.Clamp(from, 0, matrix.Count());
            to = Math.Clamp(from, from, matrix.Count());

            return Enumerable.Range(from, to)
                    .Select(x => matrix[x][columnNumber])
                    .ToList();
        }
    
        // DIAGONAL

        public static T[] GetDiagonalF(T[,] matrix)
        {

        }
    }

        // Needed functions:

        // get neighbors  (with adjustable size)
        //
        // (no wrap)
        // GetNeighbors(IEnumerable<IEnumerable<T>> grid, int x, int y, int size) -> IEnumerable<IEnumerable<T>>
        // GetNeighbors(IEnumerable<IEnumerable<T>> grid, int x, int y, int size) -> IEnumerable<T>
        // GetNeighbors(IEnumerable<T> grid, int x, int y, int size) -> IEnumerable<T>
        // (wrap)
        // GetNeighborsWrap(IEnumerable<IEnumerable<T>> grid, int x, int y, int size) -> IEnumerable<IEnumerable<T>>
        // GetNeighborsWrap(IEnumerable<IEnumerable<T>> grid, int x, int y, int size) -> IEnumerable<T>
        // GetNeighborsWrap(IEnumerable<T> grid, int x, int y, int size) -> IEnumerable<T>

        // ex. grid     2d          1d      2d big
        // a b c d e  (3,4 1)     (4,3 1)    (2,3 2)    (wrapping)
        // f g h i j   . . .                . . . . .   e a b c d
        // k l m n o   . o .       . o .    . . . . .   j f g h i
        // p q r s t   . . .         v      . . o . . > o k l m n or {e,a,b,c,d,j,f,g,h,i,o,k ... }
        // u v w x y     v         {m,n,o}  . . . . .   t p q r s
        //             b c d                . . . . .   y u v w x
        //             g h i                    v
        //             l m n                . a b c d
        //               v                  . f g h i
        //      {b,c,d,g,h,i,l,m,n}         . k l m n or {.,a,b,c,d,.,f,g,h,i ... }
        //                                  . p q r s
        //                                  . u v w x
        //          a b c d e f g           (no wrap)
        //            . . o . .
        //                v
        //           {b,c,d,e,f}
        //

        // index 2d as 1d or 1d as 2d

        //  2d as 1d     1d as 2d
        //    (4)          (3,2)
        //   . . .     
        //   o . .   . . . . . o . . .
        //   . . .

        // get diagonal from point (backwards/forwards) or from center with adjustable size
        // returns another 2d enumarable or a 1d enumarable (overrides)

        // GetDiagonalFromPointForwards(int x, int y, int length) -> IEnumerable<IEnumerable<T>>
        // GetDiagonalFromPointBackwards(int x, int y, int length) -> IEnumerable<IEnumerable<T>>
        // GetDiagonalFromPointCenter(int x, int , int length) -> IEnumerable<IEnumerable<T>>
        // GetDiagonalFromPointForwards(int x, int y, int length) -> IEnumerable<T>
        // GetDiagonalFromPointBackwards(int x, int y, int length) -> IEnumerable<T>
        // GetDiagonalFromPointCenter(int x, int , int length) -> IEnumerable<T>

        //  ex. grid     dia from point forwards                dia from point backwards
        //  a b c d e    (1,1) size 3                           (5,5) size 4
        //  f g h i j    . . . . .                              . b . . .         b . . . 
        //  k l m n o    . * . . . returns  g . .               . . h . . returns . h . .
        //  p q r s t    . . m . . ------>  . m . or {g,m,s}    . . . n . ------> . . n . or {t.,.,.,.,n,.,.,.,h,.,.,.,b,.,.,.}
        //               . . . s .          . . s               . . . . *         . . . t
        //
        //               get dia from center
        //               (3,3) size 1
        //               . . . . .
        //               . g . . . returns  g . .
        //               . . * . . ------>  . m . or {g,.,.,.,m,.,.,.,s}
        //               . . . s .          . . s

        // get row or column (from/to index optional)



        //               
        //               get row    get row    get row    get row
        // ex. grid      (2)        (3 1-4)    (1 3-5)    (4 2-4)
        // a b c d e     . . . . .  . . . . .  . . c d e  . . . . .
        // f g h i j     f g h i j  . . . . .  . . . . .  . . . . .
        // k l m n o     . . . . .  k l m n .  . . . . .  . . . . .
        // p q r s t   . . . . . .  . . . . .  . . . . .  . q r s .
        //                   v          v          v          v
        //              {f,g,h,i,j} {k,l,m,n}   {c,d,e}    {q,r,s}
        //
        //    get col    get col    get col    get col
        //    (2)        (3 1-2)    (1 2-4)    (5 2-3)
        //    . b . . .  . . c . .  . . . . .  . . . . .
        //    . g . . .  . . h . .  f . . . .  . . . . j
        //    . l . . .  . . . . .  k . . . .  . . . . o 
        //    . q . . .  . . . . .  p . . . .  . . . . .
        //        v          v          v          v
        //    {b,g,l,q}    {c,h}     {f,k,p}     {j,o} (mama so fat)
        //

    }
}
