using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    
    public int selectedWeapon = 0;
    private GameObject Player;
    private string grappling;
    private GameObject Gun;
    private string Swing;
    private GameObject Prediction_Point;
    public PickUpControllerGrappler Pick;


    private void Awake()
    {
        Player = GameObject.Find("Player");
        grappling = ("Grappling");
        Gun = GameObject.Find("Gun");
        Swing = ("Swing");
        Prediction_Point = GameObject.Find("Prediction_Point");
    }

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchingWeapon();

        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;                
            }
                                
            else
            {
                selectedWeapon++;                
            }
                
            
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) 
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;                
            }
                
            else
            {
                selectedWeapon--;
            }
                
        }

        if (previousSelectedWeapon != selectedWeapon) 
        {
            SelectWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        { 
            selectedWeapon= 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 1;
        }

    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    void SwitchingWeapon()
    {
        if (selectedWeapon <= 0)
        {
            (Player.GetComponent(grappling) as MonoBehaviour).enabled = false;
            (Gun.GetComponent(Swing) as MonoBehaviour).enabled = false;
            Prediction_Point.SetActive(false);
        }
        else if (selectedWeapon == 1 && Pick.equipped) 
        {
            (Player.GetComponent(grappling) as MonoBehaviour).enabled = true;
            (Gun.GetComponent(Swing) as MonoBehaviour).enabled = true;
            Prediction_Point.SetActive(true);
        }
            
    }
}
