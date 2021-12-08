using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField]
    Rigidbody2D rigidBody = null;
    [SerializeField]
    float speed = 15;

    void Start()
    {
        rigidBody.velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Enemy�^�O�����Q�[���I�u�W�F�N�g�i��覐΁j�ɓ������������
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        // �J�����ɉf��Ȃ��Ȃ��������
        Destroy(gameObject);
    }

}