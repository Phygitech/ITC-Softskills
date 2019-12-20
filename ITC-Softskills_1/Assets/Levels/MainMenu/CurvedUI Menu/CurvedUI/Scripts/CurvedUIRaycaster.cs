using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


namespace CurvedUI
{
    public class CurvedUIRaycaster : GraphicRaycaster
    {

        [SerializeField]
        bool showDebug = false;

        Canvas myCanvas;
        CurvedUISettings mySettings;
        Vector3 cyllinderMidPoint;

        List<GameObject> lastHovered;

        Vector2 lastCanvasPos = Vector2.zero;

        #region LIFECYCLE

        protected override void Awake()
        {
            base.Awake();
            myCanvas = GetComponent<Canvas>();
            mySettings = GetComponent<CurvedUISettings>();

            cyllinderMidPoint = new Vector3(0, 0, -mySettings.GetCyllinderRadiusInCanvasSpace());

            //the canvas needs an event camera set up to process events correctly. Try to use main camera if no one is provided.
            if (myCanvas.worldCamera == null && Camera.main != null)
                myCanvas.worldCamera = Camera.main;
        }

        protected override void Start()
        {
            
        }

        #endregion


        #region RAYCASTING

        public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
        {

            if (!mySettings.Interactable)
                return;

            //check if we have a world camera to process events by
            if (myCanvas.worldCamera == null)
                Debug.LogWarning("CurvedUIRaycaster requires Canvas to have a world camera reference to process events!", myCanvas.gameObject);

            Camera worldCamera = myCanvas.worldCamera;
            Ray ray3D;

            //get a ray to raycast with depending on the control method
            switch (mySettings.Controller)
            {
                case CurvedUISettings.CurvedUIController.MOUSE:
                    {
                        // Get a ray from the camera through the point on the screen - used for mouse input
                        ray3D = worldCamera.ScreenPointToRay(eventData.position);
                        break;
                    }
                case CurvedUISettings.CurvedUIController.GAZE:
                    {
                        //get a ray from the center of world camera. used for gaze input
                        ray3D = new Ray(worldCamera.transform.position, worldCamera.transform.forward);

                        bool selectableUnderGaze = false;

                        //find if our selected object is still under gaze
                        foreach (GameObject go in eventData.hovered)
                        {
                            if (go == eventData.selectedObject)
                            {
                                selectableUnderGaze = true;
                                break;
                            }
                        }

                        //deselect if its not
                        if (!selectableUnderGaze)
                            eventData.selectedObject = null;

                        foreach (GameObject go in eventData.hovered)
                        {
                            if (go == null)
                                continue;

                            Graphic gph = go.GetComponent<Graphic>();

                            //go through only go that can be selected and are drawn by the canvas
#if UNITY_5_1
                    if (go.GetComponent<Selectable>() != null && gph != null && gph.depth != -1)
#else
                            if (go.GetComponent<Selectable>() != null && gph != null && gph.depth != -1 && gph.raycastTarget)
#endif
                        {
                                if (eventData.selectedObject != go)
                                    eventData.selectedObject = go;

                                break;
                            }
                        }

                        //Test for selected object being dragged and initialize dragging, if needed.
                        //We do this here to trick unity's StandAloneInputModule into thinking we used a touch or mouse to do it.
                        if (eventData.IsPointerMoving() && eventData.pointerDrag != null
                            && !eventData.dragging
                            && ShouldStartDrag(eventData.pressPosition, eventData.position, EventSystem.current.pixelDragThreshold, eventData.useDragThreshold))
                        {
                            ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.beginDragHandler);
                            eventData.dragging = true;
                        }


                        break;
                    }
                case CurvedUISettings.CurvedUIController.WORLD_MOUSE:
                    {
                        // Get a ray set in CustromControllerRay property
                        ray3D = new Ray(worldCamera.transform.position, (mySettings.CanvasToCurvedCanvas(mySettings.WorldSpaceMouseInCanvasSpace) - myCanvas.worldCamera.transform.position));
                        break;
                    }
                case CurvedUISettings.CurvedUIController.VIVE:
                    {
                        // Get a ray from right controller.

#if CURVEDUI_VIVE
                    ray3D = new Ray((eventData as CurvedUIPointerEventData).Controller.transform.position, (eventData as CurvedUIPointerEventData).Controller.transform.forward);

                    break;
#else
                        goto case CurvedUISettings.CurvedUIController.CUSTOM_RAY;
#endif

                    }
                case CurvedUISettings.CurvedUIController.CUSTOM_RAY:
                    {
                        // Get a ray set in CustromControllerRay property
                        ray3D = mySettings.CustomControllerRay;

                        break;
                    }
                default:
                    {
                        ray3D = new Ray();
                        break;
                    }
            }

