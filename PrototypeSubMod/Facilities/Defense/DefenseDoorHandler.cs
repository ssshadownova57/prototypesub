using System.Collections;
using Nautilus.Handlers;
using Story;
using UnityEngine;

namespace PrototypeSubMod.Facilities.Defense;

internal class DefenseDoorHandler : MonoBehaviour
{
    [SerializeField] private FMODAsset doorOpenSFX;
    [SerializeField] private FMODAsset doorSwooshSFX;
    [SerializeField] private Transform openSFXPos;
    [SerializeField] private Animator animator;

    private bool hasOpened;

    private void OnEnable()
    {
        if (StoryGoalManager.main.IsGoalComplete("DefenseChamberDoorOpened"))
        {
            animator.SetTrigger("OpenDoor");
            hasOpened = true;
        }
    }
    
    public void OpenDoor()
    {
        if (hasOpened) return;

        if (!StoryGoalManager.main.IsGoalComplete("OnDefenseCloakDisabled")) return;

        FMODUWE.PlayOneShot(doorOpenSFX, openSFXPos.position);
        animator.SetTrigger("OpenDoor");

        Invoke(nameof(PlaySwoosh), 4.5f);
        hasOpened = true;

        StoryGoalManager.main.OnGoalComplete("DefenseChamberDoorOpened");
    }

    private void PlaySwoosh()
    {
        FMODUWE.PlayOneShot(doorSwooshSFX, openSFXPos.position);
    }
}
