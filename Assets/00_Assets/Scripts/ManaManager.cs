using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public float mana { get; private set; } = 100f;

    public Image manaBar;

    private void Start()
    {
        if (manaBar == null)
        {
            GameObject manaObject = GameObject.FindWithTag("Mana");
            if (manaObject != null)
            {
                manaBar = manaObject.GetComponent<Image>();
            }
        }

        mana = 100;
    }

    private void Update()
    {
        if (manaBar != null)
        {
            manaBar.fillAmount = mana / 100;
        }
        if(mana <= 99)
        {
            mana += Time.deltaTime;
        }
    }

    public void UseMana(float amount)
    {
        mana -= amount;
        if (mana < 0)
        {
            mana = 0;
        }
    }


}
