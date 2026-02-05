using UnityEngine;

public interface IDeflectable
{
    public void deflect(Vector2 direction);

    public float ReturnSpeed {get; set;}
}
