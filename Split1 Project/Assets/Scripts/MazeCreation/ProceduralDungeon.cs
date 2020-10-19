using System;
using System.Collections.Generic;
using UnityEngine;


public class ProceduralDungeon
{

    // Data about each room Mapwise
    public class room
    {
        public bool Up
        { get; set; }
        public bool Down
        { get; set; }
        public bool Left
        { get; set; }
        public bool Right
        { get; set; }

        public char Status
        { get; set; }

        public int Visit
        { get; set; }

        public room()
        {
            Up = UnityEngine.Random.Range(0, 3) == 0;
            Down = UnityEngine.Random.Range(0, 3) == 0;
            Left = UnityEngine.Random.Range(0, 3) == 0;
            Right = UnityEngine.Random.Range(0, 3) == 0;
            Status = 'O';
            Visit = 0;
        }
    }

    public class Path
    {
        public Tuple<int, int> Coord
        { get; set; }
        public int Direction
        { get; set; }

        public Path(int r, int c, int D)
        {
            Coord = new Tuple<int, int>(r, c);
            Direction = D;
        }
        public Path(Tuple<int, int> t, int D)
        {
            Coord = t;
            Direction = D;
        }
    }



    public room[,] Dungeon
    { get; }
    private Tuple<int, int> MaxRC;

    public Tuple<int, int> Start
    { get; }
    public Tuple<int, int> End
    { get; }

    private List<List<Tuple<int, int>>> BlockList = new List<List<Tuple<int, int>>>();
    private List<Tuple<int, int>> Temp = new List<Tuple<int, int>>();

