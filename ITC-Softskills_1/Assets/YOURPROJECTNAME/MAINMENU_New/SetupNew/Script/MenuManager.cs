using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum MenuItems {

	HowToSetup,
	Animation,
	Level1,
	Level2,
	Level3,
	Level4

};

public class MenuManager : MonoBehaviour {
	
	public static MenuManager instance;

	public MenuItems currItem;

	public static bool openObjective;

	public GameObject menuPanel;
	public GameObject objectivePanel;
	public GameObject belowMenu;

	GameObject currObj;

	void Start ( ) {

		instance = this;

		if ( openObjective ) {

			ShowObjectivePanel ( );

		} else {


		}

	}

	public void OnMainMenuBtnClick ( int order ) {
		
		switch ( ( MenuItems ) order ) { 

			case  MenuItems.HowToSetup:
				SceneManager.LoadScene ( 1 );
				break;
			
			case  MenuItems.Animation:
				SceneManager.LoadScene ( "AnimationScene" );
				break;
			
			case  MenuItems.Level1:
				SceneManager.LoadScene ( "Level1" );
				break;
			
			case  MenuItems.Level2:
				SceneManager.LoadScene ( "Level2" );
				break;
			
			case  MenuItems.Level3:
				SceneManager.LoadScene ( "Level3" );
				break;
			
			case  MenuItems.Level4:
				SceneManager.LoadScene ( "Level4" );
				break;
		
		}

		currItem = ( MenuItems ) order;

	}

	public void GoToMainMenu ( ) {

		openObjective = false;
		SceneManager.LoadScene ( 0 );

	}

	public void Quit ( ) {
		
		Application.Quit ( );
		return;

	}

	public void ShowObjectivePanel ( ) {

		openObjective = true;
		menuPanel.SetActive ( false );
		objectivePanel.SetActive ( true );
		belowMenu.SetActive ( true );

	}

}