            if (mySettings.Angle != 0 && mySettings.enabled)
            { // use custom raycasting only if Curved effect is enabled
                Vector2 remappedPosition = eventData.position;

                //Test only this object's layer if settings require it.
                int myLayerMask = -1;
                if (mySettings.RaycastMyLayerOnly)
                {
                    myLayerMask = 1 << this.gameObject.layer;
                }

                switch (mySettings.Shape)
                {
                //find if we hit anything, if not, do nothing
                    case CurvedUISettings.CurvedUIShape.CYLINDER:
                        {
                            if (!RaycastToCyllinderCanvas(ray3D, out remappedPosition, false, myLayerMask))
                                return;
                            break;
                        }
                    case CurvedUISettings.CurvedUIShape.CYLINDER_VERTICAL:
                        {
                            if (!RaycastToCyllinderVerticalCanvas(ray3D, out remappedPosition, false, myLayerMask))
                                return;
                            break;
                        }
                    case CurvedUISettings.CurvedUIShape.RING:
                        {
                            if (!RaycastToRingCanvas(ray3D, out remappedPosition, false, myLayerMask))
                                return;
                            break;
                        }
                    case CurvedUISettings.CurvedUIShape.SPHERE:
                        {
                            if (!RaycastToSphereCanvas(ray3D, out remappedPosition, false, myLayerMask))
                                return;
                            break;
                        }
                }

                // Update event data
                eventData.position = remappedPosition;

                //update delta for vive
                if (mySettings.Controller == CurvedUISettings.CurvedUIController.VIVE)
                {
                    eventData.delta = remappedPosition - lastCanvasPos;
                    lastCanvasPos = remappedPosition;
                }
               
            }

            //store objects under pointer so they can quickly retrieved if needed by other scripts
            lastHovered = eventData.hovered;

            // Use base class raycast method to finish the raycast if we hit anything
            base.Raycast(eventData, resultAppendList);
        }



