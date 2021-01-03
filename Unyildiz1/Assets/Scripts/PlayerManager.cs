using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float health, bulletSpeed;

    bool dead = false;

    Transform muzzle;

    public Transform bullet, floatingText;
    public Slider slider;
    bool mouseIsNotOverUI;

    // Start is called before the first frame update
    void Start()
    {
        muzzle = transform.GetChild(1);
        slider.maxValue = health;
        slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        mouseIsNotOverUI = EventSystem.current.currentSelectedGameObject == null;

        if (Input.GetMouseButtonDown(0) && mouseIsNotOverUI)
        {
            ShootBullet();
        }
    }

    public void GetDamage(float damage)
    {
        Instantiate(floatingText, transform.position, gameObject.transform.rotation).GetComponent<TextMesh>().text = damage.ToString();

        if ((health - damage) >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        slider.value = health;
        AmIDead();
        
    }

    void AmIDead()
    {
        if (health <= 0)
        {
            dead = true;
        }
    }
    void ShootBullet()
    {
        Transform tempBullet;
        tempBullet = Instantiate(bullet, muzzle.position, gameObject.transform.rotation);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(muzzle.forward * bulletSpeed);
        DataManager.Instance.ShotBullet++;
    }

}
