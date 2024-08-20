using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCheck : MonoBehaviour
{
    public Settings tutorialCheck;
    public GameObject tutorialUI;

    public void TutorialCompleted()
    {
        tutorialCheck.tutorialWatched = true;
    }

    private void Update()
    {
        if (tutorialCheck.tutorialWatched)
        {
            tutorialUI.SetActive(false);
        }
    }
}
