using UnityEngine;
using UnityEngine.SceneManagement;

public class boton_cambiar_nivel : MonoBehaviour
{
    public int i;


    public void cambiardenivel()
    {
        SceneManager.LoadScene(i);
    }
}
