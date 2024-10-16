using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumanganSkills : MonoBehaviour
{
    private CharStats charStats;
    public bool canSkill;
    public GameObject pointOfFire, rangeProjectile, bulletHolder;

    public float maxCooldown;

    private void Awake()
    {
        charStats = GetComponentInParent<CharStats>();
        bulletHolder = GameObject.Find("BulletHolder");
        
    }
    private void Start()
    {
        maxCooldown = (charStats.currentAttackspeed * 3f) + 3f;
        charStats.skillCooldown = maxCooldown;
        charStats.skillTime = maxCooldown;
    }
    public void Update()
    {
        if (!canSkill)
        {
            charStats.skillTime += Time.deltaTime;
        }
        else
        {
            charStats.skillTime = maxCooldown;
        }
    }
    public void Skills()
    {
        StartCoroutine(SkillNiDumangan());
    }

    IEnumerator SkillNiDumangan()
    {
        if (canSkill)
        {
            //Attackspeed and Crit Skill
            /*canSkill = false;
            float baseAtkspd = charStats.currentAttackspeed;
            float baseCrit = charStats.currentCritRate;
            charStats.currentAttackspeed -= 0.5f;
            charStats.currentCritRate += 25f;

            yield return new WaitForSeconds(5f);
            charStats.currentAttackspeed = baseAtkspd;
            charStats.currentCritRate = baseCrit;

            yield return new WaitForSeconds((baseAtkspd * 3) + 3);   
            canSkill = true;*/

            //SpreadShot Skill
            canSkill = false;
            charStats.skillTime = 0f;

            int numberOfProjectiles = 5;
            float spreadAngle = 45f;  // Total spread angle for the cone
            float angleStep = spreadAngle / (numberOfProjectiles - 1);
            float startAngle = -spreadAngle / 2;  // Start at the leftmost angle of the cone

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                // Calculate the angle for this projectile
                float angle = startAngle + (i * angleStep);

                // Get the direction for this projectile by rotating the forward direction
                Quaternion rotation = Quaternion.Euler(0, angle, 0) * pointOfFire.transform.rotation;

                // Instantiate the projectile with the calculated rotation
                GameObject projectile = Instantiate(rangeProjectile, pointOfFire.transform.position, rotation, bulletHolder.transform);
                Damage projectileScript = projectile.GetComponent<Damage>();
                projectileScript.charStats = GetComponent<CharStats>();
            }

            yield return new WaitForSeconds((charStats.currentAttackspeed * 3f) + 3f);
            canSkill = true;

        }
    }

    public void DumanganStats()
    {
        charStats.strength = PlayerPrefs.GetInt("Dumangan_Str", 1);
        charStats.agility = PlayerPrefs.GetInt("Dumangan_Agi", 1);
        charStats.intelligence = PlayerPrefs.GetInt("Dumangan_Int", 1);
        charStats.dexterity = PlayerPrefs.GetInt("Dumangan_Dex", 1);
        charStats.vitality = PlayerPrefs.GetInt("Dumangan_Vit", 1);
    }
    public void AcceptUpgrade(float strength, float agility, float intelligence, float dexterity, float vitality)
    {
        PlayerPrefs.SetFloat("Dumangan_Str", strength);
        PlayerPrefs.SetFloat("Dumangan_Agi", agility);
        PlayerPrefs.SetFloat("Dumangan_Int", intelligence);
        PlayerPrefs.SetFloat("Dumangan_Dex", dexterity);
        PlayerPrefs.SetFloat("Dumangan_Vit", vitality);

        PlayerPrefs.Save();
    }
}
