using UnityEngine;

public class Enemy : Interactable
{
    CharacterStats stats;
    public GameObject enemycanvas;
    private void Start()
    {
        stats = GetComponent<CharacterStats>();
        enemycanvas.SetActive(false);
    }
    public override void Interact()
    {
        base.Interact();

        CharacterCombat playerCombat = PlayerManager.Instance.GetComponent<CharacterCombat>();

        playerCombat.Attack(stats);
        enemycanvas.SetActive(true);
    }
}
