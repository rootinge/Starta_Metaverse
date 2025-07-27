using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance { get { return instance;}}

    public float fireWaterBastScore;
    public float fireWaterLastScore;

    public int flappyPlaneBestScore;
    public int flappyPlaneLastScore;

    public Transform oldPlayerTransform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        fireWaterBastScore = 0;
        fireWaterLastScore = 0;
        flappyPlaneBestScore = 0;
        flappyPlaneLastScore = 0;
        oldPlayerTransform = null;
    }

}
