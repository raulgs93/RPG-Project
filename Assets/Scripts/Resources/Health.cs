using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Resources
{
	public class Health : MonoBehaviour, ISaveable
	{

		[SerializeField] float health = 100f;

		bool isAlive = true;

		private void Start() {
			health = GetComponent<BaseStats>().GetHealth();
		}


		public void TakeDamage(float damage) {

			health = Mathf.Max(health - damage, 0);

			if (isAlive) {
				if (health == 0) {
					Die();
				}
				else {
					GetHit();
				}
			}

		}

		public void Die() {
			GetComponent<Animator>().SetTrigger("die");
			isAlive = false;
			GetComponent<ActionScheduler>().CancelAction();
		}

		public void GetHit() {
			GetComponent<Animator>().SetTrigger("getHit");

		}

		public bool GetIsAlive() {
			return isAlive;
		}


		public float GetPercentage() {
			return (health / GetComponent<BaseStats>().GetHealth()) * 100;
		}

		public object CaptureState() {
			return health;
		}

		public void RestoreState(object state) {
			health = (float)state;

			if (health == 0) {
				Die();
			}

		}
	}

}
