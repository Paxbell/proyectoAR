using UnityEngine;

public class ballController : MonoBehaviour
{

    [SerializeField] private float timeAutodestroy = 20f;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeAutodestroy)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("target"))
        {
            Debug.Log("COLISIÓN CON BOTELLA!");

            bottleScript botella = collision.gameObject.GetComponent<bottleScript>();
            if (botella != null)
            {
                botella.shatter();
            }
            else
            {
      
                Destroy(collision.gameObject);
            }

            gameManager.instance.UpdateScore(1f);
            Destroy(gameObject, 3f); 

        }
    }
}
