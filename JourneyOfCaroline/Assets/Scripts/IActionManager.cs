using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionManager
{
    void Fly(GameObject disk, float speed, Vector3 direction);
}