using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public Slot activeSlot;
    public int selectedSlot = 0;
    private void Start()
    {
        SelectSlot();
    }
    private void Update()
    {
        int previousSelected = selectedSlot;
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    selectedSlot = 0;

        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        //{
        //    selectedSlot = 1;


        //}

        //if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        //{
        //    selectedSlot = 2;

        //}
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(selectedSlot == 0)
            {
                selectedSlot = 3;
            }
            else
            {
                selectedSlot--;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (selectedSlot == 3)
            {
                selectedSlot = 0;
            }
            else
            {
                selectedSlot++;
            }
        }


        if (previousSelected == selectedSlot)
        {

            SelectSlot();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeValue(1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeValue(-1);
        }

    }
    void SelectSlot()
    {
        int i = 0;

        foreach (Transform child in transform)
        {
            if (i == selectedSlot)
            {
                var slot = child.GetComponent<Slot>();
                activeSlot = slot;
                slot.textNumber.color = Color.white;
                //Debug.Log(child.name);
            }
            else
            {
                child.GetComponent<Slot>().textNumber.color = Color.gray; ;
            }

            i++;
        }
    }

    void ChangeValue(int change)
    {
        if ((activeSlot.number + change) < 0)
        {
            activeSlot.number = 9;
        }
        else if ((activeSlot.number + change) > 9)
        {
            activeSlot.number = 0;
        }
        else
        {
            activeSlot.number = activeSlot.number + change;
        }

    }

}