        public virtual bool RaycastToCyllinderCanvas(Ray ray3D, out Vector2 o_canvasPos, bool OutputInCanvasSpace = false, int myLayerMask = -1)
        {

            if (showDebug)
            {
                Debug.DrawLine(ray3D.origin, ray3D.GetPoint(1000), Color.red);
            }

            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray3D, out hit, float.PositiveInfinity, myLayerMask))
            {

                //direction from the cyllinder center to the hit point
                Vector3 localHitPoint = myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(hit.point);
                Vector3 directionFromCyllinderCenter = (localHitPoint - cyllinderMidPoint).normalized;

                //angle between middle of the projected canvas and hit point direction
                float angle = -AngleSigned(directionFromCyllinderCenter.ModifyY(0), mySettings.Angle < 0 ? Vector3.back : Vector3.forward, Vector3.up);

                //convert angle to canvas coordinates
                Vector2 canvasSize = myCanvas.GetComponent<RectTransform>().rect.size;

                //map the intersection point to 2d point in canvas space
                Vector2 pointOnCanvas = new Vector3(0, 0, 0);
                pointOnCanvas.x = angle.Remap(-mySettings.Angle / 2.0f, mySettings.Angle / 2.0f, -canvasSize.x / 2.0f, canvasSize.x / 2.0f);
                pointOnCanvas.y = localHitPoint.y;


                if (OutputInCanvasSpace)
                    o_canvasPos = pointOnCanvas;
                else //convert the result to screen point in camera. This will be later used by raycaster and world camera to determine what we're pointing at
                    o_canvasPos = myCanvas.worldCamera.WorldToScreenPoint(myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(pointOnCanvas));

                if (showDebug)
                {
                    //Debug.DrawLine(canvasWorldPoint, canvasWorldPoint.ModifyY(canvasWorldPoint.y + 10), Color.blue);
                    Debug.DrawLine(hit.point, hit.point.ModifyY(hit.point.y + 10), Color.green);
                    Debug.DrawLine(hit.point, myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(cyllinderMidPoint), Color.yellow);
                }

                return true;
            }

            o_canvasPos = Vector2.zero;
            return false;
        }

        public virtual bool RaycastToCyllinderVerticalCanvas(Ray ray3D, out Vector2 o_canvasPos, bool OutputInCanvasSpace = false, int myLayerMask = -1)
        {

            if (showDebug)
            {
                Debug.DrawLine(ray3D.origin, ray3D.GetPoint(1000), Color.red);
            }

            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray3D, out hit, float.PositiveInfinity, myLayerMask))
            {

                //direction from the cyllinder center to the hit point
                Vector3 localHitPoint = myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(hit.point);
                Vector3 directionFromCyllinderCenter = (localHitPoint - cyllinderMidPoint).normalized;

                //angle between middle of the projected canvas and hit point direction
                float angle = -AngleSigned(directionFromCyllinderCenter.ModifyX(0), mySettings.Angle < 0 ? Vector3.back : Vector3.forward, Vector3.left);

                //convert angle to canvas coordinates
                Vector2 canvasSize = myCanvas.GetComponent<RectTransform>().rect.size;

                //map the intersection point to 2d point in canvas space
                Vector2 pointOnCanvas = new Vector3(0, 0, 0);
                pointOnCanvas.y = angle.Remap(-mySettings.Angle / 2.0f, mySettings.Angle / 2.0f, -canvasSize.y / 2.0f, canvasSize.y / 2.0f);
                pointOnCanvas.x = localHitPoint.x;


                if (OutputInCanvasSpace)
                    o_canvasPos = pointOnCanvas;
                else //convert the result to screen point in camera. This will be later used by raycaster and world camera to determine what we're pointing at
                    o_canvasPos = myCanvas.worldCamera.WorldToScreenPoint(myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(pointOnCanvas));

                if (showDebug)
                {
                    //Debug.DrawLine(canvasWorldPoint, canvasWorldPoint.ModifyY(canvasWorldPoint.y + 10), Color.blue);
                    Debug.DrawLine(hit.point, hit.point.ModifyY(hit.point.y + 10), Color.green);
                    Debug.DrawLine(hit.point, myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(cyllinderMidPoint), Color.yellow);
                }

                return true;
            }

            o_canvasPos = Vector2.zero;
            return false;
        }

        public virtual bool RaycastToRingCanvas(Ray ray3D, out Vector2 o_canvasPos, bool OutputInCanvasSpace = false, int myLayerMask = -1)
        {

            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray3D, out hit, float.PositiveInfinity, myLayerMask))
            {

                //local hit point on canvas and a direction from center
                Vector3 localHitPoint = myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(hit.point);
                Vector3 directionFromRingCenter = localHitPoint.ModifyZ(0).normalized;

                Vector2 canvasSize = myCanvas.GetComponent<RectTransform>().rect.size;

                //angle between middle of the projected canvas and hit point direction from center
                float angle = -AngleSigned(directionFromRingCenter.ModifyZ(0), Vector3.up, Vector3.back);

                //map the intersection point to 2d point in canvas space
                Vector2 pointOnCanvas = new Vector2(0, 0);

                if (showDebug)
                    Debug.Log("angle: " + angle);

                //map x coordinate based on angle between vector up and direction to hitpoint
                if (angle < 0)
                {
                    pointOnCanvas.x = angle.Remap(0, -mySettings.Angle, -canvasSize.x / 2.0f, canvasSize.x / 2.0f);
                }
                else
                {
                    pointOnCanvas.x = angle.Remap(360, 360 - mySettings.Angle, -canvasSize.x / 2.0f, canvasSize.x / 2.0f);
                }

                //map y coordinate based on hitpoint distance from the center and external diameter
                pointOnCanvas.y = localHitPoint.magnitude.Remap(mySettings.RingExternalDiameter * 0.5f * (1 - mySettings.RingFill), mySettings.RingExternalDiameter * 0.5f,
                    -canvasSize.y * 0.5f * (mySettings.RingFlipVertical ? -1 : 1), canvasSize.y * 0.5f * (mySettings.RingFlipVertical ? -1 : 1));


                if (OutputInCanvasSpace)
                    o_canvasPos = pointOnCanvas;
                else //convert the result to screen point in camera. This will be later used by raycaster and world camera to determine what we're pointing at
                    o_canvasPos = myCanvas.worldCamera.WorldToScreenPoint(myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(pointOnCanvas));
                return true;
            }

            o_canvasPos = Vector2.zero;
            return false;
        }


        public virtual bool RaycastToSphereCanvas(Ray ray3D, out Vector2 o_canvasPos, bool OutputInCanvasSpace = false, int myLayerMask = -1)
        {

            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray3D, out hit, float.PositiveInfinity, myLayerMask))
            {

                Vector2 canvasSize = myCanvas.GetComponent<RectTransform>().rect.size;
                float radius = (mySettings.PreserveAspect ? mySettings.GetCyllinderRadiusInCanvasSpace() : canvasSize.x / 2.0f);

                //local hit point on canvas, direction from its center and a vector perpendicular to direction, so we can use it to calculate its angle in both planes.
                Vector3 localHitPoint = myCanvas.transform.worldToLocalMatrix.MultiplyPoint3x4(hit.point);
                Vector3 SphereCenter = new Vector3(0, 0, mySettings.PreserveAspect ? -radius : 0);
                Vector3 directionFromSphereCenter = (localHitPoint - SphereCenter).normalized;
                Vector3 XZPlanePerpendicular = Vector3.Cross(directionFromSphereCenter, directionFromSphereCenter.ModifyY(0)).normalized * (directionFromSphereCenter.y < 0 ? 1 : -1);

                //horizontal and vertical angle between middle of the sphere and the hit point.
                //We do some fancy checks to determine vectors we compare them to,
                //to make sure they are negative on the left and bottom side of the canvas
                float hAngle = -AngleSigned(directionFromSphereCenter.ModifyY(0), (mySettings.Angle > 0 ? Vector3.forward : Vector3.back), (mySettings.Angle > 0 ? Vector3.up : Vector3.down));
                float vAngle = -AngleSigned(directionFromSphereCenter, directionFromSphereCenter.ModifyY(0), XZPlanePerpendicular);

                //find the size of the canvas expressed as measure of the arc it occupies on the sphere
                float hAngularSize = Mathf.Abs(mySettings.Angle) * 0.5f;
                float vAngularSize = Mathf.Abs(mySettings.PreserveAspect ? hAngularSize * canvasSize.y / canvasSize.x : mySettings.VerticalAngle * 0.5f);

                //map the intersection point to 2d point in canvas space
                Vector2 pointOnCanvas = new Vector2(hAngle.Remap(-hAngularSize, hAngularSize, -canvasSize.x * 0.5f, canvasSize.x * 0.5f),
                                            vAngle.Remap(-vAngularSize, vAngularSize, -canvasSize.y * 0.5f, canvasSize.y * 0.5f));

                if (showDebug)
                {
                    Debug.Log("h: " + hAngle + " / v: " + vAngle + " poc: " + pointOnCanvas);
                    Debug.DrawRay(myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(SphereCenter), myCanvas.transform.localToWorldMatrix.MultiplyVector(directionFromSphereCenter) * Mathf.Abs(radius), Color.red);
                    Debug.DrawRay(myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(SphereCenter), myCanvas.transform.localToWorldMatrix.MultiplyVector(XZPlanePerpendicular) * 300, Color.magenta);
                }

                if (OutputInCanvasSpace)
                    o_canvasPos = pointOnCanvas;
                else // convert the result to screen point in camera.This will be later used by raycaster and world camera to determine what we're pointing at
                    o_canvasPos = myCanvas.worldCamera.WorldToScreenPoint(myCanvas.transform.localToWorldMatrix.MultiplyPoint3x4(pointOnCanvas));

                return true;
            }

            o_canvasPos = Vector2.zero;
            return false;
        }

        #endregion


        #region SUPPORT FUNCTIONS

        /// <summary>
        /// Determine the signed angle between two vectors, with normal 'n'
        /// as the rotation axis.
        /// </summary>
        float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
        {
            return Mathf.Atan2(
                Vector3.Dot(n, Vector3.Cross(v1, v2)),
                Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
        }

        private bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
        {
            if (!useDragThreshold)
                return true;

            return (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
        }

        #endregion

        #region PUBLIC

        public void RebuildCollider()
        {
            cyllinderMidPoint = new Vector3(0, 0, -mySettings.GetCyllinderRadiusInCanvasSpace());
        }

        public List<GameObject> GetObjectsUnderPointer()
        {
            if (lastHovered == null)
                lastHovered = new List<GameObject>();
            return lastHovered;
        }

        #endregion



        #region TESSELATION

        void ModifyQuad(List<Vector3> verts, int vertexIndex, Vector2 requiredSize)
        {

            // Read the existing quad vertices
            List<Vector3> quad = new List<Vector3>();
            for (int i = 0; i < 4; i++)
                quad.Add(verts[vertexIndex + i]);

            // horizotal and vertical directions of a quad. We're going to tesselate parallel to these.
            Vector3 horizontalDir = quad[2] - quad[1];
            Vector3 verticalDir = quad[1] - quad[0];

            // Find how many quads we need to create
            int horizontalQuads = Mathf.CeilToInt(horizontalDir.magnitude * (1.0f / Mathf.Max(1.0f, requiredSize.x)));
            int verticalQuads = Mathf.CeilToInt(verticalDir.magnitude * (1.0f / Mathf.Max(1.0f, requiredSize.y)));

            // Create the quads!
            float yStart = 0.0f;
            for (int y = 0; y < verticalQuads; ++y)
            {

                float yEnd = (y + 1.0f) / verticalQuads;
                float xStart = 0.0f;

                for (int x = 0; x < horizontalQuads; ++x)
                {
                    float xEnd = (x + 1.0f) / horizontalQuads;

                    //Add new quads to list
                    verts.Add(TesselateQuad(quad, xStart, yStart));
                    verts.Add(TesselateQuad(quad, xStart, yEnd));
                    verts.Add(TesselateQuad(quad, xEnd, yEnd));
                    verts.Add(TesselateQuad(quad, xEnd, yStart));

                    //begin the next quad where we ened this one
                    xStart = xEnd;
                }
                //begin the next row where we ended this one
                yStart = yEnd;
            }
        }





        Vector3 TesselateQuad(List<Vector3> quad, float x, float y)
        {

            Vector3 ret = Vector3.zero;

            //1. calculate weighting factors
            List<float> weights = new List<float>()
            {
                (1 - x) * (1 - y),
                (1 - x) * y,
                x * y,
                x * (1 - y),
            };

            //2. interpolate pos using weighting factors
            for (int i = 0; i < 4; i++)
            {
                ret += quad[i] * weights[i];
            }
            return ret;
        }

        #endregion

    }
}
