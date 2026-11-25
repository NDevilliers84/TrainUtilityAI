using UnityEngine;
using UnityEngine.UI;

public class UtilityUI : MonoBehaviour {
    
    [SerializeField] AIController _aiController;
    
    [SerializeField] private Image _fatigueImage;
    [SerializeField] private Image _hungerImage;
    [SerializeField] private Image _boredomImage;
    [SerializeField] private Image _bladderImage;
    [SerializeField] private Image _distanceToFridgeImage;
    [SerializeField] private Image _distanceToToiletImage;
    [SerializeField] private Image _distanceToBedImage;
    [SerializeField] private Image _distanceToPCImage;
    
    [SerializeField] private Image _restImage;
    [SerializeField] private Image _feedImage;
    [SerializeField] private Image _playImage;
    [SerializeField] private Image _peeImage;
    
    private void Update() {
        _fatigueImage.fillAmount = _aiController.Context["Fatigue"];
        _hungerImage.fillAmount = _aiController.Context["Hunger"];
        _boredomImage.fillAmount = _aiController.Context["Boredom"];
        _bladderImage.fillAmount = _aiController.Context["Bladder"];
        
        _distanceToFridgeImage.fillAmount = _aiController.Context["DistanceToFridge"];
        _distanceToToiletImage.fillAmount = _aiController.Context["DistanceToToilet"];
        _distanceToBedImage.fillAmount = _aiController.Context["DistanceToBed"];
        _distanceToPCImage.fillAmount = _aiController.Context["DistanceToPC"];
    }
}