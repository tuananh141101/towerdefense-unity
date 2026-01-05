using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public GameObject buildEffect;

    private TurretBluePrints turretToBuild;
    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStart.Money >= turretToBuild.cost; } }

    public void BuildTurretOn (Node node)
    {
        
        if (PlayerStart.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough to build");
        }

        PlayerStart.Money -= turretToBuild.cost;

        GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void SetTurretToBuild(TurretBluePrints turret)
    {
        turretToBuild = turret;
    }
}
