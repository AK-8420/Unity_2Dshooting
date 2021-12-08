using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool isActive = true;

    [Header("�K�v�ȃR���|�[�l���g��o�^")]
    [SerializeField]
    Rigidbody2D rigidBody = null;
    [SerializeField]
    Transform bulletSpawn = null;
    [SerializeField]
    AudioSource audioSource = null;

    [Header("�ړ��ݒ�")]
    [SerializeField]
    float powerToMove = 10;

    [Header("�ˌ��ݒ�")]
    [SerializeField]
    GameObject bulletPrefab = null;
    [SerializeField, Min(0)]
    float fireInterval = 0.5f;
    [SerializeField]
    AudioClip fireSe = null;

    [Header("���͐ݒ�")]
    [SerializeField]
    string verticalButtonName = "Vertical";
    [SerializeField]
    string fireButtonName = "Fire1";

    bool fire = false;
    bool firing = false;
    float forwardInput;
    Vector2 mousePos;
    WaitForSeconds fireIntervalWait;
    Camera mainCamera;
    Transform thisTransform;
    Transform mainCameraTransform;

    void Start()
    {
        // transform�Ńg�����X�t�H�[�����Q�Ƃ���Ə��������d���̂ŁA�L���b�V�����Ă���
        thisTransform = transform;
        mainCamera = Camera.main;
        mainCameraTransform = mainCamera.transform;

        // �R���[�`���̒�~�������L���b�V�����Ă���
        // ��������ƃ������ɃS�~���������Â炭�Ȃ荂�����ł���
        fireIntervalWait = new WaitForSeconds(fireInterval);
    }

    void OnDisable()
    {
        StopCoroutine(nameof(Fire));
        firing = false;
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        GetInput();

        if (fire && !firing)
        {
            StartCoroutine(nameof(Fire));
        }
    }

    void FixedUpdate()
    {
        if (!isActive)
        {
            return;
        }

        MovePlayer();
    }

    void GetInput()
    {
        // �ړ�
        forwardInput = Input.GetAxis(verticalButtonName);

        // ����
        // �}�E�X���W�i�X�N���[�����W�j���擾���A���[���h���W�ɕϊ�����
        Vector3 screenMousePos = Input.mousePosition;
        screenMousePos.z = mainCameraTransform.position.z;
        mousePos = mainCamera.ScreenToWorldPoint(screenMousePos);

        // �ˌ�
        fire = Input.GetButton(fireButtonName);
    }

    void MovePlayer()
    {
        // �Ȃ߂炩�Ƀ}�E�X�̕���������
        thisTransform.rotation = Quaternion.Lerp(thisTransform.rotation, Quaternion.LookRotation(Vector3.forward, (Vector3)mousePos - thisTransform.position), 0.1f);

        // �ړ�
        rigidBody.AddForce(thisTransform.up * forwardInput * powerToMove * rigidBody.mass, ForceMode2D.Force);
    }

    IEnumerator Fire()
    {
        firing = true;

        // �e�̃Q�[���I�u�W�F�N�g�𐶐�
        Instantiate(bulletPrefab, bulletSpawn.position, thisTransform.rotation);

        yield return fireIntervalWait;

        firing = false;
    }

}