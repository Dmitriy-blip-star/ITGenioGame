using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public Transform cameraTransform; // переменная для сохранения камеры следования (Переменная типа трансформ для отслеживания  движения камеры)
    Transform[] layers; // ?? (массив трех картинок бэка, чтобы удободнее было обращаться к ним)

    public float vievZone = 5f; // ?? При изменении или удалении переменной не были замечены перемены в игре (видимая зона, необходима для своевременного переключения бэков)
    public Transform mainCameraTransform; // переменная для сохранения положения mainCamera
    int leftIndex; // Индексы бэков внутри массива
    int rightIndex;
    public float backgroundSize = 19f; // размер заднего фона (размер картинки)
    public float parralaxSpeed = 0.3f; // Скорость движения задного фона
    float lastCameraX; // последеняя позиция камеры

    void Start()
    {
        
        lastCameraX = mainCameraTransform.position.x; // При старте игры обращаемся к позиции по х нашей камеры
        layers = new Transform[transform.childCount]; // Считаем сколько объектов внутри радительского бэка. Длинна массива == кол-во бэков картинок
        for (int i = 0; i < transform.childCount; i++) // присваиваем каждому эелементу дочерний объект из бэков
        {
            layers[i] = transform.GetChild(i);
            leftIndex= 0;
            rightIndex = layers.Length - 1;
        }
    }

    void ScrollRight() // Перемещение бэков. Смена ласт райт или ласт лефт фонов в зависимости от направления
    {
        float lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }

    void ScrollLeft()
    {
        float lastIndex = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex <0)
        {
            rightIndex = layers.Length - 1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, cameraTransform.position.y); // для того чтобы бэк двигался за нами синхронно
        layers[leftIndex].transform.position = new Vector2(layers[leftIndex].transform.position.x, cameraTransform.position.y);// для того чтобы бэк двигался за нами синхронно
        layers[rightIndex].transform.position = new Vector2(layers[rightIndex].transform.position.x, cameraTransform.position.y);// для того чтобы бэк двигался за нами синхронно

        float deltaX = mainCameraTransform.position.x - lastCameraX;
        lastCameraX= mainCameraTransform.position.x;
        transform.position += Vector3.right * (deltaX * parralaxSpeed);

        if (cameraTransform.position.x < layers[leftIndex].transform.position.x + vievZone) // переход между картинками
        {
            ScrollLeft();
        }

        if (cameraTransform.position.x > layers[rightIndex].transform.position.x - vievZone)
        {
            ScrollRight();
        }
    }
}
