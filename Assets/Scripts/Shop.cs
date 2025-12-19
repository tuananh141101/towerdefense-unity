using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchasesStandardTurret ()
    {
        Debug.Log("Standard Turret Purchased");
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }

    public void PurchasesAnotherTurret ()
    {
        Debug.Log("Another Turret Purchased");
        buildManager.SetTurretToBuild(buildManager.anothersTurretPrefab);
    }
}
