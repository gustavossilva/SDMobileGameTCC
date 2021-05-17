using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{

    protected override void Awake()
    {
        IsPersistentBetweenScenes = true;
        base.Awake();
    }
}
