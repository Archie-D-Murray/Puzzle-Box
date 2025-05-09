using System;

using UnityEngine;

namespace Utilities {
    public class Helpers : Singleton<Helpers> {
        protected override void Awake() {
            base.Awake();
            MainCamera = Camera.main;
        }
        public Camera MainCamera;
        public static Vector2 FromRadians(float radians) {
            return new Vector2(Mathf.Sin(radians), Mathf.Cos(radians));
        }

        public float AngleToMouse(Transform obj) {
            return Vector2.SignedAngle(
                Vector2.up,
                ((Vector2)MainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)obj.position).normalized
            );
        }

        public float AngleToMouseOpposite(Transform obj) {
            return Vector2.SignedAngle(
                ((Vector2)MainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)obj.position).normalized,
                Vector2.up
            );
        }

        public Vector2 WorldMousePosition() {
            return MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        public Vector2 VectorToMouse(Vector2 position) {
            return (WorldMousePosition() - position).normalized;
        }

        public static float ClampAngle(float value, float min, float max) {
            while (value < -360.0f) { value += 360.0f; }
            while (value > +360.0f) { value -= 360.0f; }
            return Mathf.Clamp(value, min, max);
        }

        public static float SmoothStep01(float value) {
            return Mathf.SmoothStep(0.0f, 1.0f, value);
        }
    }
}