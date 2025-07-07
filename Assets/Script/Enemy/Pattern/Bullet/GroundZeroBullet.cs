using System.Collections;
using UnityEngine;

public class GroundZeroBullet : BulletBase
{
    GameObject[] bullets = new GameObject[3];
    float randomRotation;
    float scalePerSecond;


    public override void Setup(PatternBase patternBase)
    {
        base.Setup(patternBase);
        bullets[0] = transform.GetChild(0).gameObject;
        bullets[1] = transform.GetChild(1).gameObject;
        bullets[2] = transform.GetChild(2).gameObject;
        GroundZero asdf = (GroundZero)patternBase;
        scalePerSecond = asdf.ScalePerSecond;
        ShootBullet();
    }

    private void Update()
    {
        
    }

    protected override void ShootBullet()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        SetRandomRo();
        bullets[0].transform.rotation = Quaternion.Euler(new Vector3(0, 0, -randomRotation));
        bullets[1].transform.rotation = Quaternion.Euler(new Vector3(0, 0, -randomRotation - 120));
        bullets[2].transform.rotation = Quaternion.Euler(new Vector3(0, 0, -randomRotation - 240));

        Vector3[] tempVectors = new Vector3[3];
        tempVectors[0] = new Vector3(Mathf.Sin(randomRotation * Mathf.Deg2Rad), Mathf.Cos(randomRotation * Mathf.Deg2Rad), 0);
        tempVectors[1] = new Vector3(Mathf.Sin((randomRotation + 120) * Mathf.Deg2Rad), Mathf.Cos((randomRotation + 120) * Mathf.Deg2Rad), 0);
        tempVectors[2] = new Vector3(Mathf.Sin((randomRotation + 240) * Mathf.Deg2Rad), Mathf.Cos((randomRotation + 240) * Mathf.Deg2Rad), 0);

        while (true)
        {
            bullets[0].transform.position += speed * Time.deltaTime * tempVectors[0].normalized;
            bullets[1].transform.position += speed * Time.deltaTime * tempVectors[1].normalized;
            bullets[2].transform.position += speed * Time.deltaTime * tempVectors[2].normalized;

            bullets[0].transform.localScale += Vector3.one * Time.deltaTime * scalePerSecond;
            bullets[1].transform.localScale += Vector3.one * Time.deltaTime * scalePerSecond;
            bullets[2].transform.localScale += Vector3.one * Time.deltaTime * scalePerSecond;

            if (Vector3.Distance(bullets[0].transform.position, transform.position) > 5)
            {
                Destroy(gameObject);
            }

            yield return null;
        }
    }

    void SetRandomRo()
    {
        randomRotation = Random.Range(0, 120);
    }
}
