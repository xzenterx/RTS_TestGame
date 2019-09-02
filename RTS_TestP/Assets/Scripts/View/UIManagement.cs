using Application.Buildings;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models.Units;
using System;

public class UIManagement : MonoBehaviour
{

    private BaseScript baseScriptPlayer;

    public GameObject panelBuildingPrefab;
    public GameObject panelUnitPrefab;

    public Button buttonMenuBuildings;
    public Button buttonMenuUnits;

    public GameObject menuBuildings;
    public GameObject menuUnits;

    public GameObject ContentListBuildings;
    public GameObject ContentListUnits;

    public Text textPeople;
    public Text textGoods;
    public Text textLoans;

    public Text countUnitsAttack;
    public Text countUnitsDefense;
    public Text countUnitsSpeed;

    public InputField inputUnitsAttack;
    public InputField inputUnitsDefense;
    public InputField inputUnitsSpeed;

    private List<GameObject> panelBuildingsList = new List<GameObject>();
    private List<GameObject> panelUnitsList = new List<GameObject>();


    private bool firstCreate = true;

    private void Start()
    {
        baseScriptPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseScript>();

        ShowResourcesInfo();
        CreateBuildingsList();

        baseScriptPlayer.ChangingResources.AddListener(ShowResourcesInfo);
        baseScriptPlayer.ChangingLevelBuildingEvent.AddListener(CreateBuildingsList);
        baseScriptPlayer.ChangingUnitsCountEvent.AddListener(ShowCountUnits);
    }

    public void ShowHideMenuBuildings()
    {
        menuBuildings.SetActive(!menuBuildings.activeSelf);
    }

    public void ShowResourcesInfo()
    {
        textPeople.text = "Люди: " + baseScriptPlayer.playerResources.People.ToString();
        textGoods.text = "Товары: " + baseScriptPlayer.playerResources.Goods.ToString();
        textLoans.text = "Кредиты: " + baseScriptPlayer.playerResources.Loans.ToString();
    }

    public void ButtonExpansionBase()
    {
        baseScriptPlayer.BaseExpansion();
    }

    private void DestroyBuildingList()
    {
        foreach (GameObject building in panelBuildingsList)
        {
            Destroy(building);
        }
    }

    private void GenerateBuildingsList(Vector3 position)
    {

        List<Building> buildings = new List<Building>
        {
            baseScriptPlayer.portal,
            baseScriptPlayer.walls,
            baseScriptPlayer.barracks
        };


        foreach (Building building in buildings)
        {
            GameObject panelBuilding = Instantiate(panelBuildingPrefab, ContentListBuildings.transform);

            panelBuilding.transform.localPosition = position;


            Text text = panelBuilding.GetComponentInChildren<Text>();
            Button button = panelBuilding.GetComponentInChildren<Button>();

            text.text = building.Name + "\n" + "Уровень: " + building.Level;

            button.onClick.AddListener(() =>
            {
                baseScriptPlayer.BuyLevelUpBuildings(building);
            });

            panelBuildingsList.Add(panelBuilding);



            position.y -= 80;
        }

        foreach (ResidentialModule residentialModule in baseScriptPlayer.residentialModuleList)
        {
            GameObject panelBuilding = Instantiate(panelBuildingPrefab, ContentListBuildings.transform);

            panelBuilding.transform.localPosition = position;

            Text text = panelBuilding.GetComponentInChildren<Text>();
            Button button = panelBuilding.GetComponentInChildren<Button>();

            text.text = residentialModule.Name + "\n" + "Уровень: " + residentialModule.Level;

            button.onClick.AddListener(() =>
            {
                baseScriptPlayer.BuyLevelUpBuildings(residentialModule);
            });

            panelBuildingsList.Add(panelBuilding);

            position.y -= 80;
        }

        foreach (WorkShop workShop in baseScriptPlayer.workShopList)
        {
            GameObject panelBuilding = Instantiate(panelBuildingPrefab, ContentListBuildings.transform);

            panelBuilding.transform.localPosition = position;

            Text text = panelBuilding.GetComponentInChildren<Text>();
            Button button = panelBuilding.GetComponentInChildren<Button>();

            text.text = workShop.Name + "\n" + "Уровень: " + workShop.Level;

            button.onClick.AddListener(() =>
            {
                baseScriptPlayer.BuyLevelUpBuildings(workShop);
            });

            panelBuildingsList.Add(panelBuilding);

            position.y -= 80;
        }
    }

    private void CreateBuildingsList()
    {

        DestroyBuildingList();

        Vector3 position;
        if (!firstCreate)
        {
            position.x = 143;
            position.y = -100;
            position.z = 0;
        }
        else
        {
            position.x = 0;
            position.y = -100;
            position.z = 0;
        }


        GenerateBuildingsList(position);

        firstCreate = false;
    }

    public void ShowHideMenuUnits()
    {
        menuUnits.SetActive(!menuUnits.activeSelf);
    }

    public void TrainingAttackUnit()
    {
        baseScriptPlayer.CreateUnit(Convert.ToInt32(inputUnitsAttack.text), 0, 0);
        inputUnitsAttack.text = "";
    }

    public void TrainingDefenseUnit()
    {
        baseScriptPlayer.CreateUnit(0, Convert.ToInt32(inputUnitsDefense.text), 0);
        inputUnitsDefense.text = "";
    }

    public void TrainingSpeedUnit()
    {
        baseScriptPlayer.CreateUnit(0, 0, Convert.ToInt32(inputUnitsSpeed.text));
        inputUnitsSpeed.text = "";
    }

    public void ShowCountUnits()
    {
        countUnitsAttack.text = "Кол-во: " + baseScriptPlayer.unitAttacks.Count;
        countUnitsDefense.text = "Кол-во: " + baseScriptPlayer.unitDefense.Count;
        countUnitsSpeed.text = "Кол-во: " + baseScriptPlayer.unitSpeed.Count;
    }
}
