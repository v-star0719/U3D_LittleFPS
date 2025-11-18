using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPanel : MonoBehaviour
{
	public UIWrapContent wrapContent;
	public UIScrollView scrollView;

	public UILabel weaponNameLabel;
	public GameObject equipedTip;
	public GameObject equipBtn;

	private List<WeaponConf> weaponList = new List<WeaponConf>();
	private WeaponConf selectedWeapon = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenPanel()
	{
		gameObject.SetActive(true);

		if (weaponList.Count == 0)
		{
			int m = 0;
			for (int i = 0; i < 20; i++)
			{
				WeaponConf weapon = new WeaponConf();
				WeaponConf orgData = WeaponDataCollection.instance.ConfArray[m++];
				weapon.weaponId = orgData.weaponId;
				weapon.name = string.Format("{0} ({1})", orgData.name, i);
				weapon.modelPrefab = orgData.modelPrefab;
				weapon.damage = orgData.damage;
				weapon.texture = orgData.texture;
				weaponList.Add(weapon);

				if (m >= WeaponDataCollection.instance.ConfArray.Length)
				{
					m = 0;
				}
			}
		}

		//scrollView.ResetPosition();
		wrapContent.minIndex = 1 - weaponList.Count;
		wrapContent.maxIndex = 0;
		wrapContent.onInitializeItem = OnInitializeItem;
		wrapContent.SortAlphabetically();

		UIModelManager.instance.Open();
		OnBagItemClick(WeaponDataCollection.GetConf(GameMain.instance.CurWeaponId));
	}

	public void ClosePanel()
	{
		gameObject.SetActive(false);
		UIModelManager.instance.Close();
	}

	public void OnCloseBtnClick()
	{
		ClosePanel();
	}

	public void OnInitializeItem(GameObject go, int wrapIndex, int realIndex)
	{
		BagItemCtrl itemCtrl = go.GetComponent<BagItemCtrl>();
		itemCtrl.SetInfo(weaponList[-realIndex]);
	}

	public void OnBagItemClick(WeaponConf conf)
	{
		weaponNameLabel.text = conf.name;
		selectedWeapon = conf;
		UpdateBtnStatus();
		UIModelManager.instance.actor.ChangeWeapon(conf);
	}
	
	private void UpdateBtnStatus()
	{
		equipedTip.SetActive(selectedWeapon.weaponId == GameMain.instance.CurWeaponId);
		equipBtn.SetActive(!equipedTip.activeSelf);
	}

	public void OnEquipBtnClick()
	{
		GameMain.instance.CurWeaponId = selectedWeapon.weaponId;
		GameMain.instance.PlayerActor.ChangeWeapon(selectedWeapon);
		UpdateBtnStatus();
	}
}
