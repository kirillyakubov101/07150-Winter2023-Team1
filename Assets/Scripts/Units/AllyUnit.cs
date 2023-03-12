namespace OurGame.Units
{
    public class AllyUnit : Unit
    {
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if(IsDead())
            {
                DescendToDeath();
            }
            
        }

        private void DescendToDeath()
        {
            Destroy(gameObject,2f);
        }
    }
}

