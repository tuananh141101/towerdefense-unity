using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    public TurretBluePrints standardTurret;
    public TurretBluePrints missleLauncher;
    public TurretBluePrints lasersTurret;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret ()
    {
        buildManager.SetTurretToBuild(standardTurret);
    }

    public void SelectMissileTurret ()
    {
        buildManager.SetTurretToBuild(missleLauncher);
    }

    public void SelectLaserTurret ()
    {
        buildManager.SetTurretToBuild(lasersTurret);
    }
}
