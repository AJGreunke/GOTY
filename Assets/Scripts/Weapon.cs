using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    GameObject _heldByParent;
    GameObject _attachmentLocator;

    [SerializeField, Tooltip("Copy the rotation of the attachment point?")]
    private bool _useParentRotation;

    public GameObject _pivotObj;

    [SerializeField, Tooltip("Pause movement after an attack?")]
    float _pauseMovementMax = 0.4f;
    float _pauseMovementTimer = 0.0f;

    [SerializeField, Tooltip("The bullet projectile to fire.")]
    private GameObject _bulletToSpawn;

    [SerializeField, Tooltip("Animation to play when attacking.")]
    public string _attackAnim = "SwordAttack01";

    // Update is called once per frame
    void Update()
    {
        if (_pauseMovementTimer > 0f)
        {
            _pauseMovementTimer -= Time.deltaTime;
            // return;
        }

        if (_attachmentLocator)
        {
            // reposition the weapon gfx in relation to whoever
            // has this weapon equipped
            Transform tr = _attachmentLocator.transform;
            transform.position = tr.position;
            transform.eulerAngles = tr.eulerAngles;

            /*
            Vector3 newRot = Vector3.zero;
            //if (_useParentRotation)
                newRot += tr.eulerAngles;
            //else
              //  newRot.y += tr.eulerAngles.y;

            transform.eulerAngles = newRot;
            */
        }

        // -------------------------
    }

    public void SetAttachmentParent(GameObject locatorObj, GameObject heldByObj)
    {
        _attachmentLocator = locatorObj;
        _heldByParent = heldByObj;
    }

    public bool IsMovementPaused()
    {
        return (bool)(_pauseMovementTimer > 0);
    }

    public Vector3 swing = new Vector3(45, -90f, 90f);

    public void onAttack(Vector3 facing)
    {
        // pause movement
        _pauseMovementTimer = _pauseMovementMax;

        //handle the 'swing sword' logic
        //transform.position = transform.position + facing;
        //transform.Rotate(new Vector3(45, -90f, 90f));
        _pauseMovementTimer = _pauseMovementMax;

        // handle the 'fire blaster' logic
        if (_bulletToSpawn)
        {
            GameObject newBullet = Instantiate(_bulletToSpawn, transform.position, Quaternion.identity);
            Bullet bullet = newBullet.GetComponent<Bullet>();
            if (bullet)
            {
                bullet.SetDirection(new Vector3(facing.x, 0f, facing.z));
            }
        }
    }
}