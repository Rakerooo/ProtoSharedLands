using Proto2.Map;

namespace Proto2.Unit
{
    public class NewProtoTitan : NewProtoUnit<NewProtoRegion>
    {
        public void StartTurn()
        {
            UpdateTargetPos();
            StartCoroutine(MoveToTargetPos());
        }

        private void UpdateTargetPos()
        {
            if (finalTargetPos == currentPos)
            {
                
            }
        }
    }
}
