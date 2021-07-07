using UnityEngine;
using UnityEngine.AI;

public class PlayerStack : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _standardCamera;
    [SerializeField] private GameObject _carCamera;
    [SerializeField] private PlayerMovenment _playerMovenment;
    [SerializeField] private CapsuleCollider _collider;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PropagandaMaterialsStorage _propagandaMaterialsStorage;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    
    public bool InCar;

    public Transform Transform => _transform;
    public Animator Animator => _animator;
    public GameObject StandatdCamera => _standardCamera;
    public GameObject CarCamera => _carCamera;
    public PlayerMovenment PlayerMovenment => _playerMovenment;
    public CapsuleCollider Collider => _collider;
    public Rigidbody Rigidbody => _rigidbody;
    public PropagandaMaterialsStorage PropagandaMaterialsStorage => _propagandaMaterialsStorage;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
}