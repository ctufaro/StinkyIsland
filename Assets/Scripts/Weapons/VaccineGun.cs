using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class VaccineGun : AbstractWeapon
{
    private Animator heroAnimator;
    private float speed = 500f;

    public override string Name
    {
        get { return "VaccineGun"; }
    }

    public void Awake()
    {
        heroAnimator = this.GetComponent<Animator>();        
    }

    public override void Engage(Vector3 position, Quaternion rotation)
    {        
        Rigidbody2D dartPrefab;
        string dartSortingLayer = "Default";
        GameObject dart = Resources.Load("Dart") as GameObject;
        GameObject puff = Resources.Load("SmokePuff") as GameObject;
        puff.renderer.sortingLayerName = "Puff";
        Vector3 moveVector;

        switch (heroAnimator.GetInteger("Direction"))
        {
            case (0):
            case (1):
                moveVector = new Vector3(transform.position.x - 0.063f, transform.position.y + 1.003f, 0f);
                Instantiate(puff, new Vector3(moveVector.x, moveVector.y - .2f, 0f), Quaternion.identity);
                dartPrefab = Instantiate(dart.GetComponent<Rigidbody2D>(), moveVector, Quaternion.identity) as Rigidbody2D;
                dartPrefab.AddForce(transform.up * speed);
                break;
            case (2):
                moveVector = new Vector3(transform.position.x - 0.576f, transform.position.y - 0.205f, 0f);
                Instantiate(puff, new Vector3(moveVector.x + .2f,moveVector.y,0f), Quaternion.identity);
                dartPrefab = Instantiate(dart.GetComponent<Rigidbody2D>(), moveVector, Quaternion.Euler(0, 0, 90f)) as Rigidbody2D;
                dartPrefab.AddForce(-transform.right * speed);
                break;
            case (3):
                moveVector = new Vector3(transform.position.x - 0.049f, transform.position.y - 0.657f, 0f);
                Instantiate(puff, new Vector3(moveVector.x, moveVector.y + .2f, 0f), Quaternion.identity);
                dartPrefab = Instantiate(dart.GetComponent<Rigidbody2D>(), moveVector, Quaternion.Euler(0, 0, 180f)) as Rigidbody2D;
                dartPrefab.AddForce(-transform.up * speed);
                dartSortingLayer = "Dart";
                break;
            case (4):
                moveVector = new Vector3(transform.position.x + 0.578f, transform.position.y - 0.213f, 0f);
                Instantiate(puff, new Vector3(moveVector.x - .2f, moveVector.y, 0f), Quaternion.identity);
                dartPrefab = Instantiate(dart.GetComponent<Rigidbody2D>(), moveVector, Quaternion.Euler(0, 0, 270f)) as Rigidbody2D;
                dartPrefab.AddForce(transform.right * speed);
                break;
            default:
                dartPrefab = null;
                break;
        }

        dartPrefab.renderer.sortingLayerName = dartSortingLayer;
        Destroy(dartPrefab.gameObject, 2f);

    }

    public override void Disengage()
    {
        //print("VaccineGun Disengage");
    }
}