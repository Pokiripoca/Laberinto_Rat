using UnityEngine;

public class Cabeza : MonoBehaviour
{
    public float veloCabeza = 12f;
    public float tambaleo = 8f;
    public float resetSpeed;
    public CharacterController playerController;

    public RectTransform rtrans;
    private Vector2 defpos;
    private float timer = 0f;

    private void Awake()
    {
      rtrans =GetComponent<RectTransform>(); //transform de la recta asignado a un getcomponent
        defpos = rtrans.anchoredPosition; //defpos es la posicion inicial

        if (playerController == null)
        {
            playerController = GetComponent<CharacterController>();
        }
    }

    private void Update()
    {
        Debug.Log("Me muevo?" + IsPlayerMoving());
        if (IsPlayerMoving())
        { //si mi jugador camina...
            //Como usaremos la funcion seno para q el jugador tambalee, usaremos un temporizador que
            //avance en el tiempo para generar una onda que suba y baje, cada que el jugador frene se
            //reinicia a 0 para q comience desde el centro.

            timer += Time.deltaTime * veloCabeza; //que tan rapído y frecuente sera el subir y bajar (los pasos)

            float newY = defpos.y + Mathf.Sin(timer) * tambaleo; //calcular nueva altura

            rtrans.anchoredPosition = new Vector2(defpos.x, newY); //solo cambia en y y lo aplica

        }
        else {
            timer = 0f; //se resetea la funcion y se detiene
            rtrans.anchoredPosition = Vector2.Lerp(rtrans.anchoredPosition,defpos,Time.deltaTime*resetSpeed); //a la posicion actual la desplaza a defpos

        }

    }

    private bool IsPlayerMoving()
    {
        if (playerController != null) { 
        Vector3 horVelo=new Vector3(playerController.velocity.x,0,playerController.velocity.z); //ignorar el bamboleo en el eje Y si se salta o cae
            return playerController.isGrounded && horVelo.magnitude > 0.1f; //tiene q estar en el suelo y moviendose
        }

        return Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f;
    }
}
