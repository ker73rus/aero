using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RefillController : MonoBehaviour
{
    [SerializeField]
    CanvasController canvas;
    [SerializeField]
    float Dat1;
    [SerializeField]
    float Dat2;
    [SerializeField]
    float Dat3;
    [SerializeField]
    float Dat4;
    [SerializeField]
    DATController dAT;
    [SerializeField]
    Interactable Pusher;
    [SerializeField]
    GameObject Rope;
    [SerializeField]
    LeverControl lever;
    [SerializeField]
    Transform Puller;
    [SerializeField]
    TextMeshProUGUI fuel;
    [SerializeField]
    float fuelSpeed;
    [SerializeField]
    float rot = 0.1f;
    [SerializeField]
    Interactable shtecker;
    bool push = false;
    float fuelNum = 23987;
    [SerializeField]
    Interactable break1;
    [SerializeField]
    Interactable break2;
    [SerializeField]
    Interactable zazemlenie;
    [SerializeField]
    Interactable clip;
    [SerializeField]
    Interactable clips;
    [SerializeField]
    Interactable NNZZeml;

    [SerializeField]
    Interactable Donniy;

    [SerializeField]
    Interactable Duz;
    [SerializeField]
    Interactable She;
    bool clipsB = false;
    bool NNZzem = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RopeOut());
        StartCoroutine(RopeIn());
        StartCoroutine(PushActive());
        StartCoroutine(WaitBreak());
        StartCoroutine(WaitZeml());
        StartCoroutine(WaitClip());
        StartCoroutine(WaitClips());
        StartCoroutine(WaitNNZZeml());
        StartCoroutine(WaitShe());
        StartCoroutine(WaitDonniy());
    }

    // Update is called once per frame
    void Update()
    {
        if (push)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                dAT.SetDatClap(Dat4);
                fuelNum +=fuelSpeed;
                fuel.text = (int)(fuelNum) + "";
            }
            else
            {
                dAT.Off();
            }
        }
    }
    IEnumerator WaitClip()
    {
        yield return new WaitUntil(() => clip.picked);
        StartCoroutine(canvas.Wrong("Взят трос с клипсовым соединителем, а не трос со штырём"));
    }
    IEnumerator WaitDonniy()
    {
        yield return new WaitUntil(() => Donniy.clapan);
        if(She.she != 1)
        StartCoroutine(canvas.Wrong("Неправильно установлено положение дозатора"));
    }
    IEnumerator WaitShe()
    {
        yield return new WaitUntil(() => She.she != 0);
        if(!(clipsB && NNZzem && shtecker.open && Duz.duz))
        {
            if (!clipsB || !NNZzem)
            {
                StartCoroutine(canvas.Wrong("Не проведено выравнивание потенциалов"));
            }
            if (!Duz.duz)
            {
                StartCoroutine(canvas.Wrong("Не открыт донный клапан наливки"));
            }
            if (!shtecker.open)
            {
                StartCoroutine(canvas.Wrong("Не открыт ННЗ"));
            }
        }
        yield return new WaitForSeconds(20);
        if(!Donniy.clapan)
            StartCoroutine(canvas.Wrong("Не открыт донный клапан"));

    }
    IEnumerator WaitClips()
    {
        yield return new WaitUntil(() => clips.picked);
        yield return new WaitUntil(() => !clips.picked);
        clipsB = true;
    }
    IEnumerator WaitNNZZeml()
    {
        yield return new WaitUntil(() => NNZZeml.picked);
        yield return new WaitUntil(() => !NNZZeml.picked);
        NNZzem = true;
    }
    IEnumerator WaitZeml()
    {
        yield return new WaitUntil(() => zazemlenie.picked);
        yield return new WaitUntil(() => !zazemlenie.picked);
        if(!zazemlenie.picked && zazemlenie.parent == null)
        {
            StartCoroutine(canvas.Wrong("Неправильно устанавлен штырь заземления"));
        }
    }
    IEnumerator WaitBreak()
    {
        yield return new WaitWhile(()=> !break1.picked && !break2.picked);
        yield return new WaitWhile(() => break1.picked || break2.picked);
        yield return new WaitForSeconds(0.5f);
        if (break1.parent.name != "BreakPosForward" && break2.parent.name != "BreakPosForward")
        {
            StartCoroutine(canvas.Wrong("Неправильная последовательность установки колодок"));
        } 
    }
    IEnumerator RopeOut()
    {
        yield return new WaitUntil(() => lever.position == 1);
        print("Выезжает");

        while (lever.position == 1 && rot <= 1000)
        {
            yield return new WaitForEndOfFrame();
            Puller.Rotate(new Vector3(0,0.3f,0));
            rot += 0.3f;
        }
        StartCoroutine(RopeOut());
    }
    IEnumerator RopeIn()
    {
        yield return new WaitUntil(() => lever.position == -1);
        print("Заезжает");
        while (lever.position == -1 && rot >= 0)
        {
            yield return new WaitForEndOfFrame();
            Puller.Rotate(new Vector3(0,-0.3f,0));
            rot -= 0.3f;
        }
        StartCoroutine(RopeIn());
    }
    IEnumerator PushActive()
    {
        yield return new WaitUntil(() => Pusher.picked);
        push = true;
        yield return new WaitUntil(() => !Pusher.picked);
        StartCoroutine(PushActive());
    }
}
