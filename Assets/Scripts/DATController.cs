using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DATController : MonoBehaviour
{
    [SerializeField]
    GameObject DatEnter;
    [SerializeField]
    GameObject DatExit;
    [SerializeField]
    GameObject DatFuel;
    [SerializeField]
    GameObject DatClap;

    public void SetDatEnter(float value)
    {
        Vector3 rotate = transform.eulerAngles;
        rotate.z = -270 * value;
        DatEnter.transform.rotation = Quaternion.Euler(rotate);
    }
    public void SetDatExit(float value)
    {
        Vector3 rotate = transform.eulerAngles;
        rotate.z = -270 * value;
        DatExit.transform.rotation = Quaternion.Euler(rotate);
    }
    public void SetDatFuel(float value)
    {
        Vector3 rotate = transform.eulerAngles;
        rotate.z = -270 * value;
        DatFuel.transform.rotation = Quaternion.Euler(rotate);
    }
    public void SetDatClap(float value)
    {
        Vector3 rotate = transform.eulerAngles;
        rotate.z = -270 * value;
        DatClap.transform.rotation = Quaternion.Euler(rotate);
    }
    public void Off()
    {
        Vector3 rotate = transform.eulerAngles;
        rotate.z = 0;
        DatClap.transform.rotation = Quaternion.Euler(rotate);
    }
}
