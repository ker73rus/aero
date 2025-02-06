using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    public bool picked;
    Outline outline;
    public Transform parent;
    public bool open = false;
    public bool duz = false;
    public bool clapan = false;
    public bool newLever = false;
    public int she = 0;
    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;
    }
    public void OnHoverEnter()
    {
        outline.OutlineWidth = 4;
    }
    public void OnHoverExit()
    {
        outline.OutlineWidth = 0;
    }
    public void PickUp(GameObject gameObject)
    {
        GetComponent<Collider>().isTrigger = true;
        if (tag == "rope" ) foreach(Collider c in GetComponentsInChildren<Collider>())c.isTrigger = true;
        if (parent != null && tag != "Pusher")
            StartCoroutine(waitForPick(parent.gameObject));
        else if (CompareTag("Pusher") && parent != null)
        {
            StartCoroutine(pusherPicker(parent.gameObject));
        }
        parent = gameObject.transform;
        picked = true;
    }
    IEnumerator pusherPicker(GameObject gameObject)
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(true);

    }

    public void UseLever(int pos)
    {
        GetComponent<LeverControl>().SwitchPosition(pos);
    }
    IEnumerator Open()
    {
        open = true;
        GetComponent<Collider>().isTrigger = true;
        Transform papa = parent;
        Vector3 rotate = papa.eulerAngles;
        if (tag == "Shtecker")
        {
            while (rotate.z > -180)
            {
                rotate.z--;
                papa.rotation = Quaternion.Euler(rotate);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (papa.rotation.x < 0.9f)
            {
                rotate.x++;
                papa.rotation = Quaternion.Euler(rotate);
                yield return new WaitForEndOfFrame();
            }
        }
        print("Lox");
    }
    IEnumerator Close()
    {
        open = false;
        Transform papa = parent;
        Vector3 rotate = papa.eulerAngles;
        if (tag == "Shtecker")
        {
            while (rotate.z < 180)
            {
                rotate.z++;
                papa.rotation = Quaternion.Euler(rotate);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (papa.rotation.x > 0)
            {
                rotate.x++;
                papa.rotation = Quaternion.Euler(rotate);
                yield return new WaitForEndOfFrame();
            }
        }
            
        print("LoxC");
        GetComponent<Collider>().isTrigger = false;

    }

    IEnumerator waitForPick(GameObject gameObject)
    {
        yield return new WaitWhile(() => picked == false);
        yield return new WaitWhile(() => picked == true);
        gameObject.SetActive(true);
        GetComponent<Collider>().isTrigger = false;
    }
    public void Drop()
    {
        picked = false;
        parent = null;
    }
    public void Put(GameObject place)
    {
        parent = place.transform;
        picked = false;
        transform.position = parent.position;
        transform.rotation = parent.rotation;
        parent.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (picked) {
            this.transform.position = parent.position;
            transform.rotation = parent.rotation;
        }
    }

    public void Duz()
    {
        if (!duz)
        {
            transform.position -= new Vector3(0, 0, 0.05f);
            duz = true;
        }
        else
        {
            transform.position += new Vector3(0, 0, 0.05f);
            duz = false;
        }
    }
    public void Clapan()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (!clapan)
        {
            clapan = true;
            rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
        else
        {
            clapan = false;
            rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

    }
    public void NewLever()
    {
        if (!newLever)
        {
            transform.rotation = Quaternion.Euler(new Vector3(140, 180, 0));
            newLever = true;
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(90, 180, 0));
            newLever = false;
        }
    }
    public void She()
    {
        switch (she)
        {
            case 0:
                she = 1;
                transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
                break;
            case 1: 
                she = 2;
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 70));
                break;
            case 2:
                she = 0;
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, -90));
                break;
        }
    }
}
