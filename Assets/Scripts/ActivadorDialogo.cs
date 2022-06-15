using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorDialogo : MonoBehaviour
{
    [SerializeField] private Dialogo oraciones;

    public void ActivarDialogo()
    {
        FindObjectOfType<DialogueManager>().EmpezarDialogo(oraciones);
    }
}
 