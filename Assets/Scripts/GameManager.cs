using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float score = 0;
    //A reference to our ballController
    [SerializeField] private BallController ball;
    //A reference for our PinCollection prefab we made in Section 2.2
    [SerializeField] private GameObject pinCollection;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform pinAnchor;
    [SerializeField] private InputManager inputManager;
    
    private GameObject pinObjects;
    private FallTrigger[] pins;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputManager.OnResetPressed.AddListener(HandleReset);
        SetPins();
    }

    private void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleReset() {
        ball.ResetBall();
        SetPins();
    }

    private void SetPins()
    {
        if (pinObjects) {
            foreach (Transform child in pinObjects.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(pinObjects);
        }
        pinObjects = Instantiate(pinCollection, pinAnchor.transform.position,  Quaternion.identity, transform);
        pins = FindObjectsByType<FallTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (FallTrigger pin in pins)
        {
            pin.OnPinFall.AddListener(IncrementScore);
        }
    }
}
