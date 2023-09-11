using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public void LoadLevelByNumber(int number)
    {
        Loader.LoadLevel(number);
    }
}
