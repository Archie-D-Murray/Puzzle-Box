using System;

using UnityEngine;

using Utilities;

namespace Interactable {
    public class Enabler : InteractionReceiver {

        [Serializable]
        class RenderState {
            public MeshRenderer Renderer;
            public Color Enabled;
            public Color Disabled;
            public Collider Collider;

            public RenderState(MeshRenderer renderer, Collider collider) {
                Renderer = renderer;
                Collider = collider;
                Enabled = renderer.material.color;
                Disabled = Enabled;
                Disabled.a = 0.0f;
            }
        }

        [SerializeField] private RenderState[] _renderers;
        [SerializeField] private bool _show = true;
        [SerializeField] private CountDownTimer _timer = new CountDownTimer(1.0f);

        private void Start() {
            MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
            _renderers = new RenderState[renderers.Length];
            for (int i = 0; i < renderers.Length; i++) {
                _renderers[i] = new RenderState(renderers[i], renderers[i].GetComponent<Collider>());
            }
            UpdateRenderState(1f, _show);
            _timer.OnTimerStop += ActionFinish;
        }

        private void FixedUpdate() {
            _timer.Update(Time.fixedDeltaTime);
            if (_timer.IsRunning) {
                UpdateRenderState(_timer.Progress(), _show);
            }
        }

        private void ActionFinish() {
            foreach (RenderState state in _renderers) {
                if (!state.Collider) { continue; }
                state.Collider.enabled = _show;
            }
        }

        private void UpdateRenderState(float progress, bool opaque = true) {
            foreach (RenderState state in _renderers) {
                state.Renderer.material.color = Color.Lerp(state.Disabled, state.Enabled, Helpers.SmoothStep01(opaque ? progress : 1.0f - progress));
                if (state.Collider && progress >= 1.0f) {
                    state.Collider.enabled = opaque;
                }
            }
        }

        public override void AcceptInteraction(InteractionSource source) {
            _show = !_show;
            _timer.Reset();
            _timer.Start();
        }

        public override void CancelInteraction(InteractionSource source) {
            _show = !_show;
            _timer.Reset();
            _timer.Start();
        }
    }
}