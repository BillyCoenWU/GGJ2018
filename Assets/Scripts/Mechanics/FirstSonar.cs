namespace GGJ
{
    using UnityEngine;

    public class FirstSonar : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.GetComponent<GGJMonoBehaviour>().PlaySonar();
           //Debug.Log(collision.gameObject.name);
        }
    }
}

