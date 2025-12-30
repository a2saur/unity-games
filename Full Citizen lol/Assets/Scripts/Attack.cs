using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject player;
    public GameObject attackEffect;
    private bool attacking = false;
    private float attackingDuration;

    private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackEffect.SetActive(false);
        offset = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        /* --- Attacking --- */
        if (attacking){
            attackingDuration -= Time.deltaTime;
            if (attackingDuration <= 0) {
                attacking = false;
                attackEffect.SetActive(false);
            }

            attackEffect.transform.position = player.transform.position + offset;
        }
    }

    public void StartAttack(int facingDir, int lookingDir){
        // attack!
        attackingDuration = SettingsManager.attackDuration;
        attacking = true;
        attackEffect.SetActive(true);
        if (lookingDir == 0){
            // just left/right
            offset = new Vector3(facingDir*SettingsManager.charWidth/2, 0, 0);
        } else {
            offset = new Vector3(0, lookingDir*SettingsManager.charHeight/2, 0);
        }
    }
}
