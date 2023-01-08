using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;

public class UI : MonoBehaviour
{
    //[SerializeField] TowerManager towerManager;
    //[SerializeField] EnemyManager EnemyManager;
    VisualElement towerMenu;
    VisualElement panelCreateTower;
    VisualElement panelTowerOptions;
    VisualElement menu;
    VisualElement panelGameOwer;
    VisualElement panelPause;
    Label moneyLabel;
    private const string AnimHideTowerMeny = "popAnimationHide";
    private const string AnimShowTowerMenu = "popAnimationShow";


    private bool isShowMenuTower = false;
    public static UI Instance;

    private void Test()
    {
        Debug.Log("test");
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        towerMenu = root.Q<VisualElement>("TowerMenu");
        menu = root.Q<VisualElement>("Menu");
        panelGameOwer = root.Q<VisualElement>("PanelGameOwer");
        panelPause = root.Q<VisualElement>("PanelPause");
        moneyLabel = root.Q<Label>("MoneyLabel");
        panelTowerOptions = root.Q<VisualElement>("PanelTowerOptions");
        panelCreateTower = root.Q<VisualElement>("PanelCreateTower");
        Button Flamethrower = root.Q<Button>("Flamethrower");
        Button Gutling = root.Q<Button>("Gutling");
        Button PlasmaGun = root.Q<Button>("PlasmaGun");
        Button TestCreateEnemy1 = root.Q<Button>("TestCreateEnemy1");
        Button TestCreateEnemy2 = root.Q<Button>("TestCreateEnemy2");
        Button resume = root.Q<Button>("Resume");
        Button restartG = root.Q<Button>("RestartG");
        Button restartP = root.Q<Button>("RestartP");
        Button pause = root.Q<Button>("Pause");
        Button sellTower = root.Q<Button>("SellTower");
        Button repairTower = root.Q<Button>("RepairTower");

        //MyEventManager.OnChencheAmountMony.AddListener(GetAmontMoney);
        //MyEventManager.OnDestroyBase.AddListener(ShowMenyGameOwer);
        //MyEventManager.OnPauseEnable.AddListener(ShowMenuPause);

        //repairTower.clicked += RepairTower;
        //sellTower.clicked += SellTowerClicked;
        ////DestroyTower.clicked += Test;
        //pause.clicked += PauseClicked;
        //restartG.clicked += RestartSceneClicked;
        //restartP.clicked += RestartSceneClicked;
        //resume.clicked += ResumeClicked;
       
        //Flamethrower.clicked += towerManager.AddFlamethrower;
        //Gutling.clicked += towerManager.AddGutling;
        //PlasmaGun.clicked += towerManager.AddPlasmaGun;
       
        //TestCreateEnemy1.clicked += EnemyManager.Instance.AddEnemy1;
        //TestCreateEnemy2.clicked += EnemyManager.Instance.AddEnemy2;        
    }

    //private void RepairTower()
    //{
    //    towerManager.RepairSelectedTower();
    //}

    //private void ResumeClicked()
    //{
    //    HideAllMeny();
    //    GameManager.Instance.GamePauseDisable();
    //}

    //private void PauseClicked()
    //{ 
    //    GameManager.Instance.GamePauseEnable(); 
    //}

    //private void SellTowerClicked()
    //{
    //    Debug.Break();
    //    MyEventManager.SendSellTower();
    //    ///towerManager.DestroyTower();
    //    HideAllMeny();
    //}

    //private void Start()
    //{
    //    GetAmontMoney();
    //}

    private void RestartSceneClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ShowMenyGameOwer()
    {
        menu.style.visibility = Visibility.Visible;
        panelGameOwer.style.visibility = Visibility.Visible;
    }

    public void HideAllMeny()
    {
        menu.style.visibility = Visibility.Hidden;
        panelGameOwer.style.visibility = Visibility.Hidden;
        panelPause.style.visibility = Visibility.Hidden;
        panelTowerOptions.style.visibility = Visibility.Hidden;
        panelCreateTower.style.visibility = Visibility.Hidden;
    }

    public void ShowMenuPause()
    {
        menu.style.visibility = Visibility.Visible;
        panelPause.style.visibility = Visibility.Visible;

    }
    //public void GetAmontMoney()
    //{
    //    moneyLabel.text = GameManager.Instance.GetAmountMoney().ToString();
    //}

    public void ShowPanelTowerOptios()
    {
        isShowMenuTower = true;
        //Debug.Log("show");
        panelTowerOptions.style.visibility = Visibility.Visible;
        panelCreateTower.style.visibility = Visibility.Hidden;
        PopupTowerMenu();
        // menu.style.visibility = Visibility.Visible;
    }

    public void ShowPanelCreateTower()
    {
        isShowMenuTower = true;
        //Debug.Log("show");
        panelCreateTower.style.visibility = Visibility.Visible;
        panelTowerOptions.style.visibility = Visibility.Hidden;
        PopupTowerMenu();
    }

    private void PopupTowerMenu()
    {
        towerMenu.EnableInClassList(AnimHideTowerMeny, false);
        towerMenu.EnableInClassList(AnimShowTowerMenu, true);
    }

    private void PopDonwTowermenu()
    {
        Invoke("TryHideTowerMeny", 0.2f); // so that the panel TowerMenu does not jerk down 
    }

    public void HideTowerMeny()
    {
        isShowMenuTower = false;
        PopDonwTowermenu();
    }
    private void TryHideTowerMeny()
    {
        if (isShowMenuTower == false)
        {
            //Debug.Log("Hide");
            towerMenu.EnableInClassList(AnimHideTowerMeny, true);
            towerMenu.EnableInClassList(AnimShowTowerMenu, false);
        }

    }

}
