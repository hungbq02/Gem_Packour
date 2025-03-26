using System.Collections;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    [Header("Flame Thrower Settings")]
    [SerializeField] private ParticleSystem vfxFlameThrower; 
    [SerializeField] private GameObject triggerDeadZone;    
    [SerializeField] private float playDuration = 3f;      
    [SerializeField] private float stopDuration = 3f;

    [SerializeField] private float startDelay = 2f;

    private void Start()
    {
        StartCoroutine(FlameThrowerLoop());
    }

    private IEnumerator FlameThrowerLoop()
    {
        yield return new WaitForSeconds(startDelay);
        while (true) //
        {
            vfxFlameThrower.Play();
            triggerDeadZone.SetActive(true);

            yield return new WaitForSeconds(playDuration);

            vfxFlameThrower.Stop();
            triggerDeadZone.SetActive(false);
            vfxFlameThrower.Clear(); 
            yield return new WaitForSeconds(stopDuration);
        }
    }
}
