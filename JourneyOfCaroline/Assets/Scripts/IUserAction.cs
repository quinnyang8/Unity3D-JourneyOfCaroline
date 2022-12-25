using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void Click(Vector3 position);
    void Reset();
}