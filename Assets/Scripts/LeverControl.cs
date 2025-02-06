using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Interactable))]
public class LeverControl : MonoBehaviour
{
    Transform parent;
    public int position = 0;

    private void Start()
    {
        parent = GetComponent<Interactable>().parent;
    }

    public void SwitchPosition(int pos)
    {
        if (position == pos)
        {
            position = 0;
            parent.rotation = Quaternion.Euler(Vector3.zero);
        }
        else
        {
            position = pos;
            parent.rotation = Quaternion.Euler(new Vector3(30 * position,0,0));
        }
    }
}
