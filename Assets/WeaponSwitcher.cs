using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private Rifle _rifle;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _blendingDuration;

    private void Start()
    {
        _rifle.gameObject.SetActive(false);
        _animator.SetLayerWeight(1, 0);
    }

    public void EquipRifle()
    {
        _rifle.gameObject.SetActive(true);
        StartCoroutine(RaiseHandSmoothly());
    }

    private IEnumerator RaiseHandSmoothly()
    {
        float startTime = Time.time;
        float progress = 0;
        while (progress < 1f)
        {
            yield return null;
            progress = (Time.time - startTime) / _blendingDuration;
            _animator.SetLayerWeight(1, Mathf.Lerp(0, 1, progress));
        }
    }

    public void DropRifle()
    {
        _rifle.gameObject.SetActive(false);
        StartCoroutine(DropHandSmoothly());
    }

    private IEnumerator DropHandSmoothly()
    {
        float startTime = Time.time;
        float progress = 0f;
        while (progress < 1)
        {
            yield return null;
            progress = (Time.time - startTime) / _blendingDuration;
            _animator.SetLayerWeight(1, Mathf.Lerp(1, 0, progress));
        }
    }
}
