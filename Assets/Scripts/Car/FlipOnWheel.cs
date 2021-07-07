using UnityEngine;

public class FlipOnWheel : MonoBehaviour
{
    [SerializeField] private Transform _carTransform;
    [SerializeField] private float _minimumAngle = 60;

    private void Update()
    {
        print(_carTransform.localEulerAngles.z);

        if (_carTransform.localEulerAngles.z > _minimumAngle)
        {
            Flip();
        }

        /*if (_carTransform.eulerAngles.z < _minimumAngle * -1)
        {
            Flip();
        }*/
    }

    private void Flip()
    {
        Vector3 respawnPosition = new Vector3(_carTransform.position.x, _carTransform.position.y + 2, _carTransform.position.z);
        _carTransform.position = respawnPosition;
        
        _carTransform.localEulerAngles = Vector3.zero;
    }

}
