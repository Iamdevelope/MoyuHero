// Camera Path 3
// Available on the Unity Asset Store
// Copyright (c) 2013 Jasper Stocker http://support.jasperstocker.com/camera-path/
// For support contact email@jasperstocker.com
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(CameraPathAnimator))]
public class CameraPathAnimatorEditor : Editor
{

    private CameraPathAnimator _animator;
    private CameraPath _cameraPath;

    private const float ASPECT = 1.7777f;
    private const int PREVIEW_RESOLUTION = 800;

    private Vector3 _previewCamPos;
    private Quaternion _previewCamRot;
    private float _previewCamFov;

    private static bool previewSupported
    {
        get
        {
            if (!SystemInfo.supportsRenderTextures) return false;
            if (SystemInfo.graphicsShaderLevel >= 50 && PlayerSettings.useDirect3D11) return false;
            if (!Application.HasProLicense()) return false;

            return true;
        }
    }

    private void OnEnable()
    {
        _animator = (CameraPathAnimator)target;
        _cameraPath = _animator.GetComponent<CameraPath>();

        //Preview Camera
        if(_animator.editorPreview != null)
            DestroyImmediate(_animator.editorPreview);
        if(previewSupported)
        {
            _animator.editorPreview = new GameObject("Animtation Preview Cam");
            _animator.editorPreview.hideFlags = HideFlags.HideAndDontSave;
            _animator.editorPreview.AddComponent<Camera>();
            _animator.editorPreview.camera.fieldOfView = 60;
            _animator.editorPreview.camera.depth = -1;
            //Retreive camera settings from the main camera
            Camera[] cams = Camera.allCameras;
            bool sceneHasCamera = cams.Length > 0;
            Camera sceneCamera = null;
            Skybox sceneCameraSkybox = null;
            if(Camera.main)
            {
                sceneCamera = Camera.main;
            }
            else if(sceneHasCamera)
            {
                sceneCamera = cams[0];
            }

            if(sceneCamera != null)
                sceneCameraSkybox = sceneCamera.GetComponent<Skybox>();
            if(sceneCamera != null)
            {
                _animator.editorPreview.camera.backgroundColor = sceneCamera.backgroundColor;
                if(sceneCameraSkybox != null)
                    _animator.editorPreview.AddComponent<Skybox>().material = sceneCameraSkybox.material;
                else if(RenderSettings.skybox != null)
                    _animator.editorPreview.AddComponent<Skybox>().material = RenderSettings.skybox;
            }
            _animator.editorPreview.camera.enabled = false;
        }

        if(EditorApplication.isPlaying && _animator.editorPreview != null)
            _animator.editorPreview.SetActive(false);
    }

    void OnDisable()
    {
        CleanUp();
    }

    void OnDestroy()
    {
        CleanUp();
    }

    public void OnSceneGUI()
    {
        if (!_cameraPath.showGizmos)
            return;
        if (_cameraPath.transform.rotation != Quaternion.identity)
            return;

        //Axis Gizmo Marker
        Handles.color = CameraPathColours.GREEN;
        Handles.DrawLine(_previewCamPos, _previewCamPos + _previewCamRot * Vector3.up * 0.5f);
        Handles.DrawLine(_previewCamPos, _previewCamPos + _previewCamRot * Vector3.down * 0.5f);
        Handles.color = CameraPathColours.RED;
        Handles.DrawLine(_previewCamPos, _previewCamPos + _previewCamRot * Vector3.left * 0.5f);
        Handles.DrawLine(_previewCamPos, _previewCamPos + _previewCamRot * Vector3.right * 0.5f);
        Handles.color = CameraPathColours.BLUE;
        Handles.DrawLine(_previewCamPos, _previewCamPos + _previewCamRot * Vector3.forward * 0.5f);
        Handles.DrawLine(_previewCamPos, _previewCamPos + _previewCamRot * Vector3.back * 0.5f);

        //Preview Direction Arrow
        float handleSize = HandleUtility.GetHandleSize(_previewCamPos);
        Handles.ArrowCap(0, _previewCamPos, _previewCamRot, handleSize);
        Handles.Label(_previewCamPos, "Preview\nCamera\nPosition");

        //draw line to indicate selected target
        if ((_animator.orientationMode == CameraPathAnimator.orientationModes.followTransform || _animator.orientationMode == CameraPathAnimator.orientationModes.target) && _animator.orientationTarget != null)
        {
            Handles.color = _cameraPath.selectedPointColour;
            Handles.DrawLine(_previewCamPos, _animator.orientationTarget.transform.position);

            string targetLabel = "Animation Orientation CameraPathOnRailsTarget:";
            targetLabel += "\n" + _animator.orientationTarget.name;
            Handles.Label(_animator.orientationTarget.transform.position, targetLabel);
        }

        if (GUI.changed)
            UpdateGui();
    }

