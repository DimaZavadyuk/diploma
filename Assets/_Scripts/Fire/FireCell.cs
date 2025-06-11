using UnityEngine;

public class FireCell : MonoBehaviour
{
    public bool isBurning = false;  // Track if this cell is burning
    private GameObject fireObject;

    public void StartFire(GameObject fireObj)
    {
        if (!isBurning) 
        {
            isBurning = true;
            fireObject = fireObj;
        }
    }

    public void Extinguish()
    {
        if (isBurning)
        {
            isBurning = false;
            if (fireObject != null)
            {
                Destroy(fireObject);  // Remove the fire object
                fireObject = null;
                isBurning = false;
            }
        }
    }

    public bool IsBurning()
    {
        return isBurning;
    }
}