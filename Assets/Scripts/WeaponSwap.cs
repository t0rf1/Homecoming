using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public int selectedWeapon = 0;

    private void Start()
    {
        SelectWeapon();
    }
    private void Update()
    {
        int previousSelected = selectedWeapon;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }

        
        if (previousSelected == selectedWeapon)
        {
            SelectWeapon();
        }
    }
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
