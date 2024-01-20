using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.Netcode;
using Wolf;

namespace Wolf
{
    public class ColorPickerUI : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        enum EditTarget { H, SV }
        public delegate void OnColorChangedCallBack(Color col);

        public OnColorChangedCallBack OnColorChanged;
        [Header("マテリアルはカラーピッカーにつきひとつづつ。")]
        public Color color;

        private EditTarget editTarget;
        private float squareSize = 1f;
        private float circleIn = 1f;

        private float h;
        private float s;
        private float v;

        [SerializeField] private Image im;
        private RectTransform r;

        private void Awake()
        {
            r = GetComponent<RectTransform>();
            squareSize = im.material.GetFloat("_square");
            circleIn = im.material.GetFloat("_circleIn");

            Color.RGBToHSV(color, out h, out s, out v);
            im.material.SetFloat("_hue", h);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 localPoint = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                r,
                eventData.position, eventData.pressEventCamera, out localPoint
                );

            _SetColor(localPoint);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Vector2 localPoint = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                r,
                eventData.position, eventData.pressEventCamera, out localPoint
                );

            if (localPoint.magnitude >= r.rect.width / 2 * circleIn) editTarget = EditTarget.H;
            else editTarget = EditTarget.SV;

            _SetColor(localPoint);
        }

        void _SetColor(Vector2 MousePos)
        {
            switch (editTarget)
            {
                case EditTarget.H:
                    Vector2 vector = MousePos;
                    float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
                    if (angle < 0) angle += 360;    // correct angle.
                                                    // fix to match color circle.
                    angle = 360 - angle;
                    angle += 90;
                    if (angle > 360) angle -= 360;
                    h = angle / 360;
                    Debug.Log(string.Format("{0} {1}", MousePos, angle));
                    break;
                case EditTarget.SV:
                    float squareRealSize = r.rect.width * squareSize;
                    Vector2 fixedPos = (MousePos + new Vector2(squareRealSize / 2, squareRealSize / 2)) / squareRealSize;
                    s = Mathf.Clamp01(fixedPos.x);
                    v = Mathf.Clamp01(fixedPos.y);
                    break;
            }
            color = Color.HSVToRGB(h, s, v);
            im.material.SetFloat("_hue", h);
            if (OnColorChanged != null) OnColorChanged.Invoke(color);
        }

        public void SetColorWithoutNotify(Color col)
        {
            color = col;
            Color.RGBToHSV(color, out h, out s, out v);
            im.material.SetFloat("_hue", h);
        }


    }
}