using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public Animator doorAnimator;

    public MeshRenderer mesh;

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetAxisRaw("Fire1") != 0f && !doorAnimator.GetBool("isOpen"))
        {
            openTheDoor();
        }
    }

    private void openTheDoor()
    {
        doorAnimator.SetBool("isOpen", true);

        Material[] mats = mesh.materials;
        Material m2 = mats[2];
        mats[2] = mats[1];
        mats[1] = m2;
        mesh.materials = mats;
    }

    private void Awake()
    {
        SaveGameData.onSave += saveme;
        SaveGameData.onLoad += loadme;
    }

    private void saveme(SaveGameData savegame)
    {
        savegame.doorIsOpen = doorAnimator.GetBool("isOpen");
    }

    private void loadme(SaveGameData savegame)
    {
        Debug.Log("Doorswitch loadme");
        if (savegame.doorIsOpen)
            openTheDoor();
    }
    private void OnDestroy()
    {
        SaveGameData.onSave -= saveme;
        SaveGameData.onLoad += loadme;
    }
}
