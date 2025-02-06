using GogoGaga.OptimizedRopesAndCables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeHelper : MonoBehaviour
{

    Rope rope;
    Transform start;
    Transform end;
    public void Start()
    {
        rope = GetComponent<Rope>();
        start = rope.StartPoint;
        end = rope.EndPoint;
    }
    private void Update()
    {
        rope.ropeLength =  Mathf.Abs(start.position.z - end.position.z) * 1.2f; 
    }
}
