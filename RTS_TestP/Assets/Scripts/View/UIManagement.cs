using Application.Buildings;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{

    private BaseScript baseScriptPlayer;

    public GameObject panelBuilding;
    public Button buttonMenuBuildings;
    public GameObject menuBuildings;

    public GameObject ContentListBuildindings;

    public Text textPeople;
    public Text textGoods;
    public Text textLoans;

    private void Start()
    {
        baseScriptPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseScript>();
        ShowResourcesInfo();
        baseScriptPlayer.ChangingResources.AddListener(ShowResourcesInfo);
        ShowBuildingsList();
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

    public void ShowBuildingsList()
    {
        
        List<Building> buildings = new List<Building>();
        buildings.Add(baseScriptPlayer.portal);
        buildings.Add(baseScriptPlayer.walls);
        buildings.Add(baseScriptPlayer.barracks);

        float y = -100;

        foreach (Building building in buildings)
        {
            GameObject obj = Instantiate(panelBuilding, ContentListBuildindings.transform);
            obj.transform.SetParent(ContentListBuildindings.transform);

            obj.transform.localPosition = new Vector3(0, y, 0);

            Text text = obj.GetComponentInChildren<Text>();
            Button button = obj.GetComponentInChildren<Button>();

            text.text = building.Name + "\n" + "Уровень: " + building.Level;

            button.onClick.AddListener(() =>
            {
                baseScriptPlayer.BuyLevelUpBuildings(building);
            });

            y -= 80;
        }

        foreach (ResidentialModule residentialModule in baseScriptPlayer.residentialModuleList)
        {
            GameObject obj = Instantiate(panelBuilding, ContentListBuildindings.transform);
            obj.transform.SetParent(ContentListBuildindings.transform);

            obj.transform.localPosition = new Vector3(0, y, 0);

            Text text = obj.GetComponentInChildren<Text>();
            Button button = obj.GetComponentInChildren<Button>();

            text.text = residentialModule.Name + "\n" + "Уровень: " + residentialModule.Level;

            button.onClick.AddListener(() =>
            {
                baseScriptPlayer.BuyLevelUpBuildings(residentialModule);
            });

            y -= 80;
        }

        foreach (WorkShop workShop in baseScriptPlayer.workShopList)
        {
            GameObject obj = Instantiate(panelBuilding, ContentListBuildindings.transform);
            obj.transform.SetParent(ContentListBuildindings.transform);

            obj.transform.localPosition = new Vector3(0, y, 0);

            Text text = obj.GetComponentInChildren<Text>();
            Button button = obj.GetComponentInChildren<Button>();

            text.text = workShop.Name + "\n" + "Уровень: " + workShop.Level;

            button.onClick.AddListener(() =>
            {
                baseScriptPlayer.BuyLevelUpBuildings(workShop);
            });

            y -= 80;
        }

    }



}
