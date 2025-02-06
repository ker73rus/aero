using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapanController : MonoBehaviour
{
    [SerializeField]
    GameObject Duz;
    [SerializeField]
    GameObject Duzzer;
    [SerializeField]
    GameObject Point;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(duzReaction());
        StartCoroutine(pointReaction());
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    IEnumerator duzReaction()
    {
        yield return new WaitUntil(()=> Duzzer.GetComponent<Interactable>().duz);
        if (Duz.GetComponent<Interactable>().duz)
        {
            Duz.GetComponent<Interactable>().Duz();
            if (Point.transform.position.y > 0.70f)
                Point.transform.position -= new Vector3(0, 0.02f, 0);
        }
        StartCoroutine(duzReaction());
    }
    IEnumerator pointReaction()
    {
        yield return new WaitUntil(() => Duz.GetComponent<Interactable>().duz);
        if (!Duzzer.GetComponent<Interactable>().duz)
        {
            if(Point.transform.position.y < 0.72f)
            Point.transform.position += new Vector3(0, 0.01f, 0);
        }
        StartCoroutine(pointReaction());
    }
}
