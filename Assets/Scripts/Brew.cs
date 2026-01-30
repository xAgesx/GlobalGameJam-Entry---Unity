


using Unity.Mathematics;
using UnityEngine;

public class Brew : MonoBehaviour {
    [SerializeField] GameObject keyPrefab;
    public float spawnVelocity;
    public Vector3 offset;
    [SerializeField] AudioSource brewingAudio;
    [SerializeField] AudioSource transformingAudio;
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("BrewedPotion")) {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            GetComponent<MeshRenderer>().enabled = true;

            Destroy(other.gameObject);
            brewingAudio.Play();

        }else if (other.CompareTag("Rusty_Key")) {
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(2).GetComponent<ParticleSystem>().Play();
            transformingAudio.Play();
            var newKey = Instantiate(keyPrefab,transform.parent.position - offset,quaternion.identity);
            newKey.GetComponent<Rigidbody>().AddForce(Vector3.up*spawnVelocity);
            Destroy(other.gameObject);
            brewingAudio.Stop();
            newKey.name = "Key_Golden";
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);

        }
    }
}
