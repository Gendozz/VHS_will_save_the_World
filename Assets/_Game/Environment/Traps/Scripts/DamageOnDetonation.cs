using System.Collections;
using JSAM;
using UnityEngine;

public class DamageOnDetonation : MonoBehaviour, IActivatable
{
    [Header("Радиус поражения")] [SerializeField]
    private float _damageRadius;

    [Header("Слой игрока")] [SerializeField]
    private LayerMask _playerLayer;

    [Header("Задержка между активацией и взрывом")] [SerializeField]
    private float _delayBeforeExplosion;

    [Header("Размер урона")] [SerializeField]
    private int _damageAmount;

    [Header("Ссылка на ColorLerp")] [SerializeField]
    private ColorLerp _colorLerp;

    [SerializeField] private RedAlertLerp _redAlertLerp;

    private bool _isActivated;

    private Collider[] _playerCollider = new Collider[1];

    private bool _isIntoDamageRadius = false;

    public void Activate()
    {
        if (!_isActivated)
        {
            _isActivated = true;
            StartCoroutine(Detonate());
        }
    }

    private void FixedUpdate()
    {
        CheckPlayerIsInRadius();
    }

    private void CheckPlayerIsInRadius()
    {
        if (_isActivated)
        {
            int collidersAmount =
                Physics.OverlapSphereNonAlloc(transform.position, _damageRadius, _playerCollider, _playerLayer);
            _isIntoDamageRadius = collidersAmount > 0;
        }
    }

    private IEnumerator Detonate()
    {
        _colorLerp.StartLerp();
        _redAlertLerp.StartLerp();
        AudioManager.PlaySoundLoop(Sounds.PreDetonate);
        yield return new WaitForSeconds(_delayBeforeExplosion);
        AudioManager.StopSoundLoop(Sounds.PreDetonate);

        if (_isIntoDamageRadius)
        {
            _playerCollider[0].TryGetComponent<IDamagable>(out IDamagable damagable);
            damagable.TakeDamage(_damageAmount);
            _playerCollider[0].TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement);
            playerMovement.ApplyExternalForce(transform.position);
        }

        SelfDestroy();
    }

    private void SelfDestroy()
    {
        AudioManager.PlaySound(Sounds.Detonate);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _damageRadius);
    }
}