    public override void OnInspectorGUI()
    {

        if (_cameraPath.transform.rotation != Quaternion.identity)
        {
            EditorGUILayout.HelpBox("Camera Path does not support rotations of the main game object.", MessageType.Error);
            if(GUILayout.Button("Reset Rotation"))
                _cameraPath.transform.rotation = Quaternion.identity;
            return;
        }

        Undo.RecordObject(_animator,"Modified Animator");

        bool noPath = _cameraPath.realNumberOfPoints < 2;

        EditorGUILayout.BeginVertical(GUILayout.Width(400));
        RenderPreview();


        bool isFollowingTargetTransform = _animator.orientationMode == CameraPathAnimator.orientationModes.followTransform;
        if (isFollowingTargetTransform)
            EditorGUILayout.HelpBox("The Follow Transform mode animates the object to the closest point on the path so it is not based on time.", MessageType.Info);
        EditorGUI.BeginDisabledGroup(isFollowingTargetTransform);
        EditorGUILayout.BeginHorizontal();
        float inputPercent = _animator.editorPercentage * 100;
        float outputPercent = EditorGUILayout.Slider(inputPercent, 0, 100) * 0.01f;
        _animator.editorPercentage = outputPercent;
        EditorGUILayout.LabelField("%", GUILayout.Width(20));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if(_cameraPath.normalised)
            EditorGUILayout.LabelField("Normalised Percentage Value: "+(_cameraPath.CalculateNormalisedPercentage(outputPercent) * 100).ToString("F1")+"%");
        EditorGUILayout.EndHorizontal();
        EditorGUI.EndDisabledGroup();

        AnimateInEditor();

        //Get animation values and apply them to the preview camera
        _previewCamPos = _cameraPath.GetPathPosition(_animator.editorPercentage);
        _previewCamRot = _animator.GetAnimatedOrientation(_animator.editorPercentage,false);

        //ANIMATION TARGETS
        EditorGUILayout.BeginVertical("Box");
        if (_animator.animationObject == null)
            EditorGUILayout.HelpBox("Animation has no target to animate", MessageType.Error);
        _animator.animationObject = (Transform)EditorGUILayout.ObjectField("Animate Object", _animator.animationObject, typeof(Transform), true);
        
        if(_animator.animationObject != null)
        {            
            Rigidbody[] rigidBodies = _animator.animationObject.GetComponentsInChildren<Rigidbody>();
            if (rigidBodies.Length > 0)
                EditorGUILayout.HelpBox("Animation target object contains Rigidbodies. These could produce stuttering animations unless you select 'isKinematic' in the Rigidbody options.", MessageType.Warning);
        }

        if(_animator.orientationMode == CameraPathAnimator.orientationModes.followTransform || _animator.orientationMode == CameraPathAnimator.orientationModes.target)
        {
            if(_animator.orientationTarget == null)
                EditorGUILayout.HelpBox("Orientation target is null",MessageType.Error);
            _animator.orientationTarget = (Transform)EditorGUILayout.ObjectField("Orientation CameraPathOnRailsTarget", _animator.orientationTarget, typeof(Transform), true);     
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Animate Scene Object in Editor");
        _animator.animateSceneObjectInEditor = EditorGUILayout.Toggle(_animator.animateSceneObjectInEditor);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.EndVertical();

        //ANIMATION SETTINGS
        EditorGUILayout.BeginVertical("Box");

        _animator.playOnStart = EditorGUILayout.Toggle("Play on start", _animator.playOnStart);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Animation Mode");
        CameraPathAnimator.animationModes newAnimMode = (CameraPathAnimator.animationModes)EditorGUILayout.EnumPopup(_animator.animationMode);
        EditorGUILayout.EndHorizontal();
        if(newAnimMode != _animator.animationMode)
        {
            _animator.animationMode = newAnimMode;
            _cameraPath.RecalculateStoredValues();
            EditorUtility.SetDirty(_cameraPath);
            EditorUtility.SetDirty(_animator);
        }
        //Help texts explaining the animation modes
        switch(_animator.animationMode)
        {
            case CameraPathAnimator.animationModes.once:
                EditorGUILayout.HelpBox("The animation will run once through, begining at the start of the path and terminating at the end.", MessageType.None);
                break;
            case CameraPathAnimator.animationModes.loop:
                EditorGUILayout.HelpBox("The animation will run continuously, restarting from the beginning once it reaches the end.", MessageType.None);
                break;
            case CameraPathAnimator.animationModes.pingPong:
                EditorGUILayout.HelpBox("The animation will run continuously, reversing the direction of the animation when it reaches the end or start of the path.", MessageType.None);
                break;
            case CameraPathAnimator.animationModes.reverse:
                EditorGUILayout.HelpBox("The animation will run once through, starting at the end and finishing at the start.", MessageType.None);
                break;
            case CameraPathAnimator.animationModes.reverseLoop:
                EditorGUILayout.HelpBox("The animation will run continuously from end to start of the path and restarting once complete.", MessageType.None);
                break;
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Orientation Mode");
        CameraPathAnimator.orientationModes newOrinMode = (CameraPathAnimator.orientationModes)EditorGUILayout.EnumPopup(_animator.orientationMode);
        EditorGUILayout.EndHorizontal();
        if (newOrinMode != _animator.orientationMode)
        {
            if(newOrinMode == CameraPathAnimator.orientationModes.twoDimentions)
            {
                if(EditorUtility.DisplayDialog("Switch to 2D", "Do you want to reset the FOV points to reflect the current orthographic size?", "yes", "no"))
                {
                    int fovPoints = _cameraPath.fovList.realNumberOfPoints;
                    float orthSize = (_animator.isCamera) ? _animator.animationObject.GetComponent<Camera>().orthographicSize : 10;
                    for(int i = 0; i < fovPoints; i++)
                        _cameraPath.fovList[i].FOV = orthSize;
                }
            }

            if(_animator.orientationMode == CameraPathAnimator.orientationModes.twoDimentions)
            {
                if (EditorUtility.DisplayDialog("Switch to 3D", "Do you want to reset the FOV points to reflect the current camera field of view?", "yes", "no"))
                {
                    int fovPoints = _cameraPath.fovList.realNumberOfPoints;
                    float fov = (_animator.isCamera) ? _animator.animationObject.GetComponent<Camera>().fieldOfView : 60;
                    for (int i = 0; i < fovPoints; i++)
                        _cameraPath.fovList[i].FOV = fov;
                }
            }

            _animator.orientationMode = newOrinMode;
            _cameraPath.pointMode = CameraPath.PointModes.Transform;
            _cameraPath.RecalculateStoredValues();
            EditorUtility.SetDirty(_cameraPath);
            EditorUtility.SetDirty(_animator);
            GUI.changed = true;
        }
        //Help texts explaining the orienation modes
        switch (_animator.orientationMode)
        {
            case CameraPathAnimator.orientationModes.custom:
                EditorGUILayout.HelpBox("The orientation of the animated object will follow the custom rotations set out in the path.", MessageType.None);
                break;
            case CameraPathAnimator.orientationModes.followTransform:
                EditorGUILayout.HelpBox("The orientation of the animated object will always face the specified target and will position itself at the nearest point on the path to the target object.", MessageType.None);
                break;
            case CameraPathAnimator.orientationModes.followpath:
                EditorGUILayout.HelpBox("The orientation will match the direction the path is taking from its current position.", MessageType.None);
                break;
            case CameraPathAnimator.orientationModes.mouselook:
                EditorGUILayout.HelpBox("The orientation will be controlled by the user at runtime using the mouse.", MessageType.None);
                break;
            case CameraPathAnimator.orientationModes.reverseFollowpath:
                EditorGUILayout.HelpBox("The orientation will match the reverse direction of the path at its current position.", MessageType.None);
                break;
            case CameraPathAnimator.orientationModes.target:
                EditorGUILayout.HelpBox("The orientation of the animated object will always face the specified target.", MessageType.None);
                break;
            case CameraPathAnimator.orientationModes.twoDimentions:
                EditorGUILayout.HelpBox("The orientation of the animated object will remain facing the Z for a 2D animation.", MessageType.None);
                break;
            case CameraPathAnimator.orientationModes.fixedOrientation:
                EditorGUILayout.HelpBox("The orientation of the animated object will remain at the fixed rotation you specify.", MessageType.None);
                break;
            case CameraPathAnimator.orientationModes.none:
                EditorGUILayout.HelpBox("The orientation of the animated object will not be modified.", MessageType.None);
                break;
        }
        EditorGUILayout.EndVertical();

        //NORMALISATION
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.BeginHorizontal();
        _animator.normalised = EditorGUILayout.Toggle("Normalised Path", _animator.normalised);
        EditorGUILayout.HelpBox("Set this if you want to start another camera path animation once this has completed", MessageType.Info);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        bool usingSpeed = _cameraPath.speedList.listEnabled;
        if (usingSpeed || noPath)
            EditorGUILayout.HelpBox("Using path based speed values.", MessageType.Info);
        EditorGUI.BeginDisabledGroup(usingSpeed);
        
        EditorGUI.BeginDisabledGroup(noPath);
        EditorGUILayout.BeginHorizontal();
        float newPathSpeed = EditorGUILayout.FloatField("Animation Speed", _animator.pathSpeed);
        if (!noPath)
            _animator.pathSpeed = newPathSpeed;
        EditorGUILayout.LabelField("m/sec", GUILayout.Width(25));
        EditorGUILayout.EndHorizontal();
        EditorGUI.EndDisabledGroup();
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        if (_animator.orientationMode == CameraPathAnimator.orientationModes.mouselook)
        {
            EditorGUILayout.HelpBox("Alter the mouse sensitivity here", MessageType.Info);
            _animator.sensitivity = EditorGUILayout.Slider("Mouse Sensitivity", _animator.sensitivity, 0.1f, 2.0f);
            EditorGUILayout.HelpBox("Restrict the vertical viewable area here.", MessageType.Info);
            EditorGUILayout.LabelField("Mouse Y Restriction");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(((int)_animator.minX).ToString("F1"), GUILayout.Width(30));
            EditorGUILayout.MinMaxSlider(ref _animator.minX, ref _animator.maxX, -180, 180);
            EditorGUILayout.LabelField(((int)_animator.maxX).ToString("F1"), GUILayout.Width(30));
            EditorGUILayout.EndHorizontal();
        }

        if (_animator.orientationMode == CameraPathAnimator.orientationModes.fixedOrientation)
        {
            EditorGUILayout.HelpBox("The Orientation will be fixed on this direction", MessageType.Info);
            _animator.fixedOrientaion = EditorGUILayout.Vector3Field("Fixed Orientation Direction", _animator.fixedOrientaion);
        }

        if (GUI.changed)
            UpdateGui();
        EditorGUILayout.EndVertical();
    }

    private void RenderPreview()
    {
        if (_cameraPath.realNumberOfPoints < 2)
            return;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Preview");
        string showPreviewButtonLabel = (_animator.showPreview) ? "hide" : "show";
        if (GUILayout.Button(showPreviewButtonLabel, GUILayout.Width(74)))
            _animator.showPreview = !_animator.showPreview;
        EditorGUILayout.EndHorizontal();

        if (!_cameraPath.enablePreviews || !_animator.showPreview)
            return;

        GameObject editorPreview = _animator.editorPreview;
        if (previewSupported && !EditorApplication.isPlaying)
        {

            RenderTexture rt = RenderTexture.GetTemporary(PREVIEW_RESOLUTION, Mathf.RoundToInt(PREVIEW_RESOLUTION / ASPECT), 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1);

            editorPreview.SetActive(true);
            editorPreview.transform.position = _previewCamPos;
            editorPreview.transform.rotation = _previewCamRot;

            Camera previewCam = editorPreview.camera;
            if (_animator.orientationMode == CameraPathAnimator.orientationModes.twoDimentions)
            {
                previewCam.orthographic = true;
                previewCam.orthographicSize = _cameraPath.GetPathFOV(_animator.editorPercentage);
            }
            else
            {
                previewCam.orthographic = false;
                previewCam.fieldOfView = _cameraPath.GetPathFOV(_animator.editorPercentage);
            }
            previewCam.enabled = true;
            previewCam.targetTexture = rt;
            previewCam.Render();
            previewCam.targetTexture = null;
            previewCam.enabled = false;
            editorPreview.SetActive(false);

            GUILayout.Label(rt, GUILayout.Width(400), GUILayout.Height(225));
            RenderTexture.ReleaseTemporary(rt);
        }
        else
        {
            string errorMsg = (!previewSupported) ? "Preview is not supported, sorry." : "No Preview When Playing.";
            EditorGUILayout.LabelField(errorMsg, GUILayout.Height(225));
        }
    }

    private void AnimateInEditor()
    {
        if (_animator.animateSceneObjectInEditor)
        {
            _animator.animationObject.transform.position = _previewCamPos;
            _animator.animationObject.transform.rotation = _previewCamRot;
            Camera targetCamera = _animator.animationObject.GetComponent<Camera>();
            if (targetCamera != null)
                targetCamera.fieldOfView = _cameraPath.fovList.GetFOV(_animator.editorPercentage);
        }
    }

    /// <summary>
    /// Called to ensure we're not leaking stuff into the Editor
    /// </summary>
    public void CleanUp()
    {
        if(_animator.editorPreview != null)
            DestroyImmediate(_animator.editorPreview);
    }

    /// <summary>
    /// Handle GUI changes and repaint requests
    /// </summary>
    private void UpdateGui()
    {
        Repaint();
        HandleUtility.Repaint();
        SceneView.RepaintAll();
        _cameraPath.RecalculateStoredValues();
        EditorUtility.SetDirty(_cameraPath);
        EditorUtility.SetDirty(_animator);
    }
}
