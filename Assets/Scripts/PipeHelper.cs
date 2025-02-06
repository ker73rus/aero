using GogoGaga.OptimizedRopesAndCables;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PipeHelper : MonoBehaviour
{
    [SerializeField]
    Transform midPosition;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") { 
            CameraMouseLook player = other.GetComponentInChildren<CameraMouseLook>();
            if (player.previus.tag == "rope" && player.previus.picked)
            {
                GameObject rope = player.previus.gameObject;
                Interactable interactable = player.previus;
                rope.GetComponent<Rope>().endPoint.gameObject.GetComponent<Rope>().endPoint = midPosition;
                rope.GetComponent<Rope>().endPoint.gameObject.GetComponent<RopeHelper>().Start();
                rope.GetComponent<Rope>().endPoint = midPosition;
                rope.GetComponent<RopeHelper>().Start();

            }
        }
    }
}
