using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class ButtonDefinition : MonoBehaviour
{
    public bool animated = false;
    public Color unselectedTint = Color.grey;
    public Color selectedTint = Color.white;
    public bool selected = false;
    private Button button;
    private Image image;
    private Animator animator;

    public AudioClip swapToSFX;
    public AudioClip confirmSFX;
    public float confirmTime;

    private bool disableControls = false;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        animated = TryGetComponent<Animator>(out animator);

        if (!animated)
        {
            if (selected)
            {
                image.color = selectedTint;
            }
            else
            {
                image.color = unselectedTint;
            }
        }
    }

    public void SwappedTo()
    {
        selected = true;

        //If theres sfx for swapping buttons, play it.
        if (swapToSFX != null)
        {
            AudioSource.PlayClipAtPoint(swapToSFX, Vector3.zero);
        }

        image.color = selectedTint;
        //if theres an animator, update the selected bool to false
        if (animated)
        {
            animator.SetBool("Selected", selected);
        }
        //if theres no animator, tint the button to show unselected
        else
        {
            image.color = selectedTint;
        }
    }

    public void SwappedOff()
    {
        selected = false;
        //if theres an animator, update the selected bool to false
        if (animated)
        {
            animator.SetBool("Selected", selected);
        }
        //if theres no animator, tint the button to show unselected
        else
        {
            image.color = unselectedTint;
        }
    }

    public IEnumerator ClickButton()
    {
        if (!disableControls)
        {
            disableControls = true;

            if (confirmSFX != null)
            {
                AudioSource.PlayClipAtPoint(confirmSFX, Vector3.zero);
            }

            yield return new WaitForSeconds(confirmTime);

            button.onClick.Invoke();

            disableControls = false;
        }
    }

    public bool GetDisableControls()
    {
        return disableControls;
    }
}
