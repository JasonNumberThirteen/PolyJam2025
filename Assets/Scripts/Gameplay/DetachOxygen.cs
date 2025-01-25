using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachOxygen : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("DETACH");
    }
}
