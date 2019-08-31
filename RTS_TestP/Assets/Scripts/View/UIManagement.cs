using Application.Buildings;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models.Units;

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

    private List<GameObject> panelBuildingsList = new List<GameObject>();
    private List<GameObject> panelUnitsList = new List<GameObject>();


    private bool firstCreate = true;

    private void Start()
    {
        baseScriptPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseScript>();
        ShowResourcesInfo();
        CreateBuildingsList();
        CreateUnitsList();
        baseScriptPlayer.ChangingResources.AddListener(ShowResourcesInfo);
        baseScriptPlayer.ChangingLevelBuildingEvent.AddListener((CreateBuildingsList));
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
            //panelBuilding.transform.SetParent(ContentListBuildings.transform);

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
            //panelBuilding.transform.SetParent(ContentListBuildings.transform);

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
            //panelBuilding.transform.SetParent(ContentListBuildings.transform);

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

    private void CreateUnitsList()
    {

        string[] names = new string[] {"Юниты Атаки", "Юниты Защиты", "Юниты Скорости"};

        Vector3 position;
        position.x = 0;
        position.y = -70;
        position.z = 0;

        {
            GameObject panelUnitAttack = Instantiate(panelUnitPrefab, ContentListUnits.transform);
            panelUnitAttack.transform.SetParent(ContentListUnits.transform);

            panelUnitAttack.transform.localPosition = position;

            Text text = panelUnitAttack.GetComponentInChildren<Text>();
            Button button = panelUnitAttack.GetComponentInChildren<Button>();

            text.text = names[0] + "\n" + "Кол-во: " + baseScriptPlayer.unitAttacks.Count;

            /*button.onClick.AddListener(() =>
            {
                baseScriptPlayer.BuyLevelUpBuildings(workShop);
            });*/

            panelUnitsList.Add(panelUnitAttack);

            position.y -= 80;

        }

        {
            GameObject panelUnitDefense = Instantiate(panelUnitPrefab, ContentListUnits.transform);
            panelUnitDefense.transform.SetParent(ContentListUnits.transform);

            panelUnitDefense.transform.localPosition = position;

            Text text = panelUnitDefense.GetComponentInChildren<Text>();
            Button button = panelUnitDefense.GetComponentInChildren<Button>();

            text.text = names[1] + "\n" + "Кол-во: " + baseScriptPlayer.unitDefense.Count;

            /*button.onClick.AddListener(() =>
            {
                baseScriptPlayer.BuyLevelUpBuildings(workShop);
            });*/

            panelUnitsList.Add(panelUnitDefense);

            position.y -= 80;
        }

        {
            GameObject panelUnitSpeed = Instantiate(panelUnitPrefab, ContentListUnits.transform);
            panelUnitSpeed.transform.SetParent(ContentListUnits.transform);

            panelUnitSpeed.transform.localPosition = position;

            Text text = panelUnitSpeed.GetComponentInChildren<Text>();
            Button button = panelUnitSpeed.GetComponentInChildren<Button>();

            text.text = names[0] + "\n" + "Кол-во: " + baseScriptPlayer.unitSpeed.Count;

            /*button.onClick.AddListener(() =>
            {
                baseScriptPlayer.BuyLevelUpBuildings(workShop);
            });*/

            panelUnitsList.Add(panelUnitSpeed);

            position.y -= 80;
        }

    }

}
