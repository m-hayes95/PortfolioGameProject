using DG.Tweening;
using UnityEngine;

namespace Arcade
{
    public class CraneGameMoveClaw : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float duration;
        [SerializeField] private float xAmount, yAmount, zAmount;
        private bool useCraneControls;
        private void Start()
        {
            StartClawGrabSequence();
        }

        private void StartClawGrabSequence()
        {
            var moveClaw = DOTween.Sequence();
            moveClaw.Append(transform.DOLocalMoveX(transform.position.x + xAmount, duration));
            moveClaw.Append(transform.DOLocalMoveZ(transform.position.z + zAmount , duration));
            moveClaw.Append(transform.DOLocalMoveY(transform.position.y - yAmount, duration));
            moveClaw.PrependInterval(2f);
            moveClaw.OnComplete(ReturnClawHome);
        }

        private void ReturnClawHome()
        {
            var moveClaw = DOTween.Sequence();
            moveClaw.Append(transform.DOLocalMoveY(transform.position.y + yAmount, duration));
            moveClaw.Append(transform.DOLocalMoveZ(transform.position.z - zAmount , duration));
            moveClaw.Append(transform.DOLocalMoveX(transform.position.x - xAmount, duration));
            moveClaw.OnComplete(OnReturnHome);
        }

        private void OnReturnHome()
        {
            // Drop item
        }
    }
}