    public ProceduralDungeon(int r = 5, int c = 5)
    {
        MaxRC = new Tuple<int, int>(r, c);

        // Generate all rooms
        Dungeon = new room[r, c];
        for (int i = 0; i < r; ++i)
        {
            for (int j = 0; j < c; ++j)
            {
                Dungeon[i, j] = new room();
            }
        }

        // Clean Edges
        for (int i = 0; i < r; ++i)
        {
            Dungeon[i, 0].Left = false;
            Dungeon[i, c - 1].Right = false;
        }
        for (int i = 0; i < c; ++i)
        {
            Dungeon[0, i].Up = false;
            Dungeon[r-1, i].Down = false;
        }

        // Set Start/End
        Start = new Tuple<int, int>(UnityEngine.Random.Range(0, r), UnityEngine.Random.Range(0, c));
        Dungeon[Start.Item1, Start.Item2].Status = 'S';

        int EndRow; int EndCol;
        if (Start.Item1 > r / 2)
            EndRow = UnityEngine.Random.Range(0, (int)(r / 2)); 
        else
            EndRow = UnityEngine.Random.Range((int)(r / 2), r); 

        if (Start.Item2 > c/2)
            EndCol = UnityEngine.Random.Range(0, (int)(c / 2));  
        else
            EndCol = UnityEngine.Random.Range((int)(c / 2), c);  
        End = new Tuple<int, int>(EndRow, EndCol);

        Dungeon[EndRow, EndCol].Status = 'E';


        // Connect rooms based on entrances
        for (int i = 0; i < r; ++i)
        {
            for (int j = 0; j < c; ++j)
            {
                if (Dungeon[i, j].Up) Dungeon[i - 1, j].Down = true;
                if (Dungeon[i, j].Down) Dungeon[i + 1, j].Up = true;
                if (Dungeon[i, j].Left) Dungeon[i, j - 1].Right = true;
                if (Dungeon[i, j].Right) Dungeon[i, j + 1].Left = true;
            }
        }

        // Determine number of independent blocks
        int SearchNum = 1;
 
        while(!Search(Start.Item1, Start.Item2, SearchNum))
        {
            Temp.Clear();

            // Mark Blocks
            for (int i = 0; i < r; ++i)
            {
                for (int j = 0; j < c; ++j)
                {
                    if (Dungeon[i, j].Visit == 0)
                    {
                        Search(i, j, ++SearchNum);
                        BlockList.Add(Temp);
                        Temp = new List<Tuple<int, int>>();
                    }
                }
            }

            // Connect Blocks (Priorty to start)
            foreach (List<Tuple<int, int>> Block in BlockList)
            {
                var ConnectOrigin = new List<Path>();
                var ConnectDiff = new List<Path>();

                foreach (Tuple<int, int> Room in Block)
                {
                    int Row = Room.Item1;
                    int Col = Room.Item2;

                    if ((Row != 0) && !(Dungeon[Row, Col].Up) && (Dungeon[Row, Col].Visit != Dungeon[Row - 1, Col].Visit))
                    {
                        if (Dungeon[Row - 1, Col].Visit == 1) ConnectOrigin.Add(new Path(Room, 1));
                        else ConnectDiff.Add(new Path(Room, 1));
                    }

                    if ((Row != r - 1) && !(Dungeon[Row, Col].Down) && (Dungeon[Row, Col].Visit != Dungeon[Row + 1, Col].Visit))
                    {
                        if (Dungeon[Row + 1, Col].Visit == 1) ConnectOrigin.Add(new Path(Room, 2));
                        else ConnectDiff.Add(new Path(Room, 2));
                    }

                    if ((Col != 0) && !(Dungeon[Row, Col].Left) && (Dungeon[Row, Col].Visit != Dungeon[Row, Col - 1].Visit))
                    {
                        if (Dungeon[Row, Col - 1].Visit == 1) ConnectOrigin.Add(new Path(Room, 3));
                        else ConnectDiff.Add(new Path(Room, 3));
                    }

                    if ((Col != c - 1) && !(Dungeon[Row, Col].Down) && (Dungeon[Row, Col].Visit != Dungeon[Row, Col + 1].Visit))
                    {
                        if (Dungeon[Row, Col + 1].Visit == 1) ConnectOrigin.Add(new Path(Room, 4));
                        else ConnectDiff.Add(new Path(Room, 4));
                    }
                }

                // Determine Type of correction
                if (ConnectDiff.Count != 0 || ConnectOrigin.Count != 0)
                {
                    Path MakeConnect = new Path(-1, -1, -1);
                    if (ConnectDiff.Count != 0) MakeConnect = ConnectDiff[UnityEngine.Random.Range(0, ConnectDiff.Count)]; 
                    if (ConnectOrigin.Count != 0) MakeConnect = ConnectOrigin[UnityEngine.Random.Range(0, ConnectOrigin.Count)]; 

                    // Connect two blocks
                    switch (MakeConnect.Direction)
                    {
                        case 1:
                            Dungeon[MakeConnect.Coord.Item1, MakeConnect.Coord.Item2].Up = true;
                            Dungeon[MakeConnect.Coord.Item1 - 1, MakeConnect.Coord.Item2].Down = true;
                            break;
                        case 2:
                            Dungeon[MakeConnect.Coord.Item1, MakeConnect.Coord.Item2].Down = true;
                            Dungeon[MakeConnect.Coord.Item1 + 1, MakeConnect.Coord.Item2].Up = true;
                            break;
                        case 3:
                            Dungeon[MakeConnect.Coord.Item1, MakeConnect.Coord.Item2].Left = true;
                            Dungeon[MakeConnect.Coord.Item1, MakeConnect.Coord.Item2 - 1].Right = true;
                            break;
                        case 4:
                            Dungeon[MakeConnect.Coord.Item1, MakeConnect.Coord.Item2].Right = true;
                            Dungeon[MakeConnect.Coord.Item1, MakeConnect.Coord.Item2 + 1].Left = true;
                            break;
                        default:
                            Console.WriteLine("Undefine Direction");
                            break;
                    }
                }
            }


        // Clear Blocks
        ClearSearch();
        BlockList.Clear();
        SearchNum = 1;
        } 


        // Give Attribute status to room based on entrances/sizes
        for (int i = 0; i < (r); ++i)
        {
            for (int j = 0; j < (c); ++j)
            {
                // Check DeadEnd
                if (Dungeon[i, j].Status == 'O')
                {
                    int check = 0;
                    if (Dungeon[i, j].Left) ++check;
                    if (Dungeon[i, j].Right) ++check;
                    if (Dungeon[i, j].Up) ++check;
                    if (Dungeon[i, j].Down) ++check;

                    if (check == 1) Dungeon[i, j].Status = 'D';
                }
                
                // Check Big Room (2x2 connected rooms)(Mult Big Rooms can be connected)
                if (i < r-1 && j < c-1)
                {
                    bool x = Dungeon[i, j].Status != 'S' && Dungeon[i + 1, j].Status != 'S' && Dungeon[i, j + 1].Status != 'S' && Dungeon[i + 1, j + 1].Status != 'S' &&
                    Dungeon[i, j].Status != 'E' && Dungeon[i + 1, j].Status != 'E' && Dungeon[i, j + 1].Status != 'E' && Dungeon[i + 1, j + 1].Status != 'E';
                    
                    if (x && Dungeon[i, j].Right && Dungeon[i, j].Down && Dungeon[i + 1, j + 1].Up && Dungeon[i + 1, j + 1].Left)
                    {
                        Dungeon[i, j].Status = 'B';
                        Dungeon[i + 1, j].Status = 'B';
                        Dungeon[i, j + 1].Status = 'B';
                        Dungeon[i + 1, j + 1].Status = 'B';
                    }
                }
            }
        }
    }

