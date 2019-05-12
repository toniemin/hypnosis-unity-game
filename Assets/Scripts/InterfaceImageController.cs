using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceImageController : MonoBehaviour
{
    public Sprite[] tutorials;
    public Sprite[] stories;

    public Image output;

    private int tutorialIndex = 0;
    private int storyIndex = 0;
    

    public void showTutorial()
    {
        if (! output.enabled)
            output.enabled = true;
        nextTutorial();
    }

    public void nextTutorial()
    {
        output.sprite = tutorials[tutorialIndex];
        tutorialIndex++;

        if (tutorialIndex >= tutorials.Length)
        {
            output.enabled = false;
        }
    }

    public void showStory()
    {
        if (!output.enabled)
            output.enabled = true;

        nextStory();
    }

    public void nextStory()
    {
        output.sprite = stories[storyIndex];
        storyIndex++;

        if (storyIndex >= stories.Length)
        {
            output.enabled = false;
        }
    }
}
