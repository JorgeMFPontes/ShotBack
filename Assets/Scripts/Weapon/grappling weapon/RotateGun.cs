using UnityEngine;

public class RotateGun : MonoBehaviour
{
    public Swing swing;


    private void Update()
    {
        if (!swing.isSwinging()) return;
        transform.LookAt(swing.SwingPoint());
    }
}