    public string PrintPD()
    {
        string PrintedDungeon = "";
        for (int i = 0; i < MaxRC.Item1; ++i)
        {
            for (int j = 0; j < MaxRC.Item2; ++j)
            {
                if (Dungeon[i, j].Up && Dungeon[i, j].Visit != 0) PrintedDungeon+="  |  ";
                else PrintedDungeon += "     ";
            }
            PrintedDungeon += "\n";

            for (int j = 0; j < MaxRC.Item2; ++j)
            {
               if ((Dungeon[i,j].Status != 'O'))
                {
                    if (Dungeon[i, j].Visit == 0) PrintedDungeon += "   ";
                    else if (Dungeon[i, j].Left) PrintedDungeon += $"-{Dungeon[i, j].Status}";
                    else PrintedDungeon += $"  {Dungeon[i, j].Status}";
                }
                else
                {
                    if (Dungeon[i, j].Visit == 0) PrintedDungeon += "  ";
                    else
                    {
                        if (Dungeon[i, j].Left) PrintedDungeon += $"-{Dungeon[i, j].Visit}";
                        else PrintedDungeon += $"  {Dungeon[i, j].Visit}";
                    }
                }
                if (Dungeon[i, j].Right && Dungeon[i, j].Visit != 0) PrintedDungeon += "-";
                else PrintedDungeon += "  ";
            }
            PrintedDungeon += "\n";

            for (int j = 0; j < MaxRC.Item2; ++j)
            {
                if (Dungeon[i, j].Down && Dungeon[i, j].Visit != 0) PrintedDungeon += "  |  ";
                else PrintedDungeon += "     ";
            }
            PrintedDungeon += "\n";
        }
        return PrintedDungeon;
    }
    
    // Find connected blocks of dungeon
    private bool Search(int r, int c, int Mark)
    {
        room CurrentRoom = Dungeon[r, c];
        bool ReturnVal = false;

        // Room is already visited
        if (CurrentRoom.Visit != 0) return false;
       
        // End Room found
        if (CurrentRoom.Status == 'E') ReturnVal = true;

        Temp.Add(new Tuple<int, int>(r, c));

        // Search Other Rooms
        CurrentRoom.Visit = Mark;
        if (CurrentRoom.Up)    ReturnVal |= Search(r - 1, c, Mark);
        if (CurrentRoom.Down)  ReturnVal |= Search(r + 1, c, Mark);
        if (CurrentRoom.Left)  ReturnVal |= Search(r, c - 1, Mark);
        if (CurrentRoom.Right) ReturnVal |= Search(r, c + 1, Mark);

        return ReturnVal;
    }

    // Clear Visit marks
    private void ClearSearch()
    {
        for (int i = 0; i < MaxRC.Item1; ++i)
        {
            for (int j = 0; j < MaxRC.Item2; ++j)
            {
                Dungeon[i, j].Visit = 0;
            }
        }
    }
}
