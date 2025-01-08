using UnityEngine;

public class DragAnywhereController : MonoBehaviour
{
    [Header("Paramètres de drag")]
    [Tooltip("Facteur de multiplication du déplacement.\n1 = déplacement identique,\n>1 = déplacement amplifié,\n<1 = déplacement réduit.")]
    public float dragScale = 1f;

    [Header("Contraintes de mouvement")]
    [Tooltip("Geler le déplacement sur l'axe X si vrai.")]
    public bool freezeX;

    [Tooltip("Geler le déplacement sur l'axe Z si vrai.")]
    public bool freezeZ;

    bool isDragging;

    // Positions mémorisées au début du drag
    Vector3 objectStartPos; // Position initiale de l'objet
    Vector3 pointerStartWorldPos; // Position initiale du pointeur en coordonnées monde
    float zCoord; // Distance entre la caméra et l'objet

    void Update()
    {
        // Détecter l'entrée (Touch sur mobile, Souris sur PC)
        if (Input.touchCount > 0)
        {
            HandleTouchInput(Input.GetTouch(0));
        }
        else
        {
            HandleMouseInput();
        }
    }

    /// <summary>
    ///     Gère les entrées tactiles (mobile).
    /// </summary>
    /// <param name="touch">Le toucher actuel.</param>
    void HandleTouchInput(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                StartDragging(touch.position);
                break;

            case TouchPhase.Moved:
                if (isDragging)
                {
                    UpdateDragging(touch.position);
                }
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                StopDragging();
                break;
        }
    }

    /// <summary>
    ///     Gère les entrées de la souris (PC).
    /// </summary>
    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            UpdateDragging(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }
    }

    /// <summary>
    ///     Démarre le processus de dragging.
    /// </summary>
    /// <param name="screenPosition">La position de l'écran du pointeur (souris ou touch).</param>
    void StartDragging(Vector3 screenPosition)
    {
        isDragging = true;

        // Calculer la distance en Z entre la caméra et l'objet
        zCoord = transform.position.z - Camera.main.transform.position.z;

        // Mémoriser la position initiale de l'objet
        objectStartPos = transform.position;

        // Convertir la position du pointeur en coordonnées monde
        pointerStartWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, zCoord)
        );
    }

    /// <summary>
    ///     Met à jour la position de l'objet pendant le dragging.
    /// </summary>
    /// <param name="screenPosition">La position actuelle de l'écran du pointeur.</param>
    void UpdateDragging(Vector3 screenPosition)
    {
        // Convertir la position actuelle du pointeur en coordonnées monde
        Vector3 pointerCurrentWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, zCoord)
        );

        // Calcul de la différence (delta) et application du facteur de scale
        Vector3 delta = (pointerCurrentWorldPos - pointerStartWorldPos) * dragScale;

        // Calculer la nouvelle position en ajoutant le delta à la position initiale
        Vector3 newPosition = objectStartPos + delta;

        // Appliquer les contraintes de mouvement
        if (freezeX)
        {
            newPosition.x = objectStartPos.x;
        }

        if (freezeZ)
        {
            newPosition.z = objectStartPos.z;
        }

        // Garder la position Y inchangée pour figer le mouvement en Y (déjà géré par les contraintes Rigidbody si nécessaire)
        newPosition.y = objectStartPos.y;

        // Appliquer la position finale
        transform.position = newPosition;
    }

    /// <summary>
    ///     Termine le processus de dragging.
    /// </summary>
    void StopDragging() => isDragging = false;
}
