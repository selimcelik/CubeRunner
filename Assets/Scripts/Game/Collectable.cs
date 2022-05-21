using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum CollectableType
    {
        diamond,
        diamond5side
    }
    public CollectableType collectableType;
}
