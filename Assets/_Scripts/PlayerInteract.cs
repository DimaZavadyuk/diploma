using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private float _interactDistance = 2f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private TextMeshProUGUI _itemText;
    [SerializeField] private AudioClip _interactSound;
    [SerializeField] private AudioClip _interactSound2;

    [Space(20)]
    [SerializeField] private Color _disableInetract = new Color32(0x3D, 0x57, 0x4C, 0xFF);
    [SerializeField] private Color _enabledInetract = new Color32(0xB6, 0xFF, 0xE5, 0xFF);

    private void Awake()
    {
        _itemText.color = _enabledInetract;
    }

    private void Update()
    {
        Ray ray = new Ray(_cameraPosition.position, _cameraPosition.forward * _interactDistance);
        Debug.DrawRay(ray.origin, ray.direction, Color.magenta);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, _interactDistance, _layerMask))
        {
            _itemText.text = hitInfo.transform.name;

            if (Input.GetKeyDown(KeyCode.E))
            {
                SoundFXManager.Instance.PlaySoundFXClip(_interactSound, transform, 0.6f, false);
                SoundFXManager.Instance.PlaySoundFXClip(_interactSound2, transform, 0.6f, false);
                hitInfo.collider.gameObject.GetComponent<IInteractable>().Interact();
            }
        }
        else
        {
            _itemText.text = "";
        }
    }
}
