using Application.Buildings;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{

    private BaseScript baseScriptPlayer;

    public GameObject panelBuildingPrefab;
    public Button buttonMenuBuildings;
    public GameObject menuBuildings;

    public GameObject ContentListBuildings;

    public Text textPeople;
    public Text textGoods;
    public Text textLoans;

    private List<GameObject> panelBuildingsList = new List<GameObject>();

    private bool firstCreate = true;

    private void Start()
    {
        baseScriptPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseScript>();
        ShowResourcesInfo();
        CreateBuildingsList();
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
        int y = 0;

        List<Building> buildings = new List<Building>
        {
            baseScriptPlayer.portal,
            baseScriptPlayer.walls,
            baseScriptPlayer.barracks
        };


        foreach (Building building in buildings)
        {
            GameObject obj = Instantiate(panelBuildingPrefab, ContentListBuildings.transform);
            obj.transform.SetParent(ContentListBuildings.transform);

            obj.transform.localPosition = position;


            Text text = obj.GetComponentInChildren<Text>();
            Button button = obj.GetComponentInChildren<Button>();

            text.text = building.Name + "\n" + "Уровень: " + building.Level;

            button.onClick.AddListener(() =>
            {
                baseScriptPlayer.BuyLevelUpBuildings(building);
            });

            panelBuildingsList.Add(obj);



            position.y -= 80;
        }

        foreach (ResidentialModule residentialModule in baseScriptPlayer.residentialModuleList)
        {
            GameObject obj = Instantiate(panelBuildingPrefab, ContentListBuildings.transform);
            obj.transform.SetParent(ContentListBuildings.transform);

            obj.transform.localPosition = position;

            Text text = obj.GetComponentInChildren<Text>();
            Button button = obj.GetComponentInChildren<Button>();

            text.text = residentialModule.Name + "\n" + "Уровень: " + residentialModule.Level;

            button.onClick.AddListener(() =>
            {
                baseScriptPlayer.BuyLevelUpBuildings(residentialModule);
            });

            panelBuildingsList.Add(obj);

            position.y -= 80;
        }

        foreach (WorkShop workShop in baseScriptPlayer.workShopList)
        {
            GameObject obj = Instantiate(panelBuildingPrefab, ContentListBuildings.transform);
            obj.transform.SetParent(ContentListBuildings.transform);

            obj.transform.localPosition = position;

            Text text = obj.GetComponentInChildren<Text>();
            Button button = obj.GetComponentInChildren<Button>();

            text.text = workShop.Name + "\n" + "Уровень: " + workShop.Level;

            button.onClick.AddListener(() =>
            {
                baseScriptPlayer.BuyLevelUpBuildings(workShop);
            });

            panelBuildingsList.Add(obj);

            position.y -= 80;
        }
    }

    public void CreateBuildingsList()
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

}
