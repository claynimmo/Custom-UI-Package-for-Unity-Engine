using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this abstract class must be used as a base class on the functionality of an ability so it can be referenced by the ability cooldown
public abstract class Ability : MonoBehaviour
{
    public abstract void CooldownComplete();
}
