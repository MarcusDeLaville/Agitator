using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorChanger : MonoBehaviour
{
    [SerializeField] private Avatar _ragdollAvatar;

    private const string IsRagdoll = nameof(IsRagdoll);
    private Animator _animator;
    private Avatar _characterAvatar;
    private Coroutine _standing;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterAvatar = _animator.avatar;
    }

    public void ChangeAvatar()
    {
        _animator.avatar = _ragdollAvatar;
        _animator.SetBool(IsRagdoll, true);
        if (_standing == null)
        {
            _standing = StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(4f);
        _animator.avatar = _characterAvatar;
        _animator.SetBool(IsRagdoll, false);
        _animator.Play("Kik Up");
        print("played");
    }
}
