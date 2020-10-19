using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMaze : MonoBehaviour
{
    public int rows = 5;
    public int cols = 5;
    public GameObject[] walls;
    public GameObject[] ChangeLocations;

    // Start is called before the first frame update
    void Start()
    {
        ProceduralDungeon PD = new ProceduralDungeon(rows, cols);
        var maze = PD.Dungeon;
        //Debug.Log(PD.PrintPD());


        // Create maze walls
        for (int i = 0; i < cols; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                int r = rows - j - 1;
                int c = i;
                if (maze[r, c].Visit == 1) 
                { 
                    Vector3 posBot = new Vector3(i * 10.17f, j * 10.17f, 0);
                    if (maze[r, c].Down) Instantiate(walls[0], posBot, Quaternion.AngleAxis(0, Vector3.forward));
                    else Instantiate(walls[1], posBot, Quaternion.AngleAxis(0, Vector3.forward));

                    Vector3 posTop = new Vector3(i * 10.17f, j * 10.17f + 9.64f, 0);
                    if (maze[r, c].Up) Instantiate(walls[0], posTop, Quaternion.AngleAxis(0, Vector3.forward));
                    else Instantiate(walls[1], posTop, Quaternion.AngleAxis(0, Vector3.forward));

                    Vector3 posLeft = new Vector3(i * 10.17f - 4.82f, j * 10.17f + 4.82f, 0);
                    if (maze[r, c].Left) Instantiate(walls[0], posLeft, Quaternion.AngleAxis(90, Vector3.forward));
                    else Instantiate(walls[1], posLeft, Quaternion.AngleAxis(90, Vector3.forward));

                    Vector3 posRight = new Vector3(i * 10.17f + 4.82f, j * 10.17f + 4.82f, 0);
                    if (maze[r, c].Right) Instantiate(walls[0], posRight, Quaternion.AngleAxis(90, Vector3.forward));
                    else Instantiate(walls[1], posRight, Quaternion.AngleAxis(90, Vector3.forward));
                }
            }
        }

        // Change Locations of dependents
        for (int x = 0; x < 2; ++x)
        {
            ChangeLocations[x].transform.position = new Vector3((rows - PD.Start.Item1 - 1) * 10.17f, PD.Start.Item2 * 10.17f + 4.5f, ChangeLocations[x].transform.position.z);
        }

    }
}
