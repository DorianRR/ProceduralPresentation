using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    [SerializeField]
    private int MaxTunnels, MaxLength, NumSplits, NumRooms;

    [SerializeField]
    private GameObject Blocks = null;

    [SerializeField]
    private GameObject BlockParent = null;

    [SerializeField]
    Camera mainCamera = null;

    [SerializeField]
    private int NumberOfFullIterations;

    private GameObject[,] GOMap;

    private int Width = 100;

    private int Height = 50;

    private float WaitTime = 0.00005f;


    public void GenerateMap()
    {
        StopAllCoroutines();
        DeleteMap();
        GOMap = new GameObject[Width, Height];
        mainCamera.transform.position = new Vector3(Width / 2, Height / 2, -Width / 1.75f);
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {

                GameObject tempObject = Instantiate(Blocks, new Vector3(i, j, 0), Quaternion.identity);
                tempObject.transform.SetParent(BlockParent.transform);
                GOMap[i, j] = tempObject;
                if (i == 0 || i == Width - 1 || j == 0 | j == Height - 1)
                {
                    tempObject.GetComponent<Renderer>().material.color = Color.black;
                }
            }
        }
    }

    private void DeleteMap()
    {
        BlockData[] temp = FindObjectsOfType<BlockData>();
        foreach (BlockData thing in temp)
        {
            Destroy(thing.gameObject);
        }
    }

    public void GenerateRandomWalk()
    {
        StartCoroutine(GeneratePaths());
    }

    public void GenerateFullLevel()
    {
        int i = 0;
        do
        {
            StartCoroutine(CompleteGeneratePathsCoroutine());
            i++;
        } while (i < NumberOfFullIterations);        
    }

    public void GenerateRandomRooms()
    {
        StartCoroutine(GenerateRandomRoomsCoroutine());
    }


    IEnumerator CompleteGeneratePathsCoroutine()
    {
        int startingX = Random.Range(5, Width - 6);
        int startingY = Random.Range(5, Height - 6);
        int startingPositionX = startingX;
        int startingPositionY = startingY;
        int lastPositionX = startingX;
        int lastPositionY = startingY;

        for (int j = 0; j < MaxTunnels; j++)
        {
            int length = Random.Range(MaxLength / 5, MaxLength);
            int direction = Random.Range(0, 4);

            bool createRoom = true;

            switch (direction)
            {
                //up
                case 0:
                    {
                        int i = 0;
                        for (i = 0; i < length; i++)
                        {
                            if (GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color != Color.black)
                            {
                                GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color = Color.red;
                                lastPositionY++;
                            }
                            else
                            {
                                lastPositionX = startingPositionX;
                                lastPositionY = startingPositionY;
                                createRoom = false;
                                break;
                            }
                            yield return new WaitForSeconds(WaitTime);
                        }
                        if(createRoom)
                        {
                            StartCoroutine(GenerateRoom(new Vector2Int(lastPositionX, lastPositionY)));

                        }
                        break;
                    }
                //down
                case 1:
                    {
                        int i = 0;
                        for (i = 0; i < length; i++)
                        {
                            if (GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color != Color.black)
                            {
                                GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color = Color.red;
                                lastPositionY--;

                            }
                            else
                            {
                                lastPositionX = startingPositionX;
                                lastPositionY = startingPositionY;
                                createRoom = false;
                                break;

                            }
                            yield return new WaitForSeconds(WaitTime);

                        }
                        if (createRoom)
                        {
                            StartCoroutine(GenerateRoom(new Vector2Int(lastPositionX, lastPositionY)));

                        }
                        break;
                    }
                //left
                case 2:
                    {
                        int i = 0;
                        for (i = 0; i < length; i++)
                        {
                            if (GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color != Color.black)
                            {
                                GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color = Color.red;
                                lastPositionX--;
                            }
                            else
                            {
                                lastPositionX = startingPositionX;
                                lastPositionY = startingPositionY;

                                createRoom = false;
                                break;
                            }
                            yield return new WaitForSeconds(WaitTime);
                        }
                        if (createRoom)
                        {
                            StartCoroutine(GenerateRoom(new Vector2Int(lastPositionX, lastPositionY)));

                        }
                        break;
                    }
                //right
                case 3:
                    {
                        int i = 0;
                        for (i = 0; i < length; i++)
                        {
                            if (GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color != Color.black)
                            {
                                GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color = Color.red;
                                lastPositionX++;

                            }
                            else
                            {
                                lastPositionX = startingPositionX;
                                lastPositionY = startingPositionY;
                                break;

                            }
                            yield return new WaitForSeconds(WaitTime);
                        }
                        if (createRoom)
                        {

                            StartCoroutine(GenerateRoom(new Vector2Int(lastPositionX, lastPositionY)));

                        }
                        break;
                    }
            }
        }
        yield return null;
    }

    IEnumerator GeneratePaths()
    {
        int startingX = Random.Range(5, Width - 6);
        int startingY = Random.Range(5, Height - 6);
        int startingPositionX = startingX;
        int startingPositionY = startingY;
        int lastPositionX = startingX;
        int lastPositionY = startingY;

        for (int j = 0; j < MaxTunnels; j++)
        {
            int length = Random.Range(MaxLength / 5, MaxLength);
            int direction = Random.Range(0, 4);
            switch (direction)
            {
                //up
                case 0:
                    {
                        int i = 0;
                        for (i = 0; i < length; i++)
                        {
                            if (GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color != Color.black)
                            {
                                GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color = Color.red;
                                lastPositionY++;
                            }
                            else
                            {
                                lastPositionX = startingPositionX;
                                lastPositionY = startingPositionY;
                                break;
                            }
                            yield return new WaitForSeconds(WaitTime);
                        }
                        
                        break;
                    }
                //down
                case 1:
                    {
                        int i = 0;
                        for (i = 0; i < length; i++)
                        {
                            if (GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color != Color.black)
                            {
                                GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color = Color.red;
                                lastPositionY--;

                            }
                            else
                            {
                                lastPositionX = startingPositionX;
                                lastPositionY = startingPositionY;
                                break;

                            }
                            yield return new WaitForSeconds(WaitTime);

                        }
                       
                        break;
                    }
                //left
                case 2:
                    {
                        int i = 0;
                        for (i = 0; i < length; i++)
                        {
                            if (GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color != Color.black)
                            {
                                GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color = Color.red;
                                lastPositionX--;
                            }
                            else
                            {
                                lastPositionX = startingPositionX;
                                lastPositionY = startingPositionY;

                                break;
                            }
                            yield return new WaitForSeconds(WaitTime);
                        }
                        break;
                    }
                //right
                case 3:
                    {
                        int i = 0;
                        for (i = 0; i < length; i++)
                        {
                            if (GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color != Color.black)
                            {
                                GOMap[lastPositionX, lastPositionY].GetComponent<Renderer>().material.color = Color.red;
                                lastPositionX++;

                            }
                            else
                            {
                                lastPositionX = startingPositionX;
                                lastPositionY = startingPositionY;
                                break;

                            }
                            yield return new WaitForSeconds(WaitTime);
                        }
                        break;
                    }
            }
        }
        yield return null;
    }

    IEnumerator GenerateRandomRoomsCoroutine()
    {
        int i = 0;
        do
        {
            int StartX = Random.Range(1, Width - 4);
            int StartY = Random.Range(1, Height - 4);
            int RoomWidth = Random.Range(2, 6);
            int RoomHeight = Random.Range(2, 6);

            for(int width = -RoomWidth; width < RoomWidth; width++)
            {
                yield return new WaitForSeconds(WaitTime);

                if (StartX + width > Width -1 || StartX + width < 1)
                {
                    break;
                }
                for (int height = -RoomHeight; height < RoomHeight; height++)
                {
                    if(StartY + height > Height - 1 || StartY + height < 1)
                    {
                        break;
                    }
                    GOMap[StartX + width, StartY + height].GetComponent<Renderer>().material.color = Color.black;

                }
            }

            i++;

        } while (i < NumRooms);
    }

    IEnumerator GenerateRoomForComplete()
    {
        int i = 0;
        do
        {
            int StartX = Random.Range(1, Width - 4);
            int StartY = Random.Range(1, Height - 4);
            int RoomWidth = Random.Range(2, 6);
            int RoomHeight = Random.Range(2, 6);

            for (int width = -RoomWidth; width < RoomWidth; width++)
            {
                yield return new WaitForSeconds(WaitTime);

                if (StartX + width > Width - 1 || StartX + width < 1)
                {
                    break;
                }
                for (int height = -RoomHeight; height < RoomHeight; height++)
                {
                    if (StartY + height > Height - 1 || StartY + height < 1)
                    {
                        break;
                    }
                    GOMap[StartX + width, StartY + height].GetComponent<Renderer>().material.color = Color.black;
                }
            }
            i++;
        } while (i < NumRooms);
    }

    IEnumerator GenerateRoom(Vector2Int EntryPosition)
    {
        int RoomDepth = Random.Range(3, 5);
        int lastPositionY = EntryPosition.y;
        int lastPositionX = EntryPosition.x;
        int RoomWidth = Random.Range(3, 5);

        
        for (int width = -RoomWidth; width < RoomWidth; width++)
        {
            yield return new WaitForSeconds(WaitTime);

            if (EntryPosition.x + width > Width - 1 || EntryPosition.x + width < 1)
            {
                break;
            }
            for (int height = -RoomDepth; height < RoomDepth; height++)
            {
                if (EntryPosition.y + height > Height - 1 || EntryPosition.y + height < 1)
                {
                    break;
                }
                GOMap[EntryPosition.x + width, EntryPosition.y + height].GetComponent<Renderer>().material.color = Color.black;

            }
        }
    }

    /*
     *  I started to make a cellular automata generator as well, but decided it would be 
     *  better to tune the random walk instead.
     */

    //public void GenerateCellA()
    //{
    //    StartCoroutine(CellularAutomata());


    //}

    //IEnumerator CellularAutomata()
    //{

    //    //List<GameObject> temp = new List<GameObject>();
    //    MakeCells(NumSplits, true);
    //    if (CellWalls.Count != 0)
    //    {
    //        foreach (GameObject GO in CellWalls)
    //        {
    //            GO.GetComponent<Renderer>().material.color = Color.red;
    //            yield return new WaitForSeconds(WaitTime);

    //        }
    //    }

    //    yield return null;
    //}

    //private List<GameObject> MakeCells(int SplitsRemaining, bool LeftRight)
    //{
    //    List<GameObject> temp = new List<GameObject>();
    //    List<GameObject> returnList = new List<GameObject>();
    //    SplitsRemaining -= 1;
    //    if (SplitsRemaining > 0)
    //    {
    //        temp = MakeCells(SplitsRemaining, !LeftRight);
    //        foreach (GameObject GO in temp)
    //        {
    //            CellWalls.Add(GO);
    //        }

    //    }

    //    int starting = 0;
    //    if (!(CellWalls.Count > 0))
    //    {
    //        starting = Random.Range(5, Width - 6);
    //        for (int i = 1; i < Height - 1; i++)
    //        {
    //            GOMap[starting, i].GetComponent<BlockData>().isHorizontal = false;
    //            returnList.Add(GOMap[starting, i]);
    //        }
    //        return returnList;
    //    }
    //    else if (LeftRight && CellWalls.Count > 0)
    //    {
    //        int j;
    //        GameObject tempGO;
    //        {
    //            tempGO = CellWalls[Random.Range(0, CellWalls.Count)];
    //            starting = tempGO.GetComponent<BlockData>().positionY;
    //        } while (tempGO.GetComponent<BlockData>().isHorizontal) ;
    //        j = tempGO.GetComponent<BlockData>().positionX;
    //        while (j < Width - 1)
    //        {
    //            GOMap[j, starting].GetComponent<BlockData>().isHorizontal = true;
    //            returnList.Add(GOMap[j, starting]);
    //            j++;
    //        }

    //        int k;
    //        do
    //        {
    //            tempGO = CellWalls[Random.Range(0, CellWalls.Count)];
    //            starting = tempGO.GetComponent<BlockData>().positionY;
    //        } while (tempGO.GetComponent<BlockData>().isHorizontal);
    //        k = tempGO.GetComponent<BlockData>().positionX;

    //        while (k >= 1)
    //        {
    //            GOMap[k, starting].GetComponent<BlockData>().isHorizontal = true;
    //            returnList.Add(GOMap[k, starting]);
    //            k--;
    //        }
    //    }
    //    else
    //    {
    //        int i = 0;
    //        GameObject tempGO;
    //        do
    //        {
    //            tempGO = CellWalls[Random.Range(0, CellWalls.Count)];
    //            starting = tempGO.GetComponent<BlockData>().positionX;
    //        } while (tempGO.GetComponent<BlockData>().isHorizontal);

    //        while (i < Height - 1)
    //        {
    //            GOMap[starting, i].GetComponent<BlockData>().isHorizontal = false;
    //            returnList.Add(GOMap[starting, i]);
    //            i++;
    //        }

    //        i = 0;
    //        do
    //        {
    //            tempGO = CellWalls[Random.Range(0, CellWalls.Count)];
    //            starting = tempGO.GetComponent<BlockData>().positionX;
    //        } while (tempGO.GetComponent<BlockData>().isHorizontal);

    //        while (i >= 1)
    //        {
    //            GOMap[starting, i].GetComponent<BlockData>().isHorizontal = false;
    //            returnList.Add(GOMap[starting, i]);
    //            i--;
    //        }
    //    }
    //    return returnList;
    //}

}
