using UnityEngine;

public class EnemyPatrol : MonoBehaviour {

    public float speed;
    public Transform[] waypoints;   //Les différents waypoints par lesquels l'ennemi va passer

    public SpriteRenderer graphics;
    private Transform target;   //cible vers laquelle l'ennemi va se déplacer (un des waypoints)
    private int desPoint = 0;   //pt de destination (index)

    // Start is called before the first frame update
    void Start () {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update () {
        Vector3 dir = target.position - transform.position; //position de la cible - celle actuelle
        transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);

        //Si l'ennemi est quasiment arrivé à sa destination, changer de cible
        if (Vector3.Distance (transform.position, target.position) < 0.3f) {
            desPoint = (desPoint + 1) % waypoints.Length;
            target = waypoints[desPoint];
            graphics.flipX = !graphics.flipX; //flip de l'ennemi
        }
    }
}