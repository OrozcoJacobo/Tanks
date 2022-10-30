using UnityEngine;

public class ObjectGeneratorRandomPositionUtil : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private float _radius = 0.2f;

    protected Vector2 GetRandomPosition()
    {
        return Random.insideUnitCircle * _radius + (Vector2)transform.position;    
    }

    protected Quaternion Random2DRotation()
    {
        return Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    public void CreateObject()
    {
        Vector2 position = GetRandomPosition();
        GameObject impactObject = GetObject();
        impactObject.transform.position = position;
        impactObject.transform.rotation = Random2DRotation();
    }

    protected virtual GameObject GetObject()
    {
        return Instantiate(_objectPrefab);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
