using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMouseLook : MonoBehaviour
{
    public float sensitivity = 10f;
    public float smoothing = 2.0f;
    public Vector2 mouseLook;
    Vector2 smoothV;
    GameObject character;
    public bool menu = false;
    Ray ray;
    RaycastHit hit;
    public Interactable previus;
    [SerializeField]
    GameObject posInHands;
    [SerializeField]
    CanvasController canvas;

    void Start()
    {
        character = this.transform.parent.gameObject;
        Cursor.visible = false;
    }

    void Update()
    {
       
        if (!menu)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if(transform.localPosition.y == 1)
                    transform.localPosition = new Vector3(0,0,0);
                else transform.localPosition = new Vector3(0, 1, 0);
            }
            if(Input.GetMouseButtonDown(1)) 
            if (previus != null)
            {
                if (previus.picked && previus.tag == "rope")
                {
                   previus.Drop();
                }
            }

            var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            mouseLook += smoothV;
            mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
            //posInHands.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, posInHands.transform.up);
            Debug.DrawRay(transform.position, transform.forward * 2,Color.green);
            ray = new Ray(transform.position, transform.forward);
            //print("Voshel");
            if (Input.GetMouseButtonDown(1))
                previus.Drop();
            if (Physics.Raycast(ray, out hit, 2))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable != null)
                {
                    if (previus != interactable)
                    {
                        if (previus == null || !previus.picked)
                            interactable.OnHoverEnter();
                        if (previus != null)
                        {
                            previus.OnHoverExit();
                            if (previus.tag == "Break" && previus.picked && interactable.tag == "breakPos")
                            {
                                previus.Put(hit.transform.gameObject);
                                interactable.OnHoverExit();
                                previus.OnHoverExit();
                                previus = null;
                            }
                            else if (previus.tag == "RopeZem" && previus.picked && interactable.tag == "RopeZemPlace")
                            {
                                previus.Put(hit.transform.gameObject);
                                interactable.OnHoverExit();
                                previus.OnHoverExit();
                                previus = null;
                            }
                            else if (previus.tag == "rope" && previus.picked && interactable.tag == "ropePlace")
                            {
                                previus.Put(hit.transform.gameObject);
                                interactable.OnHoverExit();
                                previus.OnHoverExit();
                                previus = null;
                            }
                            else if (previus.tag == "Pusher" && previus.picked && interactable.tag == "PusherPlace")
                            {
                                previus.Put(hit.transform.gameObject);
                                interactable.OnHoverExit();
                                previus.OnHoverExit();
                            }
                            else if (previus.tag == "Clips" && previus.picked && interactable.tag == "ClipsPlace")
                            {
                                previus.Put(hit.transform.gameObject);
                                interactable.OnHoverExit();
                                previus.OnHoverExit();
                                previus = null;
                            }
                        }
                        if(previus == null || !previus.picked )
                            previus = interactable;
                        
                    }


                    if(interactable.tag == "Lever")
                    {
                        canvas.ShowLeverHelper();
                        StartCoroutine(closeHelper());
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            interactable.UseLever(1);
                        }
                        if (Input.GetKeyDown(KeyCode.Q)) {
                            interactable.UseLever(-1);
                        }
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        

                        if (interactable.tag != "She" && interactable.tag != "NewLever" &&  interactable.tag != "Clapan" && interactable.tag != "Duz" && interactable.tag !="Shtecker" && interactable.tag != "Door" && interactable.tag != "breakPos" && interactable.tag != "RopeZemPlace" && interactable.tag != "Lever" && interactable.tag != "ropePlace")
                            interactable.PickUp(posInHands);
                        else if( interactable.tag == "Door" || interactable.tag =="Shtecker") {
                            if (interactable.open)
                            {
                                
                                interactable.StartCoroutine("Close");
                            }
                                
                            else
                            {
                                interactable.StopAllCoroutines();
                                interactable.StartCoroutine("Open");
                            }
                                
                        }
                        else if(interactable.tag == "Duz")
                        {
                            interactable.Duz();
                        }
                        else if (interactable.tag == "Clapan")
                        {
                            interactable.Clapan();
                        }
                        else if(interactable.tag == "NewLever")
                        {
                            interactable.NewLever();
                        }
                        else if(interactable.tag == "She")
                        {
                            interactable.She();
                        }
                    }
                    if (Input.GetMouseButtonDown(1)) { 
                        interactable.Drop();
                    }


                }
               
            }
            else if (previus != null)
            {
                if(!previus.picked)
                {
                    previus.OnHoverExit();
                    previus = null;
                }
                

            }
        }
    }
    IEnumerator closeHelper()
    {
        yield return new WaitWhile(() => !menu && previus != null && previus.tag == "Lever");
        canvas.CloseLeverHelper();
    }
}