using System.Collections;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "RubyDrop", menuName = "Scriptable Objects/RubyDrop")]
public class RubyDrop : PatternBase
{
    [SerializeField]
    GameObject wall;
    Transform[] walls = new Transform[4];
    bool[] isMoveableFields = new bool[4];
    int uxoField;
    RubyDropBullet uxo;
    LineRenderer xLine;
    LineRenderer yLine;

    public override void SetPattern()
    {
        base.SetPattern();
        for (int i = 0; i < isMoveableFields.Length; i++)
        {
            isMoveableFields[i] = true;
        }
    }

    public override void StartPattern()
    {
        base.StartPattern();
    }

    protected override IEnumerator BingPattern()
    {
        enemy.OffRenderer();
        enemy.OnWraning();
        enemy.SetWraningScale(10);
        enemy.transform.position = new Vector3(center.x, center.y, enemy.transform.position.z);

        yield return new WaitForSeconds(5);

        SetUXOField();
        enemy.OnRenderer();
        enemy.OffWraning();
        enemy.SetWraningScale(1);

        yield return null;

        xLine = new GameObject().AddComponent<LineRenderer>();
        yLine = new GameObject().AddComponent<LineRenderer>();
        xLine.positionCount = 2;
        xLine.SetPosition(0, new Vector3(center.x - radius.x - 0.5f, center.y, 1));
        xLine.SetPosition(1, new Vector3(center.x + radius.x + 0.5f, center.y, 1));
        xLine.startColor = Color.green;
        xLine.endColor = Color.green;
        xLine.startWidth = 0.1f;
        xLine.endWidth = 0.1f;
        xLine.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        yLine.positionCount = 2;
        yLine.SetPosition(0, new Vector3(center.x, center.y + radius.y + 0.5f, 1));
        yLine.SetPosition(1, new Vector3(center.x, center.y - radius.y - 0.5f, 1));
        yLine.startColor = Color.green;
        yLine.endColor = Color.green;
        yLine.startWidth = 0.1f;
        yLine.endWidth = 0.1f;
        yLine.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));

        for (int i = 0; i < bulletCount; i++)
        {
            go = Instantiate(bullet, enemy.transform.position, Quaternion.identity);
            go.GetComponent<BulletBase>().Setup(this);
            go.GetComponent<RubyDropBullet>().Setup(i);
            if (uxoField == i)
            {
                uxo = go.GetComponent<RubyDropBullet>();
                uxo.SetUXO();
            }
        }

        yield return new WaitForSeconds(bulletSpawnDelay);

        enemy.SetWraningScale(5);

        while (true)
        {
            enemy.OffRenderer();
            enemy.OnWraning();
            float tempTime = Time.time;
            while (Time.time - tempTime < 2)
            {
                Vector3 temp = PlayerContoller.instance.transform.position - enemy.transform.position;
                enemy.transform.position += temp.normalized * Time.deltaTime * 3;
                yield return null;
            }
            yield return new WaitForSeconds(1);
            enemy.OnRenderer();
            enemy.OffWraning();

            int x = 0;
            int y = 0;
            int tempField = -1;
            if (enemy.transform.position.x < center.x)
            {
                if (enemy.transform.position.y < center.y)
                {
                    //2
                    tempField = 2;
                    x = -1;
                    y = -1;
                }
                else
                {
                    //1
                    tempField = 1;
                    x = -1;
                    y = 1;
                }
            }
            else
            {
                if (enemy.transform.position.y < center.y)
                {
                    //3
                    tempField = 3;
                    x = 1;
                    y = -1;
                }
                else
                {
                    //0
                    tempField = 0;
                    x = 1;
                    y = 1;
                }
            }

            if (tempField == uxoField)
            {
                //끝
                uxo.Boom();
                yield return new WaitForSeconds(1);
                StopPattern();
            }
            else
            {
                isMoveableFields[tempField] = false;
                walls[tempField] = Instantiate(wall, new Vector3(center.x + (x * ((radius.x / 2) + 0.25f)), center.y + (y * ((radius.y / 2) + 0.25f)), 1), Quaternion.identity).transform;
                walls[tempField].transform.localScale = (Vector3)radius + Vector3.one * 0.5f;
            }

            yield return new WaitForSeconds(1);
        }
    }

    void SetUXOField()
    {
        uxoField = Random.Range(0, 4);
    }

    public override void StopPattern()
    {
        base.StopPattern();
        Destroy(uxo.gameObject);
        Destroy(xLine.gameObject);
        Destroy(yLine.gameObject);
        for (int i = 0; i < walls.Length; i++)
        {
            if (!isMoveableFields[i])
            {
                Destroy(walls[i]?.gameObject);
            }
        }
    }
}
