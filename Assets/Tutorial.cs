using System;
using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float timer = 0.1f;
    public bool MovedBubble = false;
    public bool MovedPlayer = false;
    public bool Jumped = false;
    public bool Finished = false;

    public bool TutorialFlag = false;


    public int BubleBlinkCount = 3;
    public float BubleBlinkDelay = 0.5f;

    public int JumpBlinkCount = 3;
    public float JumpBlinkDelay = 0.5f;

    public float MoveBlinkactivatedelay = 3;
    public float MoveBlinkDeactivateDelay = 0.5f;

    public float TutorialTimer = 10;
    public float TutorialFinishTimer = 30;



    public GameObject BubbleArrow;
    public GameObject[] MovmentArrows;
    public GameObject[] JumpArrows;
    public GameObject FinishArrows;


    public void Finish()
    {
        Finished = true;
        PlayerPrefs.SetInt("Finished", 1); // Save as 1 (true)
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialFlag|| Finished) { return; }
        timer += Time.deltaTime;
        print(((int)timer));
        if (timer > TutorialTimer)
        {

            if (!MovedBubble)
            {
                ToggleResetTimerAndFlag();

                StartCoroutine(SimpleBlink(BubbleArrow, BubleBlinkCount, BubleBlinkDelay));
            }
            else if (!MovedPlayer)
            {
                ToggleResetTimerAndFlag();

                StartCoroutine(BlinkMoveArrow(MovmentArrows, MoveBlinkactivatedelay, MoveBlinkDeactivateDelay));

            }
            else if (!Jumped)
            {

                ToggleResetTimerAndFlag();
                StartCoroutine(BlinkJumplArrow(JumpArrows, JumpBlinkCount, JumpBlinkDelay));


            }
            else if (!Finished && (timer > TutorialFinishTimer))
            {

                ToggleResetTimerAndFlag();
                StartCoroutine(SimpleBlink(FinishArrows, BubleBlinkCount, BubleBlinkDelay));


            }
        }

        // if 20 seconds passed without movement show move tutorial
        // if 20 seconds passed without jump show jump tutorial
        // if 20 seconds passed without bubble show jump tutorial

        //save everything in playerprefs
    }
  
    public void OnEnable()
    {
        timer = 0;
        Finished = PlayerPrefs.GetInt("Finished") == 1;

    }

    IEnumerator SimpleBlink(GameObject myObject, int times, float delay)
    {

        for (int i = 0; i < times; i++)
        {
            myObject.SetActive(true);
            yield return new WaitForSeconds(delay);
            myObject.SetActive(false);
            yield return new WaitForSeconds(delay);

        }
        ToggleResetTimerAndFlag();
    }

    public void ToggleResetTimerAndFlag()
    {
        timer = 0;
        TutorialFlag = !TutorialFlag;
    }

    IEnumerator BlinkMoveArrow(GameObject[] myObjects, float Activedelay, float DeactivateDelay)
    {

        for (int i = 0; i < myObjects.Length; i++)
        {
            myObjects[i].SetActive(true);
            yield return new WaitForSeconds(Activedelay);
        }
        yield return new WaitForSeconds(DeactivateDelay);
        for (int i = 0; i < myObjects.Length; i++)
        {
            myObjects[i].SetActive(false);
        }
        ToggleResetTimerAndFlag();

    }

    IEnumerator BlinkJumplArrow(GameObject[] myObjects, int times, float delay)
    {
        for (int i = 0; i < times; i++)
        {

            for (int J = 0; J < myObjects.Length; J++)
            {
                myObjects[J].SetActive(true);
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitForSeconds(delay);


            for (int h = 0; h < myObjects.Length; h++)
            {
                myObjects[h].SetActive(false);
            }

        }
        ToggleResetTimerAndFlag();
    }



}
