using OurGame.Currency;

namespace OurGame.Units
{
    public class EnemyUnit : Unit, Pooling.IAvailable
    {
        private bool IsFreeToCreate = true; //notification for the Pooling System

        //private CurrencyManager currencyManager;

        public bool GetAvailability()
        {
            return IsFreeToCreate;
        }

        public void SetAvailability(bool status)
        {
            IsFreeToCreate = status;
        }

        public override void OnEnable()
        {
            base.OnEnable();
            //currencyManager = FindObjectOfType<CurrencyManager>();
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            if (IsDead())
            {
                CurrencyManager._instance.AddRemoveCurrency(10);
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
