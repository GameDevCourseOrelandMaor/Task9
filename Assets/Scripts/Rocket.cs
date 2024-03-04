using Fusion;
using UnityEngine;
using System.Collections;

public class Rocket : NetworkBehaviour
{
    [SerializeField] private float speed = 20f;
    private ConstantForce constForce; // This is a Unity component
    private float rotationAngle;
    private float gravity = 10f;
    private float isRightDirection = 90;
    [SerializeField] private int damage = 5;




    public override void Spawned()
    {
        constForce = GetComponent<ConstantForce>();
        // Get the rotation of the spawned rocket
        rotationAngle = transform.rotation.eulerAngles.z;
    }

    public override void FixedUpdateNetwork()
    {
        if (constForce != null)
        {
            // Move on the axis away from the player that shot it
            float speedDirection = rotationAngle == isRightDirection ? -speed : speed;
            constForce.force = new Vector3(speedDirection, gravity, 0);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        GameObject gameObjectp = other.gameObject;
        if (gameObject && gameObjectp.CompareTag("Player"))
        {
            
            Destroy(gameObject);

            if (other.gameObject.TryGetComponent<Health>(out var health))
            {
                if (other.gameObject.TryGetComponent<Shield>(out var shield))
                {

                    if (!shield.GetIsActive())
                    {
                        if(gameObjectp != this.gameObject.CompareTag("Player"))
                        {
                            health.DealDamageRpc(damage);
                            Debug.Log("rocket hit player");
                        }
                       
                }
            }
          

        }
        else
        {
            Debug.Log("Rocket hit something else");
        }
      
    }


}
}