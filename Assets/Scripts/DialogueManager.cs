using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Queue<string> oraciones;
    [SerializeField] TextMeshProUGUI textoOracion;

    // Start is called before the first frame update
    void Start()
    {
        oraciones = new Queue<string>();
    }

    public void EmpezarDialogo(Dialogo dialogo)
    {
        Debug.Log("Empezo conversacion");
        oraciones.Clear();
        foreach(string oracion in dialogo.oraciones)
        {
            oraciones.Enqueue(oracion);
        }
        MostrarSiguenteOracion();
    }

    public void MostrarSiguenteOracion()
    {
        if(oraciones.Count == 0)
        {
            TerminarDialogo();
            return;
        }
        string oracion = oraciones.Dequeue();
        this.textoOracion.text = oracion;
    }

    public void TerminarDialogo()
    {
        Debug.Log("Termino conversacion");
    }
    
}
