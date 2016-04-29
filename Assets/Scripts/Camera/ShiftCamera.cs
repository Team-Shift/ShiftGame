using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ShiftCamera : MonoBehaviour{

    public float ProjectionChangeTime = 0.5f;
    public bool ChangeProjection = false;

    private bool _changing = false;
    private float _currentT = 0.0f;

    private void Update()
    {
        if (_changing)
        {
            ChangeProjection = false;
        }
        else if (ChangeProjection)
        {
            _changing = true;
            _currentT = 0.0f;
        }
    }

    /*private void LateUpdate()
    {
        if (!_changing)
        {
            return;
        }

        var currentlyOrthographic = GetComponent<Camera>().orthographic;
        Matrix4x4 orthoMat, persMat;
        if (currentlyOrthographic)
        {
            orthoMat = GetComponent<Camera>().projectionMatrix;

            GetComponent<Camera>().orthographic = false;
            GetComponent<Camera>().ResetProjectionMatrix();
            persMat = GetComponent<Camera>().projectionMatrix;
        }
        else
        {
            persMat = GetComponent<Camera>().projectionMatrix;

            GetComponent<Camera>().orthographic = true;
            GetComponent<Camera>().ResetProjectionMatrix();
            orthoMat = GetComponent<Camera>().projectionMatrix;
        }
        GetComponent<Camera>().orthographic = currentlyOrthographic;

        _currentT += (Time.deltaTime / ProjectionChangeTime);
        if (_currentT < 1.0f)
        {
            if (currentlyOrthographic)
            {
                GetComponent<Camera>().projectionMatrix = MatrixLerp(orthoMat, persMat, _currentT * _currentT);
            }
            else
            {
                GetComponent<Camera>().projectionMatrix = MatrixLerp(persMat, orthoMat, Mathf.Sqrt(_currentT));
            }
        }
        else
        {
            _changing = false;
            GetComponent<Camera>().orthographic = !currentlyOrthographic;
            GetComponent<Camera>().ResetProjectionMatrix();
        }
    }
    */
    private Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float t)
    {
        t = Mathf.Clamp(t, 0.0f, 1.0f);
        var newMatrix = new Matrix4x4();
        newMatrix.SetRow(0, Vector4.Lerp(from.GetRow(0), to.GetRow(0), t));
        newMatrix.SetRow(1, Vector4.Lerp(from.GetRow(1), to.GetRow(1), t));
        newMatrix.SetRow(2, Vector4.Lerp(from.GetRow(2), to.GetRow(2), t));
        newMatrix.SetRow(3, Vector4.Lerp(from.GetRow(3), to.GetRow(3), t));
        return newMatrix;
    }
}
