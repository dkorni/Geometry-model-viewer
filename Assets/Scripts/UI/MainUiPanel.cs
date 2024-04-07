using GMV.Core;
using GMV.Core.DrawSrtrategies;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUiPanel : MonoBehaviour
{
    [SerializeField] private DrawSrtrategy[] figures;
    [SerializeField] private UIDocument document;
    [SerializeField] private GeometryBuilder geometryBuilder;

    public Transform objectToCenter;

    private Dictionary<string, int> figureNameToIndexDict = new Dictionary<string, int>();
    private DropdownField figureDropdown;

    private Slider positionXSlider;
    private Slider positionYSlider;
    private Slider positionZSlider;

    private Slider rotationXSlider;
    private Slider rotationYSlider;
    private Slider rotationZSlider;

    private Slider drawingPositionXSlider;
    private Slider drawingPositionYSlider;

    private Slider drawingRotationSlider;

    // Start is called before the first frame update
    void Awake()
    {
        figureDropdown = document.rootVisualElement.Q<DropdownField>("FigureDropdown");
        figureDropdown.choices = figures.Select(x=> x.name).ToList();

        for (int i = 0; i < figures.Length; i++) figureNameToIndexDict[figures[i].name] = i;

        figureDropdown.RegisterValueChangedCallback(OnFigureDropdownValueChanged);

        positionXSlider = document.rootVisualElement.Q<Slider>("PositionX");
        positionXSlider.RegisterValueChangedCallback(_ => MakeEuclideanTransformations());

        positionYSlider = document.rootVisualElement.Q<Slider>("PositionY");
        positionYSlider.RegisterValueChangedCallback(_ => MakeEuclideanTransformations());

        positionZSlider = document.rootVisualElement.Q<Slider>("PositionZ");
        positionZSlider.RegisterValueChangedCallback(_ => MakeEuclideanTransformations());

        rotationXSlider = document.rootVisualElement.Q<Slider>("RotationX");
        rotationXSlider.RegisterValueChangedCallback(_ => MakeEuclideanTransformations());

        rotationYSlider = document.rootVisualElement.Q<Slider>("RotationY");
        rotationYSlider.RegisterValueChangedCallback(_ => MakeEuclideanTransformations());

        rotationZSlider = document.rootVisualElement.Q<Slider>("RotationZ");
        rotationZSlider.RegisterValueChangedCallback(_ => MakeEuclideanTransformations());

        drawingPositionXSlider = document.rootVisualElement.Q<Slider>("DrawingPositionX");
        drawingPositionXSlider.RegisterValueChangedCallback(_ => TransformDrawing());

        drawingPositionYSlider = document.rootVisualElement.Q<Slider>("DrawingPositionY");
        drawingPositionYSlider.RegisterValueChangedCallback(_ => TransformDrawing());

        //drawingRotationSlider = document.rootVisualElement.Q<Slider>("DrawingRotation");
        //drawingRotationSlider.RegisterValueChangedCallback(_ => TransformDrawing());
    }

    private void OnFigureDropdownValueChanged(ChangeEvent<string> @event)
    {
        ResetValues();
        var figure = figures[figureNameToIndexDict[@event.newValue]];
        geometryBuilder.Build(figure);
    }

    private void MakeEuclideanTransformations()
    {
        var x = positionXSlider.value;
        positionXSlider.label = "Позиція X: " + x;

        var y = positionYSlider.value;
        positionYSlider.label = "Позиція Y: " + y;

        var z = positionZSlider.value;
        positionZSlider.label = "Позиція Z: " + z;

        var angleX = rotationXSlider.value;
        rotationXSlider.label = "Поворот X: " + angleX + "°";

        var angleY = rotationYSlider.value;
        rotationYSlider.label = "Поворот Y: " + angleY + "°";

        var angleZ = rotationZSlider.value;
        rotationZSlider.label = "Поворот Z: " + angleZ + "°";

        var translation = new Vector3(x, y, z);
        var angles = new Vector3(angleX, angleY, angleZ);

        geometryBuilder.TransformFigure(translation, angles);
    }

    private void TransformDrawing()
    {
        var x = drawingPositionXSlider.value;
        drawingPositionXSlider.label = "Позиція U: " + x;

        var y = drawingPositionYSlider.value;
        drawingPositionYSlider.label = "Позиція V: " + y;

        var translation = new Vector2(x, y);

        geometryBuilder.TransformDrawing(translation, Vector3.zero);
    }

    private void ResetValues()
    {
        positionXSlider.value = 0;
        positionYSlider.value = 0;
        positionZSlider.value = 0;

        rotationXSlider.value = 0;
        rotationYSlider.value = 0;
        rotationZSlider.value = 0;
    }
}