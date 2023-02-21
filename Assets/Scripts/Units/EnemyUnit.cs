namespace OurGame.Units
{
    public class EnemyUnit : Unit, Pooling.IAvailable
    {
        private bool IsFreeToCreate = true; //notification for the Pooling System

        public bool GetAvailability()
        {
            return IsFreeToCreate;
        }

        public void SetAvailability(bool status)
        {
            IsFreeToCreate = status;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            if (IsDead())
            {
                Invoke(nameof(DieAndMoveToPool), 2.5f);
            }

        }

        private void DieAndMoveToPool()
        {
            gameObject.SetActive(false);
            this.SetAvailability(true);
        }
    }
}
