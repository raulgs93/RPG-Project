using UnityEngine;


namespace RPG.Stats
{

	[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
	public class Progression : ScriptableObject
	{

		[SerializeField] ProgressionCharacterClass[] characterClasses;

		internal float GetHealth(CharacterClass characterClass, int lvl) {

			foreach (ProgressionCharacterClass character in characterClasses) {
				if (character.GetCharacterClass() == characterClass) {
					return character.GetHealth(lvl);
				}
			}

			return 0;
		}


		[System.Serializable]
		class ProgressionCharacterClass
		{

			[SerializeField] CharacterClass characterClass;
			[SerializeField] float[] health;

			public CharacterClass GetCharacterClass() {
				return characterClass;
			}

			public float GetHealth(int lvl) {
				return health[lvl - 1];
			}



		}

	}

}