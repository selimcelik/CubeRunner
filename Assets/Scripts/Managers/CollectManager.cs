using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CollectManager : Singleton<CollectManager>
{
    public int CollectedCoin;

    //public int CollectedDiamond5Side;


    public List<GameObject> CollectedObjects = new List<GameObject>();

    public List<GameObject> ObstacleObjects = new List<GameObject>();

    public void AddCoin(int amount)
    {
        CollectedCoin += amount;
    }

    /*public void AddDiamond5Side(int amount)
    {
        CollectedDiamond5Side += amount;
    }*/

    public async void ActiveCollectedObject()
    {
        for (int i = 0; i < CollectedObjects.Count; i++)
        {
            CollectedObjects[i].SetActive(true);
        }
        await Task.Delay(100);
        for (int i = CollectedObjects.Count - 1; i >= 0; i--)
        {
            CollectedObjects.RemoveAt(i);
        }
    }

    public async void ActiveObstacleObject()
    {
        for (int i = 0; i < ObstacleObjects.Count; i++)
        {
            ObstacleObjects[i].SetActive(true);
        }
        await Task.Delay(100);
        for (int i = ObstacleObjects.Count - 1; i >= 0; i--)
        {
            ObstacleObjects.RemoveAt(i);
        }
    }
}
