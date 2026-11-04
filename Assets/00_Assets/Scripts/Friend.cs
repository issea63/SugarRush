using UnityEngine;

public class Friend : Interactable
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

        CharacterCombat playerCombat = BossManager.Instance.GetComponent<CharacterCombat>();

        playerCombat.Attack(stats);
        enemycanvas.SetActive(true);
    }
}
