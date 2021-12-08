using UnityEngine;

public class MeteorController : MonoBehaviour
{

    [SerializeField]
    Rigidbody2D rigidBody = null;
    [SerializeField]
    GameObject explosionPrefab = null;   // ���ǉ�

    [Min(1), Space]
    public int hp = 1;
    public float speed = 5;
    [Min(0)]
    public int score = 100;

    bool isVisible = false;

    void Start()
    {
        rigidBody.velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Bullet�^�O�����Q�[���I�u�W�F�N�g�ɓ���������HP��1����
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Damage(1);
        }
    }

    void OnBecameVisible()
    {
        isVisible = true;
    }

    void OnBecameInvisible()
    {
        // ��ʊO�ɏo�������
        if (isVisible)
        {
            Destroy(gameObject);
        }
    }

    void Damage(int value)
    {
        if (value <= 0)
        {
            return;
        }

        hp -= value;

        if (hp <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}