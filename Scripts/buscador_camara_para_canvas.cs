using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class buscador_camara_para_canvas : MonoBehaviour
{
    public GameController gamecontroller_aux;
    public Canvas aux_canvas;
    void Start()
    {
        aux_canvas=GetComponent<Canvas>() as Canvas;
        aux_canvas.renderMode = RenderMode.ScreenSpaceCamera;
    }

    public void asignar_camara_al_canvas_vidas(Camera aux)
    {
        aux_canvas.worldCamera=aux;
    }
}
